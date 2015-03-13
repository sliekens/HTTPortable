namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using SLANG;

    class UpgradeLexer : Lexer<Upgrade>
    {
        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;
        private readonly ILexer<Protocol> protocolLexer;

        public UpgradeLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<Protocol> protocolLexer)
            : base("Upgrade")
        {
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.protocolLexer = protocolLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Upgrade element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Upgrade);
                return false;
            }

            var context = scanner.GetContext();
            Repetition<Sequence<Element, OptionalWhiteSpace>> commaWhiteSpaceMany;
            if (!this.TryReadCommaWhiteSpaceMany(scanner, out commaWhiteSpaceMany))
            {
                element = default(Upgrade);
                return false;
            }

            Protocol protocol;
            if (!this.protocolLexer.TryRead(scanner, out protocol))
            {
                scanner.PutBack(commaWhiteSpaceMany.Data);
                element = default(Upgrade);
                return false;
            }

            Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>> whiteSpaceCommaOptionalWhiteSpaceProtocolMany;
            if (!this.TryReadWhiteSpaceCommaOptionalWhiteSpaceProtocolMany(scanner, out whiteSpaceCommaOptionalWhiteSpaceProtocolMany))
            {
                scanner.PutBack(protocol.Data);
                scanner.PutBack(commaWhiteSpaceMany.Data);
                element = default(Upgrade);
                return false;
            }

            element = new Upgrade(commaWhiteSpaceMany, protocol, whiteSpaceCommaOptionalWhiteSpaceProtocolMany, context);
            return true;
        }

        private bool TryReadCommaWhiteSpaceMany(ITextScanner scanner, out Repetition<Sequence<Element, OptionalWhiteSpace>> element)
        {
            var context = scanner.GetContext();
            var elements = new List<Sequence<Element, OptionalWhiteSpace>>();
            Sequence<Element, OptionalWhiteSpace> sequence;
            while (this.TryReadCommaWhiteSpace(scanner, out sequence))
            {
                elements.Add(sequence);
            }

            element = new Repetition<Sequence<Element, OptionalWhiteSpace>>(elements, context);
            return true;
        }

        private bool TryReadCommaWhiteSpace(ITextScanner scanner, out Sequence<Element, OptionalWhiteSpace> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<Element, OptionalWhiteSpace>);
                return false;
            }

            var context = scanner.GetContext();
            Element comma;
            if (!TryReadTerminal(scanner, @",", out comma))
            {
                element = default(Sequence<Element, OptionalWhiteSpace>);
                return false;
            }

            OptionalWhiteSpace optionalWhiteSpace;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace))
            {
                scanner.PutBack(comma.Data);
                element = default(Sequence<Element, OptionalWhiteSpace>);
                return false;
            }

            element = new Sequence<Element, OptionalWhiteSpace>(comma, optionalWhiteSpace, context);
            return true;
        }

        private bool TryReadWhiteSpaceCommaOptionalWhiteSpaceProtocolMany(ITextScanner scanner, out Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>> element)
        {
            var context = scanner.GetContext();
            var elements =
                new List<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>>();
            Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>> sequence;
            while (this.TryReadWhiteSpaceCommaOptionalWhiteSpaceProtocol(scanner, out sequence))
            {
                elements.Add(sequence);
            }

            element = new Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>>(elements, context);
            return true;
        }

        private bool TryReadWhiteSpaceCommaOptionalWhiteSpaceProtocol(ITextScanner scanner, out Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>);
                return false;
            }

            var context = scanner.GetContext();
            OptionalWhiteSpace optionalWhiteSpace;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace))
            {
                element = default(Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>);
                return false;
            }

            Element comma;
            if (!TryReadTerminal(scanner, @",", out comma))
            {
                scanner.PutBack(optionalWhiteSpace.Data);
                element = default(Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>);
                return false;
            }

            Option<Sequence<OptionalWhiteSpace, Protocol>> optionalWhiteSpaceProtocol;
            if (!this.TryReadOptionalWhiteSpaceProtocol(scanner, out optionalWhiteSpaceProtocol))
            {
                scanner.PutBack(comma.Data);
                scanner.PutBack(optionalWhiteSpace.Data);
                element = default(Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>);
                return false;
            }

            element = new Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>(optionalWhiteSpace, comma, optionalWhiteSpaceProtocol, context);
            return true;
        }


        private bool TryReadOptionalWhiteSpaceProtocol(ITextScanner scanner, out Option<Sequence<OptionalWhiteSpace, Protocol>> element)
        {
            var context = scanner.GetContext();
            Sequence<OptionalWhiteSpace, Protocol> whiteSpaceProtocol;
            if (this.TryReadWhiteSpaceProtocol(scanner, out whiteSpaceProtocol))
            {
                element = new Option<Sequence<OptionalWhiteSpace, Protocol>>(whiteSpaceProtocol, context);
            }
            else
            {
                element = new Option<Sequence<OptionalWhiteSpace, Protocol>>(context);
            }

            return true;
        }

        private bool TryReadWhiteSpaceProtocol(ITextScanner scanner, out Sequence<OptionalWhiteSpace, Protocol> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<OptionalWhiteSpace, Protocol>);
                return false;
            }

            var context = scanner.GetContext();
            OptionalWhiteSpace optionalWhiteSpace;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace))
            {
                element = default(Sequence<OptionalWhiteSpace, Protocol>);
                return false;
            }

            Protocol protocol;
            if (!this.protocolLexer.TryRead(scanner, out protocol))
            {
                scanner.PutBack(optionalWhiteSpace.Data);
                element = default(Sequence<OptionalWhiteSpace, Protocol>);
                return false;
            }

            element = new Sequence<OptionalWhiteSpace, Protocol>(optionalWhiteSpace, protocol, context);
            return true;
        }
    }
}