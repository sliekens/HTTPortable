// ReSharper disable InconsistentNaming

namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPv6AddressLexerFactory : ILexerFactory<IPv6Address>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory;

        private readonly ILexerFactory<LeastSignificantInt32> leastSignificantInt32LexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPv6AddressLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            ILexerFactory<LeastSignificantInt32> leastSignificantInt32LexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16LexerFactory));
            }

            if (leastSignificantInt32LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(leastSignificantInt32LexerFactory));
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.hexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            this.leastSignificantInt32LexerFactory = leastSignificantInt32LexerFactory;
        }

        public ILexer<IPv6Address> Create()
        {
            // ":"
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // "::"
            var collapse = terminalLexerFactory.Create(@"::", StringComparer.Ordinal);

            // h16
            var h16 = hexadecimalInt16LexerFactory.Create();

            // ls32
            var ls32 = leastSignificantInt32LexerFactory.Create();

            // h16 ":"
            var h16c = concatenationLexerFactory.Create(h16, colon);

            // h16-2
            var h16c2 = alternativeLexerFactory.Create(concatenationLexerFactory.Create(h16, colon, h16), h16);

            // h16-3
            var h16c3 =
                alternativeLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 2), h16),
                    h16c2);

            // h16-4
            var h16c4 =
                alternativeLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 3), h16),
                    h16c3);

            // h16-5
            var h16c5 =
                alternativeLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 4), h16),
                    h16c4);

            // h16-6
            var h16c6 =
                alternativeLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 5), h16),
                    h16c5);

            // h16-7
            var h16c7 =
                alternativeLexerFactory.Create(
                    concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 0, 6), h16),
                    h16c6);

            // 6( h16 ":" ) ls32
            var alternative1 = concatenationLexerFactory.Create(repetitionLexerFactory.Create(h16c, 6, 6), ls32);

            // "::" 5( h16 ":" ) ls32
            var alternative2 = concatenationLexerFactory.Create(
                collapse,
                repetitionLexerFactory.Create(h16c, 5, 5),
                ls32);

            // [ h16 ] "::" 4( h16 ":" ) ls32
            var alternative3 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16),
                collapse,
                repetitionLexerFactory.Create(h16c, 4, 4),
                ls32);

            // [ h16-2 ] "::" 3( h16 ":" ) ls32
            var alternative4 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16c2),
                collapse,
                repetitionLexerFactory.Create(h16c, 3, 3),
                ls32);

            // [ h16-3 ] "::" 2( h16 ":" ) ls32
            var alternative5 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16c3),
                collapse,
                repetitionLexerFactory.Create(h16c, 2, 2),
                ls32);

            // [ h16-4 ] "::" h16 ":" ls32
            var alternative6 = concatenationLexerFactory.Create(
                optionLexerFactory.Create(h16c4),
                collapse,
                h16,
                colon,
                ls32);

            // [ h16-5 ] "::" ls32
            var alternative7 = concatenationLexerFactory.Create(optionLexerFactory.Create(h16c5), collapse, ls32);

            // [ h16-6 ] "::" h16
            var alternative8 = concatenationLexerFactory.Create(optionLexerFactory.Create(h16c6), collapse, h16);

            // [ h16-7 ] "::"
            var alternative9 = concatenationLexerFactory.Create(optionLexerFactory.Create(h16c7), collapse);

            var innerLexer = alternativeLexerFactory.Create(
                alternative1,
                alternative2,
                alternative3,
                alternative4,
                alternative5,
                alternative6,
                alternative7,
                alternative8,
                alternative9);

            return new IPv6AddressLexer(innerLexer);
        }
    }
}