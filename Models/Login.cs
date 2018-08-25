
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SlackClone.Models
{
    [Table("Users")]
    public class Login
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength (15)]
        public string Password { get; set; }

        public string ConnectionId { get; set; }

        public bool Status { get; set; }

    }
}
