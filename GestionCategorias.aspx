<%@ Page Language="VB" AutoEventWireup="false"
    CodeBehind="GestionCategorias.aspx.vb"
    Inherits="SistemaPrestamoHerramienta.GestionCategorias"
    MasterPageFile="~/Site.Master"
    Title="Gestión de Categorías" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updCategorias" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row mb-4">
                    <div class="col-12 d-flex justify-content-between align-items-center">
                        <div>
                            <h2 class="mb-1">Gestión de Categorías</h2>
                            <p class="text-muted mb-0">Administrar categorías de herramientas</p>
                        </div>
                        <asp:Button ID="btnNuevaCategoria" runat="server" Text="Nueva Categoría" CssClass="btn btn-primary" OnClick="btnNuevaCategoria_Click" />
                    </div>
                </div>

                <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" EmptyDataText="No hay categorías registradas" OnRowCommand="gvCategorias_RowCommand" DataKeyNames="CategoriaID">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-warning btn-sm me-1" CommandName="Editar" CommandArgument='<%# Eval("CategoriaID") %>' />
                                <asp:Button runat="server" Text="Eliminar" CssClass="btn btn-danger btn-sm" CommandName="Eliminar" CommandArgument='<%# Eval("CategoriaID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Modal Categoría -->
            <div class="modal fade" id="modalCategoria" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">
                                <asp:Literal ID="litModalTitulo" runat="server" Text="Nueva Categoría" />
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnCategoriaID" runat="server" Value="0" />
                            <div class="mb-3">
                                <label class="form-label">Nombre *</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre de la categoría" MaxLength="100" />
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Descripción de la categoría..." MaxLength="500" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Categoría" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
            </div>

      
<script>
    function abrirModalCategoria() {
        new bootstrap.Modal(document.getElementById('modalCategoria')).show();
    }
    function cerrarModalCategoria() {
        var modalElement = document.getElementById('modalCategoria');
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

    // Función para confirmar eliminación
    function confirmarEliminar(button) {
        Swal.fire({
            title: '¿Estás seguro?',
            text: 'Esta acción no se puede deshacer',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                __doPostBack(button.name, '');  // Fuerza el postback
            }
        });
    }

    // Intercepta clics en botones de eliminar
    document.addEventListener('DOMContentLoaded', function () {
        var eliminarButtons = document.querySelectorAll('input[type="submit"][value="Eliminar"]');  // Selector general, ajusta si hay varios grids
        eliminarButtons.forEach(function (button) {
            button.addEventListener('click', function (e) {
                e.preventDefault();
                confirmarEliminar(this);
            });
        });
    });
</script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
