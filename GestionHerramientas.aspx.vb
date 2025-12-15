Public Class GestionHerramientas
    Inherits System.Web.UI.Page

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
            CargarHerramientas()

        End If
    End Sub

    ' Función si es Admin
    Public Function EsAdmin() As Boolean
        If Session("RolUsuario") IsNot Nothing Then
            Return Session("RolUsuario").ToString().ToLower().Contains("admin")
        End If
        Return False
    End Function

    Private Sub CargarHerramientas()
        Dim herramientaDB As New Herramientadb()
        gvHerramientas.DataSource = herramientaDB.ObtenerTodasHerramientas()
        gvHerramientas.DataBind()
    End Sub

    Private Sub CargarCategorias()
        Dim categoriaDB As New Categoriadb()
        Dim categorias As DataTable = categoriaDB.ObtenerTodasCategorias()

        ddlCategoria.Items.Clear()
        ddlCategoria.Items.Add(New ListItem("Seleccionar categoría", ""))
        ddlCategoria.Items.Add(New ListItem("Sin categoría", "0"))

        For Each row As DataRow In categorias.Rows
            ddlCategoria.Items.Add(New ListItem(row("Nombre").ToString(), row("CategoriaID").ToString()))
        Next
    End Sub

    Protected Sub btnNuevaHerramienta_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        litModalTitulo.Text = "Nueva Herramienta"
        hdnHerramientaID.Value = "0"
        ClientScript.RegisterStartupScript(Me.GetType(), "abrirModal", "abrirModal();", True)
    End Sub

    Protected Sub gvHerramientas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            Dim herramientaID As Integer = Convert.ToInt32(e.CommandArgument)
            EditarHerramienta(herramientaID)
        ElseIf e.CommandName = "Eliminar" Then
            Dim herramientaID As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarHerramienta(herramientaID)
        End If
    End Sub

    Private Sub EditarHerramienta(herramientaID As Integer)
        Try
            System.Diagnostics.Debug.WriteLine($"EDITAR iniciado - HerramientaID: {herramientaID}")

            Dim herramientaDB As New Herramientadb()
            Dim herramienta As Herramienta = herramientaDB.ObtenerHerramientaPorID(herramientaID)

            If herramienta IsNot Nothing Then
                System.Diagnostics.Debug.WriteLine($"Herramienta encontrada: {herramienta.Nombre}")

                hdnHerramientaID.Value = herramientaID.ToString()
                txtCodigo.Text = herramienta.Codigo
                txtNombre.Text = herramienta.Nombre
                txtDescripcion.Text = herramienta.Descripcion
                ddlEstado.SelectedValue = herramienta.Estado
                txtUbicacion.Text = herramienta.Ubicacion
                chkDisponible.Checked = herramienta.Disponible

                ' DEBUG: Ver categoría
                System.Diagnostics.Debug.WriteLine($"CategoriaID: {If(herramienta.CategoriaID Is Nothing, "NULL", herramienta.CategoriaID)}")

                If herramienta.CategoriaID IsNot Nothing AndAlso herramienta.CategoriaID > 0 Then
                    Dim item As ListItem = ddlCategoria.Items.FindByValue(herramienta.CategoriaID.ToString())
                    If item IsNot Nothing Then
                        ddlCategoria.SelectedValue = herramienta.CategoriaID.ToString()
                        System.Diagnostics.Debug.WriteLine($"Categoría seleccionada: {item.Text}")
                    Else
                        ddlCategoria.SelectedIndex = 0
                        System.Diagnostics.Debug.WriteLine($"Categoría NO encontrada en lista")
                    End If
                Else
                    ddlCategoria.SelectedValue = "0"
                    System.Diagnostics.Debug.WriteLine($"Categoría NULL o 0, seleccionando '0'")
                End If

                litModalTitulo.Text = "Editar Herramienta"

                ' DEBUG: Verificar script
                System.Diagnostics.Debug.WriteLine("Intentando abrir modal...")

                ' Prueba diferentes formas:
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal",
                "console.log('Script RegisterStartupScript ejecutado'); 
                 setTimeout(function() { 
                     console.log('Abriendo modal...'); 
                     $('#modalHerramienta').modal('show'); 
                 }, 100);", True)

            Else
                System.Diagnostics.Debug.WriteLine("Herramienta NO encontrada")
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"ERROR en EditarHerramienta: {ex.Message}")
            System.Diagnostics.Debug.WriteLine($"Stack: {ex.StackTrace}")
        End Try
    End Sub

    Private Sub EliminarHerramienta(herramientaID As Integer)
        Dim herramientaDB As New Herramientadb()
        If herramientaDB.EliminarHerramienta(herramientaID) Then
            CType(Me.Master, Site).MostrarAlerta("¡Correcto!", "Herramienta eliminada correctamente", "success")
            CargarHerramientas()
        Else
            CType(Me.Master, Site).MostrarAlerta("Error", "No se puede eliminar la herramienta. Puede tener préstamos asociados.", "error")
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        If ValidarFormulario() Then
            Dim categoriaID As Integer? = Nothing

            ' ✅ CORREGIDO: Solo asignar si NO es "0" o vacío
            If Not String.IsNullOrEmpty(ddlCategoria.SelectedValue) AndAlso ddlCategoria.SelectedValue <> "0" Then
                categoriaID = Convert.ToInt32(ddlCategoria.SelectedValue)
            End If

            Dim herramienta As New Herramienta() With {
            .HerramientaID = Convert.ToInt32(hdnHerramientaID.Value),
            .Codigo = txtCodigo.Text.Trim(),
            .Nombre = txtNombre.Text.Trim(),
            .Descripcion = txtDescripcion.Text.Trim(),
            .Estado = ddlEstado.SelectedValue,
            .Ubicacion = txtUbicacion.Text.Trim(),
            .Disponible = chkDisponible.Checked,
            .CategoriaID = categoriaID ' ✅ AHORA SÍ CON CATEGORÍA
        }

            Dim herramientaDB As New Herramientadb()
            Dim resultado As Boolean = False

            If herramienta.HerramientaID = 0 Then
                resultado = herramientaDB.CrearHerramienta(herramienta)
            Else
                resultado = herramientaDB.ActualizarHerramienta(herramienta)
            End If

            If resultado Then
                ClientScript.RegisterStartupScript(Me.GetType(), "cerrarModal", "$('#modalHerramienta').modal('hide');", True)
                CType(Me.Master, Site).MostrarAlerta("¡Éxito!", "Herramienta guardada correctamente", "success")
                CargarHerramientas()
            Else
                CType(Me.Master, Site).MostrarAlerta("Error", "Error al guardar la herramienta", "error")
            End If
        End If
    End Sub

    Private Function ValidarFormulario() As Boolean
        If String.IsNullOrEmpty(txtCodigo.Text.Trim()) Then
            CType(Me.Master, Site).MostrarAlerta("Error", "El código es obligatorio", "error")
            Return False
        End If

        If String.IsNullOrEmpty(txtNombre.Text.Trim()) Then
            CType(Me.Master, Site).MostrarAlerta("Error", "El nombre es obligatorio", "error")
            Return False
        End If

        Return True
    End Function

    ' Cambio de color del badge según estado y disponibilidad
    Protected Sub gvHerramientas_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblEstado As Label = CType(e.Row.FindControl("lblEstado"), Label)
            Dim disponible As Boolean = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Disponible"))

            If lblEstado IsNot Nothing Then
                ' Combinar estado y disponibilidad en un solo badge
                Dim estadoTexto As String = lblEstado.Text
                Dim cssClass As String = ""

                Select Case estadoTexto.ToLower()
                    Case "disponible"
                        If disponible Then
                            cssClass = "badge bg-success"
                            lblEstado.Text = "Disponible"
                        Else
                            cssClass = "badge bg-warning"
                            lblEstado.Text = "En Préstamo"
                        End If
                    Case "en mantenimiento"
                        cssClass = "badge bg-warning"
                        lblEstado.Text = "En Mantenimiento"
                    Case "dañada"
                        cssClass = "badge bg-danger"
                        lblEstado.Text = "Dañada"
                    Case "retirada"
                        cssClass = "badge bg-secondary"
                        lblEstado.Text = "Retirada"
                    Case Else
                        cssClass = "badge bg-secondary"
                End Select

                lblEstado.CssClass = cssClass
            End If
        End If
    End Sub

    Private Sub LimpiarFormulario()
        txtCodigo.Text = ""
        txtNombre.Text = ""
        txtDescripcion.Text = ""
        txtUbicacion.Text = ""
        ddlEstado.SelectedValue = "Disponible"
        chkDisponible.Checked = True
        ddlCategoria.SelectedValue = "0"

    End Sub
End Class