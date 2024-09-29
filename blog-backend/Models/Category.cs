using System.ComponentModel.DataAnnotations.Schema;

namespace blog_backend.Models{
    [Table("Categories")]
    public class Category{
        public int Id{get;set;}
        public string Name{get;set;}
    }
}