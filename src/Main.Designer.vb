<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Currency = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Value = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SelectedDate = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(155, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Currency:"
        '
        'Currency
        '
        Me.Currency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Currency.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Currency.FormattingEnabled = True
        Me.Currency.Location = New System.Drawing.Point(158, 25)
        Me.Currency.Name = "Currency"
        Me.Currency.Size = New System.Drawing.Size(361, 20)
        Me.Currency.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(522, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Code:"
        '
        'Value
        '
        Me.Value.Location = New System.Drawing.Point(525, 25)
        Me.Value.Name = "Value"
        Me.Value.ReadOnly = True
        Me.Value.Size = New System.Drawing.Size(182, 20)
        Me.Value.TabIndex = 8
        Me.Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Date:"
        '
        'SelectedDate
        '
        Me.SelectedDate.Location = New System.Drawing.Point(15, 25)
        Me.SelectedDate.Name = "SelectedDate"
        Me.SelectedDate.Size = New System.Drawing.Size(137, 20)
        Me.SelectedDate.TabIndex = 6
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 76)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Currency)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Value)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SelectedDate)
        Me.Name = "Main"
        Me.Text = "CBR_Parser"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents Currency As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Value As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents SelectedDate As DateTimePicker
End Class
