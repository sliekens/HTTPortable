using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.obs_text
{
    public class ObsoleteText : Terminal
    {
        public ObsoleteText([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
