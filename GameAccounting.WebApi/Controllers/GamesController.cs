using GameAccounting.BL.Interfaces;
using GameAccounting.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAccounting.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
            => _gameService = gameService;

        /// <summary>
        /// Получения списка всех игр.
        /// </summary>
        /// <param name="page">Страница.</param>
        /// <param name="count">Количество на одной странице.</param>
        /// <returns>Список с пагинацией.</returns>
        [HttpGet]
        public async Task<IEnumerable<GameDto>> GetGamesList(
            [FromQuery] int page,
            [FromQuery] int count)
        {
            var gamelist = await _gameService.GetGameAsync();

            return gamelist.Count >= page * count && page * count > 0
                ? gamelist.Skip((page - 1) * count).Take(count)
                : gamelist;
        }

        /// <summary>
        /// Получение игры по параметрам.
        /// </summary>
        /// <param name="filterParams">Параметры запроса.</param>
        /// <returns>Список, согласно запрашиваемой информации.</returns>
        [HttpGet("Filter")]
        public async Task<IEnumerable<GameDto>> GetGameByParam(
            [FromQuery] GameParameters filterParams)
                 => await _gameService.GetGameAsync(filterParams);

        /// <summary>
        /// Добавление информации об игре.
        /// </summary>
        /// <param name="game">Информация об игре.</param>
        [HttpPost]
        public async Task UpdateGame(
            [FromBody] GameToAddDto game)
                => await _gameService.AddGameAsync(game);


        /// <summary>
        /// Обновление информации об игре.
        /// </summary>
        /// <param name="id">Идентификатор игры.</param>
        /// <param name="game">Информация об игре.</param>
        [HttpPut("{id}")]
        public async Task UpdateGame(Guid id, [FromBody] GameToAddDto game) 
           => await _gameService.EditGameAsync(id, game);

        /// <summary>
        /// Удаление информации об игре.
        /// </summary>
        /// <param name="Id">Идентификатор игры.</param>
        [HttpDelete("{id}")]
        public async Task DeleteGame(Guid Id)
            => await _gameService.DeleteGameAsync(Id);
    }
}