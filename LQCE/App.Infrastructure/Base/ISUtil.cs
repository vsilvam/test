using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.Base
{
    public sealed class ISUtil
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetProcessWorkingSetSize(IntPtr procHandle, Int32 min, Int32 max);

        public static void ClearMemory()
        {
            try
            {
                Process Mem;
                Mem = Process.GetCurrentProcess();
                SetProcessWorkingSetSize(Mem.Handle, -1, -1);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
            }
        }

        /// <summary>
        /// Crea un password aleatorio
        /// </summary>
        /// <param name="PasswordLength">largo del password</param>
        /// <returns>nuevo password aleatorio</returns>
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ0123456789";

            var randomBytes = new Byte[PasswordLength];
            var chars = new char[PasswordLength];

            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                var randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = _allowedChars[randomBytes[i]%allowedCharCount];
            }

            return new string(chars);
        }

        #region valida rut

        /// <summary>
        /// Método que realiza la validación de rut nacional
        /// </summary>
        /// <param name="rut">rut ingresado</param>
        /// <returns>true = rut correcto, false = rut incorrecto</returns>
        public static bool ValidaRut(string rut)
        {
            try
            {
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                if (rut == "1-9")
                {
                    return false;
                }
                rut = rut.Trim();
                if (string.IsNullOrEmpty(rut))
                {
                    return false;
                }
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                string digit = rut.Substring(rut.Length - 1, 1);
                digit = digit.ToUpper();
                string digitComparer = "";
                rut = rut.Substring(0, rut.Length - 1);
                int wiMultiplicador = 9;
                int wiSumatoria = 0;
                int wiSubTotal = 0;
                int wiLargo = rut.Length;
                for (int i = wiLargo; i > 0; i--)
                {
                    wiSubTotal = Convert.ToInt32(rut.Substring(i - 1, 1));
                    wiSumatoria = wiSumatoria + (wiSubTotal*wiMultiplicador);
                    if (wiMultiplicador == 4)
                    {
                        wiMultiplicador = 10;
                    }
                    wiMultiplicador = wiMultiplicador - 1;
                }
                wiSumatoria = wiSumatoria%11;
                if (wiSumatoria == 10)
                {
                    digitComparer = "K";
                }
                else
                {
                    digitComparer = wiSumatoria.ToString();
                }

                if (digit.Equals(digitComparer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Da formato a un RUT válido (XX.XXX.XXX-X)
        /// </summary>
        /// <param name="rut">rut sin formato</param>
        /// <returns>rut con formato XX.XXX.XXX-X</returns>
        public static string FormatearRut(string rut)
        {
            string rutInvertido = InvertirString(rut);
            string buffer = "";
            int j = 0;
            foreach (char c in rutInvertido)
            {
                if (j == 1)
                    buffer += "-";
                if (j == 4 || j == 7)
                    buffer += ".";
                buffer += c.ToString();
                j++;
            }
            rut = InvertirString(buffer);
            return rut;
        }

        /// <summary>
        /// Utilizado por el método Formatear rut.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string InvertirString(string str)
        {
            string buffer = "";
            foreach (char c in str)
                buffer = c + buffer;
            return buffer;
        }

        /// <summary>
        /// Da formato a un RUT válido (XX.XXX.XXX) sin digito verificador
        /// </summary>
        /// <param name="rut">rut sin formato</param>
        /// <returns>rut con formato XX.XXX.XXX-X</returns>
        public static string FormatearRutSinDv(string rut)
        {
            string rutInvertido = InvertirString(rut);
            string buffer = "";
            int j = 0;
            foreach (char c in rutInvertido)
            {
                if (j == 3 || j == 6)
                    buffer += ".";
                buffer += c.ToString();
                j++;
            }
            rut = InvertirString(buffer);
            return rut;
        }

        #endregion
    }
}