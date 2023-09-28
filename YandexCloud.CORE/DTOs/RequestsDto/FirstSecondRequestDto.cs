namespace YandexCloud.CORE.DTOs.RequestsDto
{
    public class FirstSecondRequestDto
    {
        public List<OzonFirstTableModel> OzonFirstDataList { get; set; } = new List<OzonFirstTableModel>();
        public List<OzonSecondDataDto> OzonSecondDataList { get; set; } = new List<OzonSecondDataDto>();
    }
}
