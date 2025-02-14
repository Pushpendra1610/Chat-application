using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment4
{
    /// <summary>
    /// Database context for chat messages
    /// </summary>
    public class ChatContext : DbContext
    {
        public DbSet<ChatMessage> Messages { get; set; }
    }
}
