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
    public partial class chi_tiet_nhan_vien : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        SendMail sm = new SendMail();
        Function _Function = new Function();
        int _userid = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _userid = Utils.CIntDef(Request.QueryString["userid"]);
            if (!IsPostBack)
            {
                if (_userid == 0) { ASPxPageControl2.TabPages[1].ClientEnabled = false; }
                Loadgroup();
                Getinfo();
                rdoCoBH_SelectedIndexChanged(sender, e);
            }
            Page.MaintainScrollPositionOnPostBack = true;
        }
        #region Getinfo
        public void Loadgroup()
        {
            try
            {
                var list = db.GROUPs.ToList();
                Drgroup.DataValueField = "GROUP_ID";
                Drgroup.DataTextField = "GROUP_NAME";
                Drgroup.DataSource = list;
                Drgroup.DataBind();

                ListItem l = new ListItem("---Chọn chức vụ---", "0", true);
                Drgroup.Items.Insert(0, l);
                Drgroup.SelectedIndex = 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Getinfo()
        {
            try
            {
                var list = db.USERs.Where(n => n.USER_ID == _userid).ToList();
                if (list.Count > 0)
                {
                    Txtname.Text = list[0].USER_NAME;
                    Txtusername.Text = list[0].USER_UN;
                    txtTenDangNhap.Text = list[0].USER_UN;
                    txtMaCC.Text = list[0].USER_MACC;
                    rdoGioiTinh.SelectedValue = Utils.CStrDef(list[0].USER_GIOITINH);
                    txtNgaySinh.Text = list[0].USER_NGAYSINH != null ? list[0].USER_NGAYSINH.Value.ToString("dd/MM/yyyy") : "";
                    txtCMND.Text = list[0].USER_CMND;
                    txtNgayCapCMND.Text = list[0].USER_CMND_NGAYCAP != null ? list[0].USER_CMND_NGAYCAP.Value.ToString("dd/MM/yyyy") : "";
                    txtNoiCapCMND.Text = list[0].USER_CMND_NOICAP;
                    txtDanToc.Text = list[0].USER_DANTOC;
                    txtNguyenQuan.Text = list[0].USER_NGUYENQUAN;
                    txtNoiDK_HK.Text = list[0].USER_NOIDK_HK;
                    Txtaddress.Text = list[0].USER_ADDRESS;
                    Txtemail.Text = list[0].USER_EMAIL;
                    txtEmail_CaNhan.Text = list[0].USER_EMAIL_CANHAN;
                    Txtphone.Text = list[0].USER_PHONE;
                    txtPhone_CaNhan.Text = list[0].USER_PHONE_CANHAN;
                    txtTrinhDo.Text = list[0].USER_TRINHDO;
                    txtNT_HoTen.Text = list[0].NT_HOTEN;
                    txtNT_SDT.Text = list[0].NT_SDT;
                    txtNT_MoiQuanHe.Text = list[0].NT_MOIQUANHE;
                    Drgroup.SelectedValue = Utils.CStrDef(list[0].GROUP_ID);
                    ddlChiNhanh.SelectedValue = Utils.CStrDef(list[0].USER_CHINHANH);
                    rblActive.SelectedValue = Utils.CStrDef(list[0].USER_ACTIVE);

                    txtLuongCanBan.Text = String.Format("{0:###,##0}", list[0].USER_LUONG_CB);
                    rdoCoBH.SelectedValue = Utils.CStrDef(list[0].USER_COBH);
                    if (list[0].USER_COBH == 1)
                    {
                        txtLuongBHCD.Text = String.Format("{0:###,##0}", list[0].USER_LUONG_BH);
                        txtPT_BHXH.Text = String.Format("{0:#.#}", list[0].USER_BHXH_PT);
                        txtPT_BHYT.Text = String.Format("{0:#.#}", list[0].USER_BHYT_PT);
                        txtPT_BHTN.Text = String.Format("{0:#.#}", list[0].USER_BHTN_PT);
                        txtCTBHXH.Text = String.Format("{0:###,##0}", list[0].USER_BHXH);
                        txtCTBHYT.Text = String.Format("{0:###,##0}", list[0].USER_BHYT);
                        txtCTBHTN.Text = String.Format("{0:###,##0}", list[0].USER_BHTN);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Savedata
        private bool checkexist_user(string user)
        {
            try
            {
                var list = db.USERs.Where(n => n.USER_UN == user && n.USER_ID != _userid).ToList();
                if (list.Count > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void Save(string strLink = "")
        {
            try
            {
                string SALT = "";
                string USER_PW = "";
                if (!string.IsNullOrEmpty(Txtpass.Text))
                {
                    if (Txtpass.Text != Txtrepass.Text)
                    {
                        Lberrors.Text = "2 mật khẩu không giống nhau";
                    }
                    else
                    {
                        SALT = Common.CreateSalt();
                        USER_PW = Common.Encrypt(Txtpass.Text, SALT);
                    }
                }
                if (_userid == 0)
                {
                    USER user = new USER();
                    user.USER_NAME = Txtname.Text;
                    user.USER_UN = Txtusername.Text;
                    user.USER_MACC = txtMaCC.Text;
                    user.USER_GIOITINH = Utils.CIntDef(rdoGioiTinh.SelectedValue);
                    user.USER_NGAYSINH = txtNgaySinh.Text == "" ? user.USER_NGAYSINH = null :
                        DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    user.USER_CMND = txtCMND.Text;
                    user.USER_CMND_NGAYCAP = txtNgayCapCMND.Text == "" ? user.USER_CMND_NGAYCAP = null :
                        DateTime.ParseExact(txtNgayCapCMND.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    user.USER_CMND_NOICAP = txtNoiCapCMND.Text;
                    user.USER_DANTOC = txtDanToc.Text;
                    user.USER_NGUYENQUAN = txtNguyenQuan.Text;
                    user.USER_NOIDK_HK = txtNoiDK_HK.Text;
                    user.USER_ADDRESS = Txtaddress.Text;
                    user.USER_EMAIL = Txtemail.Text;
                    user.USER_EMAIL_CANHAN = txtEmail_CaNhan.Text;
                    user.USER_PHONE = Txtphone.Text;
                    user.USER_PHONE_CANHAN = txtPhone_CaNhan.Text;
                    user.USER_TRINHDO = txtTrinhDo.Text;
                    user.NT_HOTEN = txtNT_HoTen.Text;
                    user.NT_SDT = txtNT_SDT.Text;
                    user.NT_MOIQUANHE = txtNT_MoiQuanHe.Text;
                    user.GROUP_ID = Utils.CIntDef(Drgroup.SelectedValue);
                    user.USER_CHINHANH = Utils.CIntDef(ddlChiNhanh.SelectedValue);
                    user.USER_DATE = DateTime.Now;
                    db.USERs.InsertOnSubmit(user);
                    db.SubmitChanges();

                    SendEmailNew(Txtname.Text, Txtusername.Text, Txtemail.Text, Utils.CStrDef(Drgroup.SelectedItem), rdoGioiTinh.SelectedValue == "1" ? "Nam" : "Nữ"
                        , txtNgaySinh.Text, txtCMND.Text, txtNgayCapCMND.Text, txtNoiCapCMND.Text, txtDanToc.Text, txtNguyenQuan.Text, txtNoiDK_HK.Text
                        , Txtaddress.Text, txtEmail_CaNhan.Text, Txtphone.Text, txtPhone_CaNhan.Text, txtTrinhDo.Text, txtNT_HoTen.Text, txtNT_SDT.Text, txtNT_MoiQuanHe.Text, getDiaDiem(ddlChiNhanh.SelectedValue));

                    var getlink = db.USERs.OrderByDescending(n => n.USER_ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhan-vien.aspx?userid=" + getlink[0].USER_ID : strLink;
                    }
                }
                else
                {
                    var list = db.USERs.Where(n => n.USER_ID == _userid).ToList();
                    if (ASPxPageControl2.ActiveTabIndex == 0)
                    {
                        foreach (var i in list)
                        {
                            i.USER_NAME = Txtname.Text;
                            i.USER_UN = Txtusername.Text;
                            i.USER_MACC = txtMaCC.Text;
                            i.USER_GIOITINH = Utils.CIntDef(rdoGioiTinh.SelectedValue);
                            i.USER_NGAYSINH = txtNgaySinh.Text == "" ? i.USER_NGAYSINH = null :
                                DateTime.ParseExact(txtNgaySinh.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            i.USER_CMND = txtCMND.Text;
                            i.USER_CMND_NGAYCAP = txtNgayCapCMND.Text == "" ? i.USER_CMND_NGAYCAP = null :
                                DateTime.ParseExact(txtNgayCapCMND.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            i.USER_CMND_NOICAP = txtNoiCapCMND.Text;
                            i.USER_DANTOC = txtDanToc.Text;
                            i.USER_NGUYENQUAN = txtNguyenQuan.Text;
                            i.USER_NOIDK_HK = txtNoiDK_HK.Text;
                            i.USER_ADDRESS = Txtaddress.Text;
                            i.USER_EMAIL = Txtemail.Text;
                            i.USER_EMAIL_CANHAN = txtEmail_CaNhan.Text;
                            i.USER_PHONE = Txtphone.Text;
                            i.USER_PHONE_CANHAN = txtPhone_CaNhan.Text;
                            i.USER_TRINHDO = txtTrinhDo.Text;
                            i.NT_HOTEN = txtNT_HoTen.Text;
                            i.NT_SDT = txtNT_SDT.Text;
                            i.NT_MOIQUANHE = txtNT_MoiQuanHe.Text;
                            i.GROUP_ID = Utils.CIntDef(Drgroup.SelectedValue);
                            i.USER_CHINHANH = Utils.CIntDef(ddlChiNhanh.SelectedValue);
                        }
                        SendEmailChange(Txtname.Text, Txtusername.Text, Txtemail.Text, Utils.CStrDef(Drgroup.SelectedItem), rdoGioiTinh.SelectedValue == "1" ? "Nam" : "Nữ"
                        , txtNgaySinh.Text, txtCMND.Text, txtNgayCapCMND.Text, txtNoiCapCMND.Text, txtDanToc.Text, txtNguyenQuan.Text, txtNoiDK_HK.Text
                        , Txtaddress.Text, txtEmail_CaNhan.Text, Txtphone.Text, txtPhone_CaNhan.Text, txtTrinhDo.Text, txtNT_HoTen.Text, txtNT_SDT.Text, txtNT_MoiQuanHe.Text, getDiaDiem(ddlChiNhanh.SelectedValue));
                    }
                    else if (ASPxPageControl2.ActiveTabIndex == 1)
                    {//Khi Tab Index = 1 thì cho đổi pass
                        foreach (var i in list)
                        {
                            if (i.USER_PW != null && i.USER_PW != "")
                                SendEmailChangePas(Txtname.Text, Txtusername.Text, Txtemail.Text, Utils.CStrDef(rblActive.SelectedItem), Txtpass.Text);
                            else
                                SendEmailCreatePas(Txtname.Text, Txtusername.Text, Txtemail.Text, Utils.CStrDef(rblActive.SelectedItem), Txtpass.Text);
                            if (!string.IsNullOrEmpty(USER_PW))
                            {
                                i.SALT = SALT;
                                i.USER_PW = USER_PW;
                            }
                            i.USER_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        }
                    }
                    else
                    {
                        foreach (var i in list)
                        {
                            i.USER_LUONG_CB = Utils.CDecDef(txtLuongCanBan.Text.Replace(",", ""));
                            if (rdoCoBH.SelectedValue == "1")
                            {
                                i.USER_COBH = Utils.CIntDef(rdoCoBH.SelectedValue);
                                i.USER_LUONG_BH = Utils.CDecDef(txtLuongBHCD.Text.Replace(",", ""));
                                i.USER_BHXH_PT = Utils.CDblDef(txtPT_BHXH.Text.Replace(",", "."), 0);
                                i.USER_BHYT_PT = Utils.CDblDef(txtPT_BHYT.Text.Replace(",", "."), 0);
                                i.USER_BHTN_PT = Utils.CDblDef(txtPT_BHTN.Text.Replace(",", "."), 0);
                                i.USER_BHXH = Utils.CDecDef(txtCTBHXH.Text.Replace(",", ""));
                                i.USER_BHYT = Utils.CDecDef(txtCTBHYT.Text.Replace(",", ""));
                                i.USER_BHTN = Utils.CDecDef(txtCTBHTN.Text.Replace(",", ""));
                            }
                        }
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhan-vien.aspx?userid=" + _userid : strLink;
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
                var list = db.USERs.Where(n => n.USER_ID == _userid).ToList();
                if (!Check_Condition(list[0].USER_ID))
                {
                    db.USERs.DeleteAllOnSubmit(list);
                    Response.Redirect("danh-sach-nhan-vien.aspx");
                }
                else
                {
                    string strScript = "<script>";
                    strScript += "alert('Tên nhân viên này hiện đang sử dụng ở 1 hồ sơ nào đó! Để tránh phát sinh lỗi dữ liệu nên việc xóa không thực hiện được! ');";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
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
            if (checkexist_user(Txtusername.Text))
            {
                Lberrors.Text = "Tên đăng nhập đã tồn tại";
            }
            else
                Save();
        }

        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            if (checkexist_user(Txtusername.Text))
            {
                Lberrors.Text = "Tên đăng nhập đã tồn tại";
            }
            else
                Save("danh-sach-nhan-vien.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            if (checkexist_user(Txtusername.Text))
            {
                Lberrors.Text = "Tên đăng nhập đã tồn tại";
            }
            else
            Save("chi-tiet-nhan-vien.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-nhan-vien.aspx");
        }
        #endregion

        #region Funtion
        public bool Check_Condition(int _idUser)
        {
            var obj1 = db.WORKFLOW_USERs.Where(n => n.USER_ID == _idUser).ToList();
            var obj2 = db.PROFILE_NEWs.Where(n => n.USER_ID == _idUser).ToList();
            var obj3 = db.CONG_NOs.Where(n => n.NV_KT == _idUser || n.NV_KD == _idUser || n.NV_GN == _idUser).ToList();
            if (obj1.Count > 0 || obj2.Count > 0 || obj3.Count > 0)
            {
                return true;
            }
            else return false;
        }
        private void SendEmailCreatePas(string _name, string _user, string _email, string _active, string _matkhau)
        {
            try
            {
                int idUser = Utils.CIntDef(Session["Userid"]);
                var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                string _nameSend = _getUserSend[0].USER_NAME;
                string _mailBody = string.Empty;
                _mailBody += "<br/>Xin chào <b>" + _name + "</b>";
                _mailBody += "<br/><br/>Thông tin tài khoản đăng nhập trên website <i>http://quanly.ketoanvn.com.vn/</i> đã được kích hoạt thành công!";
                _mailBody += "<br/><br/><u>Thông tin tài khoản bao gồm:</u>";
                _mailBody += "<br/>Tên đăng nhập: <strong>" + _user + "</strong>";
                _mailBody += "<br/>Mật khẩu: <strong>" + _matkhau + "</strong>";
                _mailBody += "<br/>Trạng thái: <strong>" + _active + "</strong>";
                _mailBody += "<br/>Người kích hoạt tài khoản: <strong>" + _nameSend + "</strong>";
                _mailBody += "<br/><br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để thay đổi mật khẩu mới.<br/><br/>";
                _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                string _sMailBody = string.Empty;
                _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                sm.SendEmailSMTP("Thông tin tài khoản", _email, "", "", _sMailBody, true, false);
            }
            catch (Exception) { }
        }
        private void SendEmailChangePas(string _name, string _user, string _email, string _active, string _matkhau)
        {
            try
            {
                int idUser = Utils.CIntDef(Session["Userid"]);
                var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                string _nameSend = _getUserSend[0].USER_NAME;
                string _mailBody = string.Empty;
                _mailBody += "<br/>Xin chào <b>" + _name + "</b>";
                _mailBody += "<br/><br/>Thông tin tài khoản đăng nhập trên website <i>http://quanly.ketoanvn.com.vn/</i> đã được thay đổi!";
                _mailBody += "<br/><br/><u>Thông tin tài khoản bao gồm:</u>";
                _mailBody += "<br/>Tên đăng nhập: <strong>" + _user + "</strong>";
                _mailBody += "<br/>Mật khẩu: <strong>" + _matkhau + "</strong>";
                _mailBody += "<br/>Trạng thái: <strong>" + _active + "</strong>";
                _mailBody += "<br/>Người thay đổi tài khoản: <strong>" + _nameSend + "</strong>";
                _mailBody += "<br/><br/>Xin vui lòng đăng nhập vào website <a href='quanly.ketoanvn.com.vn'>quanly.ketoanvn.com.vn</a> để thay đổi mật khẩu mới.<br/><br/>";
                _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                string _sMailBody = string.Empty;
                _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                sm.SendEmailSMTP("Thông tin tài khoản", _email, "", "", _sMailBody, true, false);
            }
            catch (Exception) { }
        }
        private void SendEmailNew(string _name, string _user, string _email, string _group, string _gioiTinh, string _ngaysinh
            ,string _cmnd, string _cmndNgayCap, string _cmndNoiCap, string _danToc, string _nguyenQuan, string _noiDK_HK, string _diaChi
            ,string _emailCaNhan, string _phone, string _phoneCaNhan, string _trinhDo, string _nt_hoten, string _nt_sdt, string _nt_moiquanhe, string _chiNhanh)
        {
            try
            {
                int idUser = Utils.CIntDef(Session["Userid"]);
                var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                string _nameSend = _getUserSend[0].USER_NAME;
                string _mailBody = string.Empty;
                _mailBody += "<br/>Xin chào <b>" + _name + "</b>";
                _mailBody += "<br/><br/>Hồ sơ cá nhân của bạn trên website <i>http://quanly.ketoanvn.com.vn/</i> đã được tạo thành công!";
                _mailBody += "<br/><br/><u>Thông tin cá nhân bao gồm:</u>";
                _mailBody += "<br/>Chi nhánh: <strong>" + _chiNhanh + "</strong>";
                _mailBody += "<br/>Chức vụ: <strong>" + _group + "</strong>";
                _mailBody += "<br/>Họ và tên: <strong>" + _name + "</strong>";
                _mailBody += "<br/>Mã nhân viên: <strong>" + _user + "</strong>";
                _mailBody += "<br/>Gới tính: <strong>" + _gioiTinh + "</strong>";
                _mailBody += "<br/>Ngày/tháng/năm sinh: <strong>" + _ngaysinh + "</strong>";
                _mailBody += "<br/>Số CMND: <strong>" + _cmnd + "</strong>";
                _mailBody += "<br/>Ngày cấp (CMND): <strong>" + _cmndNgayCap + "</strong>";
                _mailBody += "<br/>Nơi cấp (CMND): <strong>" + _cmndNoiCap + "</strong>";
                _mailBody += "<br/>Dân tộc: <strong>" + _danToc + "</strong>";
                _mailBody += "<br/>Nguyên quán: <strong>" + _nguyenQuan + "</strong>";
                _mailBody += "<br/>Nơi đăng ký hộ khẩu thường trú: <strong>" + _noiDK_HK + "</strong>";
                _mailBody += "<br/>Nơi ở hiện nay: <strong>" + _diaChi + "</strong>";
                _mailBody += "<br/>Email (công ty): <strong>" + _email + "</strong>";
                _mailBody += "<br/>Email (cá nhân): <strong>" + _emailCaNhan + "</strong>";
                _mailBody += "<br/>Số điện thoại (công ty): <strong>" + _phone + "</strong>";
                _mailBody += "<br/>Số điện thoại (cá nhân): <strong>" + _phoneCaNhan + "</strong>";
                _mailBody += "<br/>Trình độ: <strong>" + _trinhDo + "</strong>";
                _mailBody += "<br/><u>Thông tin người thân liên hệ khi có việc gấp:</u>";
                _mailBody += "<br/>Họ và tên (người thân): <strong>" + _nt_hoten + "</strong>";
                _mailBody += "<br/>Số điện thoại (người thân): <strong>" + _nt_sdt + "</strong>";
                _mailBody += "<br/>Mối quan hệ: <strong>" + _nt_moiquanhe + "</strong>";
                _mailBody += "<br/><br/>Xin kiểm tra thật kỹ lại thông tin cá nhân, nếu có sai sót xin vui lòng Reply lại mail này và ghi rõ thông tin cần chỉnh sửa.<br/><br/>";
                _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                string _sMailBody = string.Empty;
                _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                sm.SendEmailSMTP("Thông tin cá nhân", _email, "", "", _sMailBody, true, false);
            }
            catch (Exception) { }
        }
        private void SendEmailChange(string _name, string _user, string _email, string _group, string _gioiTinh, string _ngaysinh
            , string _cmnd, string _cmndNgayCap, string _cmndNoiCap, string _danToc, string _nguyenQuan, string _noiDK_HK, string _diaChi
            , string _emailCaNhan, string _phone, string _phoneCaNhan, string _trinhDo, string _nt_hoten, string _nt_sdt, string _nt_moiquanhe, string _chiNhanh)
        {
            try
            {
                int idUser = Utils.CIntDef(Session["Userid"]);
                var _getUserSend = db.USERs.Where(n => n.USER_ID == idUser).ToList();
                string _nameSend = _getUserSend[0].USER_NAME;
                string _mailBody = string.Empty;
                _mailBody += "<br/>Xin chào <b>" + _name + "</b>";
                _mailBody += "<br/><br/>Hồ sơ cá nhân của bạn trên website <i>http://quanly.ketoanvn.com.vn/</i> đã được thay đổi!";
                _mailBody += "<br/><br/><u>Thông tin cá nhân bao gồm:</u>";
                _mailBody += "<br/>Chi nhánh: <strong>" + _chiNhanh + "</strong>";
                _mailBody += "<br/>Chức vụ: <strong>" + _group + "</strong>";
                _mailBody += "<br/>Họ và tên: <strong>" + _name + "</strong>";
                _mailBody += "<br/>Mã nhân viên: <strong>" + _user + "</strong>";
                _mailBody += "<br/>Gới tính: <strong>" + _gioiTinh + "</strong>";
                _mailBody += "<br/>Ngày/tháng/năm sinh: <strong>" + _ngaysinh + "</strong>";
                _mailBody += "<br/>Số CMND: <strong>" + _cmnd + "</strong>";
                _mailBody += "<br/>Ngày cấp (CMND): <strong>" + _cmndNgayCap + "</strong>";
                _mailBody += "<br/>Nơi cấp (CMND): <strong>" + _cmndNoiCap + "</strong>";
                _mailBody += "<br/>Dân tộc: <strong>" + _danToc + "</strong>";
                _mailBody += "<br/>Nguyên quán: <strong>" + _nguyenQuan + "</strong>";
                _mailBody += "<br/>Nơi đăng ký hộ khẩu thường trú: <strong>" + _noiDK_HK + "</strong>";
                _mailBody += "<br/>Nơi ở hiện nay: <strong>" + _diaChi + "</strong>";
                _mailBody += "<br/>Email (công ty): <strong>" + _email + "</strong>";
                _mailBody += "<br/>Email (cá nhân): <strong>" + _emailCaNhan + "</strong>";
                _mailBody += "<br/>Số điện thoại (công ty): <strong>" + _phone + "</strong>";
                _mailBody += "<br/>Số điện thoại (cá nhân): <strong>" + _phoneCaNhan + "</strong>";
                _mailBody += "<br/>Trình độ: <strong>" + _trinhDo + "</strong>";
                _mailBody += "<br/><u>Thông tin người thân liên hệ khi có việc gấp:</u>";
                _mailBody += "<br/>Họ và tên (người thân): <strong>" + _nt_hoten + "</strong>";
                _mailBody += "<br/>Số điện thoại (người thân): <strong>" + _nt_sdt + "</strong>";
                _mailBody += "<br/>Mối quan hệ: <strong>" + _nt_moiquanhe + "</strong>";
                _mailBody += "<br/><br/>Xin kiểm tra thật kỹ lại thông tin cá nhân, nếu có sai sót xin vui lòng Reply lại mail này và ghi rõ thông tin cần chỉnh sửa.<br/><br/>";
                _mailBody += "Ngày gửi <i>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "</i><br/>";
                string _sMailBody = string.Empty;
                _sMailBody += "Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                sm.SendEmailSMTP("Thông tin cá nhân", _email, "", "", _sMailBody, true, false);
            }
            catch (Exception) { }
        }
        private void GetBH(TextBox t1, TextBox t2)
        {
            double _pt = Utils.CDblDef(t1.Text.Replace(",","."), 0);
            double _luongBHCD = Utils.CDblDef(txtLuongBHCD.Text.Replace(",", "."), 0);
            double _ct = _Function.Round((_luongBHCD * _pt) / 100, -3);
            t2.Text = String.Format("{0:###,##0}", _ct);
        }
        public string getDiaDiem(object diaDiem)
        {
            string str = "";
            int _diaDiem = Utils.CIntDef(diaDiem);
            switch (_diaDiem)
            {
                case 1: str = "Tp.HCM - Trụ sở chính"; break;
                case 2: str = "Hà Nội - Chi nhánh"; break;
                case 3: str = "Nha Trang - Chi nhánh"; break;
                case 4: str = "Đà Nẵng - Chi nhánh"; break;
                default: str = "----"; break;
            }
            return str;
        }
        #endregion

        #region Event
        protected void txtPT_BHXH_TextChanged(object sender, EventArgs e)
        {
            GetBH(txtPT_BHXH, txtCTBHXH);
        }
        protected void txtPT_BHYT_TextChanged(object sender, EventArgs e)
        {
            GetBH(txtPT_BHYT, txtCTBHYT);
        }
        protected void txtPT_BHTN_TextChanged(object sender, EventArgs e)
        {
            GetBH(txtPT_BHTN, txtCTBHTN);
        }
        protected void lbtnTinhPhiBH_Click(object sender, EventArgs e)
        {
            double _luongBH = Utils.CDblDef(txtLuongBHCD.Text.Replace(",", ""), 0);
            if (_luongBH > 0)
            {
                GetBH(txtPT_BHXH, txtCTBHXH);
                GetBH(txtPT_BHYT, txtCTBHYT);
                GetBH(txtPT_BHTN, txtCTBHTN);
            }
            else
            {
                string strScript = "<script>";
                strScript += "alert('Xin nhập lương bảo hiểm! ');";
                strScript += "</script>";
                Page.RegisterClientScriptBlock("strScript", strScript);
                txtLuongBHCD.Focus();
            }
        }
        protected void rdoCoBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoCoBH.SelectedValue == "1")
                pHolderBH.Visible = true;
            else pHolderBH.Visible = false;
        }
        #endregion
    }
}