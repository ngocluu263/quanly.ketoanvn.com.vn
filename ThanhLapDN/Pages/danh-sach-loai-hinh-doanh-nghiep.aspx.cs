using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using vpro.functions;
using System.Data;
using ThanhLapDN.Data;

namespace ThanhLapDN.Pages
{
    public partial class danh_sach_loai_hinh_doanh_nghiep : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        UnitData unit_data = new UnitData();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCat();
            }
            else
            {
                if (HttpContext.Current.Session["companies.listmenucha"] != null)
                {
                    ASPxTreeList_menu.DataSource = HttpContext.Current.Session["companies.listmenucha"];
                    ASPxTreeList_menu.DataBind();
                }
            }
        }
        #region Loaddata
        private void LoadCat()
        {
            try
            {
                var AllList = (from g in db.TYPE_COMPANies
                               where g.TYPE_RANK > 0
                               select new
                               {
                                   g.TYPE_ID,
                                   g.TYPE_PARENT,
                                   g.TYPE_RANK,
                                   g.TYPE_ACTIVE,
                                   g.ORDERBY,
                                   g.TYPE_NAME
                               });

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    Session["companies.listmenucha"] = DataUtil.LINQToDataTable(AllList);
                    DataTable tbl = Session["companies.listmenucha"] as DataTable;

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["TYPE_ID"] };
                    relCat = new DataRelation("TYPE_PARENT", ds.Tables[0].Columns["TYPE_ID"], ds.Tables[0].Columns["TYPE_PARENT"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    unit_data.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    HttpContext.Current.Session["companies.listmenucha"] = dsCat.Tables[0];
                    ASPxTreeList_menu.DataSource = dsCat.Tables[0];
                    ASPxTreeList_menu.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        
        #endregion
        #region Function
        public string getlink(object serid)
        {
            return Utils.CIntDef(serid) > 0 ? "chi-tiet-loai-hinh-doanh-nghiep.aspx?menuid=" + Utils.CIntDef(serid) : "chi-tiet-loai-hinh-doanh-nghiep.aspx";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 1 ? "Kích hoạt" : "Chưa kích hoạt";
        }
        #endregion
        #region Buttion
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadCat();
        }
        protected void lbtnDelete_Click1(object sender, EventArgs e)
        {
            foreach (TreeListNode node in ASPxTreeList_menu.GetSelectedNodes())
            {
                int _idmenu = Utils.CIntDef(node.Key, 0);
                var list = db.TYPE_COMPANies.Where(n => n.TYPE_ID == _idmenu);
                db.TYPE_COMPANies.DeleteAllOnSubmit(list);
                db.SubmitChanges();
            }
            //Loadmenu();
            Response.Redirect("danh-sach-loai-hinh-doanh-nghiep.aspx");
        }
        #endregion
    }
}