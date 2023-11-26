using Newtonsoft.Json;
using System.Collections.Generic;

namespace employee_raffles.Services;

public class BaseService
{
    public Dictionary<string, string> GetData(string Json)
    {
        Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(Json);
        return data;
    }
    public Dictionary<string, object> GetData2(string Json)
    {
        Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Json);
        return data;
    }
}
