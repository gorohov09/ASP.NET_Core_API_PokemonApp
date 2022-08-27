namespace ASP.NET_Core_API_PokemonApp.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Заголовок отзыва
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int Rating { get; set; }
    }
}
