using System.Collections.Generic;
using System.Reflection;
using Txt;

namespace Http
{
    public class HttpRegistrations : Registrations
    {
        public static IEnumerable<Registration> GetRegistrations(GetInstanceDelegate getInstance)
        {
            yield return new Registration(typeof(IRequiredDelimitedListLexerFactory), typeof(RequiredDelimitedListLexerFactory));
            yield return new Registration(typeof(IOptionalDelimitedListLexerFactory), typeof(OptionalDelimitedListLexerFactory));
            foreach (var registration in GetRegistrations(typeof(HttpRegistrations).GetTypeInfo().Assembly, getInstance))
            {
                yield return registration;
            }
        }
    }
}
