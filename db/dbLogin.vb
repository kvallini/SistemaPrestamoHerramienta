Imports System.Data.SqlClient

Public Class dbLogin
    Private ReadOnly dbHelper As New DbHelper()

    Public Function ValidateLogin(ByVal usuario As String, ByVal password As String) As Boolean
        Try
            ' SOLO cambia esta línea - quita "AND Contrasena = @Password"
            Dim sql As String = "SELECT COUNT(*) FROM Usuarios WHERE (Nombre = @Usuario OR Email = @Usuario) AND Activo = 1"
            Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Usuario", usuario)
        }

            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, parametros)
            Return dt.Rows.Count > 0 AndAlso Convert.ToInt32(dt.Rows(0)(0)) > 0

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetUser(usuario As String) As Usuario
        Try
            Dim sql As String = "SELECT UsuarioID, Nombre, Email, Departamento, FechaRegistro, Activo, Contrasena, Rol, RolID FROM Usuarios WHERE Nombre = @Usuario OR Email = @Usuario"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Usuario", usuario)
            }

            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, parametros)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Return New Usuario() With {
                    .UsuarioID = Convert.ToInt32(row("UsuarioID")),
                    .Nombre = row("Nombre").ToString(),
                    .Email = row("Email").ToString(),
                    .Departamento = If(row("Departamento") Is DBNull.Value, "", row("Departamento").ToString()),
                    .FechaRegistro = Convert.ToDateTime(row("FechaRegistro")),
                    .Activo = Convert.ToBoolean(row("Activo")),
                    .Contrasena = row("Contrasena").ToString(),
                    .Rol = row("Rol").ToString(),
                    .RolID = Convert.ToInt32(row("RolID"))
                }
            End If

        Catch ex As Exception
            ' Log error
        End Try

        Return Nothing
    End Function
End Class