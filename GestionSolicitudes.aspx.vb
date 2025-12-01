Public Class GestionSolicitudes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing OrElse Session("RolUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If

            ' Verificar que sea JefeBodega
            If Not Session("RolUsuario").ToString().ToLower().Contains("jefe") Then
                Response.Redirect("Login.aspx")
                Return
            End If

            CargarSolicitudesPendientes()
        End If
    End Sub

    Private Sub CargarSolicitudesPendientes()
        Dim prestamoDB As New Prestamodb()
        gvSolicitudesPendientes.DataSource = prestamoDB.ObtenerSolicitudesPendientes()
        gvSolicitudesPendientes.DataBind()
    End Sub

    Protected Sub gvSolicitudesPendientes_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Aprobar" Then
            Dim prestamoID As Integer = Convert.ToInt32(e.CommandArgument)
            AprobarSolicitud(prestamoID)
        End If
        ' Rechazar se maneja desde el modal
    End Sub

    Private Sub AprobarSolicitud(prestamoID As Integer)
        Dim prestamoDB As New Prestamodb()
        If prestamoDB.AprobarSolicitud(prestamoID) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "success", "alert('Solicitud aprobada correctamente');", True)
            CargarSolicitudesPendientes()
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('Error al aprobar la solicitud');", True)
        End If
    End Sub

    Protected Sub btnConfirmarRechazo_Click(sender As Object, e As EventArgs)
        Dim prestamoID As Integer = Convert.ToInt32(hdnPrestamoID.Value)
        Dim motivo As String = txtMotivoRechazo.Text.Trim()

        If String.IsNullOrEmpty(motivo) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('Debe ingresar un motivo de rechazo');", True)
            Return
        End If

        Dim prestamoDB As New Prestamodb()
        If prestamoDB.RechazarSolicitud(prestamoID, motivo) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "success", "alert('Solicitud rechazada correctamente');", True)
            txtMotivoRechazo.Text = "" ' Limpiar el texto
            CargarSolicitudesPendientes()
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('Error al rechazar la solicitud');", True)
        End If
    End Sub
End Class