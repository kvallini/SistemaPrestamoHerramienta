Public Class Prestamo
    Public Property PrestamoID As Integer
    Public Property HerramientaID As Integer
    Public Property UsuarioID As Integer
    Public Property FechaPrestamo As DateTime?
    Public Property FechaDevolucionPrevista As DateTime
    Public Property FechaDevolucionReal As DateTime?
    Public Property Estado As String
    Public Property Observaciones As String
    Public Property MotivoRechazo As String
    Public Property FechaSolicitud As DateTime
    Public Property FechaAprobacion As DateTime?
    Public Property ComentariosSolicitud As String

    ' Propiedades para mostrar
    Public Property NombreHerramienta As String
    Public Property NombreUsuario As String
    Public Property CodigoHerramienta As String

    Public Sub New()
    End Sub
End Class