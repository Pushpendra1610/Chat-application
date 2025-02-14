using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment4
{
    internal class ChatHub:Hub
    {
        /// <summary>
        /// Maps connection IDs to usernames
        /// </summary>
        static Dictionary<string, string> connections = new Dictionary<string, string>();

        /// <summary>
        /// Run when a user attempts to connect. Validate username and, if valid, send backlog and notify all users
        /// </summary>
        /// <param name="username">Username the user wishes to use</param>
        public async Task Connect(string username)
        {
            username = username.Trim();
            bool validName = Regex.Match(username, "^[a-z]{1,10}$", RegexOptions.IgnoreCase | RegexOptions.Singleline).Success;
            bool freeName = !connections.ContainsValue(username) || username == "System";

            string errorMessage = "";
            
            if (!validName)
            {
                errorMessage = "Usernames must contain only letters and be 1 to 10 characters long.";
            }
            else if (!freeName)
            {
                errorMessage = $"Username \"{username}\" is already in use. ";
            }
            else
            {
                connections[Context.ConnectionId] = username;
            }

            DateTimeOffset connectionTime = DateTimeOffset.Now;

            Task acceptConnection = Clients.Caller.SendAsync("Connect", validName && freeName, errorMessage, username);
            Task sendConnectionNotice = Clients.AllExcept(Context.ConnectionId).SendAsync(
                "ReceiveMessage", connectionTime.ToString("g"), "System", $"User {connections[Context.ConnectionId]} has connected.");

            using (var db = new ChatContext())
            {
                List<ChatMessage> query = await db.Messages.OrderBy(m => m.ChatMessageReceived).ToListAsync();
                foreach (ChatMessage message in query)
                {
                    // Backlog is sent one at a time to ensure order
                    await Clients.Caller.SendAsync("ReceiveMessage", message.ChatMessageReceived.ToString("g"), message.ChatMessageUsername, message.ChatMessageContent);
                }
            } 
            
            await Clients.Caller.SendAsync("ReceiveMessage", connectionTime.ToString("g"), "System", $"User {connections[Context.ConnectionId]} has connected.");
            await acceptConnection;
            await sendConnectionNotice;

        }

        /// <summary>
        /// Get messages from clients, validate them and send to other users
        /// </summary>
        /// <param name="message">Content of the message</param>
        public async Task SendMessage(string message)
        {
            DateTimeOffset timeReceived = DateTimeOffset.Now;
            string trimmedMessage = message.Trim();
            bool valid = trimmedMessage.Length <= 140 && trimmedMessage.Length > 0;

            if (!valid)
            {
                string errorMessage = trimmedMessage.Length == 0 ? "Messages must not contain only whitespace." : "Messages must be 140 characters at most.";
                await Clients.Caller.SendAsync("MessageSent", valid, message, timeReceived, errorMessage);
                return;
            }

            Task ack = Clients.Caller.SendAsync("MessageSent", valid, message, timeReceived.ToString("g"), "");

            using (var db = new ChatContext())
            {
                db.Messages.Add(
                    new ChatMessage
                    {
                        ChatMessageId = Guid.NewGuid(),
                        ChatMessageUsername = connections[Context.ConnectionId],
                        ChatMessageContent = trimmedMessage,
                        ChatMessageReceived = timeReceived,
                    }
                );

                await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", timeReceived.ToString("g"), connections[Context.ConnectionId], trimmedMessage);
                await db.SaveChangesAsync();
                await ack;
            }
        }

        /// <summary>
        /// Disconnect a user, freeing their username and informing all other connected users
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Task notify = Clients.AllExcept(Context.ConnectionId).SendAsync(
                "ReceiveMessage", DateTimeOffset.Now.ToString("g"), "System", $"User {connections[Context.ConnectionId]} has disconnected.");
            connections.Remove(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
            await notify;
        }



    }
}