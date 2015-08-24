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

namespace ThanhLapDN.Pages
{
    public partial class popup_tao_bang_cham_cong : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        ChamCongData _ChamCongData = new ChamCongData();
        UserRepo _UserRepo = new UserRepo();
        int year = 0;
        int month = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            year = Utils.CIntDef(Request.QueryString["year"]);
            month = Utils.CIntDef(Request.QueryString["month"]);
            if (!_ChamCongData.CheckExistsYearMonth(year, month, Utils.CIntDef(ddlPhongBan.SelectedValue)))
                btnDone.OnClientClick = "";
            if (!IsPostBack)
            {
                liTitle.Text = "BẢNG CHẤM CÔNG THÁNG <b style='color: #FF0000;'>" + month + "</b> NĂM <b style='color: #FF0000;'>" + year + "</b>";
                Load_Group();
            }
        }

        #region Data
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
                
            }
            catch (Exception ex) { throw; }
        }
        private void Save_Data(int _year, int _month, int _phongban)
        {
            _ChamCongData.RemoveByYearMonth(_year, _month, _phongban);//Xóa bảng lương có phòng ban đã chọn

            if (fileUpload.HasFile == true)
            {
                string path = Server.MapPath("/File/ExcelFile/" + fileUpload.FileName);
                fileUpload.SaveAs(path);
                DataTable dt = getDataexcel(path);
                int _count = dt.Rows.Count;
                for (int i = 0; i < _count; i++)
                {
                    string _maNV = Utils.CStrDef(dt.Rows[i][0]);
                    if (_maNV.Contains("Mã nhân viên"))
                    {
                        string _getMaCC = _maNV.Substring(14, 5);
                        var obj = _UserRepo.GetInfoUserByMaCC(_getMaCC, _phongban);
                        if (obj.Count > 0)
                        {
                            i += 7;
                            while (Utils.CStrDef(dt.Rows[i][0]) != "")
                            {
                                CHAM_CONG c = new CHAM_CONG();
                                c.CC_MACC = _getMaCC;
                                c.CC_USERID = obj[0].USER_ID;
                                c.CC_PHONGBAN = obj[0].GROUP_ID;
                                c.CC_NAM = _year;
                                c.CC_THANG = _month;
                                c.CC_NGAY = DateTime.ParseExact(Utils.CStrDef(dt.Rows[i][0]), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                                double dGioVao = getHourIn(Utils.CDblDef(dt.Rows[i][2]), Utils.CDblDef(dt.Rows[i][3]));
                                double dGioRa = getHourOut(Utils.CDblDef(dt.Rows[i][2]), Utils.CDblDef(dt.Rows[i][3]));

                                c.CC_THU = Utils.CStrDef(dt.Rows[i][1]);
                                c.CC_VAO = dGioVao;
                                c.CC_RA = dGioRa;
                                c.CC_SONGAYTRE = getDayLate(dGioVao);
                                c.CC_SONGAYNGHI = getDayoff(dt.Rows[i][1], dGioVao, dGioRa);
                                c.CC_SOPHUTTRE = getTimeLate(dGioVao, 8);
                                _ChamCongData.Create(c);
                                i++;
                            }
                        }
                    }
                }

                string strScript = "<script>";
                strScript += "alert('Đã Import dữ liệu thành công');";
                strScript += "parent.emailwindow.close(); parent.GetCurrentTime();";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert('Xin chọn file để Import');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        #endregion

        #region  Funtion
        private double getHourIn(object gtime1, object gtime2)
        {
            double dbl = 0;
            double _gtime1 = Utils.CDblDef(gtime1);
            double _gtime2 = Utils.CDblDef(gtime2);
            if (_gtime1 > 0.5)
                dbl = 0;
            else dbl = _gtime1;
            return dbl;
        }
        private double getHourOut(object gtime1, object gtime2)
        {
            double dbl = 0;
            double _gtime1 = Utils.CDblDef(gtime1);
            double _gtime2 = Utils.CDblDef(gtime2);
            if (_gtime1 > 0.5)
                dbl = _gtime1;
            else dbl = _gtime2;
            return dbl;
        }
        private double getDayLate(object gtime)
        {
            double dbl = 0;
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
                        if (iHH > 8 || (iHH == 8 && iMM > 0))
                            dbl = 1;
                    }
                }
            }
            return dbl;
        }
        private double getDayoff(object gDays, object gtime1, object gtime2)
        {
            double dbl = 0;
            string _gDays = Utils.CStrDef(gDays);
            double _gtime1 = Utils.CDblDef(gtime1);
            double _gtime2 = Utils.CDblDef(gtime2);
            if (_gDays != "CN")
            {
                if (_gDays == "Bảy" && _gtime1 == 0)
                    dbl = 0.5;
                else if (_gtime1 == 0 && _gtime2 == 0)
                    dbl = 1;
                else if (_gtime1 == 0)
                    dbl = 0.5;
            }
            return dbl;
        }
        private int getTimeLate(object gtime, int gHour )
        {
            int dbl = 0;
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
                        if (iHH > gHour)
                            dbl = (iHH - gHour) * 60 + iMM;
                        else if (iHH >= gHour && iMM > 0)
                            dbl = iMM == 60 ? 0 : iMM;
                    }
                }
            }
            return dbl;
        }
        private int getIdNv(string _manv)
        {
            int _idNv = 0;
            var obj = db.USERs.Where(n => n.USER_UN == _manv).Single();
            if (obj != null)
                _idNv = obj.USER_ID;
            return _idNv;
        }
        #endregion

        #region Load
        private void Load_Group()
        {
            var list = db.GROUPs.Where(n => n.GROUP_ID != 1 && n.GROUP_ID != 2).ToList();
            if (list.Count > 0)
            {
                ddlPhongBan.DataTextField = "GROUP_NAME";
                ddlPhongBan.DataValueField = "GROUP_ID";
                ddlPhongBan.DataSource = list;
                ddlPhongBan.DataBind();
                ListItem l = new ListItem("Tất cả phòng ban", "0", true);
                ddlPhongBan.Items.Insert(0, l);
                ddlPhongBan.SelectedIndex = 0;
            }
        }
        #endregion

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Save_Data(year, month, Utils.CIntDef(ddlPhongBan.SelectedValue));
            //ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }
    }
}