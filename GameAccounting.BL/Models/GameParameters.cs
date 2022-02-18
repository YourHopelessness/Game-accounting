namespace GameAccounting.BL.Models
{
    /// <summary>
    /// Параметры запроса для фильтрации.
    /// </summary>
    public class GameParameters
    {
        /// <summary>Идентификатор игры в системе.</summary>
        public Guid? Id { get; set; }

        /// <summary>Название игры.</summary>
        public string? Name { get; set; }

        /// <summary>Студия-разработчик игр.</summary>
        public string? Developer{ get; set; }

        /// <summary>Жанр или жанры игр.</summary>
        public int[]? Genre { get; set; }
    }
}
