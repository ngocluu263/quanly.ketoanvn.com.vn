using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class DeleteData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual bool DeleteMemberByProf(int _id_prof)
        {
            try
            {
                var _obj = db.PROFILE_MEMBERs.Where(n => n.PROF_ID == _id_prof).ToList();
                db.PROFILE_MEMBERs.DeleteAllOnSubmit(_obj);
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
        public virtual bool DeleteAttachByProf(int _id_prof)
        {
            try
            {
                var _obj = db.PROFILE_ATTACHes.Where(n => n.PROF_ID == _id_prof).ToList();
                db.PROFILE_ATTACHes.DeleteAllOnSubmit(_obj);
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
        public virtual bool DeleteWorkByProf(int _id_prof)
        {
            try
            {
                var _obj = db.WORKFLOW_USERs.Where(n => n.PROF_ID == _id_prof).ToList();
                db.WORKFLOW_USERs.DeleteAllOnSubmit(_obj);
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
        public virtual bool DeleteWorkStatusById(int _id_workstatus)
        {
            try
            {
                var _objTemp = db.WORKFLOW_STATUS.Single(n => n.ID == _id_workstatus);
                db.WORKFLOW_STATUS.DeleteOnSubmit(_objTemp);
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}