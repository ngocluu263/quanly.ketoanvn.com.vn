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
    public partial class chi_tiet_loai_hinh_doanh_nghiep : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        UnitData unit_data = new UnitData();
        int _menuid = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _menuid = Utils.CIntDef(Request.QueryString["menuid"]);
            if (!IsPostBack)
            {
                LoadTypeParent();
                Getinfo();
            }
            Drmenu_parent_SelectedIndexChanged(sender, e);
        }
        #region Getinfo
        private void LoadTypeParent()
        {
            try
            {
                var CatList = (
                                from t2 in db.TYPE_COMPANies
                                select new
                                {
                                    TYPE_ID = t2.TYPE_NAME == "-------Root-------" ? 0 : t2.TYPE_ID,
                                    TYPE_PARENT = t2.TYPE_PARENT,
                                    TYPE_RANK = t2.TYPE_RANK,
                                    TYPE_NAME = t2.TYPE_NAME
                                }
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["TYPE_ID"] };
                    relCat = new DataRelation("TYPE_PARENT", ds.Tables[0].Columns["TYPE_ID"], ds.Tables[0].Columns["TYPE_PARENT"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    unit_data.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    Drmenu_parent.DataSource = dsCat.Tables[0];
                    Drmenu_parent.DataTextField = "TYPE_NAME";
                    Drmenu_parent.DataValueField = "TYPE_ID";
                    Drmenu_parent.DataBind();

                }
                else
                {
                    DataTable dt = new DataTable("Newtable");

                    dt.Columns.Add(new DataColumn("TYPE_ID"));
                    dt.Columns.Add(new DataColumn("TYPE_NAME"));

                    DataRow row = dt.NewRow();
                    row["TYPE_ID"] = 0;
                    row["TYPE_NAME"] = "-------Root-------";
                    dt.Rows.Add(row);

                    Drmenu_parent.DataTextField = "TYPE_NAME";
                    Drmenu_parent.DataValueField = "TYPE_ID";
                    Drmenu_parent.DataSource = dt;
                    Drmenu_parent.DataBind();



                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public void Getinfo()
        {
            try
            {
                var list = db.TYPE_COMPANies.Where(n => n.TYPE_ID == _menuid).ToList();
                if (list.Count > 0)
                {
                    Txtname.Text = list[0].TYPE_NAME;
                    rblActive.SelectedValue = list[0].TYPE_ACTIVE.ToString();
                    rdbTypeReg.SelectedValue = list[0].TYPE_REG.ToString();
                    Drmenu_parent.SelectedValue = list[0].TYPE_PARENT.ToString();
                    txtOrderby.Text = Utils.CStrDef(list[0].ORDERBY);
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
                int _idroot = Utils.CIntDef(Drmenu_parent.SelectedValue);
                int rank = 1;
                int _idpar = 0;
                var getrank = db.TYPE_COMPANies.Where(n => n.TYPE_ID == _idroot).ToList();
                if (getrank.Count > 0)
                {
                    rank += Utils.CIntDef(getrank[0].TYPE_RANK);
                    _idpar = getrank[0].TYPE_ID;
                }

                if (_menuid == 0)
                {
                    TYPE_COMPANY menu = new TYPE_COMPANY();
                    menu.TYPE_PARENT = _idpar;
                    menu.TYPE_RANK = rank;
                    menu.TYPE_NAME = Txtname.Text;
                    menu.TYPE_REG = Utils.CIntDef(rdbTypeReg.SelectedValue);
                    menu.ORDERBY = Utils.CIntDef(txtOrderby.Text);
                    menu.TYPE_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                    db.TYPE_COMPANies.InsertOnSubmit(menu);
                    db.SubmitChanges();
                    var getlink = db.TYPE_COMPANies.OrderByDescending(n => n.TYPE_ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-loai-hinh-doanh-nghiep.aspx?menuid=" + getlink[0].TYPE_ID : strLink;
                    }
                }
                else
                {
                    var list = db.TYPE_COMPANies.Where(n => n.TYPE_ID == _menuid).ToList();
                    foreach (var i in list)
                    {
                        i.TYPE_RANK = rank;
                        i.TYPE_PARENT = _idpar;
                        i.TYPE_NAME = Txtname.Text;
                        i.TYPE_REG = Utils.CIntDef(rdbTypeReg.SelectedValue);
                        i.ORDERBY = Utils.CIntDef(txtOrderby.Text);
                        i.TYPE_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-loai-hinh-doanh-nghiep.aspx?menuid=" + _menuid : strLink;
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
                var list = db.TYPE_COMPANies.Where(n => n.TYPE_ID == _menuid).ToList();
                db.TYPE_COMPANies.DeleteAllOnSubmit(list);
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
            Save("danh-sach-loai-hinh-doanh-nghiep.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-loai-hinh-doanh-nghiep.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("danh-sach-loai-hinh-doanh-nghiep.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-loai-hinh-doanh-nghiep.aspx");
        }
        #endregion

        protected void Drmenu_parent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.CStrDef(Drmenu_parent.SelectedItem).Contains("-------Root-------"))
            {
                iTypeReg.Visible = false;
            }
            else iTypeReg.Visible = true;
        }
    }
}