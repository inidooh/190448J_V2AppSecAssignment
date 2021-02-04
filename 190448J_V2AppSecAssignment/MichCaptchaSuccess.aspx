<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MichCaptchaSuccess.aspx.cs" Inherits="_190448J_V2AppSecAssignment.MichSuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <h1> Captcha Verified! </h1>
            <p> Your account was created successfully! </p>
            <p> Login your account! </p>
        </div>
        <asp:Button ID="Btn_Login" runat="server" Text="Login" OnClick="Btn_Login_Click" /> 

        <p> Or would you like to create another account? </p>
        <asp:Button ID="Btn_Register" runat="server" Text="Create" OnClick="Btn_Register_Click" />
    </form>
</body>
</html>
