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
using System.Drawing;

namespace ThanhLapDN.Pages
{
    public partial class danh_sach_cong_no_web : System.Web.UI.Page
    {
        #region Declare
        private CongNoWebRepo _CongNoWebRepo = new CongNoWebRepo();
        private UnitData unitdata = new UnitData();
        private Function fun = new Function();
        private Pageindex_chage change = new Pageindex_chage();
        private AppketoanDataContext db = new AppketoanDataContext();
        private int _page = 0;
        private string _Month = "";
        private string _Year = "";
        private string _Search = "";
        private int _congno = 0;
        private int _tinhtrang = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _page = Utils.CIntDef(Request.QueryString["page"]);
            _Month = Utils.CStrDef(Request.QueryString["month"]);
            _Year = Utils.CStrDef(Request.QueryString["year"]);
            _Search = Utils.CStrDef(Request.QueryString["search"]);
            _congno = Utils.CIntDef(Request.QueryString["congno"]);
            _tinhtrang = Utils.CIntDef(Request.QueryString["tinhtrang"]);
            TestPermission();
            if (!IsPostBack)
            {
                loadYear();
                if (Session["CnPageCountWeb"] != null)
                {
                    ddlCountPage.SelectedValue = Utils.CStrDef(Session["CnPageCountWeb"]);
                }                
                if (_Month == "") _Month = DateTime.Now.Month.ToString();
                if(_Year == "") _Year = DateTime.Now.Year.ToString();
            }
            else
            {
                _Month = ddlThang.SelectedValue;
                _Year = ddlNam.SelectedValue;
                _congno = Utils.CIntDef(ddlCongnno.SelectedValue);
                _tinhtrang = Utils.CIntDef(ddlTinhtrang.SelectedValue);
                UpdatePageCount();
            }
            LoadProject();
        }
        protected void OnLoad(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                _Month = ddlThang.SelectedValue;
                _Year = ddlNam.SelectedValue;
                UpdatePageCount();
                LoadProject();
            }
        }
        private void loadYear()
        {
            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 10; i--)
            {
                ddlNam.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        #region LoadData
        private void LoadProject()
        {
            try
            {
                if (_Search != "" && _Search != null) txtKeyword.Value = _Search;
                if (_Month != "" && _Month != null) { ddlThang.SelectedValue = _Month; }
                if (_Year != "" && _Year != null) { ddlNam.SelectedValue = _Year; }
                if (_congno != 0) { ddlCongnno.SelectedValue = _congno.ToString(); }
                if (_tinhtrang != 0) { ddlTinhtrang.SelectedValue = _tinhtrang.ToString(); }

                int sotin = Utils.CIntDef(ddlCountPage.SelectedValue);
                int _idUser = Utils.CIntDef(Session["Userid"],0);
                int _idGroup = Utils.CIntDef(Session["Grouptype"], 0);
                if (_idGroup != 1 && _idGroup != 2 && _idGroup != 10 && _idGroup != 14)
                {
                    var list = _CongNoWebRepo.GetListByYear(Utils.CIntDef(ddlNam.SelectedValue), Utils.CIntDef(ddlThang.SelectedValue), _idUser, _Search, Utils.CIntDef(ddlTinhtrang.SelectedValue), Utils.CIntDef(ddlCongnno.SelectedValue));
                    //HttpContext.Current.Session["listCongNoWeb"] = list;
                    ASPxGridView1_project.DataSource = list.Skip(sotin * _page - sotin).Take(sotin);
                    ASPxGridView1_project.DataBind();

                    ltrPage.Text = change.result_web(list.Count, sotin, "danh-sach-cong-no-web", 0, _page, 1, ddlThang.SelectedValue, ddlNam.SelectedValue, "0", _Search, Utils.CIntDef(ddlTinhtrang.SelectedValue), Utils.CIntDef(ddlCongnno.SelectedValue));
                }
                else
                {
                    var list = _CongNoWebRepo.GetListByYear(Utils.CIntDef(ddlNam.SelectedValue), Utils.CIntDef(ddlThang.SelectedValue), -1, _Search, Utils.CIntDef(ddlTinhtrang.SelectedValue), Utils.CIntDef(ddlCongnno.SelectedValue));
                    //HttpContext.Current.Session["listCongNoWeb"] = list;
                    ASPxGridView1_project.DataSource = list.Skip(sotin * _page - sotin).Take(sotin);
                    ASPxGridView1_project.DataBind();

                    ltrPage.Text = change.result_web(list.Count, sotin, "danh-sach-cong-no-web", 0, _page, 1, ddlThang.SelectedValue, ddlNam.SelectedValue, "0", _Search, Utils.CIntDef(ddlTinhtrang.SelectedValue), Utils.CIntDef(ddlCongnno.SelectedValue));
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
        public string GetUser(object _id)
        {
            var list = db.USERs.Where(a => a.USER_ID == Convert.ToInt32(_id)).ToList();
            if (list.Count > 0)
            {
                return list[0].USER_NAME;
            }
            else { return ""; }
        }
        public string getTinhtrang(object tinhtrang)
        {
            int tt = Utils.CIntDef(tinhtrang);
            switch (tt)
            {
                case 1:
                    return "Đang chờ";
                case 2:
                    return "Đang triển khai";
                case 3:
                    return "Hoàn tất";
                case 4:
                    return "Hủy";
                default:
                    return "N/A";
            }
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
            if (Session["CnPageCountWeb"] != null)
            {
                if (Utils.CStrDef(Session["CnPageCountWeb"]) != ddlCountPage.SelectedValue)
                    Session["CnPageCountWeb"] = ddlCountPage.SelectedValue;
                else
                    ddlCountPage.SelectedValue = Utils.CStrDef(Session["CnPageCountWeb"]);
            }
            else
            {
                Session["CnPageCountWeb"] = ddlCountPage.SelectedValue;
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
                Response.Redirect("/Pages/danh-sach-cong-no-web.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue + "&field=0&search=" + txtKeyword.Value + "&congno=" + ddlCongnno.SelectedValue + "&tinhtrang=" + ddlTinhtrang.SelectedValue);
            else
                Response.Redirect("/Pages/danh-sach-cong-no-web.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue + "&congno=" + ddlCongnno.SelectedValue + "&tinhtrang=" + ddlTinhtrang.SelectedValue);
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (txtKeyword.Value != "")
                Response.Redirect("/Pages/danh-sach-cong-no-web.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue + "&field=0&search=" + txtKeyword.Value + "&congno=" + ddlCongnno.SelectedValue + "&tinhtrang=" + ddlTinhtrang.SelectedValue);
            else
                Response.Redirect("/Pages/danh-sach-cong-no-web.aspx?month=" + ddlThang.SelectedValue + "&year=" + ddlNam.SelectedValue + "&congno=" + ddlCongnno.SelectedValue + "&tinhtrang=" + ddlTinhtrang.SelectedValue);
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2")
            {
                List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
                foreach (var item in fieldValues)
                {
                    _CongNoWebRepo.Remove(Utils.CIntDef(item));
                }

                Response.Redirect("danh-sach-cong-no-web.aspx");
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert(' Bạn không có quyền này!');";
                strScript += "window.location='danh-sach-cong-no-web.aspx';";
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

            var obj = _CongNoWebRepo.GetListByYear(Utils.CIntDef(ddlNam.SelectedValue), Utils.CIntDef(ddlThang.SelectedValue), -1, _Search, Utils.CIntDef(ddlTinhtrang.SelectedValue), Utils.CIntDef(ddlCongnno.SelectedValue));
            double tongcong = 0, thanhtoan = 0, congno = 0;
            string name_ = "THEO DÕI HỢP ĐỒNG WEB " + ddlThang.SelectedValue + "/" + ddlNam.SelectedValue;
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
            cell.ColumnSpan = 28;
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
            _iHeader.Add(new InfoCells { Header = "SỐ HỢP ĐỒNG", Width = 150 });
            _iHeader.Add(new InfoCells { Header = "NGÀY KÝ HĐỒNG", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "TÊN KHÁCH HÀNG", Width = 250 });
            _iHeader.Add(new InfoCells { Header = "THÔNG TIN LH", Width = 200 });
            _iHeader.Add(new InfoCells { Header = "NỘI DUNG", Width = 200 });
            _iHeader.Add(new InfoCells { Header = "TÊN DOMAIN", Width = 200 });
            _iHeader.Add(new InfoCells { Header = "NVKD", Width = 70 });
            _iHeader.Add(new InfoCells { Header = "NVXL", Width = 70 });
            _iHeader.Add(new InfoCells { Header = "DOANH SỐ", Width = 445 });
            _iHeader.Add(new InfoCells { Header = "HOA HỒNG KH", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "VAT", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "TỔNG CỘNG", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "THANH TOÁN", Width = 120 });
            _iHeader.Add(new InfoCells { Header = "NGÀY THANH TOÁN", Width = 150 });
            _iHeader.Add(new InfoCells { Header = "NGÀY XUẤT HĐƠN", Width = 150 });
            _iHeader.Add(new InfoCells { Header = "CÔNG NỢ", Width = 100 });
            _iHeader.Add(new InfoCells { Header = "GHI CHÚ", Width = 250 });
            _iHeader.Add(new InfoCells { Header = "TÌNH TRẠNG", Width = 100 });

            for (int k = 0; k < _iHeader.Count; k++)
            {
                if (k == 8)
                {
                    cell = new TableCell();
                    cell.BackColor = Color.Yellow;
                    cell.VerticalAlign = VerticalAlign.Middle;
                    cell.Width = _iHeader[k].Width;
                    cell.Text = _iHeader[k].Header;
                    cell.ColumnSpan = 11;
                    row.Cells.Add(cell);
                }
                else
                {
                    cell = new TableCell();
                    cell.BackColor = Color.Yellow;
                    cell.VerticalAlign = VerticalAlign.Middle;
                    cell.Width = _iHeader[k].Width;
                    cell.Text = _iHeader[k].Header;
                    cell.RowSpan = 2;
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
            _iHeader1.Add(new InfoCells { Header = "DOMAIN", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "CHI PHÍ TRIỂN KHAI", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "HOSTING", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "WEB", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "LOGO,BANNER", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "ESELL", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "CHỤP HÌNH", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "CATALOGUE", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "SEO TỪ KHÓA", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "GOOGLE ADWORD", Width = 100 });
            _iHeader1.Add(new InfoCells { Header = "PHẦN MỀM", Width = 100 });

            for (int k = 0; k < _iHeader1.Count; k++)
            {
                cell = new TableCell();
                cell.BackColor = Color.Yellow;
                cell.VerticalAlign = VerticalAlign.Middle;
                cell.Width = _iHeader1[k].Width;
                cell.Text = _iHeader1[k].Header;
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
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].SO_HOPDONG) });
                string ngaykyhdong = "";
                if(Utils.CDateDef(obj[i].NGAYKY_HOPDONG, DateTime.MinValue) > DateTime.MinValue)
                    ngaykyhdong = getDate(obj[i].NGAYKY_HOPDONG);
                _items.Add(new InfoCells { Field = ngaykyhdong });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].TEN_KHACHHANG) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].THONGTINLIENHE_KHACHHANG) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NOIDUNG) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].TEN_DOMAIN) });
                _items.Add(new InfoCells { Field = GetUser(obj[i].NVKD) });
                _items.Add(new InfoCells { Field = GetUser(obj[i].NVXL) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].DOMAIN_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].CHIPHI_TRIENKHAI_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].HOSTING_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].WEB_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].LOGO_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].ESELL_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].CHUPHINH_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].CATALOGUE_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].SEOTUKHOA_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].GOOGLEADWORD_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].PHANMEM_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].HOAHONGKH_PRICE) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].VAT) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].TONGCONG) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].THANHTOAN) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAYTHANHTOAN) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].NGAYXUATHOADON) });
                _items.Add(new InfoCells { Field = fun.fomartPrice(obj[i].CONGNO) });
                _items.Add(new InfoCells { Field = Utils.CStrDef(obj[i].GHICHU) });
                _items.Add(new InfoCells { Field = getTinhtrang(obj[i].TINHTRANG) });
                tongcong += Utils.CDblDef(obj[i].TONGCONG);
                thanhtoan += Utils.CDblDef(obj[i].THANHTOAN);
                congno += Utils.CDblDef(obj[i].CONGNO);

                for (int j = 0; j < _items.Count; j++)
                {
                    cell = new TableCell();
                    cell.VerticalAlign = VerticalAlign.Middle;
                    if (j <= 6 || j == 22 || j == 23 || j == 26)
                    {
                        cell.HorizontalAlign = HorizontalAlign.Center;
                    }
                    cell.Text = _items[j].Field;
                    row.Cells.Add(cell);
                }
                tb.Rows.Add(row);
                #endregion
            }

            #region Footer
            row = new TableRow();            
            row.Font.Size = 11;
            row.Height = 25;
            row.Font.Bold = true;

            for (int k = 0; k < 28; k++)
            {
                cell = new TableCell();
                cell.BackColor = System.Drawing.Color.FromName("#C79393");
                cell.VerticalAlign = VerticalAlign.Middle;
                switch (k)
                {
                    case 21: cell.Text = fun.fomartPrice(tongcong); break;
                    case 22: cell.Text = fun.fomartPrice(thanhtoan); break;
                    case 25: cell.Text = fun.fomartPrice(congno); break;
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
            OleDbCommand command = new OleDbCommand("select * from [THWeb$]", connection);
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
                        if (i > 0)
                        {
                            CONG_NO_WEB item = new CONG_NO_WEB();
                            item.SO_HOPDONG = Utils.CStrDef(row[0]).Trim();
                            item.NGAYKY_HOPDONG = DateTime.Parse(Utils.CStrDef(row[1]));
                            item.TEN_KHACHHANG = Utils.CStrDef(row[2]);
                            item.THONGTINLIENHE_KHACHHANG = Utils.CStrDef(row[3]);
                            item.NOIDUNG = Utils.CStrDef(row[4]);
                            item.TEN_DOMAIN = Utils.CStrDef(row[5]);
                            item.NVKD = null;
                            item.NVXL = null;
                            item.DOMAIN_PRICE = Utils.CDecDef(Utils.CStrDef(row[7]).Replace(",", ""));
                            item.CHIPHI_TRIENKHAI_PRICE = Utils.CDecDef(Utils.CStrDef(row[8]).Replace(",", ""));
                            item.HOSTING_PRICE = Utils.CDecDef(Utils.CStrDef(row[9]).Replace(",", ""));
                            item.WEB_PRICE = Utils.CDecDef(Utils.CStrDef(row[10]).Replace(",", ""));
                            item.LOGO_PRICE = Utils.CDecDef(Utils.CStrDef(row[11]).Replace(",", ""));
                            item.ESELL_PRICE = Utils.CDecDef(Utils.CStrDef(row[12]).Replace(",", ""));
                            item.CHUPHINH_PRICE = Utils.CDecDef(Utils.CStrDef(row[13]).Replace(",", ""));
                            item.CATALOGUE_PRICE = Utils.CDecDef(Utils.CStrDef(row[14]).Replace(",", ""));
                            item.SEOTUKHOA_PRICE = Utils.CDecDef(Utils.CStrDef(row[15]).Replace(",", ""));
                            item.GOOGLEADWORD_PRICE = Utils.CDecDef(Utils.CStrDef(row[16]).Replace(",", ""));
                            item.PHANMEM_PRICE = Utils.CDecDef(Utils.CStrDef(row[17]).Replace(",", ""));
                            item.HOAHONGKH_PRICE = Utils.CDecDef(Utils.CStrDef(row[18]).Replace(",", ""));
                            item.VAT = Utils.CDecDef(Utils.CStrDef(row[19]).Replace(",", ""));
                            item.TONGCONG = Utils.CDecDef(Utils.CStrDef(row[20]).Replace(",", ""));
                            item.THANHTOAN = Utils.CDecDef(Utils.CStrDef(row[21]).Replace(",", ""));
                            item.NGAYTHANHTOAN = Utils.CStrDef(row[22]);
                            item.NGAYXUATHOADON = Utils.CStrDef(row[23]);
                            item.CONGNO = Utils.CDecDef(Utils.CStrDef(row[24]).Replace(",", ""));
                            item.GHICHU = Utils.CStrDef(row[25]);
                            item.TINHTRANG = Utils.CIntDef(1);

                            _CongNoWebRepo.Create(item);
                        }
                        i++;
                    }
                    string strScript = "<script>";
                    strScript += "alert('Đã Import dữ liệu thành công');";
                    strScript += "window.location='danh-sach-cong-no-web.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
                else
                {
                    string strScript = "<script>";
                    strScript += "alert('Xin chọn file để Import');";
                    strScript += "window.location='danh-sach-cong-no-web.aspx';";
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