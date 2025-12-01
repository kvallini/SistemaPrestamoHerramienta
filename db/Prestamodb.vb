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

End Class