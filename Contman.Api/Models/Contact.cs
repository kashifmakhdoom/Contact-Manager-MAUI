using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contman.Api.Models
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    
}
