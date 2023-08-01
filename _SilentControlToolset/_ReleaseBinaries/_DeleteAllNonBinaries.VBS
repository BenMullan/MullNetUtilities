REM MullNet Utilities - Delete all useless files from Binaries-Output-Folder

Set FileSystem = CreateObject("Scripting.FileSystemObject")

For Each File In FileSystem.GetFolder(".").Files
	Dim Extention : Extention = UCase(FileSystem.GetExtensionName(File.Name))
	If Not ((Extention = "EXE") Or (Extention = "DLL") Or (Extention = "VBS")) Then
		File.Delete
	End If
Next