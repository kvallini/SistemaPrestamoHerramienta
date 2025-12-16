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

    ' Obtener herramienta por ID
    Public Function ObtenerHerramientaPorID(herramientaID As Integer) As Herramienta
        Try
            Dim sql As String = "SELECT h.HerramientaID, h.Nombre, h.Codigo, h.Descripcion, h.Estado, 
                                    h.Ubicacion, h.Disponible, c.Nombre as NombreCategoria, h.CategoriaID
                             FROM Herramientas h
                             LEFT JOIN Categorias c ON h.CategoriaID = c.CategoriaID
                             WHERE h.HerramientaID = @HerramientaID"

            Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@HerramientaID", herramientaID)
        }

            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, parametros)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Return New Herramienta() With {
                .HerramientaID = Convert.ToInt32(row("HerramientaID")),
                .Nombre = row("Nombre").ToString(),
                .Codigo = row("Codigo").ToString(),
                .Descripcion = row("Descripcion").ToString(),
                .Estado = row("Estado").ToString(),
                .Ubicacion = row("Ubicacion").ToString(),
                .Disponible = Convert.ToBoolean(row("Disponible")),
                .NombreCategoria = If(row("NombreCategoria") Is DBNull.Value, "Sin categoría", row("NombreCategoria").ToString()),
                .CategoriaID = If(row("CategoriaID") Is DBNull.Value, Nothing, Convert.ToInt32(row("CategoriaID")))
            }
            End If

        Catch ex As Exception

        End Try

        Return Nothing
    End Function

    ' Obtener herramientas (Admin)
    Public Function ObtenerTodasHerramientas() As List(Of Herramienta)
        Try
            Dim sql As String = "SELECT h.HerramientaID, h.Nombre, h.Codigo, h.Descripcion, h.Estado, 
                                    h.Disponible, c.Nombre as NombreCategoria, h.CategoriaID, h.Ubicacion
                             FROM Herramientas h
                             LEFT JOIN Categorias c ON h.CategoriaID = c.CategoriaID
                             ORDER BY h.Nombre"

            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, Nothing)
            Dim herramientas As New List(Of Herramienta)()

            For Each row As DataRow In dt.Rows
                herramientas.Add(New Herramienta() With {
                    .HerramientaID = Convert.ToInt32(row("HerramientaID")),
                    .Nombre = row("Nombre").ToString(),
                    .Codigo = row("Codigo").ToString(),
                    .Descripcion = row("Descripcion").ToString(),
                    .Estado = row("Estado").ToString(),
                    .Disponible = Convert.ToBoolean(row("Disponible")),
                    .NombreCategoria = If(row("NombreCategoria") Is DBNull.Value, "Sin categoría", row("NombreCategoria").ToString()),
                    .CategoriaID = If(row("CategoriaID") Is DBNull.Value, Nothing, Convert.ToInt32(row("CategoriaID"))),
                    .Ubicacion = row("Ubicacion").ToString()
                })
            Next

            Return herramientas

        Catch ex As Exception
            Return New List(Of Herramienta)()
        End Try
    End Function

    ' Nueva herramienta
    Public Function CrearHerramienta(herramienta As Herramienta) As Boolean
        Try
            Dim sql As String = "INSERT INTO Herramientas (Nombre, Codigo, Descripcion, Estado, Ubicacion, CategoriaID, Disponible)
                             VALUES (@Nombre, @Codigo, @Descripcion, @Estado, @Ubicacion, @CategoriaID, @Disponible)"

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Nombre", herramienta.Nombre),
                New SqlParameter("@Codigo", herramienta.Codigo),
                New SqlParameter("@Descripcion", If(String.IsNullOrEmpty(herramienta.Descripcion), DBNull.Value, herramienta.Descripcion)),
                New SqlParameter("@Estado", herramienta.Estado),
                New SqlParameter("@Ubicacion", If(String.IsNullOrEmpty(herramienta.Ubicacion), DBNull.Value, herramienta.Ubicacion)),
                New SqlParameter("@CategoriaID", If(herramienta.CategoriaID Is Nothing, DBNull.Value, herramienta.CategoriaID)),
                New SqlParameter("@Disponible", herramienta.Disponible)
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Actualizar herramienta
    Public Function ActualizarHerramienta(herramienta As Herramienta) As Boolean
        Try
            Dim sql As String = "UPDATE Herramientas SET Nombre = @Nombre, Codigo = @Codigo, Descripcion = @Descripcion,
                             Estado = @Estado, Ubicacion = @Ubicacion, CategoriaID = @CategoriaID, Disponible = @Disponible
                             WHERE HerramientaID = @HerramientaID"

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@HerramientaID", herramienta.HerramientaID),
                New SqlParameter("@Nombre", herramienta.Nombre),
                New SqlParameter("@Codigo", herramienta.Codigo),
                New SqlParameter("@Descripcion", If(String.IsNullOrEmpty(herramienta.Descripcion), DBNull.Value, herramienta.Descripcion)),
                New SqlParameter("@Estado", herramienta.Estado),
                New SqlParameter("@Ubicacion", If(String.IsNullOrEmpty(herramienta.Ubicacion), DBNull.Value, herramienta.Ubicacion)),
                New SqlParameter("@CategoriaID", If(herramienta.CategoriaID Is Nothing, DBNull.Value, herramienta.CategoriaID)),
                New SqlParameter("@Disponible", herramienta.Disponible)
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Eliminar herramienta
    Public Function EliminarHerramienta(herramientaID As Integer) As Boolean
        Try
            ' Verificar si se encuentra en préstamos activos
            Dim sqlVerificar As String = "SELECT COUNT(*) FROM Prestamos WHERE HerramientaID = @HerramientaID AND Estado IN ('Pendiente', 'Aprobado')"
            Dim parametrosVerificar As New List(Of SqlParameter) From {
                New SqlParameter("@HerramientaID", herramientaID)
            }

            Dim dt As DataTable = dbHelper.ExecuteQuery(sqlVerificar, parametrosVerificar)
            Dim tienePrestamos As Boolean = Convert.ToInt32(dt.Rows(0)(0)) > 0

            If tienePrestamos Then
                Return False ' No se puede eliminar si tiene préstamos activos
            End If

            ' Eliminar la herramienta
            Dim sqlEliminar As String = "DELETE FROM Herramientas WHERE HerramientaID = @HerramientaID"
            Dim parametrosEliminar As New List(Of SqlParameter) From {
                New SqlParameter("@HerramientaID", herramientaID)
            }

            dbHelper.ExecuteNonQuery(sqlEliminar, parametrosEliminar)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetAll() As List(Of Herramienta)
        Return ObtenerTodasHerramientas()
    End Function

    Public Function GetById(id As Integer) As Herramienta
        Return ObtenerHerramientaPorID(id)
    End Function

    Public Function Insert(herramienta As Herramienta) As Boolean
        Return CrearHerramienta(herramienta)
    End Function

    Public Function Update(herramienta As Herramienta) As Boolean
        Return ActualizarHerramienta(herramienta)
    End Function

    Public Function Delete(id As Integer) As Boolean
        Return EliminarHerramienta(id)
    End Function

    ' Estadísticas de herramientas por estado
    Public Function GetEstadisticasHerramientas() As DataTable
        Dim sql As String = "SELECT Estado, COUNT(*) AS Cantidad FROM Herramientas GROUP BY Estado"
        Return DbHelper.ExecuteQuery(sql)  ' Corregido: usa DbHelper directamente
    End Function

End Class