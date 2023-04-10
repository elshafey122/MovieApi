using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Model
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { set; get; }
        [MaxLength(100)]
        public string Name { set; get; }
    }
}
