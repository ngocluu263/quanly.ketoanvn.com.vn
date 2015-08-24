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
    public partial class phan_quyen : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        UserPermissionData _UserPermissionData = new UserPermissionData();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                //Load_Group();
                Load_User();
            }
        }

        #region Getinfo
        //private void Load_Group()
        //{
        //    var list = _UnitData.GetListGroupPer();
        //    if (list.Count > 0)
        //    {
        //        ddlGroup.DataTextField = "GROUP_NAME";
        //        ddlGroup.DataValueField = "GROUP_TYPE";
        //        ddlGroup.DataSource = list;
        //        ddlGroup.DataBind();
        //    }
        //}
        private void Load_User()
        {
            var list = (from a in db.USERs
                        join b in db.GROUPs on a.GROUP_ID equals b.GROUP_ID
                        where (a.GROUP_ID != 1 && a.GROUP_ID != 2)
                        select new
                        {
                            a.USER_ID,
                            a.GROUP_ID,
                            namGroup = b.GROUP_NAME + " (" + a.USER_NAME + ")"
                        }).OrderBy(n => n.GROUP_ID).ToList();
            if (list.Count > 0)
            {
                multiselect.DataSource = list;
                multiselect.DataTextField = "namGroup";
                multiselect.DataValueField = "USER_ID";
                multiselect.DataBind();
            }
        }
        #endregion
        #region Savedata
        
        #endregion
        #region Event
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("trang-chu.aspx");
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = Utils.CIntDef(ddlMenu.SelectedValue);
            List<InfoFun> _items = new List<InfoFun>();
            switch (i)
            {
                case 1:
                    _items.Add(new InfoFun { Text = "Thêm", Field = "1" });
                    _items.Add(new InfoFun { Text = "Tùy chỉnh", Field = "2" });
                    _items.Add(new InfoFun { Text = "Xóa", Field = "3" });
                    break;
                case 2:
                    _items.Add(new InfoFun { Text = "Thêm", Field = "1" });
                    _items.Add(new InfoFun { Text = "Tùy chỉnh", Field = "2" });
                    _items.Add(new InfoFun { Text = "Xóa", Field = "3" });
                    break;
                case 3:
                    _items.Add(new InfoFun { Text = "Thêm", Field = "1" });
                    _items.Add(new InfoFun { Text = "Tùy chỉnh", Field = "2" });
                    _items.Add(new InfoFun { Text = "Xóa", Field = "3" });
                    break;
            }
            if (_items.Count > 0)
            {
                chkFuntion.DataTextField = "Text";
                chkFuntion.DataValueField = "Field";
                chkFuntion.DataSource = _items;
                chkFuntion.DataBind();
            }
        }
        protected void btnDown_Click(object sender, EventArgs e)
        {
            
        }
        #endregion
        #region Funtion
        public class InfoFun
        {
            public string Text { get; set; }
            public string Field { get; set; }
        }
        #endregion
        
    }
}