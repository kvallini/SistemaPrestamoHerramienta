Public Class HomeAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If

            ' Verificar que sea Administrador
            If Session("RolUsuario") Is Nothing OrElse Not Session("RolUsuario").ToString().ToLower().Contains("admin") Then
                Response.Redirect("Login.aspx")
                Return
            End If
        End If
    End Sub
End Class