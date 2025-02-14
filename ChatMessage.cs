using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4
{
    /// <summary>
    /// Chat message object for database storage
    /// </summary>
    /// <summary>
    /// Model Class - 
    /// Object Type that is Stored in a Database
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// Unique key for message
        /// </summary>
        [Key]
        public Guid ChatMessageId { get; set; }

        /// <summary>
        /// Username of the user who sent the message
        /// </summary>
        [Required]
        public string ChatMessageUsername { get; set; }

        /// <summary>
        /// Content of the message
        /// </summary>
        [Required]
        public string ChatMessageContent { get; set; }

        /// <summary>
        /// Time the message was received
        /// </summary>
        [Required]
        public DateTimeOffset ChatMessageReceived { get; set; }
    }
}
