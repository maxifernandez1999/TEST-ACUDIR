using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acudir.Test.Core.Domain.Entities
{
    public abstract class ResponseBase
    {
        public int Code { get; set; } = 0;

        public string Message { get; set; } = "";
    }
}
