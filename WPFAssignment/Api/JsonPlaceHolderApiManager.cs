using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPFAssignment.Model;
using WPFAssignment.ViewModel;

namespace WPFAssignment.Api
{
    public class JsonPlaceHolderApiManager
    {
        /// <summary>
        /// Fetch posts list from "jsonplaceholder" website
        /// </summary>
        public static async Task<IList<PostModel>> GetPostListAsync()
        {
            try
            {
                IList<PostModel> postList = new List<PostModel>();
                var httpResponseMessage = await ApiHelper.GetCall("https://jsonplaceholder.typicode.com/posts");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = await httpResponseMessage.Content.ReadAsStringAsync();

                    postList = JsonConvert.DeserializeObject<List<PostModel>>(result);
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
