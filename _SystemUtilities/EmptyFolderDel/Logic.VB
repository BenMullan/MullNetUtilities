Public Module EFDLogic

	Public AccessDenied_OrOtherError_DuringFolderSearch_Count As UInt32 = 0

	Public Function MakeSureThereIsBackslashOnEnd$(ByVal _Path$)
		Return If(_Path.EndsWith("\"c), _Path, _Path & "\"c)
	End Function

	Public Function DirectoryIsEmpty(ByVal _Directory As IO.DirectoryInfo) As Boolean
		Try
			Return (_Directory.GetFiles().Length = 0) AndAlso (_Directory.GetDirectories().Length = 0)
		Catch _Ex As Exception When True
			Return False
		End Try
	End Function

	Public Function GetSubFoldersWhere(ByVal _Folder As IO.DirectoryInfo, ByVal _SubFolderMustBeEmpty As Boolean, ByVal _SubFolderNameRegEx$) As IO.DirectoryInfo()

		Try

			If Not _Folder.Exists Then Throw New IO.DirectoryNotFoundException("This Directory does not exist: " & _Folder.FullName)
			Dim _SubFolders_OnCurrentLevel As New List(Of IO.DirectoryInfo)()

			For Each _SubFolder As IO.DirectoryInfo In _Folder.GetDirectories()
				Try

					REM Possible Combinations:
					'_SubFolderMustBeEmpty=True		_SubFolder is Empty					Add _SubFolder
					'_SubFolderMustBeEmpty=False	_SubFolder is Empty					Add _SubFolder
					'_SubFolderMustBeEmpty=True		_SubFolder is not Empty				Add Empty Children of _SubFolder, but NOT _SubFolder itself
					'_SubFolderMustBeEmpty=False	_SubFolder is not Empty				Add Empty Children of _SubFolder, AND _SubFolder itself

					If DirectoryIsEmpty(_SubFolder) Then
						'The Directory is empty; add _SubFolder if it matches the RegEx
						If System.Text.RegularExpressions.Regex.IsMatch(input:=_SubFolder.Name, pattern:=_SubFolderNameRegEx) Then _SubFolders_OnCurrentLevel.Add(_SubFolder)
					Else
						'The Directory isn't empty; add its childer, but only add it, if NOT _SubFolderMustBeEmpty
						_SubFolders_OnCurrentLevel.AddRange(GetSubFoldersWhere(_SubFolder, _SubFolderMustBeEmpty, _SubFolderNameRegEx).ToArray())
						If (Not _SubFolderMustBeEmpty) AndAlso System.Text.RegularExpressions.Regex.IsMatch(input:=_SubFolder.Name, pattern:=_SubFolderNameRegEx) Then _SubFolders_OnCurrentLevel.Add(_SubFolder)
					End If

				Catch _Ex As System.[Exception] When True
					'We could be here because E.g. Access to the Dir was deneid; don't add it to the list
					Throw _Ex
				End Try
			Next

			Return _SubFolders_OnCurrentLevel.ToArray()

		Catch _Ex As Exception When True
			'MsgBox("Exception during Recursive Directory Search of [" & _Folder.FullName & "]: " & vbCrLf & vbCrLf & _Ex.Message & vbCrLf & vbCrLf & "(The search will continue onto the next parent folder...)", MsgBoxStyle.Critical Or MsgBoxStyle.SystemModal Or MsgBoxStyle.MsgBoxSetForeground, _Ex.GetType().FullName)
			EFDLogic.AccessDenied_OrOtherError_DuringFolderSearch_Count += 1UI
			Return (New List(Of IO.DirectoryInfo)()).ToArray()
		End Try

	End Function

End Module