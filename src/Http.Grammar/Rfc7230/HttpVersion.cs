using System;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class HttpVersion : Element
    {
        private readonly HttpName httpName;
        private readonly Digit digit1;
        private readonly Digit digit2;

        public HttpVersion(HttpName httpName, Digit digit1, Digit digit2, ITextContext context)
            : base(string.Concat(httpName.Data, "/", digit1.Data, ".", digit2.Data), context)
        {
            Contract.Requires(httpName != null);
            Contract.Requires(digit1 != null);
            Contract.Requires(digit2 != null);
            this.httpName = httpName;
            this.digit1 = digit1;
            this.digit2 = digit2;
        }

        public HttpName HttpName
        {
            get
            {
                return this.httpName;
            }
        }

        public Digit Digit1
        {
            get
            {
                return this.digit1;
            }
        }

        public Digit Digit2
        {
            get
            {
                return this.digit2;
            }
        }

        public Version ToVersion()
        {
            var major = int.Parse(this.Digit1.Data);
            var minor = int.Parse(this.Digit2.Data);
            return new Version(major, minor);
        }

        [ContractInvariantMethod]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.httpName != null);
            Contract.Invariant(this.digit1 != null);
            Contract.Invariant(this.digit2 != null);
        }
    }
}
