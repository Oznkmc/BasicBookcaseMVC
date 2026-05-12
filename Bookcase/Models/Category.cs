using System.ComponentModel.DataAnnotations;

namespace Bookcase.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Bu alan boş bırakılamaz!!!")]
        public String CategoryName { get; set; }

        // İlişki Tanımı: Bir kategorinin birden fazla kitabı olabilir.
        public List<BooksInfo>? Books { get; set; }
    }
}
