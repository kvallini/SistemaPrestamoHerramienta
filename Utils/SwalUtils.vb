Namespace Utils
    Public Module SwalUtils
        Public Sub ShowSwalMessage(page As System.Web.UI.Page, title As String, message As String, icon As String)
            title = title.Replace("'", "")
            message = message.Replace("'", "")
            ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), ShowSwalScript(title, message, icon), True)
        End Sub

        Public Function ShowSwalScript(title As String, message As String, icon As String) As String
            title = title.Replace("'", "")
            message = message.Replace("'", "")
            Return $"swal.fire({{title: '{title}', text: '{message}', icon: '{icon}'}});"
        End Function

        Public Sub ShowSwalError(page As System.Web.UI.Page, title As String, message As String)
            ShowSwalMessage(page, title, message, "error")
        End Sub

        Public Sub ShowSwalError(page As System.Web.UI.Page, message As String)
            ShowSwalMessage(page, "Error", message, "error")
        End Sub

        Public Sub ShowSwal(page As System.Web.UI.Page, title As String, Optional message As String = "", Optional icon As String = "success")
            ShowSwalMessage(page, title, message, icon)
        End Sub
    End Module
End Namespace
