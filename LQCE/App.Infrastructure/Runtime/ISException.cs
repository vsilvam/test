using System;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using App.Infrastructure.Base;
using App.Infrastructure.Services;
using Microsoft.VisualBasic;

namespace App.Infrastructure.Runtime
{
    public sealed class ISException
    {
        private const char SEPARATOR_CHAR = '_';

        public static string NullReferenceMessage
        {
            get { return "Object null reference"; }
        }

        public static void RegisterExcepcion(string strExceptionMessage)
        {
            var ex = new Exception(strExceptionMessage);
            RegisterExcepcion(ex);
        }

        public static void RegisterExcepcion(Exception objException)
        {
            bool bolCreateEventLog =
                ISConvert.ToBoolean(ISConfiguration.GetConfigurationParameter("Exception", "_appErro_CreateEventLog"));
            bool bolCreateFileLog =
                ISConvert.ToBoolean(ISConfiguration.GetConfigurationParameter("Exception", "_appErro_CreateFileLog"));
            bool bolSendMailOnError =
                ISConvert.ToBoolean(ISConfiguration.GetConfigurationParameter("Exception", "_appErro_SendMailOnError"));

            if (bolCreateEventLog || bolCreateFileLog)
            {
                var objStackTrace = new StackTrace();

                string strClassName;
                string strMethodName;

                var sbLogMessage = new StringBuilder("");
                strClassName = objStackTrace.GetFrame(1).GetMethod().DeclaringType.Name;
                strMethodName = objStackTrace.GetFrame(1).GetMethod().Name;

                sbLogMessage.Append("Date: ").Append(DateTime.Now.ToString("dd-MM-yyyy")).Append('\t');
                sbLogMessage.Append("Time: ").Append(DateTime.Now.ToString("HH:mm:ss.fff")).Append("\r\n");
                sbLogMessage.Append("Object Class: ").Append(strClassName).Append('\t');
                sbLogMessage.Append("Object Method: ").Append(strMethodName).Append("\r\n");
                sbLogMessage.Append("Object Exception: ").Append(objException.Message).Append("\r\n");

                if (objException.GetType().FullName.Equals("System.Data.SqlClient.SqlException"))
                {
                    var objSqlException = (SqlException)objException;
                    sbLogMessage.Append("DB Procedure: ").Append(objSqlException.Procedure).Append("\r\n");
                    sbLogMessage.Append("DB Line: ").Append(objSqlException.LineNumber).Append("\r\n");
                    sbLogMessage.Append("DB Message: ").Append(objSqlException.Message).Append("\r\n");
                }
                else if (objException.GetType().FullName.Equals("System.Data.OracleClient.OracleException"))
                {
                    var objOracleException = (OracleException)objException;
                    sbLogMessage.Append("DB Procedure: ").Append(objOracleException.Source).Append("\r\n");
                    sbLogMessage.Append("DB Line: ").Append(objOracleException.TargetSite).Append("\r\n");
                    sbLogMessage.Append("DB Message: ").Append(objOracleException.Message).Append("\r\n");
                }

                if (objException.StackTrace != null)
                {
                    // STACK TRACE:
                    sbLogMessage.Append("StackTrace: ").Append("\r\n");
                    sbLogMessage.Append(objException.StackTrace).Append("\r\n");
                }

                if (objException.InnerException != null && objException.InnerException.Message != null)
                {
                    sbLogMessage.Append(objException.InnerException.Message).Append("\r\n");
                    if (objException.InnerException.StackTrace != null)
                    {
                        // STACK TRACE INNER EXCEPTION:
                        sbLogMessage.Append("StackTrace InnerException: ").Append("\r\n");
                        sbLogMessage.Append(objException.InnerException.StackTrace).Append("\r\n");
                    }
                }


                if (bolCreateFileLog)
                {
                    if (!writeFileLog(sbLogMessage.ToString()))
                    {
                        bolCreateEventLog = true;
                    }
                }

                if (bolCreateEventLog)
                {
                    if (!writeEventLog(sbLogMessage.ToString(), EventLogEntryType.Error))
                    {
                        bolSendMailOnError = true;
                    }
                }

                if (bolSendMailOnError)
                {
                    string strAdminEmail =
                        ISConvert.ToString(ISConfiguration.GetConfigurationParameter("Exception", "_appErro_AdminEmail"));
                    string strSubject =
                        ISConvert.ToString(ISConfiguration.GetConfigurationParameter("Exception", "_appErro_Subject"));

                    string strError;
                    var objISEmail = new ISEmail();
                    objISEmail.SendMailAsync(strAdminEmail, strSubject, sbLogMessage.ToString(), false, out strError);
                }
            }
            //throw objException;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static bool writeFileLog(string strMessage)
        {
            string strPathFileLog = ISConfiguration.GetConfigurationParameter("Exception", "_appErro_PathFileLog");
            if (!strPathFileLog.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPathFileLog += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPathFileLog);

            try
            {
                if ((targetFolder.Exists == false))
                {
                    Directory.CreateDirectory(strPathFileLog);
                }
                string strFileName = (strPathFileLog + (Strings.Format(DateTime.Now, "yyyy-MM-dd") + ".log"));

                var objStreamWriter = new StreamWriter(strFileName, true);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.WriteLine("");
                objStreamWriter.WriteLine(new string(SEPARATOR_CHAR, 200));
                objStreamWriter.WriteLine("");
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objStreamWriter = null;

                return true;
            }
            catch
            {
            }
            return false;
        }

        public static bool readFileLog(out string strMessage)
        {
            strMessage = string.Empty;

            string strPathFileLog = ISConfiguration.GetConfigurationParameter("Exception", "_appErro_PathFileLog");
            if (!strPathFileLog.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                strPathFileLog += Path.DirectorySeparatorChar;
            }
            var targetFolder = new DirectoryInfo(strPathFileLog);
            if ((targetFolder.Exists == false))
            {
                return false;
            }
            string strFileName = (strPathFileLog + (Strings.Format(DateTime.Now, "yyyy-MM-dd") + ".log"));

            try
            {
                var objStreamReader = new StreamReader(strFileName, true);

                strMessage = objStreamReader.ReadToEnd();

                objStreamReader.Close();
                objStreamReader = null;
                return true;
            }
            catch
            {
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static bool writeEventLog(string strMessage, EventLogEntryType eventLogEntryType)
        {
            string log = ISConfiguration.GetConfigurationParameter("Exception", "_appErro_EventLogName");
            //string strComputerName = ISConfiguration.GetConfigurationParameter("Exception", "_appErro_ComputerName");
            string source = ISConfiguration.GetConfigurationParameter("Exception", "_appErro_EntryName");

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }

            try
            {
                using (var eventLog = new EventLog(log) { Source = source })
                {
                    eventLog.WriteEntry(strMessage, eventLogEntryType, 0);
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static void WriteInformationLog(string strMessage)
        {
            bool bolCreateEventLog =
                ISConvert.ToBoolean(ISConfiguration.GetConfigurationParameter("Exception", "_appErro_CreateEventLog"));
            bool bolCreateFileLog =
                ISConvert.ToBoolean(ISConfiguration.GetConfigurationParameter("Exception", "_appErro_CreateFileLog"));

            if (bolCreateEventLog || bolCreateFileLog)
            {
                var sbLogMessage = new StringBuilder("");

                sbLogMessage.Append("Date: ").Append(DateTime.Now.ToString("dd-MM-yyyy")).Append('\t');
                sbLogMessage.Append("Time: ").Append(DateTime.Now.ToString("HH:mm:ss.fff")).Append("\r\n");

                sbLogMessage.Append(strMessage).Append("\r\n");

                if (bolCreateFileLog)
                {
                    if (!writeFileLog(sbLogMessage.ToString()))
                    {
                        bolCreateEventLog = true;
                    }
                }

                if (bolCreateEventLog)
                {
                    writeEventLog(sbLogMessage.ToString(), EventLogEntryType.Information);
                }
            }
        }
    }
}