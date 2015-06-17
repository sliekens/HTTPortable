﻿namespace Http.Grammar.Rfc7230
{
    using SLANG;

    using Uri.Grammar;

    using UriHost = Uri.Grammar.Host;
    using Pseudonym = Token;

    public class ReceivedBy : Alternative<Sequence<UriHost, Option<Sequence<Element, Port>>>, Pseudonym>
    {
        public ReceivedBy(Sequence<UriHost, Option<Sequence<Element, Port>>> element, ITextContext context)
            : base(element, 1, context)
        {
        }

        public ReceivedBy(Pseudonym element, ITextContext context)
            : base(element, 2, context)
        {
        }
    }
}
