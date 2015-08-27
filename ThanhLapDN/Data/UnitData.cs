using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhLapDN;
using vpro.functions;
using System.Data;

namespace ThanhLapDN.Data
{
    public class UnitData
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual string Namestatus(object status, object type)
        {
            //Lấy giai đoạn tiếp theo
            int id = Utils.CIntDef(status);
            int _type = Utils.CIntDef(type);
            string str = "";
            if (_type == 1)
            {
                switch (id)
                {
                    case 1: str = "Giai đoạn 2: Soạn HS"; break;
                    case 2: str = "Giai đoạn 3: Phân giao nhận"; break;
                    case 3: str = "Giai đoạn 4: Ký nhận HS"; break;
                    case 4: str = "Giai đoạn 5: Kiểm tra HS"; break;
                    case 5: str = "Giai đoạn 6: Giao hồ sơ"; break;
                    case 6: str = "Giai đoạn 7: Nộp HS lên sở"; break;
                    case 7: str = "Giai đoạn 8: Chờ nhận giấy phép"; break;
                    case 8: str = "Đã cấp giấy phép"; break;
                    case 9: str = "Đã cấp giấy phép (chờ khắc dấu)"; break;
                }
            }
            else if (_type == 2)
            {
                switch (id)
                {
                    case 1: str = "Giai đoạn 2: Soạn HS"; break;
                    case 2: str = "Giai đoạn 3: Phân giao nhận"; break;
                    case 3: str = "Giai đoạn 4: Ký nhận HS"; break;
                    case 4: str = "Giai đoạn 5: Kiểm tra HS"; break;
                    case 5: str = "Giai đoạn 6: Nộp HS lên sở"; break;
                    case 6: str = "Giai đoạn 7: Chờ nhận HS"; break;
                    case 7: str = "Đã hoàn thành</b>"; break;
                }
            }
            else
            {
                switch (id)
                {
                    case 1: str = "Giai đoạn 2: Soạn HS"; break;
                    case 2: str = "Giai đoạn 3: Phân giao nhận"; break;
                    case 3: str = "Giai đoạn 4: Ký nhận HS"; break;
                    case 4: str = "Giai đoạn 5: Nộp HS thuế"; break;
                    case 5: str = "Giai đoạn 6: Chờ nhận kết quả"; break;
                    case 6: str = "Đã nhận kết quả"; break;
                }
            }
            return str;
        }

        public virtual string Getstatus(object status, object type)
        {
            int id = Utils.CIntDef(status);
            int _type = Utils.CIntDef(type);
            string str = "";
            if (_type == 1)
            {
                switch (id)
                {
                    case 1: str = "<b style='color:#FF0000;'>Giai đoạn 1: Tiếp nhận HS</b>"; break;
                    case 2: str = "<b style='color:#0099FF;'>Giai đoạn 2: Soạn HS</b>"; break;
                    case 3: str = "<b style='color:#0099FF;'>Giai đoạn 3: Phân giao nhận</b>"; break;
                    case 4: str = "<b style='color:#0099FF;'>Giai đoạn 4: Ký nhận HS</b>"; break;
                    case 5: str = "<b style='color:#0099FF;'>Giai đoạn 5: Kiểm tra HS</b>"; break;
                    case 6: str = "<b style='color:#0099FF;'>Giai đoạn 6: Giao hồ sơ</b>"; break;
                    case 7: str = "<b style='color:#0099FF;'>Giai đoạn 7: Nộp HS lên sở</b>"; break;
                    case 8: str = "<b style='color:#0099FF;'>Giai đoạn 8: Chờ nhận giấy phép</b>"; break;
                    case 9: str = "<b style='color:#0000FF;'>Đã cấp giấy phép</b>"; break;
                    case 10: str = "<b style='color:#0000FF;'>Đã cấp giấy phép (chờ khắc dấu)</b>"; break;
                    case 11: str = "<b style='color:#006600;'>Đã nhận dấu</b>"; break;
                    case 12: str = "<b style='color:#009933;'>Đã hoàn thành</b>"; break;
                }
            }
            else if (_type == 2)
            {
                switch (id)
                {
                    case 1: str = "<b style='color:#FF0000;'>Giai đoạn 1: Tiếp nhận HS</b>"; break;
                    case 2: str = "<b style='color:#0099FF;'>Giai đoạn 2: Soạn HS</b>"; break;
                    case 3: str = "<b style='color:#0099FF;'>Giai đoạn 3: Phân giao nhận</b>"; break;
                    case 4: str = "<b style='color:#0099FF;'>Giai đoạn 4: Ký nhận HS</b>"; break;
                    case 5: str = "<b style='color:#0099FF;'>Giai đoạn 5: Kiểm tra HS</b>"; break;
                    case 6: str = "<b style='color:#0099FF;'>Giai đoạn 6: Giao hồ sơ</b>"; break;
                    case 7: str = "<b style='color:#0099FF;'>Giai đoạn 7: Nộp HS lên sở</b>"; break;
                    case 8: str = "<b style='color:#0099FF;'>Giai đoạn 8: Chờ nhận giấy phép</b>"; break;
                    case 9: str = "<b style='color:#0000FF;'>Đã hoàn thành</b>"; break;
                    case 10: str = "<b style='color:#0000FF;'>Đã cấp giấy phép (chờ khắc dấu)</b>"; break;
                    case 11: str = "<b style='color:#006600;'>Đã nhận dấu</b>"; break;
                    case 12: str = "<b style='color:#009933;'>Đã hoàn thành</b>"; break;
                }
            }
            else
            {
                switch (id)
                {
                    case 1: str = "<b style='color:#0099FF;'>Giai đoạn 1: Tiếp nhận hồ sơ</b>"; break;
                    case 2: str = "<b style='color:#0099FF;'>Giai đoạn 2: Soạn hồ sơ</b>"; break;
                    case 3: str = "<b style='color:#0099FF;'>Giai đoạn 3: Phân giao nhận</b>"; break;
                    case 4: str = "<b style='color:#0099FF;'>Giai đoạn 4: Ký nhận và nộp hồ sơ thuế</b>"; break;
                    case 5: str = "<b style='color:#0099FF;'>Giai đoạn 5: Chờ nhận kết quả</b>"; break;
                    case 6: str = "<b style='color:#0000FF;'>Đã nhận kết quả</b>"; break;
                    case 12: str = "<b style='color:#009933;'>Đã hoàn thành</b>"; break;
                }
            }
            return str;
        }

        public virtual int CheckGroup(object status, object type)
        {
            int id = Utils.CIntDef(status);
            int _type = Utils.CIntDef(type);
            int i = 0;
            if (_type == 3)
            {
                switch (id)
                {
                    case 1: i = 11; break;
                    case 3: i = 7; break;
                    case 4: i = 6; break;
                    case 5: i = 11; break;
                    case 6: i = 11; break;
                }
            }
            else
            {
                switch (id)
                {
                    case 1: i = 4; break;
                    case 2: i = 5; break;
                    case 3: i = 7; break;
                    case 4: i = 6; break;
                    case 5: i = 7; break;
                    case 6: i = 6; break;
                    case 7: i = 8; break;
                    case 8: i = 4; break;
                    case 9: i = 4; break;
                    case 10: i = 4; break;
                    case 11: i = 4; break;
                }
            }
            return i;
        }

        public virtual List<WORKFLOW_USER> listWorkflow(object id)
        {
            int _id = Utils.CIntDef(id);
            var list = db.WORKFLOW_USERs.Where(n => n.PROF_ID == _id).OrderByDescending(n => n.ID).OrderByDescending(n => n.DATE).ToList();
            return list;
        }
        public virtual List<WORKFLOW_USER> listWorkflowByStatus(object id, int status)
        {
            int _id = Utils.CIntDef(id);
            var list = db.WORKFLOW_USERs.Where(n => n.PROF_ID == _id 
                && n.WORK_STATUS == status).OrderByDescending(n => n.ID).OrderByDescending(n => n.DATE).ToList();
            return list;
        }

        public virtual List<PROFILE_ATTACH> listFile(object id)
        {
            int _id = Utils.CIntDef(id);
            var list = db.PROFILE_ATTACHes.Where(n => n.PROF_ID == _id).OrderByDescending(n => n.ID).ToList();
            return list;
        }

        public virtual void TransformTableWithSpace(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
        {
            if (parentRow == null)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (!row.HasErrors && (Utils.CIntDef(row["TYPE_PARENT"]) <= 0))
                    {
                        row["TYPE_NAME"] = (Utils.CIntDef(row["TYPE_RANK"]) <= 1 ? "" : Duplicate("---", Utils.CIntDef(row["TYPE_RANK"]))) + row["TYPE_NAME"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["TYPE_NAME"]) != "------- Root -------")
                            TransformTableWithSpace(ref source, dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["TYPE_NAME"] = (Utils.CIntDef(parentRow["TYPE_RANK"]) <= 1 ? "" : Duplicate("---", Utils.CIntDef(parentRow["TYPE_RANK"]))) + parentRow["TYPE_NAME"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["TYPE_NAME"] = (Utils.CIntDef(child["TYPE_RANK"]) <= 1 ? "" : Duplicate("---", Utils.CIntDef(child["TYPE_RANK"]))) + child["TYPE_NAME"];
                            dest.Rows.Add(child.ItemArray);
                            child.RowError = "dirty";
                            TransformTableWithSpace(ref source, dest, rel, child);
                        }
                    }
                }
            }
        }
        public static string Duplicate(string partToDuplicate, int howManyTimes)
        {
            string result = "";

            for (int i = 0; i < howManyTimes; i++)
                result += partToDuplicate;

            return result;
        }

        public List<AREA_PROPERTy> Loadcity()
        {
            try
            {
                var list = db.AREA_PROPERTies.Where(n => n.PROP_RANK == 2).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<AREA_PROPERTy> Loaddistric(int idpro)
        {
            try
            {
                var list = db.AREA_PROPERTies.Where(n => n.PROP_RANK == 3 && n.PROP_PARENT_ID == idpro).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Get_PropertyName(int idpro)
        {
            string str = "";
            var obj = db.AREA_PROPERTies.Where(n => n.PROP_ID == idpro).ToList();
            if (obj.Count > 0)
            {
                str = obj[0].PROP_NAME;
            }
            return str;
        }

        public List<GROUP> GetListGroupPer()
        {
            try
            {
                var list = db.GROUPs.Where(n => n.GROUP_TYPE != 1 && n.GROUP_TYPE != 2).OrderByDescending(n => n.GROUP_ID).ToList();
                return list;
            }
            catch (Exception) { throw; }
        }
        public List<USER> GetListUserPer()
        {
            try
            {
                var list = db.USERs.Where(n => n.GROUP_ID != 1 && n.GROUP_ID != 2).OrderByDescending(n => n.USER_ID).ToList();
                return list;
            }
            catch (Exception) { throw; }
        }
        public int GetIdGroupByUser(int _idUser)
        {
            int i = 0;
            var obj = db.USERs.Where(n => n.USER_ID == _idUser).Single();
            if (obj != null)
            {
                i = Utils.CIntDef(obj.GROUP_ID);
            }
            return i;
        }
        public string GetNameGroupByUser(int _idGroup)
        {
            string str = "";
            var obj = db.GROUPs.Where(n => n.GROUP_ID == _idGroup).Single();
            if (obj != null)
            {
                str = Utils.CStrDef(obj.GROUP_NAME);
            }
            return str;
        }
    }
}