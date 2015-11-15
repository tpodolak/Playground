using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Infrastructure;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    public class DiarySummaryController : ApiController
    {
        private readonly ICountingKsRepository repo;
        private readonly IIdentityService identityService;
        private readonly IModelFactory modelFactory;

        public DiarySummaryController(ICountingKsRepository repo, IIdentityService identityService, IModelFactory modelFactory)
        {
            this.repo = repo;
            this.identityService = identityService;
            this.modelFactory = modelFactory;
        }

        public HttpResponseMessage Get(DateTime diaryid)
        {
            try
            {
                var diary = repo.GetDiary(identityService.CurrentUser, diaryid);
                if (diary == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, modelFactory.CreateSummary(diary));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}