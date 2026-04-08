namespace MangaRepositoryAPI.Data
{
    public class Status
    {
        public Guid StatusId { get; set; }
        public required string Description { get; set; }
        public Guid MangaId { get; set; }
        public Manga? Manga { get; set; }

    }
}
