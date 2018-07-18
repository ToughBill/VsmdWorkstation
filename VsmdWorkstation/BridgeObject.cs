using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    public class BridgeObject
    {

        public void Move(string args)
        {
            JArray jsArr = (JArray)JsonConvert.DeserializeObject(args);
            for (int i = 0; i < jsArr.Count; i++)
            {
                JObject obj = (JObject)jsArr[i];
                int row = int.Parse(obj["row"].ToString());
                int col = int.Parse(obj["column"].ToString());
            }
            
        }
    }
}
