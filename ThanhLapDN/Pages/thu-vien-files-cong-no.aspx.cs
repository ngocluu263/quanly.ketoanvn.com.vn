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
    public partial class thu_vien_files_cong_no : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        CongNoData _CongNoData = new CongNoData();
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
                ASPxGridView1_project.DataSource = HttpContext.Current.Session["listListCty"];
                ASPxGridView1_project.DataBind();
            }
        }

        #region LoadData
        private void LoadProject()
        {
            try
            {
                int _idUser = Utils.CIntDef(Session["Userid"]);
                int _idGroup = Utils.CIntDef(Session["Grouptype"], 0);
                string _keyword = txtKeyword.Value;
                if (_idGroup != 1 && _idGroup != 2 && _idGroup != 10 && _idGroup != 14 && _idGroup != 7)
                {
                    var list = (from n in db.CONG_NOs
                                where ((n.NV_KD == _idUser || n.NV_GN == _idUser || n.NV_KT == _idUser)
                                && (n.TEN_KH.Contains(_keyword) || n.MST.Contains(_keyword) || "" == _keyword)
                                && (n.MST != "" && n.MST != null))
                                select new
                                {
                                    n.MST
                                }).OrderByDescending(n=>n.MST).Distinct();

                    HttpContext.Current.Session["listListCty"] = list;
                    ASPxGridView1_project.DataSource = list;
                    ASPxGridView1_project.DataBind();
                }
                else
                {
                    var list = (from n in db.CONG_NOs
                                where ((n.TEN_KH.Contains(_keyword) || n.MST.Contains(_keyword) || "" == _keyword) 
                                && (n.MST != "" && n.MST != null))
                                select new
                                {
                                    n.MST
                                }).OrderByDescending(n => n.MST).Distinct();

                    HttpContext.Current.Session["listListCty"] = list;
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
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
        public string getlink(object mst)
        {
            string _mst = Utils.CStrDef(mst);
            return "/FileBrowser/thu-vien.aspx?mst=" + _mst;
        }
        public string GetName(object mst)
        {
            string _mst = Utils.CStrDef(mst);
            string str = "";
            var obj = db.CONG_NOs.Where(n => n.MST.Contains(_mst)).OrderByDescending(n => n.NAM).Take(1).ToList();
            if (obj.Count > 0)
            {
                str = obj[0].TEN_KH;
            }
            return str;
        }
        public string GetFileSize(object mst)
        {
            string s = "";
            string _mst = Utils.CStrDef(mst);
            string _path = Server.MapPath("/File/ThuVien/" + _mst);
            if (!Directory.Exists(_path))
            {
                s = "0 MB";
            }
            else
            {
                long _size = GetFileSizeSumFromDirectory(_path);
                double _sizeSum = Math.Round(Utils.CDblDef(_size) / 1048576, 2);
                s = _sizeSum + " MB";
            }
            return s;
        }
        public static long GetFileSizeSumFromDirectory(string searchDirectory)
        {
            var files = Directory.EnumerateFiles(searchDirectory);

            // get the sizeof all files in the current directory
            var currentSize = (from file in files let fileInfo = new FileInfo(file) select fileInfo.Length).Sum();

            var directories = Directory.EnumerateDirectories(searchDirectory);

            // get the size of all files in all subdirectories
            var subDirSize = (from directory in directories select GetFileSizeSumFromDirectory(directory)).Sum();

            return currentSize + subDirSize;
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
            
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
        #endregion

    }
}