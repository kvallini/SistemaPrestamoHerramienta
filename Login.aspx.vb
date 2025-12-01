Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Si ya está autenticado, redirigir al home
            If User.Identity.IsAuthenticated Then
                Response.Redirect("HomeEmpleado.aspx")
            End If
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim usuarioInput As String = txtUsuario.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        ' Validaciones básicas
        If String.IsNullOrEmpty(usuarioInput) OrElse String.IsNullOrEmpty(password) Then
            MostrarError("Por favor ingrese usuario y contraseña")
            Return
        End If

        ' Autenticar usuario
        Dim dbLogin As New dbLogin()
        If dbLogin.ValidateLogin(usuarioInput, password) Then
            ' Obtener información del usuario
            Dim usuarioAutenticado As Usuario = dbLogin.GetUser(usuarioInput)

            If usuarioAutenticado IsNot Nothing Then
                ' Guardar en sesión
                Session("UsuarioID") = usuarioAutenticado.UsuarioID
                Session("NombreUsuario") = usuarioAutenticado.Nombre
                Session("EmailUsuario") = usuarioAutenticado.Email
                Session("RolUsuario") = usuarioAutenticado.Rol
                Session("Activo") = usuarioAutenticado.Activo

                ' Configurar autenticación de forms
                System.Web.Security.FormsAuthentication.SetAuthCookie(usuarioAutenticado.Nombre, False)

                ' Redirigir según el rol (por ahora todos van a HomeEmpleado)
                Response.Redirect("HomeEmpleado.aspx")
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