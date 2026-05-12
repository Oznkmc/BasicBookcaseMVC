using Microsoft.EntityFrameworkCore;
using Bookcase.Models; // Kendi model yolunu kontrol et

namespace Bookcase.Data
{
    public class UygulamaDbContext:DbContext
    {
                public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }
        // Bu satır: "SQL'de Kitaplar adında bir tablo oluştur" demektir.
        public DbSet<BooksInfo> Kitaplar { get; set; }
    }
}
