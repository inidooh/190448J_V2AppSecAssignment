<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MichRegistration.aspx.cs" Inherits="_190448J_V2AppSecAssignment.MichRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form </title>
            <script src="https://www.google.com/recaptcha/api.js?render=6LfRy0caAAAAABOH5Vwuj4toh6vNF3NskOhKLIr2"></script>

    <script type="text/javascript">

        function validatePassword() {
            var str = document.getElementById('<%=Tb_Password.ClientID%>').value;

            // Assignment - Set Strong Password (CLIENT SIDE)
            // to check if password is less than 8 characters
            if (str.length < 8) {
                document.getElementById("Lbl_PasswordChecker").innerHTML = "The password should have at least 8 characters";
                document.getElementById("Lbl_PasswordChecker").style.color = "Red";
                return ("too_short")
            }
            // to check if password has a number
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("Lbl_PasswordChecker").innerHTML = "The password should have at least 1 number";
                document.getElementById("Lbl_PasswordChecker").style.color = "Red";
                return ("no_number");
            }
            //to check if password has an uppercase letter (A-Z)
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("Lbl_PasswordChecker").innerHTML = "The password should have at least 1 uppercase letter";
                document.getElementById("Lbl_PasswordChecker").style.color = "Red";
                return ("no_upper_case");
            }
            //to check if password has a lowercase letter (a-z)
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("Lbl_PasswordChecker").innerHTML = "The password should have at least 1 lowercase letter";
                document.getElementById("Lbl_PasswordChecker").style.color = "Red";
                return ("no_lower_case");
            }
            // to check if password has special character by excluding all the alphabets and numbers
            else if (str.search(/[^a-zA-Z0-9]/) == -1) {
                document.getElementById("Lbl_PasswordChecker").innerHTML = "The password should have at least 1 special character";
                document.getElementById("Lbl_PasswordChecker").style.color = "Red";
                return ("no_special_character");
            }


            // if password meets the password complexity criteria
            document.getElementById("Lbl_PasswordChecker").innerHTML = "Excellent! The password is suitable!";
            document.getElementById("Lbl_PasswordChecker").style.color = "Blue";


        }

    </script>


    <style type="text/css">
        .auto-style1 {
            width: 153px;
            height: 48px;
        }

        .auto-style2 {
            width: 153px;
            height: 30px;
        }

        .auto-style3 {
            height: 30px;
        }

        .auto-style4 {
            width: 153px;
            height: 31px;
        }

        .auto-style5 {
            height: 31px;
        }

        .auto-style6 {
            height: 48px;
        }
        .auto-style7 {
            width: 153px;
            height: 38px;
        }
        .auto-style8 {
            height: 38px;
        }
        .auto-style9 {
            width: 153px;
            height: 36px;
        }
        .auto-style10 {
            height: 36px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Registration Form</h1>
            
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style2">First Name:</td>
                        <td class="auto-style3">
                            <asp:TextBox ID="Tb_FirstName" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td class="auto-style3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Last Name: </td>
                        <td class="auto-style5">
                            <asp:TextBox ID="Tb_LastName" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td class="auto-style5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Date Of Birth:</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="Tb_Birthdate" runat="server" Width="160px" TextMode="Date"></asp:TextBox>
                        </td>
                        <td class="auto-style5"></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Credit Card Info:</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="Tb_CreditCard" runat="server" Width="160px" MaxLength="16"></asp:TextBox>
                        </td>
                        <td class="auto-style5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Email Address:</td>
                        <td class="auto-style3">
                            <asp:TextBox ID="Tb_Email" runat="server" Width="160px" TextMode="Email"></asp:TextBox>
                        </td>
                        <td class="auto-style3"></td>
                    </tr>
                    <tr>
                        <td class="auto-style9">Password</td>
                        <td class="auto-style10">
                            <asp:TextBox ID="Tb_Password" runat="server" Width="160px"  TextMode="Password" onkeyup="javascript: validatePassword()"></asp:TextBox>
                            &nbsp;<asp:Label ID="Lbl_PasswordChecker" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style10"></td>
                    </tr>
                    <tr>
                        <td class="auto-style9">&nbsp;</td>
                        <td class="auto-style10">
                            <asp:Label ID="Lbl_ErrorMessage" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td class="auto-style10">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style7">Verification Code</td>
                        <td class="auto-style8">
                            <asp:Image ID="Image1" runat="server" Height="55px" Width="186px" ImageUrl="~/MichCaptcha.aspx"/>
                        </td>
                        <td class="auto-style8">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style8">
                            <asp:Label ID="Lbl_CaptchaMessage" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style8"></td>
                    </tr>
                    <tr>
                        <td class="auto-style7">Enter Verification Code:</td>
                        <td class="auto-style8">
                            <asp:TextBox ID="Tb_VerificationCode" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td class="auto-style8">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1"></td>
                        <td class="auto-style6">
                            <asp:Button ID="Btn_Submit" runat="server" Text="Submit" Width="141px" OnClick="Btn_Submit_Click" />
                        </td>
                        <td class="auto-style6"></td>
                    </tr>
                </table>
           


        </div>
    </form>
        <script>
            // Assignment - CAPTCHA
            grecaptcha.ready(function () {
                grecaptcha.execute('key', { action: 'Login' }).then(function (token) {
                    document.getElementById("g-recaptcha-response").value = token;
                });
            });
        </script>

</body>
</html>
