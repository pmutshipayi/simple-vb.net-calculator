Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'calc("5+9/-9*5-6/5+26-96-7/4*9")

        For i = 0 To 20
            Dim btn As Button = DirectCast(Controls("button" & i), Button)
            If i = 13 Or i = 12 Or i = 14 Or i = 16 Then
                styleBtn(btn, "operator")
            ElseIf i = 19 Then
                styleBtn(btn, "history")
            ElseIf i = 18 Or i = 20 Then
                styleBtn(btn, "function")
            End If
        Next
        OpenFileDialog1.ShowDialog()
    End Sub
    Sub styleBtn(ByVal btn As Button, ByRef args As String)
        Select Case args
            Case "operator"
                btn.BackColor = Color.SkyBlue
                btn.ForeColor = Color.White
            Case "function"
                btn.BackColor = Color.Red
                btn.ForeColor = Color.White
            Case "history"
                btn.BackColor = Color.Green
                btn.ForeColor = Color.White
        End Select
        btn.Font = New Font("consolas", 12)
    End Sub
    Function calc(ByVal v As String) As Double
        ' prepare the equation
        Dim tot As Double
        Dim rep = v.Replace("+", ",+").Replace("-", ",-")
        Dim prepared As String = ""
        Dim sp() As String = rep.Split(",")
        For i = 0 To sp.Length - 1
            Try
                If sp(i).EndsWith("/") Then
                    prepared += sp(i) & sp(i + 1) & ","
                    i = i + 1
                ElseIf sp(i).EndsWith("*") Then
                    prepared += sp(i) & sp(i + 1) & ","
                    i = i + 1
                Else
                    prepared += sp(i) & ","
                End If
            Catch ex As Exception

            End Try
        Next
        ' calculate
        Dim fsp() As String = prepared.Split(",")
        For i = 0 To fsp.Length - 1
            ' division and multiplication
            If fsp(i).Contains("*") Or fsp(i).Contains("/") Then
                Dim spp() As String = fsp(i).Replace("*", ",*").Replace("/", ",/").Split(",")
                Dim dsum As Double = spp(0)
                For j = 1 To spp.Length - 1
                    If spp(j).Contains("*") Then
                        dsum *= spp(j).Replace("*", "")
                    ElseIf spp(j).Contains("/") Then
                        dsum /= spp(j).Replace("/", "")
                    End If
                Next
                fsp(i) = dsum
            End If
            Try
                tot += fsp(i)
            Catch ex As Exception

            End Try
        Next
        'MsgBox(tot)
        Return tot
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button10.Click, Button11.Click, Button12.Click, Button13.Click, Button14.Click, Button15.Click, Button16.Click, Button17.Click, Button18.Click, Button19.Click, Button20.Click
        Dim btn As Button = sender
        Select Case btn.Text
            Case "AC"
                Label1.Text = "0"
                Label1.ForeColor = Color.Black
            Case "<="
            Case "Exit"
                Me.Close()
            Case "C"
                ListBox1.Items.Clear()
            Case "="
                Try
                    ListBox1.Items.Add(Label1.Text & " = " & calc(Label1.Text))
                    Label1.Text = calc(Label1.Text)
                Catch ex As Exception
                    Label1.Text = "Error : "
                    Label1.ForeColor = Color.Red
                End Try
            Case Else
                Dim txt As String = Label1.Text
                Select Case txt
                    Case "0"
                        Label1.Text = btn.Text
                    Case "Error"
                        Label1.Text = btn.Text
                        Label1.ForeColor = Color.Black
                    Case Else
                        Label1.Text += btn.Text
                End Select
        End Select
    End Sub
    Sub testKey()
        Dim key As Integer = 0
        Do While key < 10
            If key = 10 Then
                Dim dp() As Integer = {1, 5, 74, 85, 89, 32, 45}
                For Each d As Integer In dp
                    MsgBox(d * key / 10)
                Next
            End If
            key = key + 1
        Loop
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        MsgBox(OpenFileDialog1.OpenFile())
    End Sub
End Class
