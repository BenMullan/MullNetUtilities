Namespace MetaUtilities

	''' <summary>
	''' A lightweight HTTP Server, which can serve either a constant String, single File, or a file from a specified Directory.
	''' 
	''' 	<example>
	''' 		Dim _MicroServer As New MicroHTTPServer(_Port:=9020, _ResponseContentString:="Same for all URLs!", _ResponseMIMEType:="text/plain")
	'''  	 	AddHandler _MicroServer.RequestHandledSuccessfully, Sub(_Request As System.Net.HttpListenerRequest) Console.Out.WriteLine(_Request.Url.ToString())
	'''		 	_MicroServer.Run()
	''' 	</example>
	''' 
	''' 	<example>
	''' 		Dim _MicroServer As New MicroHTTPServer(_Port:=9020, _DirectoryToServeFilesFrom:=New IO.DirectoryInfo("F:\"))
	'''  	 	AddHandler _MicroServer.RequestHandledSuccessfully, Sub(_Request As System.Net.HttpListenerRequest) Console.Out.WriteLine(_Request.Url.ToString())
	'''		 	_MicroServer.Run()
	''' 	</example>
	''' 
	''' </summary>
	Public Class MicroHTTPServer

		''' <summary>Represents a mode of operation for the Server</summary>
		Public Enum MicroHTTPServerMode As [Byte]
			ServeString = 1 : ServeSingleFile = 2 : ServeFilesFromDirectory = 3
		End Enum

		''' <summary>Represents the URL$, UserAgent$, UserHostName$, and HTTPMethod$ or an incomming request</summary>
		Public Structure HTTPRequestSnapshot
			Public URL$, UserAgent$, UserHostName$, UserHostAddress$, HTTPMethod$
			Public Sub New(ByVal _URL$, ByVal _UserAgent$, ByVal _UserHostName$, ByVal _UserHostAddress$, ByVal _HTTPMethod$)
				Me.URL = _URL : Me.UserAgent = _UserAgent : Me.UserHostName = _UserHostName : Me.UserHostAddress = _UserHostAddress : Me.HTTPMethod = _HTTPMethod
			End Sub
		End Structure

		Protected Shared FileEncodingDeterminer_ As Func(Of IO.FileInfo, System.Text.Encoding) =
			Function(_File As IO.FileInfo) As System.Text.Encoding

				Dim _StreamReader As New System.IO.StreamReader(path:=_File.FullName, encoding:=System.Text.Encoding.ASCII, detectEncodingFromByteOrderMarks:=True)
				_StreamReader.Peek() : Return _StreamReader.CurrentEncoding

			End Function

		Protected MIMEType_FromFileExtension_Resolver_ As Func(Of [String], [String]) =
			Function(_FileExtension$) As [String]

				If _FileExtension Is Nothing Then Return MicroHTTPServer.DefaultMIMEType_
				_FileExtension = _FileExtension.ToUpper()

				If Not MicroHTTPServer.MIMETypesDictionary.ContainsKey(_FileExtension) Then Return MicroHTTPServer.DefaultMIMEType_
				Return MicroHTTPServer.MIMETypesDictionary.Item(_FileExtension)

			End Function

		Protected Const DefaultMIMEType_$ = "text/plain"
		Protected ReadOnly HTTPListener_ As New System.Net.HttpListener()
		Protected ReadOnly Mode_ As MicroHTTPServerMode = Nothing
		Protected ReadOnly StringToServe_ As [String], FileToServe_ As IO.FileInfo, DirectoryToServeFilesFrom_ As IO.DirectoryInfo = Nothing

#Region "Public Propities & Events"

		''' <summary>The logical port on which the Web Server listens. E.g. "900" in the URL "http://localhost:900/Help/".</summary>
		Public Port As UInt16 = Nothing

		''' <summary>The URL sub-path on which the Web Server listens. E.g. "Help" in the URL "http://localhost:900/Help/".</summary>
		Public VirtualPath As [String] = Nothing

		''' <summary>The Multipurpose Internet Mail Extensions (MIME) Type describes what the response-bytes' represent. E.g. "text/plain" or "image/bmp". If this is NOTHING, then the MIME Type will be resolved automatically from a file extention where possible.</summary>
		Public ResponseMIMEType As [String] = Nothing

		''' <summary>The Number of requests responded-to by the Web Server instance so-far. This is NOT readonly, so that it can be externally reset.</summary>
		Public SuccessfullyHandledRequestsCount As UInt32 = 0

		''' <summary>Occurs as soon as an incomming HTTP Request is received. The argument is a structure containing request data, including the URL, UserAgent, UserHostName, UserHostAddress, and HttpMethod.</summary>
		Public Event RequestReceived As Action(Of MicroHTTPServer.HTTPRequestSnapshot)

		''' <summary>Occurs directly after a HTTP Request is responded-to. The argument is a structure containing request data, including the URL, UserAgent, UserHostName, UserHostAddress, and HttpMethod.</summary>
		Public Event RequestHandledSuccessfully As Action(Of MicroHTTPServer.HTTPRequestSnapshot)

		''' <summary>Occurs when a request is received, but not responded-to correctly. The argument is an error message.</summary>
		Public Event RequestHandledUnsuccessfully As Action(Of [String])

		''' <summary>A mapping of FILE-EXTENTIONS to MIME-TYPES. E.g. {"HTM", "text/html"}, {"BMP", "image/bmp"}. If a request's File Extension dosen't appear in the Dictionary's Keys, then the default of "text/plain" is used.</summary>
		Public Shared MIMETypesDictionary As New Dictionary(Of [String], [String])() From {
			{"HTM", "text/html"},
			{"HTML", "text/html"},
			{"TXT", "text/plain"},
			{"AVI", "video/x-msvideo"},
			{"BMP", "image/bmp"},
			{"CSS", "text/css"},
			{"CSV", "text/csv"},
			{"ICO", "image/vnd.microsoft.icon"},
			{"GIF", "image/gif"},
			{"JFIF", "image/jpeg"},
			{"JPEG", "image/jpeg"},
			{"JPG", "image/jpeg"},
			{"PNG", "image/png"},
			{"JS", "text/javascript"},
			{"MP3", "audio/mpeg"},
			{"MP4", "video/mp4"},
			{"PDF", "application/pdf"},
			{"WAV", "audio/wav"},
			{"XML", "application/xml"},
			{"ZIP", "application/zip"},
			{"7Z", "application/x-7z-compressed"}
		}

		''' <summary>Serialises the MIMETypesDictionary</summary>
		Public Shared ReadOnly Property SerialisedMIMETypesDictionary As [String]
			Get
				Return [String].Join(", ", MicroHTTPServer.MIMETypesDictionary.ToArray().Select(Of [String])(Function(_ExtensionToMIME As KeyValuePair(Of [String], [String])) _ExtensionToMIME.Key & "→"c & _ExtensionToMIME.Value))
			End Get
		End Property

		''' <summary>E.g. Port=8090 and VirtualPath="Help" would make a URL of http://localhost:8090/Help/</summary>
		Public ReadOnly Property URL As [String]
			Get
				Return [String].Format("http://localhost:{0}/{1}", Me.Port.ToString(), If([String].IsNullOrEmpty(Me.VirtualPath), [String].Empty, If(Me.VirtualPath.EndsWith("/"c), Me.VirtualPath, Me.VirtualPath & "/"c)))
			End Get
		End Property

#End Region

#Region "Constructors for each of the 3 Modes"

		''' <summary>Mode 1: Constructs the MicroHTTPServer to serve a STRING. Example: Port=8090 and VirtualPath="Help" would make a URL of "http://localhost:8090/Help/"</summary>
		Public Sub New(ByVal _Port As UInt16, ByVal _ResponseContentString$, ByVal _ResponseMIMEType$, Optional ByVal _VirtualPath$ = Nothing)
			Me.Mode_ = MicroHTTPServerMode.ServeString : Me.StringToServe_ = _ResponseContentString
			Me.ResponseMIMEType = _ResponseMIMEType : Me.Port = _Port : Me.VirtualPath = _VirtualPath
		End Sub

		''' <summary>Mode 2: Constructs the MicroHTTPServer to serve a SINGLE FILE. If a _ResponseMIMEType is not specified, then the server uses the [MIMETypesDictionary] to derive a MIME-Type from the _FileToServe's Extention. Example: Port=8090 and VirtualPath="Help" would make a URL of "http://localhost:8090/Help/"</summary>
		Public Sub New(ByVal _Port As UInt16, ByVal _FileToServe As IO.FileInfo, Optional ByVal _ResponseMIMEType$ = Nothing, Optional ByVal _VirtualPath$ = Nothing)
			Me.Mode_ = MicroHTTPServerMode.ServeSingleFile : Me.FileToServe_ = _FileToServe
			Me.ResponseMIMEType = _ResponseMIMEType : Me.Port = _Port : Me.VirtualPath = _VirtualPath
		End Sub

		''' <summary>Mode 3: Constructs the MicroHTTPServer to serve FILES FROM A DIRECTORY. If a directory is requested, a dir-listing is generated. If a file is requested, it is served. If a _ResponseMIMEType is not specified, then the server uses the [MIMETypesDictionary] to derive a MIME-Type from the File-To-Serve's Extention.. Example: Port=8090 and VirtualPath="Help" would make a URL of "http://localhost:8090/Help/"</summary>
		Public Sub New(ByVal _Port As UInt16, ByVal _DirectoryToServeFilesFrom As IO.DirectoryInfo, Optional ByVal _ResponseMIMEType$ = Nothing, Optional ByVal _VirtualPath$ = Nothing)
			Me.Mode_ = MicroHTTPServerMode.ServeFilesFromDirectory : Me.DirectoryToServeFilesFrom_ = _DirectoryToServeFilesFrom
			Me.ResponseMIMEType = _ResponseMIMEType : Me.Port = _Port : Me.VirtualPath = _VirtualPath
		End Sub

#End Region

		Protected Sub HandleIncomingConnections_()
			While True
				Try

					REM Will wait here until get a Request
					Dim _HTTPListenerContext As System.Net.HttpListenerContext = Me.HTTPListener_.GetContext()

					REM We now have a Request; Extract the request and response objects
					Dim _Request As System.Net.HttpListenerRequest = _HTTPListenerContext.Request
					Dim _Response As System.Net.HttpListenerResponse = _HTTPListenerContext.Response
					Dim _ResponseContentBytes As [Byte]()

					REM Without a Snapshot object, the _Request would be disposed-of on trying to access it in the actions after _Response.Close()
					Dim _RequestSnapshot As New MicroHTTPServer.HTTPRequestSnapshot(_Request.Url.ToString(), _Request.UserAgent, _Request.UserHostName, _Request.UserHostAddress, _Request.HttpMethod)
					RaiseEvent RequestReceived(_RequestSnapshot)

					REM Prepare the Response, depending on the Instance's current Mode
					Select Case Me.Mode_

						Case MicroHTTPServerMode.ServeString

							_ResponseContentBytes = System.Text.Encoding.UTF8.GetBytes(Me.StringToServe_)
							_Response.StatusCode = Net.HttpStatusCode.OK
							_Response.ContentType = If(Not System.String.IsNullOrEmpty(Me.ResponseMIMEType), Me.ResponseMIMEType, Me.MIMEType_FromFileExtension_Resolver_.Invoke(Nothing))
							_Response.ContentEncoding = System.Text.Encoding.UTF8

						Case MicroHTTPServerMode.ServeSingleFile

							If Not Me.FileToServe_.Exists Then

								_ResponseContentBytes = System.Text.Encoding.UTF8.GetBytes("404" & vbCrLf & "This file cannot be found: " & Me.FileToServe_.FullName)
								_Response.StatusCode = Net.HttpStatusCode.NotFound
								_Response.ContentType = "text/plain"
								_Response.ContentEncoding = System.Text.Encoding.UTF8

							Else

								_ResponseContentBytes = My.Computer.FileSystem.ReadAllBytes(Me.FileToServe_.FullName)
								_Response.StatusCode = Net.HttpStatusCode.OK
								_Response.ContentType = If(Not System.String.IsNullOrEmpty(Me.ResponseMIMEType), Me.ResponseMIMEType, Me.MIMEType_FromFileExtension_Resolver_.Invoke(IO.Path.GetExtension(Me.FileToServe_.FullName)))
								_Response.ContentEncoding = MicroHTTPServer.FileEncodingDeterminer_.Invoke(Me.FileToServe_)

							End If

						Case MicroHTTPServerMode.ServeFilesFromDirectory

							Dim _RequestFileOrDirectoryPath As [String] = IO.Path.Combine(Me.DirectoryToServeFilesFrom_.FullName, If([String].IsNullOrEmpty(Me.VirtualPath), _Request.Url.LocalPath.Skip(1).ToArray(), _Request.Url.LocalPath.Skip(1 + Me.VirtualPath.Length + 1).ToArray()))   'The +1s are to skip over any leading forward slashes. Windows would treat /Dir as the root directory of a drive e.g. C:\Dir\

							If IO.File.GetAttributes(_RequestFileOrDirectoryPath).HasFlag(IO.FileAttributes.Directory) Then

								REM It's an existent Directory; serve a HTML directory listing
								_ResponseContentBytes = System.Text.Encoding.UTF8.GetBytes(MicroHTTPServer.HTMLDirectoryListingFor_(_Directory:=New IO.DirectoryInfo(_RequestFileOrDirectoryPath), _CurrentURL:=_Request.Url.ToString()))
								_Response.StatusCode = Net.HttpStatusCode.OK
								_Response.ContentType = "text/html"
								_Response.ContentEncoding = System.Text.Encoding.UTF8

							ElseIf IO.File.Exists(_RequestFileOrDirectoryPath) Then

								REM It's an existant File; serve it with either the specified MIME Type, or the file-extention-based MIME Type
								_ResponseContentBytes = My.Computer.FileSystem.ReadAllBytes(_RequestFileOrDirectoryPath)
								_Response.StatusCode = Net.HttpStatusCode.OK
								_Response.ContentType = If(Not System.String.IsNullOrEmpty(Me.ResponseMIMEType), Me.ResponseMIMEType, Me.MIMEType_FromFileExtension_Resolver_.Invoke(IO.Path.GetExtension(_RequestFileOrDirectoryPath).Replace("."c, "")))
								_Response.ContentEncoding = MicroHTTPServer.FileEncodingDeterminer_.Invoke(New IO.FileInfo(_RequestFileOrDirectoryPath))

							Else

								REM It's not an existant File or Directory; serve a 404 page
								_ResponseContentBytes = System.Text.Encoding.UTF8.GetBytes("404" & vbCrLf & "This file or directory cannot be found: " & _RequestFileOrDirectoryPath)
								_Response.StatusCode = Net.HttpStatusCode.NotFound
								_Response.ContentType = "text/plain"
								_Response.ContentEncoding = System.Text.Encoding.UTF8

							End If

						Case Else : Throw New Exception("Unrecognised MicroHTTPServerMode: " & Me.Mode_.ToString("G"))
					End Select

					REM Send the Response off
					_Response.ContentLength64 = _ResponseContentBytes.LongLength
					_Response.OutputStream.Write(_ResponseContentBytes, 0, _ResponseContentBytes.Length)
					_Response.Close()

					REM Increment the Counter and Raise the [RequestHandledSuccessfully] Event
					Me.SuccessfullyHandledRequestsCount += 1US
					RaiseEvent RequestHandledSuccessfully(_RequestSnapshot)

				Catch _Ex As Exception : RaiseEvent RequestHandledUnsuccessfully(_Ex.Message) : End Try
			End While
		End Sub

		''' <summary>Initialises the HTTPListener and begins the request-handling loop. Each time a request is handled, the [RequestHandled] Event is raised.</summary>
		Public Sub Run()

			REM Create a Http server and start listening for incoming connections
			Me.HTTPListener_.Prefixes.Add(Me.URL)
			Me.HTTPListener_.Start()

			REM Handle requests (never returns)
			Me.HandleIncomingConnections_()

			REM The listener can be closed by the instanciator of this Class calling .StopRunning()

		End Sub

		''' <summary>Shuts down the request-handling loop.</summary>
		Public Sub StopRunning()
			Me.HTTPListener_.Close()
		End Sub

#Region "Shared Utility Methods"

		Protected Shared ReadOnly Property HTMLDirectoryListingFor_(ByVal _Directory As IO.DirectoryInfo, ByVal _CurrentURL$) As [String]
			Get

				Return (
					<html>
						<head><title>Index Of <%= _Directory.FullName.WithBackslashOnEnd() %></title></head>
						<body>
							<h1>Index Of <%= _Directory.FullName.WithBackslashOnEnd() %></h1>
							<ol>
								<%=
									From _SubDirectory As IO.DirectoryInfo In _Directory.GetDirectories()
									Select <li><a href=<%= _CurrentURL.WithForwardslashOnEnd() & _SubDirectory.Name.WithForwardslashOnEnd() %>><%= _SubDirectory.Name.WithBackslashOnEnd() %></a></li>
								%>
								<%=
									From _File As IO.FileInfo In _Directory.GetFiles()
									Select <li><a href=<%= _CurrentURL.WithForwardslashOnEnd() & _File.Name %>><%= _File.Name %></a></li>
								%>
							</ol>
						</body>
					</html>
				).ToString()

			End Get
		End Property

#End Region

	End Class

End Namespace