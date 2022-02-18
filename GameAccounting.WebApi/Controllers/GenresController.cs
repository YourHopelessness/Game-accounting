using GameAccounting.BL.Interfaces;
using GameAccounting.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAccounting.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
            => _genreService = genreService;

        /// <summary>
        /// Получение списка жанров.
        /// Если количетсво страниц будет не задано, 
        /// или превышать количетсво страниц в списке, то вернется весь список.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="count">Количество на одной странице.</param>
        /// <returns>Список с пагинацией.</returns>
        [HttpGet]
        public async Task<IEnumerable<GenreDto>> GetGenresList(
           [FromQuery] int page,
           [FromQuery] int count)
        {
            var genreslist = await _genreService.GetAllGenresAsync();

            return genreslist.Count >= page * count && page * count > 0
                    ? genreslist.Skip((page - 1) * count).Take(count)
                    : genreslist;
        }

        /// <summary>
        /// Получение жанра по id.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <returns>Информация о жанре.</returns>
        [HttpGet("{id}")]
        public async Task<GenreDto> GetGenreById(int id)
           => await _genreService.GetGenreAsync(id);

        /// <summary>
        /// Добавление нового жанра.
        /// </summary>
        /// <param name="genre">Жанр.</param>
        [HttpPost]
        public async Task CreateGenre(
            [FromBody] GenreDto genre)
                => await _genreService.AddGenreAsync(genre);

        /// <summary>
        /// Обновление информации о жанре.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <param name="genre">Новая информация.</param>
        [HttpPut("{id}")]
        public async Task UpdateGenre(
            int id,
            [FromBody] GenreDto genre)
                => await _genreService.EditGenreAsync(id, genre);

        /// <summary>
        /// Удаление информации о жанре.
        /// </summary>
        /// <param name="Id">Идентификатор жанра.</param>
        [HttpDelete("{id}")]
        public async Task DeleteGenre(int Id)
          => await _genreService.DeleteGenreAsync(Id);
    }
}
