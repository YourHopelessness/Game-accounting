using GameAccounting.BL.Interfaces;
using GameAccounting.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameAccounting.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;
        public DevelopersController(IDeveloperService developerService)
            => _developerService = developerService;

        /// <summary>
        /// Получение списка разработчиков.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="count">Количество на одной странице.</param>
        /// <returns>Список с пагинацией.</returns>
        [HttpGet]
        public async Task<IEnumerable<DeveloperDto>> GetDevelopersList(
           [FromQuery] int page,
           [FromQuery] int count)
        {
            var developerlist = await _developerService.GetAllDevelopersAsync();

            return developerlist.Count >= page * count && page * count > 0
                ? developerlist.Skip((page - 1) * count).Take(count)
                : developerlist;
        }

        /// <summary>
        /// Получение студии-разработки по id.
        /// </summary>
        /// <param name="id">Идентификатор студии разработки.</param>
        /// <returns>Информация о студии разработке.</returns>
        [HttpGet("{id}")]
        public async Task<DeveloperDto> GetDeveloperById(Guid id)
                => await _developerService.GetDeveloperAsync(id);

        [HttpPost]
        public async Task CreateDeveloper(
            [FromBody] DeveloperDto developer) 
                => await _developerService.AddDeveloperAsync(developer);

        /// <summary>
        /// Обновление информации о разработчике.
        /// </summary>
        /// <param name="id">Идентификатор студии разработки.</param>
        /// <param name="developer">Новая информация.</param>
        [HttpPut("{id}")]
        public async Task UpdateDeveloper(Guid id, [FromBody] DeveloperDto developer)
           => await _developerService.EditDeveloperAsync(id, developer);

        /// <summary>
        /// Удаление информации о стдии разработке.
        /// </summary>
        /// <param name="Id">Идентификатор студии.</param>
        [HttpDelete("{id}")]
        public async Task DeleteDeveloper(Guid Id) 
          => await _developerService.DeleteDeveloperAsync(Id);
    }
}
