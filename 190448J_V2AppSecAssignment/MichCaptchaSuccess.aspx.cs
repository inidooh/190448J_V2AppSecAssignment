using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _190448J_V2AppSecAssignment
{
    public partial class MichSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("MichLogin.aspx");
        }

        protected void Btn_Register_Click(object sender, EventArgs e)
        {
            Response.Redirect("MichRegistration.aspx");
        }
    }
}