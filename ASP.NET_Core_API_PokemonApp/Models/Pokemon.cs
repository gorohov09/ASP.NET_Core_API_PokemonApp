namespace ASP.NET_Core_API_PokemonApp.Models
{
    /// <summary>
    /// Покемон
    /// </summary>
    public class Pokemon
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Отзывы
        /// </summary>
        public ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// Промежуточная сущность PokemonCategory(many-to-many)
        /// </summary>
        public ICollection<PokemonCategory> PokemonCategories { get; set; }

        /// <summary>
        /// Промежуточная сущность PokemonOwners(many-to-many)
        /// </summary>
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
    }
}
