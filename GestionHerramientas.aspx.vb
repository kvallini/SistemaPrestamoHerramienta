Imports System.Data

Public Class GestionHerramientas
    Inherits System.Web.UI.Page

    Public Function EsAdmin() As Boolean
        Return Session("RolUsuario") IsNot Nothing AndAlso
               Session("RolUsuario").ToString().ToLower().Contains("admin")
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If
            CargarCategorias()
            CargarHerramientas()
        End If
    End Sub

    Private Sub CargarHerramientas()
        gvHerramientas.DataSource = New Herramientadb().ObtenerTodasHerramientas()
        gvHerramientas.DataBind()
    End Sub

    Private Sub CargarCategorias()
        Dim dt As DataTable = New Categoriadb().ObtenerTodasCategorias()
        ddlCategoria.Items.Clear()
        ddlCategoria.Items.Add(New ListItem("Sin categoría", "0"))

        For Each r As DataRow In dt.Rows
            ddlCategoria.Items.Add(New ListItem(r("Nombre").ToString(), r("CategoriaID").ToString()))
        Next
    End Sub

    Protected Sub btnNuevaHerramienta_Click(sender As Object, e As EventArgs) Handles btnNuevaHerramienta.Click
        LimpiarFormulario()
        hdnHerramientaID.Value = "0"
        litModalTitulo.Text = "Nueva Herramienta"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModal();", True)
    End Sub

    Protected Sub gvHerramientas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            EditarHerramienta(CInt(e.CommandArgument))
        ElseIf e.CommandName = "Eliminar" Then
            EliminarHerramienta(CInt(e.CommandArgument))
        End If
    End Sub

    Private Sub EditarHerramienta(id As Integer)
        Dim h = New Herramientadb().ObtenerHerramientaPorID(id)
        If h Is Nothing Then Return

        hdnHerramientaID.Value = id
        txtCodigo.Text = h.Codigo
        txtNombre.Text = h.Nombre
        txtDescripcion.Text = h.Descripcion
        ddlEstado.SelectedValue = h.Estado
        chkDisponible.Checked = h.Disponible
        ddlCategoria.SelectedValue = If(h.CategoriaID.HasValue, h.CategoriaID.ToString(), "0")
        txtUbicacion.Text = h.Ubicacion  ' Ya corregido

        litModalTitulo.Text = "Editar Herramienta"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModal();", True)
    End Sub

    Private Sub EliminarHerramienta(id As Integer)
        If Not EsAdmin() Then Return

        If New Herramientadb().EliminarHerramienta(id) Then
            ' CAMBIO: Usar Swal.fire() directo en lugar de MostrarAlerta
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ok", "Swal.fire('Correcto','Herramienta eliminada','success');", True)
            CargarHerramientas()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','No se pudo eliminar','error');", True)
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If String.IsNullOrWhiteSpace(txtCodigo.Text) Or String.IsNullOrWhiteSpace(txtNombre.Text) Then
            ' CAMBIO: Usar Swal.fire() para validación
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "val", "Swal.fire('Validación','Código y Nombre son obligatorios','warning');", True)
            Return
        End If

        Dim h As New Herramienta With {
        .HerramientaID = CInt(hdnHerramientaID.Value),
        .Codigo = txtCodigo.Text.Trim(),
        .Nombre = txtNombre.Text.Trim(),
        .Descripcion = txtDescripcion.Text.Trim(),
        .Estado = ddlEstado.SelectedValue,
        .Disponible = chkDisponible.Checked,
        .CategoriaID = If(ddlCategoria.SelectedValue = "0", Nothing, CInt(ddlCategoria.SelectedValue)),
        .Ubicacion = txtUbicacion.Text.Trim()
    }

        Dim db As New Herramientadb()
        Try
            Dim ok = If(h.HerramientaID = 0, db.CrearHerramienta(h), db.ActualizarHerramienta(h))
            If ok Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "cerrarOk", "cerrarModal(); Swal.fire('Éxito','Herramienta guardada','success');", True)
                CargarHerramientas()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','No se pudo guardar','error');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','Error al guardar: " & ex.Message.Replace("'", "\'") & "','error');", True)
        End Try
    End Sub

    Private Sub LimpiarFormulario()
        txtCodigo.Text = ""
        txtNombre.Text = ""
        txtUbicacion.Text = ""
        txtDescripcion.Text = ""
        ddlEstado.SelectedIndex = 0
        chkDisponible.Checked = True
        ddlCategoria.SelectedValue = "0"
    End Sub
End Class
