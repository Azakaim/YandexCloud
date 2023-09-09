﻿namespace YandexCloud.CORE.DTOs
{
    public class OzonDataDto
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public string posting_number { get; set; }
        public decimal? accruals_for_sale { get; set; }
        public decimal sale_comission { get; set; }
    }
}
