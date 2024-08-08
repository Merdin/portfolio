using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace APISDK
{
    public class SessionManager
    {
        public List<string> Get(ISession session, string sessionKey)
        {
            var value = session.GetString(sessionKey);
            if (value == null)
            {
                var values = new List<string>();
                session.SetString(sessionKey, JsonConvert.SerializeObject(values));
                return values;
            }
            else
            {
                return JsonConvert.DeserializeObject<List<string>>(value);
            }

        }

        public void Set(ISession session, string sessionKey, string value)
        {
            var sessionList = Get(session, sessionKey);
            sessionList.Add(value);
            session.SetString(sessionKey, JsonConvert.SerializeObject(sessionList));
        }

        public void StopSession(ISession session, string sessionKey)
        {
            session.Remove(sessionKey);
        }
    }
}