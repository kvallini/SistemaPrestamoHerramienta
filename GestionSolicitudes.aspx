<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="GestionSolicitudes.aspx.vb" 
    Inherits="SistemaPrestamoHerramienta.GestionSolicitudes" MasterPageFile="~/Site.Master" Title="Gestión de Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-12">
                <h2>Gestión de Solicitudes de Préstamo</h2>
                <p class="text-muted">Aprobar o rechazar solicitudes pendientes</p>
            </div>
        </div>

        <!-- GridView para solicitudes pendientes -->
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="gvSolicitudesPendientes" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered" EmptyDataText="No hay solicitudes pendientes"
                    OnRowCommand="gvSolicitudesPendientes_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" />
                        <asp:BoundField DataField="Herramienta" HeaderText="Herramienta" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="FechaDevolucionPrevista" HeaderText="Devolución Prevista" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ComentariosSolicitud" HeaderText="Comentarios" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" 
                                    CssClass="btn btn-success btn-sm me-1" 
                                    CommandName="Aprobar" 
                                    CommandArgument='<%# Eval("PrestamoID") %>' />
                                <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" 
                                    CssClass="btn btn-danger btn-sm" 
                                    CommandName="Rechazar" 
                                    CommandArgument='<%# Eval("PrestamoID") %>' 
                                    OnClientClick="return mostrarMotivoRechazo(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <!-- Modal para motivo de rechazo -->
    <div class="modal fade" id="modalRechazo" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Motivo de Rechazo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hdnPrestamoID" runat="server" />
                    <div class="mb-3">
                        <label class="form-label">Ingrese el motivo del rechazo:</label>
                        <asp:TextBox ID="txtMotivoRechazo" runat="server" TextMode="MultiLine" 
                                    Rows="3" CssClass="form-control" placeholder="Motivo del rechazo..."></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarRechazo" runat="server" Text="Confirmar Rechazo" 
                                CssClass="btn btn-danger" OnClick="btnConfirmarRechazo_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function mostrarMotivoRechazo(btn) {
            var prestamoID = btn.getAttribute('CommandArgument');
            document.getElementById('<%= hdnPrestamoID.ClientID %>').value = prestamoID;
            var modal = new bootstrap.Modal(document.getElementById('modalRechazo'));
            modal.show();
            return false; 

    </script>


</asp:Content>