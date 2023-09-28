using YandexCloud.CORE.BL.RequestHandlers;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.Managers
{
    public class OzonManager : IOzonManager
    {
        readonly IUoW _uoW;
        readonly IEnumerable<IRequestHandler> _requestHandler;

        public event Action<string> OzonEventHandler;

        public OzonManager(IUoW uoW, IEnumerable<IRequestHandler> requestHandler)
        {
            _uoW = uoW;
            _requestHandler = requestHandler;
        }

        public async Task HandleOzonData(RequestDataDto requestDto)
        {




            //    var items = new List<List<Item>>();

            //    foreach (var responseModel in ozonData)
            //    {
            //        foreach (var item in responseModel.result.operations)
            //        {
            //            items.Add(item.items);
            //        }
            //    }

            //    var skus = new List<int>();

            //    foreach (var itemList in items)
            //    {
            //        foreach (var item in itemList)
            //        {
            //            skus.Add(item.sku);
            //        }
            //    }

            //    var filteredSkus = skus.Distinct().ToList();

            //    var requestArticuls = new RequestArticulsDto 
            //    {
            //        ClientId = requestModel.ClientId,
            //        ApiKey = requestModel.ApiKey,
            //        sku = filteredSkus,
            //    };
            //    var articulsModel = await _ozonFullData.GetArticulsAsync(requestArticuls);

            //    var checkSkuListFromBD = await _uoW.OzonArticuslData.ReadAsync();

            //    var uniqueInListFromOzon = new List<DTOs.Articuls.Item>();

            //    foreach(var skuBD in articulsModel.result.items)
            //    {
            //        if (!checkSkuListFromBD.Any(s => s.offer_id == skuBD.offer_id))
            //            uniqueInListFromOzon.Add(skuBD);
            //    }

            //    await _uoW.OzonArticuslData.CreateAsync(uniqueInListFromOzon);


            OzonEventHandler?.Invoke("Начинаем получать данные из озона");

            try
            {
                var tasks = new List<Task<CommonRequestDto>>();

                foreach (var item in _requestHandler)
                {
                    tasks.Add(item.SendRequest(requestDto));
                }

                await Task.WhenAll(tasks);
                
                var ozonFirstDataList = tasks[0].Result.FirstSecondRequest.OzonFirstDataList;
                var ozonSecondDataList = tasks[0].Result.FirstSecondRequest.OzonSecondDataList;

                //var ozonAcquiringData = tasks[1].Result.OzonAcquiringDataDtos;
                //var ozonMarketingActionData = tasks[2].Result.OzonMarketingActionCostModels;
                //var clientReturnAgentData = tasks[3].Result.ClientReturnAgentOperationModels;
                //var returnAgentOperationRFBSData = tasks[4].Result.ReturnAgentOperationRFBSModels;
                //var operationReturnGoodsFBSofRMSData = tasks[5].Result.OperationReturnGoodsFBSofRMSModels;
                //var priceByReturnGoodsFBSOfRMSData = tasks[5].Result.PriceByReturnGoodsFBSOfRMSModels;
                //var operationItemReturnData = tasks[6].Result.OperationItemReturnModels;
                //var priceByOperationItemReturnData = tasks[6].Result.PriceByOperationItemReturnModels;
                //var premiumCashbackIndividualPointsData = tasks[7].Result.PremiumCashbackIndividualPointsModels;
                //var holdingForUndeliverableGoodsData = tasks[8].Result.HoldingForUndeliverableGoodsModels;

                OzonEventHandler?.Invoke("Сохраняем обработанные данные");

                await _uoW.OpenTransactionAsync();

                await _uoW.OzonMainDataRepository.CreateAsync(ozonFirstDataList);
                await _uoW.OzonSecondDataRepository.CreateAsync(ozonSecondDataList);
                //await _uoW.OzonAcquiringRepository.CreateAsync(ozonAcquiringData);
                //await _uoW.OzonMarketingActionsRepository.CreateAsync(ozonMarketingActionData);
                //await _uoW.OzonClientReturnAgentRepository.CreateAsync(clientReturnAgentData);
                //await _uoW.OzonReturnAgentOperationRFBSRepository.CreateAsync(returnAgentOperationRFBSData);
                //await _uoW.OzonOperationReturnGoodsFbsOfRmsRepository.CreateAsync(operationReturnGoodsFBSofRMSData);
                //await _uoW.OzonPriceByReturnGoodsFbsOfRmsRepository.CreateAsync(priceByReturnGoodsFBSOfRMSData);
                //await _uoW.OzonOperationItemReturnRepository.CreateAsync(operationItemReturnData);
                //await _uoW.OzonPriceByOperationItemReturnRepository.CreateAsync(priceByOperationItemReturnData);
                //await _uoW.OzonPremiumCashbackIndividualPointsRepository.CreateAsync(premiumCashbackIndividualPointsData);
                //await _uoW.OzonHoldingForUndeliverableGoodsRepository.CreateAsync(holdingForUndeliverableGoodsData);


                await _uoW.CommitAsync();
                OzonEventHandler?.Invoke("Данные успешно сохранены");

            }
            catch (Exception ex)
            {
                await _uoW.RollbackAsync();
                throw;
            }
        }
    }
}
