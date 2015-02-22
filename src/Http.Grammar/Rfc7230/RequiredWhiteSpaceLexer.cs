﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class RequiredWhiteSpaceLexer : Lexer<RequiredWhiteSpace>
    {
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public RequiredWhiteSpaceLexer()
            : this(new WhiteSpaceLexer())
        {
        }

        public RequiredWhiteSpaceLexer(ILexer<WhiteSpace> whiteSpaceLexer)
        {
            Contract.Requires(whiteSpaceLexer != null);
            this.whiteSpaceLexer = whiteSpaceLexer;
        }

        public override RequiredWhiteSpace Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            RequiredWhiteSpace element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'RWS'");
        }

        public override bool TryRead(ITextScanner scanner, out RequiredWhiteSpace element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RequiredWhiteSpace);
                return false;
            }

            var context = scanner.GetContext();
            WhiteSpace whiteSpace;
            IList<WhiteSpace> elements = new List<WhiteSpace>();
            while (this.whiteSpaceLexer.TryRead(scanner, out whiteSpace))
            {
                elements.Add(whiteSpace);
            }

            if (elements.Count == 0)
            {
                element = default(RequiredWhiteSpace);
                return false;
            }

            element = new RequiredWhiteSpace(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.whiteSpaceLexer != null);
        }
    }
}