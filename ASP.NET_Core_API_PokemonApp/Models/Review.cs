namespace ASP.NET_Core_API_PokemonApp.Models
{
    /// <summary>
    /// Отзыв
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок отзыва
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }
    }
}
