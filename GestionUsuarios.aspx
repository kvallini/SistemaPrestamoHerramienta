<%@ Page Language="VB" AutoEventWireup="false"
    CodeBehind="GestionUsuarios.aspx.vb"
    Inherits="SistemaPrestamoHerramienta.GestionUsuarios"
    MasterPageFile="~/Site.Master"
    Title="Gestión de Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updUsuarios" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row mb-4">
                    <div class="col-12 d-flex justify-content-between">
                        <div>
                            <h2>Gestión de Usuarios</h2>
                            <p class="text-muted">Administrar usuarios del sistema</p>
                        </div>
                        <asp:Button ID="btnNuevoUsuario"
                                    runat="server"
                                    Text="Nuevo Usuario"
                                    CssClass="btn btn-primary"
                                    OnClick="btnNuevoUsuario_Click"
                                    Visible='<%# Session("RolUsuario") IsNot Nothing AndAlso Session("RolUsuario").ToString().ToLower().Contains("admin") %>' />
                    </div>
                </div>

                <asp:GridView ID="gvUsuarios"
                              runat="server"
                              AutoGenerateColumns="False"
                              CssClass="table table-bordered table-striped"
                              DataKeyNames="UsuarioID"
                              EmptyDataText="No hay usuarios"
                              OnRowCommand="gvUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                        <asp:BoundField DataField="Rol" HeaderText="Rol" />
                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <asp:Label ID="lblActivo" runat="server" Text='<%# If(Eval("Activo"), "Sí", "No") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server"
                                            Text="Editar"
                                            CssClass="btn btn-warning btn-sm me-1"
                                            CommandName="Editar"
                                            CommandArgument='<%# Eval("UsuarioID") %>'
                                            Visible='<%# Session("RolUsuario") IsNot Nothing AndAlso Session("RolUsuario").ToString().ToLower().Contains("admin") %>' />
                                <asp:Button runat="server"
                                            Text="Eliminar"
                                            CssClass="btn btn-danger btn-sm"
                                            CommandName="Eliminar"
                                            CommandArgument='<%# Eval("UsuarioID") %>'
                                            Visible='<%# Session("RolUsuario") IsNot Nothing AndAlso Session("RolUsuario").ToString().ToLower().Contains("admin") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- MODAL -->
            <div class="modal fade" id="modalUsuario" tabindex="-1">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">
                                <asp:Literal ID="litModalTitulo" runat="server" />
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnUsuarioID" runat="server" Value="0" />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-2" placeholder="Nombre" />
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Nombre es obligatorio" CssClass="text-danger" Display="Dynamic" />
                            
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-2" placeholder="Email" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email es obligatorio" CssClass="text-danger" Display="Dynamic" />
                            
                            <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control mb-2" placeholder="Departamento" />
                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control mb-2" />
                            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control mb-2" placeholder="Contraseña (solo para nuevos usuarios)" />
                            <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" Checked="true" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function abrirModal() {
            new bootstrap.Modal(document.getElementById('modalUsuario')).show();
        }
        function cerrarModal() {
            var modalElement = document.getElementById('modalUsuario');
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

        function confirmarEliminarUsuario(button) {
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
                    __doPostBack(button.name, '');
                }
            });
        }

        function agregarListenersEliminar() {
            var eliminarButtons = document.querySelectorAll('input[type="submit"][value="Eliminar"]');
            eliminarButtons.forEach(function (button) {
                button.removeEventListener('click', handleEliminarClick);
                button.addEventListener('click', handleEliminarClick);
            });
        }

        function handleEliminarClick(e) {
            e.preventDefault();
            confirmarEliminarUsuario(this);
        }

        document.addEventListener('DOMContentLoaded', function () {
            agregarListenersEliminar();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            agregarListenersEliminar();
        });
    </script>
</asp:Content>