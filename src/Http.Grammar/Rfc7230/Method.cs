﻿using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class Method : Element
    {
        public Method(Token token, ITextContext context)
            : base(token.Data, context)
        {
            Contract.Requires(token != null);
        }
    }
}