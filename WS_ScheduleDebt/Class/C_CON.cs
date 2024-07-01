using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
//using System.Data.OracleClient;

namespace WS_ScheduleDebt.Class
{
    public class C_CON
    {
        public DataTable CALL_STORE(string NameServer, string NameStore, List<string> list_NameParam, List<string> list_Param)
        {

            DataTable dt = new DataTable();
            SqlConnection myConn = new SqlConnection();
            string status = string.Empty;
            try
            {
                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand();


                myConn = new DBClass().SqlStrCon(NameServer);

                command.Connection = myConn;
                command.CommandType = CommandType.StoredProcedure;




                command = new SqlCommand(NameStore, myConn);
                command.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < list_NameParam.Count(); i++)
                {
                    if (list_Param[i] != "--")
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], list_Param[i]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], DBNull.Value);
                    }
                }

                //command.Parameters.AddWithValue("@PASS", Pass);
                command.CommandTimeout = 0;

                dtAdapter.SelectCommand = command;

                dtAdapter.Fill(dt);




            }
            catch (Exception e)
            {

            }
            finally
            {
                myConn.Close();
            }

            return dt;
        }


        public DataTable CALL_STORE(string NameServer, string NameStore)
        {

            DataTable dt = new DataTable();
            SqlConnection myConn = new SqlConnection();
            string status = string.Empty;
            try
            {
                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand();


                myConn = new DBClass().SqlStrCon(NameServer);

                command.Connection = myConn;
                command.CommandType = CommandType.StoredProcedure;




                command = new SqlCommand(NameStore, myConn);
                command.CommandType = CommandType.StoredProcedure;


                //command.Parameters.AddWithValue("@PASS", Pass);
                command.CommandTimeout = 0;
                dtAdapter.SelectCommand = command;

                dtAdapter.Fill(dt);




            }
            catch (Exception e)
            {

            }
            finally
            {
                myConn.Close();
            }

            return dt;
        }


        public string CALL_STORE_INSERT(string NameServer, string NameStore, List<string> list_NameParam, List<string> list_Param)
        {

            DataTable dt = new DataTable();
            SqlConnection myConn = new SqlConnection();
            string status = string.Empty;
            try
            {
                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand();


                myConn = new DBClass().SqlStrCon(NameServer);

                command.Connection = myConn;
                command.CommandType = CommandType.StoredProcedure;




                command = new SqlCommand(NameStore, myConn);
                command.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < list_NameParam.Count(); i++)
                {
                    if (list_Param[i] != "--")
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], list_Param[i]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], DBNull.Value);
                    }
                }

                //command.Parameters.AddWithValue("@PASS", Pass);
                command.CommandTimeout = 0;
                dtAdapter.SelectCommand = command;

                dtAdapter.Fill(dt);

                status = "Pass";

    

            }
            catch (Exception e)
            {
                status = "Fail";
            }
            finally
            {
                myConn.Close();
            }

            return status;
        }


        public string CALL_STORE_UPDATE(string NameServer, string NameStore, List<string> list_NameParam, List<string> list_Param)
        {

            DataTable dt = new DataTable();
            SqlConnection myConn = new SqlConnection();
            string status = string.Empty;
            try
            {
                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand();


                myConn = new DBClass().SqlStrCon(NameServer);

                command.Connection = myConn;
                command.CommandType = CommandType.StoredProcedure;




                command = new SqlCommand(NameStore, myConn);
                command.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < list_NameParam.Count(); i++)
                {
                    if (list_Param[i] != "--")
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], list_Param[i]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], DBNull.Value);
                    }
                }

                //command.Parameters.AddWithValue("@PASS", Pass);
                command.CommandTimeout = 0;
                dtAdapter.SelectCommand = command;

                dtAdapter.Fill(dt);

                status = "Pass";


            }
            catch (Exception e)
            {
                status = "Fail";
            }
            finally
            {
                myConn.Close();
            }

            return status;
        }


        public string CALL_STORE_UPDATE_NOT_PARAM(string NameServer, string NameStore)
        {

            DataTable dt = new DataTable();
            SqlConnection myConn = new SqlConnection();
            string status = string.Empty;
            try
            {
                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand();


                myConn = new DBClass().SqlStrCon(NameServer);

                command.Connection = myConn;
                command.CommandType = CommandType.StoredProcedure;


                command = new SqlCommand(NameStore, myConn);
                command.CommandType = CommandType.StoredProcedure;

            

                //command.Parameters.AddWithValue("@PASS", Pass);
                command.CommandTimeout = 0;
                dtAdapter.SelectCommand = command;

                dtAdapter.Fill(dt);

                status = "Pass";


            }
            catch (Exception e)
            {
                status = "Fail";
            }
            finally
            {
                myConn.Close();
            }

            return status;
        }

        //public DataTable CALL_ORACLE(string NameServer, string Script)
        //{

        //    DataTable dt = new DataTable();
        //    OracleConnection myConn = new OracleConnection();
        //    string status = string.Empty;
        //    try
        //    {
        //        OracleDataAdapter dtAdapter = new OracleDataAdapter();

        //        OracleCommand command = new OracleCommand();


        //        myConn = new DBClass().OracleStrCon(NameServer);

        //        command.Connection = myConn;
        //        command.CommandType = CommandType.Text;




        //        command = new OracleCommand(Script, myConn);


        //        //command.Parameters.AddWithValue("@PASS", Pass);
        //        command.CommandTimeout = 0;
        //        dtAdapter.SelectCommand = command;

        //        dtAdapter.Fill(dt);




        //    }
        //    catch (Exception e)
        //    {
             
           
        //    }
        //    finally
        //    {
        //        myConn.Close();
        //    }

        //    return dt;
        //}


        public DataSet CALL_STORE_RETURN_MULTI_TABLE(string NameServer, string NameStore, List<string> list_NameParam, List<string> list_Param)
        {
            DataSet ds = new DataSet();
            SqlConnection myConn = new SqlConnection();
            string status = string.Empty;

            SqlDataAdapter dtAdapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();

            try
            {
                myConn = new DBClass().SqlStrCon(NameServer);

                command.Connection = myConn;
                command.CommandType = CommandType.StoredProcedure;

                command = new SqlCommand(NameStore, myConn);
                command.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < list_NameParam.Count(); i++)
                {
                    if (list_Param[i] != "-")
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], list_Param[i]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue(list_NameParam[i], DBNull.Value);
                    }
                }

                //command.Parameters.AddWithValue("@PASS", Pass);
                command.CommandTimeout = 0;
                dtAdapter.SelectCommand = command;

                dtAdapter.Fill(ds);

               // ds.Tables["aaa"].Rows[1]["ssss"].ToString();

                return ds;

            }
            catch (Exception e)
            {
                return ds;
            }
            finally
            {
                myConn.Close();
                status = string.Empty;
                dtAdapter.Dispose();
                command.Dispose();
                ds.Dispose();
            }

        }
    }
}