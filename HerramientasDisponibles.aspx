<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="HerramientasDisponibles.aspx.vb" 
    Inherits="SistemaPrestamoHerramienta.HerramientasDisponibles" MasterPageFile="~/Site.Master" Title="Herramientas Disponibles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-12">
                <h2>Herramientas Disponibles</h2>
                <p class="text-muted">Solicita las herramientas que necesites para tus actividades</p>
            </div>
        </div>

        <!-- GridView para mostrar herramientas -->
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="gvHerramientas" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered" EmptyDataText="No hay herramientas disponibles">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                        <asp:BoundField DataField="NombreCategoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <asp:Button ID="btnSolicitar" runat="server" Text="Solicitar" 
                                    CssClass="btn btn-primary btn-sm" 
                                    CommandArgument='<%# Eval("HerramientaID") %>' 
                                    OnClick="btnSolicitar_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>