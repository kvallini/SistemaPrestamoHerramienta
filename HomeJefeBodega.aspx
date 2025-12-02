<%@ Page Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" 
    CodeBehind="HomeJefeBodega.aspx.vb" Inherits="SistemaPrestamoHerramienta.HomeJefeBodega" Title="Jefe de Bodega" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Header -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h2 class="mb-1">Panel de Jefe de Bodega</h2>
                        <p class="text-muted mb-0">Gestión completa de herramientas y préstamos</p>
                    </div>
                    <div class="text-end">
                        <small class="text-muted">Rol: <strong><%= If(Session("RolUsuario"), "No asignado") %></strong></small>
                    </div>
                </div>
            </div>
        </div>

        <!-- Tarjetas de Acceso Rápido -->
        <div class="row g-4">
            <!-- Gestión de Solicitudes -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-warning mb-3">
                            <i class="fas fa-clipboard-check fa-3x"></i>
                        </div>
                        <h5 class="card-title">Solicitudes Pendientes</h5>
                        <p class="card-text text-muted">Revisar y aprobar solicitudes de préstamo</p>
                        <a href="GestionSolicitudes.aspx" class="btn btn-warning mt-2 text-white">
                            <i class="fas fa-arrow-right me-2"></i>Gestionar Solicitudes
                        </a>
                    </div>
                </div>
            </div>

            <!-- Gestión de Herramientas -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-primary mb-3">
                            <i class="fas fa-tools fa-3x"></i>
                        </div>
                        <h5 class="card-title">Gestión de Herramientas</h5>
                        <p class="card-text text-muted">Agregar, editar y eliminar herramientas</p>
                        <a href="GestionHerramientas.aspx" class="btn btn-primary mt-2">
                            <i class="fas fa-arrow-right me-2"></i>Gestionar Herramientas
                        </a>
                    </div>
                </div>
            </div>

            <!-- Gestión de Categorías -->
            <div class="col-md-4">
                <div class="card h-100 border-0 shadow-sm">
                    <div class="card-body text-center p-4">
                        <div class="text-success mb-3">
                            <i class="fas fa-tags fa-3x"></i>
                        </div>
                        <h5 class="card-title">Gestión de Categorías</h5>
                        <p class="card-text text-muted">Administrar categorías de herramientas</p>
                        <a href="GestionCategorias.aspx" class="btn btn-success mt-2">
                            <i class="fas fa-arrow-right me-2"></i>Gestionar Categorías
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>