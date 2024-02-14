using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DvInfoWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Name")]
        public string Name { get; set; }
       
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Enter Display Num Between 1 - 100 !!")]
        public int DisplayOrder { get; set; }
    }
}
