Imports System.Data

Public Class HistoricoPrestamos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If
            CargarUsuariosFiltro()
            CargarPrestamos()
        End If
    End Sub

    Private Sub CargarPrestamos()
        Dim dt As DataTable = New Prestamodb().GetAll()
        ' Aplicar filtros
        If ddlFiltroUsuario.SelectedValue <> "" Then
            dt = dt.AsEnumerable().Where(Function(r) r("UsuarioID").ToString() = ddlFiltroUsuario.SelectedValue).CopyToDataTable()
        End If
        If Not String.IsNullOrEmpty(txtFiltroFecha.Text) Then
            dt = dt.AsEnumerable().Where(Function(r) CDate(r("FechaPrestamo")).Date = CDate(txtFiltroFecha.Text).Date).CopyToDataTable()
        End If
        If ddlFiltroEstado.SelectedValue <> "" Then
            dt = dt.AsEnumerable().Where(Function(r) r("Estado").ToString() = ddlFiltroEstado.SelectedValue).CopyToDataTable()
        End If
        gvPrestamos.DataSource = dt
        gvPrestamos.DataBind()
    End Sub

    Private Sub CargarUsuariosFiltro()
        ddlFiltroUsuario.Items.Clear()
        ddlFiltroUsuario.Items.Add(New ListItem("Todos los usuarios", ""))
        Dim dt As DataTable = New UsuarioDB().GetAll()
        For Each r As DataRow In dt.Rows
            ddlFiltroUsuario.Items.Add(New ListItem(r("Nombre").ToString(), r("UsuarioID").ToString()))
        Next
    End Sub

    Protected Sub gvPrestamos_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "MarcarDevuelto" Then
            hdnPrestamoID.Value = e.CommandArgument.ToString()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModalDevolucion();", True)
        End If
    End Sub

    Protected Sub btnConfirmarDevolucion_Click(sender As Object, e As EventArgs) Handles btnConfirmarDevolucion.Click
        If Session("RolUsuario") Is Nothing OrElse Session("RolUsuario").ToString().ToLower() <> "jefebodega" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','No tienes permisos para esta acción','error');", True)
            Return
        End If

        Dim ok As Boolean = New Prestamodb().Update(CInt(hdnPrestamoID.Value), DateTime.Now, "Devuelto", txtObservacionesDevolucion.Text.Trim())
        If ok Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "cerrarOk", "cerrarModalDevolucion(); Swal.fire('Éxito','Préstamo marcado como devuelto','success');", True)
            CargarPrestamos()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','No se pudo actualizar','error');", True)
        End If
    End Sub

    Protected Sub ddlFiltroUsuario_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarPrestamos()
    End Sub

    Protected Sub txtFiltroFecha_TextChanged(sender As Object, e As EventArgs)
        CargarPrestamos()
    End Sub

    Protected Sub ddlFiltroEstado_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarPrestamos()
    End Sub

    Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs)
        ddlFiltroUsuario.SelectedIndex = 0
        txtFiltroFecha.Text = ""
        ddlFiltroEstado.SelectedIndex = 0
        CargarPrestamos()
    End Sub
End Class