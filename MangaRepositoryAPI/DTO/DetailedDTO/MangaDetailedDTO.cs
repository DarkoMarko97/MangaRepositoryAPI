namespace MangaRepositoryAPI.DTO.DetailedDTO
{
    public class MangaDetailedDTO : MangaDTO
    {
        public StatusDTO? Status { get; set; }
        public List<AuthorDTO> Authors { get; set; } = [];
        public List<GenreDTO> Genres { get; set; } = [];

    }
}
