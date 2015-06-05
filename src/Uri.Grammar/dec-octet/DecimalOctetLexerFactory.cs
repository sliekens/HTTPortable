namespace Uri.Grammar.dec_octet
{
    using System;

    using SLANG;
    using SLANG.Core.DIGIT;

    public class DecimalOctetLexerFactory : ILexerFactory<DecimalOctet>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public DecimalOctetLexerFactory(
            IValueRangeLexerFactory valueRangeLexerFactory,
            IStringLexerFactory stringLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory)
        {
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException("valueRangeLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException("digitLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<DecimalOctet> Create()
        {
            // %x30-35
            var a = this.valueRangeLexerFactory.Create('\x30', '\x35');

            // "25"
            var b = this.stringLexerFactory.Create("25");

            // "25" %x30-35 
            var c = this.sequenceLexerFactory.Create(b, a);

            // DIGIT
            var d = this.digitLexerFactory.Create();

            // %x30-34
            var e = this.valueRangeLexerFactory.Create('\x30', '\x34');

            // "2"
            var f = this.stringLexerFactory.Create("2");

            // "2" %x30-34 DIGIT 
            var g = this.sequenceLexerFactory.Create(f, e, d);

            // 2DIGIT
            var h = this.repetitionLexerFactory.Create(d, 2, 2);

            // "1"
            var i = this.stringLexerFactory.Create("1");

            // "1" 2DIGIT  
            var j = this.sequenceLexerFactory.Create(i, h);

            // %x31-39
            var k = this.valueRangeLexerFactory.Create('\x31', '\x39');

            // %x31-39 DIGIT 
            var l = this.sequenceLexerFactory.Create(k, d);

            // DIGIT / %x31-39 DIGIT / "1" 2DIGIT / "2" %x30-34 DIGIT / "25" %x30-35
            //var m = this.alternativeLexerFactory.Create(d, l, j, g, c);

            // MEMO: I reversed the rule, because the rule would never work with greedy matching

            // "25" %x30-35 / "2" %x30-34 DIGIT / "1" 2DIGIT / %x31-39 DIGIT / DIGIT
            var m = this.alternativeLexerFactory.Create(c, g, j, l, d);

            // dec-octet
            return new DecimalOctetLexer(m);
        }
    }
}