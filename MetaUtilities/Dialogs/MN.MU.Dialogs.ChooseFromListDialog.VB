Namespace MetaUtilities.Dialogs

	''' <summary>
	''' Enables one of a selection of Objects to be chosen, via a GUI.
	''' 
	''' Example:
	'''     Dim _ChooseCameraDialog As New MullNet.MetaUtilities.Dialogs.ChooseFromListDialog(Of DirectShowLib.DsDevice)(_CameraDeviceProvider.Devices.ToArray(), "Camera Device", Function(_Camera As DirectShowLib.DsDevice) _Camera.Name)
	'''     'User could have clicked [Cancel] or pressed [Esc]
	'''     If Not ((_ChooseCameraDialog.ShowDialog() = (Windows.Forms.[DialogResult]).OK) AndAlso (_ChooseCameraDialog.ChosenItem IsNot Nothing)) Then Application.Exit()
	''' </summary>
	Partial Public Class ChooseFromListDialog(Of TEachItem) : Inherits MullNet.MetaUtilities.Dialogs.BaseDialog

		REM Input
		Public ReadOnly ItemsToChooseFrom As ObjectModel.ReadOnlyCollection(Of TEachItem)

		REM Output
		Public ReadOnly Property ChosenItem As TEachItem
			Get
				If Me.ItemsToChooseFrom.Count = 0 Then Return Nothing
				Return Me.ItemsToChooseFrom.ElementAt(Me.TheItemsToChooseFromComboBox.SelectedIndex)
			End Get
		End Property

		Protected WriteOnly Property EachItemIsA_Text_ As [String]
			Set(_NewValue$)
				Me.Text = "Choose " & _NewValue
				Me.TheDescriptionLabel.Text = "Select the " & _NewValue & " to use from the list..."
			End Set
		End Property

		''' <summary>Prepares a New ChooseFromListDialog for use</summary>
		''' <param name="_ItemsToChooseFrom">The ToString() of each item is added to the ComboBox</param>
		''' <param name="_EachItemIsA_Text">E.g. "Camera"</param>
		Public Sub New(ByRef _ItemsToChooseFrom As TEachItem(), ByVal _EachItemIsA_Text$, Optional ByVal _CustomItemToStringFunction As Func(Of TEachItem, [String]) = Nothing)

			Me.InitializeComponent()
			Me.ItemsToChooseFrom = New ObjectModel.ReadOnlyCollection(Of TEachItem)(_ItemsToChooseFrom.ToList())
			Me.EachItemIsA_Text_ = _EachItemIsA_Text

			Me.TheItemsToChooseFromComboBox.Items.AddRange(Me.ItemsToChooseFrom.Select(Of String)(If(_CustomItemToStringFunction, AddressOf Convert.ToString)).ToArray())
			If (Me.ItemsToChooseFrom.Count > 0) Then Me.TheItemsToChooseFromComboBox.SelectedIndex = 0

		End Sub

	End Class

End Namespace