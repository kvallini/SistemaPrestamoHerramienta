Partial Public Class HomeDefault
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Login.aspx")
    End Sub

End Class
