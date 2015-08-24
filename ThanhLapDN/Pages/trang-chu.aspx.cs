using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace ThanhLapDN.Pages
{
    public partial class trang_chu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int type = Utils.CIntDef(Session["Grouptype"]);
            UserControl UCmain = Page.LoadControl("../UIs/homeMain.ascx") as UserControl;
            PlaceHolderMain.Controls.Add(UCmain);
        }
        
    }
}