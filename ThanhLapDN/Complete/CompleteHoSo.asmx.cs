using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ThanhLapDN.Data;

namespace ThanhLapDN.Complete
{
    /// <summary>
    /// Summary description for CompleteAjax
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CompleteHoSo : System.Web.Services.WebService
    {
        ProfileData search = new ProfileData();
        [WebMethod]
        public List<EntityComplete> autocomplete(string searchitem)
        {
            return search.searchComplete(searchitem);
        }
    }

    public class EntityComplete
    {
        public string title { get; set; }
    }
}
