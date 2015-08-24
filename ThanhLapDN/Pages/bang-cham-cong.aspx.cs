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
    public partial class bang_cham_cong : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        ChamCongData _ChamCongData = new ChamCongData();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getMhYrNow();
                getTitleGrv();
                Load_ChucVu();
                Loaduser();
            }
            else
            {
                if (HttpContext.Current.Session["listChamCong"] != null)
                {
                    grvChamCong.DataSource = HttpContext.Current.Session["listChamCong"];
                    grvChamCong.DataBind();
                    getTitleGrv();
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
                                && (b.GROUP_TYPE == _chucVu || 0 == _chucVu)
                                && (a.USER_MACC != null && a.USER_MACC != "")
                                && a.USER_ACTIVE == 1)
                            select new
                            {
                                a.USER_ID,
                                a.USER_NAME,
                                a.USER_UN,
                                a.USER_ACTIVE,
                                a.USER_CHINHANH,
                                a.USER_MACC,
                                b.GROUP_TYPE
                            }).OrderByDescending(n => n.USER_ID).OrderBy(n => n.USER_ACTIVE == 0).ToList();
                if (list.Count > 0)
                {
                    HttpContext.Current.Session["listChamCong"] = list;
                    grvChamCong.DataSource = list;
                    grvChamCong.DataBind();
                }
                else
                {
                    HttpContext.Current.Session["listChamCong"] = null;
                    grvChamCong.DataSource = list;
                    grvChamCong.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Function
        public List<CHAM_CONG> listChamCong(object maCC)
        {
            string _maCC = Utils.CStrDef(maCC);
            int _thang = Utils.CIntDef(lblThang.Text, 0);
            int _nam = Utils.CIntDef(lblNam.Text, 0);
            var list = _ChamCongData.GetListByYear(_maCC, _thang, _nam);
            return list;
        }
        public string getTime(object gtime)
        {
            string s = "---";
            double _gtime = Utils.CDblDef(gtime);
            if (_gtime != 0)
            {
                double _gtemp = Math.Round(_gtime * 1440);

                for (int i = 0; i <= 1440; i += 60)
                {
                    if (i <= _gtemp && _gtemp <= i + 60)
                    {
                        int iHH = Utils.CIntDef(Math.Truncate(_gtemp / 60));
                        int iMM = Utils.CIntDef(_gtemp) - i;
                        s = String.Format("{0}:{1}", Utils.CStrDef(iHH).Length == 2 ? Utils.CStrDef(iHH) : Utils.CStrDef("0" + iHH)
                            , Utils.CStrDef(iMM).Length == 2 ? Utils.CStrDef(iMM) : Utils.CStrDef("0" + iMM));
                    }
                }
            }
            return s;
        }
        public string getMinuLate(object gMinu)
        {
            string s = "";
            int _gMinu = Utils.CIntDef(gMinu);
            if (_gMinu > 0)
                s = "<b style='color:#FF0000;'>" + _gMinu + "</b>";
            else s = "---";
            return s;
        }
        public string getMinuLateSum(object maCC)
        {
            string _maCC = Utils.CStrDef(maCC);
            int _thang = Utils.CIntDef(lblThang.Text, 0);
            int _nam = Utils.CIntDef(lblNam.Text, 0);
            int dminu = _ChamCongData.GetSumMinuLate(_maCC, _thang, _nam);
            return dminu + " phút";
        }
        public string getDayoff(object maCC)
        {
            string _maCC = Utils.CStrDef(maCC);
            int _thang = Utils.CIntDef(lblThang.Text, 0);
            int _nam = Utils.CIntDef(lblNam.Text, 0);
            double dminu = _ChamCongData.GetSumDayoff(_maCC, _thang, _nam);
            return dminu + " ngày";
        }
        public string getStatus(object maCC)
        {
            string s = "";
            string _maCC = Utils.CStrDef(maCC);
            int _thang = Utils.CIntDef(lblThang.Text, 0);
            int _nam = Utils.CIntDef(lblNam.Text, 0);
            var obj = _ChamCongData.GetListByYear(_maCC, _thang, _nam);
            if (obj.Count > 0)
                s = "<b style='color:#0000FF;'>Đã lập</b>";
            else s = "<b style='color:#FF0000;'>Chưa lập</b>";
            return s;
        }
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
        private void getTitleGrv()
        {
            string sMonth = ddlThang.SelectedValue;
            string sYear = ddlNam.SelectedValue;
            lblNam.Text = sYear;
            lblThang.Text = sMonth;
        }
        private void getMhYrNow()
        {
            string sMonthNow = Utils.CStrDef(DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1);
            string sYearNow = Utils.CStrDef(DateTime.Now.Month == 1 ? DateTime.Now.Year - 1 : DateTime.Now.Year);
            ddlThang.SelectedValue = sMonthNow.Length == 2 ? sMonthNow : "0" + sMonthNow;
            ddlNam.SelectedValue = sYearNow;
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
            List<object> fieldValues = grvChamCong.GetSelectedFieldValues(new string[] { "USER_ID" });
            if (fieldValues.Count > 0)
            {
                var list = db.CHAM_CONGs.Where(n => fieldValues.Contains(n.CC_USERID.ToString())
                    && (n.CC_NAM == Utils.CIntDef(ddlNam.SelectedValue, 0) && n.CC_THANG == Utils.CIntDef(ddlThang.SelectedValue, 0)));
                db.CHAM_CONGs.DeleteAllOnSubmit(list);
                db.SubmitChanges();
                Response.Redirect("bang-cham-cong.aspx");
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert('Chọn nhân viên cần xóa!');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        #endregion

        #region Event
        protected void grvChamCong_PageIndexChanged(object sender, EventArgs e)
        {
            Loaduser();
        }
        protected void grvChamCong_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Loaduser();
        }
        protected void grvChamCong_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //int _id = Utils.CIntDef(e.KeyValue);
            //var _obj = db.USERs.Where(n => n.USER_ID == _id).ToList();
            //if (_obj.Count > 0)
            //{
            //    if (_obj[0].USER_ACTIVE == 0)
            //    {
            //        e.Row.ForeColor = Color.FromName("#FF0000");
            //    }
            //}
        }
        #endregion
    }
}