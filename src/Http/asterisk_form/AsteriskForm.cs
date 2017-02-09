using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.asterisk_form
{
    public class AsteriskForm : Terminal
    {
        public AsteriskForm([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
