namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class RequestLineLexer : Lexer<RequestLine>
    {
        private readonly ILexer<EndOfLine> endOfLineLexer;
        private readonly ILexer<HttpVersion> httpVersionLexer;
        private readonly ILexer<Method> methodLexer;
        private readonly ILexer<RequestTarget> requestTargetLexer;
        private readonly ILexer<Space> spaceLexer;

        public RequestLineLexer()
            : this(
                new MethodLexer(), new SpaceLexer(), new RequestTargetLexer(), new HttpVersionLexer(), 
                new EndOfLineLexer())
        {
        }

        public RequestLineLexer(ILexer<Method> methodLexer, ILexer<Space> spaceLexer, 
            ILexer<RequestTarget> requestTargetLexer, 
            ILexer<HttpVersion> httpVersionLexer, ILexer<EndOfLine> endOfLineLexer)
            : base("request-line")
        {
            Contract.Requires(methodLexer != null);
            Contract.Requires(spaceLexer != null);
            Contract.Requires(requestTargetLexer != null);
            Contract.Requires(httpVersionLexer != null);
            Contract.Requires(endOfLineLexer != null);
            this.methodLexer = methodLexer;
            this.spaceLexer = spaceLexer;
            this.requestTargetLexer = requestTargetLexer;
            this.httpVersionLexer = httpVersionLexer;
            this.endOfLineLexer = endOfLineLexer;
        }

        public override bool TryRead(ITextScanner scanner, out RequestLine element)
        {
            var context = scanner.GetContext();
            Method method;
            if (!methodLexer.TryRead(scanner, out method))
            {
                return Default(out element);
            }

            Space space1;
            if (!this.spaceLexer.TryRead(scanner, out space1))
            {
                scanner.PutBack(method.Data);
                return Default(out element);
            }

            RequestTarget requestTarget;
            if (!this.requestTargetLexer.TryRead(scanner, out requestTarget))
            {
                scanner.PutBack(space1.Data);
                scanner.PutBack(method.Data);
                return Default(out element);
            }

            Space space2;
            if (!this.spaceLexer.TryRead(scanner, out space2))
            {
                scanner.PutBack(requestTarget.Data);
                scanner.PutBack(space1.Data);
                scanner.PutBack(method.Data);
                return Default(out element);
            }

            HttpVersion httpVersion;
            if (!httpVersionLexer.TryRead(scanner, out httpVersion))
            {
                scanner.PutBack(space2.Data);
                scanner.PutBack(requestTarget.Data);
                scanner.PutBack(space1.Data);
                scanner.PutBack(method.Data);
                return Default(out element);
            }

            EndOfLine endOfLine;
            if (!this.endOfLineLexer.TryRead(scanner, out endOfLine))
            {
                scanner.PutBack(httpVersion.Data);
                scanner.PutBack(space2.Data);
                scanner.PutBack(requestTarget.Data);
                scanner.PutBack(space1.Data);
                scanner.PutBack(method.Data);
                return Default(out element);
            }

            element = new RequestLine(method, space1, requestTarget, space2, httpVersion, endOfLine, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.methodLexer != null);
            Contract.Invariant(this.spaceLexer != null);
            Contract.Invariant(this.requestTargetLexer != null);
            Contract.Invariant(this.httpVersionLexer != null);
            Contract.Invariant(this.endOfLineLexer != null);
        }
    }
}