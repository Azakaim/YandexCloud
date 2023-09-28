using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class HandlersFactory
    {
        readonly IOzonFullData _ozonFullData;
        readonly IUoW _uow;

        public HandlersFactory(IOzonFullData ozonFullData, IUoW uow)
        {
            _ozonFullData = ozonFullData;
            _uow = uow;
        }

        public IEnumerable<IRequestHandler> MakeHandlers()
        {
            var handlerList = new List<IRequestHandler>
            {
                new OperationAgentDeliveredToCustomerHandler(_ozonFullData, _uow),
                new MarketplaceRedistributionOfAcquiringHandler(_ozonFullData, _uow),
                new MarketingActionCostHandler(_ozonFullData, _uow),
                new ClientReturnAgentHandler(_ozonFullData, _uow),
                new ReturnAgentOperationRfbsHandler(_ozonFullData, _uow),
                new ReturnGoodsFbsOfRmsHandler(_ozonFullData, _uow),
                new OperationItemReturnHandler(_ozonFullData, _uow),
                new ServicePremiumCashbackIndividualPointsHandler(_ozonFullData, _uow),
                new HoldingForUndeliverableGoodsHandler(_ozonFullData, _uow),
            };



            return handlerList;
        }
    }
}
