using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;
using DevExpress.Web.ASPxTreeList;

namespace ThanhLapDN.Pages
{
    public partial class noi_hoat_dong_nhan_vien_giao_nhan : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        NhanVienGiaoNhanData _NhanVienGiaoNhanData = new NhanVienGiaoNhanData();
        UserRepo _UserRepo = new UserRepo();
        UnitData _UnitData = new UnitData();
        int _id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                TestPermission();
                Load_Grid();
                Load_UserGiaoNhan();
                Load_ThanhPho();
                Getinfo();
            }
            else
            {
                if (HttpContext.Current.Session["listmenuQuanHuyen"] != null)
                {
                    ASPxTreeList_menu.DataSource = HttpContext.Current.Session["listmenuQuanHuyen"];
                    ASPxTreeList_menu.DataBind();
                }
            }
        }

        #region Getinfo
        public void Getinfo()
        {
            try
            {
                var i = _NhanVienGiaoNhanData.GetById(_id);
                if (i != null)
                {
                    ddlNhanVienGN.SelectedValue = Utils.CStrDef(i.USER_ID);
                    ddlThanhPho.SelectedValue = Utils.CStrDef(i.PROP_ID);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Load_UserGiaoNhan()
        {
            var obj = _UserRepo.GetByGroup(6);
            if (obj.Count > 0)
            {
                ddlNhanVienGN.DataValueField = "USER_ID";
                ddlNhanVienGN.DataTextField = "USER_NAME";
                ddlNhanVienGN.DataSource = obj;
                ddlNhanVienGN.DataBind();
                ListItem l = new ListItem("---Chọn nhân viên---", "0", true);
                ddlNhanVienGN.Items.Insert(0, l);
                ddlNhanVienGN.SelectedIndex = 0;
            }
        }
        private void Load_ThanhPho()
        {
            var obj = _UnitData.Loadcity();
            if (obj.Count > 0)
            {
                ddlThanhPho.DataValueField = "PROP_ID";
                ddlThanhPho.DataTextField = "PROP_NAME";
                ddlThanhPho.DataSource = obj;
                ddlThanhPho.DataBind();
                ListItem l = new ListItem("---Chọn Tỉnh/Thành phố---", "0", true);
                ddlThanhPho.Items.Insert(0, l);
                ddlThanhPho.SelectedIndex = 0;
            }
        }
        #endregion

        #region Data
        private void Delete()
        {
            try
            {
                List<object> fieldValues = ASPxGridView1_request.GetSelectedFieldValues(new string[] { "USER_ID" });
                foreach (var item in fieldValues)
                {
                    //_NhanVienGiaoNhanData.Remove(Utils.CIntDef(item));
                    var gcdel = (from gp in db.NV_GIAONHANs
                                 where gp.USER_ID == Utils.CIntDef(item,0)
                                 select gp);

                    db.NV_GIAONHANs.DeleteAllOnSubmit(gcdel);
                    db.SubmitChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SaveData(int _idUser)
        {
            string strLink = "";

            try
            {
                int i = 0;
                var gcdel = (from gp in db.NV_GIAONHANs
                             where gp.USER_ID == _idUser
                             select gp);

                db.NV_GIAONHANs.DeleteAllOnSubmit(gcdel);

                foreach (TreeListNode node in ASPxTreeList_menu.GetSelectedNodes())
                {
                    int _idmenu = Utils.CIntDef(node.Key, 0);
                    if (_idmenu > 0)
                    {
                        NV_GIAONHAN grinsert = new NV_GIAONHAN();
                        grinsert.USER_ID = _idUser;
                        grinsert.USER_NAME = GetUser(_idUser);
                        grinsert.PROP_PARENT_ID = Utils.CIntDef(ddlThanhPho.SelectedValue);
                        grinsert.PROP_ID = _idmenu;
                        db.NV_GIAONHANs.InsertOnSubmit(grinsert);
                    }
                    i++;
                }
                for (int k = 0; k < chkOtherPos.Items.Count; k++)
                {
                    if (chkOtherPos.Items[k].Selected)
                    {
                        NV_GIAONHAN grinsert = new NV_GIAONHAN();
                        grinsert.USER_ID = _idUser;
                        grinsert.USER_NAME = GetUser(_idUser);
                        grinsert.PROP_PARENT_ID = Utils.CIntDef(ddlThanhPho.SelectedValue);
                        grinsert.PROP_ID_OTHER = Utils.CIntDef(chkOtherPos.Items[k].Value);
                        db.NV_GIAONHANs.InsertOnSubmit(grinsert);
                    }
                }

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }
        }
        public void Load_Grid()
        {
            try
            {
                var list = _NhanVienGiaoNhanData.GetList1();
                ASPxGridView1_request.DataSource = list;
                ASPxGridView1_request.DataBind();
            }
            catch //(Exception)
            {

                //throw;
            }
        }
        #endregion

        #region Event
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SaveData(Utils.CIntDef(ddlNhanVienGN.SelectedValue));
            Load_Grid();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("noi-hoat-dong-nhan-vien-giao-nhan.aspx");
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("trang-chu.aspx");
        }
        protected void ASPxGridView1_request_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Load_Grid();
        }
        protected void ASPxGridView1_request_PageIndexChanged(object sender, EventArgs e)
        {
            Load_Grid();
        }
        protected void ddlThanhPho_SelectedIndexChanged(object sender, EventArgs e)
        {
            Remove_Check();//Bỏ note cũ

            int _idPr = Utils.CIntDef(ddlThanhPho.SelectedValue);
            var obj = _UnitData.Loaddistric(_idPr);
            if (obj.Count > 0)
            {
                HttpContext.Current.Session["listmenuQuanHuyen"] = obj;
                ASPxTreeList_menu.DataSource = obj;
                ASPxTreeList_menu.DataBind();

                int iUser = Utils.CIntDef(ddlNhanVienGN.SelectedValue);
                if(iUser > 0)
                    CheckArea(iUser);
            }
        }
        protected void ddlNhanVienGN_SelectedIndexChanged(object sender, EventArgs e)
        {
            Remove_Check();//Bỏ note cũ
            int iUser = Utils.CIntDef(ddlNhanVienGN.SelectedValue);
            if (Utils.CIntDef(ddlThanhPho.SelectedValue) > 0)
                CheckArea(iUser);
        }
        #endregion

        #region Funtion
        public void TestPermission()
        {
            try
            {
                getCookies _getCookies = new getCookies();
                _getCookies.getCookiesNew();

                int _groupType = Utils.CIntDef(Session["Grouptype"]);
                if (_groupType != 1 && _groupType != 2 && _groupType != 3 && _groupType != 14)
                {
                    string strScript = "<script>";
                    strScript += "alert('Bạn không được cấp quyền truy cập trang này!');";
                    strScript += "window.location='trang-chu.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
                else {
                    if (_groupType == 3)
                    {
                        tEditText.Visible = false;
                        lbtnSave.Visible = false;
                        lbtnDelete.Visible = false;
                    }
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
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
        private void CheckArea(int _idUser)
        {
            if (_idUser > 0)
            {
                var list = db.NV_GIAONHANs.Where(n => n.USER_ID == _idUser).ToList();
                foreach (TreeListNode node in ASPxTreeList_menu.GetAllNodes())
                {
                    int _idmenu = Utils.CIntDef(node.Key, 0);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (Utils.CIntDef(list[i].PROP_ID) == _idmenu)
                        {
                            node.Selected = true;
                        }
                    }
                }
                //Lấy vị trí khác
                for (int i = 0; i < list.Count; i++)
                {
                    int _prop_id = Utils.CIntDef(list[i].PROP_ID_OTHER);
                    int _count_chk = chkOtherPos.Items.Count;
                    for (int j = 0; j < _count_chk; j++)
                    {
                        if (Utils.CIntDef(chkOtherPos.Items[j].Value) == _prop_id)
                            chkOtherPos.Items[j].Selected = true;
                    }
                }
            }
        }
        private void Remove_Check()
        {
            int _count_chk = chkOtherPos.Items.Count;
            for (int j = 0; j < _count_chk; j++)
                chkOtherPos.Items[j].Selected = false;

            if (ddlNhanVienGN.SelectedValue == "0" || ddlThanhPho.SelectedValue == "0")
                ASPxTreeList_menu.ClearNodes();

            foreach (TreeListNode node in ASPxTreeList_menu.GetAllNodes())
            {
                node.Selected = false;
            }
        }
        public string GetTinhTP(object pro)
        {
            int _pro = Utils.CIntDef(pro);
            return _UnitData.Get_PropertyName(_pro);
        }
        public string GetQuanHuyen(object idUser)
        {
            int _idUser = Utils.CIntDef(idUser);
            string str = "";
            if (_idUser > 0)
            {
                var list = db.NV_GIAONHANs.Where(n => n.USER_ID == _idUser).ToList();
                int _count = list.Count;
                if (_count > 0)
                {
                    int _pId = Utils.CIntDef(list[0].PROP_ID, 0);
                    int _pIdOther = Utils.CIntDef(list[0].PROP_ID_OTHER, 0);

                    if(_pId != 0)
                        str = _UnitData.Get_PropertyName(_pId);
                    if (_pIdOther != 0)
                        str = _UnitData.Get_PropertyName(_pIdOther);
                }
                for (int i = 1; i < _count; i++)
                {
                    int _pId = Utils.CIntDef(list[i].PROP_ID, 0);
                    int _pIdOther = Utils.CIntDef(list[i].PROP_ID_OTHER, 0);
                    if(_pId != 0)
                        str += ", " + _UnitData.Get_PropertyName(_pId);
                    if (_pIdOther != 0)
                        str += ", " + _UnitData.Get_PropertyName(_pIdOther); ;
                }
            }
            return str;
        }
        #endregion

    }
}