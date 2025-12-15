
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

    Public Sub ShowSwalConfirmDelete(page As System.Web.UI.Page, nombreRegistro As String, urlEliminar As String)
        nombreRegistro = nombreRegistro.Replace("'", "\'").Replace("""", "\""")
        urlEliminar = urlEliminar.Replace("'", "\'").Replace("""", "\""")

        Dim script As String = $"
        Swal.fire({{
            title: '¿Confirmar eliminación?',
            html: '¿Está seguro de eliminar <b>{nombreRegistro}</b>?<br>Esta acción no se puede deshacer.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar',
            allowOutsideClick: false,
            backdrop: 'rgba(0,0,0,0.7)'
        }}).then((result) => {{
            if (result.isConfirmed) {{
                window.location.href = '{urlEliminar}';
            }}
        }});"

        ScriptManager.RegisterStartupScript(page, page.GetType(),
                "ConfirmDelete_" & Guid.NewGuid().ToString(), script, True)
    End Sub

    Public Sub ShowSuccess(page As System.Web.UI.Page, mensaje As String)
        ShowSwalMessage(page, "¡Éxito!", mensaje, "success")
    End Sub

    Public Sub ShowError(page As System.Web.UI.Page, mensaje As String)
        ShowSwalMessage(page, "Error", mensaje, "error")
    End Sub

End Module


