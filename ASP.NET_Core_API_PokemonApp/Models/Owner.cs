namespace ASP.NET_Core_API_PokemonApp.Models
{
    /// <summary>
    /// Владелец
    /// </summary>
    public class Owner
    {
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        public string Gym { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }
    }
}
