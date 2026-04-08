using MangaRepositoryAPI.Data;
using MangaRepositoryAPI.DTO.DetailedDTO;

namespace MangaRepositoryAPI.DTO
{
    public class Mapper
    {
        #region Manga

        public MangaDTO MapEntityToDTO(Manga entity) 
        {
            return new MangaDTO() 
            {
                Id = entity.MangaId,
                Title= entity.Title,
                ChapterNumber = entity.ChapterNumber,
                PublicationDate = entity.PublicationDate
            };
        }

        public MangaDetailedDTO MapEntityToDetailedDTO(Manga entity) 
        {
            return new MangaDetailedDTO() 
            {
                Id = entity.MangaId,
                Title = entity.Title,
                ChapterNumber = entity.ChapterNumber,
                PublicationDate = entity.PublicationDate,
                Status = entity.Status != null ? MapEntityToDTO(entity.Status) : null,
                Authors = entity.Authors != null ? entity.Authors.Select(a => MapEntityToDTO(a)).ToList() : new List<AuthorDTO>(),
                Genres = entity.Genres != null ? entity.Genres.Select(g => MapEntityToDTO(g)).ToList() : new List<GenreDTO>()
            };
        }

        public Manga MapDTOToEntity(MangaDTO dto) 
        {
            return new Manga() 
            {
                MangaId = dto.Id,
                Title = dto.Title,
                ChapterNumber = dto.ChapterNumber,
                PublicationDate = dto.PublicationDate
            };
        }

        public Manga MapDetailedDTOToEntity(MangaDetailedDTO dto) 
        {
            return new Manga() 
            {
                MangaId = dto.Id,
                Title = dto.Title,
                ChapterNumber = dto.ChapterNumber,
                PublicationDate = dto.PublicationDate,
                Status = dto.Status != null ? MapDTOToEntity(dto.Status) : null,
                Authors = dto.Authors != null ? dto.Authors.Select(a => MapDTOToEntity(a)).ToList() : new List<Author>(),
                Genres = dto.Genres != null ? dto.Genres.Select(g => MapDTOToEntity(g)).ToList() : new List<Genre>()
            };
        }

        #endregion

        #region Author

        public AuthorDTO MapEntityToDTO(Author entity) 
        {
            return new AuthorDTO() 
            {
                Id = entity.AuthorId,
                Name = entity.Name,
            };
        }

        public AuthorDetailedDTO MapEntityToDetailedDTO(Author entity) 
        {
            return new AuthorDetailedDTO() 
            {
                Id = entity.AuthorId,
                Name = entity.Name,
                Mangas = entity.Mangas != null ? entity.Mangas.Select(m => MapEntityToDTO(m)).ToList() : new List<MangaDTO>()
            };
        }

        public Author MapDTOToEntity(AuthorDTO dto) 
        {
            return new Author() 
            {
                AuthorId = dto.Id,
                Name = dto.Name 
            };
        }

        public Author MapDetailedDTOToEntity(AuthorDetailedDTO dto) 
        {
            return new Author() 
            {
                AuthorId = dto.Id,
                Name = dto.Name,
                Mangas = dto.Mangas != null ? dto.Mangas.Select(m => MapDTOToEntity(m)).ToList() : new List<Manga>()
            };
        }
 
        #endregion

        #region Genre

        public GenreDTO MapEntityToDTO(Genre entity) 
        {
            return new GenreDTO() 
            {
                Id = entity.GenreId,
                GenreName = entity.GenreName

            };
        }

        public GenreDetailedDTO MapEntityToDetailedDTO(Genre entity) 
        {
            return new GenreDetailedDTO() 
            {
                Id = entity.GenreId,
                GenreName= entity.GenreName,
                Mangas = entity.Mangas != null ? entity.Mangas.Select(m => MapEntityToDTO(m)).ToList() : new List<MangaDTO>()
            };
        }

        public Genre MapDTOToEntity(GenreDTO dto) 
        {
            return new Genre() 
            {
                GenreId = dto.Id,
                GenreName = dto.GenreName
            };
        }

        public Genre MapDetailedDTOToEntity(GenreDetailedDTO dto) 
        {
            return new Genre() 
            {
                GenreId = dto.Id,
                GenreName = dto.GenreName,
                Mangas = dto.Mangas != null ? dto.Mangas.Select(m => MapDTOToEntity(m)).ToList() : new List<Manga>()
            };
        }

        #endregion

        #region Status

        public StatusDTO MapEntityToDTO(Status entity) 
        {
            return new StatusDTO() 
            {
                Id = entity.StatusId,
                Description = entity.Description
            };
        }

        public StatusDetailedDTO MapEntityToDetailedDTO(Status entity) 
        {
            return new StatusDetailedDTO() 
            {
                Id = entity.StatusId,
                Description = entity.Description,
                MangaId = entity.MangaId,
                Manga = entity.Manga != null ? MapEntityToDTO(entity.Manga) : null
            };
        }

        public Status MapDTOToEntity(StatusDTO dto) 
        {
            return new Status() 
            {
                StatusId = dto.Id,
                Description = dto.Description,
                MangaId = dto.MangaId
            };
        }

        public Status MapDetailedDTOToEntity(StatusDetailedDTO dto) 
        {
            return new Status() 
            {
                StatusId = dto.Id,
                Description = dto.Description,
                MangaId = dto.MangaId,
                Manga = dto.Manga != null ? MapDTOToEntity(dto.Manga) : null
            };
        }

        #endregion
    }
}
