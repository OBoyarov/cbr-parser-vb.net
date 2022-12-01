Imports System.Net
Imports System.Xml

Public Class Main

#Region "Queries"
    Property QueryIsExistData As String = "SELECT TOP 1 CAST(IIF(COUNT(*) > 0, 1, 0) AS BIT) FROM [{0}] WHERE [Date] = '{1}'"
    Property QueryFillStorage As String = "SELECT FORMAT(d.[Date], 'dd.MM.yyyy') AS [Date], d.[CharCode], (d.[Value] / i.[Nominal]) as [Currency] FROM [t_cbr_data] AS d LEFT JOIN [t_cbr_items] AS i ON d.[CharCode] = i.[ISO_Char_Code] WHERE d.[Date] = '{0}'"
    Property QueryFillCurrencies As String = "SELECT DISTINCT i.[Name] + ' [' + d.[CharCode] + ']' AS [Currency] FROM [t_cbr_data] AS d LEFT JOIN [t_cbr_items] AS i ON d.[charcode] = i.[ISO_Char_Code]"
#End Region

    Private SqlEvent As SqlEvents
    Private SessionStorage As New DataTable

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SqlEvent = New SqlEvents("localhost", "db", "sa", "WT3PXs67")
        If Not SqlEvent.IsConnection Then
            MsgBox("Error establishing a database connection", MsgBoxStyle.Critical)
            Application.Exit()
        End If
        SelectedDate.MaxDate = Now
        CheckCurrencyValues(Now)
        CheckCurrencyTypes()
        FillStorage(Now)
        FillCurrencies()
    End Sub

    Private Sub SelectedDate_Or_Currency_ValueChanged(sender As Object, e As EventArgs) Handles SelectedDate.ValueChanged, Currency.SelectedIndexChanged
        Dim CurrencyStr As String = Currency.Text.Split(New Char() {"[", "]"})(1)
        Dim SelectedDateStr As String = SelectedDate.Value.ToString("dd.MM.yyyy")
        Dim DR As DataRow() = SessionStorage.Select(String.Format("Date = '{0}' AND CharCode = '{1}'", SelectedDateStr, CurrencyStr))
        If DR.Length > 0 Then
            Value.Text = DR(0)("Currency").ToString
        Else
            Value.Text = "Loading data..."
            Update()
            FillStorage(SelectedDate.Value)
            DR = SessionStorage.Select(String.Format("Date = '{0}' AND CharCode = '{1}'", SelectedDateStr, CurrencyStr))
            If DR.Length > 0 Then
                Value.Text = DR(0)("Currency").ToString
            Else
                Value.Text = "Failed to get data!"
            End If
        End If
    End Sub

    Private Sub CheckCurrencyValues(ByVal WhatDate As Date, Optional ByVal CharCode As String = Nothing)
        Dim IsExistData As Boolean = CBool(SqlEvent.Execute(String.Format(QueryIsExistData, "t_cbr_data", WhatDate.ToString("yyyy-MM-dd")), 1))
        If Not IsExistData Then
            Dim URL As String = String.Format("http://www.cbr.ru/scripts/XML_daily.asp?date_req={0}", WhatDate.ToString("dd.MM.yyyy"))
            Dim XML As String = (New WebClient).DownloadString(URL)

            Dim XmlDoc As New XmlDocument()
            XmlDoc.LoadXml(XML)
            Dim XmlNodes As XmlNodeList
            If Not CharCode Is Nothing Then
                XmlNodes = XmlDoc.SelectNodes("//ValCurs/Valute[CharCode=""" & CharCode & """]")
            Else
                XmlNodes = XmlDoc.SelectNodes("//ValCurs/Valute")
            End If

            Dim DT As DataTable = New DataTable
            DT.Columns.Add("Date", GetType(Date))
            DT.Columns.Add("CharCode", GetType(String))
            DT.Columns.Add("Value", GetType(Decimal))

            For Each x As XmlElement In XmlNodes
                Dim VCharCode As String = x.GetElementsByTagName("CharCode")(0).InnerText
                Dim VValue As Double = CDbl(x.GetElementsByTagName("Value")(0).InnerText)
                DT.Rows.Add(WhatDate, VCharCode, VValue)
            Next

            If DT.Rows.Count > 0 Then
                SqlEvent.Execute(Nothing, 3, DT, "[t_cbr_data]")
            End If
        End If
    End Sub
    Private Sub FillStorage(ByVal WhatDate As Date)
        Dim DT As DataTable = SqlEvent.Execute(String.Format(QueryFillStorage, WhatDate.ToString("yyyy-MM-dd")), 2)
        If DT.Rows.Count = 0 Then
            CheckCurrencyValues(WhatDate)
            FillStorage(WhatDate)
        Else
            SessionStorage.Merge(DT)
        End If
    End Sub

    Private Sub CheckCurrencyTypes()
        Dim IsExistData As Boolean = CBool(SqlEvent.Execute(String.Format(QueryIsExistData, "t_cbr_items", Now.ToString("yyyy-MM-dd")), 1))
        If Not IsExistData Then
            Dim URL As String = "http://www.cbr.ru/scripts/XML_valFull.asp"
            Dim XML As String = (New WebClient).DownloadString(URL)

            Dim XmlDoc As New XmlDocument()
            XmlDoc.LoadXml(XML)
            Dim XmlNodes As XmlNodeList
            XmlNodes = XmlDoc.SelectNodes("//Valuta/Item")

            Dim DT As DataTable = New DataTable
            DT.Columns.Add("Date", GetType(Date))
            DT.Columns.Add("Name", GetType(String))
            DT.Columns.Add("EngName", GetType(String))
            DT.Columns.Add("Nominal", GetType(Integer))
            DT.Columns.Add("ParentCode", GetType(String))
            DT.Columns.Add("ISO_Num_Code", GetType(Integer))
            DT.Columns.Add("ISO_Char_Code", GetType(String))

            For Each x As XmlElement In XmlNodes
                If x.GetElementsByTagName("ISO_Num_Code")(0).InnerText.Length > 0 Then
                    Dim IName As String = x.GetElementsByTagName("Name")(0).InnerText
                    Dim IEngName As String = x.GetElementsByTagName("EngName")(0).InnerText
                    Dim INominal As Integer = CInt(x.GetElementsByTagName("Nominal")(0).InnerText)
                    Dim IParentCode As String = x.GetElementsByTagName("ParentCode")(0).InnerText
                    Dim IISO_Num_Code As Integer = CInt(x.GetElementsByTagName("ISO_Num_Code")(0).InnerText)
                    Dim IISO_Char_Code As String = x.GetElementsByTagName("ISO_Char_Code")(0).InnerText
                    DT.Rows.Add(Now, IName, IEngName, INominal, IParentCode, IISO_Num_Code, IISO_Char_Code)
                End If
            Next
            If DT.Rows.Count > 0 Then
                SqlEvent.Execute("TRUNCATE TABLE [t_cbr_items]", 0)
                SqlEvent.Execute(Nothing, 3, DT, "[t_cbr_items]")
            End If
        End If
    End Sub
    Private Sub FillCurrencies()
        Dim DT As DataTable = SqlEvent.Execute(QueryFillCurrencies, 2)
        Currency.DisplayMember = "Currency"
        Currency.DataSource = DT
    End Sub

End Class
