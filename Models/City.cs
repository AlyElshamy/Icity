namespace Icity.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityTlAr { get; set; }
        public string CityTlEn { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
