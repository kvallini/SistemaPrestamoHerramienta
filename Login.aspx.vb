Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' Si NO hay usuario en Session
            If Session("UsuarioID") Is Nothing Then
                Exit Sub
            End If

            ' Si hay sesión activa redirige según rol
            Dim rol As String = Session("RolUsuario")?.ToString().ToLower()

            If rol IsNot Nothing Then
                If rol.Contains("jefe") Or rol.Contains("bodega") Then
                    Response.Redirect("HomeJefeBodega.aspx")
                ElseIf rol.Contains("admin") Then
                    Response.Redirect("HomeAdmin.aspx")
                Else
                    Response.Redirect("HomeEmpleado.aspx")
                End If
            End If
        End If
    End Sub


    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim usuarioInput As String = txtUsuario.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If String.IsNullOrEmpty(usuarioInput) OrElse String.IsNullOrEmpty(password) Then
            MostrarError("Por favor ingrese usuario y contraseña")
            Return
        End If

        Dim dbLogin As New dbLogin()
        If dbLogin.ValidateLogin(usuarioInput, password) Then

            Dim usuarioAutenticado As Usuario = dbLogin.GetUser(usuarioInput)

            If usuarioAutenticado IsNot Nothing Then

                Session("UsuarioID") = usuarioAutenticado.UsuarioID
                Session("NombreUsuario") = usuarioAutenticado.Nombre
                Session("EmailUsuario") = usuarioAutenticado.Email
                Session("RolUsuario") = usuarioAutenticado.Rol
                Session("Activo") = usuarioAutenticado.Activo

                System.Web.Security.FormsAuthentication.SetAuthCookie(usuarioAutenticado.Nombre, False)

                Dim rol As String = usuarioAutenticado.Rol.ToLower()
                If rol.Contains("jefe") Or rol.Contains("bodega") Then
                    Response.Redirect("HomeJefeBodega.aspx")
                ElseIf rol.Contains("admin") Then
                    Response.Redirect("HomeAdmin.aspx")
                Else
                    Response.Redirect("HomeEmpleado.aspx")
                End If

            Else
                MostrarError("Error al obtener información del usuario")
            End If

        Else
            MostrarError("Usuario o contraseña incorrectos")
        End If
    End Sub

    Private Sub MostrarError(mensaje As String)
        litMensaje.Text = mensaje
        pnlMensaje.Visible = True
    End Sub
End Class
