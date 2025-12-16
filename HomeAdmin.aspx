<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="HomeAdmin.aspx.vb" 
    Inherits="SistemaPrestamoHerramienta.HomeAdmin" MasterPageFile="~/Site.Master" Title="Administrador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Header -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h2 class="mb-1">Panel de Administración</h2>
                        <p class="text-muted mb-0">Gestión completa del sistema</p>
                    </div>
                    <div class="text-end">
                        <small class="text-muted">Rol: <strong><%= If(Session("RolUsuario"), "No asignado") %></strong></small>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tarjetas de Acceso Rápido -->
        <div class="row g-4">
            <!-- Gestión de Usuarios -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-primary mb-3">
                            <i class="fas fa-users fa-3x"></i>
                        </div>
                        <h5 class="card-title">Gestión de Usuarios</h5>
                        <p class="card-text text-muted">Administrar usuarios y roles del sistema</p>
                        <a href="GestionUsuarios.aspx" class="btn btn-primary mt-2">
                            <i class="fas fa-arrow-right me-2"></i>Gestionar Usuarios
                        </a>
                    </div>
                </div>
            </div>

            <!-- Gestión de Herramientas -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-success mb-3">
                            <i class="fas fa-tools fa-3x"></i>
                        </div>
                        <h5 class="card-title">Gestión de Herramientas</h5>
                        <p class="card-text text-muted">Administrar todo el inventario</p>
                        <a href="GestionHerramientas.aspx" class="btn btn-success mt-2">
                            <i class="fas fa-arrow-right me-2"></i>Gestionar Herramientas
                        </a>
                    </div>
                </div>
            </div>

            <!-- Gestión de Categorías -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-info mb-3">
                            <i class="fas fa-tags fa-3x"></i>
                        </div>
                        <h5 class="card-title">Gestión de Categorías</h5>
                        <p class="card-text text-muted">Administrar categorías de herramientas</p>
                        <a href="GestionCategorias.aspx" class="btn btn-info mt-2 text-white">
                            <i class="fas fa-arrow-right me-2"></i>Gestionar Categorías
                        </a>
                    </div>
                </div>
            </div>

            <!-- Reportes -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-warning mb-3">
                            <i class="fas fa-chart-bar fa-3x"></i>
                        </div>
                        <h5 class="card-title">Reportes y Estadísticas</h5>
                        <p class="card-text text-muted">Ver reportes del sistema</p>
                        <a href="ReportesEstadisticas.aspx" class="btn btn-warning mt-2 text-white">
                            <i class="fas fa-arrow-right me-2"></i>Ver Reportes
                        </a>
                    </div>
                </div>
            </div>

            <!-- Todos los Préstamos -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-secondary mb-3">
                            <i class="fas fa-list-alt fa-3x"></i>
                        </div>
                        <h5 class="card-title">Todos los Préstamos</h5>
                        <p class="card-text text-muted">Ver histórico de préstamos</p>
                        <a href="HistoricoPrestamos.aspx" class="btn btn-secondary mt-2">
                            <i class="fas fa-arrow-right me-2"></i>Ver Préstamos
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>