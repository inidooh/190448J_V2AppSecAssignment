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

namespace _190448J_V2AppSecAssignment
{
    public partial class MichSuccess2 : System.Web.UI.Page
    {
        string ASMichDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASMichDBConnection"].ConnectionString;
        byte[] Key;
        byte[] IV;
        byte[] creditCard = null;
        string emailLogin = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailLogin"] != null)
            {
                emailLogin = (string) Session["emailLogin"];

                displayUserProfile(emailLogin);
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
        protected string decryptData(byte[] cipherText)
        {
            string plainText = null;

            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decypted bytes from the decrypting stream & place in a string
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return plainText;
        }

        protected void displayUserProfile(string emailLogin)
        {
            SqlConnection connection = new SqlConnection(ASMichDBConnectionString);
            string sql = "select * FROM Account WHERE Email=@EMAILLOGIN";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAILLOGIN", emailLogin);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Email"] != DBNull.Value)
                        {
                            Lbl_Email.Text = reader["Email"].ToString();
                        }

                        if (reader["CreditCard"] != DBNull.Value)
                        {
                            // convert base64 in db to byte[]
                            creditCard = Convert.FromBase64String(reader["CreditCard"].ToString());
                        }

                        if (reader["IV"] != DBNull.Value)
                        {
                            IV = Convert.FromBase64String(reader["IV"].ToString());
                        }

                        if (reader["Key"] != DBNull.Value)
                        {
                            Key = Convert.FromBase64String(reader["Key"].ToString());
                        }
                    }
                    Lbl_CreditCard.Text = decryptData(creditCard);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}