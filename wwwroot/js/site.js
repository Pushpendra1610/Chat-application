// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

let chatInitialized = false;

let connection = new signalR.HubConnectionBuilder().withUrl('/chat').build();

connection.on('Connect', connectToChat);
connection.on('MessageSent', messageSent);
connection.on('ReceiveMessage', receiveMessage);
connection.start();

let username = '';

// Attempt to connect with the chosen username
function submitUsername() {
    username = document.getElementById('username').value;
    connection.invoke('Connect', username);
}

document.getElementById('connect').addEventListener('click', submitUsername);
document.getElementById('username').addEventListener('keyup', function (e) {
    if (e.keyCode == 13 && this.value.length > 0) {
        submitUsername();
    }
});

// Adds new messages to the chat log
function receiveMessage(datetime, username, content) {
    if (!chatInitialized) {
        return;
    }

    const rows = document.getElementById('rows');
    const newRow = document.createElement('tr');

    const datetimeColumn = document.createElement('td');
    datetimeColumn.innerText = datetime;
    const usernameColumn = document.createElement('td');
    usernameColumn.innerText = username;
    const contentColumn = document.createElement('td');
    contentColumn.innerText = content;

    newRow.appendChild(datetimeColumn);
    newRow.appendChild(usernameColumn);
    newRow.appendChild(contentColumn);

    rows.appendChild(newRow);

    const chatLog = document.getElementById('chatLog');
    chatLog.scrollTop = chatLog.scrollHeight;
}

// Verifies that a message was sent sucessfully
function messageSent(success, message, datetime, errorMessage) {
    const messageArea = document.getElementById('messageArea');
    messageArea.placeholder = 'Type your message here...';
    messageArea.disabled = false;
    if (success) {
        receiveMessage(datetime, username, message);
    } else {
        messageArea.value = message;
        document.getElementById('chatError').innerText = errorMessage;
    }
    messageArea.focus();
}

// Sets up the chat log when a connection is established
function initializeChat() {
    const chatDiv = document.createElement('div');
    chatDiv.id = 'chatArea';
    chatDiv.classList.add('fill');

    const welcomeMessage = document.createElement('h1');
    welcomeMessage.classList.add('display-4');
    welcomeMessage.innerText = `Welcome, ${username}!`;
    chatDiv.appendChild(welcomeMessage);

    const chatLog = document.createElement('div');
    chatLog.id = 'chatLog';
    chatDiv.appendChild(chatLog);

    const rows = document.createElement('table');
    rows.id = 'rows';
    chatLog.appendChild(rows);

    const messageArea = document.createElement('input');
    messageArea.id = 'messageArea';
    messageArea.placeholder = 'Type your message here...';
    // Send messages
    messageArea.addEventListener('keyup', function (e) {
        if (e.keyCode == 13 && this.value.length > 0) {
            connection.invoke('SendMessage', this.value);
            this.value = '';
            this.placeholder = 'Sending...';
            this.disabled = true;
            document.getElementById('chatError').innerText = '';
        }
    });
    chatDiv.appendChild(messageArea);

    const chatError = document.createElement('p');
    chatError.id = 'chatError';
    chatError.classList.add('error');
    chatDiv.appendChild(chatError);

    document.getElementById('login').insertAdjacentElement('afterend', chatDiv);
    messageArea.focus();
}

// Receives username verification, displays applicable error or initializes the chat
function connectToChat(success, errorMessage) {
    if (!success) {
        document.getElementById('loginError').innerText = errorMessage;
        return;
    }

    document.getElementById('loginError').innerText = '';
    document.getElementById('login').style.display = 'none';

    initializeChat();

    chatInitialized = true;
}