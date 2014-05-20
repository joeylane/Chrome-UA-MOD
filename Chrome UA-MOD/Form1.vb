Imports System.Net
Imports System.Text.RegularExpressions

Public Class Form1

    Private crawlers As String = Nothing
    Private browsers As String = Nothing
    Private mobilebrowsers As String = Nothing
    Private consoles As String = Nothing
    Private offlinebrowsers As String = Nothing
    Private emailclients As String = Nothing
    Private linkcheckers As String = Nothing
    Private emailcollectors As String = Nothing
    Private validators As String = Nothing
    Private feedreaders As String = Nothing
    Private libraries As String = Nothing
    Private cloudplatforms As String = Nothing
    Private others As String = Nothing

    Private selectedClientVersionString As String = Nothing

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim client As New WebClient

            'save the returned data to a string, and also a partially parsed string array
            Dim rawData As String = client.DownloadString("http://www.useragentstring.com/pages/useragentstring.php")
            rawData = rawData.Substring(rawData.IndexOf("<table "), rawData.IndexOf("</table>") - rawData.IndexOf("<table ") + 1)
            Dim data As String() = rawData.Split("<br>")

            'first parse out the categories
            Dim categoryString As String = Nothing
            For Each item As String In data
                'ignore the "ALL" category
                If item.Contains("unterMenuTitel") And Not item.Contains("ALL") Then
                    categoryString = categoryString & item & "$$$"
                End If
            Next
            'add our categories to a string array
            Dim categories As String() = categoryString.Split("$$$")
            'remove empty lines, and add our categories to ComboBox1
            For Each item In categories
                If Not item = Nothing Then
                    ComboBox1.Items.Add(item.Substring(item.LastIndexOf(">") + 1).Trim)
                End If
            Next

            'NOW PARSE OUT THE INDIVIDUAL CLIENTS FOR EACH CATEGORY

            'crawlers
            For Each item As String In rawData.Substring(rawData.IndexOf("CRAWLERS"), rawData.IndexOf("BROWSERS") - rawData.IndexOf("CRAWLERS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    crawlers = crawlers & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'browsers
            For Each item As String In rawData.Substring(rawData.IndexOf("BROWSERS"), rawData.IndexOf("MOBILE BROWSERS") - rawData.IndexOf("BROWSERS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    browsers = browsers & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'mobile browsers
            For Each item As String In rawData.Substring(rawData.IndexOf("MOBILE BROWSERS"), rawData.IndexOf("CONSOLES") - rawData.IndexOf("MOBILE BROWSERS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    mobilebrowsers = mobilebrowsers & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'consoles
            For Each item As String In rawData.Substring(rawData.IndexOf("CONSOLES"), rawData.IndexOf("OFFLINE BROWSERS") - rawData.IndexOf("CONSOLES")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    consoles = consoles & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'offline browsers
            For Each item As String In rawData.Substring(rawData.IndexOf("OFFLINE BROWSERS"), rawData.IndexOf("E-MAIL CLIENTS") - rawData.IndexOf("OFFLINE BROWSERS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    offlinebrowsers = offlinebrowsers & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'email clients
            For Each item As String In rawData.Substring(rawData.IndexOf("E-MAIL CLIENTS"), rawData.IndexOf("LINK CHECKERS") - rawData.IndexOf("E-MAIL CLIENTS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    emailclients = emailclients & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'link checkers
            For Each item As String In rawData.Substring(rawData.IndexOf("LINK CHECKERS"), rawData.IndexOf("E-MAIL COLLECTORS") - rawData.IndexOf("LINK CHECKERS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    linkcheckers = linkcheckers & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'email collectors
            For Each item As String In rawData.Substring(rawData.IndexOf("E-MAIL COLLECTORS"), rawData.IndexOf("VALIDATORS") - rawData.IndexOf("E-MAIL COLLECTORS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    emailcollectors = emailcollectors & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'validators
            For Each item As String In rawData.Substring(rawData.IndexOf("VALIDATORS"), rawData.IndexOf("FEED READERS") - rawData.IndexOf("VALIDATORS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    validators = validators & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'feed readers
            For Each item As String In rawData.Substring(rawData.IndexOf("FEED READERS"), rawData.IndexOf("LIBRARIES") - rawData.IndexOf("FEED READERS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    feedreaders = feedreaders & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'libraries
            For Each item As String In rawData.Substring(rawData.IndexOf("LIBRARIES"), rawData.IndexOf("CLOUD PLATFORMS") - rawData.IndexOf("LIBRARIES")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    libraries = libraries & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'cloud platforms
            For Each item As String In rawData.Substring(rawData.IndexOf("CLOUD PLATFORMS"), rawData.IndexOf("OTHER") - rawData.IndexOf("CLOUD PLATFORMS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    cloudplatforms = cloudplatforms & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

            'others
            For Each item As String In rawData.Substring(rawData.IndexOf("OTHERS"), rawData.IndexOf("</td></tr>") - rawData.IndexOf("OTHERS")).Split("<br /><br />")
                If item.Contains("unterMenuName") Then
                    others = others & item.Substring(item.LastIndexOf(">") + 1).Trim & "$$$"
                End If
            Next

        Catch ex As Exception

            'display error and then quit if we encounter a problem loading
            MessageBox.Show(ex.Message, "Chrome UA-MOD Error")
            End

        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        If ComboBox1.SelectedItem = "CRAWLERS" Then
            ComboBox2.Items.Clear()
            For Each item As String In crawlers.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "BROWSERS" Then
            ComboBox2.Items.Clear()
            For Each item As String In browsers.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "MOBILE BROWSERS" Then
            ComboBox2.Items.Clear()
            For Each item As String In mobilebrowsers.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "CONSOLES" Then
            ComboBox2.Items.Clear()
            For Each item As String In consoles.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "OFFLINE BROWSERS" Then
            ComboBox2.Items.Clear()
            For Each item As String In offlinebrowsers.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "E-MAIL CLIENTS" Then
            ComboBox2.Items.Clear()
            For Each item As String In emailclients.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "LINK CHECKERS" Then
            ComboBox2.Items.Clear()
            For Each item As String In linkcheckers.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "E-MAIL COLLECTORS" Then
            ComboBox2.Items.Clear()
            For Each item As String In emailcollectors.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "VALIDATORS" Then
            ComboBox2.Items.Clear()
            For Each item As String In validators.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "FEED READERS" Then
            ComboBox2.Items.Clear()
            For Each item As String In feedreaders.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "LIBRARIES" Then
            ComboBox2.Items.Clear()
            For Each item As String In libraries.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "CLOUD PLATFORMS" Then
            ComboBox2.Items.Clear()
            For Each item As String In cloudplatforms.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        If ComboBox1.SelectedItem = "OTHERS" Then
            ComboBox2.Items.Clear()
            For Each item As String In others.Split("$$$")
                If Not item = Nothing Then
                    ComboBox2.Items.Add(item.Trim)
                End If
            Next
        End If

        ComboBox2.SelectedIndex = 0

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        If Not ComboBox2.SelectedItem = "" Then

            ComboBox3.Items.Clear()

            Dim client As New WebClient
            Dim rawData As String = client.DownloadString("http://www.useragentstring.com/pages/" & ComboBox2.SelectedItem & "/")
            selectedClientVersionString = rawData.Substring(rawData.IndexOf("<div id='liste'>"), rawData.LastIndexOf("</ul>") - rawData.IndexOf("<div id='liste'>") + 6)
            
            Dim regEx As New Regex("<h4>(?<key>.*?)</h4>")

            For Each m As Match In regEx.Matches(selectedClientVersionString)
                Dim item As String = m.Groups("key").Value
                'item = item.Substring(item.IndexOf(">") + 1, item.LastIndexOf("<") - item.IndexOf(">") - 1)
                ComboBox3.Items.Add(item)
            Next

            ComboBox3.SelectedIndex = 0

        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

        If Not ComboBox3.SelectedItem = "" Then

            ComboBox4.Items.Clear()

            Dim regex1 As New Regex("<h4>" & ComboBox3.SelectedItem & "(?<key>.*?)</ul>")
            Dim regEx2 As New Regex("<li>(?<key>.*?)</li>")


            For Each m As Match In regEx2.Matches(regex1.Match(selectedClientVersionString).Value)
                Dim item As String = m.Groups("key").Value
                item = item.Substring(item.IndexOf(">") + 1, item.LastIndexOf("<") - item.IndexOf(">") - 1)
                ComboBox4.Items.Add(item)
            Next

            ComboBox4.SelectedIndex = 0

            Label5.Text = ComboBox4.Items.Count & " available for this version"

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim launcher As New ProcessStartInfo
        Dim UA As String = ComboBox4.Text
        launcher.FileName = "chrome"
        If CheckBox1.Checked = True Then
            launcher.Arguments = "--incognito --user-agent=" & Chr(34) & UA & Chr(34)
        Else
            launcher.Arguments = "--user-agent=" & Chr(34) & UA & Chr(34)
        End If

        Process.Start(launcher)

    End Sub

End Class
