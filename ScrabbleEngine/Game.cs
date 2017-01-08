using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ScrabbleEngine
{
    public sealed class Game
    {
        private Board board;
        private TileBag tileBag;
        private Log log;
        private Queue<Player> players;
        private bool isTerminated;
        private bool firstWordLaid;

        public Game(List<Player> players)
        {
            board = new Board();
            tileBag = new TileBag();
            log = new Log();
            this.players = new Queue<Player>();
            isTerminated = false;
            firstWordLaid = false;

            foreach (Player player in players)
            {
                this.players.Enqueue(player);
            }
            InicializeHands();
        }

        public Board Board
        {
            get
            {
                return board;
            }
        }

        public Log Log
        {
            get
            {
                return log;
            }
        }

        public ReadOnlyCollection<Player> Players
        {
            get
            {
                return new ReadOnlyCollection<Player>(players.ToArray());
            }
        }

        public bool IsTerminated
        {
            get
            {
                return isTerminated;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return players.Peek();
            }
        }

        public bool PerformMove(Move move)
        {
            if (!IsMoveNameAlright(move))
            {
                log.WriteMoveFailure(move.PlayerName, Settings.logMoveFailureNameMessageFormat);
                return false;
            }

            if (!IsMoveIdAlright(move))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureNameMessageFormat);
                return false;
            }

            if (!IsMoveCodeAlright(move))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureCodeMessageFormat);
                return false;
            }

            if (!IsMoveSizeAlright(move))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureSizeMessageFormat);
                return false;
            }

            MoveAction action = MoveAction.None;
            if (!IsMoveActionAlright(move, out action))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureActionMessageFormat);
                return false;
            }

            if (action == MoveAction.None)
            {
                log.WriteNoneActionMove(CurrentPlayer.Name);
                PassTurnToNextPlayer();
                return true;
            }
            
            if (action == MoveAction.Exchange)
            {
                if (tileBag.CanChangeTiles())
                {
                    ExchangePlayerTiles(move);
                    log.WriteExchangeActionMove(CurrentPlayer.Name, move.ExchangeTilesCount());
                    PassTurnToNextPlayer();
                    return true;
                }
                else
                {
                    log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureNoChangingMessageFormat);
                    return false;
                }
            }

            if (!IsMoveLayoutAlright(move))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureLayoutMessageFormat);
                return false;
            }

            if (!IsMoveOrientationAlright(move))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureOrientationMessageFormat);
                return false;
            }

            List<Word> words = GetWords(move);
            if (!IsMainWordContinuityAlright(words))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureMainWordIncontinuousMessageFormat);
                return false;
            }
            if (!AreWordsValid(words))
            {
                log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureInvalidWordMessageFormat);
                return false;
            }

            if (!firstWordLaid)
            {
                if (!IsValidFirstWordMove(move))
                {
                    log.WriteMoveFailure(CurrentPlayer.Name, Settings.logMoveFailureFirstMovePositionMessageFormat);
                    return false;
                }
                else
                {
                    firstWordLaid = true;
                }
            }

            LayDownMoveOnBoard(move);
            UpdateBoardMultipliersAfterMove(move);
            int points = PointsCount(words);
            CurrentPlayer.AddPoints(points);

            if (CurrentPlayer.Points < Settings.winningPoints)
            {
                log.WriteLayActionMove(CurrentPlayer.Name, words, points);
                CurrentPlayer.Hand.AddTiles(tileBag.PullOutTiles(CurrentPlayer.Hand.Size - CurrentPlayer.Hand.TileCount));
                PassTurnToNextPlayer();
            }
            else
            {
                log.WriteGameOverWithWinner(CurrentPlayer.Name);
                isTerminated = true;
            }
            return true;
        }

        public void PassTurnToNextPlayer()
        {
            Player playerEndingTurn = players.Dequeue();
            players.Enqueue(playerEndingTurn);
        }

        private bool IsValidFirstWordMove(Move move)
        {
            foreach (TileMove tileMove in move)
            {
                if (tileMove.XCoor == Settings.boardCenterCoor && tileMove.YCoor == Settings.boardCenterCoor)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsMoveNameAlright(Move move)
        {
            return CurrentPlayer.Name == move.PlayerName;
        }

        private bool IsMoveIdAlright(Move move)
        {
            return CurrentPlayer.Id == move.PlayerId; 
        }

        private bool IsMoveCodeAlright(Move move)
        {
            return CurrentPlayer.Code == move.PlayerCode;
        }

        private bool IsMoveSizeAlright(Move move)
        {
            return (move.Size == CurrentPlayer.Hand.Size);
        }

        private bool IsMoveActionAlright(Move move, out MoveAction moveAction)
        {
            moveAction = MoveAction.None;
            foreach (TileMove tileMove in move)
            {
                if (moveAction == MoveAction.None)
                {
                    moveAction = tileMove.Action;
                }
                else if (tileMove.Action != MoveAction.None && tileMove.Action != moveAction)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsMoveLayoutAlright(Move move)
        {
            foreach (TileMove tileMove in move)
            {
                if (!board.IsSquareEmpty(tileMove.XCoor, tileMove.YCoor))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsMoveOrientationAlright(Move move)
        {
            bool isHorizontal = true;
            bool isVertical = true;
            int firstTileMoveXCoor = 0;
            int firstTileMoveYCoor = 0;

            foreach (TileMove tileMove in move)
            {
                if (tileMove.Action == MoveAction.Lay)
                {
                    firstTileMoveXCoor = tileMove.XCoor;
                    firstTileMoveYCoor = tileMove.YCoor;
                    break;
                }
            }

            foreach (TileMove tileMove in move)
            {
                if (tileMove.Action == MoveAction.Lay)
                {
                    if (tileMove.YCoor != firstTileMoveYCoor)
                    {
                        isHorizontal = false;    
                    }
                    if (tileMove.XCoor != firstTileMoveXCoor)
                    {
                        isVertical = false;    
                    }
                }
            }

            return (isHorizontal || isVertical);
        }

        private bool IsMainWordContinuityAlright(List<Word> newWords)
        {
            int horizontalWordsCount = 0;
            int verticalWordsCount = 0;

            foreach (Word word in newWords)
            {
                if (word.Orientation == WordOrientation.Horizontal)
                {
                    horizontalWordsCount++;
                }
                else
                {
                    verticalWordsCount++;
                }
            }

            return (horizontalWordsCount == 1 || verticalWordsCount == 1);
        }

        private List<Word> GetWords(Move move)
        {
            List<Word> words = new List<Word>();
            LayDownMoveOnBoard(move);

            foreach (TileMove tileMove in move)
            {
                if (tileMove.Action == MoveAction.Lay)
                {
                    Word horizontalWord = GetHorizontalWord(tileMove.XCoor, tileMove.YCoor);
                    if (!words.Contains(horizontalWord) && horizontalWord.Value.Length >= Settings.minimalWordLength)
                    {
                        words.Add(horizontalWord);
                    }
                    Word verticalWord = GetVerticalWord(tileMove.XCoor, tileMove.YCoor);
                    if (!words.Contains(verticalWord) && verticalWord.Value.Length >= 2)
                    {
                        words.Add(verticalWord);
                    }
                }
            }

            TakeBackMoveOffBoard(move);
            return words;
        }

        private Word GetHorizontalWord(int xCoor, int yCoor)
        {
            int letterValues = 0;
            int wordMultiplier = 1;
            int coorIndex = xCoor;
            while (coorIndex > 0 && !Board.IsSquareEmpty(coorIndex - 1, yCoor))
            {
                coorIndex--;
            }

            int beginXCoor = coorIndex;
            StringBuilder word = new StringBuilder();
            while (coorIndex <= Settings.boardSize && !Board.IsSquareEmpty(coorIndex, yCoor))
            {
                letterValues += board[coorIndex, yCoor].Value * Board.GetLetterMultiplierAt(coorIndex, yCoor);
                wordMultiplier *= board.GetWordMultiplierAt(coorIndex, yCoor);
                word.Append(Board[coorIndex, yCoor].Letter);
                coorIndex++;
            }
            int wordValue = letterValues * wordMultiplier;
            return new Word(word.ToString(), beginXCoor, yCoor, WordOrientation.Horizontal, wordValue);
        }

        private Word GetVerticalWord(int xCoor, int yCoor)
        {
            int letterValues = 0;
            int wordMultiplier = 1;
            int coorIndex = yCoor;
            while (coorIndex > 0 && !Board.IsSquareEmpty(xCoor, coorIndex - 1))
            {
                coorIndex--;
            }

            int beginYCoor = coorIndex;
            StringBuilder word = new StringBuilder();
            while (coorIndex <= Settings.boardSize && !Board.IsSquareEmpty(xCoor, coorIndex))
            {
                letterValues += board[xCoor, coorIndex].Value * Board.GetLetterMultiplierAt(xCoor, coorIndex);
                wordMultiplier *= board.GetWordMultiplierAt(xCoor, coorIndex);
                word.Append(Board[xCoor, coorIndex].Letter);
                coorIndex++;
            }
            int wordValue = letterValues * wordMultiplier;
            return new Word(word.ToString(), xCoor, beginYCoor, WordOrientation.Vertical, wordValue);
        }

        private bool AreWordsValid(List<Word> words)
        {
            foreach (Word word in words)
            {
                if (!word.IsValid())
                {
                    return false;
                }
            }

            return true;
        }

        private int PointsCount(List<Word> Words)
        {
            int points = 0;
            foreach (Word word in Words)
            {
                points += word.Points;
            }
            return points;
        }

        private void InicializeHands()
        {
            foreach (Player player in players)
            {
                player.Hand.AddTiles(tileBag.PullOutTiles(Settings.handSize));
            }
        }

        private void ExchangePlayerTiles(Move move)
        {
            List<Tile> oldTiles = new List<Tile>();
            for (int tileIndex = 0; tileIndex < move.Size; tileIndex++)
            {
                if (move[tileIndex].Action == MoveAction.Exchange)
                {
                    oldTiles.Add(CurrentPlayer.Hand.RemoveTile(tileIndex));
                }
            }

            List<Tile> newTiles = tileBag.ExchangeTiles(oldTiles);
            CurrentPlayer.Hand.AddTiles(newTiles);
        }

        private void LayDownMoveOnBoard(Move move)
        {
            for (int tileIndex = 0; tileIndex < move.Size; tileIndex++)
            {
                if (move[tileIndex].Action == MoveAction.Lay)
                {
                    Tile tileToLayDown = CurrentPlayer.Hand.RemoveTile(tileIndex);
                    board.LayTileOnSquare(tileToLayDown, move[tileIndex].XCoor, move[tileIndex].YCoor);
                }
            }
        }

        private void TakeBackMoveOffBoard(Move move)
        {
            List<Tile> takenBackTiles = new List<Tile>();

            foreach (TileMove tileMove in move)
            {
                if (tileMove.Action == MoveAction.Lay)
                {
                    Tile takenBackTile = board.TakeTileOffSquare(tileMove.XCoor, tileMove.YCoor);
                    takenBackTiles.Add(takenBackTile);
                }
            }

            CurrentPlayer.Hand.AddTiles(takenBackTiles);
        }

        private void UpdateBoardMultipliersAfterMove(Move move)
        {
            foreach (TileMove tileMove in move)
            {
                board.DeleteLetterMultiplierAt(tileMove.XCoor, tileMove.YCoor);
                board.DeleteWordMultiplierAt(tileMove.XCoor, tileMove.YCoor);
            }
        }
    }
}
