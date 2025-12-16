Imports System.Data

Public Class GestionUsuarios
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
            CargarRoles()
            CargarUsuarios()
        End If
    End Sub

    Private Sub CargarUsuarios()
        gvUsuarios.DataSource = New UsuarioDB().GetAll()  ' CAMBIO: Usa GetAll()
        gvUsuarios.DataBind()
    End Sub

    Private Sub CargarRoles()
        Dim dt As DataTable = New UsuarioDB().GetAllRoles()  ' CAMBIO: Nuevo método
        ddlRol.Items.Clear()
        ddlRol.Items.Add(New ListItem("Selecciona un rol", "0"))

        For Each r As DataRow In dt.Rows
            ddlRol.Items.Add(New ListItem(r("NombreRol").ToString(), r("RolID").ToString()))
        Next
    End Sub

    Protected Sub btnNuevoUsuario_Click(sender As Object, e As EventArgs) Handles btnNuevoUsuario.Click
        If Not EsAdmin() Then Return

        LimpiarFormulario()
        hdnUsuarioID.Value = "0"
        litModalTitulo.Text = "Nuevo Usuario"
        txtContrasena.Visible = True

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModal();", True)
    End Sub

    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUsuarios.RowCommand
        If e.CommandName = "Editar" Then
            EditarUsuario(CInt(e.CommandArgument))
        ElseIf e.CommandName = "Eliminar" Then
            EliminarUsuario(CInt(e.CommandArgument))
        End If
    End Sub
    Private Sub EditarUsuario(id As Integer)
        If Not EsAdmin() Then Return

        Dim dt As DataTable = New UsuarioDB().GetById(id)
        If dt.Rows.Count = 0 Then Return

        Dim u As New Usuario With {
            .UsuarioID = CInt(dt.Rows(0)("UsuarioID")),
            .Nombre = dt.Rows(0)("Nombre").ToString(),
            .Email = dt.Rows(0)("Email").ToString(),
            .Departamento = dt.Rows(0)("Departamento").ToString(),
            .Rol = dt.Rows(0)("Rol").ToString(),
            .RolID = CInt(dt.Rows(0)("RolID")),
            .Activo = CBool(dt.Rows(0)("Activo"))
        }

        hdnUsuarioID.Value = id
        txtNombre.Text = u.Nombre
        txtEmail.Text = u.Email
        txtDepartamento.Text = u.Departamento
        ddlRol.SelectedValue = u.RolID.ToString()
        chkActivo.Checked = u.Activo
        txtContrasena.Visible = False

        litModalTitulo.Text = "Editar Usuario"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrir", "abrirModal();", True)
    End Sub

    Private Sub EliminarUsuario(id As Integer)
        If Not EsAdmin() Then Return

        Dim result As String = New UsuarioDB().Delete(id)
        If result.Contains("correctamente") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ok", "Swal.fire('Correcto','Usuario eliminado','success');", True)
            CargarUsuarios()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','" & result & "','error');", True)
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Not EsAdmin() Then Return

        If String.IsNullOrWhiteSpace(txtNombre.Text) Or String.IsNullOrWhiteSpace(txtEmail.Text) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "val", "Swal.fire('Validación','Nombre y Email son obligatorios','warning');", True)
            Return
        End If

        Dim u As New Usuario With {
            .UsuarioID = CInt(hdnUsuarioID.Value),
            .Nombre = txtNombre.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .Departamento = txtDepartamento.Text.Trim(),
            .RolID = CInt(ddlRol.SelectedValue),
            .Activo = chkActivo.Checked,
            .FechaRegistro = DateTime.Now
        }

        Dim db As New UsuarioDB()
        Dim result As String
        If u.UsuarioID = 0 Then
            If String.IsNullOrWhiteSpace(txtContrasena.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "val", "Swal.fire('Validación','Contraseña es obligatoria para nuevos usuarios','warning');", True)
                Return
            End If
            u.Contrasena = txtContrasena.Text.Trim()
            u.Rol = ddlRol.SelectedItem.Text
            result = db.Insert(u)
        Else
            u.Rol = ddlRol.SelectedItem.Text
            result = db.Update(u)
        End If

        If result.Contains("correctamente") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "cerrarOk", "cerrarModal(); Swal.fire('Éxito','Usuario guardado','success');", True)
            CargarUsuarios()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "Swal.fire('Error','" & result & "','error');", True)
        End If
    End Sub

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtEmail.Text = ""
        txtDepartamento.Text = ""
        ddlRol.SelectedIndex = 0
        chkActivo.Checked = True
        txtContrasena.Text = ""
    End Sub
End Class