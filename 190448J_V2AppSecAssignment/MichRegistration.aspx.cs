// Name: Michelle 
// Admin No: 190448J

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions; //for regular expression
using System.Drawing; //for change of color
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Net;
using System.Configuration;

namespace _190448J_V2AppSecAssignment
{
    public partial class MichRegistration : System.Web.UI.Page
    {
        // Assignment - Securing user data and passwords
        string ASMichDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASMichDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

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
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret='secretkey' &response=" + captchaResponse);

            try
            {
                // Codes to receive response in JSON format from google server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        // Response in JSON format
                        string jsonResponse = readStream.ReadToEnd();


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
        
        // Assignment - Set Strong Password (SERVER SIDE)
        private int checkPassword(string password)
        {
            int passScore = 0;

            // Score 0 
            // IF password length is less than 8 characters
            if (password.Length < 8)
            {
                return 1;
            }
            // Score 1 - Very Weak 
            // Minimum password length of 8 characters
            else
            {
                passScore = 1;
            }

            // Score 2 - Weak
            // Password contains lowercase letters
            if (Regex.IsMatch(password, "[a-z]"))
            {
                passScore++;
            }

            // Score 3 - Medium
            // Password contains uppercase letters
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                passScore++;
            }

            // Score 4 - Strong
            // Password contains numerals 
            if (Regex.IsMatch(password, "[0-9]"))
            {
                passScore++;
            }

            // Score 5 - Excellent
            // Password contains special characters
            if (Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                passScore++;
            }
            return passScore;
        }

        protected void Btn_Submit_Click(object sender, EventArgs e)
        {
            // Assignment - Proper input validation XSS
             //Response.Redirect("MichDisplay.aspx?Email=" + HttpUtility.HtmlEncode(Tb_Email.Text));

            // Assignment - Securing user data and passwords
            string passProtect = Tb_Password.Text.ToString().Trim(); //to get value from textbox

            // Generate random salt
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            // Fills array of bytes with a cryptographically strong sequence of random values
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string passWithSalt = passProtect + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(passProtect));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passWithSalt));

            finalHash = Convert.ToBase64String(hashWithSalt);

            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;

            createAccount();

            // Assignment - Set Strong Password (SERVER SIDE)
            // To extract data from the textbox
            int passScores = checkPassword(Tb_Password.Text);
            string passStatus = "";
            switch (passScores)
            {
                case 1:
                    passStatus = "Very Weak";
                    break;
                case 2:
                    passStatus = "Weak";
                    break;
                case 3:
                    passStatus = "Medium";
                    break;
                case 4:
                    passStatus = "Strong";
                    break;
                case 5:
                    passStatus = "Excellent";
                    break;
                default:
                    break;
            }
            Lbl_PasswordChecker.Text = "Password Status: " + passStatus;
            // Score needs to reach 4 for password criteria to be fulfilled
            if (passScores < 4)
            {
                Lbl_PasswordChecker.ForeColor = Color.Red;
                return;
            }
            Lbl_PasswordChecker.ForeColor = Color.Green;

            // Assignment - Captcha
            if (Tb_VerificationCode.Text.ToLower() == Session["CaptchaVerify"].ToString())
            {
                Response.Redirect("MichCaptchaSuccess.aspx");
            }
            else
            {
                Lbl_CaptchaMessage.Text = "Your Captcha input is wrong. Please correctly input the Captcha.";
                Lbl_CaptchaMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void createAccount()
        {
            // ASSIGNMENT - SQLi prevention attempt
            //string DBConnect = ConfigurationManager.ConnectionStrings["ASMichDBConnection"].ConnectionString;
            //SqlConnection myConn = new SqlConnection(DBConnect);

            //string sqlStmt = "INSERT INTO Account (FirstName, LastName, Birthdate, CreditCard, Email, Password, PasswordHash, PasswordSalt, EmailVerified, IV, Key)" +
            //                     "VALUES(@paraFirstName, @paraLastName, @paraBirthdate, @paraCreditCard, @paraEmail, @paraPassword, @paraPasswordHash, @paraPasswordSalt, @paraEmailVerified, @paraIV, @paraKey)";
            //SqlCommand cmd = new SqlCommand(sqlStmt, myConn);

            //cmd.Parameters.AddWithValue("@paraFirstName", Tb_FirstName.Text.Trim());
            //cmd.Parameters.AddWithValue("@paraLastName", Tb_LastName.Text.Trim());
            //cmd.Parameters.AddWithValue("@paraBirthdate", Tb_Birthdate.Text.Trim());
            //cmd.Parameters.AddWithValue("@paraCreditCard", Convert.ToBase64String(encryptData(Tb_CreditCard.Text.Trim())));
            //cmd.Parameters.AddWithValue("@paraEmail", Tb_Email.Text.Trim());
            //cmd.Parameters.AddWithValue("@paraPassword", Tb_Password.Text.Trim());
            //cmd.Parameters.AddWithValue("@paraPasswordHash", finalHash);
            //cmd.Parameters.AddWithValue("@paraPasswordSalt", salt);
            //cmd.Parameters.AddWithValue("@paraEmailVerified", DBNull.Value);
            //cmd.Parameters.AddWithValue("@paraIV", Convert.ToBase64String(IV));
            //cmd.Parameters.AddWithValue("@paraKey", Convert.ToBase64String(Key));

            //cmd.Connection = myConn;
            //myConn.Open();
            //cmd.ExecuteNonQuery();
            //myConn.Close();
            try
            {
                using (SqlConnection con = new SqlConnection(ASMichDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName, @LastName, @Birthdate, @CreditCard, @Email, @Password, @PasswordHash, @PasswordSalt, @EmailVerified, @IV, @Key)"))

                    //string sqlStmt = "INSERT INTO Account (FirstName, LastName, Birthdate, CreditCard, Email, Password, PasswordHash, PasswordSalt, EmailVerified, IV, Key)" +
                    //                     "VALUES(@paraFirstName, @paraLastName, @paraBirthdate, @paraCreditCard, @paraEmail, @paraPassword, @paraPasswordHash, @paraPasswordSalt, @paraEmailVerified, @paraIV, @paraKey)";
                    //SqlCommand cmd = newSqlCommand(sqlStmt, myConn);
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", Tb_FirstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", Tb_LastName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Birthdate", Tb_Birthdate.Text.Trim());
                            //cmd.Parameters.AddWithValue("@CreditCard", encryptData(Tb_CreditCard.Text.Trim())); // Credit card encryption
                            cmd.Parameters.AddWithValue("@CreditCard", Convert.ToBase64String(encryptData(Tb_CreditCard.Text.Trim())));
                            cmd.Parameters.AddWithValue("@Email", Tb_Email.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", Tb_Password.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@EmailVerified", DBNull.Value);
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));


                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

        }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }


    }
}