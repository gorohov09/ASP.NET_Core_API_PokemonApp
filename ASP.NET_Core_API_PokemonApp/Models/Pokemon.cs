namespace ASP.NET_Core_API_PokemonApp.Models
{
    /// <summary>
    /// Покемон
    /// </summary>
    public class Pokemon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
