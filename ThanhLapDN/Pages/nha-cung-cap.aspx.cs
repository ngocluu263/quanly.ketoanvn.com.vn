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
    public partial class nha_cung_cap : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        NhaCungCapData _NhaCungCapData = new NhaCungCapData();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                Load_Grid();
                Getinfo();
            }
        }

        #region Getinfo
        public void Getinfo()
        {
            try
            {
                var i = _NhaCungCapData.GetById(_id);
                if (i != null)
                {
                    txtMaNCC.Text = i.NCC_MA;
                    txtTenNCC.Text = i.NCC_TEN;
                    txtHoaHong.Text = i.NCC_HOA_HONG.Value.ToString("###,##0").Replace(".", ",");
                    txtThuTu.Text = Utils.CStrDef(i.NCC_ORDER);
                    rblActive.SelectedValue = Utils.CStrDef(i.NCC_ACTIVE);
                    rdoType.SelectedValue = Utils.CStrDef(i.NCC_TYPE);
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
                    if (_NhaCungCapData.CheckCodeExists(txtMaNCC.Text.Trim()))
                    {
                        CKS_NHACUNGCAP i = new CKS_NHACUNGCAP();
                        i.NCC_MA = txtMaNCC.Text;
                        i.NCC_TEN = txtTenNCC.Text;
                        i.NCC_HOA_HONG = Utils.CDecDef(txtHoaHong.Text.Replace(",", ""));
                        i.NCC_ORDER = Utils.CIntDef(txtThuTu.Text);
                        i.NCC_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        i.NCC_TYPE = Utils.CIntDef(rdoType.SelectedValue);
                        _NhaCungCapData.Create(i);
                        var getlink = db.CKS_NHACUNGCAPs.OrderByDescending(n => n.ID).Take(1).ToList();
                        if (getlink.Count > 0)
                        {
                            strLink = string.IsNullOrEmpty(strLink) ? "nha-cung-cap.aspx?id=" + getlink[0].ID : strLink;
                        }
                    }
                    else {
                        string strScript = "<script>";
                        strScript += "alert('Cập nhật thất bại. Mã nhà cung cấp này đã tồn tại!');";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
                else
                {
                    var i = _NhaCungCapData.GetById(_id);
                    if(i != null)
                    {
                        i.NCC_MA = txtMaNCC.Text;
                        i.NCC_TEN = txtTenNCC.Text;
                        i.NCC_HOA_HONG = Utils.CDecDef(txtHoaHong.Text.Replace(",", ""));
                        i.NCC_ORDER = Utils.CIntDef(txtThuTu.Text);
                        i.NCC_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        i.NCC_TYPE = Utils.CIntDef(rdoType.SelectedValue);
                        _NhaCungCapData.Update(i);
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "nha-cung-cap.aspx?id=" + _id : strLink;
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
                    _NhaCungCapData.Remove(Utils.CIntDef(item));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Button
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
            Save("nha-cung-cap.aspx");
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("nha-cung-cap.aspx");
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("trang-chu.aspx");
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
                var list = _NhaCungCapData.GetListAll("");
                ASPxGridView1_request.DataSource = list;
                ASPxGridView1_request.DataBind();

            }
            catch //(Exception)
            {

                //throw;
            }
        }
        public string GetType(object type)
        {
            string s = "";
            if (Utils.CStrDef(type) == "0")
                s = "Chữ ký số";
            else s = "Nhà cung cấp";
            return s;
        }
        #endregion

        protected void ASPxGridView1_request_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Load_Grid();
        }
        protected void ASPxGridView1_request_PageIndexChanged(object sender, EventArgs e)
        {
            Load_Grid();
        }
    }
}