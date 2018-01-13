namespace DotRezCore.Api.Tests.Integration.ApiClients.V1
{
    public partial class DotRezCoreApiClient 
    {
        private System.Net.Http.HttpClient _httpClient;
        private Newtonsoft.Json.JsonSerializerSettings _serializerSettings;
        private Microsoft.Extensions.Logging.ILogger<DotRezCoreApiClient> _logger;

        public DotRezCoreApiClient(System.Net.Http.HttpClient httpClient, Microsoft.Extensions.Logging.ILogger<DotRezCoreApiClient> logger)
        {
            _httpClient = httpClient;
            _serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            _logger = logger;
        }
        
        public async System.Threading.Tasks.Task<System.Collections.Generic.List<DotRezCore.Api.Tests.Integration.Models.Order>> ApiOrdersGetAsync(DotRezCore.Api.Tests.Integration.Models.GetOrderRequest request)
        { 
            string content = string.Empty;
            try
            {
                var response = await ApiOrdersGetResponseAsync(request);
                response.EnsureSuccessStatusCode();
                content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<DotRezCore.Api.Tests.Integration.Models.Order>>(content, _serializerSettings);
                return result;
            }
            catch(System.Exception ex)
            {
                Microsoft.Extensions.Logging.LoggerExtensions.LogError(_logger, default(Microsoft.Extensions.Logging.EventId), ex, "There was an error while making a API call. Response content is {0}", content);
                throw;
            }
        }
        
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ApiOrdersGetResponseAsync(DotRezCore.Api.Tests.Integration.Models.GetOrderRequest request)
        {
            using(var httpRequestMessage = new System.Net.Http.HttpRequestMessage())
            {
                var urlStringBuilder = new System.Text.StringBuilder("api/Orders");
                urlStringBuilder.Append("?");
                urlStringBuilder.Append(SerializeToQueryString(request));
                httpRequestMessage.Method = new System.Net.Http.HttpMethod("GET");
                httpRequestMessage.RequestUri = new System.Uri(urlStringBuilder.ToString());
                return await _httpClient.SendAsync(httpRequestMessage);
            }
        }
        
        public async System.Threading.Tasks.Task<DotRezCore.Api.Tests.Integration.Models.Order> ApiOrdersPostAsync(DotRezCore.Api.Tests.Integration.Models.Order order)
        { 
            string content = string.Empty;
            try
            {
                var response = await ApiOrdersPostResponseAsync(order);
                response.EnsureSuccessStatusCode();
                content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DotRezCore.Api.Tests.Integration.Models.Order>(content, _serializerSettings);
                return result;
            }
            catch(System.Exception ex)
            {
                Microsoft.Extensions.Logging.LoggerExtensions.LogError(_logger, default(Microsoft.Extensions.Logging.EventId), ex, "There was an error while making a API call. Response content is {0}", content);
                throw;
            }
        }
        
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ApiOrdersPostResponseAsync(DotRezCore.Api.Tests.Integration.Models.Order order)
        {
            using(var httpRequestMessage = new System.Net.Http.HttpRequestMessage())
            {
                var urlStringBuilder = new System.Text.StringBuilder("api/Orders");
                httpRequestMessage.Method = new System.Net.Http.HttpMethod("POST");
                httpRequestMessage.RequestUri = new System.Uri(urlStringBuilder.ToString());
                System.Net.Http.StringContent content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(order, _serializerSettings));
                content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                httpRequestMessage.Content = content;
                return await _httpClient.SendAsync(httpRequestMessage);
            }
        }
        
        public async System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, DotRezCore.Api.Tests.Integration.Models.Order>> ApiOrdersGetSomethingElseGetAsync(int id, int id2, System.DateTime id3)
        { 
            string content = string.Empty;
            try
            {
                var response = await ApiOrdersGetSomethingElseGetResponseAsync(id, id2, id3);
                response.EnsureSuccessStatusCode();
                content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, DotRezCore.Api.Tests.Integration.Models.Order>>(content, _serializerSettings);
                return result;
            }
            catch(System.Exception ex)
            {
                Microsoft.Extensions.Logging.LoggerExtensions.LogError(_logger, default(Microsoft.Extensions.Logging.EventId), ex, "There was an error while making a API call. Response content is {0}", content);
                throw;
            }
        }
        
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ApiOrdersGetSomethingElseGetResponseAsync(int id, int id2, System.DateTime id3)
        {
            using(var httpRequestMessage = new System.Net.Http.HttpRequestMessage())
            {
                var urlStringBuilder = new System.Text.StringBuilder("api/Orders/GetSomethingElse");
                urlStringBuilder.Append("?");
                urlStringBuilder.AppendFormat("{0}={1}&" , "id", System.Uri.EscapeDataString(ConvertToString(id, System.Globalization.CultureInfo.InvariantCulture)));
                urlStringBuilder.AppendFormat("{0}={1}&" , "id2", System.Uri.EscapeDataString(ConvertToString(id2, System.Globalization.CultureInfo.InvariantCulture)));
                urlStringBuilder.AppendFormat("{0}={1}" , "id3", System.Uri.EscapeDataString(id3.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
                httpRequestMessage.Method = new System.Net.Http.HttpMethod("GET");
                httpRequestMessage.RequestUri = new System.Uri(urlStringBuilder.ToString());
                return await _httpClient.SendAsync(httpRequestMessage);
            }
        }
        
        public async System.Threading.Tasks.Task<DotRezCore.Api.Tests.Integration.Models.Order> ApiOrdersByIdGetAsync(int id)
        { 
            string content = string.Empty;
            try
            {
                var response = await ApiOrdersByIdGetResponseAsync(id);
                response.EnsureSuccessStatusCode();
                content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DotRezCore.Api.Tests.Integration.Models.Order>(content, _serializerSettings);
                return result;
            }
            catch(System.Exception ex)
            {
                Microsoft.Extensions.Logging.LoggerExtensions.LogError(_logger, default(Microsoft.Extensions.Logging.EventId), ex, "There was an error while making a API call. Response content is {0}", content);
                throw;
            }
        }
        
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ApiOrdersByIdGetResponseAsync(int id)
        {
            using(var httpRequestMessage = new System.Net.Http.HttpRequestMessage())
            {
                var urlStringBuilder = new System.Text.StringBuilder("api/Orders/{id}");
                urlStringBuilder.Replace("{id}", System.Uri.EscapeDataString(ConvertToString(id, System.Globalization.CultureInfo.InvariantCulture)));
                httpRequestMessage.Method = new System.Net.Http.HttpMethod("GET");
                httpRequestMessage.RequestUri = new System.Uri(urlStringBuilder.ToString());
                return await _httpClient.SendAsync(httpRequestMessage);
            }
        }
        
        public async System.Threading.Tasks.Task<System.Collections.Generic.List<DotRezCore.Api.Tests.Integration.Models.Person>> ApiPeopleGetAsync()
        { 
            string content = string.Empty;
            try
            {
                var response = await ApiPeopleGetResponseAsync();
                response.EnsureSuccessStatusCode();
                content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<DotRezCore.Api.Tests.Integration.Models.Person>>(content, _serializerSettings);
                return result;
            }
            catch(System.Exception ex)
            {
                Microsoft.Extensions.Logging.LoggerExtensions.LogError(_logger, default(Microsoft.Extensions.Logging.EventId), ex, "There was an error while making a API call. Response content is {0}", content);
                throw;
            }
        }
        
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ApiPeopleGetResponseAsync()
        {
            using(var httpRequestMessage = new System.Net.Http.HttpRequestMessage())
            {
                var urlStringBuilder = new System.Text.StringBuilder("api/People");
                httpRequestMessage.Method = new System.Net.Http.HttpMethod("GET");
                httpRequestMessage.RequestUri = new System.Uri(urlStringBuilder.ToString());
                return await _httpClient.SendAsync(httpRequestMessage);
            }
        }
        
        public async System.Threading.Tasks.Task<DotRezCore.Api.Tests.Integration.Models.Person> ApiPeopleByIdGetAsync(int id)
        { 
            string content = string.Empty;
            try
            {
                var response = await ApiPeopleByIdGetResponseAsync(id);
                response.EnsureSuccessStatusCode();
                content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DotRezCore.Api.Tests.Integration.Models.Person>(content, _serializerSettings);
                return result;
            }
            catch(System.Exception ex)
            {
                Microsoft.Extensions.Logging.LoggerExtensions.LogError(_logger, default(Microsoft.Extensions.Logging.EventId), ex, "There was an error while making a API call. Response content is {0}", content);
                throw;
            }
        }
        
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ApiPeopleByIdGetResponseAsync(int id)
        {
            using(var httpRequestMessage = new System.Net.Http.HttpRequestMessage())
            {
                var urlStringBuilder = new System.Text.StringBuilder("api/People/{id}");
                urlStringBuilder.Replace("{id}", System.Uri.EscapeDataString(ConvertToString(id, System.Globalization.CultureInfo.InvariantCulture)));
                httpRequestMessage.Method = new System.Net.Http.HttpMethod("GET");
                httpRequestMessage.RequestUri = new System.Uri(urlStringBuilder.ToString());
                return await _httpClient.SendAsync(httpRequestMessage);
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute)) as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            return System.Convert.ToString(value, cultureInfo);
        }
    
        private string SerializeToQueryString(object request)
        {
            return string.Join("&", System.Linq.Enumerable.Select(request.GetType().GetProperties(), property => $"{property.Name}={System.Uri.EscapeDataString(property.GetValue(request).ToString())}"));
        }
    }
}