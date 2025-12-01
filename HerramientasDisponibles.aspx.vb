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

            ' Debug: ver cuántas herramientas se obtienen
            System.Diagnostics.Debug.WriteLine($"Herramientas obtenidas: {herramientas.Count}")

            gvHerramientas.DataSource = herramientas
            gvHerramientas.DataBind()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType(), "error", $"alert('Error al cargar herramientas: {ex.Message}');", True)
        End Try
    End Sub

    Protected Sub btnSolicitar_Click(sender As Object, e As EventArgs)
        Try
            Dim btnSolicitar As Button = CType(sender, Button)
            Dim herramientaID As Integer = Convert.ToInt32(btnSolicitar.CommandArgument)

            ' Debug: verificar Session
            If Session("UsuarioID") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('No hay usuario en sesión');", True)
                Return
            End If

            Dim usuarioID As Integer = Convert.ToInt32(Session("UsuarioID"))

            System.Diagnostics.Debug.WriteLine($"Solicitando: UsuarioID={usuarioID}, HerramientaID={herramientaID}")

            Dim prestamoDB As New Prestamodb()
            If prestamoDB.CrearSolicitudPrestamo(usuarioID, herramientaID, "Solicitud desde sistema") Then
                ClientScript.RegisterStartupScript(Me.GetType(), "success", "alert('Solicitud enviada correctamente');", True)
                CargarHerramientasDisponibles()
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('Error al enviar la solicitud');", True)
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType(), "error", $"alert('Error: {ex.Message}');", True)
        End Try
    End Sub
End Class