namespace ASP.NET_Core_API_PokemonApp.DTO
{
    public class ReviewerDTO
    {
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
        public ICollection<ReviewDTO> Reviews { get; set; }
    }
}
