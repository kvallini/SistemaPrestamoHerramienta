Imports System.Data

Public Class ReportesEstadisticas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If
            CargarEstadisticas()
        End If
    End Sub

    Private Sub CargarEstadisticas()
        Try
            ' Estadísticas de Herramientas
            gvEstadisticasHerramientas.DataSource = New Herramientadb().GetEstadisticasHerramientas()
            gvEstadisticasHerramientas.DataBind()

            ' Estadísticas de Usuarios
            gvEstadisticasUsuarios.DataSource = New UsuarioDB().GetEstadisticasUsuarios()
            gvEstadisticasUsuarios.DataBind()

            ' Estadísticas de Préstamos
            gvEstadisticasPrestamos.DataSource = New Prestamodb().GetEstadisticasPrestamosPorMes()
            gvEstadisticasPrestamos.DataBind()
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub
End Class