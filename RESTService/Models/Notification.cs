﻿using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        // ForeignKey for User
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}