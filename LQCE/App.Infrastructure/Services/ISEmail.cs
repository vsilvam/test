using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using App.Infrastructure.Base;
using App.Infrastructure.Runtime;
using System.IO;

namespace App.Infrastructure.Services
{
    public sealed class ISEmail : MarshalByRefObject
    {
        #region Delegates

        public delegate void DelegateSendMailCompleted(bool sucess, object userState, Exception ex);

        #endregion

        public event DelegateSendMailCompleted SendMailCompleted = null;


        public bool SendMailAsync(string strTo, string strSubject, string strBody, bool blnIsBodyHtml,
                                  out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            var strListCc = new string[0];

            //LLAMADA A SOBRECARGA 5
            return SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, true, new object(),
                            out _strResultMessage);
        }

        public bool SendMailAsync(string strTo, string strSubject, string strBody, bool blnIsBodyHtml, object objState,
                                  out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            var strListCc = new string[0];

            //LLAMADA A SOBRECARGA 5
            return SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, true, objState,
                            out _strResultMessage);
        }

        public bool SendMailAsync(string[] strListTo, string strSubject, string strBody, bool blnIsBodyHtml,
                                  object objState, out string _strResultMessage)
        {
            var strListCc = new string[0];

            //LLAMADA A SOBRECARGA 5
            return SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, true, objState,
                            out _strResultMessage);
        }

        public bool SendMailAsync(string strTo, string strCc, string strSubject, string strBody, bool blnIsBodyHtml,
                                  object objState, out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            var strListCc = new string[1];
            strListCc[0] = strCc;

            //LLAMADA A SOBRECARGA 5
            return SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, true, objState,
                            out _strResultMessage);
        }

        public bool SendMailAsync(string strTo, string[] strListCc, string strSubject, string strBody,
                                  bool blnIsBodyHtml, object objState, out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            //LLAMADA A SOBRECARGA 5
            return SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, true, objState,
                            out _strResultMessage);
        }


        public static bool SendMail(string strTo, string strSubject, string strBody, out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            var strListCc = new string[0];

            //LLAMADA A SOBRECARGA 5 
            var objISEmail = new ISEmail();
            return objISEmail.SendMail(strListTo, strListCc, strSubject, strBody, true, false, null,
                                       out _strResultMessage);
        }

        public static bool SendMail(string strTo, string strSubject, string strBody, bool blnIsBodyHtml,
                                    out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            var strListCc = new string[0];

            //LLAMADA A SOBRECARGA 5 
            var objISEmail = new ISEmail();
            return objISEmail.SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, false, null,
                                       out _strResultMessage);
        }

        public static bool SendMail(string[] strListTo, string strSubject, string strBody, bool blnIsBodyHtml,
                                    out string _strResultMessage)
        {
            var strListCc = new string[0];

            //LLAMADA A SOBRECARGA 5
            var objISEmail = new ISEmail();
            return objISEmail.SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, false, null,
                                       out _strResultMessage);
        }

        public static bool SendMail(string strTo, string strCc, string strSubject, string strBody, bool blnIsBodyHtml,
                                    out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            var strListCc = new string[1];
            strListCc[0] = strCc;

            //LLAMADA A SOBRECARGA 5
            var objISEmail = new ISEmail();
            return objISEmail.SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, false, null,
                                       out _strResultMessage);
        }

        public static bool SendMail(string strTo, string[] strListCc, string strSubject, string strBody,
                                    bool blnIsBodyHtml, out string _strResultMessage)
        {
            var strListTo = new string[1];
            strListTo[0] = strTo;

            //LLAMADA A SOBRECARGA 5
            var objISEmail = new ISEmail();
            return objISEmail.SendMail(strListTo, strListCc, strSubject, strBody, blnIsBodyHtml, false, null,
                                       out _strResultMessage);
        }


        public bool SendMail(string[] strListTo, string[] strListCc, string strSubject, string strBody,
                             bool blnIsBodyHtml, bool blnAsyncMethod, object objState, out string _strResultMessage)
        {
            if (string.IsNullOrEmpty(ISConfiguration.GetConfigurationParameter("Email", "_appMail_FromAddress")))
            {
                _strResultMessage = "Invalid configuration value: FromAddress";
                ISException.RegisterExcepcion(_strResultMessage);
                return false;
            }

            try
            {
                var objMailMessage = new MailMessage();

                objMailMessage.From =
                    new MailAddress(ISConfiguration.GetConfigurationParameter("Email", "_appMail_FromAddress"));

                for (int index = 0; index < strListTo.Length; index++)
                {
                    objMailMessage.To.Add(strListTo[index]);
                }

                for (int index = 0; index < strListCc.Length; index++)
                {
                    objMailMessage.CC.Add(strListCc[index]);
                }

                objMailMessage.Subject = strSubject;
                objMailMessage.Body = strBody;
                objMailMessage.IsBodyHtml = blnIsBodyHtml;

                var objSmtpClient = new SmtpClient();
                objSmtpClient.Host = ISConfiguration.GetConfigurationParameter("Email", "_appMail_SmtpServer");
                objSmtpClient.Port =
                    ISConvert.ToInteger(ISConfiguration.GetConfigurationParameter("Email", "_appMail_SmtpPort"));
                ;
                objSmtpClient.Credentials =
                    new NetworkCredential(ISConfiguration.GetConfigurationParameter("Email", "_appMail_User"),
                                          ISConfiguration.GetConfigurationParameter("Email", "_appMail_Password"),
                                          ISConfiguration.GetConfigurationParameter("Email", "_appMail_Domain"));
                objSmtpClient.EnableSsl =
                    ISConvert.ToBoolean(ISConfiguration.GetConfigurationParameter("Email",
                                                                                  "_appMail_AuthenticationRequired"));

                if (blnAsyncMethod)
                {
                    objSmtpClient.SendCompleted += objSmtpClient_SendCompleted;
                    objSmtpClient.SendAsync(objMailMessage, objState);

                    _strResultMessage = string.Empty;
                    return true;
                }

                objSmtpClient.Send(objMailMessage);
                _strResultMessage = string.Empty;
                return true;
            }
            catch (FormatException ex)
            {
                _strResultMessage = ex.Message;
                ISException.RegisterExcepcion(ex);
                return false;
            }
            catch (SmtpException ex)
            {
                _strResultMessage = ex.Message;
                ISException.RegisterExcepcion(ex);
                return false;
            }
            catch (Exception ex)
            {
                _strResultMessage = ex.Message;
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        /// <summary>
        /// Envio de email con un adjunto.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtmlBody"></param>
        /// <param name="attachment"></param>
        /// <param name="resultMessage"></param>
        /// <returns></returns>
        public static bool SendMailWithAttachment(string to, string subject,
            string body,
            bool isHtmlBody,
            Adjunto attachment,
            out string resultMessage)
        {
            return SendMailWithAttachment(to, subject, body, isHtmlBody, new List<Adjunto> { attachment }, out resultMessage);
        }

        /// <summary>
        /// Envio de email con varios adjuntos.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtmlBody"></param>
        /// <param name="attachments"></param>
        /// <param name="resultMessage"></param>
        /// <returns></returns>
        public static bool SendMailWithAttachment(string to, string subject,
            string body,
            bool isHtmlBody,
            IList<Adjunto> attachments,
            out string resultMessage)
        {
            //SE VALIDA CONFIGURACION DE CORREO
            if (string.IsNullOrEmpty(ISConfiguration.GetConfigurationParameter("Email", "_appMail_FromAddress")))
            {
                resultMessage = "Invalid configuration value: FromAddress";
                ISException.RegisterExcepcion(resultMessage);
                return false;
            }

            try
            {
                using (var mailMessage = new MailMessage
                                             {
                                                 From = new MailAddress(ISConfiguration.GetConfigurationParameter("Email", "_appMail_FromAddress")),
                                                 Subject = subject,
                                                 Body = body,
                                                 IsBodyHtml = isHtmlBody,
                                             })
                {
                    mailMessage.To.Add(to);
                    //SE AGREGAN LOS ADJUNTOS
                    if (attachments != null && attachments.Count > 0)
                    {
                        for (int i = 0; i < attachments.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(attachments[i].Contenido) && !string.IsNullOrEmpty(attachments[i].Nombre))
                            {
                                //SE CARGA EL ADJUNTO AL MENSAJE
                                byte[] filebytes = Convert.FromBase64String(attachments[i].Contenido);
                                Stream stream = new MemoryStream(filebytes);
                                mailMessage.Attachments.Add(new Attachment(stream, attachments[i].Nombre));
                            }
                        }
                    }

                    //SE CREA EL CLIENTE SMTP
                    var smtpClient = new SmtpClient
                                         {
                                             Host =
                                                 ISConfiguration.GetConfigurationParameter("Email", "_appMail_SmtpServer"),
                                             Port =
                                                 ISConvert.ToInteger(ISConfiguration.GetConfigurationParameter("Email",
                                                                                                               "_appMail_SmtpPort")),
                                             Credentials =
                                                 new NetworkCredential(
                                                 ISConfiguration.GetConfigurationParameter("Email", "_appMail_User"),
                                                 ISConfiguration.GetConfigurationParameter("Email", "_appMail_Password"),
                                                 ISConfiguration.GetConfigurationParameter("Email", "_appMail_Domain")),
                                             EnableSsl =
                                                 ISConvert.ToBoolean(ISConfiguration.GetConfigurationParameter("Email",
                                                                                                               "_appMail_AuthenticationRequired"))
                                         };

                    //SE ENVIA EL MAIL
                    //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(mailMessage);
                }
                resultMessage = string.Empty;
                return true;
            }
            catch (FormatException ex)
            {
                resultMessage = ex.Message;
                ISException.RegisterExcepcion(ex);
                return false;
            }
            catch (SmtpException ex)
            {
                resultMessage = ex.Message;
                ISException.RegisterExcepcion(ex);
                return false;
            }
            catch (Exception ex)
            {
                resultMessage = ex.Message;
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        public class Adjunto
        {
            public string Nombre { set; get; }
            public string Contenido { set; get; }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void objSmtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            bool sucess = true;

            if (e.Error != null)
            {
                sucess = false;

                ISException.RegisterExcepcion(e.Error);
            }

            if (SendMailCompleted != null)
            {
                SendMailCompleted(sucess, e.UserState, e.Error);
            }
        }
    }
}