using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _190448J_V2AppSecAssignment
{
    public partial class MichDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Lbl_Display.Text = Request.QueryString["Email"];
        }
    }
}