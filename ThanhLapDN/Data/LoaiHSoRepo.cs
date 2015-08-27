using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class LoaiHSoRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual LOAI_HSO GetByCode(string code)
        {
            try
            {
                return this.db.LOAI_HSOs.Single(u => u.CODE == code);
            }
            catch
            {
                return null;
            }
        }
        public virtual LOAI_HSO GetById(int id)
        {
            try
            {
                return this.db.LOAI_HSOs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<LOAI_HSO> GetAll()
        {
            return this.db.LOAI_HSOs.ToList();
        }
        public virtual void Create(LOAI_HSO LOAI_HSO)
        {
            try
            {
                this.db.LOAI_HSOs.InsertOnSubmit(LOAI_HSO);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(LOAI_HSO LOAI_HSO)
        {
            try
            {
                LOAI_HSO LOAI_HSOOld = this.GetById(LOAI_HSO.ID);
                LOAI_HSOOld = LOAI_HSO;
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
                LOAI_HSO LOAI_HSO = this.GetById(id);
                this.Remove(LOAI_HSO);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(LOAI_HSO LOAI_HSO)
        {
            try
            {
                db.LOAI_HSOs.DeleteOnSubmit(LOAI_HSO);
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
                LOAI_HSO LOAI_HSO = this.GetById(id);
                return this.Delete(LOAI_HSO);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(LOAI_HSO LOAI_HSO)
        {
            try
            {
                //LOAI_HSO.IsDelete = true;
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