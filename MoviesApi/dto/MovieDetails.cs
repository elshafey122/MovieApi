namespace MoviesApi.dto
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { set; get; }
        public double Rate { set; get; }
        public string StoreLine { set; get; }
        public byte[] Poster { set; get; }
        public Byte GenreId { set; get; }
        public string GenreName { set; get; }
    }
}
