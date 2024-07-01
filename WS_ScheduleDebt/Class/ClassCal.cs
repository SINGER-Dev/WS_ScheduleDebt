using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WS_ScheduleDebt.Model
{
    public class ClassCal
    {
        public double CutComma(string num)
        {
            double value = 0.0;

            try
            {
                string amt = num;
                string[] arrAmt = amt.Split(',');
                amt = string.Empty;

                for (int i = 0; i < arrAmt.Count(); i++)
                {
                    amt += arrAmt[i];
                }
                value = Convert.ToDouble(amt);
            }
            catch (Exception i)
            {

            }
            finally
            { 
                
            }
            return value;
        }

        public string addComma(string num)
        {
            double amt = 0.0;
            string data = string.Empty;
            amt = Convert.ToDouble(num);
            data = amt.ToString("#,##0.00");
            return data;
        }

        public string ROUNDUPTEN(string num)
        {
            string a = string.Empty;
            double b = 0.0;
            MidpointRounding m = new MidpointRounding();
            b = Math.Ceiling(Convert.ToDouble(num) / 100) * 100;
            a = b.ToString();
            return a;
        }

        public string ROUNDUP(string num)
        {
            string a = string.Empty;
            double b = 0.0;
            MidpointRounding m = new MidpointRounding();
            b = Math.Ceiling(Convert.ToDouble(num) / 10) * 10;
            a = b.ToString();
            return a;
        }

        public string formatDate(string date)
        {
            DateTime dati = Convert.ToDateTime(date);
            string day = dati.Day.ToString();
            string month = dati.ToString("MM");
            string year = dati.Year.ToString();
            string hour = dati.Hour.ToString();
            string min = dati.Minute.ToString();
            string sec = dati.Second.ToString();
            string date_out = day + "/" + month + "/" + year + "  " + hour + ":" + min + ":" + sec;

            return date_out;
        }

        public string CutTagHTML(string value)
        {
            Regex pattern = new Regex(@"<[^>]+>");

            return pattern.Replace(value, "").ToString();

        }

        public string Cash_Text(string strNumber, bool IsTrillion = false)
        {

            string BahtText = "";
            string strTrillion = "";
            string[] strThaiNumber = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] strThaiPos = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };

            decimal decNumber = 0;
            decimal.TryParse(strNumber, out decNumber);

            if (decNumber == 0)
            {
                return "ศูนย์บาทถ้วน";
            }

            strNumber = decNumber.ToString("0.00");
            string strInteger = strNumber.Split('.')[0];
            string strSatang = strNumber.Split('.')[1];

            if (strInteger.Length > 13)
                throw new Exception("รองรับตัวเลขได้เพียง ล้านล้าน เท่านั้น!");

            bool _IsTrillion = strInteger.Length > 7;
            if (_IsTrillion)
            {
                strTrillion = strInteger.Substring(0, strInteger.Length - 6);
                BahtText = Cash_Text(strTrillion, _IsTrillion);
                strInteger = strInteger.Substring(strTrillion.Length);
            }

            int strLength = strInteger.Length;
            for (int i = 0; i < strInteger.Length; i++)
            {
                string number = strInteger.Substring(i, 1);
                if (number != "0")
                {
                    if (i == strLength - 1 && number == "1" && strLength != 1)
                    {
                        BahtText += "เอ็ด";
                    }
                    else if (i == strLength - 2 && number == "2" && strLength != 1)
                    {
                        BahtText += "ยี่";
                    }
                    else if (i != strLength - 2 || number != "1")
                    {
                        BahtText += strThaiNumber[int.Parse(number)];
                    }

                    BahtText += strThaiPos[(strLength - i) - 1];
                }
            }

            if (IsTrillion)
            {
                return BahtText + "ล้าน";
            }

            if (strInteger != "0")
            {
                BahtText += "บาท";
            }

            if (strSatang == "00")
            {
                BahtText += "ถ้วน";
            }
            else
            {
                strLength = strSatang.Length;
                for (int i = 0; i < strSatang.Length; i++)
                {
                    string number = strSatang.Substring(i, 1);
                    if (number != "0")
                    {
                        if (i == strLength - 1 && number == "1" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "เอ็ด";
                        }
                        else if (i == strLength - 2 && number == "2" && strSatang[0].ToString() != "0")
                        {
                            BahtText += "ยี่";
                        }
                        else if (i != strLength - 2 || number != "1")
                        {
                            BahtText += strThaiNumber[int.Parse(number)];
                        }

                        BahtText += strThaiPos[(strLength - i) - 1];
                    }
                }

                BahtText += "สตางค์";
            }

            return BahtText;


            //string bahtTxt, n, bahtTH = "";
            //double amount;
            //try { amount = Convert.ToDouble(cash); }
            //catch { amount = 0; }
            //bahtTxt = amount.ToString("####.00");
            //string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            //string[] num2 = { "", "สิบ", "ยี่", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "" };
            //string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน", "สิบล้าน" };
            //string[] temp = bahtTxt.Split('.');
            //string intVal = temp[0];
            //string decVal = temp[1];
            //if (Convert.ToDouble(bahtTxt) == 0)
            //    bahtTH = "ศูนย์บาทถ้วน";
            //else
            //{
            //    for (int i = 0; i < intVal.Length; i++)
            //    {
            //        n = intVal.Substring(i, 1);
            //        if (n != "0")
            //        {
            //            if ((i == (intVal.Length - 1)) && (n == "1"))
            //                bahtTH += "เอ็ด";
            //            else if ((i == (intVal.Length - 2)) && (n == "2"))
            //                bahtTH += "ยี่";
            //            else if ((i == (intVal.Length - 2)) && (n == "1"))
            //                bahtTH += "";
            //            else
            //                if (intVal.Count() < 8)
            //                {
            //                    bahtTH += num[Convert.ToInt32(n)];
            //                    bahtTH += rank[(intVal.Length - i) - 1];
            //                }
            //                else if (intVal.Count() == 8)
            //                {
            //                    if (i == 0)
            //                    {
            //                        bahtTH += num2[Convert.ToInt32(n)];
            //                        if (n != "1")
            //                        {
            //                            bahtTH += rank[1];
            //                        }
            //                    }
            //                    else
            //                    {
            //                        bahtTH += num[Convert.ToInt32(n)];
            //                        bahtTH += rank[(intVal.Length - i) - 1];
            //                    }
            //                }
            //        }
            //        else
            //        {
            //            if (intVal.Count() == 8)
            //            {
            //                if (i == 1)
            //                {
            //                    bahtTH += rank[6];
            //                }

            //            }
            //        }


            //    }
            //    bahtTH += "บาท";
            //    if (decVal == "00")
            //        bahtTH += "ถ้วน";
            //    else
            //    {
            //        for (int i = 0; i < decVal.Length; i++)
            //        {
            //            n = decVal.Substring(i, 1);
            //            if (n != "0")
            //            {
            //                if ((i == decVal.Length - 1) && (n == "1"))
            //                    bahtTH += "เอ็ด";
            //                else if ((i == (decVal.Length - 2)) && (n == "2"))
            //                    bahtTH += "ยี่";
            //                else if ((i == (decVal.Length - 2)) && (n == "1"))
            //                    bahtTH += "";
            //                else
            //                    bahtTH += num[Convert.ToInt32(n)];
            //                bahtTH += rank[(decVal.Length - i) - 1];
            //            }
            //        }
            //        bahtTH += "สตางค์";
            //    }
            //}
            ////return bahtTH;



            //return bahtTH;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}