using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;

namespace ThanhLapDN.Data
{
    public class ChamCongData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CHAM_CONG> GetListByMaCC(string name)
        {
            return this.db.CHAM_CONGs.Where(n => (n.CC_MACC.Contains(name) || name == "")).OrderBy(n => n.ID).ToList();
        }
        public virtual List<CHAM_CONG> GetListByYear(string name, int _month, int _year)
        {
            return this.db.CHAM_CONGs.Where(n => n.CC_NAM == _year
                && n.CC_THANG == _month
                && (n.CC_MACC.Contains(name) || name == "")).OrderBy(n => n.CC_MACC).ToList();
        }
        public virtual CHAM_CONG GetById(int id)
        {
            try
            {
                return this.db.CHAM_CONGs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual void Create(CHAM_CONG cus)
        {
            try
            {
                this.db.CHAM_CONGs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(CHAM_CONG cus)
        {
            try
            {
                CHAM_CONG cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(CHAM_CONG b)
        {
            try
            {
                db.CHAM_CONGs.DeleteOnSubmit(b);
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
                CHAM_CONG b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void RemoveByYearMonth(int _year, int _month, int _phongban)
        {
            try
            {
                var obj = db.CHAM_CONGs.Where(n => n.CC_NAM == _year && n.CC_THANG == _month
                    && (n.CC_PHONGBAN == _phongban || 0 == _phongban)).ToList();
                if (obj.Count > 0)
                {
                    db.CHAM_CONGs.DeleteAllOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
            catch (Exception) { throw; }
        }
        public virtual bool CheckExistsYearMonth(int _year, int _month, int _phongban)
        {
            try
            {
                var obj = db.CHAM_CONGs.Where(n => n.CC_NAM == _year && n.CC_THANG == _month 
                    && (n.CC_PHONGBAN == _phongban || _phongban == 0)).ToList();
                if (obj.Count > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }
        public virtual int GetSumMinuLate(string _name, int _month, int _year)
        {
            var dminu = this.db.CHAM_CONGs.Where(n => n.CC_NAM == _year
                           && n.CC_THANG == _month
                           && (n.CC_MACC.Contains(_name))).Sum(n => n.CC_SOPHUTTRE);
            return Utils.CIntDef(dminu, 0);
        }
        public virtual double GetSumDayoff(string _name, int _month, int _year)
        {
            double dbl = 0;
            var list = this.db.CHAM_CONGs.Where(n => n.CC_NAM == _year
                           && n.CC_THANG == _month
                           && (n.CC_MACC.Contains(_name))
                           && n.CC_SONGAYNGHI != 0).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                dbl += Utils.CDblDef(list[i].CC_SONGAYNGHI);
            }
            return dbl;
        }
    }
}