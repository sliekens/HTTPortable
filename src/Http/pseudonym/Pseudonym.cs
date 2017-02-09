using Http.token;
using JetBrains.Annotations;

namespace Http.pseudonym
{
    public class Pseudonym : Token
    {
        public Pseudonym([NotNull] Token token)
            : base(token)
        {
        }
    }
}