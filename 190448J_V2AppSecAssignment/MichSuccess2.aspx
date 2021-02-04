<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MichSuccess2.aspx.cs" Inherits="_190448J_V2AppSecAssignment.MichSuccess2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 156px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> Welcome to SIT Connect! </h1>
            <h3> User Profile </h3>
            
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td>
                            <asp:Label ID="Lbl_Message" runat="server" EnableViewState="false"></asp:Label>

                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Email:</td>
                        <td>
                            <asp:Label ID="Lbl_Email" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Credit Card Info:</td>
                        <td>
                            <asp:Label ID="Lbl_CreditCard" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="Btn_Logout" runat="server" Text="Logout" OnClick="LogoutAccount" />
            


        </div>
    </form>
</body>
</html>
