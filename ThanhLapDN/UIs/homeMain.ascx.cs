using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using DevExpress.Web.ASPxGridView;
using ThanhLapDN;
using ThanhLapDN.Data;
using System.Drawing;

namespace QuanLyDuAn.UIs
{
    public partial class homeMain : System.Web.UI.UserControl
    {
        #region Declare
        private ProfileData _ProjectData = new ProfileData();
        AppketoanDataContext db = new AppketoanDataContext();
        UnitData unitdata = new UnitData();
        int idUser = 0;
        int idGroup = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            idUser = Utils.CIntDef(Session["Userid"]);
            idGroup = Utils.CIntDef(Session["Groupid"]);
            if (!IsPostBack)
            {
                CheckGroupSearch();
                Session["DangTienHanh"] = Session["DaHoanThanh"] = 0;
                Load_Search();
                LoadSoLuongYeuCau();
            }
        }

        #region Data
        //private void LoadData(int _type, ASPxGridView grv)
        //{
        //    try
        //    {

        //        if (idGroup != 1 && idGroup != 4 && idGroup != 2 && idGroup != 14)
        //        {
        //            var listPlus = (from a in db.PROFILE_NEWs where a.USER_ID == idUser select a)
        //                        .Union(from a in db.PROFILE_NEWs
        //                               join b in db.WORKFLOW_USERs on a.ID equals b.PROF_ID
        //                               where b.USER_ID == idUser 
        //                               select a).OrderByDescending(n => n.PROF_DATE);
        //            if (listPlus.Count() > 0)
        //            {
        //                var listNew = listPlus.Where(n => n.PROF_TYPE == _type && n.PROF_STATUS != 12 );
        //                grv.DataSource = listNew;
        //                grv.DataBind();
        //                Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + listNew.Count(n => n.PROF_STATUS != 12 );
        //                Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + listNew.Count(n => n.PROF_STATUS == 12 );
        //            }
        //            else
        //            {
        //                var listDom = db.PROFILE_NEWs.Where(n => n.USER_ID == idUser && n.PROF_TYPE == _type && n.PROF_STATUS != 12 ).OrderByDescending(n => n.PROF_DATE);
        //                grv.DataSource = listDom;
        //                grv.DataBind();
        //                Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + listDom.Count(n => n.PROF_STATUS != 12 );
        //                Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + listDom.Count(n => n.PROF_STATUS == 12 );
        //            }
        //        }
        //        else
        //        {
        //            var list = db.PROFILE_NEWs.Where(n => n.PROF_TYPE == _type && n.PROF_STATUS != 12 ).OrderByDescending(n => n.PROF_DATE);
        //            grv.DataSource = list;
        //            grv.DataBind();
        //            Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + list.Count(n => n.PROF_STATUS != 12);
        //            Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + list.Count(n => n.PROF_STATUS == 12);
        //        }
        //    }
        //    catch //(Exception)
        //    {

        //        //throw;
        //    }
        //}
        private void LoadSoLuongYeuCau()
        {
            if (Session["DangTienHanh"].ToString() != "0")
            {
                lblDangTienHanh.Text = "Hồ sơ đang tiến hành: <b style='color:#FF0000;font-size:14px;'>" + Session["DangTienHanh"].ToString() + "</b>";
            }
            else { lblDangTienHanh.Text = ""; }
            if (Session["DaHoanThanh"].ToString() != "0")
            {
                lblDaHoanThanh.Text = "Hồ sơ đã hoàn thành: <b style='color:#0099FF;font-size:14px;'>" + Session["DaHoanThanh"].ToString() + "</b>";
            }
            else { lblDaHoanThanh.Text = ""; }
        }
        private void LoadSearch(int _type, int _status, ASPxGridView grv)
        {
            try
            {

                if (idGroup != 1 && idGroup != 2 && idGroup != 4 && idGroup != 10 && idGroup != 14)
                {
                    var listPlus = (from a in db.PROFILE_NEWs where a.USER_ID == idUser select a)
                                .Union(from a in db.PROFILE_NEWs
                                       join b in db.WORKFLOW_USERs on a.ID equals b.PROF_ID
                                       where b.USER_ID == idUser
                                       select a).OrderByDescending(n => n.PROF_DATE);
                    if (listPlus.Count() > 0)
                    {
                        if (_status == 0)
                        {
                            var listNew = listPlus.Where(n => n.PROF_TYPE == _type && n.PROF_STATUS != 12 );
                            grv.DataSource = listNew;
                            grv.DataBind();
                            Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + listNew.Count(n => n.PROF_STATUS != 12);
                            Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + listNew.Count(n => n.PROF_STATUS == 12);
                        }
                        else
                        {
                            var listNew = listPlus.Where(n => n.PROF_TYPE == _type && n.PROF_STATUS == 12 );
                            grv.DataSource = listNew;
                            grv.DataBind();
                            Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + listNew.Count(n => n.PROF_STATUS != 12);
                            Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + listNew.Count(n => n.PROF_STATUS == 12);
                        }
                    }
                    else
                    {
                        if (_status == 0)
                        {
                            var listDom = db.PROFILE_NEWs.Where(n => n.USER_ID == idUser && n.PROF_TYPE == _type && n.PROF_STATUS != 12).OrderByDescending(n => n.PROF_DATE);
                            grv.DataSource = listDom;
                            grv.DataBind();
                            Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + listDom.Count(n => n.PROF_STATUS != 12);
                            Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + listDom.Count(n => n.PROF_STATUS == 12);
                        }
                        else
                        {
                            var listDom = db.PROFILE_NEWs.Where(n => n.USER_ID == idUser && n.PROF_TYPE == _type && n.PROF_STATUS == 12).OrderByDescending(n => n.PROF_DATE);
                            grv.DataSource = listDom;
                            grv.DataBind();
                            Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + listDom.Count(n => n.PROF_STATUS != 12);
                            Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + listDom.Count(n => n.PROF_STATUS == 12);
                        }
                    }
                }
                else
                {
                    if (_status == 0)
                    {
                        var list = db.PROFILE_NEWs.Where(n => n.PROF_TYPE == _type && n.PROF_STATUS != 12).OrderByDescending(n => n.PROF_DATE);
                        grv.DataSource = list;
                        grv.DataBind();
                        Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + list.Count(n => n.PROF_STATUS != 12);
                        Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + list.Count(n => n.PROF_STATUS == 12);
                    }
                    else
                    {
                        var list = db.PROFILE_NEWs.Where(n => n.PROF_TYPE == _type && n.PROF_STATUS == 12).OrderByDescending(n => n.PROF_DATE);
                        grv.DataSource = list;
                        grv.DataBind();
                        Session["DangTienHanh"] = Utils.CIntDef(Session["DangTienHanh"]) + list.Count(n => n.PROF_STATUS != 12);
                        Session["DaHoanThanh"] = Utils.CIntDef(Session["DaHoanThanh"]) + list.Count(n => n.PROF_STATUS == 12);
                    }
                }
            }
            catch //(Exception)
            {

                //throw;
            }
        }
        protected void Load_Search()
        {
            Session["DangTienHanh"] = Session["DaHoanThanh"] = 0;
            if (Request.QueryString["searchId"] == "0" || Request.QueryString["searchId"] == "1")
            {
                int _SearchId = Utils.CIntDef(Request.QueryString["searchId"]);
                LoadSearch(1, _SearchId, grv_create);
                LoadSearch(2, _SearchId, grv_change);
                LoadSearch(3, _SearchId, grv_acc);
                LoadSoLuongYeuCau();

                ddlListStatus.SelectedValue = Request.QueryString["searchId"];
            }
            else
            {
                LoadSearch(1, 0, grv_create);
                LoadSearch(2, 0, grv_change);
                LoadSearch(3, 0, grv_acc);
                LoadSoLuongYeuCau();
            }
        }
        #endregion
        #region Function
        public string getNote(object note)
        {
            string _note = Utils.CStrDef(note);
            if (_note != "")
                return "<div class='items'><i class='ic'></i>Ghi chú: " + _note + "</div>";
            else return "<div class='items'><i class='ic'></i>Ghi chú: ---</div>";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm tt}", News_PublishDate);
        }
        public string Getstatus(object status, object type)
        {
            return unitdata.Getstatus(status, type);
        }
        public string GetsLevel(object level)
        {
            int _level = Utils.CIntDef(level);
            string str = "";
            if (_level == 2)
            {
                str = "<b style='color:#0000FF;'>Đã cấp giấy phép (tiến hành khắc dấu)</b>";
            }
            
            return str;
        }
        public string getfiledinhkem(object obj_id, object file)
        {
            string path = "-";
            path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + Utils.CStrDef(obj_id) + "/" + Utils.CStrDef(file);
            return path;
        }
        public string getfiledinhkemPlus(object pro_id,object obj_id, object file)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + Utils.CStrDef(pro_id) + "/" + Utils.CStrDef(obj_id) + "/" + Utils.CStrDef(file);
            return path;
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
        public string GetUserAtt(object _id)
        {
            var list = db.USERs.Where(a => a.USER_ID == Convert.ToInt32(_id)).ToList();
            if (list.Count > 0)
            {
                return " - <i>Người gửi: " + list[0].USER_NAME + "</i>" ;
            }
            else { return ""; }
        }
        public string getSendProcess(object id, object status, object type)
        {
            int _id = Utils.CIntDef(id);
            int _status = Utils.CIntDef(status);
            int _type = Utils.CIntDef(type);
            string str = "";
            if (_type != 3 && (idGroup != 1 && idGroup != 2 && idGroup != 3 && unitdata.CheckGroup(_status,_type) == idGroup))
            {
                if (_status == 4)
                {
                    str = "<a href='#' onClick='openProcesHS(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 6)
                {
                    str = "<a href='#' onClick='openProcesGiaoHS(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 7)
                {
                    str = "<a href='#' onClick='openProcesNopHS(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 8)
                {
                    str = "<a href='#' onClick='openProcesTraHS(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 9 && _type != 3)
                {//Giai đoạn này cả 3 loại đều hoàn thành, nhưng chỉ có 1,2 là có thể chọn làm Hồ sơ hành chánh
                    str = "<a href='#' onClick='openProcesFn(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 10)
                {
                    str = "<a href='#' onClick='openProcesKDHS(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 11 && (_type == 1 || _type == 2))
                {
                    str = "<a href='#' onClick='openProcesFn(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status > 11)
                {
                    str = "";
                }
                else
                    str = "<a href='#' onClick='openProces(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
            }

            if (_type == 3)
            {
                if (_status == 1 && unitdata.CheckGroup(_status, _type) == idGroup)
                {
                    str = "<a href='#' onClick='openProces1_KT(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 2 && (idGroup == 11 || idGroup == 7 || idGroup == 13))
                {//Trường hợp đặc biệt không theo quy trình là có thể chọn nhiều người trong nhiều group khác nhau ở lúc tạo hồ sơ Hành chánh
                    str = "<a href='#' onClick='openProces2_KT(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 3 && unitdata.CheckGroup(_status,_type) == idGroup)
                {
                    str = "<a href='#' onClick='openProces(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 4 && unitdata.CheckGroup(_status, _type) == idGroup)
                {
                    str = "<a href='#' onClick='openProcesHS_KT(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 5 && unitdata.CheckGroup(_status, _type) == idGroup)
                {
                    str = "<a href='#' onClick='openProcesTraHS_KT(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
                else if (_status == 6 && unitdata.CheckGroup(_status, _type) == idGroup)
                {
                    str = "<a href='#' onClick='openProcesFnMain(" + _id + "," + _status + "," + _type + "); return false' title='Chuyển tiếp'><img src='../Images/User-roles.png' width='18' height='18'/></a>";
                }
            }
            return str;
        }
        public string getSkipProcess(object id, object status, object type)
        {
            int _id = Utils.CIntDef(id);
            int _status = Utils.CIntDef(status);
            int _type = Utils.CIntDef(type);
            string str = "";
            if (idGroup == 4)
            {
                if (_type == 1 && _type == 2)
                {
                    if (_status > 0 && _status < 8)
                        str = "<a href='#' onClick='openProcesSkip(" + _id + "," + _status + "," + _type + "); return false' title='Bỏ qua quy trình này'><img src='../Images/skip_forward.png' width='18' height='18'/></a>";
                }
                else 
                    if (_status > 0 && _status < 5)
                        str = "<a href='#' onClick='openProcesSkip(" + _id + "," + _status + "," + _type + "); return false' title='Bỏ qua quy trình này'><img src='../Images/skip_forward.png' width='18' height='18'/></a>";
            }
            return str;
        }
        public string getStatusPlus(object status, object status1, object type)
        {
            int _st = Utils.CIntDef(status);
            int _st1 = Utils.CIntDef(status1);
            int _type = Utils.CIntDef(type);
            string str = "";
            if (_st == 4)
            {
                switch (_st1)
                {
                    case 0: str = "<b style='color:#996633;'>Đã nhận</b>"; break;
                    case 1: str = "<b style='color:#996633;'>Đã giao khách</b>"; break;
                }
            }
            else if (_st == 6)
            {
                switch (_st1)
                {
                    case 1: str = "<b style='color:#996633;'>Chưa giao</b>"; break;
                }
            }
            else if (_st == 7)
            {
                switch (_st1)
                {
                    case 1: str = "<b style='color:#996633;'>Chưa nộp</b>"; break;
                }
            }
            else if (_st == 10)
            {
                switch (_st1)
                {
                    case 1: str = "<b style='color:#996633;'>Đã nộp hồ sơ</b>"; break;
                }
            }
            return str;
        }
        public string getStatusPlusKT(object status, object status1, object type)
        {
            int _st = Utils.CIntDef(status);
            int _st1 = Utils.CIntDef(status1);
            int _type = Utils.CIntDef(type);
            string str = "";
            if ( _st == 4)
            {
                switch (_st1)
                {
                    case 0: str = "<b style='color:#996633;'>Đã nhận</b>"; break;
                    case 1: str = "<b style='color:#996633;'>Đã giao khách</b>"; break;
                }
            }
            else if (_st == 5)
            {
                switch (_st1)
                {
                    case 1: str = "<b style='color:#996633;'>Chờ kết quả khấu trừ</b>"; break;
                    case 2: str = "<b style='color:#996633;'>Chờ kết quả đặt in</b>"; break;
                    case 3: str = "<b style='color:#996633;'>Chờ kết quả khấu trừ và đặt in</b>"; break;
                }
            }
            return str;
        }
        public string getProcessStatus(object id,object status,object type)
        {
            int _id = Utils.CIntDef(id);
            int _status = Utils.CIntDef(status);
            int _type = Utils.CIntDef(type);
            string str = "";
            if (_type != 3 && (_status == 4 || _status == 6 ||_status == 7|| _status == 10 || _status == 5))
            {
                var _getStatus = db.WORKFLOW_STATUS.Where(n => n.PROF_ID == _id && n.ST_STATUS == _status).ToList();
                if (_getStatus.Count > 0)
                {
                    str = "<br/>" + getStatusPlus(_status, _getStatus[0].ST_STATUS1, _type);
                }
            }
            else if (_type == 3 && (_status == 4 || _status == 5))
            {
                var _getStatus = db.WORKFLOW_STATUS.Where(n => n.PROF_ID == _id && n.ST_STATUS == _status).ToList();
                if (_getStatus.Count > 0)
                {
                    str = "<br/>" + getStatusPlusKT(_status, _getStatus[0].ST_STATUS1, _type);
                }
            }
            return str;
        }
        public string getEditFile(object id, object id_prof)
        {
            int _id = Utils.CIntDef(id);
            int _id_prof = Utils.CIntDef(id_prof);
            string str = "";
            if (idGroup == 4)
            {
                str = "<a href='#' onClick='openEditFile(" + _id + "," + _id_prof + "); return false' title='Chỉnh sửa file đính kèm'><i class='iedit'></i></a>";
            }
            return str;
        }
        public List<WORKFLOW_USER> listWorkflow(object id)
        {
            var list = unitdata.listWorkflow(id);
            return list;
        }
        public List<PROFILE_ATTACH> listFile(object id)
        {
            var list = unitdata.listFile(id);
            return list;
        }
        private void CheckGroupSearch()
        {
            //if (idGroup != 1 && idGroup != 3 && idGroup != 2)
            //{
            //    iSearch.Visible = false;
            //}
            //else { iSearch.Visible = true; }
            iSearch.Visible = true;
        }
        #endregion

        #region Event
        protected void grv_create_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Load_Search();
        }
        protected void grv_create_PageIndexChanged(object sender, EventArgs e)
        {
            Load_Search();
        }
        protected void grv_create_DataBound(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    ((ASPxGridView)sender).DetailRows.ExpandAllRows();
        }
        protected void grv_change_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Load_Search();
        }
        protected void grv_change_PageIndexChanged(object sender, EventArgs e)
        {
            Load_Search();
        }
        protected void grv_change_DataBound(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    ((ASPxGridView)sender).DetailRows.ExpandAllRows();
        }
        protected void grv_acc_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            Load_Search();
        }
        protected void grv_acc_PageIndexChanged(object sender, EventArgs e)
        {
            Load_Search();
        }
        protected void grv_acc_DataBound(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    ((ASPxGridView)sender).DetailRows.ExpandAllRows();
        }
        protected void grv_create_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            int _id = Utils.CIntDef(e.KeyValue);
            var _obj = unitdata.listWorkflowByStatus(_id, 2);
            if (_obj.Count > 1 && Request.QueryString["searchId"] != "1")
            {
                e.Row.BackColor = Color.FromName("#DFD4C9");
            }         
        }
        protected void grv_change_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            int _id = Utils.CIntDef(e.KeyValue);
            var _obj = unitdata.listWorkflowByStatus(_id, 2);
            if (_obj.Count > 1 && Request.QueryString["searchId"] != "1")
            {
                e.Row.BackColor = Color.FromName("#DFD4C9");
            }
        }
        protected void grv_acc_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            int _id = Utils.CIntDef(e.KeyValue);
            var _obj = unitdata.listWorkflowByStatus(_id, 2);
            if (_obj.Count > 1 && Request.QueryString["searchId"] != "1")
            {
                e.Row.BackColor = Color.FromName("#DFD4C9");
            }
        }

        protected void OnLoad(object sender, EventArgs e)
        {
            Load_Search();
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            int _iS = Utils.CIntDef(ddlListStatus.SelectedValue);
            Response.Redirect("/Pages/trang-chu.aspx?searchId=" + _iS);
        }
        #endregion
    }
}