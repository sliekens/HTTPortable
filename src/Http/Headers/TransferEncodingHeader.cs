using System.Collections.Generic;

namespace Http.Headers
{
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
