Public Class Form1
    Dim dsLoad As New DataSet
    Dim drLoad() As DataRow
    Dim drRow As DataRow
    Dim LQCode As String = String.Empty
    Dim LQName As String = String.Empty
    Dim NcCodeLQ As String = String.Empty
    Dim qtyFields As String = String.Empty
    Private Sub getDataForLoad()
        Dim dtLoad As New DataTable("dtLoad")
        Dim col1 As New DataColumn("code")
        Dim col2 As New DataColumn("name")
        Dim col3 As New DataColumn("isDeleted")
        dtLoad.Columns.Add(col1)
        dtLoad.Columns.Add(col2)
        dtLoad.Columns.Add(col3)
        For i As Integer = 1 To 6
            drRow = dtLoad.NewRow()
            drRow(0) = "LQ" & i
            drRow(1) = "Load Quality" & i
            If i = 1 Then
                drRow(2) = 0
            ElseIf i = 2 Then
                drRow(2) = 0
            ElseIf i = 3 Then
                drRow(2) = -1
            ElseIf i = 4 Then
                drRow(2) = -1
            ElseIf i = 5 Then
                drRow(2) = 0
            ElseIf i = 6 Then
                drRow(2) = -1
            End If
            dtLoad.Rows.Add(drRow)
        Next
        dsLoad.Tables.Add(dtLoad)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getDataForLoad()
        drLoad = dsLoad.Tables(0).Select("isDeleted=0")
        Dim tableLayout As New TableLayoutPanel
        tableLayout.Name = "tbl"
        tableLayout.Location = New Point(0, 11)
        tableLayout.ColumnCount = 0
        tableLayout.RowCount = 0
        tableLayout.AutoSize = True
        tableLayout.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
        tableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.AutoSize))
        tableLayout.BackColor = System.Drawing.Color.Transparent
        GroupBox1.Controls.Add(tableLayout)
        For j As Integer = 0 To drLoad.Length - 1
            tableLayout.ColumnCount += 1
            tableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.AutoSize))

            tableLayout.RowCount += 1
            tableLayout.RowStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.AutoSize))
            LQCode = Convert.ToString(drLoad(j)("code"))
            LQName = Convert.ToString(drLoad(j)("name"))
            Dim lblLQ As New Label()
            lblLQ.Name = "lblLQ1" & j

            lblLQ.Text = LQCode & "-" & LQName
            lblLQ.Width = 150
            lblLQ.Height = 20
            lblLQ.BackColor = Color.Transparent
            lblLQ.AutoSize = True
            Dim fontval As New Font("Microsoft Sans Serif", 8.25)
            lblLQ.Font = fontval
            lblLQ.Anchor = AnchorStyles.Left 'And AnchorStyles.Right
            lblLQ.TextAlign = ContentAlignment.TopLeft
            tableLayout.Controls.Add(lblLQ, 0, j)

            ''textbox
            tableLayout.ColumnCount += 1
            tableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.AutoSize))
            Dim txtLQ As New TextBox
            txtLQ.ReadOnly = True
            txtLQ.Text = 0
            txtLQ.Height = 20
            txtLQ.Width = 76
            txtLQ.Name = "txtLQ_" & LQCode
            txtLQ.Anchor = AnchorStyles.Left And AnchorStyles.Right
            tableLayout.Controls.Add(txtLQ, 1, j)
            'y2 = y2 + 28

            'button
            tableLayout.ColumnCount += 1
            tableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.AutoSize))
            Dim btnLQ As New Button
            btnLQ.Text = "Quantity"
            btnLQ.UseVisualStyleBackColor = True
            btnLQ.Name = Convert.ToString(LQCode)
            AddHandler btnLQ.Click, AddressOf btnLQ_Click
            btnLQ.Size = New System.Drawing.Size(77, 22)
            btnLQ.Anchor = AnchorStyles.Left And AnchorStyles.Right
            tableLayout.Controls.Add(btnLQ, 2, j)
            'y3 = y3 + 28

            ''hiddenTextbox
            tableLayout.ColumnCount += 1
            tableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(SizeType.AutoSize))
            Dim txtLQHdn As New TextBox
            txtLQHdn.ReadOnly = True
            txtLQHdn.Text = 0
            txtLQHdn.Visible = False
            txtLQHdn.Height = 20
            txtLQHdn.Width = 76
            txtLQHdn.Name = "txtLQHdn_" & LQCode
            txtLQHdn.Anchor = AnchorStyles.Left And AnchorStyles.Right
            tableLayout.Controls.Add(txtLQHdn, 3, j)
            tableLayout.ColumnCount = 0
        Next
    End Sub
    Private Sub btnLQ_Click(sender As Object, e As EventArgs)
        NcCodeLQ = DirectCast(sender, System.Windows.Forms.Control).Name
        OpenLoadData(NcCodeLQ)
    End Sub
    Private Sub OpenLoadData(NcCode As String)
        Dim frmHT As String = String.Empty
        Dim txtLQL As TextBox = TryCast(GroupBox1.Controls.Find("txtLQ_" & NcCode, True).FirstOrDefault(), TextBox)

        Dim returnVal As String() = {}
        Dim txtLQHdn As TextBox = TryCast(GroupBox1.Controls.Find("txtLQHdn_" & NcCode, True).FirstOrDefault, TextBox)

        Dim frmHandlingTime As New Form2()
            frmHT = frmHandlingTime.Name
            qtyFields = txtLQHdn.Text
        frmHandlingTime.InitHandlingTime(NcCode, qtyFields)
        frmHandlingTime.ShowDialog(Me)
        If frmHandlingTime.DialogResult = DialogResult.OK Then
            returnVal = frmHandlingTime.ReturntotalValue.Split(":")
            txtLQL.Text = returnVal(0)
            txtLQHdn.Text = returnVal(1)
        End If


    End Sub
End Class
