Public Class frm_main
    Private Sub SuspendProcess(ByVal process As System.Diagnostics.Process)
        For Each t As ProcessThread In process.Threads
            Dim th As IntPtr
            th = OpenThread(ThreadAccess.SUSPEND_RESUME, False, t.Id)
            If th <> IntPtr.Zero Then
                SuspendThread(th)
                CloseHandle(th)
            End If
        Next
    End Sub

    Private Sub ResumeProcess(ByVal process As System.Diagnostics.Process)
        For Each t As ProcessThread In process.Threads
            Dim th As IntPtr
            th = OpenThread(ThreadAccess.SUSPEND_RESUME, False, t.Id)
            If th <> IntPtr.Zero Then
                ResumeThread(th)
                CloseHandle(th)
            End If
        Next
    End Sub

    Private Sub MainTimer_Tick(sender As Object, e As EventArgs) Handles MainTimer.Tick
        Dim game As Process() = Process.GetProcessesByName("GTA5")
        ResumeProcess(game(0))
        MainTimer.Enabled = False
        End
    End Sub

    Private Sub frm_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hotkey.registerHotkey(Me, "F", 1, Hotkey.KeyModifier.Control + Hotkey.KeyModifier.Alt)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = Hotkey.WM_HOTKEY Then
            Dim game As Process() = Process.GetProcessesByName("GTA5")
            If game.Length = 0 Then
                MsgBox("Launch GTA V first!", MsgBoxStyle.Critical, "GTA V not found!")
            ElseIf game.Length = 1 Then
                SuspendProcess(game(0))
                MainTimer.Enabled = True
            End If
        End If
        MyBase.WndProc(m)
    End Sub 'System wide hotkey event handling
End Class
