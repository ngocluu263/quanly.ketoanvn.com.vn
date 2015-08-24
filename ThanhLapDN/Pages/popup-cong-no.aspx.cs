using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;
using System.Data;

namespace ThanhLapDN.Pages
{
    public partial class popup_cong_no : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        CongNoData proj_data = new CongNoData();
        UnitData unit_data = new UnitData();
        NhaCungCapData _NhaCungCapData = new NhaCungCapData();
        int id = 0;
        int namCN = 0;
        string mst = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"]);
            namCN = Utils.CIntDef(Request.QueryString["nam"]);
            mst = Utils.CStrDef(Request.QueryString["mst"]).Trim();

            TestPermission();

            if (!IsPostBack)
            {
                Load_LoaiCKS();
                Load_city();
                Load_distric(-1);

                //Load_NhanVien(3, ddlNVKD);//nhân viên kinh doanh
                Load_NhanVienKD(ddlNVKD);
                Load_NhanVienGN(6, ddlNVGN);//nhân viên giao nhận
                Load_NhanVienKTQL(9, 10, ddlNVKT);//nhân viên kế toán

                //GetYear
                ddlNam.SelectedValue = Utils.CStrDef(namCN,"2015");
                ddlNam_SelectedIndexChanged(sender, e);//Chọn năm công nợ
                ddlTinhTrang_SelectedIndexChanged(sender, e);
                chkCoGiuCKS_CheckedChanged(sender, e);
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Data
        private void Load_LoaiCKS()
        {
            var obj = _NhaCungCapData.GetListByType("", 0);
            if (obj.Count > 0)
            {
                ddlLoaiCKS.DataValueField = "NCC_MA";
                ddlLoaiCKS.DataTextField = "NCC_MA";
                ddlLoaiCKS.DataSource = obj;
                ddlLoaiCKS.DataBind();
                ListItem l = new ListItem("---Chọn loại---", "0", true);
                ddlLoaiCKS.Items.Insert(0, l);
                ddlLoaiCKS.SelectedIndex = 0;
            }
        }
        private void Load_NhanVien(int _idGroup, DropDownList _ddl)
        {
            try
            {
                var list = db.USERs.Where(n => n.GROUP_ID == _idGroup).ToList();
                if (list.Count > 0)
                {
                    _ddl.DataTextField = "USER_NAME";
                    _ddl.DataValueField = "USER_ID";
                    _ddl.DataSource = list;
                    _ddl.DataBind();
                    ListItem l = new ListItem("---Chọn nhân viên---", "0", true);
                    _ddl.Items.Insert(0, l);
                    _ddl.SelectedValue = "0";
                    ListItem l1 = new ListItem("Công ty", "9999", true);
                    _ddl.Items.Insert(1, l1);
                }
            }
            catch (Exception) { }
        }
        private void Load_NhanVienKD(DropDownList _ddl)
        {
            try
            {
                var list = db.USERs.Where(n => n.GROUP_ID == 3 || n.GROUP_ID == 9 || n.GROUP_ID == 14 || n.USER_ID == 34).ToList();
                if (list.Count > 0)
                {
                    _ddl.DataTextField = "USER_NAME";
                    _ddl.DataValueField = "USER_ID";
                    _ddl.DataSource = list;
                    _ddl.DataBind();
                    ListItem l = new ListItem("---Chọn nhân viên---", "0", true);
                    _ddl.Items.Insert(0, l);
                    _ddl.SelectedValue = "0";
                    ListItem l1 = new ListItem("Công ty", "9999", true);
                    _ddl.Items.Insert(1, l1);
                }
            }
            catch (Exception) { }
        }
        private void Load_NhanVienGN(int _idGroup, DropDownList _ddl)
        {
            try
            {
                var list = db.USERs.Where(n => n.GROUP_ID == _idGroup).ToList();
                if (list.Count > 0)
                {
                    _ddl.DataTextField = "USER_NAME";
                    _ddl.DataValueField = "USER_ID";
                    _ddl.DataSource = list;
                    _ddl.DataBind();
                    ListItem l = new ListItem("---Chọn nhân viên---", "0", true);
                    _ddl.Items.Insert(0, l);
                    _ddl.SelectedValue = "0";
                }
            }
            catch (Exception) { }
        }
        private void Load_NhanVienKTQL(int _idGroupKT, int _idGroupQL, DropDownList _ddl)
        {
            try
            {
                var list = db.USERs.Where(n => n.GROUP_ID == _idGroupKT || n.GROUP_ID == _idGroupQL || n.GROUP_ID == 14).ToList();
                if (list.Count > 0)
                {
                    _ddl.DataTextField = "USER_NAME";
                    _ddl.DataValueField = "USER_ID";
                    _ddl.DataSource = list;
                    _ddl.DataBind();
                    ListItem l = new ListItem("---Chọn nhân viên---", "0", true);
                    _ddl.Items.Insert(0, l);
                    _ddl.SelectedValue = "0";
                }
            }
            catch (Exception) { }
        }
        private void Load_Data(int year)
        {
            if (id > 0)
            {
                var i = proj_data.GetListMSTYear(mst, year);
                if (i != null)
                {
                    #region Load dữ liệu nếu MST và Năm tồn tại
                    ddlTinhTrang.SelectedValue = i[0].TINH_TRANG;
                    txtNgayBatDau.Text = i[0].DATE_TINHTRANG == null ? "" : i[0].DATE_TINHTRANG.Value.ToString("dd/MM/yyyy");
                    txtNgayKetThuc.Text = i[0].DATE_TINHTRANG1 == null ? "" : i[0].DATE_TINHTRANG1.Value.ToString("dd/MM/yyyy");
                    txtNgayThanhLap.Text = i[0].DATE_THANHLAP == null ? "" : i[0].DATE_THANHLAP.Value.ToString("dd/MM/yyyy");
                    txtTenKH.Text = i[0].TEN_KH;
                    ddlCity.SelectedValue = Utils.CStrDef(i[0].QL_THUE_CITY, "0");
                    Load_distric(Utils.CIntDef(i[0].QL_THUE_CITY));
                    ddlDist.SelectedValue = Utils.CStrDef(i[0].QL_THUE_DIST, "0");
                    txtSTT.Text = Utils.CStrDef(i[0].STT);
                    liNam.Text = (Utils.CIntDef(ddlNam.SelectedValue) - 1) + "";
                    txtNoNamTruoc.Text = i[0].NO_NAM_TRUOC == null ? "" : i[0].NO_NAM_TRUOC.Value.ToString("###,##0").Replace(".",",");
                    txtMST.Text = i[0].MST;
                    txtDiaChi1.Text = i[0].DIA_CHI;
                    txtGiamDoc.Text = i[0].GIAM_DOC;
                    txtDienThoai.Text = i[0].DIEN_THOAI;
                    txtEmail.Text = i[0].EMAIL;
                    txtPhi.Text = i[0].PHI == null ? "" : i[0].PHI.Value.ToString("###,##0").Replace(".",",");
                    txtNgayThu.Text = i[0].THANG_BD_THU;
                    ddlNVKT.SelectedValue = Utils.CStrDef(i[0].NV_KT, "0");
                    txtNgayKyHD.Text = i[0].NGAY_KY_HD == null ? "" : i[0].NGAY_KY_HD.Value.ToString("dd/MM/yyyy");
                    ddlLoaiCKS.SelectedValue = i[0].LOAI_CKS;
                    txtNgayHetHanCKS.Text = i[0].NGAY_HET_HAN_CKS == null ? "" : i[0].NGAY_HET_HAN_CKS.Value.ToString("dd/MM/yyyy");
                    chkCoGiuCKS.Checked = true ? i[0].GIU_CKS == 1 : i[0].GIU_CKS == 0;
                    txtNgayLayCKS.Text = i[0].NGAY_GIU_CKS == null ? "" : i[0].NGAY_GIU_CKS.Value.ToString("dd/MM/yyyy");
                    txtGhiChuMain.Text = i[0].GHI_CHU;

                    txtPhiDV01.Text = i[0].PHI_DV_1 == null ? "" : i[0].PHI_DV_1.Value.ToString("###,##0").Replace(".",",");
                    txtSL01.Text = Utils.CStrDef(i[0].SOLUONG_T1);
                    chkBPCD01.Checked = i[0].SOLUONG_T1 == 0 ? true : false;
                    txtDaTT01_1.Text = i[0].DA_TT1_1 == null ? "" : i[0].DA_TT1_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT01_2.Text = i[0].DA_TT1_2 == null ? "" : i[0].DA_TT1_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT01_3.Text = i[0].DA_TT1_3 == null ? "" : i[0].DA_TT1_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT01_4.Text = i[0].DA_TT1_4 == null ? "" : i[0].DA_TT1_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT01.Text = i[0].NGAY_TT_1;
                    txtConNo01.Text = i[0].CON_NO_1 == null ? "" : i[0].CON_NO_1.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV02.Text = i[0].PHI_DV_2 == null ? "" : i[0].PHI_DV_2.Value.ToString("###,##0").Replace(".",",");
                    txtSL02.Text = Utils.CStrDef(i[0].SOLUONG_T2);
                    chkBPCD02.Checked = i[0].SOLUONG_T2 == 0 ? true : false;
                    txtDaTT02_1.Text = i[0].DA_TT2_1 == null ? "" : i[0].DA_TT2_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT02_2.Text = i[0].DA_TT2_2 == null ? "" : i[0].DA_TT2_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT02_3.Text = i[0].DA_TT2_3 == null ? "" : i[0].DA_TT2_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT02_4.Text = i[0].DA_TT2_4 == null ? "" : i[0].DA_TT2_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT02.Text = i[0].NGAY_TT_2;
                    txtConNo02.Text = i[0].CON_NO_2 == null ? "" : i[0].CON_NO_2.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV03.Text = i[0].PHI_DV_3 == null ? "" : i[0].PHI_DV_3.Value.ToString("###,##0").Replace(".",",");
                    txtSL03.Text = Utils.CStrDef(i[0].SOLUONG_T3);
                    chkBPCD03.Checked = i[0].SOLUONG_T3 == 0 ? true : false;
                    txtDaTT03_1.Text = i[0].DA_TT3_1 == null ? "" : i[0].DA_TT3_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT03_2.Text = i[0].DA_TT3_2 == null ? "" : i[0].DA_TT3_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT03_3.Text = i[0].DA_TT3_3 == null ? "" : i[0].DA_TT3_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT03_4.Text = i[0].DA_TT3_4 == null ? "" : i[0].DA_TT3_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT03.Text = i[0].NGAY_TT_3;
                    txtConNo03.Text = i[0].CON_NO_3 == null ? "" : i[0].CON_NO_3.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV04.Text = i[0].PHI_DV_4 == null ? "" : i[0].PHI_DV_4.Value.ToString("###,##0").Replace(".",",");
                    txtSL04.Text = Utils.CStrDef(i[0].SOLUONG_T4);
                    chkBPCD04.Checked = i[0].SOLUONG_T4 == 0 ? true : false;
                    txtDaTT04_1.Text = i[0].DA_TT4_1 == null ? "" : i[0].DA_TT4_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT04_2.Text = i[0].DA_TT4_2 == null ? "" : i[0].DA_TT4_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT04_3.Text = i[0].DA_TT4_3 == null ? "" : i[0].DA_TT4_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT04_4.Text = i[0].DA_TT4_4 == null ? "" : i[0].DA_TT4_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT04.Text = i[0].NGAY_TT_4;
                    txtConNo04.Text = i[0].CON_NO_4 == null ? "" : i[0].CON_NO_4.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV05.Text = i[0].PHI_DV_5 == null ? "" : i[0].PHI_DV_5.Value.ToString("###,##0").Replace(".",",");
                    txtSL05.Text = Utils.CStrDef(i[0].SOLUONG_T5);
                    chkBPCD05.Checked = i[0].SOLUONG_T5 == 0 ? true : false;
                    txtDaTT05_1.Text = i[0].DA_TT5_1 == null ? "" : i[0].DA_TT5_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT05_2.Text = i[0].DA_TT5_2 == null ? "" : i[0].DA_TT5_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT05_3.Text = i[0].DA_TT5_3 == null ? "" : i[0].DA_TT5_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT05_4.Text = i[0].DA_TT5_4 == null ? "" : i[0].DA_TT5_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT05.Text = i[0].NGAY_TT_5;
                    txtConNo05.Text = i[0].CON_NO_5 == null ? "" : i[0].CON_NO_5.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV06.Text = i[0].PHI_DV_6 == null ? "" : i[0].PHI_DV_6.Value.ToString("###,##0").Replace(".",",");
                    txtSL06.Text = Utils.CStrDef(i[0].SOLUONG_T6);
                    chkBPCD06.Checked = i[0].SOLUONG_T6 == 0 ? true : false;
                    txtDaTT06_1.Text = i[0].DA_TT6_1 == null ? "" : i[0].DA_TT6_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT06_2.Text = i[0].DA_TT6_2 == null ? "" : i[0].DA_TT6_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT06_3.Text = i[0].DA_TT6_3 == null ? "" : i[0].DA_TT6_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT06_4.Text = i[0].DA_TT6_4 == null ? "" : i[0].DA_TT6_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT06.Text = i[0].NGAY_TT_6;
                    txtConNo06.Text = i[0].CON_NO_6 == null ? "" : i[0].CON_NO_6.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV07.Text = i[0].PHI_DV_7 == null ? "" : i[0].PHI_DV_7.Value.ToString("###,##0").Replace(".",",");
                    txtSL07.Text = Utils.CStrDef(i[0].SOLUONG_T7);
                    chkBPCD07.Checked = i[0].SOLUONG_T7 == 0 ? true : false;
                    txtDaTT07_1.Text = i[0].DA_TT7_1 == null ? "" : i[0].DA_TT7_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT07_2.Text = i[0].DA_TT7_2 == null ? "" : i[0].DA_TT7_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT07_3.Text = i[0].DA_TT7_3 == null ? "" : i[0].DA_TT7_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT07_4.Text = i[0].DA_TT7_4 == null ? "" : i[0].DA_TT7_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT07.Text = i[0].NGAY_TT_7;
                    txtConNo07.Text = i[0].CON_NO_7 == null ? "" : i[0].CON_NO_7.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV08.Text = i[0].PHI_DV_8 == null ? "" : i[0].PHI_DV_8.Value.ToString("###,##0").Replace(".",",");
                    txtSL08.Text = Utils.CStrDef(i[0].SOLUONG_T8);
                    chkBPCD08.Checked = i[0].SOLUONG_T8 == 0 ? true : false;
                    txtDaTT08_1.Text = i[0].DA_TT8_1 == null ? "" : i[0].DA_TT8_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT08_2.Text = i[0].DA_TT8_2 == null ? "" : i[0].DA_TT8_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT08_3.Text = i[0].DA_TT8_3 == null ? "" : i[0].DA_TT8_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT08_4.Text = i[0].DA_TT8_4 == null ? "" : i[0].DA_TT8_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT08.Text = i[0].NGAY_TT_8;
                    txtConNo08.Text = i[0].CON_NO_8 == null ? "" : i[0].CON_NO_8.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV09.Text = i[0].PHI_DV_9 == null ? "" : i[0].PHI_DV_9.Value.ToString("###,##0").Replace(".",",");
                    txtSL09.Text = Utils.CStrDef(i[0].SOLUONG_T9);
                    chkBPCD09.Checked = i[0].SOLUONG_T9 == 0 ? true : false;
                    txtDaTT09_1.Text = i[0].DA_TT9_1 == null ? "" : i[0].DA_TT9_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT09_2.Text = i[0].DA_TT9_2 == null ? "" : i[0].DA_TT9_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT09_3.Text = i[0].DA_TT9_3 == null ? "" : i[0].DA_TT9_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT09_4.Text = i[0].DA_TT9_4 == null ? "" : i[0].DA_TT9_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT09.Text = i[0].NGAY_TT_9;
                    txtConNo09.Text = i[0].CON_NO_9 == null ? "" : i[0].CON_NO_9.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV10.Text = i[0].PHI_DV_10 == null ? "" : i[0].PHI_DV_10.Value.ToString("###,##0").Replace(".",",");
                    txtSL10.Text = Utils.CStrDef(i[0].SOLUONG_T10);
                    chkBPCD10.Checked = i[0].SOLUONG_T10 == 0 ? true : false;
                    txtDaTT10_1.Text = i[0].DA_TT10_1 == null ? "" : i[0].DA_TT10_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT10_2.Text = i[0].DA_TT10_2 == null ? "" : i[0].DA_TT10_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT10_3.Text = i[0].DA_TT10_3 == null ? "" : i[0].DA_TT10_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT10_4.Text = i[0].DA_TT10_4 == null ? "" : i[0].DA_TT10_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT10.Text = i[0].NGAY_TT_10;
                    txtConNo10.Text = i[0].CON_NO_10 == null ? "" : i[0].CON_NO_10.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV11.Text = i[0].PHI_DV_11 == null ? "" : i[0].PHI_DV_11.Value.ToString("###,##0").Replace(".",",");
                    txtSL11.Text = Utils.CStrDef(i[0].SOLUONG_T11);
                    chkBPCD11.Checked = i[0].SOLUONG_T11 == 0 ? true : false;
                    txtDaTT11_1.Text = i[0].DA_TT11_1 == null ? "" : i[0].DA_TT11_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT11_2.Text = i[0].DA_TT11_2 == null ? "" : i[0].DA_TT11_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT11_3.Text = i[0].DA_TT11_3 == null ? "" : i[0].DA_TT11_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT11_4.Text = i[0].DA_TT11_4 == null ? "" : i[0].DA_TT11_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT11.Text = i[0].NGAY_TT_11;
                    txtConNo11.Text = i[0].CON_NO_11 == null ? "" : i[0].CON_NO_11.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV12.Text = i[0].PHI_DV_12 == null ? "" : i[0].PHI_DV_12.Value.ToString("###,##0").Replace(".",",");
                    txtSL12.Text = Utils.CStrDef(i[0].SOLUONG_T12);
                    chkBPCD12.Checked = i[0].SOLUONG_T12 == 0 ? true : false;
                    txtDaTT12_1.Text = i[0].DA_TT12_1 == null ? "" : i[0].DA_TT12_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT12_2.Text = i[0].DA_TT12_2 == null ? "" : i[0].DA_TT12_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT12_3.Text = i[0].DA_TT12_3 == null ? "" : i[0].DA_TT12_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT12_4.Text = i[0].DA_TT12_4 == null ? "" : i[0].DA_TT12_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT12.Text = i[0].NGAY_TT_12;
                    txtConNo12.Text = i[0].CON_NO_12 == null ? "" : i[0].CON_NO_12.Value.ToString("###,##0").Replace(".",",");

                    txtPhiDV13.Text = i[0].PHI_DV_BCTC == null ? "" : i[0].PHI_DV_BCTC.Value.ToString("###,##0").Replace(".",",");
                    txtSL13.Text = Utils.CStrDef(i[0].SOLUONG_T13);
                    chkBPCD13.Checked = i[0].SOLUONG_T13 == 0 ? true : false;
                    txtDaTT13_1.Text = i[0].DA_TT13_1 == null ? "" : i[0].DA_TT13_1.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT13_2.Text = i[0].DA_TT13_2 == null ? "" : i[0].DA_TT13_2.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT13_3.Text = i[0].DA_TT13_3 == null ? "" : i[0].DA_TT13_3.Value.ToString("###,##0").Replace(".",",");
                    txtDaTT13_4.Text = i[0].DA_TT13_4 == null ? "" : i[0].DA_TT13_4.Value.ToString("###,##0").Replace(".",",");
                    txtNgayTT13.Text = i[0].NGAY_TT_BCTC;
                    txtConNo13.Text = i[0].CON_NO_BCTC == null ? "" : i[0].CON_NO_BCTC.Value.ToString("###,##0").Replace(".",",");

                    txtTongNo.Text = i[0].TONG_NO == null ? "" : i[0].TONG_NO.Value.ToString("###,##0").Replace(".",",");
                    ddlNVKD.SelectedValue = Utils.CStrDef(i[0].NV_KD, "0");
                    ddlNVGN.SelectedValue = Utils.CStrDef(i[0].NV_GN, "0");

                    txtBP1_SL.Text = i[0].BIEUPHI1_SL;
                    txtBP1_PHI.Text = i[0].BIEUPHI1_PHI == null ? "" : i[0].BIEUPHI1_PHI.Value.ToString("###,##0").Replace(".",",");
                    txtBP2_SL.Text = i[0].BIEUPHI2_SL;
                    txtBP2_PHI.Text = i[0].BIEUPHI2_PHI == null ? "" : i[0].BIEUPHI2_PHI.Value.ToString("###,##0").Replace(".",",");
                    txtBP3_SL.Text = i[0].BIEUPHI3_SL;
                    txtBP3_PHI.Text = i[0].BIEUPHI3_PHI == null ? "" : i[0].BIEUPHI3_PHI.Value.ToString("###,##0").Replace(".",",");
                    txtBP4_SL.Text = i[0].BIEUPHI4_SL;
                    txtBP4_PHI.Text = i[0].BIEUPHI4_PHI == null ? "" : i[0].BIEUPHI4_PHI.Value.ToString("###,##0").Replace(".",",");
                    txtBP5_SL.Text = i[0].BIEUPHI5_SL;
                    txtBP5_PHI.Text = i[0].BIEUPHI5_PHI == null ? "" : i[0].BIEUPHI5_PHI.Value.ToString("###,##0").Replace(".",",");
                    txtBP6_SL.Text = i[0].BIEUPHI6_SL;
                    txtBP6_PHI.Text = i[0].BIEUPHI6_PHI == null ? "" : i[0].BIEUPHI6_PHI.Value.ToString("###,##0").Replace(".",",");

                    txtPhiPhatSinh.Text = i[0].BIEUPHI_THEM == null ? "" : i[0].BIEUPHI_THEM.Value.ToString("###,##0").Replace(".",",");

                    txtGhiChu01.Text = i[0].GHI_CHU1;
                    #endregion
                }
                else
                {
                    #region MST và Năm không tồn tại, lúc này lấy ID
                    var obj = proj_data.GetById(id);
                    ddlTinhTrang.SelectedValue = obj.TINH_TRANG;
                    txtNgayBatDau.Text = obj.DATE_TINHTRANG == null ? "" : obj.DATE_TINHTRANG.Value.ToString("dd/MM/yyyy");
                    txtNgayKetThuc.Text = obj.DATE_TINHTRANG1 == null ? "" : obj.DATE_TINHTRANG1.Value.ToString("dd/MM/yyyy");
                    txtNgayThanhLap.Text = obj.DATE_THANHLAP == null ? "" : obj.DATE_THANHLAP.Value.ToString("dd/MM/yyyy");
                    txtTenKH.Text = obj.TEN_KH;
                    ddlCity.SelectedValue = Utils.CStrDef(obj.QL_THUE_CITY, "0");
                    Load_distric(Utils.CIntDef(obj.QL_THUE_CITY));
                    ddlDist.SelectedValue = Utils.CStrDef(obj.QL_THUE_DIST, "0");
                    txtSTT.Text = Utils.CStrDef(obj.STT);
                    liNam.Text = (Utils.CIntDef(ddlNam.SelectedValue) - 1) + "";
                    txtNoNamTruoc.Text = obj.NO_NAM_TRUOC == null ? "" : obj.NO_NAM_TRUOC.Value.ToString("###,##0").Replace(".",",");
                    txtMST.Text = obj.MST;
                    txtDiaChi1.Text = obj.DIA_CHI;
                    txtGiamDoc.Text = obj.GIAM_DOC;
                    txtDienThoai.Text = obj.DIEN_THOAI;
                    txtEmail.Text = obj.EMAIL;
                    txtPhi.Text = obj.PHI == null ? "" : obj.PHI.Value.ToString("###,##0").Replace(".",",");
                    txtNgayThu.Text = obj.THANG_BD_THU;
                    ddlNVKT.SelectedValue = Utils.CStrDef(obj.NV_KT, "0");
                    ddlNVKD.SelectedValue = Utils.CStrDef(obj.NV_KD, "0");
                    ddlNVGN.SelectedValue = Utils.CStrDef(obj.NV_GN, "0");
                    txtNgayKyHD.Text = obj.NGAY_KY_HD == null ? "" : obj.NGAY_KY_HD.Value.ToString("dd/MM/yyyy");
                    ddlLoaiCKS.SelectedValue = obj.LOAI_CKS;
                    txtNgayHetHanCKS.Text = obj.NGAY_HET_HAN_CKS == null ? "" : obj.NGAY_HET_HAN_CKS.Value.ToString("dd/MM/yyyy");
                    chkCoGiuCKS.Checked = true ? obj.GIU_CKS == 1 : obj.GIU_CKS == 0;
                    txtNgayLayCKS.Text = obj.NGAY_GIU_CKS == null ? "" : obj.NGAY_GIU_CKS.Value.ToString("dd/MM/yyyy");
                    txtGhiChuMain.Text = obj.GHI_CHU;

                    //Tắt chọn năm khi chưa nhập MST
                    ddlNam.Enabled = obj.MST != null && obj.MST != "" ? true : false;

                    if (year == obj.NAM)
                    {
                        #region Trường hợp năm lấy từ ID = năm combobox
                        txtPhiDV01.Text = obj.PHI_DV_1 == null ? "" : obj.PHI_DV_1.Value.ToString("###,##0").Replace(".",",");
                        txtSL01.Text = Utils.CStrDef(obj.SOLUONG_T1);
                        chkBPCD01.Checked = obj.SOLUONG_T1 == 0 ? true : false;
                        txtDaTT01_1.Text = obj.DA_TT1_1 == null ? "" : obj.DA_TT1_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT01_2.Text = obj.DA_TT1_2 == null ? "" : obj.DA_TT1_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT01_3.Text = obj.DA_TT1_3 == null ? "" : obj.DA_TT1_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT01_4.Text = obj.DA_TT1_4 == null ? "" : obj.DA_TT1_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT01.Text = obj.NGAY_TT_1;
                        txtConNo01.Text = obj.CON_NO_1 == null ? "" : obj.CON_NO_1.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV02.Text = obj.PHI_DV_2 == null ? "" : obj.PHI_DV_2.Value.ToString("###,##0").Replace(".",",");
                        txtSL02.Text = Utils.CStrDef(obj.SOLUONG_T2);
                        chkBPCD02.Checked = obj.SOLUONG_T2 == 0 ? true : false;
                        txtDaTT02_1.Text = obj.DA_TT2_1 == null ? "" : obj.DA_TT2_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT02_2.Text = obj.DA_TT2_2 == null ? "" : obj.DA_TT2_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT02_3.Text = obj.DA_TT2_3 == null ? "" : obj.DA_TT2_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT02_4.Text = obj.DA_TT2_4 == null ? "" : obj.DA_TT2_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT02.Text = obj.NGAY_TT_2;
                        txtConNo02.Text = obj.CON_NO_2 == null ? "" : obj.CON_NO_2.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV03.Text = obj.PHI_DV_3 == null ? "" : obj.PHI_DV_3.Value.ToString("###,##0").Replace(".",",");
                        txtSL03.Text = Utils.CStrDef(obj.SOLUONG_T3);
                        chkBPCD03.Checked = obj.SOLUONG_T3 == 0 ? true : false;
                        txtDaTT03_1.Text = obj.DA_TT3_1 == null ? "" : obj.DA_TT3_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT03_2.Text = obj.DA_TT3_2 == null ? "" : obj.DA_TT3_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT03_3.Text = obj.DA_TT3_3 == null ? "" : obj.DA_TT3_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT03_4.Text = obj.DA_TT3_4 == null ? "" : obj.DA_TT3_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT03.Text = obj.NGAY_TT_3;
                        txtConNo03.Text = obj.CON_NO_3 == null ? "" : obj.CON_NO_3.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV04.Text = obj.PHI_DV_4 == null ? "" : obj.PHI_DV_4.Value.ToString("###,##0").Replace(".",",");
                        txtSL04.Text = Utils.CStrDef(obj.SOLUONG_T4);
                        chkBPCD04.Checked = obj.SOLUONG_T4 == 0 ? true : false;
                        txtDaTT04_1.Text = obj.DA_TT4_1 == null ? "" : obj.DA_TT4_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT04_2.Text = obj.DA_TT4_2 == null ? "" : obj.DA_TT4_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT04_3.Text = obj.DA_TT4_3 == null ? "" : obj.DA_TT4_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT04_4.Text = obj.DA_TT4_4 == null ? "" : obj.DA_TT4_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT04.Text = obj.NGAY_TT_4;
                        txtConNo04.Text = obj.CON_NO_4 == null ? "" : obj.CON_NO_4.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV05.Text = obj.PHI_DV_5 == null ? "" : obj.PHI_DV_5.Value.ToString("###,##0").Replace(".",",");
                        txtSL05.Text = Utils.CStrDef(obj.SOLUONG_T5);
                        chkBPCD05.Checked = obj.SOLUONG_T5 == 0 ? true : false;
                        txtDaTT05_1.Text = obj.DA_TT5_1 == null ? "" : obj.DA_TT5_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT05_2.Text = obj.DA_TT5_2 == null ? "" : obj.DA_TT5_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT05_3.Text = obj.DA_TT5_3 == null ? "" : obj.DA_TT5_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT05_4.Text = obj.DA_TT5_4 == null ? "" : obj.DA_TT5_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT05.Text = obj.NGAY_TT_5;
                        txtConNo05.Text = obj.CON_NO_5 == null ? "" : obj.CON_NO_5.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV06.Text = obj.PHI_DV_6 == null ? "" : obj.PHI_DV_6.Value.ToString("###,##0").Replace(".",",");
                        txtSL06.Text = Utils.CStrDef(obj.SOLUONG_T6);
                        chkBPCD06.Checked = obj.SOLUONG_T6 == 0 ? true : false;
                        txtDaTT06_1.Text = obj.DA_TT6_1 == null ? "" : obj.DA_TT6_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT06_2.Text = obj.DA_TT6_2 == null ? "" : obj.DA_TT6_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT06_3.Text = obj.DA_TT6_3 == null ? "" : obj.DA_TT6_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT06_4.Text = obj.DA_TT6_4 == null ? "" : obj.DA_TT6_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT06.Text = obj.NGAY_TT_6;
                        txtConNo06.Text = obj.CON_NO_6 == null ? "" : obj.CON_NO_6.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV07.Text = obj.PHI_DV_7 == null ? "" : obj.PHI_DV_7.Value.ToString("###,##0").Replace(".",",");
                        txtSL07.Text = Utils.CStrDef(obj.SOLUONG_T7);
                        chkBPCD07.Checked = obj.SOLUONG_T7 == 0 ? true : false;
                        txtDaTT07_1.Text = obj.DA_TT7_1 == null ? "" : obj.DA_TT7_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT07_2.Text = obj.DA_TT7_2 == null ? "" : obj.DA_TT7_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT07_3.Text = obj.DA_TT7_3 == null ? "" : obj.DA_TT7_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT07_4.Text = obj.DA_TT7_4 == null ? "" : obj.DA_TT7_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT07.Text = obj.NGAY_TT_7;
                        txtConNo07.Text = obj.CON_NO_7 == null ? "" : obj.CON_NO_7.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV08.Text = obj.PHI_DV_8 == null ? "" : obj.PHI_DV_8.Value.ToString("###,##0").Replace(".",",");
                        txtSL08.Text = Utils.CStrDef(obj.SOLUONG_T8);
                        chkBPCD08.Checked = obj.SOLUONG_T8 == 0 ? true : false;
                        txtDaTT08_1.Text = obj.DA_TT8_1 == null ? "" : obj.DA_TT8_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT08_2.Text = obj.DA_TT8_2 == null ? "" : obj.DA_TT8_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT08_3.Text = obj.DA_TT8_3 == null ? "" : obj.DA_TT8_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT08_4.Text = obj.DA_TT8_4 == null ? "" : obj.DA_TT8_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT08.Text = obj.NGAY_TT_8;
                        txtConNo08.Text = obj.CON_NO_8 == null ? "" : obj.CON_NO_8.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV09.Text = obj.PHI_DV_9 == null ? "" : obj.PHI_DV_9.Value.ToString("###,##0").Replace(".",",");
                        txtSL09.Text = Utils.CStrDef(obj.SOLUONG_T9);
                        chkBPCD09.Checked = obj.SOLUONG_T9 == 0 ? true : false;
                        txtDaTT09_1.Text = obj.DA_TT9_1 == null ? "" : obj.DA_TT9_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT09_2.Text = obj.DA_TT9_2 == null ? "" : obj.DA_TT9_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT09_3.Text = obj.DA_TT9_3 == null ? "" : obj.DA_TT9_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT09_4.Text = obj.DA_TT9_4 == null ? "" : obj.DA_TT9_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT09.Text = obj.NGAY_TT_9;
                        txtConNo09.Text = obj.CON_NO_9 == null ? "" : obj.CON_NO_9.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV10.Text = obj.PHI_DV_10 == null ? "" : obj.PHI_DV_10.Value.ToString("###,##0").Replace(".",",");
                        txtSL10.Text = Utils.CStrDef(obj.SOLUONG_T10);
                        chkBPCD10.Checked = obj.SOLUONG_T10 == 0 ? true : false;
                        txtDaTT10_1.Text = obj.DA_TT10_1 == null ? "" : obj.DA_TT10_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT10_2.Text = obj.DA_TT10_2 == null ? "" : obj.DA_TT10_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT10_3.Text = obj.DA_TT10_3 == null ? "" : obj.DA_TT10_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT10_4.Text = obj.DA_TT10_4 == null ? "" : obj.DA_TT10_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT10.Text = obj.NGAY_TT_10;
                        txtConNo10.Text = obj.CON_NO_10 == null ? "" : obj.CON_NO_10.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV11.Text = obj.PHI_DV_11 == null ? "" : obj.PHI_DV_11.Value.ToString("###,##0").Replace(".",",");
                        txtSL11.Text = Utils.CStrDef(obj.SOLUONG_T11);
                        chkBPCD11.Checked = obj.SOLUONG_T11 == 0 ? true : false;
                        txtDaTT11_1.Text = obj.DA_TT11_1 == null ? "" : obj.DA_TT11_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT11_2.Text = obj.DA_TT11_2 == null ? "" : obj.DA_TT11_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT11_3.Text = obj.DA_TT11_3 == null ? "" : obj.DA_TT11_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT11_4.Text = obj.DA_TT11_4 == null ? "" : obj.DA_TT11_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT11.Text = obj.NGAY_TT_11;
                        txtConNo11.Text = obj.CON_NO_11 == null ? "" : obj.CON_NO_11.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV12.Text = obj.PHI_DV_12 == null ? "" : obj.PHI_DV_12.Value.ToString("###,##0").Replace(".",",");
                        txtSL12.Text = Utils.CStrDef(obj.SOLUONG_T12);
                        chkBPCD12.Checked = obj.SOLUONG_T12 == 0 ? true : false;
                        txtDaTT12_1.Text = obj.DA_TT12_1 == null ? "" : obj.DA_TT12_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT12_2.Text = obj.DA_TT12_2 == null ? "" : obj.DA_TT12_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT12_3.Text = obj.DA_TT12_3 == null ? "" : obj.DA_TT12_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT12_4.Text = obj.DA_TT12_4 == null ? "" : obj.DA_TT12_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT12.Text = obj.NGAY_TT_12;
                        txtConNo12.Text = obj.CON_NO_12 == null ? "" : obj.CON_NO_12.Value.ToString("###,##0").Replace(".",",");

                        txtPhiDV13.Text = obj.PHI_DV_BCTC == null ? "" : obj.PHI_DV_BCTC.Value.ToString("###,##0").Replace(".",",");
                        txtSL13.Text = Utils.CStrDef(obj.SOLUONG_T13);
                        chkBPCD13.Checked = obj.SOLUONG_T13 == 0 ? true : false;
                        txtDaTT13_1.Text = obj.DA_TT13_1 == null ? "" : obj.DA_TT13_1.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT13_2.Text = obj.DA_TT13_2 == null ? "" : obj.DA_TT13_2.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT13_3.Text = obj.DA_TT13_3 == null ? "" : obj.DA_TT13_3.Value.ToString("###,##0").Replace(".",",");
                        txtDaTT13_4.Text = obj.DA_TT13_4 == null ? "" : obj.DA_TT13_4.Value.ToString("###,##0").Replace(".",",");
                        txtNgayTT13.Text = obj.NGAY_TT_BCTC;
                        txtConNo13.Text = obj.CON_NO_BCTC == null ? "" : obj.CON_NO_BCTC.Value.ToString("###,##0").Replace(".",",");

                        txtTongNo.Text = obj.TONG_NO == null ? "" : obj.TONG_NO.Value.ToString("###,##0").Replace(".",",");

                        txtBP1_SL.Text = obj.BIEUPHI1_SL;
                        txtBP1_PHI.Text = obj.BIEUPHI1_PHI == null ? "" : obj.BIEUPHI1_PHI.Value.ToString("###,##0").Replace(".",",");
                        txtBP2_SL.Text = obj.BIEUPHI2_SL;
                        txtBP2_PHI.Text = obj.BIEUPHI2_PHI == null ? "" : obj.BIEUPHI2_PHI.Value.ToString("###,##0").Replace(".",",");
                        txtBP3_SL.Text = obj.BIEUPHI3_SL;
                        txtBP3_PHI.Text = obj.BIEUPHI3_PHI == null ? "" : obj.BIEUPHI3_PHI.Value.ToString("###,##0").Replace(".",",");
                        txtBP4_SL.Text = obj.BIEUPHI4_SL;
                        txtBP4_PHI.Text = obj.BIEUPHI4_PHI == null ? "" : obj.BIEUPHI4_PHI.Value.ToString("###,##0").Replace(".",",");
                        txtBP5_SL.Text = obj.BIEUPHI5_SL;
                        txtBP5_PHI.Text = obj.BIEUPHI5_PHI == null ? "" : obj.BIEUPHI5_PHI.Value.ToString("###,##0").Replace(".",",");
                        txtBP6_SL.Text = obj.BIEUPHI6_SL;
                        txtBP6_PHI.Text = obj.BIEUPHI6_PHI == null ? "" : obj.BIEUPHI6_PHI.Value.ToString("###,##0").Replace(".",",");

                        txtPhiPhatSinh.Text = obj.BIEUPHI_THEM == null ? "" : obj.BIEUPHI_THEM.Value.ToString("###,##0").Replace(".",",");

                        txtGhiChu01.Text = obj.GHI_CHU1;
                        #endregion
                    }
                    else
                    {
                        #region Trường hợp năm lấy từ ID khác năm combox thì chỉ lấy thông tin phía trên
                        ClearText();
                        #endregion
                    }
                    #endregion
                }
            }
            else 
            {
                int _sttNext = proj_data.GetSTT(year) + 1;
                txtSTT.Text = _sttNext + "";
            }
        }
        private int Save_Data(int year)
        {
            var obj = proj_data.GetByMSTYear(mst, year);
            int temp = obj != null ? 1 : 0;//Nếu mst và năm chưa có thì thêm
            if (id == 0 || temp == 0)
            {
                if (id != 0 && ddlNam.Enabled == false)
                {
                    var i = db.CONG_NOs.Where(n => n.ID == id).Single();
                    i.TINH_TRANG = ddlTinhTrang.SelectedValue;
                    i.DATE_TINHTRANG = txtNgayBatDau.Text != "" ?
                        DateTime.ParseExact(txtNgayBatDau.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_TINHTRANG = null;
                    i.DATE_TINHTRANG1 = txtNgayKetThuc.Text != "" ?
                        DateTime.ParseExact(txtNgayKetThuc.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_TINHTRANG1 = null;
                    i.DATE_THANHLAP = txtNgayThanhLap.Text != "" ?
                        DateTime.ParseExact(txtNgayThanhLap.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_THANHLAP = null;
                    i.TEN_KH = txtTenKH.Text;
                    i.QL_THUE_CITY = Utils.CIntDef(ddlCity.SelectedValue);
                    i.QL_THUE_DIST = Utils.CIntDef(ddlDist.SelectedValue);
                    i.STT = Utils.CIntDef(txtSTT.Text);
                    i.NO_NAM_TRUOC = Utils.CIntDef(txtNoNamTruoc.Text.Replace(",", ""));
                    i.MST = txtMST.Text;
                    i.DIA_CHI = txtDiaChi1.Text;
                    i.GIAM_DOC = txtGiamDoc.Text;
                    i.DIEN_THOAI = txtDienThoai.Text;
                    i.EMAIL = txtEmail.Text;
                    i.PHI = Utils.CIntDef(txtPhi.Text.Replace(",", ""));
                    i.THANG_BD_THU = txtNgayThu.Text;
                    i.NV_KT = Utils.CIntDef(ddlNVKT.SelectedValue);
                    i.NV_KD = Utils.CIntDef(ddlNVKD.SelectedValue);
                    i.NV_GN = Utils.CIntDef(ddlNVGN.SelectedValue);
                    i.NGAY_KY_HD = txtNgayKyHD.Text != "" ?
                        DateTime.ParseExact(txtNgayKyHD.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_KY_HD = null;
                    i.LOAI_CKS = ddlLoaiCKS.SelectedValue;
                    i.GIU_CKS = chkCoGiuCKS.Checked ? 1 : 0;
                    i.NGAY_HET_HAN_CKS = txtNgayHetHanCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayHetHanCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_HET_HAN_CKS = null;
                    if (chkCoGiuCKS.Checked)
                    {
                        i.NGAY_GIU_CKS = txtNgayLayCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayLayCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_GIU_CKS = null;
                    }
                    else i.NGAY_GIU_CKS = null;
                    i.GHI_CHU = txtGhiChuMain.Text;
                    i.DATE = DateTime.Now;

                    i.BIEUPHI1_SL = txtBP1_SL.Text.Trim();
                    i.BIEUPHI1_PHI = Utils.CIntDef(txtBP1_PHI.Text.Replace(",", ""));

                    i.BIEUPHI2_SL = txtBP2_SL.Text.Trim();
                    i.BIEUPHI2_PHI = Utils.CIntDef(txtBP2_PHI.Text.Replace(",", ""));

                    i.BIEUPHI3_SL = txtBP3_SL.Text.Trim();
                    i.BIEUPHI3_PHI = Utils.CIntDef(txtBP3_PHI.Text.Replace(",", ""));

                    i.BIEUPHI4_SL = txtBP4_SL.Text.Trim();
                    i.BIEUPHI4_PHI = Utils.CIntDef(txtBP4_PHI.Text.Replace(",", ""));

                    i.BIEUPHI5_SL = txtBP5_SL.Text.Trim();
                    i.BIEUPHI5_PHI = Utils.CIntDef(txtBP5_PHI.Text.Replace(",", ""));

                    i.BIEUPHI6_SL = txtBP6_SL.Text.Trim();
                    i.BIEUPHI6_PHI = Utils.CIntDef(txtBP6_PHI.Text.Replace(",", ""));

                    i.BIEUPHI_THEM = Utils.CIntDef(txtPhiPhatSinh.Text.Replace(",", ""));

                    i.TONG_NO = i.CON_NO_1 + i.CON_NO_2 + i.CON_NO_3 + i.CON_NO_4 + i.CON_NO_5 + i.CON_NO_6 + i.CON_NO_7
                        + i.CON_NO_8 + i.CON_NO_9 + i.CON_NO_10 + i.CON_NO_11 + i.CON_NO_12 + i.CON_NO_BCTC + i.NO_NAM_TRUOC;

                    proj_data.Update(i);
                    db.SubmitChanges();
                }
                else
                {
                    CONG_NO i = new CONG_NO();
                    i.TINH_TRANG = ddlTinhTrang.SelectedValue;
                    i.DATE_TINHTRANG = txtNgayBatDau.Text != "" ?
                        DateTime.ParseExact(txtNgayBatDau.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_TINHTRANG = null;
                    i.DATE_TINHTRANG1 = txtNgayKetThuc.Text != "" ?
                        DateTime.ParseExact(txtNgayKetThuc.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_TINHTRANG1 = null;
                    i.DATE_THANHLAP = txtNgayThanhLap.Text != "" ?
                        DateTime.ParseExact(txtNgayThanhLap.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_THANHLAP = null;
                    i.TEN_KH = txtTenKH.Text;
                    i.QL_THUE_CITY = Utils.CIntDef(ddlCity.SelectedValue);
                    i.QL_THUE_DIST = Utils.CIntDef(ddlDist.SelectedValue);
                    i.STT = Utils.CIntDef(txtSTT.Text);
                    i.NO_NAM_TRUOC = Utils.CIntDef(txtNoNamTruoc.Text.Replace(",", ""));
                    i.MST = txtMST.Text;
                    i.DIA_CHI = txtDiaChi1.Text;
                    i.GIAM_DOC = txtGiamDoc.Text;
                    i.DIEN_THOAI = txtDienThoai.Text;
                    i.EMAIL = txtEmail.Text;
                    i.PHI = Utils.CIntDef(txtPhi.Text.Replace(",", ""));
                    i.THANG_BD_THU = txtNgayThu.Text;
                    i.NV_KT = Utils.CIntDef(ddlNVKT.SelectedValue);
                    i.NV_KD = Utils.CIntDef(ddlNVKD.SelectedValue);
                    i.NV_GN = Utils.CIntDef(ddlNVGN.SelectedValue);
                    i.NGAY_KY_HD = txtNgayKyHD.Text != "" ?
                        DateTime.ParseExact(txtNgayKyHD.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_KY_HD = null;
                    i.LOAI_CKS = ddlLoaiCKS.SelectedValue;
                    i.GIU_CKS = chkCoGiuCKS.Checked ? 1 : 0;
                    i.NGAY_HET_HAN_CKS = txtNgayHetHanCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayHetHanCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_HET_HAN_CKS = null;
                    if (chkCoGiuCKS.Checked)
                    {
                        i.NGAY_GIU_CKS = txtNgayLayCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayLayCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_GIU_CKS = null;
                    }
                    else i.NGAY_GIU_CKS = null;
                    i.GHI_CHU = txtGhiChuMain.Text;
                    i.DATE = DateTime.Now;
                    i.NAM = Utils.CIntDef(ddlNam.SelectedValue);

                    i.BIEUPHI1_SL = txtBP1_SL.Text.Trim();
                    i.BIEUPHI1_PHI = Utils.CIntDef(txtBP1_PHI.Text.Replace(",", ""));

                    i.BIEUPHI2_SL = txtBP2_SL.Text.Trim();
                    i.BIEUPHI2_PHI = Utils.CIntDef(txtBP2_PHI.Text.Replace(",", ""));

                    i.BIEUPHI3_SL = txtBP3_SL.Text.Trim();
                    i.BIEUPHI3_PHI = Utils.CIntDef(txtBP3_PHI.Text.Replace(",", ""));

                    i.BIEUPHI4_SL = txtBP4_SL.Text.Trim();
                    i.BIEUPHI4_PHI = Utils.CIntDef(txtBP4_PHI.Text.Replace(",", ""));

                    i.BIEUPHI5_SL = txtBP5_SL.Text.Trim();
                    i.BIEUPHI5_PHI = Utils.CIntDef(txtBP5_PHI.Text.Replace(",", ""));

                    i.BIEUPHI6_SL = txtBP6_SL.Text.Trim();
                    i.BIEUPHI6_PHI = Utils.CIntDef(txtBP6_PHI.Text.Replace(",", ""));

                    i.BIEUPHI_THEM = Utils.CIntDef(txtPhiPhatSinh.Text.Replace(",", ""));

                    proj_data.Create(i);

                    var getlink = db.CONG_NOs.OrderByDescending(n => n.ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        id = Utils.CIntDef(getlink[0].ID);
                        namCN = Utils.CIntDef(getlink[0].NAM);
                        mst = getlink[0].MST;
                        return 1;
                    }
                }
            }
            else
            {
                if (temp != 0)
                {
                    obj.TINH_TRANG = ddlTinhTrang.SelectedValue;
                    obj.DATE_TINHTRANG = txtNgayBatDau.Text != "" ?
                        DateTime.ParseExact(txtNgayBatDau.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : obj.DATE_TINHTRANG = null;
                    obj.DATE_TINHTRANG1 = txtNgayKetThuc.Text != "" ?
                        DateTime.ParseExact(txtNgayKetThuc.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : obj.DATE_TINHTRANG1 = null;
                    obj.DATE_THANHLAP = txtNgayThanhLap.Text != "" ?
                        DateTime.ParseExact(txtNgayThanhLap.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : obj.DATE_THANHLAP = null;
                    obj.TEN_KH = txtTenKH.Text;
                    obj.QL_THUE_CITY = Utils.CIntDef(ddlCity.SelectedValue);
                    obj.QL_THUE_DIST = Utils.CIntDef(ddlDist.SelectedValue);
                    obj.STT = Utils.CIntDef(txtSTT.Text);
                    obj.NO_NAM_TRUOC = Utils.CIntDef(txtNoNamTruoc.Text.Replace(",", ""));
                    obj.MST = txtMST.Text;
                    obj.DIA_CHI = txtDiaChi1.Text;
                    obj.GIAM_DOC = txtGiamDoc.Text;
                    obj.DIEN_THOAI = txtDienThoai.Text;
                    obj.EMAIL = txtEmail.Text;
                    obj.PHI = Utils.CIntDef(txtPhi.Text.Replace(",", ""));
                    obj.THANG_BD_THU = txtNgayThu.Text;
                    obj.NV_KT = Utils.CIntDef(ddlNVKT.SelectedValue);
                    obj.NV_KD = Utils.CIntDef(ddlNVKD.SelectedValue);
                    obj.NV_GN = Utils.CIntDef(ddlNVGN.SelectedValue);
                    obj.NGAY_KY_HD = txtNgayKyHD.Text != "" ?
                        DateTime.ParseExact(txtNgayKyHD.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : obj.NGAY_KY_HD = null;
                    obj.LOAI_CKS = ddlLoaiCKS.SelectedValue;
                    obj.GIU_CKS = chkCoGiuCKS.Checked ? 1 : 0;
                    obj.NGAY_HET_HAN_CKS = txtNgayHetHanCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayHetHanCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : obj.NGAY_HET_HAN_CKS = null;
                    if (chkCoGiuCKS.Checked)
                    {
                        obj.NGAY_GIU_CKS = txtNgayLayCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayLayCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : obj.NGAY_GIU_CKS = null;
                    }
                    else obj.NGAY_GIU_CKS = null;
                    obj.GHI_CHU = txtGhiChuMain.Text;
                    obj.DATE = DateTime.Now;

                    obj.BIEUPHI1_SL = txtBP1_SL.Text.Trim();
                    obj.BIEUPHI1_PHI = Utils.CIntDef(txtBP1_PHI.Text.Replace(",", ""));

                    obj.BIEUPHI2_SL = txtBP2_SL.Text.Trim();
                    obj.BIEUPHI2_PHI = Utils.CIntDef(txtBP2_PHI.Text.Replace(",", ""));

                    obj.BIEUPHI3_SL = txtBP3_SL.Text.Trim();
                    obj.BIEUPHI3_PHI = Utils.CIntDef(txtBP3_PHI.Text.Replace(",", ""));

                    obj.BIEUPHI4_SL = txtBP4_SL.Text.Trim();
                    obj.BIEUPHI4_PHI = Utils.CIntDef(txtBP4_PHI.Text.Replace(",", ""));

                    obj.BIEUPHI5_SL = txtBP5_SL.Text.Trim();
                    obj.BIEUPHI5_PHI = Utils.CIntDef(txtBP5_PHI.Text.Replace(",", ""));

                    obj.BIEUPHI6_SL = txtBP6_SL.Text.Trim();
                    obj.BIEUPHI6_PHI = Utils.CIntDef(txtBP6_PHI.Text.Replace(",", ""));

                    obj.BIEUPHI_THEM = Utils.CIntDef(txtPhiPhatSinh.Text.Replace(",", ""));

                    obj.TONG_NO = obj.CON_NO_1 + obj.CON_NO_2 + obj.CON_NO_3 + obj.CON_NO_4 + obj.CON_NO_5 + obj.CON_NO_6 + obj.CON_NO_7
                        + obj.CON_NO_8 + obj.CON_NO_9 + obj.CON_NO_10 + obj.CON_NO_11 + obj.CON_NO_12 + obj.CON_NO_BCTC + obj.NO_NAM_TRUOC;

                    proj_data.Update(obj);
                    db.SubmitChanges();
                }
                else
                {
                    var i = db.CONG_NOs.Where(n => n.ID == id).Single();
                    i.TINH_TRANG = ddlTinhTrang.SelectedValue;
                    i.DATE_TINHTRANG = txtNgayBatDau.Text != "" ?
                        DateTime.ParseExact(txtNgayBatDau.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_TINHTRANG = null;
                    i.DATE_TINHTRANG1 = txtNgayKetThuc.Text != "" ?
                        DateTime.ParseExact(txtNgayKetThuc.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_TINHTRANG1 = null;
                    i.DATE_THANHLAP = txtNgayThanhLap.Text != "" ?
                        DateTime.ParseExact(txtNgayThanhLap.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.DATE_THANHLAP = null;
                    i.TEN_KH = txtTenKH.Text;
                    i.QL_THUE_CITY = Utils.CIntDef(ddlCity.SelectedValue);
                    i.QL_THUE_DIST = Utils.CIntDef(ddlDist.SelectedValue);
                    i.STT = Utils.CIntDef(txtSTT.Text);
                    i.NO_NAM_TRUOC = Utils.CIntDef(txtNoNamTruoc.Text.Replace(",", ""));
                    i.MST = txtMST.Text;
                    i.DIA_CHI = txtDiaChi1.Text;
                    i.GIAM_DOC = txtGiamDoc.Text;
                    i.DIEN_THOAI = txtDienThoai.Text;
                    i.EMAIL = txtEmail.Text;
                    i.PHI = Utils.CIntDef(txtPhi.Text.Replace(",", ""));
                    i.THANG_BD_THU = txtNgayThu.Text;
                    i.NV_KT = Utils.CIntDef(ddlNVKT.SelectedValue);
                    i.NV_KD = Utils.CIntDef(ddlNVKD.SelectedValue);
                    i.NV_GN = Utils.CIntDef(ddlNVGN.SelectedValue);
                    i.NGAY_KY_HD = txtNgayKyHD.Text != "" ?
                        DateTime.ParseExact(txtNgayKyHD.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_KY_HD = null;
                    i.LOAI_CKS = ddlLoaiCKS.SelectedValue;
                    i.GIU_CKS = chkCoGiuCKS.Checked ? 1 : 0;
                    i.NGAY_HET_HAN_CKS = txtNgayHetHanCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayHetHanCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_HET_HAN_CKS = null;
                    if (chkCoGiuCKS.Checked)
                    {
                        i.NGAY_GIU_CKS = txtNgayLayCKS.Text != "" ?
                            DateTime.ParseExact(txtNgayLayCKS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_GIU_CKS = null;
                    }
                    else i.NGAY_GIU_CKS = null;
                    i.GHI_CHU = txtGhiChuMain.Text;
                    i.DATE = DateTime.Now;

                    i.BIEUPHI1_SL = txtBP1_SL.Text.Trim();
                    i.BIEUPHI1_PHI = Utils.CIntDef(txtBP1_PHI.Text.Replace(",", ""));

                    i.BIEUPHI2_SL = txtBP2_SL.Text.Trim();
                    i.BIEUPHI2_PHI = Utils.CIntDef(txtBP2_PHI.Text.Replace(",", ""));

                    i.BIEUPHI3_SL = txtBP3_SL.Text.Trim();
                    i.BIEUPHI3_PHI = Utils.CIntDef(txtBP3_PHI.Text.Replace(",", ""));

                    i.BIEUPHI4_SL = txtBP4_SL.Text.Trim();
                    i.BIEUPHI4_PHI = Utils.CIntDef(txtBP4_PHI.Text.Replace(",", ""));

                    i.BIEUPHI5_SL = txtBP5_SL.Text.Trim();
                    i.BIEUPHI5_PHI = Utils.CIntDef(txtBP5_PHI.Text.Replace(",", ""));

                    i.BIEUPHI6_SL = txtBP6_SL.Text.Trim();
                    i.BIEUPHI6_PHI = Utils.CIntDef(txtBP6_PHI.Text.Replace(",", ""));

                    i.BIEUPHI_THEM = Utils.CIntDef(txtPhiPhatSinh.Text.Replace(",", ""));

                    i.TONG_NO = i.CON_NO_1 + i.CON_NO_2 + i.CON_NO_3 + i.CON_NO_4 + i.CON_NO_5 + i.CON_NO_6 + i.CON_NO_7
                        + i.CON_NO_8 + i.CON_NO_9 + i.CON_NO_10 + i.CON_NO_11 + i.CON_NO_12 + i.CON_NO_BCTC + i.NO_NAM_TRUOC;

                    proj_data.Update(i);
                    db.SubmitChanges();
                }
            }
            return 0;
        }
        #endregion

        #region Funtion
        public void TestPermission()
        {
            try
            {
                getCookies _getCookies = new getCookies();
                _getCookies.getCookiesNew();

                //groupType = 7 là hành chánh, nhớ kiểm tra lại
                int _groupType = Utils.CIntDef(Session["Grouptype"]);
                if (_groupType != 1 && _groupType != 2 && _groupType != 14 && _groupType != 12 && _groupType != 7)
                {
                    //Phần chung
                    btnSave.Visible = false;
                    btnSaveT1.Visible = btnSaveT2.Visible = btnSaveT3.Visible = btnSaveT4.Visible = btnSaveT5.Visible =
                        btnSaveT6.Visible = btnSaveT7.Visible = btnSaveT8.Visible = btnSaveT9.Visible = btnSaveT10.Visible =
                        btnSaveT11.Visible = btnSaveT12.Visible = btnSaveT13.Visible = false;

                    //Dành cho giao nhận
                    if (_groupType == 6)
                    {
                        pMain.Visible = pMonth1.Visible = pMonth2.Visible = pMonth3.Visible = pMonth4.Visible = pMonth5.Visible = 
                            pMonth6.Visible = pMonth7.Visible = pMonth8.Visible = pMonth9.Visible = pMonth10.Visible = 
                            pMonth11.Visible = pMonth12.Visible = pMonth13.Visible = pDebt.Visible = false;
                        ddlNam.Enabled = false;

                        int _monthNow = Utils.CIntDef(DateTime.Now.Month);
                        switch (_monthNow)
                        {
                            case 1: pMonth1.Visible = btnSaveT1.Visible = true; break;
                            case 2: pMonth2.Visible = btnSaveT2.Visible = true; break;
                            case 3: pMonth3.Visible = btnSaveT3.Visible = true; break;
                            case 4: pMonth4.Visible = btnSaveT4.Visible = true; break;
                            case 5: pMonth5.Visible = btnSaveT5.Visible = true; break;
                            case 6: pMonth6.Visible = btnSaveT6.Visible = true; break;
                            case 7: pMonth7.Visible = btnSaveT7.Visible = true; break;
                            case 8: pMonth8.Visible = btnSaveT8.Visible = true; break;
                            case 9: pMonth9.Visible = btnSaveT9.Visible = true; break;
                            case 10: pMonth10.Visible = btnSaveT10.Visible = true; break;
                            case 11: pMonth11.Visible = btnSaveT11.Visible = true; break;
                            case 12: pMonth12.Visible = btnSaveT12.Visible = true; break;
                        }
                    }
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
        private DateTime fmDate(TextBox txt)
        {
            DateTime _date = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return _date;
        }
        private int get_bieuphi(string sl, int soluong)
        {
            try
            {
                int kq = 0;
                int temp1 = 0;
                int temp2 = 0;
                int _count1 = sl.Split('-').Count();
                int _count2 = sl.Split('>').Count();
                if (sl.Contains("-"))
                {
                    if(_count1 > 0)
                    {
                        temp1 = Utils.CIntDef(sl.Split('-')[0]);
                    }
                    if (_count1 > 1)
                    {
                        temp2 = Utils.CIntDef(sl.Split('-')[1]);
                    }
                    if (soluong >= temp1 && soluong <= temp2)
                    {
                        kq = 1;
                    }
                }
                else if (sl.Contains(">"))
                {
                    if (_count2 > 1)
                    {
                        temp2 = Utils.CIntDef(sl.Split('>')[1]);
                    }
                    if (soluong > temp2)
                    {
                        kq = 1;
                    }
                }
                return kq;
            }
            catch (Exception) { return 0; }
        }
        private int get_conno(int phi, int t1, int t2, int t3, int t4)
        {
            try
            {
                int conno = 0;
                conno = phi - (t1 + t2 + t3 + t4);
                return conno;
            }
            catch (Exception) { return 0; }
        }
        private void load_tongno()
        {
            int t1 = Utils.CIntDef(txtConNo01.Text.Replace(",", ""), 0);
            int t2 = Utils.CIntDef(txtConNo02.Text.Replace(",", ""), 0);
            int t3 = Utils.CIntDef(txtConNo03.Text.Replace(",", ""), 0);
            int t4 = Utils.CIntDef(txtConNo04.Text.Replace(",", ""), 0);
            int t5 = Utils.CIntDef(txtConNo05.Text.Replace(",", ""), 0);
            int t6 = Utils.CIntDef(txtConNo06.Text.Replace(",", ""), 0);
            int t7 = Utils.CIntDef(txtConNo07.Text.Replace(",", ""), 0);
            int t8 = Utils.CIntDef(txtConNo08.Text.Replace(",", ""), 0);
            int t9 = Utils.CIntDef(txtConNo09.Text.Replace(",", ""), 0);
            int t10 = Utils.CIntDef(txtConNo10.Text.Replace(",", ""), 0);
            int t11 = Utils.CIntDef(txtConNo11.Text.Replace(",", ""), 0);
            int t12 = Utils.CIntDef(txtConNo12.Text.Replace(",", ""), 0);
            int t13 = Utils.CIntDef(txtConNo13.Text.Replace(",", ""), 0);
            int namTruoc = Utils.CIntDef(txtNoNamTruoc.Text.Replace(",", ""), 0);
            int tong = t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12 + t13 + namTruoc;
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                i.TONG_NO = tong;
                proj_data.Update(i);
                db.SubmitChanges();
            }
            txtTongNo.Text = String.Format("{0:###,##0}",tong);
        }
        private string load_Phi(int sl, CheckBox chk)
        {
            string temp = "";
            int _phithem = Utils.CIntDef(txtPhiPhatSinh.Text.Replace(",", ""));
            if (chk.Checked)
            {
                temp = txtPhi.Text;
            }
            else if ((get_bieuphi(txtBP1_SL.Text, sl) == 1 || txtBP1_SL.Text == "") && sl != 0)
            {
                temp = txtBP1_PHI.Text;
            }
            else if ((get_bieuphi(txtBP2_SL.Text, sl) == 1 || txtBP2_SL.Text == "") && sl != 0)
            {
                int phidv = Utils.CIntDef(txtBP2_PHI.Text.Replace(",", ""));
                if (txtBP2_SL.Text == "")
                {
                    int _getBP1 = Utils.CIntDef(txtBP1_SL.Text.Split('-')[1], 0);
                    int _slNew = sl - _getBP1;
                    phidv = Utils.CIntDef(txtBP1_PHI.Text.Replace(",", ""));
                    phidv = phidv + (_phithem * _slNew);
                }
                temp = String.Format("{0:###,##0}", phidv);
            }
            else if ((get_bieuphi(txtBP3_SL.Text, sl) == 1 || txtBP3_SL.Text == "") && sl != 0)
            {
                int phidv = Utils.CIntDef(txtBP3_PHI.Text.Replace(",", ""));
                if (txtBP3_SL.Text == "")
                {
                    int _getBP2 = Utils.CIntDef(txtBP2_SL.Text.Split('-')[1], 0);
                    int _slNew = sl - _getBP2;
                    phidv = Utils.CIntDef(txtBP2_PHI.Text.Replace(",", ""));
                    phidv = phidv + (_phithem * _slNew);
                }
                temp = String.Format("{0:###,##0}", phidv);
            }
            else if ((get_bieuphi(txtBP4_SL.Text, sl) == 1 || txtBP4_SL.Text == "") && sl != 0)
            {
                int phidv = Utils.CIntDef(txtBP4_PHI.Text.Replace(",", ""));
                if (txtBP4_SL.Text == "")
                {
                    int _getBP3 = Utils.CIntDef(txtBP3_SL.Text.Split('-')[1], 0);
                    int _slNew = sl - _getBP3;
                    phidv = Utils.CIntDef(txtBP3_PHI.Text.Replace(",", ""));
                    phidv = phidv + (_phithem * _slNew);
                }
                temp = String.Format("{0:###,##0}", phidv);
            }
            else if ((get_bieuphi(txtBP5_SL.Text, sl) == 1 || txtBP5_SL.Text == "") && sl != 0)
            {
                int phidv = Utils.CIntDef(txtBP5_PHI.Text.Replace(",", ""));
                if (txtBP5_SL.Text == "")
                {
                    int _getBP4 = Utils.CIntDef(txtBP4_SL.Text.Split('-')[1], 0);
                    int _slNew = sl - _getBP4;
                    phidv = Utils.CIntDef(txtBP4_PHI.Text.Replace(",", ""));
                    phidv = phidv + (_phithem * _slNew);
                }
                temp = String.Format("{0:###,##0}", phidv);
            }
            else if ((get_bieuphi(txtBP6_SL.Text, sl) == 1 || txtBP6_SL.Text == "") && sl != 0)
            {
                int phidv = Utils.CIntDef(txtBP6_PHI.Text.Replace(",", ""));
                if (txtBP6_SL.Text == "")
                {
                    int _getBP5 = Utils.CIntDef(txtBP5_SL.Text.Split('-')[1], 0);
                    int _slNew = sl - _getBP5;
                    phidv = Utils.CIntDef(txtBP5_PHI.Text.Replace(",", ""));
                    phidv = phidv + (_phithem * _slNew);
                }
                temp = String.Format("{0:###,##0}", phidv);
            }
            return temp;
        }
        private void ClearText()
        {
            //Thông tin chung
            //txtTenKH.Text = txtMST.Text = txtQlThue.Text = txtDiaChi1.Text = txtDiaChi2.Text = txtGiamDoc.Text
            //    = txtEmail.Text = txtDienThoai.Text = txtNgayThu.Text = txtPhi.Text
            //    = txtBP1_PHI.Text = txtBP1_SL.Text = txtBP2_PHI.Text = txtBP2_SL.Text = txtBP3_PHI.Text = txtBP3_SL.Text
            //    = txtBP4_PHI.Text = txtBP4_SL.Text = txtBP5_PHI.Text = txtBP5_SL.Text = txtBP6_PHI.Text = txtBP6_SL.Text = txtPhiPhatSinh.Text = "";
            //Tháng 1
            txtPhiDV01.Text = txtSL01.Text = txtNgayTT01.Text = txtGhiChu01.Text = txtDaTT01_1.Text = txtDaTT01_2.Text = txtDaTT01_3.Text = txtDaTT01_4.Text = txtConNo01.Text = "";
            chkBPCD01.Checked = false;
            //Tháng 2
            txtPhiDV02.Text = txtSL02.Text = txtNgayTT02.Text = txtGhiChu02.Text = txtDaTT02_1.Text = txtDaTT02_2.Text = txtDaTT02_3.Text = txtDaTT02_4.Text = txtConNo02.Text = "";
            chkBPCD02.Checked = false;
            //Tháng 3
            txtPhiDV03.Text = txtSL03.Text = txtNgayTT03.Text = txtGhiChu03.Text = txtDaTT03_1.Text = txtDaTT03_2.Text = txtDaTT03_3.Text = txtDaTT03_4.Text = txtConNo03.Text = "";
            chkBPCD03.Checked = false;
            //Tháng 4
            txtPhiDV04.Text = txtSL04.Text = txtNgayTT04.Text = txtGhiChu04.Text = txtDaTT04_1.Text = txtDaTT04_2.Text = txtDaTT04_3.Text = txtDaTT04_4.Text = txtConNo04.Text = "";
            chkBPCD04.Checked = false;
            //Tháng 5
            txtPhiDV05.Text = txtSL05.Text = txtNgayTT05.Text = txtGhiChu05.Text = txtDaTT05_1.Text = txtDaTT05_2.Text = txtDaTT05_3.Text = txtDaTT05_4.Text = txtConNo05.Text = "";
            chkBPCD05.Checked = false;
            //Tháng 6
            txtPhiDV06.Text = txtSL06.Text = txtNgayTT06.Text = txtGhiChu06.Text = txtDaTT06_1.Text = txtDaTT06_2.Text = txtDaTT06_3.Text = txtDaTT06_4.Text = txtConNo06.Text = "";
            chkBPCD06.Checked = false;
            //Tháng 7
            txtPhiDV07.Text = txtSL07.Text = txtNgayTT07.Text = txtGhiChu07.Text = txtDaTT07_1.Text = txtDaTT07_2.Text = txtDaTT07_3.Text = txtDaTT07_4.Text = txtConNo07.Text = "";
            chkBPCD07.Checked = false;
            //Tháng 8
            txtPhiDV08.Text = txtSL08.Text = txtNgayTT08.Text = txtGhiChu08.Text = txtDaTT08_1.Text = txtDaTT08_2.Text = txtDaTT08_3.Text = txtDaTT08_4.Text = txtConNo08.Text = "";
            chkBPCD08.Checked = false;
            //Tháng 9
            txtPhiDV09.Text = txtSL09.Text = txtNgayTT09.Text = txtGhiChu09.Text = txtDaTT09_1.Text = txtDaTT09_2.Text = txtDaTT09_3.Text = txtDaTT09_4.Text = txtConNo09.Text = "";
            chkBPCD09.Checked = false;
            //Tháng 10
            txtPhiDV10.Text = txtSL10.Text = txtNgayTT10.Text = txtGhiChu10.Text = txtDaTT10_1.Text = txtDaTT10_2.Text = txtDaTT10_3.Text = txtDaTT10_4.Text = txtConNo10.Text = "";
            chkBPCD10.Checked = false;
            //Tháng 11
            txtPhiDV11.Text = txtSL11.Text = txtNgayTT11.Text = txtGhiChu11.Text = txtDaTT11_1.Text = txtDaTT11_2.Text = txtDaTT11_3.Text = txtDaTT11_4.Text = txtConNo11.Text = "";
            chkBPCD11.Checked = false;
            //Tháng 12
            txtPhiDV12.Text = txtSL12.Text = txtNgayTT12.Text = txtGhiChu12.Text = txtDaTT12_1.Text = txtDaTT12_2.Text = txtDaTT12_3.Text = txtDaTT12_4.Text = txtConNo12.Text = "";
            chkBPCD12.Checked = false;
            //Tháng 13
            txtPhiDV13.Text = txtSL13.Text = txtNgayTT13.Text = txtGhiChu13.Text = txtDaTT13_1.Text = txtDaTT13_2.Text = txtDaTT13_3.Text = txtDaTT13_4.Text = txtConNo13.Text = "";
            chkBPCD13.Checked = false;
            //Tổng
            txtTongNo.Text = "";
        }
        private void Load_city()
        {
            ddlCity.DataValueField = "PROP_ID";
            ddlCity.DataTextField = "PROP_NAME";
            ddlCity.DataSource = unit_data.Loadcity();
            ddlCity.DataBind();
            ListItem l = new ListItem("Tỉnh/Thành", "0");
            l.Selected = true;
            ddlCity.Items.Insert(0, l);
        }
        private void Load_distric(int id)
        {
            var list = unit_data.Loaddistric(id);
            if (list.Count > 0)
            {
                ddlDist.DataValueField = "PROP_ID";
                ddlDist.DataTextField = "PROP_NAME";
                ddlDist.DataSource = list;
                ddlDist.DataBind();
                ListItem l = new ListItem("Quận/Huyện", "0");
                l.Selected = true;
                ddlDist.Items.Insert(0, l);
            }
            else
            {
                DataTable dt = new DataTable("Newtable");

                dt.Columns.Add(new DataColumn("PROP_ID"));
                dt.Columns.Add(new DataColumn("PROP_NAME"));

                DataRow row = dt.NewRow();
                row["PROP_ID"] = 0;
                row["PROP_NAME"] = "Quận/Huyện";
                dt.Rows.Add(row);

                ddlDist.DataTextField = "PROP_NAME";
                ddlDist.DataValueField = "PROP_ID";
                ddlDist.DataSource = dt;
                ddlDist.DataBind();

            }
        }
        #endregion

        #region Event
        protected void chkCoGiuCKS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoGiuCKS.Checked)
                iNgayLayCKS.Visible = true;
            else iNgayLayCKS.Visible = false;
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCity = Utils.CIntDef(ddlCity.SelectedValue);
            Load_distric(idCity);
        }
        protected void ddlTinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTinhTrang.SelectedValue == "---")
            {
                iThoigianBDKT.Visible = false;
            }
            else 
            {
                iThoigianBDKT.Visible = true;
                if (ddlTinhTrang.SelectedIndex == 1)
                {
                    iThoiGianTieuDe.Text = "Bắt đầu - Kết thúc";
                    txtNgayBatDau.Visible = txtNgayKetThuc.Visible = true;
                }
                else
                {
                    iThoiGianTieuDe.Text = "Bắt đầu";
                    txtNgayBatDau.Visible = true;
                    txtNgayKetThuc.Visible = false;
                }
            }
        }
        protected void ddlNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Data(Utils.CIntDef(ddlNam.SelectedValue));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //=1 là Create
            if(Save_Data(Utils.CIntDef(ddlNam.SelectedValue)) == 1)
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>var mst = String('" + mst + "');parent.document.getElementById('openCongNo(" + id + ",mst," + namCN + ")').onclick = parent.openCongNo(" + id + ",mst," + namCN + ");</script>");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }

        protected void txtSL01_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL01.Text);
            txtPhiDV01.Text = load_Phi(sl, chkBPCD01);
        }
        protected void txtSL02_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL02.Text);
            txtPhiDV02.Text = load_Phi(sl, chkBPCD02);
        }
        protected void txtSL03_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL03.Text);
            txtPhiDV03.Text = load_Phi(sl, chkBPCD03);
        }
        protected void txtSL04_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL04.Text);
            txtPhiDV04.Text = load_Phi(sl, chkBPCD04);
        }
        protected void txtSL05_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL05.Text);
            txtPhiDV05.Text = load_Phi(sl, chkBPCD05);
        }
        protected void txtSL06_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL06.Text);
            txtPhiDV06.Text = load_Phi(sl, chkBPCD06);
        }
        protected void txtSL07_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL07.Text);
            txtPhiDV07.Text = load_Phi(sl, chkBPCD07);
        }
        protected void txtSL08_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL08.Text);
            txtPhiDV08.Text = load_Phi(sl, chkBPCD08);
        }
        protected void txtSL09_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL09.Text);
            txtPhiDV09.Text = load_Phi(sl, chkBPCD09);
        }
        protected void txtSL10_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL10.Text);
            txtPhiDV10.Text = load_Phi(sl, chkBPCD10);
        }
        protected void txtSL11_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL11.Text);
            txtPhiDV11.Text = load_Phi(sl, chkBPCD11);
        }
        protected void txtSL12_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL12.Text);
            txtPhiDV12.Text = load_Phi(sl, chkBPCD12);
        }
        protected void txtSL13_TextChanged(object sender, EventArgs e)
        {
            int sl = Utils.CIntDef(txtSL13.Text);
            txtPhiDV13.Text = load_Phi(sl, chkBPCD13);
        }

        protected void chkBPCD01_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD01.Checked)
            {
                txtSL01.Enabled = false;
            }
            else
            {
                txtSL01.Enabled = true;
            }
            txtSL01_TextChanged(sender, e);
        }
        protected void chkBPCD02_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD02.Checked)
            {
                txtSL02.Enabled = false;
            }
            else
            {
                txtSL02.Enabled = true;
            }
            txtSL02_TextChanged(sender, e);
        }
        protected void chkBPCD03_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD03.Checked)
            {
                txtSL03.Enabled = false;
            }
            else
            {
                txtSL03.Enabled = true;
            }
            txtSL03_TextChanged(sender, e);
        }
        protected void chkBPCD04_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD04.Checked)
            {
                txtSL04.Enabled = false;
            }
            else
            {
                txtSL04.Enabled = true;
            }
            txtSL04_TextChanged(sender, e);
        }
        protected void chkBPCD05_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD05.Checked)
            {
                txtSL05.Enabled = false;
            }
            else
            {
                txtSL05.Enabled = true;
            }
            txtSL05_TextChanged(sender, e);
        }
        protected void chkBPCD06_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD06.Checked)
            {
                txtSL06.Enabled = false;
            }
            else
            {
                txtSL06.Enabled = true;
            }
            txtSL06_TextChanged(sender, e);
        }
        protected void chkBPCD07_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD07.Checked)
            {
                txtSL07.Enabled = false;
            }
            else
            {
                txtSL07.Enabled = true;
            }
            txtSL07_TextChanged(sender, e);
        }
        protected void chkBPCD08_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD08.Checked)
            {
                txtSL08.Enabled = false;
            }
            else
            {
                txtSL08.Enabled = true;
            }
            txtSL08_TextChanged(sender, e);
        }
        protected void chkBPCD09_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD09.Checked)
            {
                txtSL09.Enabled = false;
            }
            else
            {
                txtSL09.Enabled = true;
            }
            txtSL09_TextChanged(sender, e);
        }
        protected void chkBPCD10_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD10.Checked)
            {
                txtSL10.Enabled = false;
            }
            else
            {
                txtSL10.Enabled = true;
            }
            txtSL10_TextChanged(sender, e);
        }
        protected void chkBPCD11_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD11.Checked)
            {
                txtSL11.Enabled = false;
            }
            else
            {
                txtSL11.Enabled = true;
            }
            txtSL11_TextChanged(sender, e);
        }
        protected void chkBPCD12_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD12.Checked)
            {
                txtSL12.Enabled = false;
            }
            else
            {
                txtSL12.Enabled = true;
            }
            txtSL12_TextChanged(sender, e);
        }
        protected void chkBPCD13_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBPCD13.Checked)
            {
                txtSL13.Enabled = false;
            }
            else
            {
                txtSL13.Enabled = true;
            }
            txtSL13_TextChanged(sender, e);
        }

        protected void btnSaveT1_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV01.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT01_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT01_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT01_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT01_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD01.Checked)
                    i.SOLUONG_T1 = 0;
                else
                    i.SOLUONG_T1 = Utils.CIntDef(txtSL01.Text);
                i.PHI_DV_1 = phidv;
                i.DA_TT1_1 = tt1;
                i.DA_TT1_2 = tt2;
                i.DA_TT1_3 = tt3;
                i.DA_TT1_4 = tt4;
                i.CON_NO_1 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_1 = txtNgayTT01.Text;
                i.GHI_CHU1 = txtGhiChu01.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo01.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT2_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV02.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT02_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT02_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT02_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT02_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD02.Checked)
                    i.SOLUONG_T2 = 0;
                else
                    i.SOLUONG_T2 = Utils.CIntDef(txtSL02.Text);
                i.PHI_DV_2 = phidv;
                i.DA_TT2_1 = tt1;
                i.DA_TT2_2 = tt2;
                i.DA_TT2_3 = tt3;
                i.DA_TT2_4 = tt4;
                i.CON_NO_2 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_2 = txtNgayTT02.Text;
                i.GHI_CHU2 = txtGhiChu02.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo02.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT3_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV03.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT03_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT03_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT03_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT03_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD03.Checked)
                    i.SOLUONG_T3 = 0;
                else
                    i.SOLUONG_T3 = Utils.CIntDef(txtSL03.Text);
                i.PHI_DV_3 = phidv;
                i.DA_TT3_1 = tt1;
                i.DA_TT3_2 = tt2;
                i.DA_TT3_3 = tt3;
                i.DA_TT3_4 = tt4;
                i.CON_NO_3 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_3 = txtNgayTT03.Text;
                i.GHI_CHU3 = txtGhiChu03.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo03.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT4_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV04.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT04_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT04_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT04_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT04_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD04.Checked)
                    i.SOLUONG_T4 = 0;
                else
                    i.SOLUONG_T4 = Utils.CIntDef(txtSL04.Text);
                i.PHI_DV_4 = phidv;
                i.DA_TT4_1 = tt1;
                i.DA_TT4_2 = tt2;
                i.DA_TT4_3 = tt3;
                i.DA_TT4_4 = tt4;
                i.CON_NO_4 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_4 = txtNgayTT04.Text;
                i.GHI_CHU4 = txtGhiChu04.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo04.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT5_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV05.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT05_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT05_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT05_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT05_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD05.Checked)
                    i.SOLUONG_T5 = 0;
                else
                    i.SOLUONG_T5 = Utils.CIntDef(txtSL05.Text);
                i.PHI_DV_5 = phidv;
                i.DA_TT5_1 = tt1;
                i.DA_TT5_2 = tt2;
                i.DA_TT5_3 = tt3;
                i.DA_TT5_4 = tt4;
                i.CON_NO_5 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_5 = txtNgayTT05.Text;
                i.GHI_CHU5 = txtGhiChu05.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo05.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT6_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV06.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT06_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT06_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT06_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT06_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD06.Checked)
                    i.SOLUONG_T6 = 0;
                else
                    i.SOLUONG_T6 = Utils.CIntDef(txtSL06.Text);
                i.PHI_DV_6 = phidv;
                i.DA_TT6_1 = tt1;
                i.DA_TT6_2 = tt2;
                i.DA_TT6_3 = tt3;
                i.DA_TT6_4 = tt4;
                i.CON_NO_6 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_6 = txtNgayTT06.Text;
                i.GHI_CHU6 = txtGhiChu06.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo06.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT7_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV07.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT07_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT07_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT07_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT07_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD07.Checked)
                    i.SOLUONG_T7 = 0;
                else
                    i.SOLUONG_T7 = Utils.CIntDef(txtSL07.Text);
                i.PHI_DV_7 = phidv;
                i.DA_TT7_1 = tt1;
                i.DA_TT7_2 = tt2;
                i.DA_TT7_3 = tt3;
                i.DA_TT7_4 = tt4;
                i.CON_NO_7 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_7 = txtNgayTT07.Text;
                i.GHI_CHU7 = txtGhiChu07.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo07.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT8_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV08.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT08_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT08_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT08_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT08_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD08.Checked)
                    i.SOLUONG_T8 = 0;
                else
                    i.SOLUONG_T8 = Utils.CIntDef(txtSL08.Text);
                i.PHI_DV_8 = phidv;
                i.DA_TT8_1 = tt1;
                i.DA_TT8_2 = tt2;
                i.DA_TT8_3 = tt3;
                i.DA_TT8_4 = tt4;
                i.CON_NO_8 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_8 = txtNgayTT08.Text;
                i.GHI_CHU8 = txtGhiChu08.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo08.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT9_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV09.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT09_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT09_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT09_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT09_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD09.Checked)
                    i.SOLUONG_T9 = 0;
                else
                    i.SOLUONG_T9 = Utils.CIntDef(txtSL09.Text);
                i.PHI_DV_9 = phidv;
                i.DA_TT9_1 = tt1;
                i.DA_TT9_2 = tt2;
                i.DA_TT9_3 = tt3;
                i.DA_TT9_4 = tt4;
                i.CON_NO_9 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_9 = txtNgayTT09.Text;
                i.GHI_CHU9 = txtGhiChu09.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo09.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT10_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV10.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT10_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT10_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT10_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT10_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD10.Checked)
                    i.SOLUONG_T10 = 0;
                else
                    i.SOLUONG_T10 = Utils.CIntDef(txtSL10.Text);
                i.PHI_DV_10 = phidv;
                i.DA_TT10_1 = tt1;
                i.DA_TT10_2 = tt2;
                i.DA_TT10_3 = tt3;
                i.DA_TT10_4 = tt4;
                i.CON_NO_10 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_10 = txtNgayTT10.Text;
                i.GHI_CHU10 = txtGhiChu10.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo10.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT11_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV11.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT11_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT11_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT11_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT11_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD11.Checked)
                    i.SOLUONG_T11 = 0;
                else
                    i.SOLUONG_T11 = Utils.CIntDef(txtSL11.Text);
                i.PHI_DV_11 = phidv;
                i.DA_TT11_1 = tt1;
                i.DA_TT11_2 = tt2;
                i.DA_TT11_3 = tt3;
                i.DA_TT11_4 = tt4;
                i.CON_NO_11 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_11 = txtNgayTT11.Text;
                i.GHI_CHU11 = txtGhiChu11.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo11.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT12_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV12.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT12_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT12_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT12_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT12_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD12.Checked)
                    i.SOLUONG_T12 = 0;
                else
                    i.SOLUONG_T12 = Utils.CIntDef(txtSL12.Text);
                i.PHI_DV_12 = phidv;
                i.DA_TT12_1 = tt1;
                i.DA_TT12_2 = tt2;
                i.DA_TT12_3 = tt3;
                i.DA_TT12_4 = tt4;
                i.CON_NO_12 = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_12 = txtNgayTT12.Text;
                i.GHI_CHU12 = txtGhiChu12.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo01.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        protected void btnSaveT13_Click(object sender, EventArgs e)
        {
            int phidv = Utils.CIntDef(txtPhiDV08.Text.Replace(",", ""));
            int tt1 = Utils.CIntDef(txtDaTT13_1.Text.Replace(",", ""));
            int tt2 = Utils.CIntDef(txtDaTT13_2.Text.Replace(",", ""));
            int tt3 = Utils.CIntDef(txtDaTT13_3.Text.Replace(",", ""));
            int tt4 = Utils.CIntDef(txtDaTT13_4.Text.Replace(",", ""));
            var i = proj_data.GetByMSTYear(mst, Utils.CIntDef(ddlNam.SelectedValue));
            if (i != null)
            {
                if (chkBPCD13.Checked)
                    i.SOLUONG_T13 = 0;
                else
                    i.SOLUONG_T13 = Utils.CIntDef(txtSL13.Text);
                i.PHI_DV_BCTC = phidv;
                i.DA_TT13_1 = tt1;
                i.DA_TT13_2 = tt2;
                i.DA_TT13_3 = tt3;
                i.DA_TT13_4 = tt4;
                i.CON_NO_BCTC = get_conno(phidv, tt1, tt2, tt3, tt4);
                i.NGAY_TT_BCTC = txtNgayTT13.Text;
                i.GHI_CHU13 = txtGhiChu13.Text;
                proj_data.Update(i);
                db.SubmitChanges();

                txtConNo13.Text = String.Format("{0:###,##0}", get_conno(phidv, tt1, tt2, tt3, tt4));
                load_tongno();
            }
        }
        #endregion
    }
}