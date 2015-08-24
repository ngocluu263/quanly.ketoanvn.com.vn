using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThanhLapDN.Data;
using vpro.functions;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxTreeList;

namespace ThanhLapDN.Pages
{
    public partial class chi_tiet_ho_so : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        ProfileData _ProjectData = new ProfileData();
        CongNoData _CongNoData = new CongNoData();
        DeleteData _delData = new DeleteData();
        SendMail sm = new SendMail();
        UnitData unit_data = new UnitData();
        getCookies _getCookies = new getCookies();
        int _id = 0;
        string _eMailRecevice = System.Configuration.ConfigurationManager.AppSettings["EmailRecevice"];
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            getCookies _getCookies = new getCookies();
            _getCookies.getCookiesNew();
            int _grouptype = Utils.CIntDef(Session["Grouptype"], 0);
            if (_grouptype != 1 && _grouptype != 2)
            {
                txtDaThu.Enabled = txtFromDate.Visible = false;
            }


            _id = Utils.CIntDef(Request.QueryString["id"]);
            idNameUser.Visible = false;
            ddlType.Enabled = _id == 0 ? true : false;
            if (!IsPostBack)
            {
                Getinfo();
                //TestPermission();
                ddlType_SelectedIndexChanged(sender, e);
                LoadTypeReg(_id);
                CheckTypeReg(_id);
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

        #region Email
        private void SendEmail(int type)
        {
            try
            {
                string str = "";
                switch (type)
                {
                    case 1: str = "Hồ sơ thành lập mới"; break;
                    case 2: str = "Hồ sơ thay đổi"; break;
                    case 3: str = "Hồ sơ hành chánh"; break;
                }
                var listUser = db.USERs.Where(n => n.GROUP_ID == 4).ToList();
                if (listUser.Count > 0)
                {
                    int idUser = Utils.CIntDef(Session["Userid"]);
                    var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                    string _nameSend = _getUserSend[0].USER_NAME;
                    string _mailBody = string.Empty;
                    _mailBody += "<br/>Xin chào!";
                    _mailBody += "<br/>Bạn vừa nhận được hồ sơ mới từ mục <strong>" + str + "</strong> được gửi đến từ <strong>" + _nameSend + "</strong>";
                    _mailBody += "<br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để biết thêm thông tin.<br/><br/>";
                    _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo từ Kế Toán VN: Tiếp nhận hồ sơ mới", listUser[0].USER_EMAIL, "", "", _sMailBody, true, false);
                }
            }
            catch (Exception) { throw; }
        }
        private void SendEmailKeToan(int idUserRecive, int type, string str)
        {
            try
            {
                int idUser = Utils.CIntDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
                sm.SendEmail(idUser, idUserRecive, type, str, _ProjectData.Get_NameCompany(_id));
            }
            catch (Exception) { throw; }
        }
        private void SendEmailChangeCongNo(string _mst)
        {
            try
            {
                var listUser = db.USERs.Where(n => n.GROUP_ID == 10).ToList();
                if (listUser.Count > 0)
                {
                    int idUser = Utils.CIntDef(Session["Userid"]);
                    var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                    string _nameSend = _getUserSend[0].USER_NAME;
                    string _mailBody = string.Empty;
                    _mailBody += "<br/>Xin thông báo!";
                    _mailBody += "<br/>Hồ sơ công ty có mã số thuế <strong>"+_mst+"</strong> sẽ được thay đổi.";
                    _mailBody += "<br/>Tên nhân viên thay đổi hồ sơ: <strong>" + _nameSend + "</strong>";
                    _mailBody += "<br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để biết thêm thông tin.<br/><br/>";
                    _mailBody += "Ngày cập nhật <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo từ Kế Toán VN: Thông báo về chỉnh sửa hồ sơ", listUser[0].USER_EMAIL, "", "", _sMailBody, true, false);
                }
            }
            catch (Exception) { throw; }
        }
        private void SendEmailNVKeToanCongNo(string _mst, int _idKeToan)
        {
            try
            {
                var listUser = db.USERs.Where(n => n.USER_ID == _idKeToan).ToList();
                if (listUser.Count > 0)
                {
                    int idUser = Utils.CIntDef(Session["Userid"]);
                    var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                    string _nameSend = _getUserSend[0].USER_NAME;
                    string _mailBody = string.Empty;
                    _mailBody += "<br/>Xin thông báo!";
                    _mailBody += "<br/>Hồ sơ công ty có mã số thuế <strong>" + _mst + "</strong> sẽ được thay đổi.";
                    _mailBody += "<br/>Tên nhân viên thay đổi hồ sơ: <strong>" + _nameSend + "</strong>";
                    _mailBody += "<br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để biết thêm thông tin.<br/><br/>";
                    _mailBody += "Ngày cập nhật <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    sm.SendEmailSMTP("Thông báo từ Kế Toán VN: Thông báo về chỉnh sửa hồ sơ", listUser[0].USER_EMAIL, "", "", _sMailBody, true, false);
                }
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Getinfo
        public void Getinfo()
        {
            try
            {
                var list = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
                if (list.Count > 0)
                {
                    int type = Utils.CIntDef(list[0].PROF_TYPE);
                    ddlType.SelectedValue = Utils.CStrDef(list[0].PROF_TYPE);

                    TestPermission(Utils.CStrDef(type), Utils.CIntDef(list[0].USER_ID));

                    lblLoadNameFile.Text = list[0].PROF_FILE;
                    txtTongPhi.Text = list[0].PROF_COST1.Value.ToString("###,##0").Replace(".",",");
                    txtDaThu.Text = list[0].PROF_COST2.Value.ToString("###,##0").Replace(".", ",");
                    lblConLai.Text = (Utils.CDecDef(list[0].PROF_COST1) - Utils.CDecDef(list[0].PROF_COST2)).ToString("###,##0 vnđ");
                    txtFromDate.Value = list[0].PROF_DATE_COST != null ? list[0].PROF_DATE_COST.Value.ToString("dd/MM/yyyy") : "";

                    Load_Employ(Utils.CStrDef(list[0].USER_ID));

                    if (type == 2)
                    {
                        txtChTenCty.Text = list[0].PROF_NAME;
                        txtChDiaChi.Text = list[0].PROF_ADDRESS;
                        txtChMST.Text = list[0].PROF_TAXCODE;
                        txtChDienThoai.Text = list[0].PROF_PHONE;
                        txtChContent.Text = list[0].PROF_NOTE;
                        txtChEmail.Text = list[0].PROF_EMAIL;
                    }
                    else
                    {
                        txtTenCty.Text = list[0].PROF_NAME;
                        txtTenGiaoDich.Text = list[0].PROF_TRANSACTION;
                        txtTenVietTat.Text = list[0].PROF_ATC;
                        txtTruSoChinh.Text = list[0].PROF_ADDRESS;
                        //ddlNganhKD.Text = Utils.CStrDef(list[0].TRADES_ID);
                        txtTongSoVonGop.Text = list[0].PROF_TOTAL_CAPITAL.Value.ToString("###,##0").Replace(".", ",");
                        txtVonPhapDinh.Text = list[0].PROF_CAPITAL.Value.ToString("###,##0").Replace(".", ",");
                        txtDienThoai.Text = list[0].PROF_PHONE;
                        txtEmail.Text = list[0].PROF_EMAIL;
                        //rdbTypeReg.SelectedValue = Utils.CStrDef(list[0].PROF_REG1);

                        //Thông báo chưa nhập thành viên
                        int _idparent = Utils.CIntDef(list[0].PROF_PARENT, 0);
                        int _idprof = _idparent > 0 ? _idparent : _id;
                        if (CheckEmptMember(_idprof)) lblMsg.Text = "Lưu ý: Hồ sơ thành lập mới chưa có thành viên, thêm mới thành viên ở bên dưới màn hình";
                        else lblMsg.Text = "";
                        LoadLinkMem(_idprof);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void LoadLinkMem(int _idprof)
        {
            if (_idprof != 0)
                liLoadMemLink.Text = @"<a href='#' onClick='openDSTV(" + _idprof + "); return false' title='Thêm thành viên'><img src='../Images/Create_product.png' height='18' width='18' /></a>&nbsp;&nbsp;<a href='#' onClick='openDSTV1(" + _idprof + "); return false' title='Danh sách thành viên'><img src='../Images/List_Product.png' height='18' width='18' /></a>";
        }
        private void LoadTypeReg(int _id_prof)
        {
            try
            {
                //Kiểm tra type của prof
                int _type = Utils.CIntDef(ddlType.SelectedValue);
                var AllList = (from g in db.TYPE_COMPANies
                               where g.TYPE_RANK > 0 && (g.TYPE_REG == null || g.TYPE_REG == _type)
                               select new
                               {
                                   g.TYPE_ID,
                                   g.TYPE_PARENT,
                                   g.TYPE_RANK,
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
        #region Data
        private void Save(string strLink = "")
        {
            try
            {
                //gán lại session
                _getCookies.getCookiesNew();

                string img = string.Empty;
                string pathfile = string.Empty;
                string fullpathfile = string.Empty;
                string path = string.Empty;
                int type = Utils.CIntDef(ddlType.SelectedValue);
                if (_id == 0)
                {
                    /*--------------File-------------*/
                    if (fileUpload.PostedFile.FileName != null)
                    {
                        img = fileUpload.PostedFile.FileName;
                    }
                    /*------------------------------*/

                    PROFILE_NEW i = new PROFILE_NEW();
                    i.PROF_TYPE = type;
                    i.PROF_FILE = img;
                    i.PROF_COST1 = Utils.CDecDef(txtTongPhi.Text.Replace(",", ""));
                    i.PROF_COST2 = Utils.CDecDef(txtDaThu.Text.Replace(",", ""));
                    i.PROF_ACTIVE = 1;
                    i.PROF_STATUS = 1;
                    i.PROF_LEVEL = 1;
                    i.PROF_DATE = DateTime.Now;
                    i.PROF_DATE_COST = txtFromDate.Value != "" ? fmDate(txtFromDate.Value) : i.PROF_DATE_COST = null;
                    i.USER_ID = Utils.CIntDef(Session["Userid"]);

                    if (type == 2)
                    {
                        i.PROF_NAME = txtChTenCty.Text;
                        i.PROF_ADDRESS = txtChDiaChi.Text;
                        i.PROF_TAXCODE = txtChMST.Text;
                        i.PROF_PHONE = txtChDienThoai.Text;
                        i.PROF_NOTE = txtChContent.Text;
                        i.PROF_EMAIL = txtChEmail.Text;
                        if (_CongNoData.CheckByMST(txtChMST.Text))
                        {//Chỉ gửi mail cho bên kế toán là hồ sơ có thay đổi
                            if (_CongNoData.GetIdKeToan(txtChMST.Text) == 0)
                                SendEmailNVKeToanCongNo(txtChMST.Text, _CongNoData.GetIdKeToan(txtChMST.Text));
                            SendEmailChangeCongNo(txtChMST.Text);
                        }
                    }
                    else
                    {
                        i.PROF_NAME = txtTenCty.Text;
                        i.PROF_TRANSACTION = txtTenGiaoDich.Text;
                        i.PROF_ATC = txtTenVietTat.Text;
                        i.PROF_ADDRESS = txtTruSoChinh.Text;
                        i.PROF_TOTAL_CAPITAL = Utils.CDecDef(txtTongSoVonGop.Text.Replace(",", ""), 0);
                        i.PROF_CAPITAL = Utils.CDecDef(txtVonPhapDinh.Text.Replace(",", ""), 0);
                        i.PROF_PHONE = txtDienThoai.Text;
                        i.PROF_EMAIL = txtEmail.Text;
                    }
                    _ProjectData.Create(i);
                    SendEmail(type);
                    var getlink = db.PROFILE_NEWs.OrderByDescending(n => n.ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        if(type == 3) Save_Employ(getlink[0].ID);//Nếu ở giai đoạn 3 mới cho người nhận là NVXLHS
                        SaveTypeReg(getlink[0].ID);//Save TypeReg
                        if (!string.IsNullOrEmpty(fileUpload.PostedFile.FileName))
                        {
                            pathfile = Server.MapPath("/File/Profile/" + Utils.CStrDef(getlink[0].ID));
                            path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + Utils.CStrDef(getlink[0].ID + "/" + img);
                            fullpathfile = pathfile + "/" + img;

                            if (!Directory.Exists(pathfile))
                            {
                                Directory.CreateDirectory(pathfile);
                            }
                            fileUpload.PostedFile.SaveAs(fullpathfile);
                        }

                        strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-ho-so.aspx?id=" + getlink[0].ID : strLink;
                    }
                }
                else
                {
                    /*--------------File-------------*/
                    if (fileUpload.PostedFile.FileName != null && fileUpload.PostedFile.FileName != "")
                    {
                        img = fileUpload.PostedFile.FileName;
                        string pathfileOld = Server.MapPath("/File/Profile/" + Utils.CStrDef(_id));
                        string fullpathfileOld = pathfileOld + "/" + _ProjectData.GetById(_id).PROF_FILE;
                        /*Trước khi Update file mới ta phải xóa file cũ*/
                        if (File.Exists(fullpathfileOld))
                        {
                            File.Delete(fullpathfileOld);
                        }
                        /*=============================================*/
                    }/*------------------------------*/

                    var list = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
                    foreach (var i in list)
                    {
                        if (img != "")
                        {
                            i.PROF_FILE = img;
                        }
                        i.PROF_COST1 = Utils.CDecDef(txtTongPhi.Text.Replace(",", ""));
                        i.PROF_COST2 = Utils.CDecDef(txtDaThu.Text.Replace(",", ""));
                        i.PROF_DATE = DateTime.Now;
                        i.PROF_DATE_COST = txtFromDate.Value != "" ? fmDate(txtFromDate.Value) : i.PROF_DATE_COST = null;
                        i.USER_ID = Utils.CIntDef(ddlNameUser.SelectedValue);

                        if (type == 2)
                        {
                            i.PROF_NAME = txtChTenCty.Text;
                            i.PROF_ADDRESS = txtChDiaChi.Text;
                            i.PROF_TAXCODE = txtChMST.Text;
                            i.PROF_PHONE = txtChDienThoai.Text;
                            i.PROF_NOTE = txtChContent.Text;
                            i.PROF_EMAIL = txtChEmail.Text;
                        }
                        else
                        {
                            i.PROF_NAME = txtTenCty.Text;
                            i.PROF_TRANSACTION = txtTenGiaoDich.Text;
                            i.PROF_ATC = txtTenVietTat.Text;
                            i.PROF_ADDRESS = txtTruSoChinh.Text;
                            //i.TRADES_ID = Utils.CIntDef(ddlNganhKD.SelectedValue);
                            i.PROF_TOTAL_CAPITAL = Utils.CDecDef(txtTongSoVonGop.Text.Replace(",", ""));
                            i.PROF_CAPITAL = Utils.CDecDef(txtVonPhapDinh.Text.Replace(",", ""));
                            i.PROF_PHONE = txtDienThoai.Text;
                            i.PROF_EMAIL = txtEmail.Text;
                            //i.PROF_REG1 = Utils.CIntDef(rdbTypeReg.SelectedValue);
                        }

                        _ProjectData.Update(i);
                    }
                    db.SubmitChanges();
                    SaveTypeReg(Utils.CIntDef(_id));//Save TypeReg
                    if (!string.IsNullOrEmpty(fileUpload.PostedFile.FileName))
                    {

                        pathfile = Server.MapPath("/File/Profile/" + Utils.CStrDef(_id));
                        path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + Utils.CStrDef(_id + "/" + img);
                        fullpathfile = pathfile + "/" + img;
                        if (!Directory.Exists(pathfile))
                        {
                            Directory.CreateDirectory(pathfile);
                        }
                        fileUpload.PostedFile.SaveAs(fullpathfile);
                    }

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-ho-so.aspx?id=" + _id : strLink;
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
                    //Response.Redirect(strLink);
                    string strScript = "<script>";
                    strScript += "alert('Hồ sơ đã được cập nhật thành công!');";
                    strScript += "window.location='" + strLink + "';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
            }
        }
        private void SaveTypeReg(int _id_prof)
        {
            string strLink = "";

            try
            {
                int i = 0;
                var gcdel = (from gp in db.TYPE_LIST_CHOOSEs
                             where gp.PROF_ID == _id_prof
                             select gp);

                db.TYPE_LIST_CHOOSEs.DeleteAllOnSubmit(gcdel);

                foreach (TreeListNode node in ASPxTreeList_menu.GetSelectedNodes())
                {
                    int _idmenu = Utils.CIntDef(node.Key, 0);
                    if (_idmenu > 0)
                    {
                        TYPE_LIST_CHOOSE grinsert = new TYPE_LIST_CHOOSE();
                        grinsert.PROF_ID = _id_prof;
                        grinsert.TYPE_ID = _idmenu;

                        db.TYPE_LIST_CHOOSEs.InsertOnSubmit(grinsert);
                    }

                    i++;
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
        private void Delete()
        {
            try
            {
                int id = _id;
                string img = _ProjectData.GetById(_id).PROF_FILE;
                _delData.DeleteMemberByProf(_id);
                _delData.DeleteAttachByProf(_id);
                _delData.DeleteWorkByProf(_id);
                _ProjectData.Remove(_id);
                string pathfileOld = Server.MapPath("/File/Profile/" + Utils.CStrDef(id));
                DeleteAllFilesInFolder(pathfileOld);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Save_Employ(int _idprof)
        {
            //Gửi cho toàn bộ nhân viên xử lý hồ sơ
            var list = db.USERs.Where(n => n.GROUP_ID == 11).ToList();
            if (list.Count > 0)
            {
                foreach (var i in list)
                {
                    int _i = Utils.CIntDef(i.USER_ID);
                    WORKFLOW_USER b = new WORKFLOW_USER();
                    b.USER_ID = _i;
                    b.PROF_ID = _idprof;
                    b.DATE = DateTime.Now;
                    b.WORK_STATUS = 1;
                    db.WORKFLOW_USERs.InsertOnSubmit(b);
                    db.SubmitChanges();
                    SendEmailKeToan(_i, 3, "Giai đoạn 1: Tiếp nhận hồ sơ");
                }
            }
        }
        private void Load_Employ(string _idUser)
        {
            try
            {
                if (Session["Grouptype"].ToString() == "1" || Session["Grouptype"].ToString() == "2")
                {
                    idNameUser.Visible = true;
                    var list = db.USERs.ToList();
                    if (list.Count > 0)
                    {
                        ddlNameUser.DataTextField = "USER_NAME";
                        ddlNameUser.DataValueField = "USER_ID";
                        ddlNameUser.DataSource = list;
                        ddlNameUser.DataBind();

                        ddlNameUser.SelectedValue = _idUser;
                    }
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
        #endregion

        #region Button
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (CheckEmptType() && ddlType.SelectedValue != "3")
            {
                string strScript = "<script>";
                strScript += "alert('Loại hình đăng ký không được bỏ trống!');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
            else Save();
        }
        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            if (CheckEmptType() && ddlType.SelectedValue != "3")
            {
                string strScript = "<script>";
                strScript += "alert('Loại hình đăng ký không được bỏ trống!');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
            else Save("danh-sach-ho-so.aspx");
        }
        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            if (CheckEmptType() && ddlType.SelectedValue != "3")
            {
                string strScript = "<script>";
                strScript += "alert('Loại hình đăng ký không được bỏ trống!');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
            }
            else Save("chi-tiet-ho-so.aspx");
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("danh-sach-ho-so.aspx");
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-ho-so.aspx");
        }
        #endregion
        #region Load
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _type = Utils.CIntDef(ddlType.SelectedValue);
            switch (_type)
            {
                case 0: iCreate.Visible = iChange.Visible = false; break;
                case 1: iCreate.Visible = true; iChange.Visible = false; idLoaiHinh.Visible = true; break;
                case 2: iCreate.Visible = false; iChange.Visible = true; idLoaiHinh.Visible = true; break;
                case 3: iCreate.Visible = true; iChange.Visible = idLoaiHinh.Visible = false;  break;
            }
            //Getinfo();
            LoadTypeReg(_id);
        }
        protected void OnLoad(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //Kiểm tra hồ sơ đã có thành viên hay ko
                var list = db.PROFILE_NEWs.Where(n => n.ID == _id).ToList();
                if (list.Count > 0)
                {
                    int type = Utils.CIntDef(list[0].PROF_TYPE);
                    if (type == 2) { }
                    else
                    {
                        int _idparent = Utils.CIntDef(list[0].PROF_PARENT, 0);
                        int _idprof = _idparent > 0 ? _idparent : _id;
                        if (CheckEmptMember(_idprof)) lblMsg.Text = "Lưu ý: Hồ sơ thành lập mới chưa có thành viên, thêm mới thành viên ở bên dưới màn hình";
                        else lblMsg.Text = "";
                    }
                }
            }
        }
        #endregion

        #region Funtion
        public bool CompPrice(decimal price1, decimal price2)
        {
            if (price2 > price1)
                return false;
            else return true;
        }
        private bool CheckEmptType()
        {
            if (ASPxTreeList_menu.GetSelectedNodes().Count > 0)
                return false;
            else
                return true;
        }
        private bool CheckEmptMember(int idProf)
        {
            var list = db.PROFILE_MEMBERs.Where(n => n.PROF_ID == idProf).ToList();
            if (list.Count > 0)
                return false;
            else return true;
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
        private DateTime fmDate(string txt)
        {
            DateTime _date = DateTime.ParseExact(txt, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return _date;
        }
        private void CheckTypeReg(int _id_prof)
        {
            if (_id_prof > 0)
            {
                var list = db.TYPE_LIST_CHOOSEs.Where(n => n.PROF_ID == _id_prof).ToList();
                foreach (TreeListNode node in ASPxTreeList_menu.GetAllNodes())
                {
                    int _idmenu = Utils.CIntDef(node.Key, 0);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (Utils.CIntDef(list[i].TYPE_ID) == _idmenu)
                        {
                            node.Selected = true;
                        }
                    }
                }
            }
        }
        private void TestPermission(string _type, int _idUser)
        {
            try
            {
                if (Session["Grouptype"].ToString() == "1" || Session["Grouptype"].ToString() == "2") {/*Toàn quyền*/ }
                else if (Session["Grouptype"].ToString() == "4" && _type != "3")
                {
                    //Được cập nhật, ko xóa
                    lbtnDelete.Visible = false;
                }
                else if (Session["Grouptype"].ToString() == "10" && _type == "3")
                {
                    //Được cập nhật, ko xóa
                    lbtnDelete.Visible = false;
                }
                else
                {
                    if (_id != 0)
                    {
                        lbtnSave.Visible = lbtnSaveClose.Visible = lbtnSaveNew.Visible = lbtnDelete.Visible = false;
                        if (Utils.CIntDef(Session["Userid"]) != _idUser)
                            liLoadMemLink.Visible = false;
                    }
                }
            }
            catch (Exception) { Response.Redirect("trang-chu.aspx"); }
        }
        #endregion

    }
}