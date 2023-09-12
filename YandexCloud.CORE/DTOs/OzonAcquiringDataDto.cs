namespace YandexCloud.CORE.DTOs
{
    public class OzonAcquiringDataDto
    {
        public int id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public decimal amount { get; set; }
        public string posting_number { get; set; }
        public string operation_id { get; set; }
        public DateTime date { get; set; }
    }
}
