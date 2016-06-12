using System;
using System.Text;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.obs_text
{
    public class ObsoleteTextLexerFactory : ILexerFactory<ObsoleteText>
    {
        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public ObsoleteTextLexerFactory([NotNull] IValueRangeLexerFactory valueRangeLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            this.valueRangeLexerFactory = valueRangeLexerFactory;
        }

        public ILexer<ObsoleteText> Create()
        {
            return new ObsoleteTextLexer(valueRangeLexerFactory.Create(0x80, 0xFF, Encoding.UTF8));
        }
    }
}
