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
    public partial class popup_tao_bang_luong : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        BangLuongData _BangLuongData = new BangLuongData();
        DoanhThuSPData _DoanhThuSPData = new DoanhThuSPData();
        CongNoData _CongNoData = new CongNoData();
        CongNoCKSData _CongNoCKSData = new CongNoCKSData();
        UserRepo _UserRepo = new UserRepo();
        int year = 0;
        int month = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            year = Utils.CIntDef(Request.QueryString["year"]);
            month = Utils.CIntDef(Request.QueryString["month"]);
            if (!_BangLuongData.CheckExistsYearMonth(year, month,Utils.CIntDef(ddlPhongBan.SelectedValue)))
                btnDone.OnClientClick = "";
            if (!IsPostBack)
            {
                liTitle.Text = "LƯƠNG THÁNG <b style='color: #FF0000;'>" + month + "</b> NĂM <b style='color: #FF0000;'>" + year + "</b>";
                Load_Group();
            }
        }

        #region Data
        private void Save_DoanhThuSp(int _Idgroup, int _idNV,string _maNV, int _year, int _month, int _tyle)
        {
            int _monthFirst = _month;
            int _yearFirst = _year;

            if(_month == 1)
            {
                _year = _year -1;
                _month = 12;
            }
            else _month = _month - 1;

            #region doanh thu công nợ chung
            var list = _CongNoData.ListDanhThu(_Idgroup, _idNV, _year);
            foreach (var u in list)
            {
                //Lấy doanh thu
                int _doanhThuKT = 0;
                int _doanhThuKD = 0;
                switch (_month)
                {
                    case 1:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_1);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_1) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_1) : 0;
                        break;
                    case 2:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_2);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_2) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_2) : 0;
                        break;
                    case 3:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_3);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_3) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_3) : 0;
                        break;
                    case 4:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_4);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_4) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_4) : 0;
                        break;
                    case 5:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_5);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_5) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_5) : 0;
                        break;
                    case 6:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_6);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_6) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_6) : 0;
                        break;
                    case 7:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_7);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_7) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_7) : 0;
                        break;
                    case 8:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_8);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_8) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_8) : 0;
                        break;
                    case 9:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_9);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_9) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_9) : 0;
                        break;
                    case 10:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_10);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_10) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_10) : 0;
                        break;
                    case 11:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_11);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_11) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_11) : 0;
                        break;
                    case 12:
                        _doanhThuKT = Utils.CIntDef(u.PHI_DV_12);
                        _doanhThuKD = Utils.CIntDef(u.CON_NO_12) == 0 ? _doanhThuKD = Utils.CIntDef(u.PHI_DV_12) : 0;
                        break;
                }
                int _doanhThu = _Idgroup == 3 ? _doanhThuKD : _doanhThuKT;
                double _luongTheoSP = (_doanhThu * _tyle) / 100;

                if (_doanhThu != 0)
                {//Có doanh thu mới Insert
                    LUONG_DOANHTHU_SP i = new LUONG_DOANHTHU_SP();
                    i.MST = u.MST;
                    i.TEN_CTY = u.TEN_KH;
                    i.MA_NV = _maNV;
                    i.DOANH_THU = _doanhThu;
                    i.TYLE_LUONG = _tyle;
                    i.LUONG_SP = Utils.CDecDef(_luongTheoSP);
                    i.PHONGBAN = _Idgroup;
                    i.NAM = _yearFirst;
                    i.THANG = _monthFirst;
                    _DoanhThuSPData.Create(i);
                }
            }
            #endregion

            #region doanh thu công nợ cks
            var listCKS = _CongNoCKSData.ListDoanhThu(_Idgroup, _idNV, _year, _month);
            foreach (var u in listCKS)
            {
                decimal _doanhThu = Utils.CDecDef(u.CKS_PHI_DV) + Utils.CDecDef(u.PM_PHI_DV);
                decimal _luongTheoDA = (_doanhThu * _tyle) / 100;

                LUONG_DOANHTHU_SP i = new LUONG_DOANHTHU_SP();
                i.MST = u.MST;
                i.TEN_CTY = "Chữ ký số: " + u.TEN_CTY;
                i.MA_NV = _maNV;
                i.DOANH_THU = _doanhThu;
                i.TYLE_LUONG = _tyle;
                i.LUONG_DUAN_TV = _luongTheoDA;
                i.PHONGBAN = _Idgroup;
                i.NAM = _yearFirst;
                i.THANG = _monthFirst;
                _DoanhThuSPData.Create(i);
            }
            #endregion
        }
        private void Save_Data(int _year, int _month, int _phongban)
        {
            _DoanhThuSPData.RemoveByPhongBan(_year, _month, _phongban);//Xóa doanh thu có phòng ban đã chọn
            _BangLuongData.RemoveByYearMonth(_year, _month, _phongban);//Xóa bảng lương có phòng ban đã chọn
            var list = _UserRepo.GetByGroupBangLuong(_phongban);
            foreach(var u in list)
            {
                LUONG_DANHSACH i = new LUONG_DANHSACH();

                //Lưu doanh thu trước
                if (Utils.CIntDef(u.GROUP_ID, 0) == 3 || Utils.CIntDef(u.GROUP_ID, 0) == 9 || Utils.CIntDef(u.GROUP_ID, 0) == 14)
                {
                    Save_DoanhThuSp(Utils.CIntDef(u.GROUP_ID, 0), Utils.CIntDef(u.USER_ID, 0)
                        , u.USER_UN, _year, _month, Utils.CIntDef(txtTyLeLuongSP.Text));
                }
                i.BL_MANV = u.USER_UN;
                i.BL_TENNV = u.USER_NAME;
                i.BL_THANG = month;
                i.BL_NAM = year;
                i.BL_LUONG_CB = u.USER_LUONG_CB;
                i.BL_DT_SPDV = getDanhThuAll(_year, _month, u.USER_UN);
                i.BL_LUONG_SP = getLuongSPAll(_year, _month, u.USER_UN);
                i.BL_LUONG_DA_TV = getDatvAll(_year, _month, u.USER_UN);
                i.BL_PC_THUONG = null;
                i.BL_PC_KHAC = null;
                i.BL_BHXH = u.USER_BHXH;
                i.BL_BHYT = u.USER_BHYT;
                i.BL_BHTN = u.USER_BHTN;
                i.BL_KHAUTRU_KHAC = null;
                i.BL_THUNHAP_TRUOCTHUE = null;
                i.BL_TAMUNG = null;
                i.BL_LUONG_THUCNHAN = null;
                i.BL_PHONGBAN = u.GROUP_ID;
                _BangLuongData.Create(i);
            }
        }
        #endregion

        #region  Funtion
        private int getIdNv(string _manv)
        {
            int _idNv = 0;
            var obj = db.USERs.Where(n => n.USER_UN == _manv).Single();
            if (obj != null)
                _idNv = obj.USER_ID;
            return _idNv;
        }
        private decimal getDanhThuAll(int _year, int _month, string _manv)
        {
            decimal _dtCongNoChung = 0;
            decimal _dtTong = 0;

            _dtCongNoChung = _DoanhThuSPData.getDoanhThu(_year, _month, _manv);
            _dtTong = _dtCongNoChung;

            return _dtTong;
        }
        private decimal getLuongSPAll(int _year, int _month, string _manv)
        {
            decimal _dtCongNoChung = 0;
            decimal _dtTong = 0;

            _dtCongNoChung = _DoanhThuSPData.getLuongSP(_year, _month, _manv);
            _dtTong = _dtCongNoChung;
            return _dtTong;
        }
        private decimal getDatvAll(int _year, int _month, string _manv)
        {
            decimal _dtCongNoChung = 0;
            decimal _dtTong = 0;

            _dtCongNoChung = _DoanhThuSPData.getLuongDATV(_year, _month, _manv);
            _dtTong = _dtCongNoChung;
            return _dtTong;
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