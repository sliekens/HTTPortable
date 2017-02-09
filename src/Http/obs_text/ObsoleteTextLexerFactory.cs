using System.Text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.obs_text
{
    public class ObsoleteTextLexerFactory : RuleLexerFactory<ObsoleteText>
    {
        static ObsoleteTextLexerFactory()
        {
            Default = new ObsoleteTextLexerFactory();
        }

        [NotNull]
        public static ObsoleteTextLexerFactory Default { get; }

        public override ILexer<ObsoleteText> Create()
        {
            var innerLexer = ValueRange.Create(0x80, 0xFF, Encoding.UTF8);
            return new ObsoleteTextLexer(innerLexer);
        }
    }
}
