Public Class HomeJefeBodega
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If

            ' Verificar que sea JefeBodega
            If Session("RolUsuario") Is Nothing OrElse Session("RolUsuario").ToString().ToLower() <> "jefebodega" Then
                Response.Redirect("Login.aspx")
                Return
            End If
        End If
    End Sub
End Class