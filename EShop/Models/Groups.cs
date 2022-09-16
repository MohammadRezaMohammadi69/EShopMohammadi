using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    public class Groups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int  ParentId { get; set; }
        public string ? Icon { get; set; }
        public string ? Page { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string? ShowCreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public string? ShowEditDate { get; set; }

    }
}
