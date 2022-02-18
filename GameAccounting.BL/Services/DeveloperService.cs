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
    /// Сервис для CRUD над сущьностью разработчиков
    /// summary к функциям описаны в интерфейсе
    /// </summary>
    public class DeveloperService : IDeveloperService
    {
        private readonly GamesContext _context;
        private readonly IMapper _autoMapper;

        public DeveloperService(GamesContext context, IMapper autoMapper)
            => (_context, _autoMapper) = (context, autoMapper);

        public async Task<bool> AddDeveloperAsync(DeveloperDto newDeveloper)
        {
            _context.Developer.Add(new Developer
            {
                Name = newDeveloper.Name != null && newDeveloper.Name != String.Empty
                    ? newDeveloper.Name
                    : throw new ApiResponseException(
                        HttpStatusCode.BadRequest,
                        "Название жанра не может быть пустым"),
                Description = newDeveloper.Description
            });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteDeveloperAsync(Guid id)
        {
            var dev = await _context.Developer.FirstOrDefaultAsync(d => d.Id == id);
            if (dev == null) throw new ApiResponseException(
                                       HttpStatusCode.NotFound,
                                       $"Нет разработчика с идентификатором {id}");
            _context.Developer.Remove(dev);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditDeveloperAsync(Guid id, DeveloperDto newDeveloper)
        {
            var dev = await _context.Developer.FirstOrDefaultAsync(d => d.Id == id);
            if (dev == null) throw new ApiResponseException(
                                HttpStatusCode.NotFound,
                                $"Нет разработчика с идентификатором {id}");

            if (dev.Name != null) dev.Name = newDeveloper.Name ??
                               throw new ApiResponseException(
                                   HttpStatusCode.BadRequest,
                                   "Название разработчика не может быть пустым");
            if (dev.Description != null) dev.Description = newDeveloper.Description;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<DeveloperDto>> GetAllDevelopersAsync()
        {
            var dev = await _context.Developer.AsNoTracking().ToListAsync();

            return _autoMapper.Map<List<DeveloperDto>>(dev);
        }

        public async Task<DeveloperDto> GetDeveloperAsync(Guid id)
        {
            var dev = await _context.Developer.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (dev == null) throw new ApiResponseException(
                                HttpStatusCode.NotFound,
                                $"Нет разработчика с идентификатором {id}");

            return _autoMapper.Map<DeveloperDto>(dev);
        }
    }
}
