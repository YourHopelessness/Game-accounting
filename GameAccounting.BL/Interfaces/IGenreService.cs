using GameAccounting.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAccounting.BL.Interfaces
{
    /// <summary>
    /// CRUD для жанров.
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Получение жанра по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <returns>Информация о жанре.</returns>
        Task<GenreDto> GetGenreAsync(int id);

        /// <summary>
        /// Получение списка всех жанров.
        /// </summary>
        /// <returns>Список жанров.</returns>
        Task<List<GenreDto>> GetAllGenresAsync();

        /// <summary>
        /// Добавление нового жанра.
        /// </summary>
        /// <param name="newGenre">Информация о новом жанре.</param>
        /// <returns>Результат сохранения.</returns>
        Task<bool> AddGenreAsync(GenreDto newGenre);

        /// <summary>
        /// Редактирование жанра.
        /// </summary>
        /// <param name="id">Идентификатор изменяемой записи.</param>
        /// <param name="newGenre">Информация о новом жанре.</param>
        /// <returns>Результат сохранения.</returns>
        Task<bool> EditGenreAsync(int id, GenreDto newGenre);

        /// <summary>
        /// Удаление жанра.
        /// </summary>
        /// <param name="id">Идентификатор жанра.</param>
        /// <returns>Результат удаления.</returns>
        Task<bool> DeleteGenreAsync(int id);
    }
}
