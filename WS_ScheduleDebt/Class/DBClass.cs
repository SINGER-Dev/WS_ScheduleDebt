using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
//using System.Data.OracleClient;

namespace WS_ScheduleDebt.Class
{
    public class DBClass
    {
        //SQL Server
        public SqlConnection SqlStrCon(string NameServer)
        {
            //return new SqlConnection("Data Source=sg-k2dev;Initial Catalog=CORELOAN;User Id=phannut;Password=phannut;");

            return new SqlConnection(ConfigurationManager.AppSettings[NameServer].ToString());

        }

        //ORACLE Server
        //public OracleConnection OracleStrCon(string NameServer)
        //{
        //    return new OracleConnection(ConfigurationManager.AppSettings[NameServer].ToString());

        //}


    }
}