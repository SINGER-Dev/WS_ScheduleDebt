using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WS_ScheduleDebt.Class;
using WS_ScheduleDebt.Models;

namespace WS_ScheduleDebt.Controllers
{
    
    public class DataController : ApiController
    {
        
        [HttpPost, ActionName("ScheduleLoanHP")]
        public M_OutPut Gen_ScheduleLoan(M_Input mi)
        {
            M_OutPut mo = new M_OutPut();
            try
            {

                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                string c = headers.GetValues("x-apikey").First();
                if (!headers.Contains("x-apikey"))
                {

                    //headers.
                    // headers.Connection

                   // result = "invalid request header.";
                }
                else
                {
                    if (c == ConfigurationManager.AppSettings["API_KEY"].ToString())
                    {
                        C_ScheduleLoan mc = new C_ScheduleLoan();
                        string zip_path = mc.Call_ScheduleLoan(mi.ApplicationCode, HttpContext.Current.Server.MapPath("~/"));
                        Byte[] bytes = File.ReadAllBytes(zip_path);
                        String file = Convert.ToBase64String(bytes);

                        mo.ApplicationCode = mi.ApplicationCode;
                        mo.File_Base64 = file;
                        mo.Error = "False";
                        mo.Message = "";
                    }
                }
      

            }
            catch (Exception i)
            {
                mo.ApplicationCode = mi.ApplicationCode;
                mo.File_Base64 = "";
                mo.Error = "True";
                mo.Message = i.Message.ToString();
            }

            return mo;
            //functional requirements  
        }



      



    }
}
