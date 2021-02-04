using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _190448J_V2AppSecAssignment
{
    public partial class MichHomepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Assignment - Avoid Session Fixation Attack
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("MichLogin.aspx", false);
                }
                else
                {
                    Lbl_Message.Text = "Congratulations! You are logged in!";
                    Lbl_Message.ForeColor = System.Drawing.Color.Green;
                    Btn_Logout.Visible = true;
                }
            }
            else
            {
                Response.Redirect("MichLogin.aspx", false);
            }
        }

        protected void LogoutAccount(object sender, EventArgs e)
        {
            // Assignment - Avoid Session Fixation Attack (Check by running Login and Inspect Cookies)
            Session.Clear(); 
            Session.Abandon(); 
            Session.RemoveAll();

            Response.Redirect("MichLogin.aspx", false);

            // To ensure that the cookie is removed from browser and expire the AuthToken cookie
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }

    }
}