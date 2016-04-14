using System;
using Txt;

namespace Http.obs_text
{
    public class ObsoleteTextLexerFactory : ILexerFactory<ObsoleteText>
    {
        public ILexer<ObsoleteText> Create()
        {
            throw new NotImplementedException();
        }
    }
}