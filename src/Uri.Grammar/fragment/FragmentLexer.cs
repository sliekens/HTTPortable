namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class FragmentLexer : Lexer<Fragment>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">*( pchar / "/" / "?" )</param>
        public FragmentLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Fragment> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Fragment>.FromError(new SyntaxError
                {
                    Message = "Expected 'fragment'.",
                    RuleName = "fragment",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Fragment(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Fragment>.FromResult(element);
        }
    }
}