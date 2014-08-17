using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barter.li.Win.Util
{
    public class ResponseConverter
    {
        public async static Task<T> Deserialize<T>(string response)
        {
            try
            {
                return  await Task.Factory.StartNew<T>(()=> Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response));                
            }

            catch(Exception e)
            {
                throw e;
            }           
        }

        public async static Task<String> Serialize(string value)
        {            
            try
            {
                return await Task.Factory.StartNew<String>(() => Newtonsoft.Json.JsonConvert.SerializeObject(value));
            }

            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
