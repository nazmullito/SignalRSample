﻿using Microsoft.Build.Framework;

namespace SignalRSample.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
