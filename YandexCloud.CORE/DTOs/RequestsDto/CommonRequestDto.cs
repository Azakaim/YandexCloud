namespace YandexCloud.CORE.DTOs.RequestsDto
{
    public class CommonRequestDto
    {
        public FirstSecondRequestDto FirstSecondRequest { get; set; }
        public IEnumerable<OzonAcquiringDataDto> OzonAcquiringDataDtos { get; set; }
        public IEnumerable<OzonMarketingActionCostModel> OzonMarketingActionCostModels { get; set; }
        public IEnumerable<ClientReturnAgentOperationModel> ClientReturnAgentOperationModels { get; set; }
        public IEnumerable<ReturnAgentOperationRFBSModel> ReturnAgentOperationRFBSModels { get; set; }
        public IEnumerable<OperationReturnGoodsFBSofRMSModel> OperationReturnGoodsFBSofRMSModels { get; set; }
        public IEnumerable<PriceByReturnGoodsFBSOfRMSModel> PriceByReturnGoodsFBSOfRMSModels { get; set; }
        public IEnumerable<OperationItemReturnModel> OperationItemReturnModels { get; set; }
        public IEnumerable<PriceByOperationItemReturnModel> PriceByOperationItemReturnModels { get; set; }
        public IEnumerable<PremiumCashbackIndividualPointsModel> PremiumCashbackIndividualPointsModels { get; set; }
        public IEnumerable<HoldingForUndeliverableGoodsModel> HoldingForUndeliverableGoodsModels { get; set; }
    }
}
