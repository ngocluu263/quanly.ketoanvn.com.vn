using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;

namespace ThanhLapDN.Data
{
    public class CongNoCKSData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CONG_NO_CK> GetListByName(string name)
        {
            return this.db.CONG_NO_CKs.Where(n => (n.TEN_CTY.Contains(name) || name == "")).OrderBy(n => n.TEN_CTY).ToList();
        }
        public virtual List<CONG_NO_CK> GetListByMult(string _year, string _month, string _txt)
        {
            return this.db.CONG_NO_CKs.Where(n => (n.NAM == _year)
                && n.THANG == _month
                && (n.TEN_CTY.Contains(_txt) || n.MST.Contains(_txt) || "" == _txt)).OrderByDescending(n => n.STT).ToList();
        }
        public virtual List<CONG_NO_CK> GetListByYear(string _year, string _month)
        {
            return this.db.CONG_NO_CKs.Where(n => (n.NAM == _year)
                && n.THANG == _month).OrderBy(n => n.STT).ToList();
        }
        public virtual CONG_NO_CK GetById(int id)
        {
            try
            {
                return this.db.CONG_NO_CKs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual string Get_NameCompany(int id)
        {
            try
            {
                string s = "";
                var obj = this.db.CONG_NO_CKs.Single(u => u.ID == id);
                if (obj != null)
                {
                    s = obj.TEN_CTY;
                }
                return s;
            }
            catch
            {
                return "";
            }
        }
        public virtual void Create(CONG_NO_CK cus)
        {
            try
            {
                this.db.CONG_NO_CKs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(CONG_NO_CK cus)
        {
            try
            {
                CONG_NO_CK cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(CONG_NO_CK b)
        {
            try
            {
                db.CONG_NO_CKs.DeleteOnSubmit(b);
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
                CONG_NO_CK b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual List<CONG_NO_CK> GetListByYearNV(string _year,string _month, int _idUser, string _txt)
        {
            return this.db.CONG_NO_CKs.Where(n =>
                            n.NV_KD == _idUser
                            && n.NAM == _year && n.THANG == _month
                            && (n.TEN_CTY.Contains(_txt) || n.MST.Contains(_txt) || "" == _txt)).OrderByDescending(n => n.STT).ToList();
        }
        public virtual List<CONG_NO_CK> GetListByYear(string _year, string _month, string _txt)
        {
            return this.db.CONG_NO_CKs.Where(n =>
                            n.NAM == _year && n.THANG == _month
                            && (n.TEN_CTY.Contains(_txt) || n.MST.Contains(_txt) || "" == _txt)).OrderByDescending(n => n.STT).ToList();
        }

        public virtual List<CONG_NO_CK> ListDoanhThu(int _type, int _idNV, int _year, int _month)
        {
            string _Cyear = Utils.CStrDef(_year);
            string _Cmonth = _month.ToString().Length == 1 ? Utils.CStrDef("0" + _month) : Utils.CStrDef(_month);
            var list = db.CONG_NO_CKs.Where(n => n.NAM == _Cyear && n.THANG == _Cmonth
                    && n.NV_KD == _idNV).ToList();
            return list;
        }
    }
}