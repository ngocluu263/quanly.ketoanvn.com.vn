using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace ThanhLapDN.Pages
{
    public partial class popup_quy_trinh_skip : System.Web.UI.Page
    {
        int _id = 0;
        int status = 0;
        int _type = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            status = Utils.CIntDef(Request.QueryString["status"]);
            _type = Utils.CIntDef(Request.QueryString["type"]);
            if(!IsPostBack)
                Load_Proccess();
        }

        #region funtion
        private void Load_Proccess()
        {
            if (_type == 3)
            {
                ddlProccess.DataValueField = "Value";
                ddlProccess.DataTextField = "Text";
                ddlProccess.DataSource = listProcess1();
                ddlProccess.DataBind();
            }
            else
            {
                ddlProccess.DataValueField = "Value";
                ddlProccess.DataTextField = "Text";
                ddlProccess.DataSource = listProcess();
                ddlProccess.DataBind();
            }
        }
        private List<ListItem> listProcess()
        {
            List<ListItem> l = new List<ListItem>();
            string[] _value = { "2", "3", "4", "5", "6", "7" };
            string[] _text = { "Giai đoạn 2: Soạn HS", 
                                 "Giai đoạn 3: Phân giao nhận", 
                                 "Giai đoạn 4: Ký nhận HS", 
                                 "Giai đoạn 5: Kiểm tra HS", 
                                 "Giai đoạn 6: Giao hồ sơ",
                                 "Giai đoạn 7: Nộp HS lên sở"};
            for (int i = 0; i < _value.Count(); i++)
            {
                l.Add(new ListItem(_text[i], _value[i]));
            }
            return l;
        }
        private List<ListItem> listProcess1()
        {
            List<ListItem> l = new List<ListItem>();
            string[] _value = { "2", "3", "4" };
            string[] _text = { "Giai đoạn 2: Soạn HS", 
                                 "Giai đoạn 3: Phân giao nhận", 
                                 "Giai đoạn 4: Ký nhận và nộp lên thuế"};
            for (int i = 0; i < _value.Count(); i++)
            {
                l.Add(new ListItem(_text[i], _value[i]));
            }
            return l;
        }
        public string getSendProcess(int _statusSelect)
        {
            string str = "";
            if (_statusSelect == 4)
            {
                if (_type == 3)
                    str = "openProcesHS_KT(" + _id + "," + _statusSelect + "," + _type + ")";
                else
                    str = "openProcesHS(" + _id + "," + _statusSelect + "," + _type + ")";
            }
            else if (_statusSelect == 2 && _type == 3)
            {
                str = "openProces2_KT(" + _id + "," + _statusSelect + "," + _type + ")";
            }
            else if (_statusSelect == 5 && _type == 3)
            {//Loại là 3 khi ở trạng thái 5
                str = "openProcesNopHS(" + _id + "," + _statusSelect + "," + _type + ")";
            }
            else if (_statusSelect == 6)
            {
                if (_type == 3)
                    str = "openProcesTraHS_KT(" + _id + "," + _statusSelect + "," + _type + ")";
                else
                    str = "openProcesGiaoHS(" + _id + "," + _statusSelect + "," + _type + ")";
            }
            else if (_statusSelect == 7)
            {
                if (_type == 3)
                    str = "openProcesFnMain(" + _id + "," + _statusSelect + "," + _type + ")";
                else
                    str = "openProcesNopHS(" + _id + "," + _statusSelect + "," + _type + ")";
            }
            else
            {
                //Do ở openProces bên kia tự tăng lên 1 nên mình sẽ giảm ở đây
                int _sProces = _statusSelect - 1;
                str = "openProces(" + _id + "," + _sProces + "," + _type + ")";
            }
            return str;
        }
        #endregion

        protected void btnDone_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.document.getElementById('" + getSendProcess(Utils.CIntDef(ddlProccess.SelectedValue)) + "').onclick = parent." + getSendProcess(Utils.CIntDef(ddlProccess.SelectedValue)) + ";</script>");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }
    }
}