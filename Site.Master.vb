Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected litUserName As Literal
    Protected userMenu As HtmlGenericControl
    Protected loginMenu As HtmlGenericControl
    Protected lnkCerrarSesion As LinkButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ConfigurarMenu()
        End If
    End Sub

    Private Sub ConfigurarMenu()
        Dim estaAutenticado As Boolean = (Session("UsuarioID") IsNot Nothing)

        If estaAutenticado Then
            userMenu.Visible = True
            loginMenu.Visible = False

            If Session("NombreUsuario") IsNot Nothing Then
                litUserName.Text = Session("NombreUsuario").ToString()
            Else
                litUserName.Text = Context.User.Identity.Name
            End If
        Else
            userMenu.Visible = False
            loginMenu.Visible = True
        End If
    End Sub

    Protected Sub lnkCerrarSesion_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()

        System.Web.Security.FormsAuthentication.SignOut()

        Response.Redirect("~/Login.aspx")
    End Sub

    'SweetAlert en todo el proyecto
    Public Sub MostrarAlerta(titulo As String, mensaje As String, tipo As String)
        Dim script As String = $"showAlert('{titulo}', '{mensaje}', '{tipo}');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SwalMensajeGlobal", script, True)
    End Sub

End Class
