﻿using System.Globalization;

namespace HTTPortable.Core.Headers
{
    public class ContentLengthHeader : Header
    {
        public const string FieldName = "Content-Length";

        public ContentLengthHeader()
            : base(FieldName)
        {
        }

        public ContentLengthHeader(long contentLength)
            : base(FieldName)
        {
            Add(contentLength.ToString(NumberFormatInfo.InvariantInfo));
        }
    }
}