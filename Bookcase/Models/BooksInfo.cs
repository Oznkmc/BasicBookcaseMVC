using System.ComponentModel.DataAnnotations;

namespace Bookcase.Models
{
    public class BooksInfo
    {
        [Key] // (Bunu yazarsan EF Core bunun Primary Key olduğunu kesin anlar)
        public int Id { get; set; }


        [Required(ErrorMessage = "Kitap adını yazmayı unuttun!")]
        public String BookName { get; set; }


        [StringLength(100, MinimumLength = 3, ErrorMessage = "karakter en az 3 karakter olmalıdır.")]
        public String Author { get; set; }

        [Range(10, 2000, ErrorMessage = "Sayfa sayısı  10 ile 2000 arasında olmalı")]
        public int BookPages { get; set; }

    }
}
