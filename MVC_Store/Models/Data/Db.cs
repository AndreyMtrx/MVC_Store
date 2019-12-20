using System.Data.Entity;

namespace MVC_Store.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
    }
}