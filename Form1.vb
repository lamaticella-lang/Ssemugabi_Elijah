Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    Private ReadOnly txtSalary As New TextBox()
    Private ReadOnly txtDependents As New TextBox()
    Private ReadOnly txtSchoolFees As New TextBox()
    Private ReadOnly lblSchoolTotal As New Label()
    Private ReadOnly lblTakeHome As New Label()
    Private ReadOnly lblNssf As New Label()
    Private ReadOnly lblPaye As New Label()
    Private ReadOnly lblSacco As New Label()
    Private ReadOnly lblTotalDeductions As New Label()
    Private ReadOnly lblAfterDeductions As New Label()

    Public Sub New()
        Me.Text = "Sam's Salary Calculator"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Size = New Size(700, 650)
        Me.MinimumSize = New Size(700, 650)
        Me.Font = New Font("Segoe UI", 10.0F)
        Me.BackColor = Color.WhiteSmoke

        Dim heading As New Label With {
            .Text = "SAM'S SALARY CALCULATOR",
            .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
            .AutoSize = True,
            .Location = New Point(25, 20)
        }
        Me.Controls.Add(heading)

        AddInput("Monthly salary (UGX):", txtSalary, "2300000", 75)
        AddInput("Number of dependents:", txtDependents, "4", 120)
        AddInput("School fees per dependent (UGX):", txtSchoolFees, "430000", 165)

        Dim btnCalculate As New Button With {
            .Text = "Calculate",
            .Location = New Point(30, 215),
            .Size = New Size(130, 38),
            .BackColor = Color.SteelBlue,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        AddHandler btnCalculate.Click, AddressOf CalculateResults
        Me.Controls.Add(btnCalculate)

        Dim btnClear As New Button With {
            .Text = "Clear Results",
            .Location = New Point(175, 215),
            .Size = New Size(130, 38)
        }
        AddHandler btnClear.Click, AddressOf ClearResults
        Me.Controls.Add(btnClear)

        Dim groupA As New GroupBox With {
            .Text = "Part (a): School Fees and Take-Home Balance",
            .Location = New Point(25, 270),
            .Size = New Size(630, 105)
        }
        lblSchoolTotal.Location = New Point(20, 30)
        lblSchoolTotal.AutoSize = True
        lblTakeHome.Location = New Point(20, 62)
        lblTakeHome.AutoSize = True
        groupA.Controls.Add(lblSchoolTotal)
        groupA.Controls.Add(lblTakeHome)
        Me.Controls.Add(groupA)

        Dim groupB As New GroupBox With {
            .Text = "Part (b): Balance After Salary Deductions",
            .Location = New Point(25, 390),
            .Size = New Size(630, 180)
        }
        SetResultLabel(lblNssf, "NSSF (10%):", 28)
        SetResultLabel(lblPaye, "PAYE (9%):", 55)
        SetResultLabel(lblSacco, "SACCO (5%):", 82)
        SetResultLabel(lblTotalDeductions, "Total deductions:", 109)
        SetResultLabel(lblAfterDeductions, "Balance after deductions:", 138)
        groupB.Controls.AddRange(New Control() {lblNssf, lblPaye, lblSacco, lblTotalDeductions, lblAfterDeductions})
        Me.Controls.Add(groupB)

        Dim note As New Label With {
            .Text = "Note: Part (a) and part (b) are calculated separately, following the question's formulas.",
            .AutoSize = False,
            .Location = New Point(28, 580),
            .Size = New Size(625, 35),
            .Font = New Font("Segoe UI", 8.5F, FontStyle.Italic)
        }
        Me.Controls.Add(note)
    End Sub

    Private Sub AddInput(caption As String, input As TextBox, defaultValue As String, y As Integer)
        Dim label As New Label With {
            .Text = caption,
            .Location = New Point(30, y + 3),
            .AutoSize = True
        }
        input.Text = defaultValue
        input.Location = New Point(300, y)
        input.Size = New Size(200, 28)
        Me.Controls.Add(label)
        Me.Controls.Add(input)
    End Sub

    Private Sub SetResultLabel(target As Label, caption As String, y As Integer)
        target.Text = caption
        target.Location = New Point(20, y)
        target.AutoSize = True
    End Sub

    Private Sub CalculateResults(sender As Object, e As EventArgs)
        Dim salary As Decimal
        Dim dependents As Integer
        Dim schoolFees As Decimal

        If Not Decimal.TryParse(txtSalary.Text, salary) OrElse salary < 0D Then
            MessageBox.Show("Enter a valid non-negative monthly salary.", "Invalid Input",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSalary.Focus()
            Return
        End If

        If Not Integer.TryParse(txtDependents.Text, dependents) OrElse dependents < 0 Then
            MessageBox.Show("Enter a valid non-negative number of dependents.", "Invalid Input",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDependents.Focus()
            Return
        End If

        If Not Decimal.TryParse(txtSchoolFees.Text, schoolFees) OrElse schoolFees < 0D Then
            MessageBox.Show("Enter valid non-negative school fees.", "Invalid Input",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSchoolFees.Focus()
            Return
        End If

        ' Part (a): calculate total school fees and salary less school fees.
        Dim totalExpenses As Decimal = dependents * schoolFees
        Dim takeHomeBalance As Decimal = salary - totalExpenses

        ' Part (b): calculate NSSF, PAYE, and SACCO from monthly salary.
        Dim nssf As Decimal = salary * 10D / 100D
        Dim paye As Decimal = salary * 9D / 100D
        Dim sacco As Decimal = salary * 5D / 100D
        Dim totalDeductions As Decimal = nssf + paye + sacco
        Dim balanceAfterDeductions As Decimal = salary - totalDeductions

        lblSchoolTotal.Text = "Total school fees: UGX " & totalExpenses.ToString("N0")
        lblTakeHome.Text = "Take-home balance: UGX " & takeHomeBalance.ToString("N0")
        lblNssf.Text = "NSSF (10%): UGX " & nssf.ToString("N0")
        lblPaye.Text = "PAYE (9%): UGX " & paye.ToString("N0")
        lblSacco.Text = "SACCO (5%): UGX " & sacco.ToString("N0")
        lblTotalDeductions.Text = "Total deductions: UGX " & totalDeductions.ToString("N0")
        lblAfterDeductions.Text = "Balance after deductions: UGX " & balanceAfterDeductions.ToString("N0")
    End Sub

    Private Sub ClearResults(sender As Object, e As EventArgs)
        lblSchoolTotal.Text = ""
        lblTakeHome.Text = ""
        lblNssf.Text = ""
        lblPaye.Text = ""
        lblSacco.Text = ""
        lblTotalDeductions.Text = ""
        lblAfterDeductions.Text = ""
    End Sub
End Class
