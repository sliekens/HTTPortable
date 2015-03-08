namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;

    
    using ParameterPart = SLANG.Sequence<OptionalWhiteSpace, SLANG.Element, OptionalWhiteSpace, TransferParameter>;

    public class TransferExtensionLexer : Lexer<TransferExtension>
    {
        private readonly ILexer<Token> tokenLexer;

        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        private readonly ILexer<TransferParameter> transferParameterLexer;

        public TransferExtensionLexer()
            : this(new TokenLexer(), new OptionalWhiteSpaceLexer(), new TransferParameterLexer())
        {
        }

        public TransferExtensionLexer(ILexer<Token> tokenLexer, ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<TransferParameter> transferParameterLexer)
            : base("transfer-extension")
        {
            Contract.Requires(tokenLexer != null);
            Contract.Requires(optionalWhiteSpaceLexer != null);
            Contract.Requires(transferParameterLexer != null);
            this.tokenLexer = tokenLexer;
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.transferParameterLexer = transferParameterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out TransferExtension element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TransferExtension);
                return false;
            }

            var context = scanner.GetContext();
            Token extension;
            if (!this.tokenLexer.TryRead(scanner, out extension))
            {
                element = default(TransferExtension);
                return false;
            }

            var parameters = new List<ParameterPart>();
            ParameterPart parameter;
            while (this.TryReadParameterPart(scanner, out parameter))
            {
                parameters.Add(parameter);
            }

            element = new TransferExtension(extension, parameters, context);
            return true;
        }

        private bool TryReadParameterPart(ITextScanner scanner, out ParameterPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ParameterPart);
                return false;
            }

            var context = scanner.GetContext();
            OptionalWhiteSpace optionalWhiteSpace1;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace1))
            {
                element = default(ParameterPart);
                return false;
            }

            Element semicolon;
            if (!TryReadTerminal(scanner, ';', out semicolon))
            {
                scanner.PutBack(optionalWhiteSpace1.Data);
                element = default(ParameterPart);
                return false;
            }

            OptionalWhiteSpace optionalWhiteSpace2;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace2))
            {
                scanner.PutBack(semicolon.Data);
                scanner.PutBack(optionalWhiteSpace1.Data);
                element = default(ParameterPart);
                return false;
            }

            TransferParameter transferParameter;
            if (!this.transferParameterLexer.TryRead(scanner, out transferParameter))
            {
                scanner.PutBack(transferParameter.Data);
                scanner.PutBack(semicolon.Data);
                scanner.PutBack(optionalWhiteSpace1.Data);
                element = default(ParameterPart);
                return false;
            }

            element = new ParameterPart(optionalWhiteSpace1, semicolon, optionalWhiteSpace2, transferParameter, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
            Contract.Invariant(this.optionalWhiteSpaceLexer != null);
            Contract.Invariant(this.transferParameterLexer != null);
        }
    }
}