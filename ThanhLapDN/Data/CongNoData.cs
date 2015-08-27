using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;
using System.Data;
using Appketoan.Components;

namespace ThanhLapDN.Data
{
    public class CongNoData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CONG_NO> GetListByName(string name)
        {
            return this.db.CONG_NOs.Where(n => (n.TEN_KH.Contains(name) || name == "")).OrderBy(n => n.TEN_KH).ToList();
        }
        public virtual List<CONG_NO> GetListByYear(int _year, string _field, string keyword, string _status)
        {
            string _txt = clsFormat.ClearUnicode(keyword);

            return this.db.CONG_NOs.Where(n => (n.NAM == _year)
                && (n.TINH_TRANG == getTinhTrang(_status) || "" == _status)
                && (_field == "1" ? (db.fClearUnicode(n.TEN_KH).Contains(_txt) || "" == _txt) :
                            _field == "2" ? (n.MST.Contains(_txt) || "" == _txt) :
                            _field == "3" ? ((iListArea(_txt).Contains(n.QL_THUE_DIST ?? 0)) || (iListArea(_txt).Contains(n.QL_THUE_CITY ?? 0))) :
                            _field == "4" ? (db.fClearUnicode(n.DIA_CHI).Contains(_txt) || "" == _txt) :
                            _field == "5" ? (n.DIEN_THOAI.Contains(_txt) || "" == _txt) :
                            _field == "6" ? (iListUser(_txt).Contains(n.NV_KT ?? 0)) :
                            _field == "7" ? (iListUserKD(_txt).Contains(n.NV_KD ?? 0)) :
                            _field == "8" ? (iListUser(_txt).Contains(n.NV_GN ?? 0)) :
                            _field == "9" ? ((n.PHI == null || n.PHI == 0) && (n.BIEUPHI1_SL == null || n.BIEUPHI1_SL == "")) :
                            _field == "10" ? ((n.NV_KT == null || n.NV_KT == 0)) :
                            _field == "11" ? ((n.NV_KD == null || n.NV_KD == 0)) :
                            _field == "12" ? ((n.NV_GN == null || n.NV_GN == 0)) :
                            "" == _txt)
                ).OrderByDescending(n => n.STT).ToList();
        }
        private List<int> iListUser(string _txt)
        {
            
            List<int> list = (from a in db.USERs where db.fClearUnicode(a.USER_NAME).Contains(_txt) select a.USER_ID).ToList();
            return list;
        }
        private List<int> iListUserKD(string _txt)
        {
            string temp = "nhân viên công ty";
            if (temp.ToLower().Contains(_txt))
            {
                List<int> list = new List<int>();
                list.Add(9999);
                return list;
            }
            else
            {
                List<int> list = (from a in db.USERs where db.fClearUnicode(a.USER_NAME).Contains(_txt) select a.USER_ID).ToList();
                return list;
            }
        }
        private List<decimal> iListArea(string _txt)
        {
            List<decimal> list = (from a in db.AREA_PROPERTies where db.fClearUnicode(a.PROP_NAME).Contains(_txt) select a.PROP_ID).ToList();
            return list;
        }
        public virtual List<CONG_NO> GetListByYearOrder(int _year,string _field, string _txt)
        {
            return this.db.CONG_NOs.Where(n => (n.NAM == _year)
                && (_field == "1" ? (n.TEN_KH.Contains(_txt) || "" == _txt) :
                            _field == "2" ? (n.MST.Contains(_txt) || "" == _txt) :
                            _field == "3" ? ((iListArea(_txt).Contains(n.QL_THUE_DIST ?? 0)) || (iListArea(_txt).Contains(n.QL_THUE_CITY ?? 0))) :
                            _field == "4" ? (n.DIA_CHI.Contains(_txt) || "" == _txt) :
                            _field == "5" ? (n.DIEN_THOAI.Contains(_txt) || "" == _txt) :
                            _field == "6" ? (iListUser(_txt).Contains(n.NV_KT ?? 0)) :
                            _field == "7" ? (iListUser(_txt).Contains(n.NV_KD ?? 0)) :
                            _field == "8" ? (iListUser(_txt).Contains(n.NV_GN ?? 0)) :
                            "" == _txt)
                ).OrderBy(n => n.STT).ToList();
        }
        public virtual DataTable GetListCongNo(int _year, string _txt, int _iBegin, int _iEnd)
        {
            string lenh = String.Format("Select * From(Select * , ROW_NUMBER() OVER (ORDER BY ID desc) AS RowNum From CONG_NO Where NAM = {0} and (TEN_KH like N'{1}' or MST like N'{2}' or '' = N'{3}') ) as T Where T.RowNum BETWEEN {4} AND {5} Order By STT desc", _year, _txt, _txt, _txt, _iBegin, _iEnd);
            DataTable dt = new DataTable();
            dt = XLDLRepo.ReadData(lenh);
            return dt;
        }
        public virtual List<CONG_NO> GetListByYearNV(int _year, int _idUser,string _field, string _txt, string _status)
        {
            return this.db.CONG_NOs.Where(n =>
                            (n.NV_KD == _idUser || n.NV_GN == _idUser || n.NV_KT == _idUser)
                            && n.NAM == _year && (n.TINH_TRANG == getTinhTrang(_status) || "" == _status)
                            && (_field == "1" ? (n.TEN_KH.Contains(_txt) || "" == _txt) : 
                            _field == "2" ? (n.MST.Contains(_txt) || "" == _txt) :
                            _field == "3" ? (n.QL_THUE_DIST == Utils.CIntDef(_txt, 0) || n.QL_THUE_CITY == Utils.CIntDef(_txt, 0) || "" == _txt) : 
                            _field == "4" ? (n.DIA_CHI.Contains(_txt) || "" == _txt) : 
                            _field == "5" ? (n.DIEN_THOAI.Contains(_txt) || "" == _txt) :
                            _field == "6" ? (n.NV_KT == Utils.CIntDef(_txt, 0) || "" == _txt) :
                            _field == "7" ? (n.NV_KD == Utils.CIntDef(_txt, 0) || "" == _txt) :
                            _field == "8" ? (n.NV_GN == Utils.CIntDef(_txt, 0) || "" == _txt) :
                            _field == "9" ? ((n.PHI == null || n.PHI == 0) && (n.BIEUPHI1_SL == null || n.BIEUPHI1_SL == "")) :
                            _field == "10" ? ((n.NV_KT == null || n.NV_KT == 0)) :
                            _field == "11" ? ((n.NV_KD == null || n.NV_KD == 0)) :
                            _field == "12" ? ((n.NV_GN == null || n.NV_GN == 0)) :                            
                            "" == _txt)
            ).OrderByDescending(n => n.STT).ToList();
        }
        public virtual CONG_NO GetById(int id)
        {
            try
            {
                return this.db.CONG_NOs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual CONG_NO GetByMST(string mst)
        {
            try
            {
                return this.db.CONG_NOs.Where(u => u.MST == mst).OrderByDescending(n => n.NAM).First();
            }
            catch
            {
                return null;
            }
        }
        public virtual CONG_NO GetByMSTYear(string mst, int year)
        {
            try
            {
                var obj = db.CONG_NOs.Single(u => u.MST == mst && u.NAM == year);
                return obj;
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CONG_NO> GetListMSTYear(string mst, int year)
        {
            try
            {
                var obj = db.CONG_NOs.Where(u => (u.MST == mst && u.MST != "") && u.NAM == year).ToList();
                if (obj.Count > 0)
                {
                    return obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(CONG_NO cus)
        {
            try
            {
                this.db.CONG_NOs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(CONG_NO cus)
        {
            try
            {
                CONG_NO cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(CONG_NO b)
        {
            try
            {
                db.CONG_NOs.DeleteOnSubmit(b);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(int id)
        {
            try
            {
                CONG_NO b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual bool CheckByMST(string _masothue)
        {
            try
            {
                var _obj = db.CONG_NOs.Where(n => n.MST.Contains(_masothue)).Single();
                if (_obj != null)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception) { return false; }
        }
        public virtual bool CheckUpdateByMST(string _masothue, string _tencty, string _diachi, string _sdt, string _email)
        {
            try
            {
                var _obj = db.CONG_NOs.Where(n => n.MST.Contains(_masothue)).Single();
                if (_obj != null)
                {
                    _obj.TEN_KH = _tencty;
                    _obj.DIA_CHI = _diachi;
                    _obj.DIEN_THOAI = _sdt;
                    _obj.EMAIL = _email;
                    db.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception) { return false; }
        }
        public virtual int GetIdKeToan(string _masothue)
        {
            try
            {
                int temp = 0;
                var _obj = db.CONG_NOs.Where(n => n.MST.Contains(_masothue)).Single();
                if (_obj != null)
                {
                    temp = Utils.CIntDef(_obj.NV_KT, 0);
                }
                return temp;
            }
            catch (Exception) { return 0; }
        }
        public virtual int GetSTT(int year)
        {
            try
            {
                var obj = db.CONG_NOs.Where(u => u.NAM == year).Max(u => u.STT);
                if (obj.Value > 0)
                    return obj.Value;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }
        private string getTinhTrang(string sTinhTrang)
        {
            string str = "";
            switch (sTinhTrang)
            {
                case "0": str = "0"; break;
                case "1": str = "---"; break;
                case "2": str = "Tạm ngưng hoạt động"; break;
                case "3": str = "Giải thể"; break;
                case "4": str = "Ngừng dịch vụ"; break;
                case "5": str = "Không thu phí"; break;
            }
            return str;
        }

        public int SyncNhanVienGiaoNhan(int _idUser,int _idProp, int _type, int _nam)
        {
            try
            {
                var obj = db.CONG_NOs.Where(u => (_type == 1 ? u.QL_THUE_DIST == _idProp : u.QL_THUE_CITY == _idProp)
                    && u.NAM == _nam).ToList();
                for (int i = 0; i < obj.Count; i++)
                {
                    obj[i].NV_GN = _idUser;
                    db.SubmitChanges();
                }
                return obj.Count;
            }
            catch
            {
                return 0;
            }
        }

        public virtual List<CONG_NO> ListDanhThu(int _type, int _idNV, int _year)
        {
            var list = db.CONG_NOs.Where(n => n.NAM == _year &&
                _type == 3 ? n.NV_KD == _idNV : n.NV_KT == _idNV).ToList();
            return list;
        }
    }
}