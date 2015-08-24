using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThanhLapDN.Data;
using vpro.functions;

namespace ThanhLapDN.Pages
{
    public partial class popup_danh_sach_thanh_vien : System.Web.UI.Page
    {
        ProfileMember member = new ProfileMember();
        AppketoanDataContext db = new AppketoanDataContext();
        int _id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                rdbType_SelectedIndexChanged(sender, e);
            }
        }

        #region funtion
        private bool CheckExists(int id_prof , int type)
        {
            var _obj = db.PROFILE_MEMBERs.Where(n => n.PROF_ID == id_prof && n.MEM_TYPE == type).ToList();
            if (_obj.Count > 0)
            {
                return true;
            }
            else return false;
        }
        #endregion
        private void SaveData()
        {
            if (_id != 0)
            {
                PROFILE_MEMBER i = new PROFILE_MEMBER();
                i.MEM_TYPE = Utils.CIntDef(rdbType.SelectedValue);
                i.MEM_FULLNAME = txtHovaTen.Text;
                i.MEM_BIRTHDAY = pickdate_deli.returnDate;
                i.MEM_FIELD1 = txtDanToc.Text;
                i.MEM_SEX = Utils.CIntDef(rdbGioiTinh.SelectedValue);
                i.MEM_NATIONALITY = txtQuocTich.Text;
                i.MEM_CMND = txtCMND.Text;
                i.MEM_DATE_CMND = pickdate_CMND.returnDate;
                i.MEM_ADDRESS_CMND = txtNoiCapCMND.Text;
                i.MEM_HOUSEHOLD = txtHKTT.Text;
                i.MEM_ADDRESS = txtTamTru.Text;
                i.MEM_CAPITAL = Utils.CDecDef(txtVonGop.Text.Replace(",", ""));
                i.MEM_PERCENT = Utils.CIntDef(txtTyLe.Text.Replace(",", ""));
                i.MEM_ACTIVE = 1;
                i.MEM_STATUS = 1;
                i.MEM_DATE = DateTime.Now;
                i.PROF_ID = _id;
                if (rdbType.SelectedValue == "0")
                {
                    i.MEM_POSTION = txtChucDanh.Text;
                    i.MEM_FIELD2 = txtNguoiGiuChucConLai.Text;
                }
                member.Create(i);
            }
            else
            {
 
            }
        }
        private void ShowId( bool b)
        {
            iChucDanh.Visible = iNguoiGiuChucDanh.Visible = b;
        }

        protected void rdbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbType.SelectedValue == "0")
            {
                ShowId(true);
                if (CheckExists(_id, Utils.CIntDef(rdbType.SelectedValue)))
                {
                    lblMsg.Text = "Người đại diện đã được tạo!";
                    btnSave.Enabled = false;
                    return;
                }
            }
            else
            {
                lblMsg.Text = "";
                btnSave.Enabled = true;
                ShowId(false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
            }
            catch (Exception) { throw; }
        }

        #region CMND
        protected void txtCMND_TextChanged(object sender, EventArgs e)
        {
            Load_CMND();
        }
        private void Load_CMND()
        {
            var list = db.CMNDs.ToList();
            if (list.Count > 0)
            {
                string s = txtCMND.Text;
                string sNews = "";
                if (s.Length > 1)
                {
                    sNews = s.Substring(0, 2);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].CMND_CODE.Contains(sNews))
                        {
                            txtNoiCapCMND.Text = list[i].CMND_NAME;
                            return;
                        }
                        else { txtNoiCapCMND.Text = ""; }
                    }
                }
            }
        }
        #endregion
    }
}