
' Clase UsuarioDB, maneja todas las operaciones CRUD de la tabla Usuarios.

Imports System.Data
Imports System.Data.SqlClient
Imports Sistema_Herramienta.Models

Public Class UsuarioDB

    ' Instancia del helper de base de datos
    Private ReadOnly db As New DbHelper()

    ' Obtener TODOS los usuarios registrados en la tabla.
    Public Function GetAll() As DataTable

        Dim sql As String =
            "SELECT UsuarioID, Nombre, Email, Departamento, FechaRegistro, Activo, Rol 
             FROM Usuarios"

        Return db.ExecuteQuery(sql)

    End Function


    ' Buscar un usuario por su ID.
    Public Function GetById(id As Integer) As DataTable

        Dim sql As String = "SELECT * FROM Usuarios WHERE UsuarioID = @id"

        Dim parametros = New List(Of SqlParameter) From {
            New SqlParameter("@id", id)
        }

        Return db.ExecuteQuery(sql, parametros)

    End Function


    ' Insertar un nuevo usuario en la base de datos.
    Public Function Insert(u As Usuario) As String

        Dim sql As String =
            "INSERT INTO Usuarios (Nombre, Email, Departamento, FechaRegistro, Activo, Contrasena, Rol) 
             VALUES (@Nombre, @Email, @Departamento, @FechaRegistro, @Activo, @Contrasena, @Rol)"

        ' Lista de parámetros que van a la consulta
        Dim parametros = New List(Of SqlParameter) From {
            New SqlParameter("@Nombre", u.Nombre),
            New SqlParameter("@Email", u.Email),
            New SqlParameter("@Departamento", u.Departamento),
            New SqlParameter("@FechaRegistro", u.FechaRegistro),
            New SqlParameter("@Activo", u.Activo),
            New SqlParameter("@Contrasena", u.Contrasena),
            New SqlParameter("@Rol", u.Rol)
        }

        Try
            db.ExecuteNonQuery(sql, parametros)
            Return "Usuario creado correctamente"
        Catch ex As Exception
            Return "Error al insertar usuario: " & ex.Message
        End Try

    End Function

    'Actualizar los datos de un usuario existente.
    ' u = objeto Usuario con la información nueva.
    Public Function Update(u As Usuario) As String

        Dim sql As String =
            "UPDATE Usuarios SET 
                Nombre = @Nombre,
                Email = @Email,
                Departamento = @Departamento,
                Activo = @Activo,
                Contrasena = @Contrasena,
                Rol = @Rol
             WHERE UsuarioID = @UsuarioID"

        Dim parametros = New List(Of SqlParameter) From {
            New SqlParameter("@Nombre", u.Nombre),
            New SqlParameter("@Email", u.Email),
            New SqlParameter("@Departamento", u.Departamento),
            New SqlParameter("@Activo", u.Activo),
            New SqlParameter("@Contrasena", u.Contrasena),
            New SqlParameter("@Rol", u.Rol),
            New SqlParameter("@UsuarioID", u.UsuarioID)
        }

        Try
            db.ExecuteNonQuery(sql, parametros)
            Return "Usuario actualizado correctamente"
        Catch ex As Exception
            Return "Error al actualizar usuario: " & ex.Message
        End Try

    End Function

    ' Eliminar un usuario por su ID.
    Public Function Delete(id As Integer) As String

        Dim sql As String = "DELETE FROM Usuarios WHERE UsuarioID = @id"

        Dim parametros = New List(Of SqlParameter) From {
            New SqlParameter("@id", id)
        }

        Try
            db.ExecuteNonQuery(sql, parametros)
            Return "Usuario eliminado correctamente"
        Catch ex As Exception
            Return "Error al eliminar usuario: " & ex.Message
        End Try

    End Function

    ' OBJETIVO: Validar correo y contraseña.
    Public Function ValidateCredentials(email As String, password As String) As DataTable

        Dim sql As String =
            "SELECT UsuarioID, Nombre, Rol, Activo 
             FROM Usuarios 
             WHERE Email = @Email AND Contrasena = @Contrasena"

        Dim parametros = New List(Of SqlParameter) From {
            New SqlParameter("@Email", email),
            New SqlParameter("@Contrasena", password)
        }

        Return db.ExecuteQuery(sql, parametros)

    End Function

End Class
