<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MichHomepage.aspx.cs" Inherits="_190448J_V2AppSecAssignment.MichHomepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> Welcome to SITConnect! </h1>
            <p> 
                <asp:Label ID="Lbl_Message" runat="server" EnableViewState="false"></asp:Label>
            </p>
            <p> 
                <asp:Button ID="Btn_Logout" runat="server" Text="Logout" OnClick="LogoutAccount" Visible="false" />
            </p>
        </div>
    </form>
</body>
</html>
