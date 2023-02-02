Imports System.Data.SqlClient

Public Class SqlEvents

    Private ConnectionString As String = "Data Source={0};Database={1};User ID={2};Password={3};Connect Timeout={4};"
    Property IsConnection As Boolean = False

    Sub New(ByVal source As String, ByVal database As String, ByVal user As String, ByVal password As String, Optional ByVal timeout As Integer = 30)
        ConnectionString = String.Format(ConnectionString, source, database, user, password, timeout)
        TestConnection()
    End Sub

    Private Sub TestConnection()
        Try
            Execute("SELECT * FROM INFORMATION_SCHEMA.TABLES", 0)
            IsConnection = True
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf &
                            "Событие: " & Reflection.MethodBase.GetCurrentMethod().Name & vbCrLf &
                            "Время: " & Now.ToLocalTime,
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    ''' <summary>
    ''' Подключение и выполнение запроса к БД
    ''' </summary>
    ''' <param name="query">SQL запрос</param>
    ''' <param name="type">Тип запроса(0 - ExecuteNonQuery, 1 - Scalar, 2 - DataTable, 3 - BulkCopy)</param>
    ''' <param name="dataTableBulk">Датасет с данными для записи в таблицу</param>
    ''' <param name="destinationTableName">Имя исходной таблицы для записи</param>
    ''' <param name="batchSizeBulkCopy">Кол-во строк в пакете, по умолчанию 1000</param>
    ''' <param name="bulkCopyTimeout">Таймаут для завершения операции, по умолчанию 60 сек</param>
    ''' <returns>Возвращает объект (строка, датасет, булевая)</returns>
    Public Function Execute(ByVal query As String, ByVal type As Integer, Optional ByVal dataTableBulk As DataTable = Nothing, Optional ByVal destinationTableName As String = "", Optional ByVal batchSizeBulkCopy As Integer = 1000, Optional ByVal bulkCopyTimeout As Integer = 60) As Object
        Select Case type
            Case 0
                Using connect = New SqlConnection(ConnectionString)
                    connect.Open()
                    Using command As New SqlCommand(query, connect)
                        Return command.ExecuteNonQuery()
                    End Using
                End Using
            Case 1
                Using connect = New SqlConnection(ConnectionString)
                    connect.Open()
                    Using command As New SqlCommand(query, connect)
                        Return command.ExecuteScalar
                    End Using
                End Using
            Case 2
                Using connect = New SqlConnection(ConnectionString)
                    connect.Open()
                    Dim dataTable As DataTable = New DataTable
                    Using command As New SqlCommand(query, connect)
                        command.ExecuteNonQuery()
                        Dim dataAdapter As SqlDataAdapter = New SqlDataAdapter(command)
                        dataAdapter.Fill(dataTable)
                    End Using
                    Return dataTable
                End Using
            Case 3
                Using BulkCopy As SqlBulkCopy = New SqlBulkCopy(ConnectionString)
                    BulkCopy.DestinationTableName = destinationTableName
                    BulkCopy.BatchSize = batchSizeBulkCopy
                    BulkCopy.BulkCopyTimeout = bulkCopyTimeout
                    BulkCopy.WriteToServer(dataTableBulk)
                End Using
                Return True
        End Select
        Return Nothing
    End Function
End Class
