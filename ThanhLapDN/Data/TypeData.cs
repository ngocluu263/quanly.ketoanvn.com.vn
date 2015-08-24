using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.Data
{
    public class TypeData
    {
        public class News_List_Type
        {
            public int TYPE_ID { get; set; }
            public string TYPE_NAME { get; set; }
        }
        public List<News_List_Type> ListType()
        {
            List<News_List_Type> l = new List<News_List_Type>();
            int[] id = { 1, 2, 3, 4, 5 };
            string[] name = { "Đăng ký Tên miền", "Đăng ký Hosting", "Đăng ký Email Server", "Thiết kế layout", "Đăng ký Google Adwords" };
            for (int i = 0; i < 5; i++)
            {
                News_List_Type p = new News_List_Type();
                p.TYPE_ID = id[i];
                p.TYPE_NAME = name[i];
                l.Add(p);
            }
            return l;
        }

        public class News_List_Status
        {
            public int STATUS_ID { get; set; }
            public string STATUS_NAME { get; set; }
        }
        public List<News_List_Status> ListStatus()
        {
            List<News_List_Status> l = new List<News_List_Status>();
            int[] id = { 0, 1, 2};
            string[] name = { "Chưa duyệt", "Đã duyệt", "Đã bàn giao" };
            for (int i = 0; i < 3; i++)
            {
                News_List_Status p = new News_List_Status();
                p.STATUS_ID = id[i];
                p.STATUS_NAME = name[i];
                l.Add(p);
            }
            return l;
        }
    }
}