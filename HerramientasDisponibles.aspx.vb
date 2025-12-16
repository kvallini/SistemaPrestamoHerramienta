Public Class HerramientasDisponibles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If
            CargarHerramientasDisponibles()
        End If
    End Sub

    Private Sub CargarHerramientasDisponibles()
        Try
            Dim herramientaDB As New Herramientadb()
            Dim herramientas = herramientaDB.ObtenerHerramientasDisponibles()

            gvHerramientas.DataSource = herramientas
            gvHerramientas.DataBind()

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(
                Me, Me.GetType(), "err",
                "Swal.fire('Error','Error al cargar herramientas','error');", True)
        End Try
    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs)
        Try
            Dim btnSolicitar As Button = CType(sender, Button)
            Dim herramientaID As Integer = Convert.ToInt32(btnSolicitar.CommandArgument)

            If Session("UsuarioID") Is Nothing Then
                ScriptManager.RegisterStartupScript(
                    Me, Me.GetType(), "sess",
                    "Swal.fire('Sesión','Debe iniciar sesión nuevamente','warning');", True)
                Return
            End If

            Dim usuarioID As Integer = Convert.ToInt32(Session("UsuarioID"))

            Dim prestamoDB As New Prestamodb()
            If prestamoDB.CrearSolicitudPrestamo(usuarioID, herramientaID, "Solicitud desde sistema") Then

                ScriptManager.RegisterStartupScript(
                    Me, Me.GetType(), "ok",
                    "Swal.fire('Éxito','Solicitud enviada correctamente','success');", True)

                CargarHerramientasDisponibles()
            Else
                ScriptManager.RegisterStartupScript(
                    Me, Me.GetType(), "err2",
                    "Swal.fire('Error','No se pudo enviar la solicitud','error');", True)
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(
                Me, Me.GetType(), "ex",
                "Swal.fire('Error','Ocurrió un error inesperado','error');", True)
        End Try
    End Sub
End Class
