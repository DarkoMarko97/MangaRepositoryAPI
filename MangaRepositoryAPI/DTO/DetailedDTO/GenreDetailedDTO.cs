namespace MangaRepositoryAPI.DTO.DetailedDTO
{
    public class GenreDetailedDTO : GenreDTO
    {
        public List<MangaDTO> Mangas { get; set; } = [];
    }
}
