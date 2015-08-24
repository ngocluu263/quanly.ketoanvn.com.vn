using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThanhLapDN.Data;
using vpro.functions;
using System.IO;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace ThanhLapDN.Pages
{
    public partial class bang_luong : System.Web.UI.Page
    {
        #region Declare
        private BangLuongData _BangLuongData = new BangLuongData();
        UnitData unitdata = new UnitData();
        AppketoanDataContext db = new AppketoanDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPermission();
            if (!IsPostBack)
            {

                LoadProject();
            }
            else
            {
                ASPxGridView1_project.DataSource = HttpContext.Current.Session["listBangLuong"];
                ASPxGridView1_project.DataBind();
            }
        }

        #region LoadData
        private void LoadProject()
        {
            try
            {
                if (Utils.CStrDef(Session["Grouptype"]) != "1" && Utils.CStrDef(Session["Grouptype"]) != "9" && Utils.CStrDef(Session["Grouptype"]) != "14")
                {
                    var list = _BangLuongData.GetListByNV(txtKeyword.Value, Utils.CIntDef(ddlThang.SelectedValue), Utils.CIntDef(ddlNam.SelectedValue), GetUser(Utils.CIntDef(Session["Userid"])));

                    HttpContext.Current.Session["listBangLuong"] = list;
                    ASPxGridView1_project.DataSource = list;
                    ASPxGridView1_project.DataBind();
                }
                else
                {
                    var list = _BangLuongData.GetListByYear(txtKeyword.Value, Utils.CIntDef(ddlThang.SelectedValue), Utils.CIntDef(ddlNam.SelectedValue));

                    HttpContext.Current.Session["listBangLuong"] = list;
                    ASPxGridView1_project.DataSource = list;
                    ASPxGridView1_project.DataBind();
                }
            }
            catch //(Exception)
            {
                //throw;
            }
        }
        #endregion

        #region Function
        public void TestPermission()
        {
            try
            {
                getCookies _getCookies = new getCookies();
                _getCookies.getCookiesNew();

                if (Session["Grouptype"].ToString() != "1" && Session["Grouptype"].ToString() != "2")
                {
                    lbtnDelete.Visible = false;
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
        public string getGroupName(object groupId)
        {
            string str = "";
            int _groupId = Utils.CIntDef(groupId);
            var obj = db.GROUPs.Where(n => n.GROUP_ID == _groupId).Single();
            if (obj != null)
                str = obj.GROUP_NAME;
            return str;
        }
        public string getlink(object id)
        {
            if (Session["Grouptype"].ToString() == "1" || Session["Grouptype"].ToString() == "2" || Session["Grouptype"].ToString() == "4" || Session["Grouptype"].ToString() == "10")
            {
                return Utils.CIntDef(id) > 0 ? "chi-tiet-ho-so.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-ho-so.aspx";
            }
            else { return "bang-luong.aspx"; }
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string getPrice(object price)
        {
            return string.Format("{0:###,##0}", price);
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
        private void DeleteAllFilesInFolder(string folderpath)
        {
            try
            {
                foreach (var f in System.IO.Directory.GetFiles(folderpath))
                    System.IO.File.Delete(f);
                foreach (var f in System.IO.Directory.GetDirectories(folderpath))
                {
                    foreach (var i in System.IO.Directory.GetFiles(f))
                        System.IO.File.Delete(i);
                    System.IO.Directory.Delete(f);
                }
                if (Directory.Exists(folderpath))
                {
                    Directory.Delete(folderpath);
                }
            }
            catch (Exception ex) { }
        }
        public string Getstatus(object status, object level)
        {
            return unitdata.Getstatus(status, level);
        }
        protected virtual void PrepareTotalFilterItemsNV(ASPxGridViewHeaderFilterEventArgs e, string _field)
        {
            int count = e.Values.Count;
            ArrayList list = new ArrayList();
            if (count >= 3)
            {
                for (int i = 3; i < count; i++)
                {
                    list.Add(e.Values[i].Value);
                }
            }
            e.Values.Clear();
            if (e.Column.Settings.HeaderFilterMode == HeaderFilterMode.List)
                e.AddShowAll();
            for (int i = 0; i < list.Count; i++)
            {
                if (Utils.CStrDef(list[i]) == "0")
                    e.AddValue("Chưa nhập tên", Utils.CStrDef(list[i]), string.Format("[{0}] == {1}", _field, list[i]));
                else
                    e.AddValue(GetUser(list[i]), Utils.CStrDef(list[i]), string.Format("[{0}] == {1}", _field, list[i]));
            }
            list.Clear();
        }
        #endregion

        #region Event
        protected void ASPxGridView1_project_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadProject();
        }
        protected void ASPxGridView1_project_PageIndexChanged(object sender, EventArgs e)
        {
            LoadProject();
        }
        protected void ASPxGridView1_project_DataBound(object sender, EventArgs e)
        {
            
        }
        protected void ASPxGridView1_project_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "USER_ID")
                PrepareTotalFilterItemsNV(e, "USER_ID");
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            if (Utils.CStrDef(Session["Grouptype"]) == "1" || Utils.CStrDef(Session["Grouptype"]) == "2")
            {
                List<object> fieldValues = ASPxGridView1_project.GetSelectedFieldValues(new string[] { "ID" });
                foreach (var item in fieldValues)
                {
                    _BangLuongData.Remove(Utils.CIntDef(item));
                }

                Response.Redirect("bang-luong.aspx");
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert(' Bạn không có quyền này!');";
                strScript += "window.location='bang-luong.aspx';";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
        }
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }
        #endregion

    }
}