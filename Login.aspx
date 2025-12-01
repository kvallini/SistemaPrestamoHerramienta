<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="SistemaPrestamoHerramienta.Login" 
    MasterPageFile="~/Site.Master" Title="Iniciar Sesión" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center align-items-center min-vh-50">
        <div class="col-md-5 col-lg-4">
            <div class="card shadow">
                <div class="card-header bg-primary text-white text-center py-3">
                    <h4 class="card-title mb-0">
                        <i class="fas fa-tools me-2"></i>Iniciar Sesión
                    </h4>
                </div>
                <div class="card-body p-4">
                    <!-- Mensajes -->
                    <asp:Panel ID="pnlMensaje" runat="server" CssClass="alert alert-danger" Visible="false">
                        <i class="fas fa-exclamation-triangle me-2"></i>
                        <asp:Literal ID="litMensaje" runat="server" />
                    </asp:Panel>

                    <!-- Formulario -->
                    <div class="mb-3">
                        <label for="txtUsuario" class="form-label fw-bold">Usuario o Email</label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control form-control-lg" 
                                    placeholder="Ingrese su usuario o email" autocomplete="username" />
                    </div>
                    
                    <div class="mb-3">
                        <label for="txtPassword" class="form-label fw-bold">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                                    CssClass="form-control form-control-lg" 
                                    placeholder="Ingrese su contraseña" autocomplete="current-password" />
                    </div>
                    
                    <div class="d-grid gap-2">
                        <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" 
                                   CssClass="btn btn-primary btn-lg" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .min-vh-50 {
            min-height: 50vh;
        }
    </style>
</asp:Content>