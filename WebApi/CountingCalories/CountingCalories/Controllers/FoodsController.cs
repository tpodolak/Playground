using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Infrastructure;
using CountingKs.Models;
namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly ICountingKsRepository countingKsRepo;
        private readonly IModelFactory modelFactory;
        private const int PageSize = 50;

        public FoodsController(ICountingKsRepository countingKsRepo, IModelFactory modelFactory)
        {
            this.countingKsRepo = countingKsRepo;
            this.modelFactory = modelFactory;
        }

        public PageResult<FoodModel> Get(bool includeMeasures = true, int page = 0)
        {
            var urlHelper = new UrlHelper(Request);

            var query = includeMeasures ? countingKsRepo.GetAllFoodsWithMeasures() : countingKsRepo.GetAllFoods();
            query = query.OrderBy(val => val.Description);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            var resultQuery = query.Skip(page * PageSize)
                                   .Take(PageSize);

            var results = modelFactory.Create(resultQuery);

            var pageResult = new PageResult<FoodModel>(results, totalPages, totalCount)
            {
                PreviousPageUrl = page > 0 ? urlHelper.Link("Foods", new { page = page - 1 }) : string.Empty,
                NextPageUrl = page < totalPages - 1 ? urlHelper.Link("Foods", new { page = page + 1 }) : string.Empty
            };
            return pageResult;
        }

        public FoodModel Get(int foodid)
        {
            var food = countingKsRepo.GetFood(foodid);
            return modelFactory.Create(food);
        }
    }
}