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
    public partial class san_pham : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        SanPhamData _SanPhamData = new SanPhamData();
        NhaCungCapData _NhaCungCapData = new NhaCungCapData();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                Load_MaNCC();
                Load_Grid();
                Getinfo();
            }
        }

        #region Getinfo
        private void Load_MaNCC()
        {
            var obj = _NhaCungCapData.GetListAll("");
            if (obj.Count > 0)
            {
                ddlMaNCC.DataTextField = "NCC_MA";
                ddlMaNCC.DataValueField = "NCC_MA";
                ddlMaNCC.DataSource = obj;
                ddlMaNCC.DataBind();

                ListItem l = new ListItem("---Chọn nhà cung cấp---", "0", true);
                ddlMaNCC.Items.Insert(0, l);
                ddlMaNCC.SelectedIndex = 0;
            }
        }
        public void Getinfo()
        {
            try
            {
                var i = _SanPhamData.GetById(_id);
                if (i != null)
                {
                    txtMaSP.Text = i.SP_MA;
                    txtTenSP.Text = i.SP_TEN;
                    txtPhiDV.Text = i.SP_PHIDV != null ? i.SP_PHIDV.Value.ToString("###,##0") : "0";
                    txtGiaTB.Text = i.SP_GIATB != null ? i.SP_GIATB.Value.ToString("###,##0") : "0";
                    txtVAT.Text = i.SP_VAT != null ? i.SP_VAT.Value.ToString("###,##0") : "0";
                    txtTongGia.Text = i.SP_TONGPHI != null ? i.SP_TONGPHI.Value.ToString("###,##0") : "0";
                    txtSoThang.Text = i.SP_SOTHANG != null ? i.SP_SOTHANG.Value.ToString("###,##0") : "0";
                    ddlMaNCC.Text = i.NCC_MA;
                    rblActive.SelectedValue = Utils.CStrDef(i.SP_ACTIVE);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Savedata
        private void Save(string strLink = "")
        {
            try
            {
                if (_id == 0)
                {
                    if (_SanPhamData.CheckCodeExists(txtMaSP.Text.Trim()))
                    {
                        CKS_SANPHAM i = new CKS_SANPHAM();
                        i.SP_MA = txtMaSP.Text;
                        i.SP_TEN = txtTenSP.Text;
                        i.SP_PHIDV = Utils.CDecDef(txtPhiDV.Text.Replace(",", ""));
                        i.SP_GIATB = Utils.CDecDef(txtGiaTB.Text.Replace(",", ""));
                        i.SP_VAT = Utils.CDecDef(txtVAT.Text.Replace(",", ""));
                        i.SP_TONGPHI = Utils.CDecDef(txtTongGia.Text.Replace(",", ""));
                        i.SP_SOTHANG = Utils.CIntDef(txtSoThang.Text.Replace(",", ""));
                        i.NCC_MA = ddlMaNCC.SelectedValue;
                        i.SP_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        _SanPhamData.Create(i);
                        var getlink = db.CKS_SANPHAMs.OrderByDescending(n => n.ID).Take(1).ToList();
                        if (getlink.Count > 0)
                        {
                            strLink = string.IsNullOrEmpty(strLink) ? "san-pham.aspx?id=" + getlink[0].ID : strLink;
                        }
                    }
                    else {
                        string strScript = "<script>";
                        strScript += "alert('Cập nhật thất bại. Mã sản phẩm này đã tồn tại!');";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
                else
                {
                    var i = _SanPhamData.GetById(_id);
                    if(i != null)
                    {
                        i.SP_MA = txtMaSP.Text;
                        i.SP_TEN = txtTenSP.Text;
                        i.SP_PHIDV = Utils.CDecDef(txtPhiDV.Text.Replace(",", ""));
                        i.SP_GIATB = Utils.CDecDef(txtGiaTB.Text.Replace(",", ""));
                        i.SP_VAT = Utils.CDecDef(txtVAT.Text.Replace(",", ""));
                        i.SP_TONGPHI = Utils.CDecDef(txtTongGia.Text.Replace(",", ""));
                        i.SP_SOTHANG = Utils.CIntDef(txtSoThang.Text.Replace(",", ""));
                        i.NCC_MA = ddlMaNCC.SelectedValue;
                        i.SP_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        _SanPhamData.Update(i);
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "san-pham.aspx?id=" + _id : strLink;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                if (!string.IsNullOrEmpty(strLink))
                {
                    Response.Redirect(strLink);
                }
            }
        }
        private void Delete()
        {
            try
            {
                List<object> fieldValues = ASPxGridView1_request.GetSelectedFieldValues(new string[] { "ID" });
                foreach (var item in fieldValues)
                {
                    _SanPhamData.Remove(Utils.CIntDef(item));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            Save("trang-chu.aspx");
        }
        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("san-pham.aspx");
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("san-pham.aspx");
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("trang-chu.aspx");
        }
        protected void lbtnTinhVTongGia_Click(object sender, EventArgs e)
        {
            double phiDV = Utils.CDblDef(txtPhiDV.Text, 0);
            double giaTB = Utils.CDblDef(txtGiaTB.Text, 0);
            double phanTramVat = Utils.CDblDef(txtPhanTramVAT.Text, 0);
            double phiVat = 0;
            double tongGia = 0;
            //Tính Phí VAT
            phiVat = ((phiDV + giaTB) * phanTramVat) / 100;
            //Tính tổng giá
            tongGia = phiDV + giaTB + phiVat;
            //Gán giá trị
            txtVAT.Text = String.Format("{0:###,##0}", phiVat);
            txtTongGia.Text = String.Format("{0:###,##0}", tongGia);
        }
        protected void ASPxGridView1_request_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Load_Grid();
        }
        protected void ASPxGridView1_request_PageIndexChanged(object sender, EventArgs e)
        {
            Load_Grid();
        }
        #endregion

        #region Funtion
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 0 ? "<img src='../Images/AcTive_Hide.png' width='20' heigh='20' />" : "<img src='../Images/AcTive.png' width='20' heigh='20' />";
        }
        public void Load_Grid()
        {
            try
            {
                var list = _SanPhamData.GetListAll("");
                ASPxGridView1_request.DataSource = list;
                ASPxGridView1_request.DataBind();
            }
            catch //(Exception)
            {

                //throw;
            }
        }
        #endregion
    }
}