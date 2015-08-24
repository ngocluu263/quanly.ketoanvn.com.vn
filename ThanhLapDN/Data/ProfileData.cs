using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhLapDN.Complete;
using System.Text.RegularExpressions;

namespace ThanhLapDN.Data
{
    public class ProfileData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<PROFILE_NEW> GetListByName(string name)
        {
            return this.db.PROFILE_NEWs.Where(n => (n.PROF_NAME.Contains(name) || name == "")).OrderBy(n => n.PROF_NAME).ToList();
        }
        public virtual PROFILE_NEW GetById(int id)
        {
            try
            {
                return this.db.PROFILE_NEWs.Single(u => u.ID == id);
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
                var obj = this.db.PROFILE_NEWs.Single(u => u.ID == id);
                if (obj != null)
                {
                    s = obj.PROF_NAME;
                }
                return s;
            }
            catch
            {
                return "";
            }
        }
        public virtual void Create(PROFILE_NEW cus)
        {
            try
            {
                this.db.PROFILE_NEWs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(PROFILE_NEW cus)
        {
            try
            {
                PROFILE_NEW cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void InsertDuplicate(PROFILE_NEW cus, int type, int status, int id_prof, int have_services)
        {
            try
            {
                db.PROFILE_NEWs.InsertOnSubmit(
                    new PROFILE_NEW {
                        PROF_TYPE = type, 
                        PROF_NAME = cus.PROF_NAME,
                        PROF_TRANSACTION = cus.PROF_TRANSACTION,
                        PROF_ATC = cus.PROF_ATC,
                        PROF_ADDRESS = cus.PROF_ADDRESS,
                        PROF_TAXCODE = cus.PROF_TAXCODE,
                        PROF_TOTAL_CAPITAL = cus.PROF_TOTAL_CAPITAL == null ? 0 : cus.PROF_TOTAL_CAPITAL,
                        PROF_CAPITAL = cus.PROF_CAPITAL == null ? 0 : cus.PROF_CAPITAL,
                        PROF_PHONE = cus.PROF_PHONE,
                        PROF_EMAIL = cus.PROF_EMAIL,
                        PROF_NOTE = cus.PROF_NOTE,
                        PROF_DATE = DateTime.Now,
                        PROF_ACTIVE = cus.PROF_ACTIVE,
                        PROF_STATUS = status,
                        PROF_LEVEL = cus.PROF_LEVEL,
                        PROF_COST1 = cus.PROF_COST1,
                        PROF_COST2 = cus.PROF_COST2,
                        PROF_DATE_COST = cus.PROF_DATE_COST,
                        PROF_REG1 = cus.PROF_REG1,
                        PROF_REG2 = cus.PROF_REG2,
                        PROF_FIELD1 = cus.PROF_FIELD1,
                        PROF_FIELD2 = cus.PROF_FIELD2,
                        PROF_FIELD3 = cus.PROF_FIELD3,
                        TRADES_ID = cus.TRADES_ID,
                        MEM_ID = cus.MEM_ID,
                        CAT_ID = cus.CAT_ID,
                        ATT_ID = cus.ATT_ID,
                        USER_ID = cus.USER_ID,
                        HAVE_SERVICES = have_services,
                        PROF_PARENT = id_prof
                    });
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }

        public virtual void Remove(PROFILE_NEW b)
        {
            try
            {
                db.PROFILE_NEWs.DeleteOnSubmit(b);
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
                PROFILE_NEW b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EntityComplete> searchComplete(string searchitem)
        {
            List<EntityComplete> l = new List<EntityComplete>();
            var list = (from a in db.PROFILE_NEWs
                        where a.PROF_NAME.Contains(searchitem)
                        select new
                        {
                            a.PROF_NAME,
                            a.PROF_DATE,
                            a.ID
                        }).Distinct().OrderByDescending(n => n.PROF_DATE).Take(999);
            foreach (var i in list)
            {
                EntityComplete enti = new EntityComplete();
                enti.title = i.PROF_NAME;
                l.Add(enti);
            }
            return l;
        }
        private string ClearUnicode(string SourceString)
        {

            SourceString = Regex.Replace(SourceString, "[ÂĂÀÁẠẢÃÂẦẤẬẨẪẰẮẶẲẴàáạảãâầấậẩẫăằắặẳẵ]", "a");
            SourceString = Regex.Replace(SourceString, "[ÈÉẸẺẼÊỀẾỆỂỄèéẹẻẽêềếệểễ]", "e");
            SourceString = Regex.Replace(SourceString, "[IÌÍỈĨỊìíịỉĩ]", "i");
            SourceString = Regex.Replace(SourceString, "[ÒÓỌỎÕÔỒỐỔỖỘƠỜỚỞỠỢòóọỏõôồốộổỗơờớợởỡ]", "o");
            SourceString = Regex.Replace(SourceString, "[ÙÚỦŨỤƯỪỨỬỮỰùúụủũưừứựửữ]", "u");
            SourceString = Regex.Replace(SourceString, "[ỲÝỶỸỴỳýỵỷỹ]", "y");
            SourceString = Regex.Replace(SourceString, "[đĐ]", "d");

            return SourceString;
        }
    }
}