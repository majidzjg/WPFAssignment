using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPFAssignment.ViewModel;

namespace WPFAssignment.Api
{
    public class JsonPlaceHolderApiManager
    {
        /// <summary>
        /// Fetch posts list from "jsonplaceholder" website
        /// </summary>
        public static async Task<IList<Post>> GetPostListAsync()
        {
            try
            {
                IList<Post> postList = new List<Post>();
                var httpResponseMessage = await ApiHelper.GetCall("https://jsonplaceholder.typicode.com/posts");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = await httpResponseMessage.Content.ReadAsStringAsync();

                    postList = JsonConvert.DeserializeObject<List<Post>>(result);
                }

                return postList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
