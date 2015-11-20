using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using CountingCalories.Data;
using CountingCalories.Filters;
using CountingCalories.Infrastructure;
using CountingCalories.Models;

namespace CountingCalories.Controllers
{
    [TokenAuthorization(false)]
    public class FoodsController : ApiController
    {
        private readonly ICountingCaloriesRepository repo;
        private readonly IModelFactory modelFactory;
        private const int PageSize = 50;

        public FoodsController(ICountingCaloriesRepository repo, IModelFactory modelFactory)
        {
            this.repo = repo;
            this.modelFactory = modelFactory;
        }

        public PageResult<FoodModel> Get(bool includeMeasures = true, int page = 0)
        {
            var urlHelper = new UrlHelper(Request);

            var query = includeMeasures ? repo.GetAllFoodsWithMeasures() : repo.GetAllFoods();
            query = query.OrderBy(val => val.Description);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            var resultQuery = query.Skip(page * PageSize)
                                   .Take(PageSize);

            var results = modelFactory.Create(resultQuery);

            var pageResult = new PageResult<FoodModel>(results, totalPages, totalCount);
            if (page > 0)
                pageResult.Links.Add(modelFactory.CreateLink(urlHelper.Link("Foods", new {page = page - 1}), "previousPage"));

            if (page < totalPages - 1)
            {
                pageResult.Links.Add(modelFactory.CreateLink(urlHelper.Link("Foods", new {page = page + 1}), "nextPage"));
            }
            return pageResult;
        }

        public FoodModel Get(int foodid)
        {
            var food = repo.GetFood(foodid);
            return modelFactory.Create(food);
        }
    }
}