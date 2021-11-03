using System;
using System.ComponentModel.DataAnnotations;

namespace JS.Chat.Domain.Models
{
    public class TMessage
    {
        [Required]
        public int Id { get; set; }       
        public Guid UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime DateMessage { get; set; }
    }
}
