namespace MangaRepositoryAPI.Data
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public required string GenreName { get; set; }
        public List<Manga>? Mangas { get; set; } = [];
    }
}
