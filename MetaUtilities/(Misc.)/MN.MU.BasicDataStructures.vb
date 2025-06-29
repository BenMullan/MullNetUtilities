Namespace MetaUtilities

	''' <summary> Contains useful rudimentry generic data-structures. </summary>
	Public Module BasicDataStructures

		''' <summary> Represents a node in a recursive tree. </summary>
		Public Class TreeNode

			Public Property Name As [String] = ""
			Public Property Value As [String] = ""
			Public Property Children As New List(Of TreeNode)()

			''' <summary>Returns each child on its own line</summary>
			Public Overrides Function ToString() As [String]

				Return [String].Format(
					"{0}={1}{2}{3}", {
						Me.Name,
						Me.Value,
						vbCrLf,
						[String].Join(vbCrLf, Me.Children.Select(Of [String])(Function(_TreeNode As TreeNode) vbTab & _TreeNode.ToString()))
					}
				)

			End Function

			''' <summary>Returns e.g. <![CDATA[  <TreeNode Name="Smartcard" Value="Somat"> <TreeNode ... /> </TreeNode>  ]]> </summary>
			Public Function ToXML() As XElement
				Return (<TreeNode Name=<%= Me.Name %> Value=<%= Me.Value %>/>).WithChildren(Me.Children.Select(Of XElement)(Function(_Child As TreeNode) _Child.ToXML()))
			End Function

			''' <summary>
			''' The returned list includes the Name and Value of THIS TreeNode, and all its Children, recursively.
			''' </summary>
			'''	<returns>
			'''		<example>
			'''			Top = Value1
			'''			Child = Value2
			'''			GrandChild = Value3
			'''		</example>
			'''	</returns>
			Public ReadOnly Property AsFlatData As KeyValuePair(Of [String], [String])()
				Get

					Dim _Collected_KeyValuePairs As New List(Of KeyValuePair(Of [String], [String]))() From {
						New KeyValuePair(Of [String], [String])(Me.Name, Me.Value)
					}

					For Each _Child As TreeNode In Me.Children
						_Collected_KeyValuePairs.Add(New KeyValuePair(Of [String], [String])(_Child.Name, _Child.Value))
						_Collected_KeyValuePairs.AddRange(_Child.AsFlatData)
					Next

					Return _Collected_KeyValuePairs.ToArray()

				End Get
			End Property

			''' <summary>
			''' The returned list includes the Name and Value of THIS TreeNode, and all its Children, recursively.
			''' The key of each KVP is fully-qualified with a dot-joined heirarchical path to the root node.
			''' </summary>
			'''	<returns>
			'''		<example>
			'''			[Top]							= Value1
			'''			[Top] . [Child]					= Value2
			'''			[Top] . [Child] . [GrandChild]	= Value3
			'''		</example>
			'''	</returns>
			Public ReadOnly Property AsFlatHeirarchicalData As KeyValuePair(Of [String], [String])()
				Get

					Dim _Collected_KeyValuePairs As New List(Of KeyValuePair(Of [String], [String]))() From {
						New KeyValuePair(Of [String], [String])($"[Me.Name]", Me.Value)
					}

					For Each _Child As TreeNode In Me.Children
						_Collected_KeyValuePairs.Add(New KeyValuePair(Of [String], [String])($"[{Me.Name}] . [{_Child.Name}]", _Child.Value))
						_Collected_KeyValuePairs.AddRange(_Child.AsFlatHeirarchicalData)
					Next

					Return _Collected_KeyValuePairs.ToArray()

				End Get
			End Property

		End Class

	End Module

End Namespace