<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="HomeEmpleado.aspx.vb" 
    Inherits="SistemaPrestamoHerramienta.HomeEmpleado" MasterPageFile="~/Site.Master" Title="Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Header -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h2 class="mb-1">Bienvenido</h2>
                        <p class="text-muted mb-0">Sistema de Gestión de Préstamo de Herramientas</p>
                    </div>
                    <div class="text-end">
                        <small class="text-muted">Rol: <strong><%= If(Session("RolUsuario"), "No asignado") %></strong></small>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tarjetas de Acceso Rápido -->
        <div class="row g-4">
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-primary mb-3">
                            <i class="fas fa-tools fa-3x"></i>
                        </div>
                        <h5 class="card-title">Herramientas Disponibles</h5>
                        <p class="card-text text-muted">Explora y solicita herramientas disponibles para préstamo</p>
                        <a href="HerramientasDisponibles.aspx" class="btn btn-primary mt-2">
                            <i class="fas fa-arrow-right me-2"></i>Ver Herramientas
                        </a>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-success mb-3">
                            <i class="fas fa-clipboard-list fa-3x"></i>
                        </div>
                        <h5 class="card-title">Mis Solicitudes</h5>
                        <p class="card-text text-muted">Revisa el estado de tus solicitudes de préstamo</p>
                        <a href="MisSolicitudes.aspx" class="btn btn-success mt-2">
                            <i class="fas fa-arrow-right me-2"></i>Ver Solicitudes
                        </a>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-info mb-3">
                            <i class="fas fa-history fa-3x"></i>
                        </div>
                        <h5 class="card-title">Préstamos Activos</h5>
                        <p class="card-text text-muted">Consulta tus préstamos activos y fechas de devolución</p>
                        <a href="MisSolicitudes.aspx?filtro=aprobados" class="btn btn-info mt-2 text-white">
                            <i class="fas fa-arrow-right me-2"></i>Ver Préstamos
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Información del Usuario -->
        <div class="row mt-5">
            <div class="col-lg-8 mx-auto">
                <div class="card">
                    <div class="card-header bg-light">
                        <h5 class="card-title mb-0">
                            <i class="fas fa-user-circle me-2"></i>Información de la Sesión
                        </h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <p><strong>Usuario:</strong> <%= If(Session("NombreUsuario"), "No disponible") %></p>
                                <p><strong>Email:</strong> <%= If(Session("EmailUsuario"), "No disponible") %></p>
                            </div>
                            <div class="col-md-6">
                                <p><strong>Rol:</strong> <%= If(Session("RolUsuario"), "No disponible") %></p>
                                <p><strong>ID Usuario:</strong> <%= If(Session("UsuarioID"), "No disponible") %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <style>
        .card {
            transition: transform 0.2s, box-shadow 0.2s;
        }
        .card:hover {
            transform: translateY(-2px);
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15) !important;
        }
    </style>
</asp:Content>