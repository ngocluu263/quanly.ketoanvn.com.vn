using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;

namespace ThanhLapDN.Pages
{
    public partial class popup_thanh_vien : System.Web.UI.Page
    {
        #region Declare
        private ProfileMember _member = new ProfileMember();
        AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadProject();
                TestPermission();
            }
            else
            {
                ASPxGridView1_project.DataSource = HttpContext.Current.Session["listMember"];
                ASPxGridView1_project.DataBind();
            }
        }
        private void LoadProject()
        {
            try
            {
                var list = db.PROFILE_MEMBERs.Where(n => n.PROF_ID == _id).OrderByDescending(n => n.MEM_DATE);

                HttpContext.Current.Session["listMember"] = list;
                ASPxGridView1_project.DataSource = list;
                ASPxGridView1_project.DataBind();
            }
            catch //(Exception)
            {

                //throw;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _member.Remove(Utils.CIntDef(item));
            }
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }

        #region Function
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string getPrice(object price)
        {
            return string.Format("{0:###,##0}", price);
        }

        public string GetType(object type)
        {
            int _type = Utils.CIntDef(type);
            string str = "";
            switch (_type)
            {
                case 0: str = "<b style='color:#0099FF;'>Đại diện pháp luật</b>"; break;
                case 1: str = "<b style='color:#0000FF;'>Thành viên</b>"; break;
            }
            return str;
        }

        public string GetInfoChild(object type, object MEM_BIRTHDAY, object MEM_FIELD1, object MEM_SEX, object MEM_NATIONALITY, object MEM_CMND,
            object MEM_DATE_CMND, object MEM_ADDRESS_CMND, object MEM_HOUSEHOLD, object MEM_ADDRESS, object MEM_CAPITAL, object MEM_PERCENT,
            object MEM_POSTION, object MEM_FIELD2)
        {
            string str = "";
            int _type = Utils.CIntDef(type);
            if (_type == 1)
            {
                string sGioiTinh = Utils.CIntDef(MEM_SEX) == 1 ? "Nam" : "Nữ";
                str = String.Format(@"
                    Ngày sinh: <b>{0}</b><br />
                    Dân tộc: <b>{1}</b><br />
                    Giới tính: <b>{2}</b><br />
                    Quốc tịch: <b>{3}</b><br />
                    CMND: <b>{4}</b><br />
                    Ngày cấp (CMND): <b>{5}</b><br />
                    Nơi cấp (CMND): <b>{6}</b><br />
                    Nởi thường trú hộ khẩu: <b>{7}</b><br />
                    Hiện tạm trú tại: <b>{8}</b><br />
                    Vốn góp: <b>{9}</b><br />
                    Tỷ lệ %: <b>{10}</b><br />"
                    , getDate(MEM_BIRTHDAY), MEM_FIELD1, sGioiTinh, MEM_NATIONALITY, MEM_CMND,
                        MEM_DATE_CMND, MEM_ADDRESS_CMND, MEM_HOUSEHOLD, MEM_ADDRESS, getPrice(MEM_CAPITAL), MEM_PERCENT);
            }
            else
            {
                string sGioiTinh = Utils.CIntDef(MEM_SEX) == 1 ? "Nam" : "Nữ";
                str = String.Format(@"
                    Ngày sinh: <b>{0}</b><br />
                    Dân tộc: <b>{1}</b><br />
                    Giới tính: <b>{2}</b><br />
                    Quốc tịch: <b>{3}</b><br />
                    CMND: <b>{4}</b><br />
                    Ngày cấp (CMND): <b>{5}</b><br />
                    Nơi cấp (CMND): <b>{6}</b><br />
                    Nởi thường trú hộ khẩu: <b>{7}</b><br />
                    Hiện tạm trú tại: <b>{8}</b><br />
                    Vốn góp: <b>{9}</b><br />
                    Tỷ lệ %: <b>{10}</b><br />
                    Chức danh ĐDPL: <b>{11}</b><br />
                    Người giữ chức danh còn lại: <b>{12}</b><br />"
                    , getDate(MEM_BIRTHDAY), MEM_FIELD1, sGioiTinh, MEM_NATIONALITY, MEM_CMND,
                        MEM_DATE_CMND, MEM_ADDRESS_CMND, MEM_HOUSEHOLD, MEM_ADDRESS, getPrice(MEM_CAPITAL), MEM_PERCENT,
                        MEM_POSTION, MEM_FIELD2);
            }
            return str;
        }

        public void TestPermission()
        {
            try
            {
                if (Session["Grouptype"].ToString() != "1" && Session["Grouptype"].ToString() != "2")
                {
                    lbtnDelete.Visible = false;
                }
            }
            catch (Exception) { }
        }
        #endregion

        #region Event
        protected void ASPxGridView1_project_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadProject();
        }
        protected void ASPxGridView1_project_PageIndexChanged(object sender, EventArgs e)
        {
            LoadProject();
        }
        protected void ASPxGridView1_project_DataBound(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    ((ASPxGridView)sender).DetailRows.ExpandAllRows();
        }
        #endregion
    }
}