using System;
using System.Collections;

namespace ScrabbleEngine
{
    public sealed class Move : MarshalByRefObject, IEnumerable
    {
        private readonly int playerId;
        private readonly string playerName;
        private readonly int playerCode;
        private readonly TileMove[] tileMoves;

        public Move(int playerId, string playerName, int playerCode)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.playerCode = playerCode;
            tileMoves = new TileMove[Settings.handSize];
            for (int tileMoveIndex = 0; tileMoveIndex < Settings.handSize; tileMoveIndex++)
            {
                tileMoves[tileMoveIndex] = new TileMove();
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public int PlayerId
        {
            get
            {
                return playerId;
            }
        }

        public string PlayerName
        {
            get
            {
                return playerName;
            }
        }

        public int PlayerCode
        {
            get
            {
                return playerCode;
            }
        }

        public TileMove this[int tileMoveIndex]
        {
            get
            {
                return tileMoves[tileMoveIndex];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return (tileMoves as IEnumerable).GetEnumerator();
        }
        
        public int Size
        {
            get
            {
                return tileMoves.Length;
            }
        }

        public int ExchangeTilesCount()
        {
            int exchangeTilesCount = 0;
            foreach (TileMove tileMove in tileMoves)
            {
                if (tileMove.Action == MoveAction.Exchange)
                {
                    exchangeTilesCount++;
                }
            }
            return exchangeTilesCount;
        }

        public int LayTilesCount()
        {
            int exchangeTilesCount = 0;
            foreach (TileMove tileMove in tileMoves)
            {
                if (tileMove.Action == MoveAction.Lay)
                {
                    exchangeTilesCount++;
                }
            }
            return exchangeTilesCount;
        }
    }
}
