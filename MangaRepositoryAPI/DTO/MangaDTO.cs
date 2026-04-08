namespace MangaRepositoryAPI.DTO
{
    public class MangaDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required int ChapterNumber { get; set; }
        public required DateOnly PublicationDate { get; set; }
        public StatusDTO? Status { get; set; }
        public List<AuthorDTO> Authors { get; set; } = [];
        public List<GenreDTO> Genres { get; set; } = [];
    }
}
