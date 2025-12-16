<%@ Page Language="VB" AutoEventWireup="false"
    CodeBehind="GestionSolicitudes.aspx.vb"
    Inherits="SistemaPrestamoHerramienta.GestionSolicitudes"
    MasterPageFile="~/Site.Master"
    Title="Gestión de Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updSolicitudes" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row mb-4">
                    <div class="col-12">
                        <h2>Gestión de Solicitudes de Préstamo</h2>
                        <p class="text-muted">Aprobar o rechazar solicitudes pendientes</p>
                    </div>
                </div>

                <asp:GridView ID="gvSolicitudesPendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="PrestamoID" OnRowCommand="gvSolicitudesPendientes_RowCommand"
                    EmptyDataText="No hay solicitudes pendientes">
                    <Columns>
                        <asp:BoundField DataField="Herramienta" HeaderText="Herramienta" />
                        <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ComentariosSolicitud" HeaderText="Comentarios" />
                        <asp:BoundField DataField="FechaDevolucionPrevista" HeaderText="Devolución Prevista" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" Text="Aprobar" CssClass="btn btn-success btn-sm me-1" CommandName="Aprobar" CommandArgument='<%# Eval("PrestamoID") %>' />
                                <asp:Button runat="server" Text="Rechazar" CssClass="btn btn-danger btn-sm me-1" OnClientClick='<%# "abrirModalRechazo(" & Eval("PrestamoID") & "); return false;" %>' />
                                <asp:Button runat="server" Text="Marcar Devuelto" CssClass="btn btn-info btn-sm" CommandName="MarcarDevuelto" CommandArgument='<%# Eval("PrestamoID") %>' Visible='<%# Eval("Estado").ToString() = "Aprobado" AndAlso Session("RolUsuario") IsNot Nothing AndAlso Session("RolUsuario").ToString().ToLower().Contains("jefebodega") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Modal para Rechazar -->
            <div class="modal fade" id="modalRechazo" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Rechazar Solicitud</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnPrestamoID" runat="server" />
                            <asp:TextBox ID="txtMotivoRechazo" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Motivo de rechazo" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmarRechazo" runat="server" Text="Confirmar Rechazo" CssClass="btn btn-danger" OnClick="btnConfirmarRechazo_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal para Devolución -->
            <div class="modal fade" id="modalDevolucion" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Marcar Préstamo como Devuelto</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnPrestamoIDDevolucion" runat="server" />
                            <p>¿Confirmar devolución del préstamo?</p>
                            <asp:TextBox ID="txtObservacionesDevolucion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Observaciones (opcional)" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmarDevolucion" runat="server" Text="Confirmar" CssClass="btn btn-primary" OnClick="btnConfirmarDevolucion_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function abrirModalRechazo(prestamoID) {
            document.getElementById('<%= hdnPrestamoID.ClientID %>').value = prestamoID;
            new bootstrap.Modal(document.getElementById('modalRechazo')).show();
        }
        function cerrarModalRechazo() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalRechazo'));
            if (modal) modal.hide();
        }

        function abrirModalDevolucion(prestamoID) {
            document.getElementById('<%= hdnPrestamoIDDevolucion.ClientID %>').value = prestamoID;
            new bootstrap.Modal(document.getElementById('modalDevolucion')).show();
        }
        function cerrarModalDevolucion() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalDevolucion'));
            if (modal) modal.hide();
        }
    </script>
</asp:Content>