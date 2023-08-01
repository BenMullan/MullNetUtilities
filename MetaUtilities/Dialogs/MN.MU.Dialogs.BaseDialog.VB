Namespace MetaUtilities.Dialogs
	Partial Public Class BaseDialog : Inherits System.Windows.Forms.Form

		Public Sub New()

			InitializeComponent()

			REM Add an EventHandler to [Shown], to make TheOkButton focused
			AddHandler Me.Shown, Sub() Me.TheOkButton.Focus()

		End Sub

	End Class
End Namespace