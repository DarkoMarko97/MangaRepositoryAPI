namespace MangaRepositoryAPI.Data
{
    public class Manga
    {
        public Guid MangaId { get; set; }
        public required string Title { get; set; }
        public required int ChapterNumber { get; set; }
        public required DateOnly PublicationDate { get; set; }
        public Status? Status { get; set; }
        public List<Author>? Authors { get; set; } = [];
        public List<Genre>? Genres { get; set; } = [];
    }
}
