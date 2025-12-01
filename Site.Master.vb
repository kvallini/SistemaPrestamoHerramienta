Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected litUserName As Literal
    Protected userMenu As HtmlGenericControl
    Protected loginMenu As HtmlGenericControl
    Protected lnkCerrarSesion As LinkButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ConfigurarMenu()
        End If
    End Sub

    Private Sub ConfigurarMenu()
        Dim estaAutenticado As Boolean = Context.User.Identity.IsAuthenticated

        If estaAutenticado Then
            ' Usuario autenticado
            userMenu.Visible = True
            loginMenu.Visible = False

            ' Mostrar nombre de usuario
            If Session("NombreUsuario") IsNot Nothing Then
                litUserName.Text = Session("NombreUsuario").ToString()
            Else
                litUserName.Text = Context.User.Identity.Name
            End If
        Else
            ' Usuario no autenticado
            userMenu.Visible = False
            loginMenu.Visible = True
        End If
    End Sub

    Protected Sub lnkCerrarSesion_Click(sender As Object, e As EventArgs)
        ' Limpiar sesión
        Session.Clear()
        Session.Abandon()

        ' Cerrar autenticación
        System.Web.Security.FormsAuthentication.SignOut()

        ' Redirigir al login
        Response.Redirect("~/Login.aspx")
    End Sub
End Class