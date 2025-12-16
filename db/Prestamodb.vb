Imports System.Data.SqlClient

Public Class Prestamodb
    Private ReadOnly dbHelper As New DbHelper()

    Public Function CrearSolicitudPrestamo(usuarioID As Integer, herramientaID As Integer, comentarios As String) As Boolean
        Try
            Dim sql As String = "INSERT INTO Prestamos (UsuarioID, HerramientaID, FechaSolicitud, Estado, ComentariosSolicitud, FechaDevolucionPrevista) 
                                 VALUES (@UsuarioID, @HerramientaID, GETDATE(), 'Pendiente', @Comentarios, DATEADD(day, 7, GETDATE()))"

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@UsuarioID", usuarioID),
                New SqlParameter("@HerramientaID", herramientaID),
                New SqlParameter("@Comentarios", If(String.IsNullOrEmpty(comentarios), DBNull.Value, comentarios))
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"ERROR SQL: {ex.Message}")
            Return False
        End Try
    End Function

    Public Function ObtenerSolicitudesPorUsuario(usuarioID As Integer) As DataTable
        Try
            Dim sql As String = "SELECT p.PrestamoID, h.Nombre as Herramienta, p.FechaSolicitud, p.Estado, p.ComentariosSolicitud as Comentarios,
                                        c.Nombre as Categoria, p.FechaAprobacion, p.MotivoRechazo, p.FechaDevolucionPrevista
                                 FROM Prestamos p
                                 INNER JOIN Herramientas h ON p.HerramientaID = h.HerramientaID
                                 LEFT JOIN Categorias c ON h.CategoriaID = c.CategoriaID
                                 WHERE p.UsuarioID = @UsuarioID
                                 ORDER BY p.FechaSolicitud DESC"

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@UsuarioID", usuarioID)
            }

            Return dbHelper.ExecuteQuery(sql, parametros)

        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    Public Function ObtenerSolicitudesPendientes() As DataTable
        Try
            Dim sql As String = "SELECT p.PrestamoID, u.Nombre as Solicitante, h.Nombre as Herramienta, 
                                    c.Nombre as Categoria, p.FechaSolicitud, p.ComentariosSolicitud,
                                    p.FechaDevolucionPrevista
                             FROM Prestamos p
                             INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID
                             INNER JOIN Herramientas h ON p.HerramientaID = h.HerramientaID
                             LEFT JOIN Categorias c ON h.CategoriaID = c.CategoriaID
                             WHERE p.Estado = 'Pendiente'
                             ORDER BY p.FechaSolicitud ASC"

            Return dbHelper.ExecuteQuery(sql, Nothing)

        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    Public Function AprobarSolicitud(prestamoID As Integer) As Boolean
        Try
            Dim sql As String = "UPDATE Prestamos SET Estado = 'Aprobado', FechaAprobacion = GETDATE() 
                             WHERE PrestamoID = @PrestamoID"

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@PrestamoID", prestamoID)
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function RechazarSolicitud(prestamoID As Integer, motivo As String) As Boolean
        Try
            Dim sql As String = "UPDATE Prestamos SET Estado = 'Rechazado', MotivoRechazo = @Motivo, 
                             FechaAprobacion = GETDATE() 
                             WHERE PrestamoID = @PrestamoID"

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@PrestamoID", prestamoID),
                New SqlParameter("@Motivo", motivo)
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    'Obtener Prestamos
    Public Function GetAll() As DataTable
        Try
            Dim sql As String = "SELECT p.PrestamoID, p.UsuarioID, h.Nombre AS NombreHerramienta, u.Nombre AS NombreUsuario, p.FechaPrestamo, p.FechaDevolucionPrevista, p.FechaDevolucionReal, p.Estado FROM Prestamos p INNER JOIN Herramientas h ON p.HerramientaID = h.HerramientaID INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID ORDER BY p.FechaPrestamo DESC"
            Return DbHelper.ExecuteQuery(sql, Nothing)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    ' Actualizar préstamo
    Public Function Update(prestamoID As Integer, fechaDevolucion As DateTime, estado As String, observaciones As String) As Boolean
        Try
            Dim sql As String = "UPDATE Prestamos SET FechaDevolucionReal = @Fecha, Estado = @Estado, Observaciones = @Obs WHERE PrestamoID = @ID"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Fecha", fechaDevolucion),
                New SqlParameter("@Estado", estado),
                New SqlParameter("@Obs", observaciones),
                New SqlParameter("@ID", prestamoID)
            }
            DbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Estadísticas de préstamos por mes
    Public Function GetEstadisticasPrestamosPorMes() As DataTable
        Dim sql As String = "SELECT FORMAT(FechaPrestamo, 'yyyy-MM') AS Mes, COUNT(*) AS Cantidad FROM Prestamos WHERE FechaPrestamo >= DATEADD(MONTH, -12, GETDATE()) GROUP BY FORMAT(FechaPrestamo, 'yyyy-MM') ORDER BY Mes DESC"
        Return DbHelper.ExecuteQuery(sql, Nothing)
    End Function

End Class