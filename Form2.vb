Public Class Form2
    Private NCatCompCode As String = String.Empty
    Private qtyLQFields As String = String.Empty
    Public Sub InitHandlingTime(ByVal NCompCode As String, Optional ByVal qtyFields As String = "")
        NCatCompCode = NCompCode
        qtyLQFields = qtyFields
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim qtyValues As String = GetHandlingQtyValues()
        GetQty(qtyValues)
    End Sub
    Private Function GetHandlingQtyValues() As String
        Dim qtyValues As String = String.Empty
        qtyValues = qtyLQFields
        Dim qty As String() = {}
        Dim txtVal As String = String.Empty
        Dim i As Integer = 0
        If (qtyValues IsNot Nothing And qtyValues.Length > 0) Then
            qty = qtyValues.Split("$")

            For i = 0 To qty.Length - 1

                If (i = 0) Then
                    txtVal = qty(i) & "$"
                Else
                    txtVal = txtVal & qty(i) & "$"
                End If
            Next


        Else
            For i = 0 To 0

                If (i = 0) Then
                    txtVal = 0 & "$"

                Else
                    txtVal = txtVal & txtVal
                End If
            Next

        End If
        If (txtVal.Substring(txtVal.Length - 1) = "$") Then
            txtVal = txtVal.Substring(0, txtVal.Length - 1)
        End If
        Return txtVal
    End Function
    Private Sub GetQty(ByVal txtQtyVal As String)
        Dim valHM As String() = {}
        Dim qtyHM As String() = {}
        Dim codeNames As String() = {}
        Dim codename As String = String.Empty
        valHM = txtQtyVal.Split("$")

        TextBox1.Text = valHM(0)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim qtyVal As String() = {}
        Dim qtyVals As String() = {}
        Dim totalReturn As String = String.Empty
        Dim total As Decimal = 0
        If (Not String.IsNullOrEmpty(qtyLQFields)) Then
            qtyVal = qtyLQFields.Split("$")
            totalReturn = TextBox1.Text
            total = Convert.ToDecimal(totalReturn)
        End If
        Me.ReturntotalValue = total & ":" & totalReturn
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    Public Property ReturntotalValue() As String

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        e.Handled = IsNumeric(e.KeyChar)
        If (e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Back)) Then
            e.Handled = False
        End If
    End Sub
    Private Function IsNumeric(ByVal key As Char) As Boolean
        Dim nonNumericRegex As New System.Text.RegularExpressions.Regex("\D")
        If (nonNumericRegex.IsMatch(key)) Then
            Return True
        End If
        Return False
    End Function
End Class