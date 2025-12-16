<%@ Page Language="VB" AutoEventWireup="false"
    CodeBehind="ReportesEstadisticas.aspx.vb"
    Inherits="SistemaPrestamoHerramienta.ReportesEstadisticas"
    MasterPageFile="~/Site.Master"
    Title="Reportes y Estadísticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updReportes" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row mb-4">
                    <div class="col-12">
                        <h2>Reportes y Estadísticas</h2>
                        <p class="text-muted">Vista general del sistema</p>
                    </div>
                </div>

                <!-- Estadísticas de Herramientas -->
                <div class="row mb-4">
                    <div class="col-12">
                        <h4>Estadísticas de Herramientas</h4>
                        <asp:GridView ID="gvEstadisticasHerramientas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Estadísticas de Usuarios -->
                <div class="row mb-4">
                    <div class="col-12">
                        <h4>Estadísticas de Usuarios</h4>
                        <asp:GridView ID="gvEstadisticasUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Estadísticas de Préstamos -->
                <div class="row mb-4">
                    <div class="col-12">
                        <h4>Préstamos por Mes (Últimos 12 Meses)</h4>
                        <asp:GridView ID="gvEstadisticasPrestamos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="Mes" HeaderText="Mes" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad de Préstamos" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>