using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class RequestLineLexer : Lexer<RequestLine>
    {
        private readonly ILexer<EndOfLine> crLfLexer;
        private readonly ILexer<HttpVersion> httpVersionLexer;
        private readonly ILexer<Method> methodLexer;
        private readonly ILexer<Space> spLexer;
        private readonly ILexer<Token> tokenLexer;

        public RequestLineLexer()
            : this(new MethodLexer(), new SpaceLexer(), new TokenLexer(), new HttpVersionLexer(), new EndOfLineLexer())
        {
        }

        public RequestLineLexer(ILexer<Method> methodLexer, ILexer<Space> spLexer, ILexer<Token> tokenLexer,
            ILexer<HttpVersion> httpVersionLexer, ILexer<EndOfLine> crLfLexer)
            : base("request-line")
        {
            Contract.Requires(methodLexer != null);
            Contract.Requires(spLexer != null);
            Contract.Requires(tokenLexer != null);
            Contract.Requires(httpVersionLexer != null);
            Contract.Requires(crLfLexer != null);
            this.methodLexer = methodLexer;
            this.spLexer = spLexer;
            this.tokenLexer = tokenLexer;
            this.httpVersionLexer = httpVersionLexer;
            this.crLfLexer = crLfLexer;
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
            if (!spLexer.TryRead(scanner, out space1))
            {
                scanner.PutBack(method.Data);
                return Default(out element);
            }

            // TODO: implement a URI parser
            Token requestTarget;
            if (!tokenLexer.TryRead(scanner, out requestTarget))
            {
                scanner.PutBack(space1.Data);
                scanner.PutBack(method.Data);
                return Default(out element);
            }

            Space space2;
            if (!spLexer.TryRead(scanner, out space2))
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
            if (!crLfLexer.TryRead(scanner, out endOfLine))
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
            Contract.Invariant(methodLexer != null);
            Contract.Invariant(spLexer != null);
            Contract.Invariant(tokenLexer != null);
            Contract.Invariant(httpVersionLexer != null);
            Contract.Invariant(crLfLexer != null);
        }
    }
}