using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;

namespace ThanhLapDN.Data
{
    public class getCookies
    {
        public virtual void getCookiesNew()
        {
            HttpContext.Current.Session["Userid"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_ID"]);
            HttpContext.Current.Session["Name"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_USER_NAME"]);
            HttpContext.Current.Session["Groupid"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_GROUP_ID"]);
            HttpContext.Current.Session["Grouptype"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_GROUP_TYPE"]);
            HttpContext.Current.Session["Groupname"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_INFO"]["PITM_GROUP_NAME"]);
        }
    }
}