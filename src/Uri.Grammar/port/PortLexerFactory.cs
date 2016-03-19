namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class PortLexerFactory : ILexerFactory<Port>
    {
        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public PortLexerFactory(IRepetitionLexerFactory repetitionLexerFactory, ILexerFactory<Digit> digitLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
        }

        public ILexer<Port> Create()
        {
            var digit = digitLexerFactory.Create();
            var innerLexer = repetitionLexerFactory.Create(digit, 0, int.MaxValue);
            return new PortLexer(innerLexer);
        }
    }
}