<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="UserManagement.WebForms.About" %>

<asp:Content Id="BodyContent" ContentPlaceHolderId="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 Id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provIde additional information.</p>
    </main>
</asp:Content>
