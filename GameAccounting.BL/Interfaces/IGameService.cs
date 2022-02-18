using GameAccounting.BL.Models;

namespace GameAccounting.BL.Interfaces
{
    /// <summary>
    /// Управление информацей об играх.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Получение игр, удовлетворяющих условию параметров.
        /// </summary>
        /// <param name="parameters">Параметры поиска.</param>
        /// <returns>
        /// Все игры, подошедшие по критерию,
        /// если нет таких, то вернется пустой список.
        /// Если параметер не указан, т.е null, то вернется список всех игр.
        /// </returns>
        Task<List<GameDto>> GetGameAsync(GameParameters? parameters = null);

        /// <summary>
        /// Добавление информации о новой игре.
        /// </summary>
        /// <param name="newGame">Новая игра.</param>
        /// <returns>Результат сохранения.</returns>
        Task<bool> AddGameAsync(GameToAddDto newGame);

        /// <summary>
        /// Редактирование информации об игре.
        /// </summary>
        /// <param name="id">Идентификатор изменяемой записи.</param>
        /// <param name="newGame">Новая информация об игре.</param>
        /// <returns>Результат сохранения.</returns>
        Task<bool> EditGameAsync(Guid id, GameToAddDto newGame);

        /// <summary>
        /// Удалеине информации об игре.
        /// </summary>
        /// <param name="id">Идентификатор игры.</param>
        /// <returns>Результат удаления.</returns>
        Task<bool> DeleteGameAsync(Guid id);
    }
}
