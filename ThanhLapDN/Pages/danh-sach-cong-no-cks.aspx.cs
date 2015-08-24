using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using DevExpress.Web.ASPxGridView;
using System.Collections;
using System.IO;
using ThanhLapDN.Data;
using System.Data;
using System.Data.OleDb;

namespace ThanhLapDN.Pages
{
    public partial class danh_sach_cong_no_cks : System.Web.UI.Page
    {
        #region Declare
        private CongNoCKSData _CongNoCKSData = new CongNoCKSData();
        UnitData unitdata = new UnitData();
        Function fun = new Function();
        Pageindex_chage change = new Pageindex_chage();
        AppketoanDataContext db = new AppketoanDataContext();
        int _page = 0;
        string _Month = "";
        string _Year = "";
        string _Search = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _page = Utils.CIntDef(Request.QueryString["page"]);
            _Month = Utils.CStrDef(Request.QueryString["month"]);
            _Year = Utils.CStrDef(Request.QueryString["year"]);
            _Search = Utils.CStrDef(Request.QueryString["search"]);
            TestPermission();
            if (!IsPostBack)
            {
                if (Session["CnPageCountCKS"] != null)
                {
                    ddlCountPage.SelectedValue = Utils.CStrDef(Session["CnPageCountCKS"]);
                }
                if (_Search != "") txtKeyword.Value = _Search;
            }
            else
            {
                _Month = ddlThang.SelectedValue;
                _Year = ddlNam.SelectedValue;
                UpdatePageCount();
            }
            LoadProject();
        }

        #region LoadData
        private void LoadProject()
        {
            try
            {
                if (_Month != "" && _Month != null) { ddlThang.SelectedValue = _Month; }
                if (_Year != "" && _Year != null) { ddlNam.SelectedValue = _Year; }
                int sotin = Utils.CIntDef(ddlCountPage.SelectedValue);
                int _idUser = Utils.CIntDef(Session["Userid"],0);
                int _idGroup = Utils.CIntDef(Session["Grouptype"], 0);
                if (_idGroup != 1 && _idGroup != 2 && _idGroup != 10 && _idGroup != 14)
                {
                    var list = _CongNoCKSData.GetListByYearNV(ddlNam.SelectedValue, ddlThang.SelectedValue, _idUser, _Search);
                    //HttpContext.Current.Session["listCongNoCKS"] = list;
                    ASPxGridView1_project.DataSource = list.Skip(sotin * _page - sotin).Take(sotin);
                    ASPxGridView1_project.DataBind();

                    ltrPage.Text = change.result_cks(list.Count, sotin, "danh-sach-cong-no-cks", 0, _page, 1, ddlThang.SelectedValue, ddlNam.SelectedValue, "0", _Search);
                }
                else
                {
                    var list = _CongNoCKSData.GetListByYear(ddlNam.SelectedValue, ddlThang.SelectedValue, _Search);
                    //HttpContext.Current.Session["listCongNoCKS"] = list;
                    ASPxGridView1_project.DataSource = list.Skip(sotin * _page - sotin).Take(sotin);
                    ASPxGridView1_project.DataBind();

                    ltrPage.Text = change.result_cks(list.Count, sotin, "danh-sach-cong-no-cks", 0, _page, 1, ddlThang.SelectedValue, ddlNam.SelectedValue, "0", _Search);
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
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string getPrice(object price)
        {
            return string.Format("{0:###,##0}", price);
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
        private void UpdatePageCount()
        {
            if (Session["CnPageCountCKS"] != null)
            {
                if (Utils.CStrDef(Session["CnPageCountCKS"]) != ddlCountPage.SelectedValue)
                    Session["CnPageCountCKS"] = ddlCountPage.SelectedValue;
                else
                    ddlCountPage.SelectedValue = Utils.CStrDef(Session["CnPageCountCKS"]);
            }
            else
            {
                Session["CnPageCountCKS"] = ddlCountPage.SelectedValue;
            }
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
        protected void ASPxGridView1_project_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "NV_KD")
                PrepareTotalFilterItemsNV(e, "NV_KD");
        }

        protected void ddlCountPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtKeyword.Value != "")
                Response.Redirect("/Pages/danh-sach-cong-no-cks.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue + "&field=0&search=" + txtKeyword.Value);
            else
                Response.Redirect("/Pages/danh-sach-cong-no-cks.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue);
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (txtKeyword.Value != "")
                Response.Redirect("/Pages/danh-sach-cong-no-cks.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue + "&field=0&search=" + txtKeyword.Value);
            else
                Response.Redirect("/Pages/danh-sach-cong-no-cks.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue);
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2")
            {
                List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
                foreach (var item in fieldValues)
                {
                    _CongNoCKSData.Remove(Utils.CIntDef(item));
                }

                Response.Redirect("danh-sach-cong-no-cks.aspx");
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert(' Bạn không có quyền này!');";
                strScript += "window.location='danh-sach-cong-no-cks.aspx';";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
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

            var obj = _CongNoCKSData.GetListByYear(ddlNam.SelectedValue,ddlThang.SelectedValue);
            double sumCKS_PhiDv = 0, sumCKS_GiaTK = 0, sumCKS_VAT = 0 , sumCKS_TongCong = 0;
            double sumCKS_HoaHong = 0, sumCKS_TongTTNCC = 0;
            double sumTT_TkCks = 0, sumTT_PM = 0, sumTT_HoaHongCks = 0, sumTT_HoaHongPm = 0, sumTT_ConNo = 0;
            string name_ = "CÔNG NỢ TỔNG HỢP CHỮ KÝ SỐ THÁNG " + ddlThang.SelectedValue + "/" + ddlNam.SelectedValue;
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
            cell.Text = "TỔNG HỢP THÁNG  " + ddlThang.SelectedValue + "/" + ddlNam.SelectedValue;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 6;
            row.Cells.Add(cell);

            header++;
            tb.Rows.Add(row);
            #endregion
            #region Title
            row = new TableRow();
            row.Font.Bold = true;
            row.Font.Size = 11;
            row.HorizontalAlign = HorizontalAlign.Center;
            row.VerticalAlign = VerticalAlign.Middle;
            List<InfoCells> _iHeader = new List<InfoCells>();
            _iHeader.Add(new InfoCells { Header = "STT", Width = 45 });
            _iHeader.Add(new InfoCells { Header = "TÊN CTY", Width = 200 });
            _iHeader.Add(new InfoCells { Header = "MST", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "NVKD", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "NGÀY NHẬN TB CỦA NVKD", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "TÌNH TRẠNG", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "LOẠI HỢP ĐỒNG", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "DỊCH VỤ CHỮ KÝ SỐ + PHẦN MỀM"});
            _iHeader.Add(new InfoCells { Header = "THU TIỀN KHÁCH HÀNG" });
            _iHeader.Add(new InfoCells { Header = "GHI CHÚ", Width = 150});

            for (int k = 0; k < _iHeader.Count; k++)
            {
                if ((k >= 0 && k <= 6) || k == 9)
                {
                    cell = new TableCell();
                    cell.Height = 100;
                    cell.Width = _iHeader[k].Width;
                    cell.Text = _iHeader[k].Header;
                    cell.RowSpan = 3;
                    row.Cells.Add(cell);
                }
                else if (k == 7)
                {
                    cell = new TableCell();
                    cell.Height = 20;
                    cell.Text = _iHeader[k].Header;
                    cell.ColumnSpan = 11;
                    row.Cells.Add(cell);
                }
                else if (k == 8)
                {
                    cell = new TableCell();
                    cell.Text = _iHeader[k].Header;
                    cell.ColumnSpan = 6;
                    row.Cells.Add(cell);
                }
            }
            tb.Rows.Add(row);

            row = new TableRow();
            row.Font.Bold = true;
            row.Font.Size = 11;
            row.HorizontalAlign = HorizontalAlign.Center;
            row.VerticalAlign = VerticalAlign.Middle;

            List<InfoCells> _iHeader1 = new List<InfoCells>();
            _iHeader1.Add(new InfoCells { Header = "NHÀ CUNG CẤP", Width = 80 });
            _iHeader1.Add(new InfoCells { Header = "SẢN PHẨM", Width = 80 });
            _iHeader1.Add(new InfoCells { Header = "NGÀY HẾT HẠN TB", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "PHÍ DV", Width = 140 });
            _iHeader1.Add(new InfoCells { Header = "GIÁ TB TOKEN", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "VAT", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "TỔNG CỘNG", Width = 120 });
            _iHeader1.Add(new InfoCells { Header = "HOA HỒNG ĐẠI LÝ CỦA KL", Width = 120 });
            _iHeader1.Add(new InfoCells { Header = "VAT HOA HỒNG", Width = 120 });
            _iHeader1.Add(new InfoCells { Header = "TỔNG HOA HỒNG", Width = 120 });
            _iHeader1.Add(new InfoCells { Header = "TỔNG THANH TOÁN NHÀ CUNG CẤP", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "TOKEN + CHỮ KÝ SỐ", Width = 110 });
            _iHeader1.Add(new InfoCells { Header = "PHẦN MỀM", Width = 110 });
            _iHeader1.Add(new InfoCells { Header = "NVKD TRÍCH HOA HỒNG"});
            _iHeader1.Add(new InfoCells { Header = "CÒN NỢ", Width = 110 });
            _iHeader1.Add(new InfoCells { Header = "NGÀY THU", Width = 110 });
            for (int k = 0; k < _iHeader1.Count; k++)
            {
                if (k == 13)
                {
                    cell = new TableCell();
                    cell.Text = _iHeader1[k].Header;
                    cell.ColumnSpan = 2;
                    row.Cells.Add(cell);
                }
                else
                {
                    cell = new TableCell();
                    cell.Width = _iHeader1[k].Width;
                    cell.Text = _iHeader1[k].Header;
                    cell.Height = 20;
                    cell.RowSpan = 2;
                    row.Cells.Add(cell);
                }
            }
            tb.Rows.Add(row);

            row = new TableRow();
            row.Font.Bold = true;
            row.Font.Size = 11;
            row.Height = 60;
            row.HorizontalAlign = HorizontalAlign.Center;
            row.VerticalAlign = VerticalAlign.Middle;

            List<InfoCells> _iHeader2 = new List<InfoCells>();
            _iHeader2.Add(new InfoCells { Header = "CHỮ KÝ SỐ", Width = 110 });
            _iHeader2.Add(new InfoCells { Header = "PHẦN MỀM", Width = 110 });

            for (int k = 0; k < _iHeader2.Count; k++)
            {
                cell = new TableCell();
                cell.Text = _iHeader2[k].Header;
                cell.Width = _iHeader2[k].Width;
                row.Cells.Add(cell);
            }
            tb.Rows.Add(row);


            header++;
            #endregion
            for (int i = 0; i < obj.Count; i++)
            {
                #region Items
                row = new TableRow();
                row.Font.Size = 11;

                List<InfoCells> _items = new List<InfoCells>();
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].STT) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].TEN_CTY) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].MST) });
                _items.Add(new InfoCells { Field = GetUser(obj[i].NV_KD) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_NHAN_TB) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].TINH_TRANG) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].LOAI_HOPDONG) });

                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].CKS_NHA_CUNG_CAP) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].CKS_SAN_PHAM) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].CKS_NGAY_HH_TB) });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_PHI_DV) });
                sumCKS_PhiDv += Utils.CDblDef(obj[i].CKS_PHI_DV);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_GIA_TK) });
                sumCKS_GiaTK += Utils.CDblDef(obj[i].CKS_GIA_TK);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_VAT) });
                sumCKS_VAT += Utils.CDblDef(obj[i].CKS_VAT);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_TONG_CONG) });
                sumCKS_TongCong += Utils.CDblDef(obj[i].CKS_TONG_CONG);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_HOA_HONG_DL) });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_HOA_HONG_VAT) });
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_HOA_HONG) });
                sumCKS_HoaHong += Utils.CDblDef(obj[i].CKS_HOA_HONG);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CKS_TONG_TT_NCC) });
                sumCKS_TongTTNCC += Utils.CDblDef(obj[i].CKS_TONG_TT_NCC);

                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].TT_TK_CKS) });
                sumTT_TkCks += Utils.CDblDef(obj[i].TT_TK_CKS);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].TT_PHAN_MEM) });
                sumTT_PM += Utils.CDblDef(obj[i].TT_PHAN_MEM);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].TT_HH_CKS) });
                sumTT_HoaHongCks += Utils.CDblDef(obj[i].TT_HH_CKS);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].TT_HH_PM) });
                sumTT_HoaHongPm += Utils.CDblDef(obj[i].TT_HH_PM);
                _items.Add(new InfoCells { Field = fun.Getprice(obj[i].CON_NO) });
                sumTT_ConNo += Utils.CDblDef(obj[i].CON_NO);
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAY_THU) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].GHI_CHU) });

                for (int j = 0; j < _items.Count; j++)
                {
                    cell = new TableCell();
                    //if ((j >= 0 && j <= 13) || j == 18 || (j >= 26 && j <= 34))
                    //{
                    //    cell.VerticalAlign = VerticalAlign.Middle;
                    //    cell.HorizontalAlign = HorizontalAlign.Center;
                    //}
                    cell.Text = _items[j].Field;
                    row.Cells.Add(cell);
                }
                tb.Rows.Add(row);
                #endregion
            }

            #region Footer
            row = new TableRow();
            row.BackColor = System.Drawing.Color.FromName("#C79393");
            row.Font.Size = 11;
            row.Height = 25;
            row.Font.Bold = true;

            for (int k = 0; k < 24; k++)
            {
                cell = new TableCell();
                switch (k)
                {
                    case 10: cell.Text = fun.Getprice(sumCKS_PhiDv); break;
                    case 11: cell.Text = fun.Getprice(sumCKS_GiaTK); break;
                    case 12: cell.Text = fun.Getprice(sumCKS_VAT); break;
                    case 13: cell.Text = fun.Getprice(sumCKS_TongCong); break;
                    case 16: cell.Text = fun.Getprice(sumCKS_HoaHong); break;
                    case 17: cell.Text = fun.Getprice(sumCKS_TongTTNCC); break;
                    case 18: cell.Text = fun.Getprice(sumTT_TkCks); break;
                    case 19: cell.Text = fun.Getprice(sumTT_PM); break;
                    case 20: cell.Text = fun.Getprice(sumTT_HoaHongCks); break;
                    case 21: cell.Text = fun.Getprice(sumTT_HoaHongPm); break;
                    case 22: cell.Text = fun.Getprice(sumTT_ConNo); break;
                }
                row.Cells.Add(cell);
            }

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
            OleDbCommand command = new OleDbCommand("select * from [TONGHOP2015$]", connection);
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
                        CONG_NO_CK obj = new CONG_NO_CK();
                        obj.STT = Utils.CIntDef(row[0].ToString().Trim());
                        obj.TEN_CTY = row[1].ToString().Trim();
                        obj.MST = row[2].ToString().Trim();
                        obj.NV_KD = null;
                        obj.NGAY_NHAN_TB = DateTime.ParseExact(row[3].ToString().Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        obj.CKS_NHA_CUNG_CAP = row[6].ToString().Trim();
                        obj.CKS_SAN_PHAM = row[9].ToString().Trim();

                        obj.CKS_PHI_DV = Utils.CIntDef(row[14].ToString().Replace(".", ""));
                        obj.CKS_GIA_TK = Utils.CIntDef(row[15].ToString().Replace(".", ""));
                        obj.CKS_VAT = Utils.CIntDef(row[16].ToString().Replace(".", ""));
                        obj.CKS_TONG_CONG = Utils.CIntDef(row[17].ToString().Replace(".", ""));
                        obj.CKS_HOA_HONG = Utils.CIntDef(row[23].ToString().Replace(".", ""));
                        obj.CKS_TONG_TT_NCC = Utils.CIntDef(row[24].ToString().Replace(".", ""));

                        obj.TT_TK_CKS = Utils.CIntDef(row[34].ToString().Replace(".", ""));
                        obj.TT_PHAN_MEM = Utils.CIntDef(row[35].ToString().Replace(".", ""));
                        obj.TT_HH_CKS = Utils.CIntDef(row[36].ToString().Replace(".", ""));
                        obj.TT_HH_PM = Utils.CIntDef(row[37].ToString().Replace(".", ""));

                        obj.CON_NO = Utils.CIntDef(row[38].ToString().Replace(".", ""));
                        obj.NGAY_THU = row[40].ToString().Trim();
                        obj.GHI_CHU = row[41].ToString().Trim();

                        obj.THANG = ddlThang.SelectedValue;
                        obj.NAM = ddlNam.SelectedValue;

                        _CongNoCKSData.Create(obj);
                        i++;
                    }
                    string strScript = "<script>";
                    strScript += "alert('Đã Import dữ liệu thành công');";
                    strScript += "window.location='danh-sach-cong-no-cks.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
                else
                {
                    string strScript = "<script>";
                    strScript += "alert('Xin chọn file để Import');";
                    strScript += "window.location='danh-sach-cong-no-cks.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
            }
            catch (Exception ex) { throw; }
        }
        protected void imgBtnImport_Click1(object sender, EventArgs e)
        {
            Import_data();
        }
        #endregion
    }
}