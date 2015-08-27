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
        UnitData _UnitData = new UnitData();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                
            }
            multiselect_to.Multiple = true;
        }

        #region Getinfo
        private void Load_User()
        {
            int _type = Utils.CIntDef(ddlMenu.SelectedValue);
            List<int> listItems = (from a in db.USER_PERMISSIONs where a.PER_TYPE == _type select Utils.CIntDef(a.PER_USER, 0)).ToList();

            db = new AppketoanDataContext();
            var list = (from a in db.USERs
                        join b in db.GROUPs on a.GROUP_ID equals b.GROUP_ID
                        where (a.GROUP_ID != 1 && a.GROUP_ID != 2 && !listItems.Contains(a.USER_ID))
                        select new
                        {
                            a.USER_ID,
                            a.GROUP_ID,
                            namGroup = b.GROUP_NAME + " (" + a.USER_NAME + ")"
                        }).OrderBy(n => n.USER_ID).OrderBy(n => n.GROUP_ID).ToList();
            if (list.Count > 0)
            {
                multiselect.DataSource = list;
                multiselect.DataTextField = "namGroup";
                multiselect.DataValueField = "USER_ID";
                multiselect.DataBind();
                multiselect.Multiple = true;
            }
        }
        private void Load_Grid()
        {
            int _type = Utils.CIntDef(ddlMenu.SelectedValue);
            var list = _UserPermissionData.GetByType(_type);
            grvData.DataSource = list;
            grvData.DataBind();
        }
        #endregion
        #region Savedata
        private void Save_Data()
        {
            try
            {
                List<ListItem> ilist= multiselect_to.Items.Cast<ListItem>().ToList();
                if (ilist.Count > 0)
                {
                    foreach (var i in ilist)
                    {
                        USER_PERMISSION u = new USER_PERMISSION();
                        u.PER_TYPE = Utils.CIntDef(ddlMenu.SelectedValue);
                        u.PER_GROUP = _UnitData.GetIdGroupByUser(Utils.CIntDef(i.Value));
                        u.PER_USER = Utils.CIntDef(i.Value);
                        u.PER_VIEW = chkFuntion.Items[0].Selected == true ? true : false;
                        u.PER_ADD = chkFuntion.Items[1].Selected == true ? true : false;
                        u.PER_EDIT = chkFuntion.Items[2].Selected == true ? true : false;
                        u.PER_DELE = chkFuntion.Items[3].Selected == true ? true : false;

                        _UserPermissionData.Create(u);
                        db.SubmitChanges();
                    }
                }
            }
            catch (Exception) { throw; }
        }
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
                    _items.Add(new InfoFun { Text = "Xem", Field = "1" });
                    _items.Add(new InfoFun { Text = "Thêm", Field = "2" });
                    _items.Add(new InfoFun { Text = "Sửa", Field = "3" });
                    _items.Add(new InfoFun { Text = "Xóa", Field = "4" });
                    break;
                case 2:
                    _items.Add(new InfoFun { Text = "Xem", Field = "1" });
                    _items.Add(new InfoFun { Text = "Thêm", Field = "2" });
                    _items.Add(new InfoFun { Text = "Sửa", Field = "3" });
                    _items.Add(new InfoFun { Text = "Xóa", Field = "4" });
                    break;
                case 3:
                    _items.Add(new InfoFun { Text = "Xem", Field = "1" });
                    _items.Add(new InfoFun { Text = "Thêm", Field = "2" });
                    _items.Add(new InfoFun { Text = "Sửa", Field = "3" });
                    _items.Add(new InfoFun { Text = "Xóa", Field = "4" });
                    break;
            }
            if (_items.Count > 0)
            {
                Load_User();
                Load_Grid();

                chkFuntion.DataTextField = "Text";
                chkFuntion.DataValueField = "Field";
                chkFuntion.DataSource = _items;
                chkFuntion.DataBind();
            }
        }
        protected void btnDown_Click(object sender, EventArgs e)
        {
            Save_Data();
            Load_Grid();
            multiselect_to.Items.Clear();
            Load_User();
        }
        protected void lbtnRightAll_Click(object sender, EventArgs e)
        {
            List<ListItem> ilist = multiselect.Items.Cast<ListItem>().Where(n => n.Selected).ToList();
            if (ilist.Count > 0)
            {
                int k = 0;
                foreach (var i in ilist)
                {
                    //Thêm vào mulTo
                    ListItem l = new ListItem(i.Text, i.Value, true);
                    multiselect_to.Items.Insert(k, l);
                    k++;

                    //Xóa khỏi mul
                    multiselect.Items.Remove(l);
                }
            }
        }
        protected void lbtnLeftAll_Click(object sender, EventArgs e)
        {
            List<ListItem> ilistNoCheck = multiselect_to.Items.Cast<ListItem>().Where(n => n.Selected == false).ToList();
            List<ListItem> ilist = multiselect_to.Items.Cast<ListItem>().Where(n => n.Selected).ToList();
            if (ilist.Count > 0)
            {
                int k = 0;
                foreach (var i in ilist)
                {
                    ListItem l = new ListItem(i.Text, i.Value, true);
                    multiselect.Items.Insert(k, l);
                    k++;

                    multiselect_to.Items.Remove(l);
                }
            }
        }
        protected void grvData_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int _key = Utils.CIntDef(e.Keys[0]);
            var obj = _UserPermissionData.GetById(_key);
            if (obj != null)
            {
                obj.PER_VIEW = Utils.CBoolDef(e.NewValues[0]);
                obj.PER_ADD = Utils.CBoolDef(e.NewValues[1]);
                obj.PER_EDIT = Utils.CBoolDef(e.NewValues[2]);
                obj.PER_DELE = Utils.CBoolDef(e.NewValues[3]);
                _UserPermissionData.Update(obj);
                db.SubmitChanges();

                Load_Grid();
            }
            e.Cancel = true;
            this.grvData.CancelEdit();
        }
        protected void grvData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int _key = Utils.CIntDef(e.Keys[0]);
            var obj = _UserPermissionData.GetById(_key);
            if (obj != null)
            {
                _UserPermissionData.Remove(_key);
                db.SubmitChanges();
            }
            Load_Grid();            
            e.Cancel = true;
            this.grvData.CancelEdit();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "helloworldpopup", "alert('hello world');", true);
            ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>alter('asdad');</script>");
        }
        protected void lbtnReload_Click(object sender, EventArgs e)
        {
            Load_User();
        }
        #endregion
        #region Funtion
        public class InfoFun
        {
            public string Text { get; set; }
            public string Field { get; set; }
        }
        public string GetUser(object _id)
        {
            var list = db.USERs.Where(a => a.USER_ID == Convert.ToInt32(_id)).ToList();
            if (list.Count > 0)
            {
                return list[0].USER_NAME;
            }
            else { return ""; }
        }
        public string GetNameGroup(object idGroup)
        {
            int _idGroup = Utils.CIntDef(idGroup);
            string str = _UnitData.GetNameGroupByUser(_idGroup); ;
            return str;
        }
        #endregion
    }
}