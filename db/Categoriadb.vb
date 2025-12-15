Imports System.Data.SqlClient

Public Class Categoriadb
    Private ReadOnly dbHelper As New DbHelper()

    Public Function ObtenerTodasCategorias() As DataTable
        Try
            Dim sql As String = "SELECT CategoriaID, Nombre, Descripcion FROM Categorias ORDER BY Nombre"
            Return DbHelper.ExecuteQuery(sql, Nothing)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    Public Function ObtenerCategoriaPorID(categoriaID As Integer) As DataTable
        Try
            Dim sql As String = "SELECT CategoriaID, Nombre, Descripcion FROM Categorias WHERE CategoriaID = @CategoriaID"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@CategoriaID", categoriaID)
            }
            Return DbHelper.ExecuteQuery(sql, parametros)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    Public Function CrearCategoria(nombre As String, descripcion As String) As Boolean
        Try
            Dim sql As String = "INSERT INTO Categorias (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Nombre", nombre),
                New SqlParameter("@Descripcion", If(String.IsNullOrEmpty(descripcion), DBNull.Value, descripcion))
            }
            DbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ActualizarCategoria(categoriaID As Integer, nombre As String, descripcion As String) As Boolean
        Try
            Dim sql As String = "UPDATE Categorias SET Nombre = @Nombre, Descripcion = @Descripcion WHERE CategoriaID = @CategoriaID"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@CategoriaID", categoriaID),
                New SqlParameter("@Nombre", nombre),
                New SqlParameter("@Descripcion", If(String.IsNullOrEmpty(descripcion), DBNull.Value, descripcion))
            }
            DbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EliminarCategoria(categoriaID As Integer) As Boolean
        Try
            ' Verificar que no tenga herramientas asociadas
            Dim sqlVerificar As String = "SELECT COUNT(*) FROM Herramientas WHERE CategoriaID = @CategoriaID"
            Dim parametrosVerificar As New List(Of SqlParameter) From {
                New SqlParameter("@CategoriaID", categoriaID)
            }

            Dim dt As DataTable = DbHelper.ExecuteQuery(sqlVerificar, parametrosVerificar)
            Dim tieneHerramientas As Boolean = Convert.ToInt32(dt.Rows(0)(0)) > 0

            If tieneHerramientas Then
                Return False ' No se puede eliminar si tiene herramientas asociadas
            End If

            Dim sqlEliminar As String = "DELETE FROM Categorias WHERE CategoriaID = @CategoriaID"
            Dim parametrosEliminar As New List(Of SqlParameter) From {
                New SqlParameter("@CategoriaID", categoriaID)
            }
            DbHelper.ExecuteNonQuery(sqlEliminar, parametrosEliminar)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function
End Class