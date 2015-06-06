// ReSharper disable InconsistentNaming

namespace Uri.Grammar.IPv6address
{
    using System;

    using SLANG;

    public class IPv6AddressLexerFactory : ILexerFactory<IPv6Address>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory;

        private readonly ILexerFactory<LeastSignificantInt32> leastSignificantInt32LexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public IPv6AddressLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IStringLexerFactory stringLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            ILexerFactory<LeastSignificantInt32> leastSignificantInt32LexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException("optionLexerFactory");
            }

            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException("hexadecimalInt16LexerFactory");
            }

            if (leastSignificantInt32LexerFactory == null)
            {
                throw new ArgumentNullException("leastSignificantInt32LexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.hexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            this.leastSignificantInt32LexerFactory = leastSignificantInt32LexerFactory;
        }

        public ILexer<IPv6Address> Create()
        {
            // ":"
            var colon = this.stringLexerFactory.Create(@":");

            // "::"
            var collapse = this.stringLexerFactory.Create(@"::");

            // h16
            var h16 = this.hexadecimalInt16LexerFactory.Create();

            // ls32
            var ls32 = this.leastSignificantInt32LexerFactory.Create();

            // h16 ":"
            var h16c = this.sequenceLexerFactory.Create(h16, colon);

            // h16-2
            var h16c2 = this.alternativeLexerFactory.Create(this.sequenceLexerFactory.Create(this.optionLexerFactory.Create(h16c), h16), h16);

            // h16-3
            var h16c3 =
                this.alternativeLexerFactory.Create(
                    this.sequenceLexerFactory.Create(this.repetitionLexerFactory.Create(h16c, 0, 2), h16),
                    h16c2);

            // h16-4
            var h16c4 =
                this.alternativeLexerFactory.Create(
                    this.sequenceLexerFactory.Create(this.repetitionLexerFactory.Create(h16c, 0, 3), h16),
                    h16c3);

            // h16-5
            var h16c5 =
                this.alternativeLexerFactory.Create(
                    this.sequenceLexerFactory.Create(this.repetitionLexerFactory.Create(h16c, 0, 4), h16),
                    h16c4);

            // h16-6
            var h16c6 =
                this.alternativeLexerFactory.Create(
                    this.sequenceLexerFactory.Create(this.repetitionLexerFactory.Create(h16c, 0, 5), h16),
                    h16c5);

            // h16-7
            var h16c7 =
                this.alternativeLexerFactory.Create(
                    this.sequenceLexerFactory.Create(this.repetitionLexerFactory.Create(h16c, 0, 6), h16),
                    h16c6);

            // 6( h16 ":" ) ls32
            var alternative1 = this.sequenceLexerFactory.Create(this.repetitionLexerFactory.Create(h16c, 6, 6), ls32);

            // "::" 5( h16 ":" ) ls32
            var alternative2 = this.sequenceLexerFactory.Create(
                collapse,
                this.repetitionLexerFactory.Create(h16c, 5, 5),
                ls32);

            // [ h16 ] "::" 4( h16 ":" ) ls32
            var alternative3 = this.sequenceLexerFactory.Create(
                this.optionLexerFactory.Create(h16),
                collapse,
                this.repetitionLexerFactory.Create(h16c, 4, 4),
                ls32);

            // [ h16-2 ] "::" 3( h16 ":" ) ls32
            var alternative4 = this.sequenceLexerFactory.Create(
                this.optionLexerFactory.Create(h16c2),
                collapse,
                this.repetitionLexerFactory.Create(h16c, 3, 3),
                ls32);

            // [ h16-3 ] "::" 2( h16 ":" ) ls32
            var alternative5 = this.sequenceLexerFactory.Create(
                this.optionLexerFactory.Create(h16c3),
                collapse,
                this.repetitionLexerFactory.Create(h16c, 2, 2),
                ls32);

            // [ h16-4 ] "::" h16 ":" ls32
            var alternative6 = this.sequenceLexerFactory.Create(
                this.optionLexerFactory.Create(h16c4),
                collapse,
                h16,
                colon,
                ls32);

            // [ h16-5 ] "::" ls32
            var alternative7 = this.sequenceLexerFactory.Create(this.optionLexerFactory.Create(h16c5), collapse, ls32);

            // [ h16-6 ] "::" h16
            var alternative8 = this.sequenceLexerFactory.Create(this.optionLexerFactory.Create(h16c6), collapse, h16);

            // [ h16-7 ] "::"
            var alternative9 = this.sequenceLexerFactory.Create(this.optionLexerFactory.Create(h16c7), collapse);

            var innerLexer = this.alternativeLexerFactory.Create(
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