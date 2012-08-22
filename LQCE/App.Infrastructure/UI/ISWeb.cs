using System;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using App.Infrastructure.Base;

namespace App.Infrastructure.UI
{
    public class ISWeb
    {
        public static string GetQueryStringParamDecrypted(HttpRequest _request, string strKey)
        {
            if (_request.QueryString.Get("qs") != null)
            {
                string strQueryStringCrypted = _request.QueryString.Get("qs");
                string strQueryStringDecrypted = String.Empty;
                string strResultMessage = String.Empty;
                var objISCrypto = new ISCrypto();
                if (objISCrypto.decrypt(strQueryStringCrypted.Replace(" ", "+"), ref strQueryStringDecrypted,
                                        ref strResultMessage))
                {
                    strQueryStringDecrypted = strQueryStringDecrypted.ToLower();
                    strKey = strKey.ToLower();
                    string[] strKeys = strQueryStringDecrypted.Split('&');
                    foreach (string strTemp in strKeys)
                    {
                        if ((strTemp.IndexOf(strKey) > -1))
                        {
                            return strTemp.Replace((strKey + "="), "").Trim();
                        }
                    }
                }
            }
            return String.Empty;
        }

        public static string GetQueryStringParamDecrypted(string strQueryString, string strKey)
        {
            if (strQueryString.IndexOf("?qs=") > -1)
            {
                string strQueryStringCrypted = strQueryString.Remove(0, 4);
                string strQueryStringDecrypted = String.Empty;
                string strResultMessage = String.Empty;
                var objISCrypto = new ISCrypto();
                if (objISCrypto.decrypt(strQueryStringCrypted.Replace(" ", "+"), ref strQueryStringDecrypted,
                                        ref strResultMessage))
                {
                    strQueryStringDecrypted = strQueryStringDecrypted.ToLower();
                    strKey = strKey.ToLower();
                    string[] strKeys = strQueryStringDecrypted.Split('&');
                    foreach (string strTemp in strKeys)
                    {
                        if ((strTemp.IndexOf(strKey) > -1))
                        {
                            return strTemp.Replace((strKey + "="), "").Trim();
                        }
                    }
                }
            }
            return String.Empty;
        }

        public static string SetQueryStringCrypted(string strQueryStringDecrypted)
        {
            if (!string.IsNullOrEmpty(strQueryStringDecrypted))
            {
                string strResultMessage = String.Empty;
                string strQueryStringCrypted = String.Empty;
                var objISCrypto = new ISCrypto();
                if (objISCrypto.crypt(strQueryStringDecrypted, ref strQueryStringCrypted))
                {
                    strQueryStringCrypted = ("qs=" + strQueryStringCrypted);
                    return strQueryStringCrypted;
                }
            }
            return String.Empty;
        }

        public static bool GetCacheData(out object Value, string Key)
        {
            Value = null;
            if (HttpRuntime.Cache[Key] == null)
            {
                return false;
            }

            Value = HttpRuntime.Cache[Key];
            return true;
        }

        public static void SetCacheData(object Value, string Key)
        {
            if (HttpRuntime.Cache[Key] == null)
            {
                HttpRuntime.Cache.Insert(Key, Value);
                return;
            }

            HttpRuntime.Cache[Key] = Value;
        }

        public static void SetCacheData(object Value, string Key, int Timeout)
        {
            if (HttpRuntime.Cache[Key] == null)
            {
                HttpRuntime.Cache.Insert(Key, Value, null, DateTime.Now.AddMinutes(Timeout), TimeSpan.Zero,
                                         CacheItemPriority.Default, null);
                return;
            }

            HttpRuntime.Cache[Key] = Value;
        }

        public static bool LoadDropDownList(DropDownList _dropDownList, object objData, string dataTextField,
                                            string dataValueField)
        {
            if (objData != null)
            {
                _dropDownList.DataSource = null;
                _dropDownList.Items.Clear();

                _dropDownList.DataSource = objData;
                _dropDownList.DataTextField = dataTextField;
                _dropDownList.DataValueField = dataValueField;

                try
                {
                    _dropDownList.DataBind();
                }
                catch
                {
                    return false;
                }


                return true;
            }

            return false;
        }

        public static bool LoadDropDownList(DropDownList _dropDownList, object objData, string dataTextField,
                                            string dataValueField, string defaultText)
        {
            _dropDownList.DataSource = null;
            _dropDownList.Items.Clear();
            if (objData != null)
            {
                _dropDownList.DataSource = objData;
                _dropDownList.DataTextField = dataTextField;
                _dropDownList.DataValueField = dataValueField;

                try
                {
                    _dropDownList.DataBind();
                }
                catch
                {
                    return false;
                }

                _dropDownList.Items.Insert(0, new ListItem(defaultText, "-1"));

                return true;
            }

            _dropDownList.Items.Insert(0, new ListItem(defaultText, "-1"));
            return false;
        }

        public static bool LoadListBox(ListBox _listBox, object objData, string dataTextField, string dataValueField)
        {
            if (objData != null)
            {
                _listBox.DataSource = null;
                _listBox.Items.Clear();

                _listBox.DataSource = objData;
                _listBox.DataTextField = dataTextField;
                _listBox.DataValueField = dataValueField;

                try
                {
                    _listBox.DataBind();
                }
                catch
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public static int LoadGridView(GridView _gridView, object objData)
        {
            if (objData != null)
            {
                _gridView.DataSource = null;
                _gridView.DataSource = objData;
                try
                {
                    _gridView.DataBind();
                }
                catch
                {
                    return 0;
                }

                return _gridView.Rows.Count;
            }

            return 0;
        }

        public static void ShowMessage(Page _page, string strMessage)
        {
            _page.Response.Write("<script language='javascript'>");
            _page.Response.Write("alert('" + strMessage + "');");
            _page.Response.Write("</script>");
        }

        public static void SetDropDownListSelectedValue(DropDownList _dropDownList, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _dropDownList.SelectedIndex = 0;
                return;
            }

            try
            {
                _dropDownList.SelectedValue = value;
            }
            catch (ArgumentOutOfRangeException)
            {
                _dropDownList.SelectedIndex = 0;
            }
        }

        public static ListItem GetDropDownListItem(DropDownList dropDownList, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new ListItem("N/A", "-1");
            }

            ListItem item = dropDownList.Items.FindByValue(value);
            if (item != null) return item;

            return new ListItem("N/A", "-1");
        }

        public static bool LoadCheckBoxList(CheckBoxList _CheckBoxList, object objData, string dataTextField,
                                            string dataValueField)
        {
            if (objData != null)
            {
                _CheckBoxList.DataSource = null;
                _CheckBoxList.Items.Clear();

                _CheckBoxList.DataSource = objData;
                _CheckBoxList.DataTextField = dataTextField;
                _CheckBoxList.DataValueField = dataValueField;

                try
                {
                    _CheckBoxList.DataBind();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static ListItem GetCheckBoxListItem(CheckBoxList checkBoxList, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new ListItem("N/A", "-1");
            }

            ListItem item = checkBoxList.Items.FindByValue(value);
            if (item != null) return item;

            return new ListItem("N/A", "-1");
        }

        public static bool LoadListBoxWithDefault(ListBox _listBox, object objData, string dataTextField, string dataValueField, string defaultText)
        {

            if (objData != null)
            {
                _listBox.DataSource = null;
                _listBox.Items.Clear();

                _listBox.DataSource = objData;
                _listBox.DataTextField = dataTextField;
                _listBox.DataValueField = dataValueField;

                try
                {
                    _listBox.DataBind();
                }
                catch
                {
                    return false;
                }
                _listBox.Items.Insert(0, new ListItem(defaultText, "-1"));

                return true;
            }

            return false;
        }

        public static string GetCurrenteUser(Page page)
        {
            var sUser = page.User;
            if (sUser == null || sUser.Identity.Name == null) return string.Empty;

            var userDomain = sUser.Identity.Name;
            return userDomain.Substring(userDomain.IndexOf("\\") + 1);
            //return "lsoto";

            //SPWeb sWeb = SPContext.Current.Web;
            //var user = sWeb.CurrentUser;
        }
    }
}