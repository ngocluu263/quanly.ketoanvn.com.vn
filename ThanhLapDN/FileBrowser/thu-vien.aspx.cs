using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThanhLapDN.FileBrowser.Code;
using System.Globalization;
using IZ.WebFileManager;
using System.IO;
using vpro.functions;
using ThanhLapDN.Data;

namespace ThanhLapDN.FileBrowser
{
    public partial class thu_vien : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
    {
        protected AjaxJsonResponse ajaxResponse = new AjaxJsonResponse();
        AppketoanDataContext db = new AppketoanDataContext();
        public string Opener;
        public string FilesFolder { get; set; }
        string _mst = "";
        string _name = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _mst = Utils.CStrDef(Request.QueryString["mst"]);
            _name = GetName(_mst);

            HF_FileBrowserConfig.Attributes["data-filesfolder"] = _mst;
            FilesFolder = (!String.IsNullOrEmpty(HF_FileBrowserConfig.Attributes["data-filesfolder"]) ?
                        HF_FileBrowserConfig.Attributes["data-filesfolder"] : _mst);

            CultureInfo culture;
            try
            {
                culture = new CultureInfo(Request["langCode"]);
            }
            catch (Exception)
            {
                culture = CultureInfo.CurrentCulture;
            }
            FileManager1.ShowAddressBar = false;
            FileManager1.AllowUpload = true;

            String cbReference =
                Page.ClientScript.GetCallbackEventReference(this,
                "arg", "ReceiveServerData", "context");
            String callbackScript;
            callbackScript = "function CallServer(arg, context)" +
                "{ " + cbReference + ";}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                "CallServer", callbackScript, true);

            if (!IsPostBack)
            {
                int fnumber = 0;
                string caller, fn;

                // the caller is CKEditor
                if (!string.IsNullOrEmpty(Request["CKEditor"]))
                {
                    caller = "ckeditor";
                }
                else
                    caller = (String.IsNullOrEmpty(Request["caller"]) ? "parent" : Request["caller"]);

                HF_Opener.Value = caller;

                fn = Request["fn"];

                if (!String.IsNullOrEmpty(fn))
                    HF_CallBack.Value = fn;

                if (int.TryParse(Request["CKEditorFuncNum"], out fnumber))
                    HF_CKEditorFunctionNumber.Value = fnumber.ToString();

                if (!String.IsNullOrEmpty(Request["field"]))
                    HF_Field.Value = Request["field"];

                string mainRoot = "~/File/ThuVien";

                if (FileManager1.Culture == null)
                    FileManager1.Culture = culture;

                HF_CurrentCulture.Value = FileManager1.Culture.Name;

                FileManager1.CustomToolbarButtons[0].Text = FileManager1.Controller.GetResourceString("View_file", "View File");
                Upload_button.InnerText = FileManager1.Controller.GetResourceString("Upload_file_click", "Click here to download a file");
                DND_message.InnerText = FileManager1.Controller.GetResourceString("Upload_dnd", "Or drag 'nd drop one or more files on the above area");

                if (!String.IsNullOrEmpty(FileManager1.MainDirectory))
                    mainRoot = FileManager1.MainDirectory;
                //mainRoot = ResolveClientUrl(mainRoot);
                if (!Directory.Exists(Server.MapPath(ResolveClientUrl(mainRoot))))
                    throw new Exception("User directory with write privileges is needed.");

                DirectoryInfo mainRootInfo = new DirectoryInfo(Server.MapPath(ResolveClientUrl(mainRoot)));

                mainRootInfo.CreateSubdirectory(FilesFolder);

                CreateFolder(_mst);

                RootDirectory files;
                FileManager1.RootDirectories.Clear();
                files = new RootDirectory();
                files.ShowRootIndex = false;
                files.DirectoryPath = VirtualPathUtility.AppendTrailingSlash(mainRoot) + FilesFolder;
                files.LargeImageUrl = "~/FileBrowser/img/32/folder-document-alt.png";
                files.SmallImageUrl = "~/FileBrowser/img/16/folder-document-alt.png";
                files.Text = FileManager1.Controller.GetResourceString("Root_File", _name + "-" + _mst);
                FileManager1.RootDirectories.Add(files);
            }
            TestPermission();
        }
        public void RaiseCallbackEvent(String eventArgument)
        {
            string[] cmds = eventArgument.Split(new char[] { ',' });
            ajaxResponse.command = cmds[0].ToLower();
            switch (cmds[0].ToLower())
            {
                case "showfile":
                    if (FileManager1.CurrentDirectory != null)
                        ajaxResponse.data = VirtualPathUtility.AppendTrailingSlash(FileManager1.CurrentDirectory.VirtualPath) + cmds[1];
                    break;
                case "upload":
                    if (FileManager1.CurrentDirectory != null)
                        ajaxResponse.data = VirtualPathUtility.AppendTrailingSlash(FileManager1.CurrentDirectory.VirtualPath);
                    break;
                default:
                    break;
            }

        }
        public String GetCallbackResult()
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(ajaxResponse);

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
        private void CreateFolder(string mst)
        {
            if (GetName(mst) != "" && mst != "")
            {
                string pathFolder = Server.MapPath("/File/ThuVien/" + mst);
                if (Directory.Exists(pathFolder))
                {
                    string pathFolderSub = Server.MapPath("/File/ThuVien/" + mst + "/" + DateTime.Now.Year);
                    if (!Directory.Exists(pathFolderSub))
                    {
                        Directory.CreateDirectory(pathFolderSub);
                        if (Directory.Exists(pathFolderSub))
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                string _i = Utils.CStrDef((i + 1)).Length == 1 ? Utils.CStrDef("0" + (i + 1)) : Utils.CStrDef((i + 1));
                                string pathFolderItems = Server.MapPath("/File/ThuVien/" + mst + "/" + DateTime.Now.Year + "/Tháng_" + _i);
                                Directory.CreateDirectory(pathFolderItems);
                            }
                        }
                    }
                }
            }
        }

        public void TestPermission()
        {
            try
            {
                getCookies _getCookies = new getCookies();
                _getCookies.getCookiesNew();

                int _idUser = Utils.CIntDef(Session["Userid"], 0);
                int _idGroup = Utils.CIntDef(Session["Grouptype"], 0);
                if (_idGroup != 1 && _idGroup != 7)
                {
                    FileManager1.Visible = true;
                    Panel_upload.Visible = false;
                    Panel_deny.Visible = false;
                    FileManager1.ReadOnly = false;
                    FileManager1.AllowDelete = false;//Cho phép Xóa
                    FileManager1.AllowOverwrite = true;
                }
                else
                {
                    FileManager1.Visible = true;
                    Panel_upload.Visible = false;
                    Panel_deny.Visible = false;
                    FileManager1.ReadOnly = false;
                    FileManager1.AllowDelete = true;//Cho phép Xóa
                    FileManager1.AllowOverwrite = true;
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
    }
}