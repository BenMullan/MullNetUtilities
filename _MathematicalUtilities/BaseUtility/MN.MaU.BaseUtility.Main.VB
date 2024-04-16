Friend Module BaseUtility

	<CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification:="Part of proper Entrypoint signature!")>
	Public Function Main%(ByVal _CLAs$())

		While True
			Try
				Console.Write("<Value> <Tokens> ")
				Dim _Value_And_Tokens$() = Console.ReadLine().Split(" "c)
				Console.WriteLine([UsefulMethods].IncrementNumericalValue(_Value_And_Tokens.ElementAt(0), _Value_And_Tokens.ElementAt(1)))
			Catch _Ex As Exception : UsefulMethods.ConsoleErrorWriteLineInColour(_Ex.Message, ConsoleColor.Red) : End Try
		End While

		Return 0
	End Function

End Module