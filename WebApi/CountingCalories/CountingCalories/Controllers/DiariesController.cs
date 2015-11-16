using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Filters;
using CountingKs.Infrastructure;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    [BasicAuthorize]
    public class DiariesController : ApiController
    {
        private readonly ICountingKsRepository repo;
        private readonly IModelFactory modelFactory;
        private readonly IIdentityService identityService;

        public DiariesController(ICountingKsRepository repo, IModelFactory modelFactory, IIdentityService identityService)
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