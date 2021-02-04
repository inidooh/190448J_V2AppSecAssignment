<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MichLogin.aspx.cs" Inherits="_190448J_V2AppSecAssignment.MichLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Login Form </title>
  
        <script src="https://www.google.com/recaptcha/api.js?render=6LfRy0caAAAAABOH5Vwuj4toh6vNF3NskOhKLIr2"></script>

    <style type="text/css">
        .auto-style2 {
            width: 170px;
            height: 31px;
        }
        .auto-style3 {
            height: 31px;
        }
        .auto-style4 {
            width: 170px;
            height: 33px;
        }
        .auto-style5 {
            height: 33px;
        }
        .auto-style6 {
            width: 170px;
            height: 34px;
        }
        .auto-style7 {
            height: 34px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> Login Form </h1>

        </div>
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">Email Address:</td>
                <td class="auto-style3">
                            <asp:TextBox ID="Tb_Email" runat="server" Width="160px" TextMode="Email" ></asp:TextBox>
                        </td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style4">Password:</td>
                <td class="auto-style5">
                            <asp:TextBox ID="Tb_Password" runat="server" Width="160px" TextMode="Password"></asp:TextBox>
                        </td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style6"></td>
                <td class="auto-style7">
                            <asp:Button ID="Btn_Login" runat="server" Text="Login" Width="141px" OnClick="LoginAccount" />
                        </td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style7">
                            <asp:Label ID="Lbl_ErrorMessage" runat="server"></asp:Label>
                        </td>
                <td class="auto-style7">&nbsp;</td>
            </tr>
        </table>

        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response" />
        <asp:Label ID="Lbl_GScore" runat="server"></asp:Label>    
    </form>

    <script>
        // Assignment - CAPTCHA
        grecaptcha.ready(function () {
            grecaptcha.execute('6LfRy0caAAAAABOH5Vwuj4toh6vNF3NskOhKLIr2', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
