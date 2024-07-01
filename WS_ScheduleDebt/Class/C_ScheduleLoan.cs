using Microsoft.Reporting.WebForms;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using WS_ScheduleDebt.Class;
using ZXing;

namespace WS_ScheduleDebt.Class
{
    public class C_ScheduleLoan
    {

        public string Call_ScheduleLoan(string APP_CODE,string ReportPath)
        {
            string path = string.Empty;
            DataSet ds = new DataSet();




            ds = GET_DATA_FOR_SCHEDULE_LOAN(APP_CODE, "1");



            // string In_Rate = CSA.QueryDataScheduleShowHPRate(ApplicationCode);
            LocalReport report = new LocalReport();
            report.ReportPath = ReportPath + @"RDLC\ScheduleLoan.rdlc";
            ReportDataSource rds = new ReportDataSource();
            report.EnableExternalImages = true;
            string pathQr = ReportPath + @"Report\QR_IN_Schedule\";
           // string pathBarCode = report_name_path + @"BARCODE\";
            string taxId = ConfigurationSettings.AppSettings["TaxID"];
            string Suffix = ConfigurationSettings.AppSettings["Suffix_ScheduleLoan"];
            string Ref2 = ConfigurationSettings.AppSettings["Ref2"];

            pathQr += taxId + Suffix + ds.Tables[0].Rows[0]["AccountNo"].ToString() + Ref2 + "0.png";
          
            string QRCode = "|" + taxId + Suffix + "\r" + ds.Tables[0].Rows[0]["AccountNo"].ToString() + "\r" + Ref2 + "\r" + "0";



            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRCode, QRCodeGenerator.ECCLevel.H);
            QRCode qrCode = new QRCode(qrCodeData);
            System.IO.File.Delete(pathQr);

            using (Bitmap bitMap = new Bitmap(qrCode.GetGraphic(20), new Size(600, 470)))
            {
                bitMap.Save(pathQr);
            }

            BarcodeReader barcodeReader = new BarcodeReader { AutoRotate = true };
            string QRCode_Read = string.Empty;
            string Barcode_Read = string.Empty;
            Bitmap bitmap1 = new Bitmap(pathQr);
            var result = barcodeReader.Decode(bitmap1);
            if (result != null)
            {
                QRCode_Read = result.Text;
            }
           // Bitmap bitmap2 = new Bitmap(pathBarCode);
            //var result2 = barcodeReader.Decode(bitmap2);
            //if (result2 != null)
            //{
            //    Barcode_Read = result2.Text;
            //}

            //ReportParameter[] a = new ReportParameter[3];
            //a[0] = new ReportParameter("ReportParameter1", AccountNo, false);
            //a[1] = new ReportParameter("NameCus", CustomerName, false);
            //a[2] = new ReportParameter("NameReport", "ScheduleHP.rdl", false); 
            string AccountNo = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            string CUS_NAME = ds.Tables[0].Rows[0]["CustomerName"].ToString();


            ReportParameter[] a = new ReportParameter[4];
            a[0] = new ReportParameter("ReportParameter1", AccountNo, false);
            a[1] = new ReportParameter("NameCus", CUS_NAME, false);
            a[2] = new ReportParameter("NameReport", "ScheduleLoan.rdlc", false);
            if (QRCode_Read == QRCode)
            {
                a[3] = new ReportParameter("PathQRCode", "File://" + pathQr, false);
            }
            else
            {
                a[3] = new ReportParameter("PathQRCode", "-", false);
            }
            //   a[3] = new ReportParameter("Status_Report", "ข้อมูลจาก C100", false);
            // a[3] = new ReportParameter("In_Rate", In_Rate, false); 

            report.SetParameters(a);


            rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file

            rds.Value = ds.Tables[1];
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF");
            //Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            using (FileStream fs = File.Create(ReportPath + @"Report\ScheduleLoan_" + APP_CODE + ".pdf"))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }

            path = ReportPath + @"Report\ScheduleLoan_" + APP_CODE + ".pdf";


            return path;
        }


        public DataSet GET_DATA_FOR_SCHEDULE_LOAN(string APP_CODE,string Status_DDL)
        {
            DataTable dtRecord = new DataTable();
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            C_CON CON = new C_CON();
            List<string> list_NameParam = new List<string>();
            List<string> list_Param = new List<string>();

            list_NameParam.Add("AppCode");

            list_Param.Add(APP_CODE);



            dtRecord = CON.CALL_STORE("ConnSingerSQLServer", "QueryDataScheduleHP_AUTOSALE", list_NameParam, list_Param);
            ds.Tables.Add(dtRecord);

            List<decimal> list_installment = Match_PRICE_LIST_STEP(APP_CODE);

            if (dtRecord.Rows.Count != 0)
            {
                decimal a = Convert.ToDecimal(dtRecord.Rows[0]["Cash"]);
                a = a * Convert.ToDecimal(0.07);

                decimal Cash = Convert.ToDecimal(dtRecord.Rows[0]["Cash"]) + a;

                List<SG_ECC_MASTER_DLL.SGECCMASTER.ECC_MASTER> ECC = new List<SG_ECC_MASTER_DLL.SGECCMASTER.ECC_MASTER>();

                if (Status_DDL == "1")
                {
                    if (list_installment.Count > 0)
                    {
                        ECC = SG_ECC_MASTER_DLL.SGECCMASTER.GETECCMASTERTOP(
                            Convert.ToDecimal(dtRecord.Rows[0]["Cash"])
                            , Convert.ToDecimal(dtRecord.Rows[0]["HirePurchasePrice"])
                            , Convert.ToDecimal(dtRecord.Rows[0]["DownPayment"])
                            , list_installment
                            , Convert.ToInt32(dtRecord.Rows[0]["InstallmentPeriod"])
                            , Convert.ToDateTime(dtRecord.Rows[0]["ARM_SALES_DATE"])
                            , Convert.ToDateTime(dtRecord.Rows[0]["FirstDueDate"])
                            , false, false, false);
                    }
                    else
                    {
                        ECC = SG_ECC_MASTER_DLL.SGECCMASTER.GETECCMASTERTOP(
                              Convert.ToDecimal(dtRecord.Rows[0]["Cash"])
                              , Convert.ToDecimal(dtRecord.Rows[0]["HirePurchasePrice"])
                              , Convert.ToDecimal(dtRecord.Rows[0]["DownPayment"])
                              , Convert.ToDecimal(dtRecord.Rows[0]["MonthlyPayment"])
                              , Convert.ToInt32(dtRecord.Rows[0]["InstallmentPeriod"])
                              , Convert.ToDateTime(dtRecord.Rows[0]["ARM_SALES_DATE"])
                              , Convert.ToDateTime(dtRecord.Rows[0]["FirstDueDate"])
                              , false, false, false);
                    }
                }
                else if (Status_DDL == "2")
                {
                    if (list_installment.Count > 0)
                    {
                        ECC = SG_ECC_MASTER_DLL.SGECCMASTER.GETECCMASTERBUTTOM(
                          Convert.ToDecimal(dtRecord.Rows[0]["Cash"])
                          , Convert.ToDecimal(dtRecord.Rows[0]["HirePurchasePrice"])
                          , Convert.ToDecimal(dtRecord.Rows[0]["DownPayment"])
                          , list_installment
                          , Convert.ToInt32(dtRecord.Rows[0]["InstallmentPeriod"])
                          , Convert.ToDateTime(dtRecord.Rows[0]["ARM_SALES_DATE"])
                          , Convert.ToDateTime(dtRecord.Rows[0]["FirstDueDate"])
                          , false, false, false);
                    }
                    else
                    {
                        ECC = SG_ECC_MASTER_DLL.SGECCMASTER.GETECCMASTERBUTTOM(
                        Convert.ToDecimal(dtRecord.Rows[0]["Cash"])
                        , Convert.ToDecimal(dtRecord.Rows[0]["HirePurchasePrice"])
                        , Convert.ToDecimal(dtRecord.Rows[0]["DownPayment"])
                        , Convert.ToDecimal(dtRecord.Rows[0]["MonthlyPayment"])
                        , Convert.ToInt32(dtRecord.Rows[0]["InstallmentPeriod"])
                        , Convert.ToDateTime(dtRecord.Rows[0]["ARM_SALES_DATE"])
                        , Convert.ToDateTime(dtRecord.Rows[0]["FirstDueDate"])
                        , false, false, false);
                    }
                }



                table.Columns.Add("Period", typeof(string));
                table.Columns.Add("PaidDate", typeof(string));
                table.Columns.Add("Principle", typeof(double));
                table.Columns.Add("Interest", typeof(double));
                table.Columns.Add("Sum", typeof(double));
                table.Columns.Add("Balance", typeof(double));



                for (int i = 0; i < ECC.Count; i++)
                {
                    DateTime dat = DateTime.Now;
                    dat = Convert.ToDateTime(ECC[i].MONTHDATE);
                    string sum = Convert.ToDouble(ECC[i].Instamemt_AMT).ToString("#,###");
                    table.Rows.Add(i + 1, dat.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), Convert.ToDouble(ECC[i].CUR_PUR_PRINCIPLE), Convert.ToDouble(ECC[i].CUR_PUR_INTEREST), Convert.ToDouble(ECC[i].Instamemt_AMT), Convert.ToDouble(ECC[i].PUR_PRINCIPLE_BAL));
                }


            }
            ds.Tables.Add(table);


            return ds;
        }


        public List<decimal> Match_PRICE_LIST_STEP(string ApplicationCode)
        {

            DataTable dtRecord = new DataTable();
            dtRecord =  Query_PRICE_LIST_STEP(ApplicationCode);
            List<decimal> installment = new List<decimal>();
            if (dtRecord.Rows.Count > 0)
            {
                if (dtRecord.Rows[0]["PERIOD_TOTAL"].ToString() != "")
                {
                    for (int i = 0; i < dtRecord.Rows.Count; i++)
                    {
                        for (int j = 0; j < Convert.ToInt32(dtRecord.Rows[i]["PERIOD_TOTAL"]); j++)
                        {
                            installment.Add(Convert.ToDecimal(dtRecord.Rows[i]["INSTALLMENT_PER_PERIOD"]));
                        }
                    }
                }
            }


            return installment;
        }

        public DataTable Query_PRICE_LIST_STEP(string APP_CODE)
        {
            DataTable dt = new DataTable();

            C_CON CON = new C_CON();
            List<string> list_NameParam = new List<string>();
            List<string> list_Param = new List<string>();

            list_NameParam.Add("APP_CODE");

            list_Param.Add(APP_CODE);


            dt = CON.CALL_STORE("ConnSingerSQLServer", "GET_BALLON_PERIOD", list_NameParam, list_Param);
            return dt;
        }


    }
}