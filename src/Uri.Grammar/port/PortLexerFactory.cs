namespace Uri.Grammar.port
{
    using System;

    using SLANG;
    using SLANG.Core.DIGIT;

    public class PortLexerFactory : ILexerFactory<Port>
    {
        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public PortLexerFactory(IRepetitionLexerFactory repetitionLexerFactory, ILexerFactory<Digit> digitLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException("digitLexerFactory");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
        }

        public ILexer<Port> Create()
        {
            var digit = this.digitLexerFactory.Create();
            var innerLexer = this.repetitionLexerFactory.Create(digit, 0, int.MaxValue);
            return new PortLexer(innerLexer);
        }
    }
}