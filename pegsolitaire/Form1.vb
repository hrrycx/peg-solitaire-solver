Imports System.DirectoryServices.ActiveDirectory
Imports System.Linq.Expressions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar
Imports pegsolitaire.Form1

Public Class Form1
    Public invcolor As Color = Color.Black
    Public empcolor As Color = Color.White
    Public maincolor As Color = Color.Blue
    Public pixel(6, 6) As Label
    Public select1 As Integer = 1
    Public move1 As Integer = 0
    ' these are essentially consts.
    Public invalid() As Int16 = {0, 1, 5, 6, 10, 11, 15, 16, 50, 51, 55, 56, 60, 61, 65, 66}
    Public valid(48) As Int16
    Public empty() As Int16 = {33}
    Public Structure Induvidual
        Dim moves As String()
        Dim score As Integer
    End Structure

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 500
        Timer1.Enabled = False
        pixelassign()
        pixelcolor()
        validassign()
        'genetics()
    End Sub

    Sub pixelassign()
        pixel(0, 0) = Label01
        pixel(0, 1) = Label08
        pixel(0, 2) = Label15
        pixel(0, 3) = Label22
        pixel(0, 4) = Label29
        pixel(0, 5) = Label36
        pixel(0, 6) = Label43

        pixel(1, 0) = Label02
        pixel(1, 1) = Label09
        pixel(1, 2) = Label16
        pixel(1, 3) = Label23
        pixel(1, 4) = Label30
        pixel(1, 5) = Label37
        pixel(1, 6) = Label44

        pixel(2, 0) = Label03
        pixel(2, 1) = Label10
        pixel(2, 2) = Label17
        pixel(2, 3) = Label24
        pixel(2, 4) = Label31
        pixel(2, 5) = Label38
        pixel(2, 6) = Label45

        pixel(3, 0) = Label04
        pixel(3, 1) = Label11
        pixel(3, 2) = Label18
        pixel(3, 3) = Label25
        pixel(3, 4) = Label32
        pixel(3, 5) = Label39
        pixel(3, 6) = Label46

        pixel(4, 0) = Label05
        pixel(4, 1) = Label12
        pixel(4, 2) = Label19
        pixel(4, 3) = Label26
        pixel(4, 4) = Label33
        pixel(4, 5) = Label40
        pixel(4, 6) = Label47

        pixel(5, 0) = Label06
        pixel(5, 1) = Label13
        pixel(5, 2) = Label20
        pixel(5, 3) = Label27
        pixel(5, 4) = Label34
        pixel(5, 5) = Label41
        pixel(5, 6) = Label48

        pixel(6, 0) = Label07
        pixel(6, 1) = Label14
        pixel(6, 2) = Label21
        pixel(6, 3) = Label28
        pixel(6, 4) = Label35
        pixel(6, 5) = Label42
        pixel(6, 6) = Label49

    End Sub
    Sub pixelcolor()
        For y = 0 To 6
            For x = 0 To 6
                pixel(x, y).Size = New System.Drawing.Size(50, 50)
                pixel(x, y).Left = 50 * x
                pixel(x, y).Top = 50 * y
                pixel(x, y).Text = x * 10 + y
                If invalid.Contains(x * 10 + y) Then
                    pixel(x, y).BackColor = invcolor
                ElseIf empty.Contains(x * 10 + y) Then
                    pixel(x, y).BackColor = empcolor
                Else
                    pixel(x, y).BackColor = maincolor
                End If

            Next
        Next
    End Sub
    Sub validassign()
        Dim count As Int16 = 0
        For i = 0 To 66
            If Not (invalid.Contains(i)) And i Mod 10 < 7 Then
                valid(count) = i
                count += 1
            End If
        Next
    End Sub
    Function genrandommoves() As String()
        Dim moves(1000) As String
        For i = 0 To 1000
            moves(i) = genmove()
        Next
        Return moves
    End Function
    Function genmove()
        Dim rand As New Random
        Dim move As String
        Dim x As Integer
        Dim y As Integer
        Randomize()
        x = rand.Next(0, 7)
        y = rand.Next(0, 7)
        If Rnd() > 0.5 Then
            If Rnd() > 0.75 Then
                move = x & y & x + 2 & y + 0
            Else
                move = x & y & x - 2 & y + 0
            End If
        Else
            If Rnd() > 0.25 Then
                move = x & y & x + 0 & y + 2
            Else
                move = x & y & x + 0 & y - 2
            End If
        End If
        Return move
    End Function
    Sub playmoves(moves As String())
        For i = 0 To 1000
            If moveisvalid(moves(i).Substring(0, 2), moves(i).Substring(2, 2)) Then
                makemove(moves(i).Substring(0, 2), moves(i).Substring(2, 2))
            End If
        Next
    End Sub
    Function findpegsleft() As Integer
        For i = 0 To 6
            For j = 0 To 6
                If pixel(i, j).BackColor = maincolor Then
                    findpegsleft += 1
                End If
            Next
        Next
        Return findpegsleft
    End Function
    Function moveisvalid(starts As Int16, ends As Int16) As Boolean
        If Not (valid.Contains(ends) And valid.Contains(starts)) Then
            Return False
        End If

        Dim xdiff As Int16 = Math.Abs(starts \ 10 - ends \ 10)
        Dim ydiff As Int16 = Math.Abs(starts Mod 10 - ends Mod 10)

        If xdiff + ydiff <> 2 Or xdiff = 1 Or ydiff = 1 Then
            Return False
        End If

        If pixel(ends \ 10, ends Mod 10).BackColor <> empcolor Or pixel(starts \ 10, starts Mod 10).BackColor <> maincolor Or pixel((starts \ 10 + ends \ 10) \ 2, (starts Mod 10 + ends Mod 10) \ 2).BackColor <> maincolor Then
            Return False
        End If

        Return True
    End Function
    Sub makemove(starts As Int16, ends As Int16)
        pixel(ends \ 10, ends Mod 10).BackColor = maincolor
        pixel(starts \ 10, starts Mod 10).BackColor = empcolor
        pixel((starts \ 10 + ends \ 10) \ 2, (starts Mod 10 + ends Mod 10) \ 2).BackColor = empcolor
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label01.Click, Label09.Click, Label08.Click, Label07.Click, Label06.Click, Label05.Click, Label49.Click, Label48.Click, Label47.Click, Label46.Click, Label45.Click, Label44.Click, Label43.Click, Label42.Click, Label41.Click, Label40.Click, Label04.Click, Label39.Click, Label38.Click, Label37.Click, Label36.Click, Label35.Click, Label34.Click, Label33.Click, Label32.Click, Label31.Click, Label30.Click, Label03.Click, Label29.Click, Label28.Click, Label27.Click, Label26.Click, Label25.Click, Label24.Click, Label23.Click, Label22.Click, Label21.Click, Label20.Click, Label02.Click, Label19.Click, Label18.Click, Label17.Click, Label16.Click, Label15.Click, Label14.Click, Label13.Click, Label12.Click, Label11.Click, Label10.Click
        Dim number As Integer = sender.name.substring(5, 2) - 1
        number = (number \ 7) + (number Mod 7) * 10
        Console.WriteLine(0)
        If select1 = 1 Then
            If pixel(number \ 10, number Mod 10).BackColor = maincolor Then
                pixel(number \ 10, number Mod 10).ForeColor = Color.Green
                move1 = number
                select1 = 2
            End If
        ElseIf number = move1 Then
            select1 = 1
            pixel(number \ 10, number Mod 10).ForeColor = Color.Black
        Else


            If moveisvalid(move1, number) Then
                makemove(move1, number)
                select1 = 1
                pixel(move1 \ 10, move1 Mod 10).ForeColor = Color.Black
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        pixelcolor()
    End Sub
    Sub genetics()
        Dim iteration As Integer = 0
        Dim population(199) As Induvidual
        birth(population)
        Dim rand As New Random
        Do
            simulate(population)
            If population(0).score = 1 Then
                Exit Do
            End If
            zeroscore(population)
            For i = 0 To 99
                population(i + 100).moves = breeding(population(rand.Next(0, 100)).moves, population(rand.Next(0, 100)).moves)
            Next
            iteration += 1
            Debug.WriteLine(iteration)
        Loop
        pixelcolor()
        MessageBox.Show(population(0).moves.ToString)
        playmoves(population(0).moves)
    End Sub
    Sub birth(ByRef population() As Induvidual)
        For i = 0 To 199
            population(i).moves = genrandommoves()
        Next
    End Sub
    Sub zeroscore(ByRef population() As Induvidual)
        For i = 0 To 199
            population(i).score = 0
        Next
    End Sub
    Sub simulate(ByRef population() As Induvidual)
        Dim lowest As Integer = 32
        Dim sum As Integer = 0
        For i = 0 To 199
            pixelcolor()
            playmoves(population(i).moves)
            population(i).score = findpegsleft()
            If population(i).score < lowest Then
                lowest = population(i).score
            End If
            sum = sum + population(i).score
        Next
        Debug.WriteLine(lowest)
        Debug.WriteLine(sum / population.Length)
        sortpop(population)
    End Sub
    Function writemoves(moves As String()) As String
        Dim output As String = ""
        For i = 0 To 1000
            output = output + moves(i) + "/"
        Next
        Return output
    End Function
    Sub sortpop(ByRef pop() As Induvidual)
        Dim placeholder As Induvidual
        For j = 0 To 199
            For i = 0 To 198
                If pop(i).score > pop(i + 1).score Then
                    placeholder = pop(i + 1)
                    pop(i + 1) = pop(i)
                    pop(i) = placeholder
                End If
            Next
        Next
    End Sub
    Function breeding(parentAmoves As String(), parentBmoves As String())
        Dim childmoves(1000) As String
        For i = 0 To 125
            childmoves(i) = parentAmoves(i)
        Next
        For i = 125 To 250
            childmoves(i) = parentBmoves(i)
        Next
        For i = 250 To 375
            childmoves(i) = parentAmoves(i)
        Next
        For i = 375 To 500
            childmoves(i) = parentBmoves(i)
        Next
        For i = 500 To 625
            childmoves(i) = parentAmoves(i)
        Next
        For i = 625 To 750
            childmoves(i) = parentBmoves(i)
        Next
        For i = 750 To 875
            childmoves(i) = parentAmoves(i)
        Next
        For i = 875 To 1000
            childmoves(i) = parentBmoves(i)
        Next
        Dim rand As New Random
        For i = 0 To 150
            childmoves(rand.Next(0, 1001)) = genmove()
        Next
        Return childmoves
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PictureBox1.Image = grid

        gridGraphics.Clear(Color.Transparent)
        For x = 0 To 6
            For y = 0 To 6
                If pixel(x, y).BackColor = maincolor Then
                    gridGraphics.FillRectangle(Brushes.Blue, x * 10, y * 10, 10, 10)
                ElseIf pixel(x, y).BackColor = empcolor Then
                    gridGraphics.FillRectangle(Brushes.White, x * 10, y * 10, 10, 10)
                End If
            Next
        Next
        Loopers += 1
        Dim endtrue As Boolean = False
        Do
            If findpegsleft() <> 1 Then
                Debug.WriteLine("a")
                cmove = ""
                Do
                    cmove = cmove + stringput(count)
                    count += 1
                Loop Until stringput(count) = "/"
                Debug.WriteLine(cmove)
                count += 1
                If Not (cmove.Contains("-")) Then
                    If moveisvalid(cmove.Substring(0, 2), cmove.Substring(2, 2)) Then
                        Debug.WriteLine("c")
                        endtrue = True
                        makemove(cmove.Substring(0, 2), cmove.Substring(2, 2))
                    End If
                End If
            End If
        Loop Until endtrue
        If findpegsleft() = 1 Then
            gridGraphics.Clear(Color.Transparent)
            For x = 0 To 6
                For y = 0 To 6
                    If pixel(x, y).BackColor = maincolor Then
                        gridGraphics.FillRectangle(Brushes.Blue, x * 10, y * 10, 10, 10)
                    ElseIf pixel(x, y).BackColor = empcolor Then
                        gridGraphics.FillRectangle(Brushes.White, x * 10, y * 10, 10, 10)
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub drawer_Click(sender As Object, e As EventArgs) Handles drawer.Click
        draw()
    End Sub
    Dim Loopers As Integer = 0
    Dim grid As New Bitmap(70, 70)
    Dim gridGraphics As Graphics = Graphics.FromImage(grid)
    Dim stringput As String = "2646/6686/3212/2022/0121/4464/5153/1636/4547/4244/5636/6080/2000/5250/2628/14-14/4446/3111/4121/5351/3616/2303/06-26/3638/3133/1214/3133/2606/6040/6444/4042/1513/12-12/5535/0222/0608/1517/1517/03-23/6668/6444/5351/5232/0020/6141/3537/0626/2527/3656/2325/4323/5232/06-26/3616/4020/3151/5030/2527/5052/4547/2321/6567/6545/2527/0301/5658/0608/5434/5654/5153/3638/1315/4442/4626/3414/1214/4525/4525/303-2/2325/3335/5535/5232/2202/4042/2101/4042/4648/5052/1311/4442/16-16/4020/6365/2141/04-24/02-22/2101/4042/6668/5676/6646/4626/4244/3234/000-2/5553/3032/5351/3252/6444/3555/1232/02-22/4446/5131/05-25/6343/616-1/0608/3537/2303/6040/2505/2141/4525/2628/5355/3454/11-11/2404/2022/5030/5232/2123/3133/5232/2527/2646/5557/2123/2325/313-1/4323/5333/11-11/6484/5333/6563/11-11/14-14/4121/6686/2000/0002/6668/1517/1416/4244/12-12/6444/6141/5333/5434/2628/0204/4626/12-12/2545/2523/16-16/6484/2303/1210/4042/00-20/4547/1311/6062/4525/2505/3032/3212/3432/6242/3414/0002/0002/4341/4446/0222/2224/06-26/3533/6264/0103/4525/2624/4143/1333/6260/3234/4626/4042/000-2/5131/3515/6466/1614/2303/6040/4547/5434/2101/2224/6646/5030/5636/5434/1113/4341/2325/6264/3638/0608/1012/3032/3533/4525/0103/4121/3331/1416/0507/4244/3252/3032/6242/4323/0103/2527/4547/3212/505-2/0608/4442/3515/0103/0424/4525/5373/4222/6646/5676/6462/1232/3537/6062/5557/14-14/4446/4424/0301/5373/6141/1513/3335/4121/10-10/3533/6664/5131/6062/616-1/3638/4648/16-16/6141/2606/5553/4121/12-12/5131/2628/03-23/03-23/5254/6466/5333/6242/13-13/0103/4525/4222/3313/6141/4446/3111/3010/1131/0103/101-2/0002/4424/6242/14-14/6141/4424/2606/1214/4121/2000/4240/3133/2426/2022/6462/606-2/4222/4042/0121/3432/5030/2325/0200/1434/2505/15-15/5030/2321/06-26/6163/0604/6567/4547/5658/6567/2606/6686/0525/3335/6040/3234/6365/5333/6264/6163/2343/5052/0103/3335/3533/5456/5131/0323/00-20/4121/02-22/6141/3331/505-2/4222/2624/414-1/3234/4648/2624/6260/03-23/202-2/4020/1513/0507/2202/0204/5272/6141/5456/06-26/15-15/2646/6686/3414/2545/1131/5553/4222/2022/0222/5333/14-14/5070/0323/5171/2000/4240/0604/000-2/12-12/5131/6545/3515/1012/2646/1214/0402/4666/6462/0406/4121/0103/3111/0608/3111/6668/3634/6062/0424/3414/6567/2202/0608/05-25/5052/4626/2505/6646/1434/4262/4121/4222/0103/4626/3638/3414/4121/2224/2141/5434/0525/1513/2101/6484/0002/4363/6646/6545/2426/0604/6062/4020/14-14/0608/6462/6242/2022/3313/2523/12-12/3133/5658/6163/4666/2343/2624/0604/3230/5131/1614/3111/2545/5654/3234/2321/5636/3454/6668/6062/3414/414-1/0103/3111/3133/05-25/01-21/2202/2101/4626/6264/00-20/6242/5131/2444/0402/4222/2242/14-14/4323/2000/3010/6668/5232/3133/3331/3335/0222/02-22/4626/6567/4060/2224/2505/6567/6260/6567/0424/6062/4240/1412/6040/0301/0305/3414/5052/3313/2527/5351/1618/2628/6365/0424/5557/1214/2123/2325/6545/6264/1214/6646/5131/3234/2527/6242/0406/3537/2303/3331/06-26/01-21/6264/6365/6062/4240/6260/1517/3656/02-22/6062/1535/313-1/1434/5456/4345/0305/0002/6062/6668/5030/4121/3537/4424/1012/5434/0503/5658/1012/6383/303-2/5355/3533/5333/5557/3638/11-11/5456/4060/1517/4222/5030/4666/3616/4060/6163/0204/010-1/3111/4446/0204/2505/06-26/5658/2202/0002/06-26/5452/2101/4525/01-21/3537/5373/10-10/5250/4446/11-11/5575/6163/6462/0002/15-15/2242/1232/2202/0402/4525/4323/0507/5654/3656/3515/2545/5153/2628/4442/2123/3537/5535/2527/5153/1210/3515/4464/4464/12-12/3212/4143/4648/2426/4121/5355/0626/4547/0020/05-25/01-21/5636/2527/04-24/6163/3335/14-14/2303/4464/6181/404-2/5658/2123/2202/2141/4244/1517/3133/3638/6585/6545/0200/6646/6664/4345/6282/05-25/3331/1618/5456/1416/5658/6444/2404/2505/3537/5355/6563/3111/1315/4644/3515/2022/4323/5250/303-2/4424/12-12/6080/5153/4626/3313/5658/5575/2220/5232/5232/313-1/5254/3414/4060/6567/4442/2022/12-12/6242/6444/5456/5052/4648/5232/1517/6668/2404/4244/06-26/1012/4240/4525/6040/5658/4060/4222/1113/6444/2224/4442/4626/5232/6264/1412/1113/2123/6484/6242/1618/3414/1614/2545/2505/0002/03-23/2022/2444/5658/3050/1618/6264/2646/0222/0323/2628/02-22/13-13/5474/0305/3111/5535/4060/3537/4244/2444/4323/3010/6365/6163/4240/0608/3212/6545/3414/2101/6264/2123/1434/0103/1333/6062/2527/16-16/0103/5658/0608/4345/3010/2426/6646/3032/3414/6361/0002/4042/0222/6062/6444/2202/06-26/4244/2224/2303/5658/0626/1311/2123/3010/5232/6466/2505/0204/3111/2404/4121/5355/3010/3515/15-15/0424/5434/1535/1210/0103/3515/5535/4363/1636/3353/3533/5333/3335/5557/0305/0200/0305/2321/02-22/0305/4644/4464/5456/5535/2224/10-10/3212/01-21/2628/4262/6444/6163/3111/3212/6567/1315/5355/5232/6260/2527/4644/2022/5535/5254/0002/2624/13-13/0608/2303/4446/2426/2202/1517/4143/4648/5452/4547/3436/0507/4648/1315/1517/4121/0507/2224/2202/15-15/3133/5171/5434/6646/4648/2505/14-14/6163/12-12/0525/5333/2220/3335/3353/2527/11-11/0406/4222/6062/11-11/5250/6365/0002/2325/6444/6466/3234/3616/1517/2141/2123/0103/6545/2505/5232/6361/2101/2123/616-1/5333/5535/4648/0507/0002/04-24/6080/2202/3616/12-12/0121/2628/0002/6365/5636/101-2/4442/5434/3212/3616/0507/5333/1434/0608/02-22/3515/6466/3638/2224/6365/5654/10-10/0608/0103/2343/2444/4525/6163/1618/4543/4323/3032/12-12/202-2/3515/3436/1012/6646/16-16/2101/2444/5456/6141/6181/0503/2523/4323/313-1/3537/5658/6563/0222/2606/1131/4121/14-14/"
    Dim copy As String = stringput
    Dim cmove As String = ""
    Dim count As Integer = 0
    Sub draw()
        Timer1.Enabled = True
    End Sub
    Private Sub genesis_Click(sender As Object, e As EventArgs) Handles genesis.Click
        genetics()
    End Sub
End Class
