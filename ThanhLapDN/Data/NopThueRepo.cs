using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class NopThueRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual NOP_THUE GetByMstAndNamAndThang(string mst, int nam, int thang)
        {
            try
            {
                return this.db.NOP_THUEs.Single(u => u.MST == mst && u.NAM == nam && u.THANG == thang);
            }
            catch
            {
                return null;
            }
        }
        public virtual NOP_THUE GetById(int id)
        {
            try
            {
                return this.db.NOP_THUEs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<NOP_THUE> GetAll()
        {
            return this.db.NOP_THUEs.ToList();
        }
        public virtual void Create(NOP_THUE NOP_THUE)
        {
            try
            {
                this.db.NOP_THUEs.InsertOnSubmit(NOP_THUE);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(NOP_THUE NOP_THUE)
        {
            try
            {
                NOP_THUE NOP_THUEOld = this.GetById(NOP_THUE.ID);
                NOP_THUEOld = NOP_THUE;
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
                NOP_THUE NOP_THUE = this.GetById(id);
                this.Remove(NOP_THUE);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(NOP_THUE NOP_THUE)
        {
            try
            {
                db.NOP_THUEs.DeleteOnSubmit(NOP_THUE);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(int id)
        {
            try
            {
                NOP_THUE NOP_THUE = this.GetById(id);
                return this.Delete(NOP_THUE);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(NOP_THUE NOP_THUE)
        {
            try
            {
                //NOP_THUE.IsDelete = true;
                db.SubmitChanges();
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}