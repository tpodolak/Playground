using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingCalories.Data;
using CountingCalories.Filters;
using CountingCalories.Infrastructure;
using CountingCalories.Models;

namespace CountingCalories.Controllers
{
    [TokenAuthorization(true)]
    public class DiariesController : ApiController
    {
        private readonly ICountingCaloriesRepository repo;
        private readonly IModelFactory modelFactory;
        private readonly IIdentityService identityService;

        public DiariesController(ICountingCaloriesRepository repo, IModelFactory modelFactory, IIdentityService identityService)
        {
            this.repo = repo;
            this.modelFactory = modelFactory;
            this.identityService = identityService;
        }

        public IEnumerable<DiaryModel> Get()
        {
            var results = repo.GetDiaries(identityService.CurrentUser)
                .OrderByDescending(val => val.CurrentDate)
                .Take(10);
            return modelFactory.Create(results);
        }

        public HttpResponseMessage Get(DateTime diaryid)
        {
            var diary = repo.GetDiary(identityService.CurrentUser, diaryid);
            if (diary == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, modelFactory.Create(diary));
        }
    }
}