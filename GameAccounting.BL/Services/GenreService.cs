using AutoMapper;
using GameAccounting.BL.Interfaces;
using GameAccounting.BL.Models;
using GameAccounting.DAL;
using GameAccounting.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameAccounting.BL.Services
{
    /// <summary>
    /// Сервис для CRUD над сущностью жанров.
    /// summary к функциям описаны в интерфейсе.
    /// </summary>
    public class GenreService : IGenreService
    {
        private readonly GamesContext _context;
        private readonly IMapper _autoMapper;
        public GenreService(GamesContext context, IMapper autoMapper)
            => (_context, _autoMapper) = (context, autoMapper);

        public async Task<bool> AddGenreAsync(GenreDto newGenre)
        {
            _context.Genre.Add(new Genre
            {
                Name = newGenre.Name != null && newGenre.Name != String.Empty
                    ? newGenre.Name 
                    : throw new ApiResponseException(
                        HttpStatusCode.BadRequest, 
                        "Название жанра не может быть пустым"),
                Description = newGenre.Description
            });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(d => d.Id == id);
            if (genre == null) throw new ApiResponseException(
                                       HttpStatusCode.NotFound,
                                       $"Нет жанра с идентификатором {id}");
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditGenreAsync(int id, GenreDto newGenre)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(d => d.Id == id);
            if (genre == null) throw new ApiResponseException(
                                HttpStatusCode.NotFound, 
                                $"Нет жанра с идентификатором {id}");

            if (genre.Name != null) genre.Name = newGenre.Name ??
                               throw new ApiResponseException(
                                   HttpStatusCode.BadRequest, 
                                   "Название жанра не может быть пустым");
            if (genre.Description != null) genre.Description = newGenre.Description;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            var genre = await _context.Genre.AsNoTracking().ToListAsync();

            return _autoMapper.Map<List<GenreDto>>(genre);
        }

        public async Task<GenreDto> GetGenreAsync(int id)
        {
            var genre = await _context.Genre.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (genre == null) throw new ApiResponseException(
                                HttpStatusCode.NotFound,
                                $"Нет жанра с идентификатором {id}");

            return _autoMapper.Map<GenreDto>(genre);
        }
    }
}
