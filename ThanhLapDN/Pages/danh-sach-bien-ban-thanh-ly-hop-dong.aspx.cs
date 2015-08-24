using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThanhLapDN.Data;
using vpro.functions;
using System.IO;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace ThanhLapDN.Pages
{
    public partial class danh_sach_bien_ban_thanh_ly_hop_dong : System.Web.UI.Page
    {
        #region Declare
        UnitData unitdata = new UnitData();
        AppketoanDataContext db = new AppketoanDataContext();
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        MerThanhLyHopDongDVData _MerThanhLyHopDongDVData = new MerThanhLyHopDongDVData();
        CongNoData _CongNoData = new CongNoData();
        getCookies _getCookies = new getCookies();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            getCookies _getCookies = new getCookies();
            _getCookies.getCookiesNew();
            TestPermission();

            if (!IsPostBack)
            {
                pickdate_Begin.returnDate = Convert.ToDateTime("01/01/2015");
                fromDate = pickdate_Begin.returnDate;
                pickdate_End.returnDate = DateTime.Now;
                toDate = pickdate_End.returnDate;
                _getCookies.getCookiesNew();//cấp lại session
                LoadProject();
            }
            else
            {
                ASPxGridView1_project.DataSource = HttpContext.Current.Session["listThanhLyHopDong"];
                ASPxGridView1_project.DataBind();
            }
        }
        
        #region Data
        private void LoadProject()
        {
            try
            {
                int _idUser = Utils.CIntDef(Session["Userid"]);
                int _idGroup = Utils.CIntDef(Session["Grouptype"], 0);
                fromDate = pickdate_Begin.returnDate;
                toDate = new DateTime(pickdate_End.returnDate.Year, pickdate_End.returnDate.Month, pickdate_End.returnDate.Day, 23, 59, 59);
                if (_idGroup != 1 && _idGroup != 2 && _idGroup != 9 && _idGroup != 14 && _idUser != 24)
                {
                    var list = _MerThanhLyHopDongDVData.GetListAll()
                        .Where(a => a.USER_ID == Utils.CIntDef(Session["Userid"])
                            && (a.MER_DATE <= toDate && a.MER_DATE >= fromDate)
                            && (a.MER_NAME2.Contains(txtKeyword.Value.ToUpper()) || a.MER_NAME2 == ""))
                        .OrderByDescending(n => n.MER_DATE);

                    HttpContext.Current.Session["listThanhLyHopDong"] = list;
                    ASPxGridView1_project.DataSource = list;
                    ASPxGridView1_project.DataBind();
                }
                else
                {
                    var list = _MerThanhLyHopDongDVData.GetListAll()
                        .Where(a => (a.MER_DATE <= toDate && a.MER_DATE >= fromDate)
                            && (a.MER_NAME2.Contains(txtKeyword.Value.ToUpper()) || a.MER_NAME2 == ""))
                        .OrderByDescending(n => n.MER_DATE);

                    HttpContext.Current.Session["listThanhLyHopDong"] = list;
                    ASPxGridView1_project.DataSource = list;
                    ASPxGridView1_project.DataBind();
                }
            }
            catch //(Exception)
            {
                //throw;
            }
        }
        private void CreateCongNo()
        {
            List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
            if (fieldValues.Count > 0)
            {
                foreach (var item in fieldValues)
                {
                    var obj = _MerThanhLyHopDongDVData.GetById(Utils.CIntDef(item));
                    if (obj != null)
                    {
                        //Kiểm tra xem trạng thái đã Hoàn thành chưa
                        if (Utils.CIntDef(obj.MER_STATUS) == 3)
                        {
                            var objCheck = db.CONG_NOs.Where(u => u.MST == obj.MER_TAXCODE && u.NAM == Utils.CIntDef(obj.MER_POS05, 0)).ToList();
                            if (objCheck.Count > 0)
                            {
                                objCheck[0].TINH_TRANG = "Ngừng dịch vụ";
                                db.SubmitChanges();
                            }
                        }
                    }
                }
                string strScript = "<script>";
                strScript += "alert('Đã xử lý xong!');";
                strScript += "window.location='danh-sach-bien-ban-thanh-ly-hop-dong.aspx';";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
            else {
                string strScript = "<script>";
                strScript += "alert('Xin chọn hợp đồng để cập nhật vào công nợ kế toán!');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        #endregion

        #region Function
        private void TestPermission()
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Userid"]) == "24")
            {
                ASPxGridView1_project.Columns[9].Visible = true;
                ASPxGridView1_project.Columns[10].Visible = true;
                lbtnCapNhapCongNo.Visible = true;
                if (Utils.CStrDef(Session["Userid"]) == "24")
                    lbtnDelete.Visible = false;
            }
            else
            {
                ASPxGridView1_project.Columns[9].Visible = false;
                ASPxGridView1_project.Columns[10].Visible = false;
                lbtnCapNhapCongNo.Visible = false;
                lbtnDelete.Visible = false;
            }
        }
        public string getDVKT(object taxcode)
        {
            string s = "";
            string _taxcode = Utils.CStrDef(taxcode);
            if (_taxcode != "")
            {
                var obj = _CongNoData.GetByMST(_taxcode);
                if (obj != null)
                {
                    s = String.Format(@"<a href='danh-sach-cong-no.aspx?year={0}&field=2&search={1}' title='Xem hồ sơ dịch vụ kế toán' target='_blank'>
                    <img src='/Images/icon_png.png' width='24' style='cursor:pointer;'/></a>"
                        , obj.NAM, obj.MST);
                }
                else s = "<p style='color:#FF0000;'>-----</p>";
            }
            else { s = "<p style='color:#FF0000;'>-----</p>"; }
            return s;
        }
        public string getStatus(object status, object ngayHT)
        {
            string str = "";
            int _status = Utils.CIntDef(status);
            string _ngayHT = Utils.CStrDef(ngayHT);
            if (_status == 1)
                str = "<b style='color:#0066FF;'>Xử lý xong</b>";
            else if (_status == 2)
                str = "<b style='color:#FF0000;'>Không xử lý</b>";
            else if (_status == 3)
            {
                str += "<b style='color:#009933;'>Đã hoàn thành</b>";
                str += "<br/>" + _ngayHT;
            }
            else
                str = "<b style='color:#ADADAD;'>Chưa xử lý</b>";
            return str;
        }
        public string getlink(object id)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2" || Utils.CStrDef(Session["Grouptype"]) == "3" || Utils.CStrDef(Session["Grouptype"]) == "4" || Utils.CStrDef(Session["Grouptype"]) == "10" || Utils.CStrDef(Session["Grouptype"]) == "14")
            {
                return Utils.CIntDef(id) > 0 ? "thanh-ly-hop-dong-ke-toan.aspx?id=" + Utils.CIntDef(id) : "thanh-ly-hop-dong-ke-toan.aspx";
            }
            else {
                return "danh-sach-bien-ban-thanh-ly-hop-dong.aspx"; 
            }
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string getfiledinhkem(object obj_id)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/WordFile/HopDongThanhLyDVKT/HopDongThanhLyDVKeToan_Code_" + Utils.CStrDef(obj_id) + ".docx";
            return path;
        }
        public string GetUser(object _id)
        {
            var list = db.USERs.Where(a => a.USER_ID == Convert.ToInt32(_id)).ToList();
            if (list.Count > 0)
            {
                return list[0].USER_NAME;
            }
            else { return ""; }
        }
        private void DeleteAllFilesInFolder(string folderpath)
        {
            try
            {
                foreach (var f in System.IO.Directory.GetFiles(folderpath))
                    System.IO.File.Delete(f);
                foreach (var f in System.IO.Directory.GetDirectories(folderpath))
                {
                    foreach (var i in System.IO.Directory.GetFiles(f))
                        System.IO.File.Delete(i);
                    System.IO.Directory.Delete(f);
                }
                if (Directory.Exists(folderpath))
                {
                    Directory.Delete(folderpath);
                }
            }
            catch (Exception ex) { }
        }
        protected virtual void PrepareTotalFilterItemsNV(ASPxGridViewHeaderFilterEventArgs e, string _field)
        {
            int count = e.Values.Count;
            ArrayList list = new ArrayList();
            if (count >= 3)
            {
                for (int i = 3; i < count; i++)
                {
                    list.Add(e.Values[i].Value);
                }
            }
            e.Values.Clear();
            if (e.Column.Settings.HeaderFilterMode == HeaderFilterMode.List)
                e.AddShowAll();
            for (int i = 0; i < list.Count; i++)
            {
                if (Utils.CStrDef(list[i]) == "0")
                    e.AddValue("Chưa nhập tên", Utils.CStrDef(list[i]), string.Format("[{0}] == {1}", _field, list[i]));
                else
                    e.AddValue(GetUser(list[i]), Utils.CStrDef(list[i]), string.Format("[{0}] == {1}", _field, list[i]));
            }
            list.Clear();
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
            
        }
        protected void ASPxGridView1_project_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "USER_ID")
                PrepareTotalFilterItemsNV(e, "USER_ID");
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            _getCookies.getCookiesNew();//cấp lại session
            //if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2")
            //{
            //    List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
            //    foreach (var item in fieldValues)
            //    {
            //        _MerThanhLyHopDongDVData.Remove(Utils.CIntDef(item));
            //        //string pathfileOld = Server.MapPath("/File/WordFile/HopDongDVKT/HopDongDVKeToan_Code_" + Utils.CStrDef(item) + ".docx");
            //        //System.IO.File.Delete(pathfileOld);
            //    }

            //    Response.Redirect("danh-sach-bien-ban-thanh-ly-hop-dong.aspx");
            //}
            //else
            //{
            //    string strScript = "<script>";
            //    strScript += "alert(' Bạn không có quyền này!');";
            //    strScript += "window.location='danh-sach-bien-ban-thanh-ly-hop-dong.aspx';";
            //    strScript += "</script>";
            //    Page.RegisterClientScriptBlock("strScript", strScript);
            //}
            string strScript = "<script>";
            strScript += "alert(' Xin liên hệ với quản lý để được cấp quyền!');";
            strScript += "window.location='danh-sach-bien-ban-thanh-ly-hop-dong.aspx';";
            strScript += "</script>";
            Page.RegisterClientScriptBlock("strScript", strScript);
        }
        protected void OnLoad(object sender, EventArgs e)
        {
            if (IsPostBack)
                LoadProject();
        }
        protected void lbtnCapNhapCongNo_Click(object sender, EventArgs e)
        {
            _getCookies.getCookiesNew();//cấp lại session
            CreateCongNo();
        }

        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }
        #endregion

    }
}