﻿Imports System.Globalization

Public Class CalenderControl
    Private CurrentDate As Date
    Public Sub ReloadCal(ByVal ldate As Date, ByVal Selected As Integer)
        CurrentDate = ldate
        Me.clearall()
        lblDate.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ldate.Month) + " " + ldate.Year.ToString
        Dim fdate As DayOfWeek = GetFirstOfMonthDay(ldate)
        Dim idate As Integer = 1
        Dim row As Integer = 1
        Dim col As Integer = 0
        Do
            If col >= fdate Then
                Exit Do
            End If
            getButton(col, row).Visible = False
            col += 1
        Loop
        Do
            getButton(fdate, row).Text = idate
            getButton(fdate, row).ForeColor = Color.Black
            getButton(fdate, row).BackColor = Color.White
            If idate = Selected And ldate.Month = Date.Today.Month And ldate.Year = Date.Today.Year Then
                getButton(fdate, row).ForeColor = Color.White
                getButton(fdate, row).BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
            End If
            If fdate = DayOfWeek.Saturday Then
                row += 1
            End If
            fdate = tdate(fdate)
            idate += 1
            If idate = Date.DaysInMonth(ldate.Year, ldate.Month) + 1 Then
                Exit Do
            End If
        Loop
        Do
            If (idate + GetFirstOfMonthDay(ldate)) > 42 Then
                Exit Do
            End If
            getButton(fdate, row).Visible = False
            If fdate = DayOfWeek.Saturday Then
                row += 1
            End If
            fdate = tdate(fdate)
            idate += 1
        Loop
    End Sub

    Sub clearall()
        For Each txt In {
            Su1, Su2, Su3, Su4, Su5, Su6,
            Mo1, Mo2, Mo3, Mo4, Mo5, Mo6,
            Tu1, Tu2, Tu3, Tu4, Tu5, Tu6,
            We1, We2, We3, We4, We5, We6,
            Th1, Th2, Th3, Th4, Th5, Th6,
            Fr1, Fr2, Fr3, Fr4, Fr5, Fr6,
            Sa1, Sa2, Sa3, Sa4, Sa5, Sa6}
            txt.Text = Nothing
            txt.Visible = True
        Next
    End Sub

    Function getButton(ByVal day As DayOfWeek, ByVal row As Integer) As System.Windows.Forms.Button
        Select Case day
            Case DayOfWeek.Sunday
                Select Case row
                    Case 1
                        Return Su1
                    Case 2
                        Return Su2
                    Case 3
                        Return Su3
                    Case 4
                        Return Su4
                    Case 5
                        Return Su5
                    Case Else
                        Return Su6
                End Select
            Case DayOfWeek.Monday
                Select Case row
                    Case 1
                        Return Mo1
                    Case 2
                        Return Mo2
                    Case 3
                        Return Mo3
                    Case 4
                        Return Mo4
                    Case 5
                        Return Mo5
                    Case Else
                        Return Mo6
                End Select
            Case DayOfWeek.Tuesday
                Select Case row
                    Case 1
                        Return Tu1
                    Case 2
                        Return Tu2
                    Case 3
                        Return Tu3
                    Case 4
                        Return Tu4
                    Case 5
                        Return Tu5
                    Case Else
                        Return Tu6
                End Select
            Case DayOfWeek.Wednesday
                Select Case row
                    Case 1
                        Return We1
                    Case 2
                        Return We2
                    Case 3
                        Return We3
                    Case 4
                        Return We4
                    Case 5
                        Return We5
                    Case Else
                        Return We6
                End Select
            Case DayOfWeek.Thursday
                Select Case row
                    Case 1
                        Return Th1
                    Case 2
                        Return Th2
                    Case 3
                        Return Th3
                    Case 4
                        Return Th4
                    Case 5
                        Return Th5
                    Case Else
                        Return Th6
                End Select
            Case DayOfWeek.Friday
                Select Case row
                    Case 1
                        Return Fr1
                    Case 2
                        Return Fr2
                    Case 3
                        Return Fr3
                    Case 4
                        Return Fr4
                    Case 5
                        Return Fr5
                    Case Else
                        Return Fr6
                End Select
            Case Else
                Select Case row
                    Case 1
                        Return Sa1
                    Case 2
                        Return Sa2
                    Case 3
                        Return Sa3
                    Case 4
                        Return Sa4
                    Case 5
                        Return Sa5
                    Case Else
                        Return Sa6
                End Select
        End Select
    End Function

    Private Function GetFirstOfMonthDay(ByVal ThisDay As Date) As DayOfWeek
        Dim tday As DayOfWeek = ThisDay.DayOfWeek
        Dim tint As Integer = ThisDay.Day
        If tint = 1 Then
            Return tday
            Exit Function
        End If
        Do
            tint -= 1
            tday = ydate(tday)
            If tint = 1 Then Exit Do
        Loop
        Return tday
    End Function

    Private Function ydate(ByVal tday As DayOfWeek) As DayOfWeek
        Dim rday As DayOfWeek
        Select Case tday
            Case DayOfWeek.Sunday
                rday = DayOfWeek.Saturday
            Case DayOfWeek.Monday
                rday = DayOfWeek.Sunday
            Case DayOfWeek.Tuesday
                rday = DayOfWeek.Monday
            Case DayOfWeek.Wednesday
                rday = DayOfWeek.Tuesday
            Case DayOfWeek.Thursday
                rday = DayOfWeek.Wednesday
            Case DayOfWeek.Friday
                rday = DayOfWeek.Thursday
            Case DayOfWeek.Saturday
                rday = DayOfWeek.Friday
        End Select
        Return rday
    End Function

    Private Function tdate(ByVal tday As DayOfWeek) As DayOfWeek
        Dim rday As DayOfWeek
        Select Case tday
            Case DayOfWeek.Sunday
                rday = DayOfWeek.Monday
            Case DayOfWeek.Monday
                rday = DayOfWeek.Tuesday
            Case DayOfWeek.Tuesday
                rday = DayOfWeek.Wednesday
            Case DayOfWeek.Wednesday
                rday = DayOfWeek.Thursday
            Case DayOfWeek.Thursday
                rday = DayOfWeek.Friday
            Case DayOfWeek.Friday
                rday = DayOfWeek.Saturday
            Case DayOfWeek.Saturday
                rday = DayOfWeek.Sunday
        End Select
        Return rday
    End Function

    Public Sub New(flp As FlowLayoutPanel)
        InitializeComponent()
        ReloadCal(Date.Today, Date.Today.Day)
    End Sub

    Private Sub btnBeforeMonth_Click(sender As Object, e As EventArgs) Handles btnBeforeMonth.Click
        ReloadCal(CurrentDate.AddMonths(-1), CurrentDate.AddMonths(-1).Day)
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        ReloadCal(CurrentDate.AddMonths(1), CurrentDate.AddMonths(1).Day)
    End Sub

    Private Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click
        ReloadCal(Date.Today, Date.Today.Day)
        NHLGamesMetro.m_Date = Date.Today
        NHLGamesMetro.m_lblDate.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(Date.Today.DayOfWeek).Substring(0, 3) + ", " +
            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Date.Today.Month).Substring(0, 3) + " " +
            Date.Today.Day.ToString + ", " + Date.Today.Year.ToString
        NHLGamesMetro.m_flpCalendar.Visible = False
    End Sub

    Private Sub Day_Click(sender As Object, e As EventArgs) Handles We6.Click, We5.Click, We4.Click, We3.Click, We2.Click, We1.Click, Tu6.Click, Tu5.Click, Tu4.Click, Tu3.Click, Tu2.Click, Tu1.Click, Th6.Click, Th5.Click, Th4.Click, Th3.Click, Th2.Click, Th1.Click, Su6.Click, Su5.Click, Su4.Click, Su3.Click, Su2.Click, Su1.Click, Sa6.Click, Sa5.Click, Sa4.Click, Sa3.Click, Sa2.Click, Sa1.Click, Mo6.Click, Mo5.Click, Mo4.Click, Mo3.Click, Mo2.Click, Mo1.Click, Fr6.Click, Fr5.Click, Fr4.Click, Fr3.Click, Fr2.Click, Fr1.Click
        Dim btn As Button
        btn = sender
        Dim myDate = CurrentDate.AddDays(-CurrentDate.Day + btn.Text)
        NHLGamesMetro.m_Date = CurrentDate.AddDays(-CurrentDate.Day + btn.Text)
        NHLGamesMetro.m_lblDate.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(myDate.DayOfWeek).Substring(0, 3) + ", " +
            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(myDate.Month).Substring(0, 3) + " " +
            myDate.Day.ToString + ", " + myDate.Year.ToString
        NHLGamesMetro.m_flpCalendar.Visible = False
    End Sub

    Private Sub btnBeforeYear_Click(sender As Object, e As EventArgs) Handles btnBeforeYear.Click
        ReloadCal(CurrentDate.AddYears(1), CurrentDate.AddYears(1).Day)
    End Sub

    Private Sub btnNextYear_Click(sender As Object, e As EventArgs) Handles btnNextYear.Click
        ReloadCal(CurrentDate.AddYears(-1), CurrentDate.AddYears(-1).Day)
    End Sub
End Class