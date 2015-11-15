using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Infrastructure;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    public class DiaryEntriesController : ApiController
    {
        private readonly ICountingKsRepository repo;
        private readonly IIdentityService identityService;
        private readonly IModelFactory modelFactory;

        public DiaryEntriesController(ICountingKsRepository repo, IIdentityService identityService, IModelFactory modelFactory)
        {
            this.repo = repo;
            this.identityService = identityService;
            this.modelFactory = modelFactory;
        }

        public IEnumerable<DiaryEntryModel> Get(DateTime diaryid)
        {
            var results = repo.GetDiaryEntries(identityService.CurrentUser, diaryid);
            return modelFactory.Create(results);
        }

        public HttpResponseMessage Get(DateTime diaryid, int id)
        {
            var result = repo.GetDiaryEntry(identityService.CurrentUser, diaryid, id);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, modelFactory.Create(result));
        }

        public HttpResponseMessage Post(DateTime diaryId, [FromBody] DiaryEntryModel diaryEntry)
        {
            try
            {
                var entity = modelFactory.Parse(diaryEntry);
                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read diary entry in body");

                var diary = repo.GetDiary(identityService.CurrentUser, diaryId);
                if (diary == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                diary.Entries.Add(entity);

                if (repo.SaveAll())
                    return Request.CreateResponse(HttpStatusCode.Created, modelFactory.Create(entity));

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not save to the database");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(DateTime diaryid, int id)
        {
            try
            {
                if (!repo.GetDiaryEntries(identityService.CurrentUser, diaryid).Any(val => val.Id == id))
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Could not find diary with id={id}");

                if (repo.DeleteDiaryEntry(id) && repo.SaveAll())
                    return Request.CreateResponse(HttpStatusCode.OK);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to delete entry");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Put spec states that we should send entire object to update - no for partial updates
        // Patch on the other hand - yes for partial updates
        [HttpPatch]
        [HttpPost]
        public HttpResponseMessage Patch(DateTime diaryid, int id, [FromBody] DiaryEntryModel diaryEntry)
        {
            try
            {
                var entry = repo.GetDiaryEntry(identityService.CurrentUser, diaryid, id);
                if (entry == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                var parsedValue = modelFactory.Parse(diaryEntry);

                if (parsedValue == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                if (entry.Quantity != parsedValue.Quantity)
                {
                    entry.Quantity = parsedValue.Quantity;
                    if (repo.SaveAll())
                        return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}