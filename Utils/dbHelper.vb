Imports System.Data
Imports System.Data.SqlClient

Public Class DbHelper
    Private connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("HerramientasDB").ConnectionString
    End Sub

    Public Function ExecuteQuery(query As String, parameters As List(Of SqlParameter)) As DataTable
        Dim dataTable As New DataTable()

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                If parameters IsNot Nothing Then
                    For Each param As SqlParameter In parameters
                        command.Parameters.Add(param)
                    Next
                End If

                connection.Open()
                Using adapter As New SqlDataAdapter(command)
                    adapter.Fill(dataTable)
                End Using
            End Using
        End Using

        Return dataTable
    End Function

    Public Function ExecuteNonQuery(query As String, parameters As List(Of SqlParameter)) As Integer
        Dim rowsAffected As Integer = 0

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                If parameters IsNot Nothing Then
                    For Each param As SqlParameter In parameters
                        command.Parameters.Add(param)
                    Next
                End If

                connection.Open()
                rowsAffected = command.ExecuteNonQuery()
            End Using
        End Using

        Return rowsAffected
    End Function

    Public Function ExecuteScalar(query As String, parameters As List(Of SqlParameter)) As Object
        Dim result As Object = Nothing

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                If parameters IsNot Nothing Then
                    For Each param As SqlParameter In parameters
                        command.Parameters.Add(param)
                    Next
                End If

                connection.Open()
                result = command.ExecuteScalar()
            End Using
        End Using

        Return result
    End Function
End Class