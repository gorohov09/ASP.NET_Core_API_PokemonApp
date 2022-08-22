namespace ASP.NET_Core_API_PokemonApp.Models
{
    /// <summary>
    /// Эксперт
    /// </summary>
    public class Reviewer
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Статьи эксперта
        /// </summary>
        public ICollection<Review> Reviews { get; set;}
    }
}
