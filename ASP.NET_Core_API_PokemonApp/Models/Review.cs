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

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int Rating { get; set; } 

        /// <summary>
        /// Эксперт, который написал отзыв
        /// </summary>
        public Reviewer Reviewer { get; set; }

        /// <summary>
        /// Покемон, которому адресован отзыв
        /// </summary>
        public Pokemon Pokemon { get; set; }
    }
}
