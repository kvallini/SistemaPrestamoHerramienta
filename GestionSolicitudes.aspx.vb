Imports System.Data

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
        ElseIf e.CommandName = "MarcarDevuelto" Then
            hdnPrestamoIDDevolucion.Value = e.CommandArgument.ToString()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalDevolucion();", True)
        End If
    End Sub

    Private Sub AprobarSolicitud(prestamoID As Integer)
        Dim prestamoDB As New Prestamodb()
        If prestamoDB.AprobarSolicitud(prestamoID) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "success", "Swal.fire('Éxito','Solicitud aprobada correctamente','success');", True)
            CargarSolicitudesPendientes()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "Swal.fire('Error','Error al aprobar la solicitud','error');", True)
        End If
    End Sub

    Protected Sub btnConfirmarRechazo_Click(sender As Object, e As EventArgs)
        Dim prestamoID As Integer = Convert.ToInt32(hdnPrestamoID.Value)
        Dim motivo As String = txtMotivoRechazo.Text.Trim()

        If String.IsNullOrEmpty(motivo) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "Swal.fire('Validación','Debe ingresar un motivo de rechazo','warning');", True)
            Return
        End If

        Dim prestamoDB As New Prestamodb()
        If prestamoDB.RechazarSolicitud(prestamoID, motivo) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "success", "cerrarModalRechazo(); Swal.fire('Éxito','Solicitud rechazada correctamente','success');", True)
            txtMotivoRechazo.Text = ""
            CargarSolicitudesPendientes()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "error", "Swal.fire('Error','Error al rechazar la solicitud','error');", True)
        End If
    End Sub

    Protected Sub btnConfirmarDevolucion_Click(sender As Object, e As EventArgs)
        If Session("RolUsuario") Is Nothing OrElse Not Session("RolUsuario").ToString().ToLower().Contains("admin") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','No tienes permisos para esta acción','error');", True)
            Return
        End If

        Dim ok As Boolean = New Prestamodb().Update(CInt(hdnPrestamoIDDevolucion.Value), DateTime.Now, "Devuelto", txtObservacionesDevolucion.Text.Trim())
        If ok Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "cerrarOk", "cerrarModalDevolucion(); Swal.fire('Éxito','Préstamo marcado como devuelto','success');", True)
            CargarSolicitudesPendientes()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','No se pudo actualizar','error');", True)
        End If
    End Sub
End Class