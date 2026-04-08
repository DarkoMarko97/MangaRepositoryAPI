namespace MangaRepositoryAPI.DTO.DetailedDTO
{
    public class AuthorDetailedDTO : AuthorDTO
    {
        public List<MangaDTO> Mangas { get; set; } = [];
    }
}
