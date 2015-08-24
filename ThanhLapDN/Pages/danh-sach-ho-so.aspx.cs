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
    public partial class danh_sach_ho_so : System.Web.UI.Page
    {
        #region Declare
        private ProfileData _ProjectData = new ProfileData();
        private DeleteData _delData = new DeleteData();
        UnitData unitdata = new UnitData();
        AppketoanDataContext db = new AppketoanDataContext();
        public DateTime fromDate = new DateTime();
        public DateTime toDate = new DateTime();
        CongNoData _CongNoData = new CongNoData();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPermission();
            viewDebtCol();
            if (!IsPostBack)
            {
                pickdate_Begin.returnDate = Convert.ToDateTime("01/01/2015");
                fromDate = pickdate_Begin.returnDate;
                pickdate_End.returnDate = DateTime.Now;
                toDate = pickdate_End.returnDate;
                LoadProject();
            }
            else
            {
                ASPxGridView1_project.DataSource = HttpContext.Current.Session["listProj"];
                ASPxGridView1_project.DataBind();
            }
        }

        #region LoadData
        private void LoadProject()
        {
            try
            {
                int _typeprof = Utils.CIntDef(ddlTypeProf.SelectedValue);
                fromDate = pickdate_Begin.returnDate;
                toDate = new DateTime(pickdate_End.returnDate.Year, pickdate_End.returnDate.Month, pickdate_End.returnDate.Day, 23, 59, 59);
                if (Utils.CStrDef(Session["Grouptype"]) == "3")
                {
                    var list = _ProjectData.GetListByName(txtKeyword.Value)
                        .Where(a => a.USER_ID == Utils.CIntDef(Session["Userid"]) 
                            && (a.PROF_DATE <= toDate && a.PROF_DATE >= fromDate) 
                            && (a.PROF_TYPE == _typeprof || 0 == _typeprof )
                            && (chkViewDebt.Checked ? a.PROF_COST1 != a.PROF_COST2 : "" == "") )
                        .OrderByDescending(n => n.PROF_DATE).OrderBy(n=>n.PROF_STATUS == 12);

                    HttpContext.Current.Session["listProj"] = list;
                    ASPxGridView1_project.DataSource = list;
                    ASPxGridView1_project.DataBind();
                }
                else
                {
                    var list = _ProjectData.GetListByName(txtKeyword.Value)
                        .Where(a => (a.PROF_DATE <= toDate && a.PROF_DATE >= fromDate)
                            && (a.PROF_TYPE == _typeprof || 0 == _typeprof)
                            && (chkViewDebt.Checked ? a.PROF_COST1 != a.PROF_COST2 : "" == "") )
                        .OrderByDescending(n => n.PROF_DATE).OrderBy(n => n.PROF_STATUS == 12);

                    HttpContext.Current.Session["listProj"] = list;
                    ASPxGridView1_project.DataSource = list;
                    ASPxGridView1_project.DataBind();
                }
            }
            catch //(Exception)
            {
                //throw;
            }
        }
        #endregion

        #region Function
        public void TestPermission()
        {
            try
            {
                getCookies _getCookies = new getCookies();
                _getCookies.getCookiesNew();

                if (Utils.CStrDef(Session["Grouptype"]) != "1" && Utils.CStrDef(Session["Grouptype"]) != "2")
                {
                    lbtnDelete.Visible = false;
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
        public string getlink(object id)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2" || Utils.CStrDef(Session["Grouptype"]) == "4" || Utils.CStrDef(Session["Grouptype"]) == "10")
            {
                return Utils.CIntDef(id) > 0 ? "chi-tiet-ho-so.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-ho-so.aspx";
            }
            else { return "danh-sach-ho-so.aspx"; }
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string getPrice(object price)
        {
            return string.Format("{0:###,##0}", price);
        }
        public string Getactive(object active)
        {
            int id = Utils.CIntDef(active);
            string str = "";
            switch (id)
            {
                case 0: str = "<b style='color:#FF0000;'>Chưa duyệt</b>"; break;
                case 1: str = "<b style='color:#0099FF;'>Đang tiến hành</b>"; break;
                case 2: str = "<b style='color:#0000FF;'>Đã hoàn thành</b>"; break;
            }
            return str;
        }
        public string GetType(object type)
        {
            int _type = Utils.CIntDef(type);
            string str = "";
            switch (_type)
            {
                case 1: str = "<b style='color:#0099FF;'>Hồ sơ thành lập mới</b>"; break;
                case 2: str = "<b style='color:#009900;'>Hồ sơ thay đổi</b>"; break;
                case 3: str = "<b style='color:#FF9966;'>Hồ sơ hành chánh</b>"; break;
            }
            return str;
        }
        public string getfiledinhkem(object obj_id, object file)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + Utils.CStrDef(obj_id) + "/" + Utils.CStrDef(file);
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
        public string GetTrades(object idTrades)
        {
            int _idTrades = Utils.CIntDef(idTrades);
            var _obj = db.PROFILE_TRADEs.Where(n => n.ID == _idTrades).ToList();
            return _obj.Count > 0 ? _obj[0].TRAD_NAME : "";
        }
        public string GetInfoChild(object ID, object type, object PROF_NAME, object PROF_TRANSACTION, object PROF_ATC, object PROF_ADDRESS,
            object PROF_TOTAL_CAPITAL, object PROF_CAPITAL, object PROF_PHONE, object PROF_TAXCODE, object PROF_NOTE, object PROF_PARENT)
        {
            string str = "";
            int _type = Utils.CIntDef(type);
            if (_type == 1)
            {
                str = String.Format(@"
                    Tên công ty: <b>{0}</b><br />
                    Tên Giao dịch(Nếu có): <b>{1}</b><br />
                    Tên Viết Tắt(Nếu có): <b>{2}</b><br />
                    Trụ sở chính: <b>{3}</b><br />
                    Tổng số vốn góp: <b>{4}</b><br />
                    Vốn pháp định: <b>{5}</b><br />
                    Điện thoại liên hệ: <b>{6}</b><br />"
                    , PROF_NAME, PROF_TRANSACTION, PROF_ATC, PROF_ADDRESS,
                        getPrice(PROF_TOTAL_CAPITAL), getPrice(PROF_CAPITAL), PROF_PHONE);
                str += @"Thành viên thành lập: <a href='#' onClick='openDSTV1(" + ID + "); return false' title='Danh sách thành viên'><b>Danh sách thành viên</b></a>";
            }
            else if (_type == 2)
            {
                str = String.Format(@"
                    Tên công ty: <b>{0}</b><br />
                    Mã số thuế: <b>{2}</b><br />
                    Địa chỉ: <b>{1}</b><br />
                    Số điện thoại: <b>{3}</b><br />
                    Nội dung thay đổi: <b>{4}</b><br />"
                    , PROF_NAME, PROF_TRANSACTION, PROF_ADDRESS, PROF_TAXCODE, PROF_NOTE);
            }
            else {
                int idParent = Utils.CIntDef(PROF_PARENT,0);
                str += String.Format(@"
                    Tên công ty: <b>{0}</b><br />
                    Mã số thuế: <b>{7}</b><br />
                    Tên Giao dịch(Nếu có): <b>{1}</b><br />
                    Tên Viết Tắt(Nếu có): <b>{2}</b><br />
                    Trụ sở chính: <b>{3}</b><br />
                    Tổng số vốn góp: <b>{4}</b><br />
                    Vốn pháp định: <b>{5}</b><br />
                    Điện thoại liên hệ: <b>{6}</b><br />"
                    , PROF_NAME, PROF_TRANSACTION, PROF_ATC, PROF_ADDRESS,
                        getPrice(PROF_TOTAL_CAPITAL), getPrice(PROF_CAPITAL), PROF_PHONE, PROF_TAXCODE);
                if (idParent > 0)
                    str += @"Thành viên thành lập: <a href='#' onClick='openDSTV1(" + idParent + "); return false' title='Danh sách thành viên'><b>Danh sách thành viên</b></a>";
                else
                    str += @"Thành viên thành lập: <a href='#' onClick='openDSTV1(" + ID + "); return false' title='Danh sách thành viên'><b>Danh sách thành viên</b></a>";
            }
            return str;
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
        public string Getstatus(object status, object level)
        {
            return unitdata.Getstatus(status, level);
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
        protected virtual void PrepareTotalFilterItemsType(ASPxGridViewHeaderFilterEventArgs e, string _field)
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
                    e.AddValue(GetType(list[i]), Utils.CStrDef(list[i]), string.Format("[{0}] == {1}", _field, list[i]));
            }
            list.Clear();
        }
        private void viewDebtCol()
        {
            if (chkViewDebt.Checked)
                ASPxGridView1_project.Columns[6].Visible = true;
            else
                ASPxGridView1_project.Columns[6].Visible = false;
        }
        public string getDVKT(object prof_type, object prof_taxcode, object prof_parent)
        {
            string s = "";
            int _prof_type = Utils.CIntDef(prof_type);
            string _prof_taxcode = Utils.CStrDef(prof_taxcode);
            string _prof_parent = Utils.CStrDef(prof_parent);
            if (_prof_taxcode != "")
            {
                var obj = _CongNoData.GetByMST(_prof_taxcode);
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
            if (e.Column.FieldName == "PROF_TYPE")
                PrepareTotalFilterItemsType(e, "PROF_TYPE");
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2")
            {
                List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
                foreach (var item in fieldValues)
                {
                    _delData.DeleteMemberByProf(Utils.CIntDef(item));
                    _delData.DeleteAttachByProf(Utils.CIntDef(item));
                    _delData.DeleteWorkByProf(Utils.CIntDef(item));
                    _ProjectData.Remove(Utils.CIntDef(item));
                    string pathfileOld = Server.MapPath("/File/Profile/" + Utils.CStrDef(item));
                    DeleteAllFilesInFolder(pathfileOld);
                }

                Response.Redirect("danh-sach-ho-so.aspx");
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert(' Bạn không có quyền này!');";
                strScript += "window.location='danh-sach-ho-so.aspx';";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }
        #endregion

    }
}