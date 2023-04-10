namespace MoviesApi.dto
{
    public class MovieDto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { set; get; }
        public double Rate { set; get; }
        [MaxLength(2500)]
        public string StoreLine { set; get; }
        public IFormFile? Poster { set; get; }
        public Byte GenreId { set; get; }
    }
}
