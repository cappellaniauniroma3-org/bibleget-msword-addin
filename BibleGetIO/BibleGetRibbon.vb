﻿Imports Microsoft.Office.Tools.Ribbon
Imports System.Diagnostics
Imports System.Globalization

Public Class BibleGetRibbon

    Private preferencesForm As Preferences
    Private insertQuoteForm As InsertQuoteDialog
    Private aboutBibleGetForm As AboutBibleGet
    Private helpForm As BibleGetHelp
    Private progressBarForm As QuoteFromSelectProgressBar
    Private feedBackForm As Feedback
    Private bibleGetDB As BibleGetDatabase
    Public BibleVersionForSearch As String
    Public TermToSearch As String

    Private Shared Function __(ByVal myStr As String, ByVal locale As CultureInfo) As String
        Dim myTranslation As String = BibleGetAddIn.RM.GetString(myStr, locale)
        If Not String.IsNullOrEmpty(myTranslation) Then
            Return myTranslation
        Else
            Return myStr
        End If
    End Function

    Private Sub BibleGetRibbon_Close(sender As Object, e As EventArgs) Handles Me.Close
        If preferencesForm IsNot Nothing Then preferencesForm.Dispose()
        If insertQuoteForm IsNot Nothing Then insertQuoteForm.Dispose()
        If aboutBibleGetForm IsNot Nothing Then aboutBibleGetForm.Dispose()
        If helpForm IsNot Nothing Then helpForm.Dispose()
        If progressBarForm IsNot Nothing Then progressBarForm.Dispose()
        If feedBackForm IsNot Nothing Then feedBackForm.Dispose()
    End Sub

    Private Sub BibleGetRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load
        Dim Application As Word.Application = Globals.BibleGetAddIn.Application
        Dim lang As Office.MsoLanguageID = Application.LanguageSettings.LanguageID(Office.MsoAppLanguageID.msoLanguageIDUI)
        Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang)
        Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang)
        Dim locale As CultureInfo = CultureInfo.GetCultureInfo(lang)
        InsertBibleQuoteFromDialogBtn.Label = __("Insert quote from input window", locale)
        InsertBibleQuoteFromTextSelectionBtn.Label = __("Insert quote from text selection", locale)
        PreferencesBtn.Label = __("User Preferences", locale)
        HelpBtn.Label = __("Help", locale)
        SendFeedbackBtn.Label = __("Send feedback", locale)
        MakeContributionBtn.Label = __("Contribute", locale)
        AboutBtn.Label = __("About this plugin", locale)
        SearchBtn.Label = __("Search for verses by keyword", locale)
        bibleGetDB = New BibleGetDatabase
        If bibleGetDB.IsInitialized Then
            StatusBtn.Image = My.Resources.green_checkmark
            StatusBtn.Label = "STATUS: READY"
        End If
    End Sub


    Private Sub InsertBibleQuoteFromDialogBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles InsertBibleQuoteFromDialogBtn.Click
        insertQuoteForm = New InsertQuoteDialog
        insertQuoteForm.Show()
    End Sub

    Private Sub InsertBibleQuoteFromTextSelectionBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles InsertBibleQuoteFromTextSelectionBtn.Click
        progressBarForm = New QuoteFromSelectProgressBar
        progressBarForm.Show()
    End Sub

    Private Sub PreferencesBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles PreferencesBtn.Click
        preferencesForm = New Preferences
        preferencesForm.Show()
    End Sub

    Private Sub HelpBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles HelpBtn.Click
        helpForm = New BibleGetHelp
        helpForm.Show()
        'oForm.ShowDialog()
    End Sub

    Private Sub SendFeedbackBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles SendFeedbackBtn.Click
        feedBackForm = New Feedback
        feedBackForm.Show()
    End Sub

    Private Sub MakeContributionBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles MakeContributionBtn.Click
        Dim webAddress As String = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=HDS7XQKGFHJ58"
        Process.Start(webAddress)
    End Sub

    Private Sub AboutBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles AboutBtn.Click
        aboutBibleGetForm = New AboutBibleGet
        aboutBibleGetForm.Show()
    End Sub

    Private Sub StatusBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles StatusBtn.Click
        Dim oForm As HealthStatus = New HealthStatus
        oForm.Show()
    End Sub


    Private Sub SearchBtn_Click(sender As Object, e As RibbonControlEventArgs) Handles SearchBtn.Click
        Dim oForm As BibleGetSearchResults = New BibleGetSearchResults
        oForm.Show()
    End Sub
End Class
