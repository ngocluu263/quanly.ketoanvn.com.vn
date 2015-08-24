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
    public partial class popup_cong_no_cks : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        CongNoCKSData proj_data = new CongNoCKSData();
        NhaCungCapData _NhaCungCapData = new NhaCungCapData();
        SanPhamData _SanPhamData = new SanPhamData();
        UnitData unit_data = new UnitData();
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                Load_MaNCC(ddlNhaCungCap, 0);
                Load_MaNCC(ddlNhaCungCap1, 1);
                Load_NhanVien(3, ddlNVKD);//nhân viên kinh doanh
                Load_Data();
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Data
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
                }
            }
            catch (Exception) { }
        }
        private void Load_Data()
        {
            if (id > 0)
            {
                var i = proj_data.GetById(id);
                if (i != null)
                {
                    txtSTT.Text = Utils.CStrDef(i.STT);
                    txtTenKH.Text = i.TEN_CTY;
                    txtMST.Text = i.MST;
                    txtNgayNhanTB.Text = i.NGAY_NHAN_TB == null ? "" : i.NGAY_NHAN_TB.Value.ToString("dd/MM/yyyy");
                    ddlNVKD.Text = Utils.CStrDef(i.NV_KD);
                    ddlTinhTrang.Text = Utils.CStrDef(i.TINH_TRANG);
                    ddlLoaiHD.Text = Utils.CStrDef(i.LOAI_HOPDONG);
                    txtGhiChu.Text = i.GHI_CHU;

                    #region CKS
                    ddlNhaCungCap.SelectedValue = i.CKS_NHA_CUNG_CAP;
                    if (ddlNhaCungCap.SelectedValue != null && ddlNhaCungCap.SelectedValue != "0")
                        Load_MaSP(ddlNhaCungCap.SelectedValue, ddlSanPham);
                    ddlSanPham.SelectedValue = i.CKS_SAN_PHAM;
                    txtCKS_PhiDV.Text = i.CKS_PHI_DV == null ? "" : i.CKS_PHI_DV.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_GiaTbToken.Text = i.CKS_GIA_TK == null ? "" : i.CKS_GIA_TK.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_VAT.Text = i.CKS_VAT == null ? "" : i.CKS_VAT.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_TongCong.Text = i.CKS_TONG_CONG == null ? "" : i.CKS_TONG_CONG.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_HoaHong_TyLe.Text = i.CKS_HOA_HONG_TL == null ? "" : i.CKS_HOA_HONG_TL.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_HoaHong_DL.Text = i.CKS_HOA_HONG_DL == null ? "" : i.CKS_HOA_HONG_DL.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_HoaHong_VAT.Text = i.CKS_HOA_HONG_VAT == null ? "" : i.CKS_HOA_HONG_VAT.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_HoaHong.Text = i.CKS_HOA_HONG == null ? "" : i.CKS_HOA_HONG.Value.ToString("###,##0").Replace(".",",");
                    txtCKS_TongTTNhaCC.Text = i.CKS_TONG_TT_NCC == null ? "" : i.CKS_TONG_TT_NCC.Value.ToString("###,##0").Replace(".",",");
                    txtNgayHetHanTB.Text = i.CKS_NGAY_HH_TB == null ? "" : i.CKS_NGAY_HH_TB.Value.ToString("dd/MM/yyyy");

                    #endregion

                    #region PM
                    ddlNhaCungCap1.SelectedValue = i.PM_NHA_CUNG_CAP;
                    if (ddlNhaCungCap1.SelectedValue != null && ddlNhaCungCap1.SelectedValue != "0")
                        Load_MaSP(ddlNhaCungCap1.SelectedValue, ddlSanPham1);
                    ddlSanPham1.SelectedValue = i.PM_SAN_PHAM;
                    txtPM_PhiDV.Text = i.PM_PHI_DV == null ? "" : i.PM_PHI_DV.Value.ToString("###,##0").Replace(".",",");
                    txtPM_GiaTbToken.Text = i.PM_GIA_TK == null ? "" : i.PM_GIA_TK.Value.ToString("###,##0").Replace(".",",");
                    txtPM_VAT.Text = i.PM_VAT == null ? "" : i.PM_VAT.Value.ToString("###,##0").Replace(".",",");
                    txtPM_TongCong.Text = i.PM_TONG_CONG == null ? "" : i.PM_TONG_CONG.Value.ToString("###,##0").Replace(".",",");
                    txtPM_HoaHong_TyLe.Text = i.PM_HOA_HONG_TL == null ? "" : i.PM_HOA_HONG_TL.Value.ToString("###,##0").Replace(".",",");
                    txtPM_HoaHong_DL.Text = i.PM_HOA_HONG_DL == null ? "" : i.PM_HOA_HONG_DL.Value.ToString("###,##0").Replace(".",",");
                    txtPM_HoaHong_VAT.Text = i.PM_HOA_HONG_VAT == null ? "" : i.PM_HOA_HONG_VAT.Value.ToString("###,##0").Replace(".",",");
                    txtPM_HoaHong.Text = i.PM_HOA_HONG == null ? "" : i.PM_HOA_HONG.Value.ToString("###,##0").Replace(".",",");
                    txtPM_TongTTNhaCC.Text = i.PM_TONG_TT_NCC == null ? "" : i.PM_TONG_TT_NCC.Value.ToString("###,##0").Replace(".",",");
                    txtNgayHetHanTB.Text = i.PM_NGAY_HH_TB == null ? "" : i.PM_NGAY_HH_TB.Value.ToString("dd/MM/yyyy");
                    #endregion

                    txtTT_TokenCKS.Text = i.TT_TK_CKS == null ? "" : i.TT_TK_CKS.Value.ToString("###,##0").Replace(".",",");
                    txtTT_PM.Text = i.TT_PHAN_MEM == null ? "" : i.TT_PHAN_MEM.Value.ToString("###,##0").Replace(".",",");
                    txtTTHoaHong_CKS.Text = i.TT_HH_CKS == null ? "" : i.TT_HH_CKS.Value.ToString("###,##0").Replace(".",",");
                    txtTTHoaHong_PM.Text = i.TT_HH_PM == null ? "" : i.TT_HH_PM.Value.ToString("###,##0").Replace(".",",");
                    txtTT_ConNo.Text = i.CON_NO == null ? "" : i.CON_NO.Value.ToString("###,##0").Replace(".",",");
                    txtTT_NgayThu.Text = i.NGAY_THU;
                }
            }
            else 
            {
                
            }
        }
        private int Save_Data()
        {
            if (id != 0)
            {
                var i = proj_data.GetById(id);
                i.STT = Utils.CIntDef(txtSTT.Text);
                i.TEN_CTY = txtTenKH.Text;
                i.MST = txtMST.Text;
                i.NGAY_NHAN_TB = txtNgayNhanTB.Text != "" ?
                    DateTime.ParseExact(txtNgayNhanTB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_NHAN_TB = null;
                i.NV_KD = Utils.CIntDef(ddlNVKD.SelectedValue);
                i.TINH_TRANG = ddlTinhTrang.Text;
                i.LOAI_HOPDONG = ddlLoaiHD.Text;

                #region CKS
                i.CKS_NHA_CUNG_CAP = ddlNhaCungCap.SelectedValue;
                i.CKS_SAN_PHAM = ddlSanPham.SelectedValue;
                i.CKS_PHI_DV = Utils.CIntDef(txtCKS_PhiDV.Text.Replace(",", ""));
                i.CKS_GIA_TK = Utils.CIntDef(txtCKS_GiaTbToken.Text.Replace(",", ""));
                i.CKS_VAT = Utils.CIntDef(txtCKS_VAT.Text.Replace(",", ""));
                i.CKS_TONG_CONG = Utils.CIntDef(txtCKS_TongCong.Text.Replace(",", ""));
                i.CKS_HOA_HONG_TL = Utils.CIntDef(txtCKS_HoaHong_TyLe.Text.Replace(",", ""));
                i.CKS_HOA_HONG_DL = Utils.CIntDef(txtCKS_HoaHong_DL.Text.Replace(",", ""));
                i.CKS_HOA_HONG_VAT = Utils.CIntDef(txtCKS_HoaHong_VAT.Text.Replace(",", ""));
                i.CKS_HOA_HONG = Utils.CIntDef(txtCKS_HoaHong.Text.Replace(",", ""));
                i.CKS_TONG_TT_NCC = Utils.CIntDef(txtCKS_TongTTNhaCC.Text.Replace(",", ""));
                i.CKS_NGAY_HH_TB = txtNgayHetHanTB.Text != "" ?
                    DateTime.ParseExact(txtNgayHetHanTB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.CKS_NGAY_HH_TB = null;
                #endregion

                #region PM
                i.PM_NHA_CUNG_CAP = ddlNhaCungCap1.SelectedValue;
                i.PM_SAN_PHAM = ddlSanPham1.SelectedValue;
                i.PM_PHI_DV = Utils.CIntDef(txtPM_PhiDV.Text.Replace(",", ""));
                i.PM_GIA_TK = Utils.CIntDef(txtPM_GiaTbToken.Text.Replace(",", ""));
                i.PM_VAT = Utils.CIntDef(txtPM_VAT.Text.Replace(",", ""));
                i.PM_TONG_CONG = Utils.CIntDef(txtPM_TongCong.Text.Replace(",", ""));
                i.PM_HOA_HONG_TL = Utils.CIntDef(txtPM_HoaHong_TyLe.Text.Replace(",", ""));
                i.PM_HOA_HONG_DL = Utils.CIntDef(txtPM_HoaHong_DL.Text.Replace(",", ""));
                i.PM_HOA_HONG_VAT = Utils.CIntDef(txtPM_HoaHong_VAT.Text.Replace(",", ""));
                i.PM_HOA_HONG = Utils.CIntDef(txtPM_HoaHong.Text.Replace(",", ""));
                i.PM_TONG_TT_NCC = Utils.CIntDef(txtPM_TongTTNhaCC.Text.Replace(",", ""));
                i.PM_NGAY_HH_TB = txtNgayHetHanTB1.Text != "" ?
                    DateTime.ParseExact(txtNgayHetHanTB1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.PM_NGAY_HH_TB = null;
                #endregion

                i.TT_TK_CKS = Utils.CIntDef(txtTT_TokenCKS.Text.Replace(",", ""));
                i.TT_PHAN_MEM = Utils.CIntDef(txtTT_PM.Text.Replace(",", ""));
                i.TT_HH_CKS = Utils.CIntDef(txtTTHoaHong_CKS.Text.Replace(",", ""));
                i.TT_HH_PM = Utils.CIntDef(txtTTHoaHong_PM.Text.Replace(",", ""));
                i.CON_NO = Utils.CIntDef(txtTT_ConNo.Text.Replace(",", ""));
                i.NGAY_THU = txtTT_NgayThu.Text;
                i.GHI_CHU = txtGhiChu.Text;

                proj_data.Update(i);
                db.SubmitChanges();
            }
            else
            {
                CONG_NO_CK i = new CONG_NO_CK();
                i.STT = Utils.CIntDef(txtSTT.Text);
                i.TEN_CTY = txtTenKH.Text;
                i.MST = txtMST.Text;
                i.NGAY_NHAN_TB = txtNgayNhanTB.Text != "" ?
                    DateTime.ParseExact(txtNgayNhanTB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.NGAY_NHAN_TB = null;
                i.NV_KD = Utils.CIntDef(ddlNVKD.SelectedValue);
                i.TINH_TRANG = ddlTinhTrang.Text;
                i.LOAI_HOPDONG = ddlLoaiHD.Text;

                #region CKS
                i.CKS_NHA_CUNG_CAP = ddlNhaCungCap.SelectedValue;
                i.CKS_SAN_PHAM = ddlSanPham.SelectedValue;
                i.CKS_PHI_DV = Utils.CIntDef(txtCKS_PhiDV.Text.Replace(",", ""));
                i.CKS_GIA_TK = Utils.CIntDef(txtCKS_GiaTbToken.Text.Replace(",", ""));
                i.CKS_VAT = Utils.CIntDef(txtCKS_VAT.Text.Replace(",", ""));
                i.CKS_TONG_CONG = Utils.CIntDef(txtCKS_TongCong.Text.Replace(",", ""));
                i.CKS_HOA_HONG_TL = Utils.CIntDef(txtCKS_HoaHong_TyLe.Text.Replace(",", ""));
                i.CKS_HOA_HONG_DL = Utils.CIntDef(txtCKS_HoaHong_DL.Text.Replace(",", ""));
                i.CKS_HOA_HONG_VAT = Utils.CIntDef(txtCKS_HoaHong_VAT.Text.Replace(",", ""));
                i.CKS_HOA_HONG = Utils.CIntDef(txtCKS_HoaHong.Text.Replace(",", ""));
                i.CKS_TONG_TT_NCC = Utils.CIntDef(txtCKS_TongTTNhaCC.Text.Replace(",", ""));
                i.CKS_NGAY_HH_TB = txtNgayHetHanTB.Text != "" ?
                    DateTime.ParseExact(txtNgayHetHanTB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.CKS_NGAY_HH_TB = null;
                #endregion

                #region PM
                i.PM_NHA_CUNG_CAP = ddlNhaCungCap1.SelectedValue;
                i.PM_SAN_PHAM = ddlSanPham1.SelectedValue;
                i.PM_PHI_DV = Utils.CIntDef(txtPM_PhiDV.Text.Replace(",", ""));
                i.PM_GIA_TK = Utils.CIntDef(txtPM_GiaTbToken.Text.Replace(",", ""));
                i.PM_VAT = Utils.CIntDef(txtPM_VAT.Text.Replace(",", ""));
                i.PM_TONG_CONG = Utils.CIntDef(txtPM_TongCong.Text.Replace(",", ""));
                i.PM_HOA_HONG_TL = Utils.CIntDef(txtPM_HoaHong_TyLe.Text.Replace(",", ""));
                i.PM_HOA_HONG_DL = Utils.CIntDef(txtPM_HoaHong_DL.Text.Replace(",", ""));
                i.PM_HOA_HONG_VAT = Utils.CIntDef(txtPM_HoaHong_VAT.Text.Replace(",", ""));
                i.PM_HOA_HONG = Utils.CIntDef(txtPM_HoaHong.Text.Replace(",", ""));
                i.PM_TONG_TT_NCC = Utils.CIntDef(txtPM_TongTTNhaCC.Text.Replace(",", ""));
                i.PM_NGAY_HH_TB = txtNgayHetHanTB.Text != "" ?
                    DateTime.ParseExact(txtNgayHetHanTB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : i.PM_NGAY_HH_TB = null;
                #endregion

                i.TT_TK_CKS = Utils.CIntDef(txtTT_TokenCKS.Text.Replace(",", ""));
                i.TT_PHAN_MEM = Utils.CIntDef(txtTT_PM.Text.Replace(",", ""));
                i.TT_HH_CKS = Utils.CIntDef(txtTTHoaHong_CKS.Text.Replace(",", ""));
                i.TT_HH_PM = Utils.CIntDef(txtTTHoaHong_PM.Text.Replace(",", ""));
                i.CON_NO = Utils.CIntDef(txtTT_ConNo.Text.Replace(",", ""));
                i.NGAY_THU = txtTT_NgayThu.Text;
                i.GHI_CHU = txtGhiChu.Text;

                proj_data.Create(i);

                var getlink = db.CONG_NO_CKs.OrderByDescending(n => n.ID).Take(1).ToList();
                if (getlink.Count > 0)
                {
                    id = Utils.CIntDef(getlink[0].ID);
                    return 1;
                }
            }
            return 0;
        }
        private void Load_MaNCC(DropDownList dll, int type)
        {
            var list = _NhaCungCapData.GetListByType("", type);
            dll.DataTextField = "NCC_MA";
            dll.DataValueField = "NCC_MA";
            dll.DataSource = list;
            dll.DataBind();

            ListItem l = new ListItem("---Chọn nhà cung cấp---", "0", true);
            dll.Items.Insert(0, l);
            dll.SelectedIndex = 0;
        }
        private void Load_MaSP(string maNCC, DropDownList ddl)
        {
            var list = _SanPhamData.GetListByNCC(maNCC);
            ddl.DataTextField = "SP_MA";
            ddl.DataValueField = "SP_MA";
            ddl.DataSource = list;
            ddl.DataBind();

            ListItem l = new ListItem("---Chọn sản phẩm---", "0", true);
            ddl.Items.Insert(0, l);
            ddl.SelectedIndex = 0;
        }
        private void Load_PhiSP(string maSP, int iType)
        {
            if (maSP != null)
            {
                var obj = _SanPhamData.GetListByName(maSP);
                if (obj.Count > 0)
                {
                    //Lấy ra ngày nhận thiết bị
                    var i = proj_data.GetById(id);
                    if (iType == 0)
                    {
                        txtCKS_PhiDV.Text = obj[0].SP_PHIDV != null ? obj[0].SP_PHIDV.Value.ToString("###,##0") : "0";
                        txtCKS_GiaTbToken.Text = obj[0].SP_GIATB != null ? obj[0].SP_GIATB.Value.ToString("###,##0") : "0";
                        txtCKS_VAT.Text = obj[0].SP_VAT != null ? obj[0].SP_VAT.Value.ToString("###,##0") : "0";
                        txtCKS_TongCong.Text = obj[0].SP_TONGPHI != null ? obj[0].SP_TONGPHI.Value.ToString("###,##0") : "0";
                        txtNgayHetHanTB.Text = i.NGAY_NHAN_TB.Value.AddMonths(Utils.CIntDef(obj[0].SP_SOTHANG != null ? obj[0].SP_SOTHANG : 0)).ToShortDateString();
                    }
                    else
                    {
                        txtPM_PhiDV.Text = obj[0].SP_PHIDV != null ? obj[0].SP_PHIDV.Value.ToString("###,##0") : "0";
                        txtPM_GiaTbToken.Text = obj[0].SP_GIATB != null ? obj[0].SP_GIATB.Value.ToString("###,##0") : "0";
                        txtPM_VAT.Text = obj[0].SP_VAT != null ? obj[0].SP_VAT.Value.ToString("###,##0") : "0";
                        txtPM_TongCong.Text = obj[0].SP_TONGPHI != null ? obj[0].SP_TONGPHI.Value.ToString("###,##0") : "0";
                        txtNgayHetHanTB1.Text = i.NGAY_NHAN_TB.Value.AddMonths(Utils.CIntDef(obj[0].SP_SOTHANG != null ? obj[0].SP_SOTHANG : 0)).ToShortDateString();
                    }
                }
            }
        }
        private void Load_PhiHH_DL(string maNCC, int iType)
        {
            var obj = _NhaCungCapData.GetListByName(maNCC);
            if (obj.Count > 0)
            {
                if (iType == 0)
                {
                    double tyLeHoaHong = Utils.CDblDef(obj[0].NCC_HOA_HONG, 0);
                    double phiDV = Utils.CDblDef(txtCKS_PhiDV.Text.Replace(",", ""));
                    double tongCongPhi = Utils.CDblDef(txtCKS_TongCong.Text.Replace(",", ""));
                    double phiHoaHong = (tyLeHoaHong * phiDV) / 100;
                    txtCKS_HoaHong_TyLe.Text = String.Format("{0:###,##0}", tyLeHoaHong);
                    txtCKS_HoaHong_DL.Text = String.Format("{0:###,##0}", phiHoaHong);
                    txtCKS_HoaHong_VAT.Text = String.Format("{0:###,##0}", phiHoaHong * 0.1);
                    txtCKS_HoaHong.Text = String.Format("{0:###,##0}", phiHoaHong * 1.1);
                    txtCKS_TongTTNhaCC.Text = String.Format("{0:###,##0}", tongCongPhi - phiHoaHong * 1.1);
                }
                else
                {
                    double tyLeHoaHong = Utils.CDblDef(obj[0].NCC_HOA_HONG, 0);
                    double phiDV = Utils.CDblDef(txtPM_PhiDV.Text.Replace(",", ""));
                    double tongCongPhi = Utils.CDblDef(txtPM_TongCong.Text.Replace(",", ""));
                    double phiHoaHong = (tyLeHoaHong * phiDV) / 100;
                    txtPM_HoaHong_TyLe.Text = String.Format("{0:###,##0}", tyLeHoaHong);
                    txtPM_HoaHong_DL.Text = String.Format("{0:###,##0}", phiHoaHong);
                    txtPM_HoaHong_VAT.Text = String.Format("{0:###,##0}", phiHoaHong * 0.1);
                    txtPM_HoaHong.Text = String.Format("{0:###,##0}", phiHoaHong * 1.1);
                    txtPM_TongTTNhaCC.Text = String.Format("{0:###,##0}", tongCongPhi - phiHoaHong * 1.1);
                }
            }
        }
        #endregion

        #region Funtion
        private DateTime fmDate(TextBox txt)
        {
            DateTime _date = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return _date;
        }
        private void Load_ConNo()
        {
            double tokenCKS = Utils.CDblDef(txtTT_TokenCKS.Text.Replace(",",""), 0);
            double PM = Utils.CDblDef(txtTT_PM.Text.Replace(",", ""), 0);
            double tongCKS = Utils.CDblDef(txtCKS_TongCong.Text.Replace(",", ""), 0);
            double tongPM = Utils.CDblDef(txtPM_TongCong.Text.Replace(",", ""), 0);
            double hhCKS = Utils.CDblDef(txtTTHoaHong_CKS.Text.Replace(",", ""), 0);
            double hhPM = Utils.CDblDef(txtTTHoaHong_PM.Text.Replace(",", ""), 0);
            double conNo = tongCKS + tongPM - hhCKS - hhPM;
            txtTT_ConNo.Text = String.Format("{0:###,##0}", conNo - (tokenCKS + PM));
        }
        #endregion

        #region Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //=1 là Create
            if(Save_Data() == 1)
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.document.getElementById('openCongNoCKS(" + id + ")').onclick = parent.openCongNoCKS(" + id + ");</script>");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }
        protected void ddlNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_MaSP(ddlNhaCungCap.SelectedValue, ddlSanPham);
        }
        protected void ddlSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSanPham.SelectedValue != null && ddlSanPham.SelectedValue != "0")
            {
                Load_PhiSP(ddlSanPham.SelectedValue, 0);
                Load_PhiHH_DL(ddlNhaCungCap.SelectedValue, 0);
            }
        }
        protected void ddlNhaCungCap1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_MaSP(ddlNhaCungCap1.SelectedValue, ddlSanPham1);
        }
        protected void ddlSanPham1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSanPham1.SelectedValue != null && ddlSanPham1.SelectedValue != "0")
            {
                Load_PhiSP(ddlSanPham1.SelectedValue, 1);
                Load_PhiHH_DL(ddlNhaCungCap1.SelectedValue, 1);
            }
        }
        protected void txtTT_TokenCKS_TextChanged(object sender, EventArgs e)
        {
            txtTT_TokenCKS.Text = String.Format("{0:###,##0}", Utils.CDblDef(txtTT_TokenCKS.Text, 0));
            Load_ConNo();
        }
        protected void txtTT_PM_TextChanged(object sender, EventArgs e)
        {
            txtTT_PM.Text = String.Format("{0:###,##0}", Utils.CDblDef(txtTT_PM.Text, 0));
            Load_ConNo();
        }
        protected void txtTTHoaHong_CKS_TextChanged(object sender, EventArgs e)
        {
            txtTTHoaHong_CKS.Text = String.Format("{0:###,##0}", Utils.CDblDef(txtTTHoaHong_CKS.Text, 0));
            Load_ConNo();
        }
        protected void txtTTHoaHong_PM_TextChanged(object sender, EventArgs e)
        {
            txtTTHoaHong_PM.Text = String.Format("{0:###,##0}", Utils.CDblDef(txtTTHoaHong_PM.Text, 0));
            Load_ConNo();
        }
        #endregion
    }
}