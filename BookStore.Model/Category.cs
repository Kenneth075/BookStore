using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
