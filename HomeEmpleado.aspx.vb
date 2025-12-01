Public Class HomeEmpleado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' ✅ SOLO verifica que haya sesión de usuario
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If
            ' Si hay sesión, mostrar la página
        End If
    End Sub
End Class