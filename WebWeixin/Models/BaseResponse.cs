using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class BaseResponse
    {
        public int Ret { get; set; }
        public string ErrMsg { get; set; }
    }
}
