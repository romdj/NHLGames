﻿Namespace Utilities

    Public Enum GameStateEnum
#Disable Warning InconsistentNaming
        Undefined = 0
        Scheduled = 1
        Pregame = 2
        InProgress = 3
        Ending = 4
        Ended = 5
        Unknown1 = 6
        Final = 7
        Unknown2 = 8
        Postponed = 9
        Unknown3 = 10
    End Enum

    Public Enum GameTypeEnum
        Preseason = 1
        Season = 2
        Series = 3
    End Enum

    Public Enum StreamType
        None = 0
        Away
        Home
        National
        French
        EndzoneCam1
        EndzoneCam2
        MultiCam1
        MultiCam2
        RefCam
        StarCam
    End Enum

    Public Enum CdnType
        Akc = 0
        L3C
    End Enum

    Public Enum StreamQuality
        best = 0
        _720p = 1
        _540p = 2
        _504p = 3
        _360p = 4
        _288p = 5
        _224p = 6
        worst = 7
    End Enum

    Public Enum PlayerTypeEnum
        None = 0
        Vlc = 1
        Mpc = 2
        Mpv = 3
    End Enum

    Public Enum SettingsEnum
        Version = 1
        DefaultWatchArgs = 2
        VlcPath = 3
        MpcPath = 4
        MpvPath = 5
        StreamerPath = 6
        ServerList = 7
        ShowScores = 8
        SelectedServer = 9
        SelectedLanguage = 10
        ShowLiveScores = 11
        ShowSeriesRecord = 12
        LanguageList = 13
        AdDetection = 14
        ShowTeamCityAbr = 15
        ShowTodayLiveGamesFirst = 16
        LastWindowSize = 17
    End Enum

    Public Enum OutputType
        Normal = 0
        Status = 1
        [Error] = 2
        Warning = 3
        Cli = 4
    End Enum

    Public Enum AdModulesEnum
        Spotify = 1
        Obs
    End Enum

    Public Enum StreamerTypeEnum
        None = 0
        LiveStreamer
        StreamLink
    End Enum

    Public Enum WindowsCode
        WM_NCLBUTTONDOWN = &HA1
        HTLEFT = 10
        HTRIGHT = 11
        HTTOP = 12
        HTTOPLEFT = 13
        HTTOPRIGHT = 14
        HTBOTTOM = 15
        HTBOTTOMLEFT = 16
        HTBOTTOMRIGHT = 17
        VKMNEXT = 176
        VKMPREVIOUS = 177
    End Enum

    Public Enum ShowWindowCode
        SW_HIDE = 0
        SW_SHOWNORMAL = 1
        SW_SHOWMINIMIZED = 2
        SW_MAXIMIZE = 3
        SW_SHOWNOACTIVATE = 4
        SW_SHOW = 5
        SW_MINIMIZE = 6
        SW_SHOWMINNOACTIVE = 7
        SW_SHOWNA = 8
        SW_RESTORE = 9
    End Enum

    Public Enum LiveReplayCode
        Live = 0
        _5 = 1
        _10 = 2
        _30 = 3
        Full = 4
    End Enum
#Enable Warning InconsistentNaming
End Namespace
