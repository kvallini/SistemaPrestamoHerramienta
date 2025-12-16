Imports System.Data

Public Class GestionCategorias
    Inherits System.Web.UI.Page

    Public ReadOnly Property EsAdmin As Boolean
        Get
            Return Session("RolUsuario") IsNot Nothing AndAlso
                   Session("RolUsuario").ToString().ToLower().Contains("admin")
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If
            CargarCategorias()
        End If
    End Sub

    Private Sub CargarCategorias()
        Dim db As New Categoriadb()
        gvCategorias.DataSource = db.ObtenerTodasCategorias()
        gvCategorias.DataBind()
    End Sub

    Protected Sub btnNuevaCategoria_Click(sender As Object, e As EventArgs) Handles btnNuevaCategoria.Click
        LimpiarFormulario()
        hdnCategoriaID.Value = "0"
        litModalTitulo.Text = "Nueva Categoría"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModalCategoria", "abrirModalCategoria();", True)
    End Sub

    Protected Sub gvCategorias_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            EditarCategoria(CInt(e.CommandArgument))
        ElseIf e.CommandName = "Eliminar" Then
            EliminarCategoria(CInt(e.CommandArgument))
        End If
    End Sub

    Private Sub EditarCategoria(id As Integer)
        Dim db As New Categoriadb()
        Dim dt As DataTable = db.ObtenerCategoriaPorID(id)
        If dt.Rows.Count = 0 Then Return

        hdnCategoriaID.Value = id.ToString()
        txtNombre.Text = dt.Rows(0)("Nombre").ToString()
        txtDescripcion.Text = dt.Rows(0)("Descripcion").ToString()
        litModalTitulo.Text = "Editar Categoría"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModalCategoria", "abrirModalCategoria();", True)
    End Sub

    Private Sub EliminarCategoria(id As Integer)

        Dim db As New Categoriadb()
        If db.EliminarCategoria(id) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ok", "Swal.fire('Correcto','Categoría eliminada','success');", True)
            CargarCategorias()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','No se puede eliminar','error');", True)
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If String.IsNullOrWhiteSpace(txtNombre.Text) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "val", "Swal.fire('Validación','El nombre es obligatorio','warning');", True)
            Return
        End If

        Dim db As New Categoriadb()
        Dim id As Integer = CInt(hdnCategoriaID.Value)
        Dim ok As Boolean = If(id = 0,
                           db.CrearCategoria(txtNombre.Text.Trim(), txtDescripcion.Text.Trim()),
                           db.ActualizarCategoria(id, txtNombre.Text.Trim(), txtDescripcion.Text.Trim()))

        If ok Then
            ' CAMBIO: Usar cerrarModalCategoria() como en GestionHerramientas
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ok", "cerrarModalCategoria(); Swal.fire('Éxito','Categoría guardada','success');", True)
            CargarCategorias()
        End If
    End Sub

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtDescripcion.Text = ""
    End Sub
End Class
