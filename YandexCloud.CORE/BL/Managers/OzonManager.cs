using YandexCloud.CORE.BL.RequestHandlers;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.Managers
{
    public class OzonManager : IOzonManager
    {
        readonly IUoW _uow;
        readonly IEnumerable<IRequestHandler> _requestHandler;

        public event Action<string> OzonEventHandler;

        public OzonManager(IUoW uoW, IEnumerable<IRequestHandler> requestHandler)
        {
            _uow = uoW;
            _requestHandler = requestHandler;
        }

        public async Task HandleOzonData(RequestDataDto requestDto)
        {
            #region comments

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
            #endregion

            OzonEventHandler?.Invoke("Начинаем получать данные из озона");

            try
            {
                var tasks = new List<Task<CommonRequestDto>>();
                var webRequestsStart = DateTime.Now;

                var ozonServiseNames = await _uow.OzonServiceNamesRepository.GetAsync();

                foreach (var item in _requestHandler)
                {
                    tasks.Add(item.SendRequest(requestDto, ozonServiseNames));
                }

                await _uow.OpenTransactionAsync();
                while (tasks.Count > 0)
                {
                    var finishedTask = await Task.WhenAny(tasks);

                    await SaveResults(finishedTask);
                    tasks.Remove(finishedTask);
                }
                await _uow.CommitAsync();

                OzonEventHandler?.Invoke($"Длительность выполнения запросов: {DateTime.Now - webRequestsStart}");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        private async Task SaveResults(Task<CommonRequestDto> finishedTask)
        {
            var result = finishedTask.Result;

            if (result.FirstSecondRequest.OzonFirstDataList.Count > 0)
            {
                await _uow.OzonMainDataRepository.CreateAsync(result.FirstSecondRequest.OzonFirstDataList);
                await _uow.OzonSecondDataRepository.CreateAsync(result.FirstSecondRequest.OzonSecondDataList);
            } 
            else if (result.OzonAcquiringDataDtos?.Count() > 0)
            {
                await _uow.OzonAcquiringRepository.CreateAsync(result.OzonAcquiringDataDtos);
            }
            else if (result.OzonMarketingActionCostModels?.Count() > 0)
            {
                await _uow.OzonMarketingActionsRepository.CreateAsync(result.OzonMarketingActionCostModels);
            }
            else if (result.ClientReturnAgentOperationModels?.Count() > 0)
            {
                await _uow.OzonClientReturnAgentRepository.CreateAsync(result.ClientReturnAgentOperationModels);
            }
            else if (result.ReturnAgentOperationRFBSModels?.Count() > 0)
            {
                await _uow.OzonReturnAgentOperationRFBSRepository.CreateAsync(result.ReturnAgentOperationRFBSModels);
            }
            else if (result.OperationReturnGoodsFBSofRMSModels?.Count() > 0)
            {
                await _uow.OzonOperationReturnGoodsFbsOfRmsRepository.CreateAsync(result.OperationReturnGoodsFBSofRMSModels);
                await _uow.OzonPriceByReturnGoodsFbsOfRmsRepository.CreateAsync(result.PriceByReturnGoodsFBSOfRMSModels);
            }
            else if (result.OperationItemReturnModels?.Count() > 0)
            {
                await _uow.OzonOperationItemReturnRepository.CreateAsync(result.OperationItemReturnModels);
                await _uow.OzonPriceByOperationItemReturnRepository.CreateAsync(result.PriceByOperationItemReturnModels);
            }
            else if (result.PremiumCashbackIndividualPointsModels?.Count() > 0)
            {
                await _uow.OzonPremiumCashbackIndividualPointsRepository.CreateAsync(result.PremiumCashbackIndividualPointsModels);
            }
            else if (result.HoldingForUndeliverableGoodsModels?.Count() > 0)
            {
                await _uow.OzonHoldingForUndeliverableGoodsRepository.CreateAsync(result.HoldingForUndeliverableGoodsModels);
            }
        }
    }
}
