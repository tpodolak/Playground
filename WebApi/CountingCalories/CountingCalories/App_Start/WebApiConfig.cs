using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using CacheCow.Server;
using CountingCalories.Converteres;
using CountingCalories.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace CountingCalories
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            ConfigureControllerSelector(config);
            config.Routes.MapHttpRoute(
                name: "Foods",
                routeTemplate: "api/nutrition/foods/{foodid}",
                defaults: new { controller = "foods", foodid = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "Measures",
                routeTemplate: "api/nutrition/foods/{foodid}/measures/{id}",
                defaults: new { controller = "measures", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "Diaries",
                routeTemplate: "api/user/diaries/{diaryid}",
                defaults: new { controller = "diaries", diaryid = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "DiaryEntries",
                routeTemplate: "api/user/diaries/{diaryid}/entries/{id}",
                defaults: new { controller = "diaryentries", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "DiarySummary",
                routeTemplate: "api/user/diaries/{diaryid}/summary",
                defaults: new { controller = "diarysummary" }
                );

            config.Routes.MapHttpRoute(
              name: "Token",
              routeTemplate: "api/token",
              defaults: new { controller = "token" }
              );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }

        public static void ConfigureFormatters(HttpConfiguration config)
        {
            foreach (var formatter in config.Formatters.OfType<JsonMediaTypeFormatter>())
            {
                formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                formatter.SerializerSettings.Converters.Add(new LinkModelJsonConverter());
                CreateMediatypes(formatter);
            }
        }

        public static void ConfigureHttps(HttpConfiguration config)
        {
#if !DEBUG
            config.Filters.Add(new RequireHttpsAttribute());
#endif
        }

        public static void ConfigureControllerSelector(HttpConfiguration config)
        {
           config.Services.Replace(typeof(IHttpControllerSelector), new CustomHttpControllerSelector(config));
        }

        private static void CreateMediatypes(JsonMediaTypeFormatter mediaFormatter)
        {
            // this supports only one media type, for prod we should also take care of others like application/xml etc
            var mediaTypes = new[]
            {
                "application/vnd.coutingcalories.food.v1+json",
                "application/vnd.coutingcalories.measure.v1+json",
                "application/vnd.coutingcalories.measure.v2+json",
                "application/vnd.coutingcalories.diary.v1+json",
                "application/vnd.coutingcalories.diaryEntry.v1+json",
            };

            foreach (var media in mediaTypes.Select(val => new MediaTypeHeaderValue(val)))
                mediaFormatter.SupportedMediaTypes.Add(media);
        }

        public static void ConfigureETagSupport(HttpConfiguration httpConfiguration)
        {
            var cacheHandler = new CachingHandler(httpConfiguration);
            httpConfiguration.MessageHandlers.Add(cacheHandler);
        }
    }
}