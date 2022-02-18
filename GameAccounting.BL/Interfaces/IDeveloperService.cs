using GameAccounting.BL.Models;

namespace GameAccounting.BL.Interfaces
{
    /// <summary>
    /// CRUD для студий-разработчиков.
    /// </summary>
    public interface IDeveloperService
    {
        /// <summary>
        /// Получение студии-разрабочика по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор студии разработки.</param>
        /// <returns>Студию-разработчик с данным идентификатором.</returns>
        Task<DeveloperDto> GetDeveloperAsync(Guid id);

        /// <summary>
        /// Получение информации обо всех студиях разработки.
        /// </summary>
        /// <returns>Список всех студий разработки.</returns>
        Task<List<DeveloperDto>> GetAllDevelopersAsync();

        /// <summary>
        /// Добавить новую студию разработки.
        /// </summary>
        /// <param name="newDeveloper">Информация о студии.</param>
        /// <returns>Результат сохранения.</returns>
        Task<bool> AddDeveloperAsync(DeveloperDto newDeveloper);

        /// <summary>
        /// Измененение информации о студии-разработчике.
        /// </summary>
        /// <param name="id">Идентификатор изменяемой записи.</param>
        /// <param name="newDeveloper">Информация о студии.</param>
        /// <returns>Результат сохранения.</returns>
        Task<bool> EditDeveloperAsync(Guid id, DeveloperDto newDeveloper);

        /// <summary>
        /// Удаление информации о студии разработки из системы. 
        /// Удаляет так же все игры, разработанные этой студией.
        /// </summary>
        /// <param name="id">Идентификатор студии разработки.</param>
        /// <returns>Результат удаления.</returns>
        Task<bool> DeleteDeveloperAsync(Guid id);
    }
}
