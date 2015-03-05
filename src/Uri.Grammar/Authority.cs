using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uri.Grammar
{
    using Text.Scanning;

    public class Authority : Element
    {
        public Authority(string data, ITextContext context)
            : base(data, context)
        {
        }
    }
}
