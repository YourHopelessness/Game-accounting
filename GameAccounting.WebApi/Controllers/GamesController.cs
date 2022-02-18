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
        /// ��������� ������ ���� ���.
        /// </summary>
        /// <param name="page">��������.</param>
        /// <param name="count">���������� �� ����� ��������.</param>
        /// <returns>������ � ����������.</returns>
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
        /// ��������� ���� �� ����������.
        /// </summary>
        /// <param name="filterParams">��������� �������.</param>
        /// <returns>������, �������� ������������� ����������.</returns>
        [HttpGet("Filter")]
        public async Task<IEnumerable<GameDto>> GetGameByParam(
            [FromQuery] GameParameters filterParams)
                 => await _gameService.GetGameAsync(filterParams);

        /// <summary>
        /// ���������� ���������� �� ����.
        /// </summary>
        /// <param name="game">���������� �� ����.</param>
        [HttpPost]
        public async Task UpdateGame(
            [FromBody] GameToAddDto game)
                => await _gameService.AddGameAsync(game);


        /// <summary>
        /// ���������� ���������� �� ����.
        /// </summary>
        /// <param name="id">������������� ����.</param>
        /// <param name="game">���������� �� ����.</param>
        [HttpPut("{id}")]
        public async Task UpdateGame(Guid id, [FromBody] GameToAddDto game) 
           => await _gameService.EditGameAsync(id, game);

        /// <summary>
        /// �������� ���������� �� ����.
        /// </summary>
        /// <param name="Id">������������� ����.</param>
        [HttpDelete("{id}")]
        public async Task DeleteGame(Guid Id)
            => await _gameService.DeleteGameAsync(Id);
    }
}