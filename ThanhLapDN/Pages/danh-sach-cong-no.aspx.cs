using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using DevExpress.Web.ASPxGridView;
using System.Collections;

namespace ThanhLapDN.Pages
{
    public partial class danh_sach_cong_no : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        CongNoData _ProjectData = new CongNoData();
        NhanVienGiaoNhanData _NhanVienGiaoNhanData = new NhanVienGiaoNhanData();
        UnitData unit_data = new UnitData();
        Pageindex_chage change = new Pageindex_chage();
        Function fun = new Function();
        int _page = 0;
        string _Year = "";
        string _Search = "";
        string _Status = "";
        string _Field = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPermission();

            _page = Utils.CIntDef(Request.QueryString["page"]);
            _Year = Utils.CStrDef(Request.QueryString["year"]);
            _Search = Utils.CStrDef(Request.QueryString["search"]);
            _Status = Utils.CStrDef(Request.QueryString["status"]);
            _Field = Utils.CStrDef(Request.QueryString["field"]);

            if (!IsPostBack)
            {
                if (Session["CnPageCount"] != null)
                {
                    ddlCountPage.SelectedValue = Utils.CStrDef(Session["CnPageCount"]);
                }
                if (_Search != "") txtKeyword.Value = _Search;
                if (_Status != "") ddlStatus.SelectedValue = _Status;
                if (_Field != "") ddlFields.SelectedValue = _Field;
                ddlNam.SelectedValue = Utils.CStrDef(DateTime.Now.Year, "2015");
                LoadProject1();
            }
            Get_iCount(Utils.CIntDef(Session["countAllCN"], 0), Utils.CStrDef(Session["countNoThang"]), Utils.CStrDef(Session["countNoNam"]));
        }

        #region LoadData
        private void LoadProject1()
        {
            try
            {
                if (_Year != "" && _Year != null) { ddlNam.SelectedValue = _Year; }
                int _idUser = Utils.CIntDef(Session["Userid"]);
                int _idGroup = Utils.CIntDef(Session["Grouptype"], 0);
                int sotin = Utils.CIntDef(ddlCountPage.SelectedValue);
                ASPxGridView1_project.SettingsPager.PageSize = sotin;

                //groupType = 7 là hành chánh, nhớ kiểm tra lại
                if (_idGroup != 1 && _idGroup != 2 && _idGroup != 10 && _idGroup != 14 && _idGroup != 7)
                {
                    var list = _ProjectData.GetListByYearNV(Utils.CIntDef(ddlNam.SelectedValue), _idUser, _Field, _Search, _Status);
                    var listSkip = list.Skip(sotin * _page - sotin).Take(sotin);
                    ASPxGridView1_project.DataSource = listSkip;
                    ASPxGridView1_project.DataBind();

                    int _month = DateTime.Now.Month;
                    var _noThang = list.Sum(n =>
                        _month == 1 ? n.CON_NO_1 : _month == 2 ? n.CON_NO_2 :
                        _month == 3 ? n.CON_NO_3 : _month == 4 ? n.CON_NO_4 :
                        _month == 5 ? n.CON_NO_5 : _month == 6 ? n.CON_NO_6 :
                        _month == 7 ? n.CON_NO_7 : _month == 8 ? n.CON_NO_8 :
                        _month == 9 ? n.CON_NO_9 : _month == 10 ? n.CON_NO_10 :
                        _month == 11 ? n.CON_NO_11 : n.CON_NO_12);
                    var _noNam = list.Sum(n => n.TONG_NO);
                    Set_iCount(list.Count(), Utils.CDblDef(_noThang), Utils.CDblDef(_noNam));

                    ltrPage.Text = change.result(list.Count, sotin, "danh-sach-cong-no", 0, _page, 1, ddlNam.SelectedValue, _Field, _Search, _Status);
                }
                else
                {
                    var list = _ProjectData.GetListByYear(Utils.CIntDef(ddlNam.SelectedValue), _Field, _Search, _Status);
                    var listSkip = list.Skip(sotin * _page - sotin).Take(sotin);
                    ASPxGridView1_project.DataSource = listSkip;
                    ASPxGridView1_project.DataBind();

                    int _month = DateTime.Now.Month;
                    var _noThang = list.Sum(n=> 
                        _month == 1 ? n.CON_NO_1 : _month == 2 ? n.CON_NO_2 : 
                        _month == 3 ? n.CON_NO_3 : _month == 4 ? n.CON_NO_4 : 
                        _month == 5 ? n.CON_NO_5 : _month == 6 ? n.CON_NO_6 : 
                        _month == 7 ? n.CON_NO_7 : _month == 8 ? n.CON_NO_8 : 
                        _month == 9 ? n.CON_NO_9 : _month == 10 ? n.CON_NO_10 : 
                        _month == 11 ? n.CON_NO_11 : n.CON_NO_12 );
                    var _noNam = list.Sum(n => n.TONG_NO);
                    Set_iCount(list.Count(), Utils.CDblDef(_noThang), Utils.CDblDef(_noNam));

                    ltrPage.Text = change.result(list.Count, sotin, "danh-sach-cong-no", 0, _page, 1, ddlNam.SelectedValue, _Field, _Search, _Status);
                }
            }
            catch //(Exception)
            {
                Response.Redirect("/Pages/trang-chu.aspx");
                //throw;
            }
        }
        private void Update_Sync()
        {
            try
            {
                int _sum = 0;
                var obj = _NhanVienGiaoNhanData.GetListArea(1);
                for (int i = 0; i < obj.Count; i++)
                {
                    _sum += _ProjectData.SyncNhanVienGiaoNhan(Utils.CIntDef(obj[i].USER_ID), Utils.CIntDef(obj[i].PROP_ID), 1, Utils.CIntDef(ddlNam.SelectedValue));
                }

                var obj1 = _NhanVienGiaoNhanData.GetListArea(2);
                for (int i = 0; i < obj1.Count; i++)
                {
                    _sum += _ProjectData.SyncNhanVienGiaoNhan(Utils.CIntDef(obj1[i].USER_ID), Utils.CIntDef(obj1[i].PROP_ID_OTHER), 2, Utils.CIntDef(ddlNam.SelectedValue));
                }

                if (_sum > 0)
                {
                    string strScript = "<script>";
                    strScript += "alert('Đã cập nhật nhân viên giao nhận thành công!');";
                    strScript += "window.location='danh-sach-cong-no.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
            }
            catch { };
        }
        #endregion

        #region Funtion
        public void TestPermission()
        {
            try
            {
                getCookies _getCookies = new getCookies();
                _getCookies.getCookiesNew();

                int _groupType = Utils.CIntDef(Session["Grouptype"]);
                if (_groupType != 1 && _groupType != 2 && _groupType != 14)
                {
                    iListBtn.Visible = false;
                    imgBtnImport.Visible = fileUpload.Visible = false;
                }

                //Bổ sung cho Hành chánh dc nhập
                if (_groupType == 7)
                {
                    iListBtn.Visible = true;
                    btnSync.Visible = false;
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
        private void Set_iCount(int _countAll, double _noThang, double _noNam)
        {
            Session["countAllCN"] = _countAll;
            Session["countNoThang"] = String.Format("{0:###,##0} đ",_noThang);
            Session["countNoNam"] = String.Format("{0:###,##0} đ", _noNam);
        }
        private void Get_iCount(int _countAll, string _noThang, string _noNam)
        {
            liSoCty.Text = _countAll + "";
            liNoTrongThang.Text = _noThang + "";
            liNoTrongNam.Text = _noNam + "";
        }
        public string GetUser(object _id)
        {
            var list = db.USERs.Where(a => a.USER_ID == Convert.ToInt32(_id)).ToList();
            if (list.Count > 0)
            {
                return list[0].USER_NAME;
            }
            else if (Convert.ToInt32(_id) == 9999)
            {
                return "Nhân viên công ty";
            }
            else { return ""; }
        }
        public string getPropertyName(object idpro)
        {
            string str = "";
            if(idpro != null)
            {
                int _idpro = Utils.CIntDef(idpro);
                str = unit_data.Get_PropertyName(_idpro);
            }
            return str;
        }
        protected virtual void PrepareTotalFilterItems(ASPxGridViewHeaderFilterEventArgs e, string _field)
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
        protected virtual void PrepareTotalFilterItemsQLT(ASPxGridViewHeaderFilterEventArgs e)
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
                    e.AddValue("Chưa nhập tên", Utils.CStrDef(list[i]), string.Format("[QL_THUE_DIST] == {0}", list[i]));
                else
                    e.AddValue(getPropertyName(list[i]), Utils.CStrDef(list[i]), string.Format("[QL_THUE_DIST] == {0}", list[i]));
            }
            list.Clear();
        }
        private void UpdatePageCount()
        {
            if (Session["CnPageCount"] != null)
            {
                if (Utils.CStrDef(Session["CnPageCount"]) != ddlCountPage.SelectedValue)
                    Session["CnPageCount"] = ddlCountPage.SelectedValue;
                else
                    ddlCountPage.SelectedValue = Utils.CStrDef(Session["CnPageCount"]);
            }
            else
            {
                Session["CnPageCount"] = ddlCountPage.SelectedValue;
            }
        }
        #endregion

        #region Event
        protected void ASPxGridView1_project_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            int _id = Utils.CIntDef(e.KeyValue);
            var _obj = db.CONG_NOs.Where(n => n.ID == _id).ToList();
            if (_obj.Count > 0)
            {
                if (_obj[0].TINH_TRANG != null)
                {
                    if (_obj[0].TINH_TRANG.Contains("Tạm ngưng"))
                    {
                        e.Row.BackColor = Color.Orange;
                    }
                    else if (_obj[0].TINH_TRANG.Contains("Giải thể") || _obj[0].TINH_TRANG.Contains("Ngừng dịch vụ"))
                    {
                        e.Row.ForeColor = Color.Red;
                    }
                }
            }
        }
        protected void ASPxGridView1_project_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "NV_KD")
                PrepareTotalFilterItems(e, "NV_KD");
            if (e.Column.FieldName == "NV_GN")
                PrepareTotalFilterItems(e, "NV_GN");
            if (e.Column.FieldName == "NV_KT")
                PrepareTotalFilterItems(e, "NV_KT");
            if (e.Column.FieldName == "QL_THUE_DIST")
                PrepareTotalFilterItemsQLT(e);
        }
        protected void ASPxGridView1_project_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadProject1();
        }
        
        protected void ddlCountPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _iFields = Utils.CIntDef(ddlFields.SelectedValue);
            if (_iFields >= 9)
                Response.Redirect("/Pages/danh-sach-cong-no.aspx?year=" + ddlNam.SelectedValue + "&field=" + ddlFields.SelectedValue + "&status=" + ddlStatus.SelectedValue);
            else if (txtKeyword.Value != "" || ddlStatus.SelectedValue != "")
                Response.Redirect("/Pages/danh-sach-cong-no.aspx?year=" + ddlNam.SelectedValue + "&field=" + ddlFields.SelectedValue + "&status=" + ddlStatus.SelectedValue + "&search=" + txtKeyword.Value);
            else
                Response.Redirect("/Pages/danh-sach-cong-no.aspx?year=" + ddlNam.SelectedValue);
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            int _iFields = Utils.CIntDef(ddlFields.SelectedValue);
            if (_iFields >= 9)
                Response.Redirect("/Pages/danh-sach-cong-no.aspx?year=" + ddlNam.SelectedValue + "&field=" + ddlFields.SelectedValue + "&status=" + ddlStatus.SelectedValue);
            else if (txtKeyword.Value != "" || ddlStatus.SelectedValue != "")
                Response.Redirect("/Pages/danh-sach-cong-no.aspx?year=" + ddlNam.SelectedValue + "&field=" + ddlFields.SelectedValue + "&status=" + ddlStatus.SelectedValue + "&search=" + txtKeyword.Value);
            else
                Response.Redirect("/Pages/danh-sach-cong-no.aspx?year=" + ddlNam.SelectedValue);
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2")
            {
                List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
                foreach (var item in fieldValues)
                {
                    _ProjectData.Remove(Utils.CIntDef(item));
                }

                Response.Redirect("danh-sach-cong-no.aspx");
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert(' Bạn không có quyền này!');";
                strScript += "window.location='danh-sach-cong-no.aspx';";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        protected void OnLoad(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                _Year = ddlNam.SelectedValue;
                UpdatePageCount();
                LoadProject1();
            }
        }
        protected void btnSync_Click(object sender, EventArgs e)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2")
            {
                Update_Sync();
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert(' Bạn không có quyền này!');";
                strScript += "window.location='danh-sach-cong-no.aspx';";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        #endregion

        #region Import
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}
        private DataTable getDataexcel(string SourceFilePath)
        {

            DataTable dtExcel = new DataTable();
            // Connection String to Excel Workbook
            string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", SourceFilePath);
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = excelConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
            OleDbDataAdapter data = new OleDbDataAdapter(command);
            data.Fill(dtExcel);
            return dtExcel;
        }
        private void Import_data()
        {
            try
            {
                if (fileUpload.HasFile == true)
                {
                    string path = Server.MapPath("/File/ExcelFile/" + fileUpload.FileName);
                    fileUpload.SaveAs(path);
                    DataTable dt = getDataexcel(path);
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        CONG_NO obj = new CONG_NO();
                        obj.STT = Utils.CIntDef(row[0].ToString().Trim());
                        obj.TEN_KH = row[1].ToString().Trim();
                        obj.MST = row[2].ToString().Trim();
                        obj.DIA_CHI = row[3].ToString().Trim();
                        obj.GIAM_DOC = row[5].ToString().Trim();
                        obj.DIEN_THOAI = row[6].ToString().Trim();
                        obj.EMAIL = row[7].ToString().Trim();
                        obj.THANG_BD_THU = row[8].ToString().Trim();
                        obj.NO_NAM_TRUOC = Utils.CIntDef(row[9].ToString().Replace(".", ""));

                        obj.PHI_DV_1 = Utils.CIntDef(row[10].ToString().Replace(".", ""));
                        obj.DA_TT1_1 = Utils.CIntDef(row[11].ToString().Replace(".", ""));
                        obj.NGAY_TT_1 = row[12].ToString().Trim();
                        obj.CON_NO_1 = Utils.CIntDef(row[13].ToString().Replace(".", ""));

                        obj.PHI_DV_2 = Utils.CIntDef(row[14].ToString().Replace(".", ""));
                        obj.DA_TT2_1 = Utils.CIntDef(row[15].ToString().Replace(".", ""));
                        obj.NGAY_TT_2 = row[16].ToString().Trim();
                        obj.CON_NO_2 = Utils.CIntDef(row[17].ToString().Replace(".", ""));

                        obj.PHI_DV_3 = Utils.CIntDef(row[18].ToString().Replace(".", ""));
                        obj.DA_TT3_1 = Utils.CIntDef(row[19].ToString().Replace(".", ""));
                        obj.NGAY_TT_3 = row[20].ToString().Trim();
                        obj.CON_NO_3 = Utils.CIntDef(row[21].ToString().Replace(".", ""));

                        obj.PHI_DV_4 = Utils.CIntDef(row[22].ToString().Replace(".", ""));
                        obj.DA_TT4_1 = Utils.CIntDef(row[23].ToString().Replace(".", ""));
                        obj.NGAY_TT_4 = row[24].ToString().Trim();
                        obj.CON_NO_4 = Utils.CIntDef(row[25].ToString().Replace(".", ""));

                        obj.PHI_DV_5 = Utils.CIntDef(row[26].ToString().Replace(".", ""));
                        obj.DA_TT5_1 = Utils.CIntDef(row[27].ToString().Replace(".", ""));
                        obj.NGAY_TT_5 = row[28].ToString().Trim();
                        obj.CON_NO_5 = Utils.CIntDef(row[29].ToString().Replace(".", ""));

                        obj.PHI_DV_6 = Utils.CIntDef(row[30].ToString().Replace(".", ""));
                        obj.DA_TT6_1 = Utils.CIntDef(row[31].ToString().Replace(".", ""));
                        obj.NGAY_TT_6 = row[32].ToString().Trim();
                        obj.CON_NO_6 = Utils.CIntDef(row[33].ToString().Replace(".", ""));

                        obj.PHI_DV_7 = Utils.CIntDef(row[34].ToString().Replace(".", ""));
                        obj.DA_TT7_1 = Utils.CIntDef(row[35].ToString().Replace(".", ""));
                        obj.NGAY_TT_7 = row[36].ToString().Trim();
                        obj.CON_NO_7 = Utils.CIntDef(row[37].ToString().Replace(".", ""));

                        obj.PHI_DV_8 = Utils.CIntDef(row[38].ToString().Replace(".", ""));
                        obj.DA_TT8_1 = Utils.CIntDef(row[39].ToString().Replace(".", ""));
                        obj.NGAY_TT_8 = row[40].ToString().Trim();
                        obj.CON_NO_8 = Utils.CIntDef(row[41].ToString().Replace(".", ""));

                        obj.PHI_DV_9 = Utils.CIntDef(row[42].ToString().Replace(".", ""));
                        obj.DA_TT9_1 = Utils.CIntDef(row[43].ToString().Replace(".", ""));
                        obj.NGAY_TT_9 = row[44].ToString().Trim();
                        obj.CON_NO_9 = Utils.CIntDef(row[45].ToString().Replace(".", ""));

                        obj.PHI_DV_10 = Utils.CIntDef(row[46].ToString().Replace(".", ""));
                        obj.DA_TT10_1 = Utils.CIntDef(row[47].ToString().Replace(".", ""));
                        obj.NGAY_TT_10 = row[48].ToString().Trim();
                        obj.CON_NO_10 = Utils.CIntDef(row[49].ToString().Replace(".", ""));

                        obj.PHI_DV_11 = Utils.CIntDef(row[50].ToString().Replace(".", ""));
                        obj.DA_TT11_1 = Utils.CIntDef(row[51].ToString().Replace(".", ""));
                        obj.NGAY_TT_11 = row[52].ToString().Trim();
                        obj.CON_NO_11 = Utils.CIntDef(row[53].ToString().Replace(".", ""));

                        obj.PHI_DV_12 = Utils.CIntDef(row[54].ToString().Replace(".", ""));
                        obj.DA_TT12_1 = Utils.CIntDef(row[55].ToString().Replace(".", ""));
                        obj.NGAY_TT_12 = row[56].ToString().Trim();
                        obj.CON_NO_12 = Utils.CIntDef(row[57].ToString().Replace(".", ""));

                        obj.PHI_DV_BCTC = Utils.CIntDef(row[58].ToString().Replace(".", ""));
                        obj.DA_TT13_1 = Utils.CIntDef(row[59].ToString().Replace(".", ""));
                        obj.NGAY_TT_BCTC = row[60].ToString().Trim();
                        obj.CON_NO_BCTC = Utils.CIntDef(row[61].ToString().Replace(".", ""));

                        obj.TONG_NO = Utils.CIntDef(row[62].ToString().Replace(".", ""));
                        obj.GHI_CHU = row[63].ToString().Trim();
                        obj.NAM = Utils.CIntDef(ddlNam.SelectedValue);

                        _ProjectData.Create(obj);
                        i++;
                    }
                    string strScript = "<script>";
                    strScript += "alert('Đã Import dữ liệu thành công');";
                    strScript += "window.location='danh-sach-cong-no.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
                else
                {
                    string strScript = "<script>";
                    strScript += "alert('Xin chọn file để Import');";
                    strScript += "window.location='danh-sach-cong-no.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
            }
            catch {
                string strScript = "<script>";
                strScript += "alert('Dữ liệu không khớp với hệ thống!');";
                strScript += "window.location='danh-sach-cong-no.aspx';";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        protected void imgBtnImport_Click1(object sender, EventArgs e)
        {
            Import_data();
        }
        #endregion

        #region Export
        protected void imgBtnExport_Click1(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void ExportExcel()
        {

            #region Declare
            string keyword = txtKeyword.Value;

            var obj = _ProjectData.GetListByYearOrder(Utils.CIntDef(ddlNam.SelectedValue), _Field, _Search);
            double tongNoNam = 0;
            double tongPhiDV1 = 0, tongPhiDV2 = 0, tongPhiDV3 = 0, tongPhiDV4 = 0, tongPhiDV5 = 0, tongPhiDV6 = 0, 
                tongPhiDV7 = 0, tongPhiDV8 = 0, tongPhiDV9 = 0, tongPhiDV10 = 0, tongPhiDV11 = 0, tongPhiDV12 = 0, tongPhiDV13 = 0;
            double tongDaTT1 = 0, tongDaTT2 = 0, tongDaTT3 = 0, tongDaTT4 = 0, tongDaTT5 = 0, tongDaTT6 = 0, 
                tongDaTT7 = 0, tongDaTT8 = 0, tongDaTT9 = 0, tongDaTT10 = 0, tongDaTT11 = 0, tongDaTT12 = 0, tongDaTT13 = 0;
            double tongConNo1 = 0, tongConNo2 = 0, tongConNo3 = 0, tongConNo4 = 0, tongConNo5 = 0, tongConNo6 = 0, 
                tongConNo7 = 0, tongConNo8 = 0, tongConNo9 = 0, tongConNo10 = 0, tongConNo11 = 0, tongConNo12 = 0, tongConNo13 = 0;
            double tongNo = 0;

            string name_ = "CongNoKeToan" + ddlNam.SelectedValue;
            //Tạo bảng
            Table tb = new Table();
            //Định dạng bảng
            tb.CellPadding = 4;
            tb.GridLines = GridLines.Both;
            tb.Font.Name = "Times New Roman";
            
            TableCell cell;
            TableRow row;
            int header = 0;
            #endregion

            #region Header
            row = new TableRow();
            row.Font.Bold = true;
            row.Font.Size = 18;

            cell = new TableCell();
            cell.Height = 40;
            cell.Text = "DANH SÁCH TỔNG HỢP BCT NĂM " + ddlNam.SelectedValue;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 5;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.ColumnSpan = 9;
            row.Cells.Add(cell);

            for (int t = 0; t < 12; t++)
            {
                cell = new TableCell();
                cell.Text = "T" + (t + 1);
                cell.HorizontalAlign = HorizontalAlign.Center;
                cell.ColumnSpan = 4;
                row.Cells.Add(cell);
            }

            cell = new TableCell();
            cell.Text = "BCTC";
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 4;
            row.Cells.Add(cell);

            header++;
            tb.Rows.Add(row);
            #endregion
            #region Title
            row = new TableRow();
            row.Font.Bold = true;
            row.Font.Size = 12;
            row.Style.Value = "vertical-align: middle";

            List<InfoCells> _iHeader = new List<InfoCells>();
            _iHeader.Add(new InfoCells { Header = "STT", Width = 45 });
            _iHeader.Add(new InfoCells { Header = "Tình Trạng", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "Tên khách hàng", Width = 300 });
            _iHeader.Add(new InfoCells { Header = "Quản lý thuế", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "MST", Width = 80 });
            _iHeader.Add(new InfoCells { Header = "Địa chỉ", Width = 250 });
            _iHeader.Add(new InfoCells { Header = "Giám đốc", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "Điện thoại liên lạc", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "Email", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "Phí", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "Tháng bắt đầu thu", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "NV Kế Toán", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "Năm " + (Utils.CIntDef(ddlNam.SelectedValue) - 1), Width = 100 });

            //Từng tháng
            for (int imonth = 0; imonth < 13; imonth++)
            {
                _iHeader.Add(new InfoCells { Header = "Phí dịch vụ", Width = 100 });
                _iHeader.Add(new InfoCells { Header = "Đã thanh toán", Width = 100 });
                _iHeader.Add(new InfoCells { Header = "Ngày TT", Width = 100 });
                _iHeader.Add(new InfoCells { Header = "Còn nợ", Width = 100 });
            }
            _iHeader.Add(new InfoCells { Header = "TỔNG NỢ", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "NVKD", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "NV Giao Nhận", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "Ghi chú", Width = 200 });

            int ktemp1 = 0;
            int ktemp2 = 0;
            for (int k = 0; k < _iHeader.Count; k++)
            {
                cell = new TableCell();
                if (k < 12 || k > 65)
                {
                    cell.BackColor = System.Drawing.Color.FromName("#99CCFF");
                }
                else if (k == 12)
                {
                    cell.BackColor = System.Drawing.Color.FromName("#00CC00");
                }
                else if (k > 12 && 52 > (k - 13))
                {
                    if (ktemp2 < 4)
                    {
                        if (ktemp1 % 2 == 0)
                        {
                            cell.BackColor = System.Drawing.Color.FromName("#FFFF00");
                            ktemp1 += 2;
                            ktemp2++;
                        }
                        else
                        {
                            cell.BackColor = System.Drawing.Color.FromName("#FF9900");
                            ktemp1 += 2;
                            ktemp2++;
                        }
                    }
                    else
                    {
                        ktemp1++;
                        ktemp2 = 0;
                        if (ktemp1 % 2 == 0)
                        {
                            cell.BackColor = System.Drawing.Color.FromName("#FFFF00");
                            ktemp1 += 2;
                            ktemp2++;
                        }
                        else
                        {
                            cell.BackColor = System.Drawing.Color.FromName("#FF9900");
                            ktemp1 += 2;
                            ktemp2++;
                        }
                    }
                }
                else
                {
                    cell.BackColor = System.Drawing.Color.FromName("#DA8347");
                }
                cell.Height = 60;
                cell.Width = _iHeader[k].Width;
                cell.Text = _iHeader[k].Header;
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);
            }

            header++;
            tb.Rows.Add(row);
            #endregion
            for (int i = 0; i < obj.Count; i++)
            {
                #region Items
                row = new TableRow();
                row.Font.Size = 10;
                row.Style.Value = "vertical-align: middle";

                List<InfoCells> _items = new List<InfoCells>();
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].STT) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].TINH_TRANG) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].TEN_KH) });
                _items.Add(new InfoCells { Field = getPropertyName(obj[i].QL_THUE_DIST) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].MST) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].DIA_CHI) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].GIAM_DOC) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].DIEN_THOAI) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].EMAIL) });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].THANG_BD_THU).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = GetUser(obj[i].NV_KT) });

                tongNoNam += Utils.CDblDef(obj[i].NO_NAM_TRUOC);
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NO_NAM_TRUOC) });

                tongPhiDV1 += Utils.CDblDef(obj[i].PHI_DV_1);
                tongDaTT1 += Utils.CDblDef(obj[i].DA_TT1_1) + Utils.CDblDef(obj[i].DA_TT1_2) + Utils.CDblDef(obj[i].DA_TT1_3) + Utils.CDblDef(obj[i].DA_TT1_4);
                tongConNo1 += Utils.CDblDef(obj[i].CON_NO_1);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_1) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT1_1) + Utils.CDblDef(obj[i].DA_TT1_2) + Utils.CDblDef(obj[i].DA_TT1_3) + Utils.CDblDef(obj[i].DA_TT1_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_1).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_1) });

                tongPhiDV2 += Utils.CDblDef(obj[i].PHI_DV_2);
                tongDaTT2 += Utils.CDblDef(obj[i].DA_TT2_1) + Utils.CDblDef(obj[i].DA_TT2_2) + Utils.CDblDef(obj[i].DA_TT2_3) + Utils.CDblDef(obj[i].DA_TT2_4);
                tongConNo2 += Utils.CDblDef(obj[i].CON_NO_2);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_2) });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].DA_TT2_1 + obj[i].DA_TT2_2 + obj[i].DA_TT2_3 + obj[i].DA_TT2_4) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_2).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_2) });

                tongPhiDV3 += Utils.CDblDef(obj[i].PHI_DV_3);
                tongDaTT3 += Utils.CDblDef(obj[i].DA_TT3_1) + Utils.CDblDef(obj[i].DA_TT3_2) + Utils.CDblDef(obj[i].DA_TT3_3) + Utils.CDblDef(obj[i].DA_TT3_4);
                tongConNo3 += Utils.CDblDef(obj[i].CON_NO_3);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_3) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT3_1) + Utils.CDblDef(obj[i].DA_TT3_2) + Utils.CDblDef(obj[i].DA_TT3_3) + Utils.CDblDef(obj[i].DA_TT3_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_3).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_3) });

                tongPhiDV4 += Utils.CDblDef(obj[i].PHI_DV_4);
                tongDaTT4 += Utils.CDblDef(obj[i].DA_TT4_1) + Utils.CDblDef(obj[i].DA_TT4_2) + Utils.CDblDef(obj[i].DA_TT4_3) + Utils.CDblDef(obj[i].DA_TT4_4);
                tongConNo4 += Utils.CDblDef(obj[i].CON_NO_4);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_4) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT4_1) + Utils.CDblDef(obj[i].DA_TT4_2) + Utils.CDblDef(obj[i].DA_TT4_3) + Utils.CDblDef(obj[i].DA_TT4_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_4).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_4) });

                tongPhiDV5 += Utils.CDblDef(obj[i].PHI_DV_5);
                tongDaTT5 += Utils.CDblDef(obj[i].DA_TT5_1) + Utils.CDblDef(obj[i].DA_TT5_2) + Utils.CDblDef(obj[i].DA_TT5_3) + Utils.CDblDef(obj[i].DA_TT5_4);
                tongConNo5 += Utils.CDblDef(obj[i].CON_NO_5);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_5) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT5_1) + Utils.CDblDef(obj[i].DA_TT5_2) + Utils.CDblDef(obj[i].DA_TT5_3) + Utils.CDblDef(obj[i].DA_TT5_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_5).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_5) });

                tongPhiDV6 += Utils.CDblDef(obj[i].PHI_DV_6);
                tongDaTT6 += Utils.CDblDef(obj[i].DA_TT6_1) + Utils.CDblDef(obj[i].DA_TT6_2) + Utils.CDblDef(obj[i].DA_TT6_3) + Utils.CDblDef(obj[i].DA_TT6_4);
                tongConNo6 += Utils.CDblDef(obj[i].CON_NO_6);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_6) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT6_1) + Utils.CDblDef(obj[i].DA_TT6_2) + Utils.CDblDef(obj[i].DA_TT6_3) + Utils.CDblDef(obj[i].DA_TT6_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_6).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_6) });

                tongPhiDV7 += Utils.CDblDef(obj[i].PHI_DV_7);
                tongDaTT7 += Utils.CDblDef(obj[i].DA_TT7_1) + Utils.CDblDef(obj[i].DA_TT7_2) + Utils.CDblDef(obj[i].DA_TT7_3) + Utils.CDblDef(obj[i].DA_TT7_4);
                tongConNo7 += Utils.CDblDef(obj[i].CON_NO_7);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_7) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT7_1) + Utils.CDblDef(obj[i].DA_TT7_2) + Utils.CDblDef(obj[i].DA_TT7_3) + Utils.CDblDef(obj[i].DA_TT7_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_7).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_7) });

                tongPhiDV8 += Utils.CDblDef(obj[i].PHI_DV_8);
                tongDaTT8 += Utils.CDblDef(obj[i].DA_TT8_1) + Utils.CDblDef(obj[i].DA_TT8_2) + Utils.CDblDef(obj[i].DA_TT8_3) + Utils.CDblDef(obj[i].DA_TT8_4);
                tongConNo8 += Utils.CDblDef(obj[i].CON_NO_8);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_8) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT8_1) + Utils.CDblDef(obj[i].DA_TT8_2) + Utils.CDblDef(obj[i].DA_TT8_3) + Utils.CDblDef(obj[i].DA_TT8_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_8).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_8) });

                tongPhiDV9 += Utils.CDblDef(obj[i].PHI_DV_9);
                tongDaTT9 += Utils.CDblDef(obj[i].DA_TT9_1) + Utils.CDblDef(obj[i].DA_TT9_2) + Utils.CDblDef(obj[i].DA_TT9_3) + Utils.CDblDef(obj[i].DA_TT9_4);
                tongConNo9 += Utils.CDblDef(obj[i].CON_NO_9);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_9) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT9_1) + Utils.CDblDef(obj[i].DA_TT9_2) + Utils.CDblDef(obj[i].DA_TT9_3) + Utils.CDblDef(obj[i].DA_TT9_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_9).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_9) });

                tongPhiDV10 += Utils.CDblDef(obj[i].PHI_DV_10);
                tongDaTT10 += Utils.CDblDef(obj[i].DA_TT10_1) + Utils.CDblDef(obj[i].DA_TT10_2) + Utils.CDblDef(obj[i].DA_TT10_3) + Utils.CDblDef(obj[i].DA_TT10_4);
                tongConNo10 += Utils.CDblDef(obj[i].CON_NO_10);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_10) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT10_1) + Utils.CDblDef(obj[i].DA_TT10_2) + Utils.CDblDef(obj[i].DA_TT10_3) + Utils.CDblDef(obj[i].DA_TT10_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_10).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_10) });

                tongPhiDV11 += Utils.CDblDef(obj[i].PHI_DV_11);
                tongDaTT11 += Utils.CDblDef(obj[i].DA_TT11_1) + Utils.CDblDef(obj[i].DA_TT11_2) + Utils.CDblDef(obj[i].DA_TT11_3) + Utils.CDblDef(obj[i].DA_TT11_4);
                tongConNo11 += Utils.CDblDef(obj[i].CON_NO_11);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_11) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT11_1) + Utils.CDblDef(obj[i].DA_TT11_2) + Utils.CDblDef(obj[i].DA_TT11_3) + Utils.CDblDef(obj[i].DA_TT11_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_11).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_11) });

                tongPhiDV12 += Utils.CDblDef(obj[i].PHI_DV_12);
                tongDaTT12 += Utils.CDblDef(obj[i].DA_TT12_1) + Utils.CDblDef(obj[i].DA_TT12_2) + Utils.CDblDef(obj[i].DA_TT12_3) + Utils.CDblDef(obj[i].DA_TT12_4);
                tongConNo12 += Utils.CDblDef(obj[i].CON_NO_12);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_12) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT12_1) + Utils.CDblDef(obj[i].DA_TT12_2) + Utils.CDblDef(obj[i].DA_TT12_3) + Utils.CDblDef(obj[i].DA_TT12_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_12).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_12) });

                tongPhiDV13 += Utils.CDblDef(obj[i].PHI_DV_BCTC);
                tongDaTT13 += Utils.CDblDef(obj[i].DA_TT13_1) + Utils.CDblDef(obj[i].DA_TT13_2) + Utils.CDblDef(obj[i].DA_TT13_3) + Utils.CDblDef(obj[i].DA_TT13_4);
                tongConNo13 += Utils.CDblDef(obj[i].CON_NO_BCTC);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].PHI_DV_BCTC) });
                _items.Add(new InfoCells { Field = fun.Getprice(Utils.CDblDef(obj[i].DA_TT13_1) + Utils.CDblDef(obj[i].DA_TT13_2) + Utils.CDblDef(obj[i].DA_TT13_3) + Utils.CDblDef(obj[i].DA_TT13_4)) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_TT_BCTC).Replace("12:00:00 AM", "").Trim() });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO_BCTC) });

                tongNo += Utils.CDblDef(obj[i].TONG_NO);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].TONG_NO) });
                _items.Add(new InfoCells { Field = GetUser(obj[i].NV_KD) });
                _items.Add(new InfoCells { Field = GetUser(obj[i].NV_GN) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].GHI_CHU) });

                int jtemp1 = 0;
                int jtemp2 = 0;
                for (int j = 0; j < _items.Count; j++)
                {
                    cell = new TableCell();
                    if (j < 12 || j > 65)
                    {
                        if (j == 0 || j == 3 || j == 4)
                            cell.Style.Value = "text-align: center";
                        cell.BackColor = System.Drawing.Color.FromName("#fff");
                    }
                    else if (j == 12)
                        cell.BackColor = System.Drawing.Color.FromName("#00CC00");
                    else if (j > 12 && 52 > (j - 13))
                    {
                        if (jtemp2 < 4)
                        {
                            if (jtemp1 % 2 == 0)
                            {
                                cell.BackColor = System.Drawing.Color.FromName("#FFFF00");
                                jtemp1 += 2;
                                jtemp2++;
                            }
                            else
                            {
                                cell.BackColor = System.Drawing.Color.FromName("#FF9900");
                                jtemp1 += 2;
                                jtemp2++;
                            }
                        }
                        else
                        {
                            jtemp1++;
                            jtemp2 = 0;
                            if (jtemp1 % 2 == 0)
                            {
                                cell.BackColor = System.Drawing.Color.FromName("#FFFF00");
                                jtemp1 += 2;
                                jtemp2++;
                            }
                            else
                            {
                                cell.BackColor = System.Drawing.Color.FromName("#FF9900");
                                jtemp1 += 2;
                                jtemp2++;
                            }
                        }
                    }
                    else
                    {
                        cell.BackColor = System.Drawing.Color.FromName("#DA8347");
                    }

                    cell.Text = _items[j].Field;
                    row.Cells.Add(cell);
                }
                tb.Rows.Add(row);
                #endregion
            }

            #region Footer
            row = new TableRow();
            row.BackColor = System.Drawing.Color.FromName("#C79393");
            row.Font.Size = 10;
            row.Style.Value = "vertical-align: middle";
            row.Font.Bold = true;
            cell = new TableCell();
            cell.Height = 40;
            cell.Text = "Tổng cộng doanh thu DV hàng tháng";
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 5;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.ColumnSpan = 7;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongNoNam);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV1);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT1);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo1);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV2);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT2);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo2);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV3);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT3);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo3);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV4);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT4);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo4);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV5);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT5);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo5);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV6);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT6);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo6);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV7);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT7);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo7);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV8);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT8);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo8);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV9);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT9);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo9);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV10);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT10);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo10);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV11);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT11);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo11);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV12);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT12);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo12);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongPhiDV13);
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongDaTT13);
            row.Cells.Add(cell);
            cell = new TableCell();
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = fun.Getprice(tongConNo13);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = fun.Getprice(tongNo);
            row.Cells.Add(cell);

            tb.Rows.Add(row);
            #endregion

            #region Process
            Response.Clear();
            Response.Buffer = true;

            string ex_ = "xls";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + name_ + "." + ex_);

            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "UTF-8";
            this.EnableViewState = false;
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter ht = new System.Web.UI.HtmlTextWriter(sw);
            ht.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            tb.RenderControl(ht);
            Response.Write(sw.ToString());
            Response.End();
            #endregion
        }
        public class InfoCells
        {
            public string Header { get; set; }
            public string Field { get; set; }
            public int Width { get; set; }
        }
        #endregion
    }
}