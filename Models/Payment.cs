namespace ArtClub.Models
{
    public class Payment : ModelEntity
    {

        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime GeneratedDateTime { get; set; }
        public decimal MoneyAmount { get; set; }
        public string CurrencyName { get; set; } = string.Empty;


    }
}
