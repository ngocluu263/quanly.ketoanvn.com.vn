using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class CongNoWebRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();


        public virtual List<CONG_NO_WEB> GetListByYear(int _year, int _month, int _idUser, string _txt, int _tinhtrang, int _congno)
        {
            return this.db.CONG_NO_WEBs.Where(n =>
                            (n.NVKD == _idUser || n.NVXL == _idUser || _idUser == -1)
                            && n.NGAYKY_HOPDONG != null
                            && n.NGAYKY_HOPDONG.Value.Year == _year
                            && n.NGAYKY_HOPDONG.Value.Month == _month
                            && (n.TINHTRANG == _tinhtrang || _tinhtrang == 0)
                            && ((_congno == 1) ? n.CONGNO > 0 : n.CONGNO <= 0 || _congno == 0)
                            && (n.TEN_KHACHHANG.Contains(_txt) || n.THONGTINLIENHE_KHACHHANG.Contains(_txt) || "" == _txt)).OrderByDescending(n => n.ID).ToList();
        }
        public virtual CONG_NO_WEB GetById(int id)
        {
            try
            {
                return this.db.CONG_NO_WEBs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<CONG_NO_WEB> GetAll()
        {
            return this.db.CONG_NO_WEBs.ToList();
        }
        public virtual void Create(CONG_NO_WEB CONG_NO_WEB)
        {
            try
            {
                this.db.CONG_NO_WEBs.InsertOnSubmit(CONG_NO_WEB);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CONG_NO_WEB CONG_NO_WEB)
        {
            try
            {
                CONG_NO_WEB CONG_NO_WEBOld = this.GetById(CONG_NO_WEB.ID);
                CONG_NO_WEBOld = CONG_NO_WEB;
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
                CONG_NO_WEB CONG_NO_WEB = this.GetById(id);
                this.Remove(CONG_NO_WEB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CONG_NO_WEB CONG_NO_WEB)
        {
            try
            {
                db.CONG_NO_WEBs.DeleteOnSubmit(CONG_NO_WEB);
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
                CONG_NO_WEB CONG_NO_WEB = this.GetById(id);
                return this.Delete(CONG_NO_WEB);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CONG_NO_WEB CONG_NO_WEB)
        {
            try
            {
                //CONG_NO_WEB.IsDelete = true;
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