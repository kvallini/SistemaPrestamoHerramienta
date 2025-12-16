<%@ Page Language="VB" AutoEventWireup="false"
    CodeBehind="HistoricoPrestamos.aspx.vb"
    Inherits="SistemaPrestamoHerramienta.HistoricoPrestamos"
    MasterPageFile="~/Site.Master"
    Title="Histórico de Préstamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updPrestamos" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row mb-4">
                    <div class="col-12">
                        <h2>Histórico de Préstamos</h2>
                        <p class="text-muted">Ver y gestionar préstamos de herramientas</p>
                    </div>
                </div>

                <!-- Filtros -->
                <div class="row mb-3">
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlFiltroUsuario" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroUsuario_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtFiltroFecha" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="txtFiltroFecha_TextChanged" placeholder="Fecha de préstamo" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                            <asp:ListItem Text="Todos" Value="" />
                            <asp:ListItem Text="Activo" />
                            <asp:ListItem Text="Devuelto" />
                            <asp:ListItem Text="Rechazado" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="btn btn-secondary" OnClick="btnLimpiarFiltros_Click" />
                    </div>
                </div>

                <asp:GridView ID="gvPrestamos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="PrestamoID" OnRowCommand="gvPrestamos_RowCommand" EmptyDataText="No hay préstamos">
                    <Columns>
                        <asp:BoundField DataField="NombreHerramienta" HeaderText="Herramienta" />
                        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="FechaPrestamo" HeaderText="Fecha Préstamo" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="FechaDevolucionPrevista" HeaderText="Devolución Prevista" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="FechaDevolucionReal" HeaderText="Devolución Real" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" Text="Marcar Devuelto" CssClass="btn btn-success btn-sm" CommandName="MarcarDevuelto" CommandArgument='<%# Eval("PrestamoID") %>' Visible='<%# Eval("Estado").ToString() <> "Devuelto" AndAlso Session("RolUsuario") IsNot Nothing AndAlso Session("RolUsuario").ToString().ToLower() = "jefebodega" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- MODAL para marcar devuelto -->
            <div class="modal fade" id="modalDevolucion" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Marcar Préstamo como Devuelto</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnPrestamoID" runat="server" />
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
        function abrirModalDevolucion() {
            new bootstrap.Modal(document.getElementById('modalDevolucion')).show();
        }
        function cerrarModalDevolucion() {
            var modalElement = document.getElementById('modalDevolucion');
            var modal = bootstrap.Modal.getInstance(modalElement);
            if (modal) {
                modal.hide();
            } else {
                var bsModal = new bootstrap.Modal(modalElement);
                bsModal.hide();
            }
            setTimeout(function () {
                modalElement.style.display = 'none';
                document.body.classList.remove('modal-open');
                document.body.style.overflow = 'auto';
                var backdrop = document.querySelector('.modal-backdrop');
                if (backdrop) {
                    backdrop.remove();
                }
            }, 200);
        }
    </script>
</asp:Content>