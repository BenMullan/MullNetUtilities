using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using MullNet.CompilerExtentions;

namespace SmartcardAppLaunch {

	public partial class Form1 : Form {

		public String ProcessToLaunch = "calc.exe";
		public const Byte TargetCardReaderIndex = 0;

		public Form1() { InitializeComponent(); }

		private void Form1_Shown(Object sender, EventArgs e) {

			if (System.Environment.GetCommandLineArgs().Length == 2) {
				this.ProcessToLaunch = Environment.GetCommandLineArgs()[1];
			}

            System.Threading.Tasks.Task.Run(
                () => {

                    global::LibPCSC.PCSCReader _SmartcardReader = new LibPCSC.PCSCReader();
                    _SmartcardReader.CardInserted += this.HandleSmartcardInsertion;

                    // Hang around, until a smartcard is inserted.
                    // The delegate will run on the UI thread.
                    while (true) { Thread.Sleep(100); }
                    
                }
            );

		}

		public void HandleSmartcardInsertion(String _ReaderName, Byte[] _ATR) {

            if (this.InvokeRequired) {
                this.Invoke(new Action(() => HandleSmartcardInsertion(_ReaderName, _ATR)));
                return;
            }
			
			this.InsertSmartcard_InfoLabel.Hide(); this.InsertSmartcard_IconPictureBox.Hide();
			this.Text = "→ " + _ReaderName; this.LaunchingApp_ProgressBar.Show();
			this.Invalidate(); this.Refresh();

			System.Threading.Tasks.Task.Run(
				() => {

					var _SmartcardData = global::MullNet.MetaUtilities.SmartcardUtils.ReadSmartcard(Form1.TargetCardReaderIndex);
					
					this.Invoke(
						new Action(
							() => {
								this.CardholderName_Label.Text = _SmartcardData.CardholderName;
								this.CardholderName_Label.Show();
								this.ShowTrayNotification(_SmartcardData.CardholderName, _SmartcardData.CardProviderName.Split(',')[0] + " card inserted");
								try { System.Diagnostics.Process.Start(this.ProcessToLaunch); } catch (Exception _E) { MessageBox.Show(_E.Message, "On launching"); }
								try { global::PrintToEpson.PrintString(Form1.GetSmartcardAsciiRepresentation(_SmartcardData)); } catch (Exception _E) { MessageBox.Show(_E.Message, "On printing"); }
								Task.Run(() => { System.Threading.Thread.Sleep(1000); System.Environment.Exit(0); });
							}
						)
					);

				}
			);

		}

		public void ShowTrayNotification(String _Title, String _Message) {

			NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon() { Visible = true };
			notifyIcon.Icon = SystemIcons.Information;
			notifyIcon.BalloonTipTitle = _Title;
			notifyIcon.BalloonTipText = _Message;
			notifyIcon.BalloonTipIcon = ToolTipIcon.Info;

			notifyIcon.ShowBalloonTip(3000);
			Task.Delay(3500).ContinueWith(_ => notifyIcon.Dispose());

		}

		public static String GetSmartcardAsciiRepresentation(MullNet.MetaUtilities.SmartcardUtils.SmartcardDataSnapshot _SmartcardData) {

//			const UInt16 _ValueWidth = 32;
//			return (
//$@"
//┌──────────────{'─'.Repeat(_ValueWidth).JoinAsStrings("")}─┐
//│  $ EMV Smartcard {' '.Repeat(_ValueWidth - 4).JoinAsStrings("")} │▒
//│  ^^^^^^^^^^^^{'^'.Repeat(_ValueWidth).JoinAsStrings("")} │▒
//│              {' '.Repeat(_ValueWidth).JoinAsStrings("")} │▒
//│  Cardholder: {_SmartcardData.CardholderName.PadRight(totalWidth: _ValueWidth)} │▒
//│  Provider:   {_SmartcardData.CardProviderName.PadRight(totalWidth: _ValueWidth)} │▒
//│              {' '.Repeat(_ValueWidth).JoinAsStrings("")} │▒
//│  Lang:       {_SmartcardData.CardLanguageID.PadRight(totalWidth: _ValueWidth)} │▒
//│  PSE:        {_SmartcardData.PaymentSystemEnvironment.PadRight(totalWidth: _ValueWidth)} │▒
//│              {' '.Repeat(_ValueWidth).JoinAsStrings("")} │▒
//└──────────────{'─'.Repeat(_ValueWidth).JoinAsStrings("")}─┘▒
// ▒▒▒▒▒▒▒▒▒▒▒▒▒▒{'▒'.Repeat(_ValueWidth).JoinAsStrings("")}▒▒▒
//{"\r\n".Repeat(3).JoinAsStrings("")}"
//			);

			const int totalWidth = 38;
			const int padding = 12;

			string line = "+" + new string('-', totalWidth + 2) + "+";
			string empty = "|" + new string(' ', totalWidth + 2) + "|";

			return string.Join(
				separator: Environment.NewLine,
				new[] {
					line,
					"|  $ EMV Smartcard".PadRight(totalWidth + 3) + "|",
					"|" + new string('^', totalWidth + 2) + "|",
					empty,
					"|  Cardholder: " + _SmartcardData.CardholderName.PadRight(totalWidth - padding) + "|",
					"|  Provider:   " + _SmartcardData.CardProviderName.PadRight(totalWidth - padding) + "|",
					empty,
					"|  Lang:       " + _SmartcardData.CardLanguageID.PadRight(totalWidth - padding) + "|",
					"|  PSE:        " + _SmartcardData.PaymentSystemEnvironment.PadRight(totalWidth - padding) + "|",
					empty,
					line,
					"\r\n\r\n"
				}
			);

		}

	}

}