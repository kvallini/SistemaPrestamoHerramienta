Public Class MisSolicitudes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If
            CargarMisSolicitudes()
        End If
    End Sub

    Private Sub CargarMisSolicitudes()
        Dim usuarioID As Integer = Convert.ToInt32(Session("UsuarioID"))
        Dim prestamoDB As New Prestamodb()

        gvSolicitudes.DataSource = prestamoDB.ObtenerSolicitudesPorUsuario(usuarioID)
        gvSolicitudes.DataBind()
    End Sub

    ' CColores en estado mejora
    Protected Sub gvSolicitudes_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblEstado As Label = CType(e.Row.FindControl("lblEstado"), Label)

            If lblEstado IsNot Nothing Then
                Select Case lblEstado.Text.ToLower()
                    Case "pendiente"
                        lblEstado.CssClass = "badge bg-warning"
                    Case "aprobado", "aprobada"
                        lblEstado.CssClass = "badge bg-success"
                    Case "rechazado", "rechazada"
                        lblEstado.CssClass = "badge bg-danger"
                    Case "devuelto", "completado"
                        lblEstado.CssClass = "badge bg-info"
                    Case Else
                        lblEstado.CssClass = "badge bg-secondary"
                End Select
            End If
        End If
    End Sub
End Class