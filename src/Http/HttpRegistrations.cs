using System.Collections.Generic;
using System.Reflection;
using Txt;

namespace Http
{
    public class HttpRegistrations : Registrations
    {
        public static IEnumerable<Registration> GetRegistrations(GetInstanceDelegate getInstance)
        {
            return GetRegistrations(typeof(HttpRegistrations).GetTypeInfo().Assembly, getInstance);
        }
    }
}
