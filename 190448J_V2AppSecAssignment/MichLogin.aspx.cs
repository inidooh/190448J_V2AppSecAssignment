// Name: Michelle 
// Admin No: 190448J

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Net;

namespace _190448J_V2AppSecAssignment
{
    public partial class MichLogin : System.Web.UI.Page
    {
        string ASMichDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASMichDBConnection"].ConnectionString;

        // Assignment - Captcha
        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessge { get; set; }
        }

        public bool ValidateCaptcha()
        {
            bool result = true;
            // When user submits the recaptcha form, the user gets a response POST parameter
            // captchaResponse consist of the user click patterns. Behvaiour Analystics involved
            string captchaResponse = Request.Form["g-recaptcha-response"];

            // To send a GET response to Google along with the response and secret key
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LfRy0caAAAAALVz5VheKF0a3YPjTDpAEvxuHZsy &response=" + captchaResponse);

            try
            {
                // Codes to receive response in JSON format from google server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        // Response in JSON format
                        string jsonResponse = readStream.ReadToEnd();

                        // to show the JSON response string for learning purpose
                        Lbl_GScore.Text = jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        // Create jsonObject to handle the response (Ex. Success / Error)
                        // Deserialize Json
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        // Convert the string "False" to bool false or "True" to bool true
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginAccount(object sender, EventArgs e)
        {
            if (ValidateCaptcha())
            {
                // Login Account
                string passLogin = Tb_Password.Text.ToString().Trim();
                string emailLogin = Tb_Email.Text.ToString().Trim();

                SHA512Managed hashing = new SHA512Managed();
                string dbHash = getDBHash(emailLogin);
                string dbSalt = getDBSalt(emailLogin);

                try
                {
                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string passWithSalt = passLogin + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);

                        if (userHash.Equals(dbHash))
                        {
                            Session["LoggedIn"] = Tb_Email.Text.Trim();
                            Session["emailLogin"] = emailLogin;

                            // Assignment - Avoid Session Fixation Attack
                            // create a new GUID (unique value that is impoosible to guess) & save as new sesion variable AuthToken
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            // now create a new cookie with the GUID value
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                            Response.Redirect("MichSuccess2.aspx", false);


                        }
                        else
                        {
                            Lbl_ErrorMessage.Text = "Email or password is not valid";
                            Lbl_ErrorMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { }
            }
        }

        protected string getDBHash(string emailLogin)
        {
            string h = null;

            SqlConnection connection = new SqlConnection(ASMichDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@EMAILLOGIN";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAILLOGIN", emailLogin);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h; 
        }
        protected string getDBSalt(string emailLogin)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(ASMichDBConnectionString);
            string sql = "select PASSWORDSALT FROM Account WHERE Email=@EMAILLOGIN";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAILLOGIN", emailLogin);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;

        }

    }
}