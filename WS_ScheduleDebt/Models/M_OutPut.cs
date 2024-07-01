using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_ScheduleDebt.Models
{
    public class M_OutPut
    {
        public string ApplicationCode { get; set; }
        public string File_Base64 { get; set; }

        public string Error { get; set; }
        public string Message { get; set; }
    }
}