﻿namespace Http.Grammar
{
    using System;

    using TextFx;

    public class BadWhiteSpaceLexer : Lexer<BadWhiteSpace>
    {
        private readonly ILexer<OptionalWhiteSpace> innerLexer;

        public BadWhiteSpaceLexer(ILexer<OptionalWhiteSpace> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out BadWhiteSpace element)
        {
            OptionalWhiteSpace result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new BadWhiteSpace(result);
                return true;
            }

            element = default(BadWhiteSpace);
            return false;
        }
    }
}