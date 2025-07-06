using System;
using System.Runtime.InteropServices;

public static class PrintToEpson {

	public static void PrintString(String message) {

		// Find the printer name (change as needed)
		string printerName = "EPSON TM-T88IV Receipt";

		// ESC/POS: Add line feed and cut command
		string escposMessage = message + "\n\n\n" + "\x1D\x56\x00";

		// Convert to bytes (Epson expects ASCII)
		byte[] bytes = System.Text.Encoding.ASCII.GetBytes(escposMessage);

		// Send to printer
		RawPrinterHelper.SendBytesToPrinter(printerName, bytes);
	}

	// Helper class for raw printing
	public static class RawPrinterHelper {

		[DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true)]
		public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

		[DllImport("winspool.Drv", EntryPoint = "ClosePrinter")]
		public static extern bool ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true)]
		public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In] ref DOCINFOA di);

		[DllImport("winspool.Drv", EntryPoint = "EndDocPrinter")]
		public static extern bool EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "StartPagePrinter")]
		public static extern bool StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "EndPagePrinter")]
		public static extern bool EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "WritePrinter")]
		public static extern bool WritePrinter(IntPtr hPrinter, byte[] pBytes, int dwCount, out int dwWritten);

		[StructLayout(LayoutKind.Sequential)]
		public struct DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)]
			public string pDocName;
			[MarshalAs(UnmanagedType.LPStr)]
			public string pOutputFile;
			[MarshalAs(UnmanagedType.LPStr)]
			public string pDataType;
		}

		public static bool SendBytesToPrinter(string printerName, byte[] bytes)
		{
			IntPtr hPrinter;
			DOCINFOA di = new DOCINFOA
			{
				pDocName = "ESC/POS Print",
				pDataType = "RAW"
			};
			if (!OpenPrinter(printerName.Normalize(), out hPrinter, IntPtr.Zero))
				return false;
			bool success = false;
			if (StartDocPrinter(hPrinter, 1, ref di))
			{
				if (StartPagePrinter(hPrinter))
				{
					int dwWritten = 0;
					success = WritePrinter(hPrinter, bytes, bytes.Length, out dwWritten);
					EndPagePrinter(hPrinter);
				}
				EndDocPrinter(hPrinter);
			}
			ClosePrinter(hPrinter);
			return success;
		}
	}

}