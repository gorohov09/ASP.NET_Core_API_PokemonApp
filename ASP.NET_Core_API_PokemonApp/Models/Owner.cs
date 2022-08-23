namespace ASP.NET_Core_API_PokemonApp.Models
{
    /// <summary>
    /// Владелец
    /// </summary>
    public class Owner
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

        public string Gym { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }

        public ICollection<PokemonOwner> PokemonOwners { get; set; }
    }
}
