<%@ Page Language="VB" AutoEventWireup="false"
    CodeBehind="HomeDefault.aspx.vb"
    Inherits="SistemaPrestamoHerramienta.HomeDefault"
    MasterPageFile="~/Site.Master"
    Title="Inicio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4 text-center">

       <img src="~/Images/LogoHerramienta.jpg" runat="server" style="width:160px; height:auto;" />

        <h1 class="display-5 fw-bold mt-3">Sistema de Préstamo de Herramientas</h1>
        <p class="text-muted">Bienvenido</p>

        <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión"
            CssClass="btn btn-primary btn-lg mt-3"
            OnClick="btnLogin_Click" />


    </div>

 

</asp:Content>
