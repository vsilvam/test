using System;
using System.Text;
using Microsoft.VisualBasic;

namespace App.Infrastructure.Base
{
    public class ISFormat
    {
        public static string ToCurrency(string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                strInput = Strings.Format(strInput, "Currency");
                return strInput.Substring(0, strInput.Length - 3);
            }

            return string.Empty;
        }

        public static string PascalToDescription(string strVar)
        {
            var strReturn = new StringBuilder();
            for (int i = 0; i < strVar.Length; i++)
            {
                if (char.IsLower(strVar[i]))
                {
                    strReturn.Append(strVar[i]);
                }
                if (char.IsUpper(strVar[i]))
                {
                    strReturn.Append(" ").Append(strVar[i]);
                }
            }
            if (strReturn.ToString().Substring(0, 1) == " ")
            {
                return strReturn.ToString().Substring(1, strReturn.Length - 1);
            }
            return strReturn.ToString();
        }

        public static string FirstLower(string strVar)
        {
            return strVar.Substring(0, 1).ToLower() + strVar.Substring(1);
        }

        public static string UpperCaseToCapital(string strVar, int index)
        {
            string[] strText = strVar.Split('_');


            var strReturn = new StringBuilder();
            for (int i = index; i < strText.Length; i++)
            {
                if (!string.IsNullOrEmpty(strText[i]))
                {
                    strReturn.Append(strText[i].Substring(0, 1).ToUpper()).Append(
                        strText[i].Substring(1, strText[i].Length - 1).ToLower()).Append(" ");
                }
            }

            if (string.IsNullOrEmpty(strReturn.ToString()))
            {
                return strText[0].Trim();
            }

            return strReturn.ToString().Trim();
        }

        public static string DeleteAccent(string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                var sbHelper = new StringBuilder(strInput);

                sbHelper.Replace("á", "a");
                sbHelper.Replace("é", "e");
                sbHelper.Replace("í", "i");
                sbHelper.Replace("ó", "o");
                sbHelper.Replace("ú", "u");

                return sbHelper.ToString();
            }

            return string.Empty;
        }

        public static string DeleteBlank(string strInput)
        {
            var sbHelper = new StringBuilder(strInput);

            while (sbHelper.ToString().IndexOf(" ") > -1)
            {
                sbHelper.Replace(" ", string.Empty);
            }

            return sbHelper.ToString();
        }

        public static string NumberToText(string number)
        {
            string res, dec = "";

            Int64 entero;

            int decimales;

            double nro;

            try
            {
                nro = Convert.ToDouble(number);
            }

            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));

            decimales = Convert.ToInt32(Math.Round((nro - entero)*100, 2));

            if (decimales > 0)
            {
                dec = " CON " + decimales + "/100";
            }

            res = ToText(Convert.ToDouble(entero)) + dec;

            return res;
        }

        private static string ToText(double value)
        {
            string Num2Text = "";

            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + ToText(value - 10);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + ToText(value - 20);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = ToText(Math.Truncate(value/10)*10) + " Y " + ToText(value%10);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + ToText(value - 100);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800))
                Num2Text = ToText(Math.Truncate(value/100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = ToText(Math.Truncate(value/100)*100) + " " + ToText(value%100);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + ToText(value%1000);

            else if (value < 1000000)
            {
                Num2Text = ToText(Math.Truncate(value/1000)) + " MIL";

                if ((value%1000) > 0) Num2Text = Num2Text + " " + ToText(value%1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + ToText(value%1000000);

            else if (value < 1000000000000)
            {
                Num2Text = ToText(Math.Truncate(value/1000000)) + " MILLONES ";

                if ((value - Math.Truncate(value/1000000)*1000000) > 0)
                    Num2Text = Num2Text + " " + ToText(value - Math.Truncate(value/1000000)*1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000)
                Num2Text = "UN BILLON " + ToText(value - Math.Truncate(value/1000000000000)*1000000000000);

            else
            {
                Num2Text = ToText(Math.Truncate(value/1000000000000)) + " BILLONES";

                if ((value - Math.Truncate(value/1000000000000)*1000000000000) > 0)
                    Num2Text = Num2Text + " " + ToText(value - Math.Truncate(value/1000000000000)*1000000000000);
            }

            return Num2Text;
        }
    }
}