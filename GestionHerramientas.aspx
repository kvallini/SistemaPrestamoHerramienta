<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="GestionHerramientas.aspx.vb" 
    Inherits="SistemaPrestamoHerramienta.GestionHerramientas" MasterPageFile="~/Site.Master" Title="Gestión de Herramientas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <h2 class="mb-1">Gestión de Herramientas</h2>
                        <p class="text-muted mb-0">Administrar el inventario de herramientas</p>
                    </div>
                    <div>
                        <asp:Button ID="btnNuevaHerramienta" runat="server" Text="Nueva Herramienta" 
                                    CssClass="btn btn-primary" OnClick="btnNuevaHerramienta_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- GridView para todas las herramientas -->
        <div class="row">
            <div class="col-12">
                <!-- Reemplaza el GridView actual por este: -->
<asp:GridView ID="gvHerramientas" runat="server" AutoGenerateColumns="False" 
    CssClass="table table-striped table-bordered" EmptyDataText="No hay herramientas registradas"
    OnRowCommand="gvHerramientas_RowCommand" DataKeyNames="HerramientaID"
    OnRowDataBound="gvHerramientas_RowDataBound">
    <Columns>
        <asp:BoundField DataField="Codigo" HeaderText="Código" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="NombreCategoria" HeaderText="Categoría" />
        <asp:TemplateField HeaderText="Estado">
            <ItemTemplate>
                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" />
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                    CssClass="btn btn-warning btn-sm me-1" 
                    CommandName="Editar" 
                    CommandArgument='<%# Eval("HerramientaID") %>' />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                    CssClass="btn btn-danger btn-sm" 
                    CommandName="Eliminar" 
                    CommandArgument='<%# Eval("HerramientaID") %>'
                    Visible='<%# EsAdmin() %>' 
                    OnClientClick="return confirm('¿Está seguro de eliminar esta herramienta?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
            </div>
        </div>
    </div>

    <!-- Modal para crear/editar herramienta -->
    <div class="modal fade" id="modalHerramienta" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <asp:Literal ID="litModalTitulo" runat="server" Text="Nueva Herramienta" />
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hdnHerramientaID" runat="server" Value="0" />
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Código *</label>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" 
                                        placeholder="Código único de la herramienta" MaxLength="50" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Nombre *</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" 
                                        placeholder="Nombre de la herramienta" MaxLength="100" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Categoría</label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Seleccionar categoría" Value="" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Estado *</label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Disponible" Value="Disponible" Selected="True" />
                                <asp:ListItem Text="En Mantenimiento" Value="En Mantenimiento" />
                                <asp:ListItem Text="Dañada" Value="Dañada" />
                                <asp:ListItem Text="Retirada" Value="Retirada" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Ubicación</label>
                            <asp:TextBox ID="txtUbicacion" runat="server" CssClass="form-control" 
                                        placeholder="Ubicación en bodega" MaxLength="100" />
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Disponible</label>
                            <div class="form-check mt-2">
                                <asp:CheckBox ID="chkDisponible" runat="server" CssClass="form-check-input" Checked="true" />
                                <label class="form-check-label" for="<%= chkDisponible.ClientID %>">
                                    Herramienta disponible para préstamo
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                                    Rows="3" CssClass="form-control" placeholder="Descripción de la herramienta..." MaxLength="500" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Herramienta" 
                                CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function abrirModal() {
            var modal = new bootstrap.Modal(document.getElementById('modalHerramienta'));
            modal.show();
        }
    </script>
</asp:Content>