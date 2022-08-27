namespace ASP.NET_Core_API_PokemonApp.DTO
{
    public class PokemonDetailsDTO
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

        public ICollection<OwnerDTO> Owners { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
    }
}
