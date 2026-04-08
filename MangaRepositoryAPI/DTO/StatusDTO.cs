namespace MangaRepositoryAPI.DTO
{
    public class StatusDTO
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public Guid MangaId { get; set; }
    }
}
