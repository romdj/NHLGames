﻿Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports Newtonsoft.Json.Linq
Public Class NHLGames

    Private AvailableGames As New List(Of String)
    Private Games As New List(Of Game)

    Private ReadOnly Property SelectedGame As Game
        Get
            Dim selectedGameID As String = gridGames.SelectedRows(0).Cells(0).Value
            Return Games.Find(Function(val) val.Id.ToString() = selectedGameID)
        End Get
    End Property
    Private Sub VersionCheck()
        Dim settingsReader As New Configuration.AppSettingsReader

        Dim strLatest = Downloader.DownloadApplicationVersion()

        If strLatest > settingsReader.GetValue("version", GetType(String)) Then
            lblVersion.Text = "Version " & strLatest & " available! You are running " & settingsReader.GetValue("version", GetType(String)) & "."
            lblVersion.ForeColor = Color.Red
        Else
            lblVersion.Text = "Up to date! You are running " & settingsReader.GetValue("version", GetType(String)) & "."
            lblVersion.ForeColor = Color.Green
        End If
    End Sub

    Private Sub NHLGames_Load(sender As Object, e As EventArgs) Handles Me.Load
        VersionCheck()
        dtDate.Value = DateTime.Now()
    End Sub

    Private Sub dtDate_ValueChanged(sender As Object, e As EventArgs) Handles dtDate.ValueChanged
        LoadGames(dtDate.Value)
    End Sub

    Private Sub LoadGames(dateTime As DateTime)

        SetupDataGridColumns()

        Dim row As String()
        Try

            Dim JSONSchedule As JObject = Downloader.DownloadJSONSchedule(dateTime)
            AvailableGames = Downloader.DownloadAvailableGames()
            Games = Game.GetGames(JSONSchedule, availableGames)

            For Each game As Game In Games
                row = {game.Id.ToString(), game.Date.ToLocalTime().ToString("hh:mm"), game.AwayTeam, game.AwayAbbrev, game.HomeTeam, game.HomeAbbrev, game.AwayStream.GameURL, game.AwayStream.VODURL, game.AwayStream.Availability, game.HomeStream.GameURL, game.HomeStream.VODURL, game.HomeStream.Availability, game.NationalStream.GameURL, game.NationalStream.VODURL, game.NationalStream.Availability, game.FrenchStream.GameURL, game.FrenchStream.VODURL, game.FrenchStream.Availability}
                gridGames.Rows.Add(row)
            Next

            UpdateCellColors()

        Catch ex As ArgumentOutOfRangeException
            row = {"No Games"}
            gridGames.Rows.Add(row)
        Catch ex As NullReferenceException
            row = {"No Games"}
            gridGames.Rows.Add(row)
        End Try
    End Sub

    Private Sub UpdateCellColors()

        For Each row As DataGridViewRow In gridGames.Rows
            For Each cell As DataGridViewCell In row.Cells
                If cell.Value = "Available" Then
                    cell.Style.ForeColor = Color.Green
                ElseIf cell.Value = "Unavailable" Then
                    cell.Style.ForeColor = Color.Red
                End If
            Next
        Next

    End Sub

    Private Sub SetupDataGridColumns()

        gridGames.Columns.Clear()
        gridGames.Rows.Clear()

        gridGames.CellBorderStyle = DataGridViewCellBorderStyle.None

        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "GameID", .HeaderText = "Game ID", .Visible = False})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "time", .HeaderText = "Time", .Width = 35})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "away", .HeaderText = "Away Team", .Width = 150})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "awayAbbrev", .HeaderText = "Away Abbrev", .Visible = False})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "home", .HeaderText = "Home Team", .Width = 150})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "homeAbbrev", .HeaderText = "Home Abbrev", .Visible = False})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "awayLIVE", .HeaderText = "Away URL", .Visible = False})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "awayVOD", .HeaderText = "Away VOD", .Visible = False})
        gridGames.Columns.Add(New DataGridViewLinkColumn() With {.Name = "awayLIVEStatus", .HeaderText = "Away Stream", .Width = 65})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "homeLIVE", .HeaderText = "Home URL", .Visible = False})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "homeVOD", .HeaderText = "Home VOD", .Visible = False})
        gridGames.Columns.Add(New DataGridViewLinkColumn() With {.Name = "homeLIVEStatus", .HeaderText = "Home Stream", .Width = 65})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "nationalLIVE", .HeaderText = "National URL", .Visible = False})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "nationalVOD", .HeaderText = "National VOD", .Visible = False})
        gridGames.Columns.Add(New DataGridViewLinkColumn() With {.Name = "nationalLIVEStatus", .HeaderText = "National Stream", .Width = 65})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "frenchLIVE", .HeaderText = "French URL", .Visible = False})
        gridGames.Columns.Add(New DataGridViewTextBoxColumn() With {.Name = "frenchVOD", .HeaderText = "French VOD", .Visible = False})
        gridGames.Columns.Add(New DataGridViewLinkColumn() With {.Name = "frenchLIVEStatus", .HeaderText = "French Stream", .Width = 65})



        For Each column As DataGridViewColumn In gridGames.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

    End Sub

    Private Sub gridGames_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridGames.CellClick

        Dim cellText As String = gridGames.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        If cellText = "Watch" Then

            Dim selectedGameID As String = gridGames.Rows(e.RowIndex).Cells(0).Value
            Dim game As Game = Games.Find(Function(val) val.Id.ToString() = selectedGameID)


            If (gridGames.Columns(e.ColumnIndex).Name = "awayLIVEStatus") Then
                WatchGame(rbLive.Checked = False, game, GameStream.StreamType.Away)
            ElseIf (gridGames.Columns(e.ColumnIndex).Name = "homeLIVEStatus") Then
                WatchGame(rbLive.Checked = False, game, GameStream.StreamType.Home)
            ElseIf (gridGames.Columns(e.ColumnIndex).Name = "nationalLIVEStatus") Then
                WatchGame(rbLive.Checked = False, game, GameStream.StreamType.National)
            ElseIf (gridGames.Columns(e.ColumnIndex).Name = "frenchLIVEStatus") Then
                WatchGame(rbLive.Checked = False, game, GameStream.StreamType.French)
            End If

        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        VersionCheck()
        LoadGames(dtDate.Value)
    End Sub

    Private Sub btnHosts_Click(sender As Object, e As EventArgs) Handles btnHosts.Click

        HostsFile.AddEntry("82.196.2.27 mf.svc.nhl.com")

    End Sub

    Private Sub btnWatch_Click(sender As Object, e As EventArgs) Handles btnWatch.Click
        'If rbLive.Checked Then
        '    WatchGame(False)
        'Else
        '    WatchGame(True)
        'End If
    End Sub

    Private Sub gridGames_SelectionChanged(sender As Object, e As EventArgs) Handles gridGames.SelectionChanged

        'With inline watch links, not needed anymore
        gridGames.ClearSelection()


        'If gridGames.SelectedRows.Count = 1 Then

        '    rbAway.Enabled = SelectedGame.AwayStream.IsAvailable
        '    rbHome.Enabled = SelectedGame.HomeStream.IsAvailable
        '    rbNational.Enabled = SelectedGame.NationalStream.IsAvailable
        '    rbFrench.Enabled = SelectedGame.FrenchStream.IsAvailable

        '    If SelectedGame.AwayStream.IsAvailable = False Then
        '        rbAway.Checked = False
        '    End If

        '    If SelectedGame.HomeStream.IsAvailable Then
        '        If rbAway.Checked = False Then
        '            rbHome.Checked = True
        '        End If
        '    Else
        '        rbHome.Checked = False
        '    End If

        '    rbNational.Checked = SelectedGame.NationalStream.IsAvailable

        '    If SelectedGame.FrenchStream.IsAvailable = False Then
        '        rbFrench.Checked = False
        '    End If

        '    Dim streamIsAvailable As Boolean = SelectedGame.AreAnyStreamsAvailable
        '    btnWatch.Enabled = streamIsAvailable
        '    btnURL.Enabled = streamIsAvailable
        '    gbQuality.Enabled = streamIsAvailable
        '    gbServer.Enabled = streamIsAvailable
        '    gbCDN.Enabled = streamIsAvailable

        'End If

    End Sub

    Private Sub WatchGame(isVOD As Boolean, game As Game, streamType As GameStream.StreamType)



        Dim args As New Game.GameWatchArguments
        args.IsVOD = isVOD
        Dim cdn As String = String.Empty


        If rbLevel3.Checked Then
            args.CDN = "l3c"
        ElseIf rbAkamai.Checked Then
            args.CDN = "akc"
        End If

        If rbQual1.Checked Then
            args.Quality = "224p"
        ElseIf rbQual2.Checked Then
            args.Quality = "288p"
        ElseIf rbQual3.Checked Then
            args.Quality = "360p"
        ElseIf rbQual4.Checked Then
            args.Quality = "504p"
        ElseIf rbQual5.Checked Then
            args.Quality = "540p"
        ElseIf rbQual6.Checked Then
            args.Quality = "720p"
            If chk60.Checked Then
                args.Is60FPS = True
                args.Quality = "best"
            End If
        End If

        If streamType = GameStream.StreamType.Away Then
            args.Stream = game.AwayStream
        ElseIf streamType = GameStream.StreamType.Home Then
            args.Stream = game.HomeStream
        ElseIf streamType = GameStream.StreamType.National Then
            args.Stream = game.NationalStream
        ElseIf streamType = GameStream.StreamType.French Then
            args.Stream = game.FrenchStream
        End If

        args.VLCPath = FileAccess.LocateEXE("vlc.exe", "\VideoLAN\VLC")

        game.Watch(args)


    End Sub


    Private Sub chk60_CheckedChanged(sender As Object, e As EventArgs) Handles chk60.CheckedChanged
        If rbQual6.Checked = False And chk60.Checked = True Then
            rbQual6.Checked = True
        End If
    End Sub

    Private Sub rbQual1_CheckedChanged(sender As Object, e As EventArgs) Handles rbQual1.CheckedChanged
        If rbQual1.Checked = True Then
            chk60.Checked = False
        End If
    End Sub

    Private Sub rbQual2_CheckedChanged(sender As Object, e As EventArgs) Handles rbQual2.CheckedChanged
        If rbQual2.Checked = True Then
            chk60.Checked = False
        End If
    End Sub

    Private Sub rbQual3_CheckedChanged(sender As Object, e As EventArgs) Handles rbQual3.CheckedChanged
        If rbQual3.Checked = True Then
            chk60.Checked = False
        End If
    End Sub

    Private Sub rbQual4_CheckedChanged(sender As Object, e As EventArgs) Handles rbQual4.CheckedChanged
        If rbQual4.Checked = True Then
            chk60.Checked = False
        End If
    End Sub

    Private Sub rbQual5_CheckedChanged(sender As Object, e As EventArgs) Handles rbQual5.CheckedChanged
        If rbQual5.Checked = True Then
            chk60.Checked = False
        End If
    End Sub

    Private Sub btnURL_Click(sender As Object, e As EventArgs) Handles btnURL.Click
        Dim strUrl As String = String.Empty
        Dim strServer As String = String.Empty
        Dim strCDN As String = String.Empty

        If rbLive.Checked Then
            strServer = "LIVE"
        Else
            strServer = "VOD"
        End If

        If rbLevel3.Checked Then
            strCDN = "l3c"
        Else
            strCDN = "akc"
        End If

        If rbHome.Checked Then
            strUrl = gridGames.SelectedRows(0).Cells("home" & strServer).Value.Replace("CDN", strCDN)
        ElseIf rbAway.Checked Then
            strUrl = gridGames.SelectedRows(0).Cells("away" & strServer).Value.Replace("CDN", strCDN)
        ElseIf rbNational.Checked Then
            strUrl = gridGames.SelectedRows(0).Cells("national" & strServer).Value.Replace("CDN", strCDN)
        ElseIf rbFrench.Checked Then
            strUrl = gridGames.SelectedRows(0).Cells("french" & strServer).Value.Replace("CDN", strCDN)
        End If

        Dim dialogURL As New dlURL(strUrl)
        dialogURL.ShowDialog()
    End Sub
End Class
