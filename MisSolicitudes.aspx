<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="MisSolicitudes.aspx.vb" 
    Inherits="SistemaPrestamoHerramienta.MisSolicitudes" MasterPageFile="~/Site.Master" Title="Mis Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-12">
                <h2>Mis Solicitudes de Préstamo</h2>
                <p class="text-muted">Revisa el estado de tus solicitudes</p>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered" EmptyDataText="No tienes solicitudes"
                    OnRowDataBound="gvSolicitudes_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Herramienta" HeaderText="Herramienta" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="FechaDevolucionPrevista" HeaderText="Devolución Prevista" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" />
                        <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha Respuesta" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="MotivoRechazo" HeaderText="Motivo Rechazo" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Resumen de estados -->
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title mb-0">Resumen de Estados</h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3 text-center">
                                <span class="badge bg-warning fs-6">Pendiente</span>
                                <p class="mt-1 mb-0">Esperando aprobación</p>
                            </div>
                            <div class="col-md-3 text-center">
                                <span class="badge bg-success fs-6">Aprobado</span>
                                <p class="mt-1 mb-0">Solicitud aceptada</p>
                            </div>
                            <div class="col-md-3 text-center">
                                <span class="badge bg-danger fs-6">Rechazado</span>
                                <p class="mt-1 mb-0">Solicitud denegada</p>
                            </div>
                            <div class="col-md-3 text-center">
                                <span class="badge bg-info fs-6">Devuelto</span>
                                <p class="mt-1 mb-0">Herramienta devuelta</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>