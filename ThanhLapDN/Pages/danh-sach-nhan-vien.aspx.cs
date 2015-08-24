using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using DevExpress.Web.ASPxGridView;
using ThanhLapDN.Data;
using System.Drawing;
namespace ThanhLapDN.Pages
{
    public partial class danh_sach_nhan_vien : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_ChucVu();
                Loaduser();
            }
            else
            {
                if (HttpContext.Current.Session["ktoan.listuser"] != null)
                {
                    ASPxGridView1_user.DataSource = HttpContext.Current.Session["ktoan.listuser"];
                    ASPxGridView1_user.DataBind();
                }
            }
        }
        #region Loaddata
        private void Load_ChucVu()
        {
            var obj = db.GROUPs.Where(n => n.GROUP_TYPE != 2).ToList();
            if (obj.Count > 0)
            {
                ddlChucVu.DataValueField = "GROUP_TYPE";
                ddlChucVu.DataTextField = "GROUP_NAME";
                ddlChucVu.DataSource = obj;
                ddlChucVu.DataBind();

                ListItem l = new ListItem("---Tất cả chức vụ---", "0", true);
                ddlChucVu.Items.Insert(0, l);
                ddlChucVu.SelectedIndex = 0;
            }
        }
        public void Loaduser()
        {
            try
            {
                int _chucVu = Utils.CIntDef(ddlChucVu.SelectedValue);
                int _diaDiem = Utils.CIntDef(ddlDiaDiem.SelectedValue);
                var list = (from a in db.USERs
                            join b in db.GROUPs on a.GROUP_ID equals b.GROUP_ID
                            where ((a.USER_NAME.Contains(txtKeyword.Value) || txtKeyword.Value == "")
                                && (a.USER_CHINHANH == _diaDiem || 0 == _diaDiem)
                                && (b.GROUP_TYPE == _chucVu || 0 == _chucVu))
                            select new
                            {
                                a.USER_ID,
                                a.USER_NAME,
                                a.USER_UN,
                                a.USER_EMAIL,
                                a.USER_PHONE,
                                a.USER_ADDRESS,
                                a.USER_GIOITINH,
                                a.USER_NGAYSINH,
                                a.USER_DANTOC,
                                a.USER_NGUYENQUAN,
                                a.USER_CMND,
                                a.USER_CMND_NGAYCAP,
                                a.USER_CMND_NOICAP,
                                a.USER_NOIDK_HK,
                                a.USER_EMAIL_CANHAN,
                                a.USER_PHONE_CANHAN,
                                a.USER_TRINHDO,
                                a.NT_HOTEN,
                                a.NT_MOIQUANHE,
                                a.NT_SDT,
                                a.USER_ACTIVE,
                                a.USER_CHINHANH,
                                b.GROUP_TYPE
                            }).OrderByDescending(n => n.USER_ID).OrderBy(n => n.USER_ACTIVE == 0).ToList();
                if (list.Count > 0)
                {
                    HttpContext.Current.Session["ktoan.listuser"] = list;
                    ASPxGridView1_user.DataSource = list;
                    ASPxGridView1_user.DataBind();
                }
                else
                {
                    HttpContext.Current.Session["ktoan.listuser"] = null;
                    ASPxGridView1_user.DataSource = list;
                    ASPxGridView1_user.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Function
        public string getDiaDiem(object diaDiem)
        {
            string str = "";
            int _diaDiem = Utils.CIntDef(diaDiem);
            switch (_diaDiem)
            {
                case 1: str = "Tp.HCM - Trụ sở chính"; break;
                case 2: str = "Hà Nội - Chi nhánh"; break;
                case 3: str = "Nha Trang - Chi nhánh"; break;
                case 4: str = "Đà Nẵng - Chi nhánh"; break;
                default: str = "----"; break;
            }
            return str;
        }
        public string getlink(object userid)
        {
            return Utils.CIntDef(userid) > 0 ? "chi-tiet-nhan-vien.aspx?userid=" + Utils.CIntDef(userid) : "chi-tiet-nhan-vien.aspx";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string getSex(object User_GioiTinh)
        {
            string str = "";
            if (User_GioiTinh != null)
            {
                int _User_GioiTinh = Utils.CIntDef(User_GioiTinh);
                if (_User_GioiTinh == 1)
                    str = "Nam";
                else
                    str = "Nữ";
            }
            return str;
        }
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 1 ? "Kích hoạt" : "Tạm khóa";
        }
        public string Getnhom(object groupid)
        {
            int id = Utils.CIntDef(groupid);
            string str = "";
            switch (id)
            {
                case 1: str = "Admin"; break;
                case 2: str = "Quản lý chung"; break;
                case 3: str = "Kinh doanh"; break;
                case 4: str = "Quản lý xử lý hồ sơ - Sở"; break;
                case 5: str = "Nhân viên xử lý hồ sơ -Sở"; break;
                case 6: str = "Nhân viên giao nhận"; break;
                case 7: str = "Nhân viên hành chánh"; break;
                case 8: str = "Nhân viên nộp hồ sơ"; break;
                case 9: str = "Kế toán"; break;
                case 10: str = "Quản lý xử lý hồ sơ - Hành chánh"; break;
                case 11: str = "Nhân viên xử lý hồ sơ - Hành chánh"; break;
                case 12: str = "Nhân viên hành chánh - Hành chánh"; break;
                case 13: str = "Nhân viên soạn hồ sơ - Hành chánh"; break;
                case 14: str = "Kế toán nội bộ"; break;
            }
            return str;
        }
        public bool Check_Condition(List<object> l)
        {
            var obj = db.WORKFLOW_USERs.Where(n => l.Contains(n.USER_ID)).ToList();
            var obj2 = db.PROFILE_NEWs.Where(n => l.Contains(n.USER_ID)).ToList();
            var obj3 = db.CONG_NOs.Where(n => l.Contains(n.NV_KT) || l.Contains(n.NV_KD) || l.Contains(n.NV_GN)).ToList();
            if (obj.Count > 0 || obj2.Count > 0 || obj3.Count > 0)
            {
                return true;
            }
            else return false;
        }
        #endregion

        #region Buttion
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Loaduser();
        }
        protected void lbtnDelete_Click1(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_user.GetSelectedFieldValues(new string[] { "USER_ID" });
            if (!Check_Condition(fieldValues))
            {
                var list = db.USERs.Where(n => fieldValues.Contains(n.USER_ID.ToString()));
                db.USERs.DeleteAllOnSubmit(list);
                db.SubmitChanges();
                //Loaduser();
                Response.Redirect("danh-sach-nhan-vien.aspx");
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert('Tên nhân viên này hiện đang sử dụng ở 1 hồ sơ nào đó! Để tránh phát sinh lỗi dữ liệu nên việc xóa không thực hiện được! ');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        #endregion

        #region Event
        protected void ASPxGridView1_user_PageIndexChanged(object sender, EventArgs e)
        {
            Loaduser();
        }
        protected void ASPxGridView1_user_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Loaduser();
        }
        protected void ASPxGridView1_user_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            int _id = Utils.CIntDef(e.KeyValue);
            var _obj = db.USERs.Where(n => n.USER_ID == _id).ToList();
            if (_obj.Count > 0)
            {
                if (_obj[0].USER_ACTIVE == 0)
                {
                    e.Row.ForeColor = Color.FromName("#FF0000");
                }
            }
        }
        #endregion
    }
}