using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace ThanhLapDN.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Data.getCookies _getCookies = new Data.getCookies();
                _getCookies.getCookiesNew();
                if (Session["Userid"] == null)
                {
                    Response.Redirect("/Pages/dang-nhap.aspx", false);
                }
            }
            catch (Exception) { Response.Redirect("/Pages/dang-nhap.aspx", false); }
        }
    }
}