namespace MoviesApi.Model
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { set; get; }
        public double Rate { set; get; }
        [MaxLength(2500)]
        public string StoreLine { set; get; }
        public byte[] Poster { set; get; }
        public Byte GenreId { set; get; }
        public Genre genres { set; get; }
    }
}
