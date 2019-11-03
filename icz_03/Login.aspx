<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="icz_03.Login" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<%--        <h2 style="color: Green">
        Login using Xml file in ASP.NET 4, C#</h2>--%>

        <asp:Login ID="Login1" runat="server" BackColor="#00CCFF" BorderColor="#FF9966" BorderStyle="Solid"
        BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" Height="225px" OnAuthenticate="Login1_Authenticate"
        Width="417px" FailureText="Nesprávne prihlasovacie údaje!" LoginButtonText="Prihlásenie" PasswordLabelText="Prihlasovacie heslo:" RememberMeText="Zapamätaj prihlasovacie údaje." TitleText="Autentizácia" UserNameLabelText="Prihlasovacie meno:">
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:Login>

    </div>
    </form>
</body>
</html>

