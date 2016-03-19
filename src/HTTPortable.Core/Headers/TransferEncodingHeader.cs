namespace Http.Headers
{
    using System.Collections.Generic;

    public class TransferEncodingHeader : Header
    {
        public const string FieldName = "Transfer-Encoding";

        public TransferEncodingHeader()
            : base(FieldName)
        {
        }

        public TransferEncodingHeader(IList<string> transferCodings)
            : base(FieldName)
        {
            AddRange(transferCodings);
        }
    }
}
