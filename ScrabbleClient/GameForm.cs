using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using ScrabbleEngine;

namespace ScrabbleClient
{
    public partial class GameForm : Form
    {
        private BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
        private BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
        private Hashtable clientProperties = new Hashtable();
        private IPAddress serverIP;
        private int serverPort;
        private string serverURI;        
        private TcpChannel tcpChannel;
        private IServer remoteServer;
        private Random random = new Random();
        private int playerId;
        private string playerName;
        private int playerCode;
        private EventProxy eventProxy;
        private Hand playerHand;
        private ReadOnlyCollection<Player> players;
        private Board board;
        private bool isPlaying;
        private bool gameIsTerminated;
        private Move currentMove;
        private int? handTileSelection = null; 
        private Graphics boardGraphics;
        private Graphics handGraphics;
        private readonly Font tileLetterFont = new Font("Arial", 15);
        private readonly Font tileValueFont = new Font("Arial", 6);
        private StringFormat tileLetterStringFormat = new StringFormat();
        private StringFormat tileValueStringFormat = new StringFormat();
        private Brush tileBrush = Brushes.OldLace;
        private Brush tileExchangeBrush = Brushes.Crimson;
        private Brush tileLayBrush = Brushes.LawnGreen;
        private Brush tileSelectBrush = Brushes.Gold;
        private Brush squareBrush = Brushes.Green;
        private Brush squareLetterMul2Brush = Brushes.Cyan;
        private Brush squareLetterMul3Brush = Brushes.MediumBlue;
        private Brush squareWordMul2Brush = Brushes.Magenta;
        private Brush squareWordMul3Brush = Brushes.Red;

        private delegate void SetControlEnabilityDen(bool enabled);
        private delegate void UpdatePlayerListDel();
        private delegate void UpdateLogDel(string message);

        public GameForm()
        {
            InitializeComponent();
            isPlaying = false;
            SetEnabilityForConnectionControls(true);
            SetEnabilityForGameControls(false);

            clientProperties[Settings.nameKeyword] = Settings.clientURI;
            clientProperties[Settings.portKeyword] = 0;
            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            boardGraphics = boardPictureBox.CreateGraphics();
            handGraphics = handPictureBox.CreateGraphics();
            tileLetterStringFormat.Alignment = StringAlignment.Center;
            tileLetterStringFormat.LineAlignment = StringAlignment.Center;
            tileValueStringFormat.Alignment = StringAlignment.Far;
            tileValueStringFormat.LineAlignment = StringAlignment.Far;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(ipTextBox.Text, out serverIP))
            {
                ShowMessageBox(Settings.invalidIPMessage);
                return;
            }

            if (portTextBox.Text == Settings.emptyString)
            {
                serverPort = Settings.defaultServerPort;
            } 
            else if (!Int32.TryParse(portTextBox.Text, out serverPort))
            {
                ShowMessageBox(Settings.invalidPortMessage);
                return;
            }

            playerName = nameTextBox.Text;
            if (playerName.Length < Settings.nameLengthLowerBound || playerName.Length > Settings.nameLengthUpperBound)
            {
                ShowMessageBox(Settings.invalidNameMessage);
                return;
            }

            playerCode = random.Next();

            eventProxy = new EventProxy();
            eventProxy.communicationIdWasSet += new CommunicationIdWasSetEvent(SetCommunicationId);
            eventProxy.gameBegun += new GameBegunEvent(BeginGame);
            eventProxy.logWasUpdated += new LogWasUpdatedEvent(UpdateLog);
            eventProxy.moveWasMade += new MoveWasMadeEvent(UpdateAfterMove);

            serverURI = Settings.tcpProtocolString + serverIP.ToString() + Settings.ipPortUriSeparator + serverPort.ToString() + Settings.serverURITail;
            tcpChannel = new TcpChannel(clientProperties, clientProvider, serverProvider);
            ChannelServices.RegisterChannel(tcpChannel, false);
            RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IServer), serverURI));
            remoteServer = (IServer)Activator.GetObject(typeof(IServer), serverURI);
            remoteServer.AddPlayer(playerName, playerCode, eventProxy);

            Text = Settings.scrabbleClientString + Settings.connectedAndWaitingString;
            SetEnabilityForConnectionControls(false);
        }

        private void SetCommunicationId(int id)
        {
            playerId = id;
        }

        private void BeginGame()
        {
            isPlaying = true;
            Text = Settings.scrabbleClientString + Settings.columnChar + Settings.spaceChar + playerName;
            SetEnabilityForGameControls(true);
        }

        private void UpdateLog(string message)
        {
            if (logRichTextBox.InvokeRequired)
            {
                BeginInvoke(new UpdateLogDel(UpdateLog), new object[] { message });
                return;
            }
            else
            {
                logRichTextBox.AppendText(message + Settings.newLineString);
            }
        }

        private void UpdateAfterMove(ReadOnlyCollection<Player> players, Board board, bool gameIsTerminated)
        {
            this.players = players;
            this.board = board;
            this.gameIsTerminated = gameIsTerminated;
            currentMove = new Move(playerId, playerName, playerCode);
            handTileSelection = null;

            RelistPlayerList();
            RelistHand();

            RedrawHand();
            RedrawBoard();    

            if (!this.gameIsTerminated && IsPlayersTurn())
            {
                SetEnabilityForMoveButton(true);
            }
            else
            {
                SetEnabilityForMoveButton(false);
            }
        }

        private bool IsPlayersTurn()
        {
            return (players[0].Id == playerId);
        }

        private bool IsSquareTempEmpty(int xCoor, int yCoor)
        {
            foreach (TileMove tileMove in currentMove)
            {
                if (tileMove.XCoor == xCoor && tileMove.YCoor == yCoor && tileMove.Action == MoveAction.Lay)
                {
                    return false;
                }
            }
            return true;
        }
        
        private void RelistPlayerList()
        {
            if (playersRichTextBox.InvokeRequired)
            {
                BeginInvoke(new UpdatePlayerListDel(RelistPlayerList));
                return;
            }
            else
            {
                playersRichTextBox.Clear();
                foreach (Player player in players)
                {
                    playersRichTextBox.AppendText(player.Name + Settings.columnChar + Settings.spaceChar + player.Points + Settings.newLineString);
                }
            }
        }

        private void RelistHand() 
        {
            foreach (Player player in players)
            {
                if (player.Id == playerId)
                {
                    playerHand = player.Hand;
                    return;
                }
            }
        }

        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        private void SetEnabilityForConnectionControls(bool enabled)
        {
            if (connectionGroupBox.InvokeRequired)
            {
                BeginInvoke(new SetControlEnabilityDen(SetEnabilityForConnectionControls), new object[] { enabled });
                return;
            }
            else
            {
                connectionGroupBox.Enabled = enabled;
            }
            
        }

        private void SetEnabilityForGameControls(bool enabled)
        {
            if (boardGroupBox.InvokeRequired || handGroupBox.InvokeRequired || playersGroupBox.InvokeRequired || logGroupBox.InvokeRequired)
            {
                BeginInvoke(new SetControlEnabilityDen(SetEnabilityForGameControls), new object[] { enabled });
                return;
            }
            else
            {
                boardGroupBox.Enabled = enabled;
                handGroupBox.Enabled = enabled;
                playersGroupBox.Enabled = enabled;
                logGroupBox.Enabled = enabled;
            }
        }

        private void SetEnabilityForMoveButton(bool enabled)
        {
            if (moveButton.InvokeRequired)
            {
                BeginInvoke(new SetControlEnabilityDen(SetEnabilityForMoveButton), new object[] { enabled });
                return;
            }
            else
            {
                moveButton.Enabled = enabled;
            }
        }

        private void RedrawHand()
        {
            for (int coor = 0; coor < playerHand.Size; coor++)
            {
                RedrawHandSquare(coor);
            }
        }

        private void RedrawHandSquare(int coor)
        {
            if (!playerHand.HasTileAt(coor))
            {
                RedrawSquare(handGraphics, coor, 0, squareBrush);
            }
            else if (handTileSelection != null && handTileSelection == coor)
            {
                RedrawSquare(handGraphics, coor, 0, tileSelectBrush, playerHand[coor].Letter, playerHand[coor].Value);
            }
            else if (currentMove[coor].Action == MoveAction.Exchange)
            {
                RedrawSquare(handGraphics, coor, 0, tileExchangeBrush, playerHand[coor].Letter, playerHand[coor].Value);
            }
            else if (currentMove[coor].Action == MoveAction.Lay)
            {
                RedrawSquare(handGraphics, coor, 0, tileLayBrush, playerHand[coor].Letter, playerHand[coor].Value);
            }
            else if (currentMove[coor].Action == MoveAction.None)
            {
                RedrawSquare(handGraphics, coor, 0, tileBrush, playerHand[coor].Letter, playerHand[coor].Value);
            }
        }

        private void RedrawTempBoardTiles()
        {
            for (int tileIndex = 0; tileIndex < currentMove.Size; tileIndex++)
			{
			    if (currentMove[tileIndex].Action == MoveAction.Lay)
	            {
                    RedrawSquare(boardGraphics, currentMove[tileIndex].XCoor, currentMove[tileIndex].YCoor, tileLayBrush, playerHand[tileIndex].Letter, playerHand[tileIndex].Value);
	            }
			}
        }

        private void RedrawBoard()
        {
            for (int coorX = 0; coorX < board.Size; coorX++)
            {
                for (int coorY = 0; coorY < board.Size; coorY++)
                {
                    RedrawBoardSquare(coorX, coorY);
                }
            }

        }

        private void RedrawBoardSquare(int xCoor, int yCoor)
        {
            if (!board.IsSquareEmpty(xCoor, yCoor))
            {
                RedrawSquare(boardGraphics, xCoor, yCoor, tileBrush, board[xCoor, yCoor].Letter, board[xCoor, yCoor].Value);
            }
            else
            {
                RedrawBoardEmptySquare(xCoor, yCoor);
            }
        }

        private void RedrawBoardEmptySquare(int xCoor, int yCoor)
        {
            int letterMultiplier = board.GetLetterMultiplierAt(xCoor, yCoor);
            int wordMultiplier = board.GetWordMultiplierAt(xCoor, yCoor);
            Rectangle square = new Rectangle(xCoor * (Settings.squareGraphicSize + Settings.squareGapGraphicSize), yCoor * (Settings.squareGraphicSize + Settings.squareGapGraphicSize), Settings.squareGraphicSize, Settings.squareGraphicSize);
            
            if (letterMultiplier == 2)
            {
                boardGraphics.FillRectangle(squareLetterMul2Brush, square);
            }
            else if (letterMultiplier == 3)
            {
                boardGraphics.FillRectangle(squareLetterMul3Brush, square);
            }
            else if (wordMultiplier == 2)
            {
                boardGraphics.FillRectangle(squareWordMul2Brush, square);
            }
            else if (wordMultiplier == 3)
            {
                boardGraphics.FillRectangle(squareWordMul3Brush, square);
            }
            else
            {
                boardGraphics.FillRectangle(squareBrush, square);
            }
        }

        private void RedrawSquare(Graphics graphics, int xCoor, int yCoor, Brush brush)
        {
            Rectangle square = new Rectangle(xCoor * (Settings.squareGraphicSize + Settings.squareGapGraphicSize), yCoor * (Settings.squareGraphicSize + Settings.squareGapGraphicSize), Settings.squareGraphicSize, Settings.squareGraphicSize);
            graphics.FillRectangle(brush, square);
        }

        private void RedrawSquare(Graphics graphics, int xCoor, int yCoor, Brush brush, char letter, int value)
        {
            Rectangle square = new Rectangle(xCoor * (Settings.squareGraphicSize + Settings.squareGapGraphicSize), yCoor * (Settings.squareGraphicSize + Settings.squareGapGraphicSize), Settings.squareGraphicSize, Settings.squareGraphicSize);
            graphics.FillRectangle(brush, square);
            graphics.DrawString(letter.ToString(), tileLetterFont, Brushes.Black, square, tileLetterStringFormat);
            graphics.DrawString(value.ToString(), tileValueFont, Brushes.Black, square, tileValueStringFormat);
        }

        private void handPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPlayersTurn())
            {
                return;
            }

            int handTileCoor = e.Location.X / (Settings.squareGraphicSize + Settings.squareGapGraphicSize);
            if (!playerHand.HasTileAt(handTileCoor))
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (handTileCoor == handTileSelection)
                {
                    handTileSelection = null;
                }
                else if (currentMove[handTileCoor].Action == MoveAction.Lay)
                {
                    RedrawBoardSquare(currentMove[handTileCoor].XCoor, currentMove[handTileCoor].YCoor);
                    currentMove[handTileCoor].Action = MoveAction.None;
                }
                else
                {
                    handTileSelection = handTileCoor;
                    currentMove[handTileCoor].Action = MoveAction.None;
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                if (handTileCoor == handTileSelection)
                {
                    handTileSelection = null;
                }

                if (currentMove[handTileCoor].Action == MoveAction.Exchange)
                {
                    currentMove[handTileCoor].Action = MoveAction.None;
                }
                else if (currentMove[handTileCoor].Action == MoveAction.None)
                {
                    currentMove[handTileCoor].Action = MoveAction.Exchange;
                }
                else if (currentMove[handTileCoor].Action == MoveAction.Lay)
                {
                    RedrawBoardSquare(currentMove[handTileCoor].XCoor, currentMove[handTileCoor].YCoor);
                    currentMove[handTileCoor].Action = MoveAction.None;
                }
            }

            RedrawHand();
        }

        private void boardPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPlayersTurn())
            {
                return;
            }

            int boardSquareXCoor = e.Location.X / (Settings.squareGraphicSize + Settings.squareGapGraphicSize);
            int boardSquareYCoor = e.Location.Y / (Settings.squareGraphicSize + Settings.squareGapGraphicSize);

            if (e.Button == MouseButtons.Left && handTileSelection != null && board.IsSquareEmpty(boardSquareXCoor, boardSquareYCoor) && IsSquareTempEmpty(boardSquareXCoor, boardSquareYCoor))
            {
                currentMove[(int)handTileSelection].Action = MoveAction.Lay;
                currentMove[(int)handTileSelection].XCoor = boardSquareXCoor;
                currentMove[(int)handTileSelection].YCoor = boardSquareYCoor;
                RedrawSquare(boardGraphics, boardSquareXCoor, boardSquareYCoor, tileLayBrush, playerHand[(int)handTileSelection].Letter, playerHand[(int)handTileSelection].Value);
                handTileSelection = null;
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (TileMove tileMove in currentMove)
                {
                    if (tileMove.XCoor == boardSquareXCoor && tileMove.YCoor == boardSquareYCoor)
                    {
                        tileMove.Action = MoveAction.None;
                        RedrawBoardSquare(boardSquareXCoor, boardSquareYCoor);
                        break;
                    }
                }
            }

            RedrawHand();
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            remoteServer.PerformMove(currentMove);
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            if (!isPlaying)
            {
                return;
            }

            boardPictureBox.Refresh();
            RedrawBoard();
            RedrawTempBoardTiles();
            handPictureBox.Refresh();
            RedrawHand();
        }
    }
}
