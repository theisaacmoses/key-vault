using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyVault.Models
{
    [Table("Vault")]
    public class Vault
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        public string Type { get; set; }
        public string Tags { get; set; }

    }
}
