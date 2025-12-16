Public Class Herramienta
    Public Property HerramientaID As Integer
    Public Property Nombre As String
    Public Property Codigo As String
    Public Property Estado As String
    Public Property Ubicacion As String
    Public Property Descripcion As String
    Public Property FechaCreacion As DateTime
    Public Property CategoriaID As Integer?
    Public Property Disponible As Boolean
    Public Property NombreCategoria As String

    Public Sub New()
    End Sub

End Class