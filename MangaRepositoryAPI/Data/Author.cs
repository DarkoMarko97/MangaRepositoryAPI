namespace MangaRepositoryAPI.Data
{
    public class Author
    {
        public Guid AuthorId { get; set; }
        public required string Name { get; set; }
        public List<Manga>? Mangas { get; set; } = [];
    }
}
