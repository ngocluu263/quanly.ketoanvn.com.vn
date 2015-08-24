using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Appketoan.Components;

namespace ThanhLapDN.Pages
{
    public partial class dang_nhap : System.Web.UI.Page
    {
        #region Declare

        AppketoanDataContext db = new AppketoanDataContext();

        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region My Function

        private void LogOut()
        {
            try
            {
                Session.Abandon();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void LogIn()
        {
            if (Log_In(txtUsername.Value, txtPassword.Value))
            {
                if (Check_Active(txtUsername.Value))
                {
                    Load_All_Cus(txtUsername.Value);
                    Response.Redirect("trang-chu.aspx", false);
                }
                else
                {
                    clsDataUtil.Show("Tài khoản này hiện đang tạm khóa!");
                }
            }
            else
            {
                clsDataUtil.Show("Tên đăng nhập hoặc mập khẩu không chính xác!");
            }
        }

        public bool Log_In(string Username, string MatKhau)
        {
            try
            {
                var _vLogin = db.GetTable<USER>().Where(a => a.USER_UN == Username);
                if (_vLogin.ToList().Count > 0)
                {
                    string asd=Common.Encrypt(MatKhau, _vLogin.First().SALT);
                    if (Common.Encrypt(MatKhau, _vLogin.First().SALT) == _vLogin.First().USER_PW)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Check_Active(string Username)
        {
            try
            {
                var _vLogin = db.GetTable<USER>().Where(a => a.USER_UN == Username);
                if (_vLogin.ToList().Count > 0)
                {
                    if (_vLogin.Single().USER_ACTIVE == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Load_All_Cus(string Username)
        {
            try
            {
                var _cus = db.GetTable<USER>().Where(a => a.USER_UN == Username);
                if (_cus.ToList().Count > 0)
                {
                    Session["Userid"] = _cus.First().USER_ID;
                    Session["Name"] = _cus.First().USER_NAME;
                    Session["Groupid"] = _cus.First().GROUP_ID;
                    var gettype = db.GROUPs.Where(n => n.GROUP_ID == _cus.First().GROUP_ID).ToList();
                    if (gettype.Count > 0)
                    {
                        Session["Grouptype"] = gettype.First().GROUP_TYPE;
                        Session["Groupname"] = gettype.First().GROUP_NAME;
                    }
                    HttpContext.Current.Response.Cookies["PITM_INFO"]["PITM_USER_ID"] = Session["Userid"].ToString();
                    HttpContext.Current.Response.Cookies["PITM_INFO"]["PITM_USER_NAME"] = Session["Name"].ToString();
                    HttpContext.Current.Response.Cookies["PITM_INFO"]["PITM_GROUP_ID"] = Session["Groupid"].ToString();
                    HttpContext.Current.Response.Cookies["PITM_INFO"]["PITM_GROUP_TYPE"] = Session["Grouptype"].ToString();
                    HttpContext.Current.Response.Cookies["PITM_INFO"]["PITM_GROUP_NAME"] = Session["Groupname"].ToString();
                    HttpContext.Current.Response.Cookies["PITM_INFO"].Expires = DateTime.Now.AddDays(30);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion

        #region Button Handler

        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            LogIn();
        }
        #endregion
    }
}