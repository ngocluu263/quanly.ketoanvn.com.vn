using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;
using System.Data;

namespace ThanhLapDN.Pages
{
    public partial class popup_kiem_tra_mst_cong_no : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        int year = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            year = Utils.CIntDef(Request.QueryString["year"]);
            if (!IsPostBack)
            {
                liTitle.Text = "CÔNG NỢ NĂM <b style='color: #FF0000;'>" + year + "</b>";
            }
        }

        #region Data
        private void Check_Data(int _year)
        {
            string str = Check_TrungMST(_year);
            if (str != "")
                txtContent.Text = str;
            else
                txtContent.Text = "----------Không tìm thấy lỗi---------";
        }
        #endregion

        #region Funtion
        private string Check_TrungMST(int _year)
        {
            string str = "";
            string lenh = "SELECT MST,NAM, count(*) FROM CONG_NO WHERE MST != '' and NAM = " + _year + " GROUP BY MST,NAM HAVING count(*) > 1";
            DataTable dt = new DataTable();
            dt = XLDLRepo.ReadData(lenh);
            int _count = dt.Rows.Count;
            if (_count > 0)
            {
                str += "Đã phát hiện ra năm " + _year + " có " + _count + " mã số thuế bị trùng----\n\n";
                for (int i = 0; i < _count; i++)
                {
                    str += (i + 1) + ".MST: " + dt.Rows[i]["MST"] + "\n";
                    var list = db.CONG_NOs.Where(n => n.MST == Utils.CStrDef(dt.Rows[i]["MST"], "") && n.NAM == _year).ToList();
                    if (list.Count > 0)
                    {
                        str += "---Năm / STT / MST / Tên công ty\n";
                        for (int j = 0; j < list.Count; j++)
                        {
                            str += String.Format("---{0} / {1} / {2} / {3}\n"
                                , list[j].NAM, list[j].STT, list[j].MST, list[j].TEN_KH);
                        }
                    }
                }
                return str;
            }
            else { return ""; }
        }
        #endregion

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Check_Data(year);
            //ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>parent.emailwindow.close(); parent.GetCurrentTime()</script>");
        }
    }
}