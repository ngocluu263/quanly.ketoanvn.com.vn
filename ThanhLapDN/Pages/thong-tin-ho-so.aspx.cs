using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using ThanhLapDN.Data;
using System.Data;
using DevExpress.Web.ASPxTreeList;

namespace ThanhLapDN.Pages
{
    public partial class thong_tin_ho_so : System.Web.UI.Page
    {
        AppketoanDataContext db = new AppketoanDataContext();
        UnitData unit_data = new UnitData();
        int _id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string group_id = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_GROUP_ID"]);
            _id = Utils.CIntDef(Request.QueryString["id"]);
            Load_Proj(_id);
            LoadLinkMem();
            LoadTypeReg(_id);
            CheckTypeReg(_id);
            if (group_id != "1" && group_id != "2")
            {
                CheckInfoGroup(false);
            }
        }
        public string getfiledinhkem(object obj_id, object file)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/File/Profile/" + Utils.CStrDef(obj_id) + "/" + Utils.CStrDef(file);
            return path;
        }
        public string GetTrades(int _idTrades)
        {
            string str = "";
            var list = db.PROFILE_TRADEs.Where(n => n.TRAD_ACTIVE == 1 && n.ID == _idTrades).ToList();
            if (list.Count > 0)
            {
                str = list[0].TRAD_NAME;
            }
            return str;
        }
        private void Load_Proj(int idProj)
        {
            var _obj = db.PROFILE_NEWs.Where(n => n.ID == idProj).ToList();
            if (_obj.Count > 0)
            {
                string _sLoai = "";
                if (Utils.CIntDef(_obj[0].PROF_TYPE) == 1) { _sLoai = "HỒ SƠ THÀNH LẬP MỚI"; }
                else if (Utils.CIntDef(_obj[0].PROF_TYPE) == 1) { _sLoai = "HỒ SƠ THAY ĐỔI"; }
                else { _sLoai = "HỒ SƠ HÀNH CHÁNH"; }
                lblLoai.Text = _sLoai;
                lblDinhKem.Text = "<a href='" + getfiledinhkem(_obj[0].ID, _obj[0].PROF_FILE) + "'>" + _obj[0].PROF_FILE + "</a>";
                lblTongPhi.Text = _obj[0].PROF_COST1.Value.ToString("###,##0 vnđ");
                lblDaThu.Text = _obj[0].PROF_COST2.Value.ToString("###,##0 vnđ");
                lblConLai.Text = (Utils.CIntDef(_obj[0].PROF_COST1) - Utils.CIntDef(_obj[0].PROF_COST2)).ToString("###,##0 vnđ");
                if (Utils.CIntDef(_obj[0].PROF_TYPE) == 1)
                {
                    ShowNews(true);
                    lbltenCongTy.Text = _obj[0].PROF_NAME;
                    lblTenGD.Text = _obj[0].PROF_TRANSACTION;
                    lblTenVT.Text = _obj[0].PROF_ATC;
                    lblTruSoChinh.Text = _obj[0].PROF_ADDRESS;
                    lblNganhNgheKD.Text = GetTrades(Utils.CIntDef(_obj[0].TRADES_ID));
                    lblTongVopGop.Text = _obj[0].PROF_TOTAL_CAPITAL.Value.ToString("###,##0 vnđ");
                    lblVonPhapDinh.Text = _obj[0].PROF_CAPITAL.Value.ToString("###,##0 vnđ");
                    lblDienThoai.Text = _obj[0].PROF_PHONE;
                }
                else if (Utils.CIntDef(_obj[0].PROF_TYPE) == 2)
                {
                    ShowNews(false);
                    lblCTenCongTy.Text = _obj[0].PROF_NAME;
                    lblCDiaChiCty.Text = _obj[0].PROF_ADDRESS;
                    lblCMST.Text = _obj[0].PROF_TAXCODE;
                    lblCDienThoai.Text = _obj[0].PROF_PHONE;
                    lblCNoiDungCanDoi.Text = _obj[0].PROF_NOTE;
                }
                else {
                    var objCheck = db.PROFILE_NEWs.Where(n => n.ID == _obj[0].PROF_PARENT).ToList();
                    if (objCheck.Count > 0)
                    {
                        if (objCheck[0].PROF_TYPE == 1)
                        {
                            ShowNews(true);
                            idNew0.Visible = false;
                            idCreate3.Visible = true;
                            lbltenCongTy.Text = _obj[0].PROF_NAME;
                            lblTenGD.Text = _obj[0].PROF_TRANSACTION;
                            lblTenVT.Text = _obj[0].PROF_ATC;
                            lblTruSoChinh.Text = _obj[0].PROF_ADDRESS;
                            lblNganhNgheKD.Text = GetTrades(Utils.CIntDef(_obj[0].TRADES_ID));
                            lblTongVopGop.Text = _obj[0].PROF_TOTAL_CAPITAL.Value.ToString("###,##0 vnđ");
                            lblVonPhapDinh.Text = _obj[0].PROF_CAPITAL.Value.ToString("###,##0 vnđ");
                            lblDienThoai.Text = _obj[0].PROF_PHONE;
                            lblCMST.Text = _obj[0].PROF_TAXCODE;
                            liMsgType3.Text = "<div style='color:#0099FF;'>Hồ sơ này được chuyển từ HỒ SƠ THÀNH LẬP CÔNG TY MỚI.</div>";
                        }
                        else
                        {
                            ShowNews(false);
                            idNew0.Visible = false;
                            lblCTenCongTy.Text = _obj[0].PROF_NAME;
                            lblCDiaChiCty.Text = _obj[0].PROF_ADDRESS;
                            lblCMST.Text = _obj[0].PROF_TAXCODE;
                            lblCDienThoai.Text = _obj[0].PROF_PHONE;
                            lblCNoiDungCanDoi.Text = _obj[0].PROF_NOTE;
                            liMsgType3.Text = "<div style='color:#0099FF;'>Hồ sơ này được chuyển từ HỒ SƠ THAY ĐỔI.</div>";
                        }
                    }
                    else
                    {
                        ShowNews(true);
                        idNew0.Visible = false;
                        idCreate3.Visible = true;
                        lbltenCongTy.Text = _obj[0].PROF_NAME;
                        lblTenGD.Text = _obj[0].PROF_TRANSACTION;
                        lblTenVT.Text = _obj[0].PROF_ATC;
                        lblTruSoChinh.Text = _obj[0].PROF_ADDRESS;
                        lblNganhNgheKD.Text = GetTrades(Utils.CIntDef(_obj[0].TRADES_ID));
                        lblTongVopGop.Text = _obj[0].PROF_TOTAL_CAPITAL.Value.ToString("###,##0 vnđ");
                        lblVonPhapDinh.Text = _obj[0].PROF_CAPITAL.Value.ToString("###,##0 vnđ");
                        lblDienThoai.Text = _obj[0].PROF_PHONE;
                        lblCMST.Text = _obj[0].PROF_TAXCODE;
                    }
                }
            }
        }
        public void ShowNews(bool b)
        {
            idNew1.Visible = idNew2.Visible = idNew3.Visible = idNew4.Visible = idNew5.Visible = idNew6.Visible = idNew7.Visible = idNew8.Visible = idNew9.Visible = b;
            idCreate1.Visible = idCreate2.Visible = idCreate3.Visible = idCreate4.Visible = idCreate5.Visible = !b;
        }
        public void CheckInfoGroup(bool b)
        {
            idMain1.Visible = idMain2.Visible = idMain3.Visible = idMain4.Visible = idMain5.Visible = idMain6.Visible = b;
        }
        public void LoadLinkMem()
        {
            if (_id != 0)
                liLoadMemLink.Text = @"<a href='#' onClick='openDSTV1(" + _id + "); return false' title='Chi tiết'>Danh sách thành viên</a>";
        }

        #region TypeReg
        private void LoadTypeReg(int _id_prof)
        {
            try
            {
                //Kiểm tra type của prof
                int _type = 0;
                var _objType = db.PROFILE_NEWs.Where(n => n.ID == _id_prof).ToList();
                if (_objType.Count > 0)
                {
                    _type = Utils.CIntDef(_objType[0].PROF_TYPE);
                }
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
        #endregion
    }
}