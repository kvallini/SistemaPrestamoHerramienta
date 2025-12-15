Public Class GestionCategorias
    Inherits System.Web.UI.Page

    Public ReadOnly Property EsAdmin As Boolean
        Get
            If Session("RolUsuario") IsNot Nothing Then
                Return Session("RolUsuario").ToString().ToLower().Contains("admin")
            End If
            Return False
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("NombreUsuario") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If

            ' Verificar que sea Admin o JefeBodega
            Dim rol As String = Session("RolUsuario").ToString().ToLower()
            If Not rol.Contains("admin") AndAlso Not rol.Contains("jefe") Then
                Response.Redirect("Login.aspx")
                Return
            End If

            CargarCategorias()
        End If
    End Sub

    Private Sub CargarCategorias()
        Dim categoriaDB As New Categoriadb()
        gvCategorias.DataSource = categoriaDB.ObtenerTodasCategorias()
        gvCategorias.DataBind()
    End Sub

    Protected Sub btnNuevaCategoria_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        litModalTitulo.Text = "Nueva Categoría"
        hdnCategoriaID.Value = "0"
        ClientScript.RegisterStartupScript(Me.GetType(), "abrirModal", "abrirModalCategoria();", True)
    End Sub

    Protected Sub gvCategorias_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            Dim categoriaID As Integer = Convert.ToInt32(e.CommandArgument)
            EditarCategoria(categoriaID)
        ElseIf e.CommandName = "Eliminar" Then
            Dim categoriaID As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarCategoria(categoriaID)
        End If
    End Sub

    Private Sub EditarCategoria(categoriaID As Integer)
        Dim categoriaDB As New Categoriadb()
        Dim dt As DataTable = categoriaDB.ObtenerCategoriaPorID(categoriaID)

        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            hdnCategoriaID.Value = categoriaID.ToString()
            txtNombre.Text = row("Nombre").ToString()
            txtDescripcion.Text = row("Descripcion").ToString()

            litModalTitulo.Text = "Editar Categoría"
            ClientScript.RegisterStartupScript(Me.GetType(), "abrirModal", "abrirModalCategoria();", True)
        End If
    End Sub

    Private Sub EliminarCategoria(categoriaID As Integer)
        Dim categoriaDB As New Categoriadb()
        If categoriaDB.EliminarCategoria(categoriaID) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "success", "alert('Categoría eliminada correctamente');", True)
            CargarCategorias()
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('No se puede eliminar la categoría. Puede tener herramientas asociadas.');", True)
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        If ValidarFormulario() Then
            Dim categoriaID As Integer = Convert.ToInt32(hdnCategoriaID.Value)
            Dim nombre As String = txtNombre.Text.Trim()
            Dim descripcion As String = txtDescripcion.Text.Trim()

            Dim categoriaDB As New Categoriadb()
            Dim resultado As Boolean = False

            If categoriaID = 0 Then
                resultado = categoriaDB.CrearCategoria(nombre, descripcion)
            Else
                resultado = categoriaDB.ActualizarCategoria(categoriaID, nombre, descripcion)
            End If

            If resultado Then
                ClientScript.RegisterStartupScript(Me.GetType(), "cerrarModal", "$('#modalCategoria').modal('hide');", True)
                ClientScript.RegisterStartupScript(Me.GetType(), "success", "alert('Categoría guardada correctamente');", True)
                CargarCategorias()
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('Error al guardar la categoría');", True)
            End If
        End If
    End Sub

    Private Function ValidarFormulario() As Boolean
        If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "error", "alert('El nombre es obligatorio');", True)
            Return False
        End If
        Return True
    End Function

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtDescripcion.Text = ""
    End Sub
End Class