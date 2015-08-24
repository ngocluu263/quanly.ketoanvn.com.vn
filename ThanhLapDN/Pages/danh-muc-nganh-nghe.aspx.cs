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
    public partial class danh_muc_nganh_nghe : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        TradesData _trades = new TradesData();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                Load_CateDomain();
                Getinfo();
            }
        }
        #region Getinfo

        public void Getinfo()
        {
            try
            {
                var list = db.PROFILE_TRADEs.Where(n => n.ID == _id).ToList();
                if (list.Count > 0)
                {
                    txtTenNganhNghe.Text = list[0].TRAD_NAME;
                    txtThuTu.Text = Utils.CStrDef(list[0].TRAD_ORDER);
                    rblActive.SelectedValue = Utils.CStrDef(list[0].TRAD_ACTIVE);
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
                    PROFILE_TRADE i = new PROFILE_TRADE();
                    i.TRAD_NAME = txtTenNganhNghe.Text;
                    i.TRAD_ORDER = Utils.CIntDef(txtThuTu.Text);
                    i.TRAD_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                    _trades.Create(i);
                    var getlink = db.PROFILE_TRADEs.OrderByDescending(n => n.ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        strLink = string.IsNullOrEmpty(strLink) ? "danh-muc-nganh-nghe.aspx?id=" + getlink[0].ID : strLink;
                    }
                }
                else
                {
                    var list = db.PROFILE_TRADEs.Where(a => a.ID == _id).ToList();
                    foreach (var i in list)
                    {
                        i.TRAD_NAME = txtTenNganhNghe.Text;
                        i.TRAD_ORDER = Utils.CIntDef(txtThuTu.Text);
                        i.TRAD_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        _trades.Update(i);
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "danh-muc-nganh-nghe.aspx?id=" + _id : strLink;
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
                    _trades.Remove(Utils.CIntDef(item));
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
            Save("danh-muc-nganh-nghe.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("danh-muc-nganh-nghe.aspx");
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

        public void Load_CateDomain()
        {
            try
            {
                var list = _trades.GetListByName("");
                ASPxGridView1_request.DataSource = list;
                ASPxGridView1_request.DataBind();

            }
            catch //(Exception)
            {

                //throw;
            }
        }

        #endregion

        protected void ASPxGridView1_request_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Load_CateDomain();
        }

        protected void ASPxGridView1_request_PageIndexChanged(object sender, EventArgs e)
        {
            Load_CateDomain();
        }
    }
}