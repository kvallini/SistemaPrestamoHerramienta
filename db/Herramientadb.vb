Imports System.Data.SqlClient

Public Class Herramientadb
    Private ReadOnly dbHelper As New DbHelper()

    Public Function ObtenerHerramientasDisponibles() As List(Of Herramienta)
        Try
            Dim sql As String = "SELECT h.HerramientaID, h.Nombre, h.Descripcion, h.Estado, h.Disponible, 
                                        c.Nombre as NombreCategoria, h.CategoriaID, h.Codigo
                                 FROM Herramientas h
                                 LEFT JOIN Categorias c ON h.CategoriaID = c.CategoriaID
                                 WHERE h.Disponible = 1 AND h.Estado = 'Disponible'"

            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, Nothing)
            Dim herramientas As New List(Of Herramienta)()

            For Each row As DataRow In dt.Rows
                herramientas.Add(New Herramienta() With {
                    .HerramientaID = Convert.ToInt32(row("HerramientaID")),
                    .Nombre = row("Nombre").ToString(),
                    .Descripcion = row("Descripcion").ToString(),
                    .Estado = row("Estado").ToString(),
                    .Disponible = Convert.ToBoolean(row("Disponible")),
                    .NombreCategoria = If(row("NombreCategoria") Is DBNull.Value, "Sin categoría", row("NombreCategoria").ToString()),
                    .CategoriaID = If(row("CategoriaID") Is DBNull.Value, Nothing, Convert.ToInt32(row("CategoriaID"))),
                    .Codigo = row("Codigo").ToString()
                })
            Next

            Return herramientas

        Catch ex As Exception
            Return New List(Of Herramienta)()
        End Try
    End Function
End Class