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
    /// Сервис для CRUD над сущностью игр.
    /// summary к функциям описаны в интерфейсе.
    /// </summary>
    public class GameService : IGameService
    {
        private readonly GamesContext _context;
        private readonly IMapper _autoMapper;

        public GameService(GamesContext context, IMapper autoMapper)
            => (_context, _autoMapper) = (context, autoMapper);

        public async Task<bool> AddGameAsync(GameToAddDto newGame)
        {
            var game = new Game();
            game.Name = newGame.Name != null && newGame.Name != string.Empty
                        ? newGame.Name
                        : throw new ApiResponseException(
                               HttpStatusCode.BadRequest,
                               "Название игры не может быть пустым");
            game.Description = newGame.Description;
            game.ReleaseDate = newGame.ReleaseDate ??
                throw new ApiResponseException(HttpStatusCode.BadRequest, "Дата выхода не может быть пуста");
            game.DeveloperId = newGame.Developer ??
                throw new ApiResponseException(HttpStatusCode.BadRequest, "У игры не может быть разработчика"); ;
            game.Genres = await AddGenreList(newGame.Genres ??
                throw new ApiResponseException(HttpStatusCode.BadRequest, "У игры должен быть хоть один жанр"));
            _context.Game.Add(game);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            var game = await _context.Game.FirstOrDefaultAsync(d => d.Id == id);
            if (game == null) throw new ApiResponseException(
                                HttpStatusCode.NotFound,
                                $"Нет игры с идентификатором {id}");
            _context.Game.Remove(game);

            return true;
        }

        public async Task<bool> EditGameAsync(Guid id, GameToAddDto newGame)
        {
            var game = await _context.Game.Include(g => g.Genres).FirstOrDefaultAsync(d => d.Id == id);
            if (game == null) throw new ApiResponseException(
                                HttpStatusCode.NotFound,
                                $"Нет игры с идентификатором {id}");

            if (newGame.Name != null)
                game.Name = newGame.Name != string.Empty
                    ? newGame.Name
                    : throw new ApiResponseException(
                           HttpStatusCode.BadRequest,
                           "Название игры не может быть пустым");
            if (newGame.Description != null) game.Description = newGame.Description;
            if (newGame.ReleaseDate != null) game.ReleaseDate = newGame.ReleaseDate.Value;
            if (newGame.Developer != null) game.DeveloperId = newGame.Developer.Value;
            if (newGame.Genres != null)
            {
                var removed = game.Genres.Select(g => g.Id).Except(newGame.Genres);
                foreach (var rem in removed)
                {
                    var remove = game.Genres.Where(g => g.Id == rem).SingleOrDefault();
                    if (remove == null) throw new ApiResponseException(
                                            HttpStatusCode.NotFound,
                                            $"Нет жанра с идентификатором {id}");
                    game.Genres.Remove(remove);
                }
                var added = newGame.Genres.Except(game.Genres.Select(g => g.Id));
                game.Genres = await AddGenreList(added);
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<GameDto>> GetGameAsync(GameParameters? parameters = null)
        {
            var query = await _context.Game.AsNoTracking().Include(g => g.Developer).Include(g => g.Genres).ToListAsync();
            if (parameters != null)
            {
                if (parameters.Id != null)
                    query = query.Where(g => g.Id == parameters.Id).ToList();
                if (parameters.Name != null)
                    query = query.Where(g => g.Name == parameters.Name).ToList();
                if (parameters.Developer != null)
                    query = query.Where(d => d.Developer.Name == parameters.Developer).ToList();
                if (parameters.Genre != null)
                    query = query.Where(gens => parameters.Genre.All(g => gens.Genres.Select(i => i.Id).Contains(g))).ToList();
            }

            return _autoMapper.Map<List<GameDto>>(query);
        }

        private async Task<List<Genre>> AddGenreList(IEnumerable<int> genresId)
        {
            var genres = new List<Genre>();
            foreach (var idGenre in genresId)
            {
                var genre = await _context.Genre.FirstOrDefaultAsync(g => g.Id == idGenre);
                if (genre == null)
                    throw new ApiResponseException(
                       HttpStatusCode.NotFound,
                       $"Нет жанра с id {idGenre}");
                genres.Add(genre);
            }
            return genres;
        }
    }
}
