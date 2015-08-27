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
    public partial class popup_cong_no_web : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        private CongNoWebRepo _CongNoWebRepo = new CongNoWebRepo();
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"]);

            if (!IsPostBack)
            {
                Load_NhanVien(3, ddlNVKD);
                Load_NhanVien(3, ddlNVXL);
                Load_Data();
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }

        #region Data
        private void Load_NhanVien(int _idGroup, DropDownList _ddl)
        {
            try
            {
                var list = db.USERs.Where(n => n.GROUP_ID >= _idGroup).ToList();
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
                var i = _CongNoWebRepo.GetById(id);
                if (i != null)
                {
                    txtSohopdong.Text = Utils.CStrDef(i.SO_HOPDONG);
                    txtNgaykyhopdong.Value = string.Format("{0:dd/MM/yyyy}", i.NGAYKY_HOPDONG);
                    txtTenkhachhang.Text = i.TEN_KHACHHANG;
                    txtThongtinlienhe.Text = i.THONGTINLIENHE_KHACHHANG;
                    txtNoidung.Text = Utils.CStrDef(i.NOIDUNG);
                    txtTendomain.Text = Utils.CStrDef(i.TEN_DOMAIN);
                    ddlNVKD.SelectedValue = Utils.CStrDef(i.NVKD);
                    ddlNVXL.SelectedValue = Utils.CStrDef(i.NVXL);
                    txtDomain.Text = string.Format("{0:###,###}", i.DOMAIN_PRICE);
                    txtChiphitrienkhai.Text = string.Format("{0:###,###}", i.CHIPHI_TRIENKHAI_PRICE);
                    txtHosting.Text = string.Format("{0:###,###}", i.HOSTING_PRICE);
                    txtWeb.Text = string.Format("{0:###,###}", i.WEB_PRICE);
                    txtLogo.Text = string.Format("{0:###,###}", i.LOGO_PRICE);
                    txtEsell.Text = string.Format("{0:###,###}", i.ESELL_PRICE);
                    txtChuphinh.Text = string.Format("{0:###,###}", i.CHUPHINH_PRICE);
                    txtCatalogue.Text = string.Format("{0:###,###}", i.CATALOGUE_PRICE);
                    txtSeotukhoa.Text = string.Format("{0:###,###}", i.SEOTUKHOA_PRICE);
                    txtGoogleadword.Text = string.Format("{0:###,###}", i.GOOGLEADWORD_PRICE);
                    txtPhanmem.Text = string.Format("{0:###,###}", i.PHANMEM_PRICE);
                    txtHoahongkh.Text = string.Format("{0:###,###}", i.HOAHONGKH_PRICE);
                    txtVAT.Text = string.Format("{0:###,###}", i.VAT);
                    txtTongcong.Text = string.Format("{0:###,###}", i.TONGCONG);
                    txtThanhtoan.Text = string.Format("{0:###,###}", i.THANHTOAN);
                    txtNgaythanhtoan.Text = i.NGAYTHANHTOAN;
                    txtNgayxuathoadon.Text = i.NGAYXUATHOADON;
                    txtCongno.Text = string.Format("{0:###,###}", i.CONGNO);
                    txtGhichu.Text = i.GHICHU;
                    ddlTinhtrang.SelectedValue = Utils.CStrDef(i.TINHTRANG);
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
                var item = _CongNoWebRepo.GetById(id);
                item.SO_HOPDONG = txtSohopdong.Text;
                item.NGAYKY_HOPDONG = DateTime.ParseExact(txtNgaykyhopdong.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                item.TEN_KHACHHANG = txtTenkhachhang.Text;
                item.THONGTINLIENHE_KHACHHANG = txtThongtinlienhe.Text;
                item.NOIDUNG = txtNoidung.Text;
                item.TEN_DOMAIN = txtTendomain.Text;
                item.NVKD = Utils.CIntDef(ddlNVKD.SelectedValue);
                item.NVXL = Utils.CIntDef(ddlNVXL.SelectedValue);
                item.DOMAIN_PRICE = Utils.CDecDef(txtDomain.Text.Replace(",", ""));
                item.CHIPHI_TRIENKHAI_PRICE = Utils.CDecDef(txtChiphitrienkhai.Text.Replace(",", ""));
                item.HOSTING_PRICE = Utils.CDecDef(txtHosting.Text.Replace(",", ""));
                item.WEB_PRICE = Utils.CDecDef(txtWeb.Text.Replace(",", ""));
                item.LOGO_PRICE = Utils.CDecDef(txtLogo.Text.Replace(",", ""));
                item.ESELL_PRICE = Utils.CDecDef(txtEsell.Text.Replace(",", ""));
                item.CHUPHINH_PRICE = Utils.CDecDef(txtChuphinh.Text.Replace(",", ""));
                item.CATALOGUE_PRICE = Utils.CDecDef(txtCatalogue.Text.Replace(",", ""));
                item.SEOTUKHOA_PRICE = Utils.CDecDef(txtSeotukhoa.Text.Replace(",", ""));
                item.GOOGLEADWORD_PRICE = Utils.CDecDef(txtGoogleadword.Text.Replace(",", ""));
                item.PHANMEM_PRICE = Utils.CDecDef(txtPhanmem.Text.Replace(",", ""));
                item.HOAHONGKH_PRICE = Utils.CDecDef(txtHoahongkh.Text.Replace(",", ""));
                item.VAT = Utils.CDecDef(txtVAT.Text.Replace(",", ""));
                item.TONGCONG = Utils.CDecDef(txtTongcong.Text.Replace(",", ""));
                item.THANHTOAN = Utils.CDecDef(txtThanhtoan.Text.Replace(",", ""));
                item.NGAYTHANHTOAN = txtNgaythanhtoan.Text;
                item.NGAYXUATHOADON = txtNgayxuathoadon.Text;
                item.CONGNO = Utils.CDecDef(txtCongno.Text.Replace(",", ""));
                item.GHICHU = txtGhichu.Text;
                item.TINHTRANG = Utils.CIntDef(ddlTinhtrang.SelectedValue);

                _CongNoWebRepo.Update(item);
            }
            else
            {
                CONG_NO_WEB item = new CONG_NO_WEB();
                item.SO_HOPDONG = txtSohopdong.Text;
                item.NGAYKY_HOPDONG = DateTime.ParseExact(txtNgaykyhopdong.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                item.TEN_KHACHHANG = txtTenkhachhang.Text;
                item.THONGTINLIENHE_KHACHHANG = txtThongtinlienhe.Text;
                item.NOIDUNG = txtNoidung.Text;
                item.TEN_DOMAIN = txtTendomain.Text;
                item.NVKD = Utils.CIntDef(ddlNVKD.SelectedValue);
                item.NVXL = Utils.CIntDef(ddlNVXL.SelectedValue);
                item.DOMAIN_PRICE = Utils.CDecDef(txtDomain.Text.Replace(",", ""));
                item.CHIPHI_TRIENKHAI_PRICE = Utils.CDecDef(txtChiphitrienkhai.Text.Replace(",", ""));
                item.HOSTING_PRICE = Utils.CDecDef(txtHosting.Text.Replace(",", ""));
                item.WEB_PRICE = Utils.CDecDef(txtWeb.Text.Replace(",", ""));
                item.LOGO_PRICE = Utils.CDecDef(txtLogo.Text.Replace(",", ""));
                item.ESELL_PRICE = Utils.CDecDef(txtEsell.Text.Replace(",", ""));
                item.CHUPHINH_PRICE = Utils.CDecDef(txtChuphinh.Text.Replace(",", ""));
                item.CATALOGUE_PRICE = Utils.CDecDef(txtCatalogue.Text.Replace(",", ""));
                item.SEOTUKHOA_PRICE = Utils.CDecDef(txtSeotukhoa.Text.Replace(",", ""));
                item.GOOGLEADWORD_PRICE = Utils.CDecDef(txtGoogleadword.Text.Replace(",", ""));
                item.PHANMEM_PRICE = Utils.CDecDef(txtPhanmem.Text.Replace(",", ""));
                item.HOAHONGKH_PRICE = Utils.CDecDef(txtHoahongkh.Text.Replace(",", ""));
                item.VAT = Utils.CDecDef(txtVAT.Text.Replace(",", ""));
                item.TONGCONG = Utils.CDecDef(txtTongcong.Text.Replace(",", ""));
                item.THANHTOAN = Utils.CDecDef(txtThanhtoan.Text.Replace(",", ""));
                item.NGAYTHANHTOAN = txtNgaythanhtoan.Text;
                item.NGAYXUATHOADON = txtNgayxuathoadon.Text;
                item.CONGNO = Utils.CDecDef(txtCongno.Text.Replace(",", ""));
                item.GHICHU = txtGhichu.Text;
                item.TINHTRANG = Utils.CIntDef(ddlTinhtrang.SelectedValue);

                _CongNoWebRepo.Create(item);
                id = item.ID;
                return 1;
            }
            return 0;
        }
        #endregion

        #region Funtion
        private DateTime fmDate(TextBox txt)
        {
            DateTime _date = DateTime.ParseExact(txt.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return _date;
        }
        #endregion

        #region Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //=1 là Create
            if(Save_Data() == 1)
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.document.getElementById('openCongNoWeb(" + id + ")').onclick = parent.openCongNoWeb(" + id + ");</script>");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }        
        #endregion
    }
}