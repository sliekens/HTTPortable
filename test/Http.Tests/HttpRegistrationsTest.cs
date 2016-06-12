using System.Collections.Generic;
using SimpleInjector;
using Txt.ABNF;
using UriSyntax;
using Xunit;
using Registration = Txt.Core.Registration;

namespace Http
{
    public class HttpRegistrationsTest
    {
        private readonly Container container = new Container();

        [Fact]
        public void VerifyRegistrations()
        {
            List<Registration> registrations = new List<Registration>();
            registrations.AddRange(AbnfRegistrations.GetRegistrations(container.GetInstance));
            registrations.AddRange(UriRegistrations.GetRegistrations(container.GetInstance));
            registrations.AddRange(HttpRegistrations.GetRegistrations(container.GetInstance));
            foreach (var registration in registrations)
            {
                if (registration.Implementation != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Implementation);
                }
                if (registration.Instance != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Instance);
                }
                if (registration.Factory != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Factory);
                }
            }
            container.Verify();
        }
    }
}
