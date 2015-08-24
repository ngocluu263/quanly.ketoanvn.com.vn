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
    public partial class chi_tiet_bang_luong : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        BangLuongData _BangLuongData = new BangLuongData();
        DoanhThuSPData _DoanhThuSPData = new DoanhThuSPData();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                Getinfo();
                Load_Grid();
            }
        }
        #region Getinfo
        public void Getinfo()
        {
            try
            {
                var obj = _BangLuongData.GetById(_id);
                if (obj != null)
                {
                    txtTenNV.Text = obj.BL_TENNV;
                    txtMaNV.Text = obj.BL_MANV;
                    txtLuongCanBan.Text = obj.BL_LUONG_CB != null ? obj.BL_LUONG_CB.Value.ToString("###,##0") : "";
                    txtThuNhapTruocThue.Text = obj.BL_THUNHAP_TRUOCTHUE != null ? obj.BL_THUNHAP_TRUOCTHUE.Value.ToString("###,##0") : "";
                    txtTamUng.Text = obj.BL_TAMUNG != null ? obj.BL_TAMUNG.Value.ToString("###,##0") : "";
                    txtThueThuNhap.Text = obj.BL_THUE_THUNHAP != null ? obj.BL_THUE_THUNHAP.Value.ToString("###,##0") : "";
                    txtPCThuong.Text = obj.BL_PC_THUONG != null ? obj.BL_PC_THUONG.Value.ToString("###,##0") : "";
                    txtPCKhac.Text = obj.BL_PC_KHAC != null ? obj.BL_PC_KHAC.Value.ToString("###,##0") : "";
                    txtCTBHXH.Text = obj.BL_BHXH != null ? obj.BL_BHXH.Value.ToString("###,##0") : "";
                    txtCTBHYT.Text = obj.BL_BHYT != null ? obj.BL_BHYT.Value.ToString("###,##0") : "";
                    txtCTBHTN.Text = obj.BL_BHTN != null ? obj.BL_BHTN.Value.ToString("###,##0") : "";
                    txtCTKhac.Text = obj.BL_KHAUTRU_KHAC != null ? obj.BL_KHAUTRU_KHAC.Value.ToString("####,##0") : "";

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Load_Grid()
        {
            try
            {
                var obj = _BangLuongData.GetById(_id);
                if (obj != null)
                {
                    var list = _DoanhThuSPData.GetListByMaNV(obj.BL_MANV, Utils.CIntDef(obj.BL_THANG), Utils.CIntDef(obj.BL_NAM));
                    ASPxGridView1_request.DataSource = list;
                    ASPxGridView1_request.DataBind();
                }
            }
            catch //(Exception)
            {

                //throw;
            }
        }
        #endregion

        #region Data
        private void Delete()
        {
            try
            {
                var obj = _BangLuongData.GetById(_id);
                if (obj != null)
                {
                    db.LUONG_DANHSACHes.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    Response.Redirect("bang-luong.aspx");
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

        }
        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("bang-luong.aspx");
        }
        #endregion

        #region Event
        protected void ASPxGridView1_request_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            //Load_Grid();
        }
        protected void ASPxGridView1_request_PageIndexChanged(object sender, EventArgs e)
        {
            //Load_Grid();
        }
        protected void ASPxGridView1_request_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int _key = Utils.CIntDef(e.Keys[0]);
            var obj = _DoanhThuSPData.GetById(_key);
            if (obj != null)
            {
                obj.MST = Utils.CStrDef(e.NewValues[0]);
                obj.TEN_CTY = Utils.CStrDef(e.NewValues[1]);
                obj.DOANH_THU = Utils.CDecDef(e.NewValues[2]);
                obj.TYLE_LUONG = Utils.CIntDef(e.NewValues[3]);
                obj.LUONG_SP = Utils.CDecDef(e.NewValues[4]);
                obj.LUONG_DUAN_TV = Utils.CDecDef(e.NewValues[5]);
                _DoanhThuSPData.Update(obj);
                db.SubmitChanges();

                Load_Grid();
            }
            e.Cancel = true;
            this.ASPxGridView1_request.CancelEdit();
        }
        protected void ASPxGridView1_request_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int _key = Utils.CIntDef(e.Keys[0]);
            var obj = _DoanhThuSPData.GetById(_key);
            if (obj != null)
            {
                _DoanhThuSPData.Remove(_key);
                db.SubmitChanges();
            }
            Load_Grid();
            e.Cancel = true;
            this.ASPxGridView1_request.CancelEdit();
        }
        #endregion

        #region Funtion

        #endregion
    }
}