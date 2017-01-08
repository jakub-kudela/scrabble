using System;

namespace ScrabbleEngine
{
    public sealed class Board : MarshalByRefObject 
    {
        private Tile[,] tiles;
        private int[,] letterMultipliers;
        private int[,] wordMultipliers;

        internal Board()
        {
            tiles = new Tile[Settings.boardSize, Settings.boardSize];
            letterMultipliers = new int[Settings.boardSize, Settings.boardSize];
            wordMultipliers = new int[Settings.boardSize, Settings.boardSize];

            InicializeMultipliers();
        }

        public int Size
        {
            get
            {
                return Settings.boardSize;
            }
        }

        public Tile this[int coorX, int coorY]
        {
            get
            {
                return tiles[coorX, coorY];
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool IsSquareEmpty(int xCoor, int yCoor)
        {
            return (tiles[xCoor, yCoor] == null);
        }

        public int GetLetterMultiplierAt(int coorX, int coorY)
        {
            return letterMultipliers[coorX, coorY];
        }

        public int GetWordMultiplierAt(int coorX, int coorY)
        {
            return wordMultipliers[coorX, coorY];
        }  

        internal void LayTileOnSquare(Tile tile, int xCoor, int yCoor)
        {
            tiles[xCoor, yCoor] = tile;
        }

        internal Tile TakeTileOffSquare(int xCoor, int yCoor)
        {
            Tile tileTakenOff = tiles[xCoor, yCoor];
            tiles[xCoor, yCoor] = null;
            return tileTakenOff;
        }

        internal void DeleteLetterMultiplierAt(int coorX, int coorY)
        {
            letterMultipliers[coorX, coorY] = 1;
        }

        internal void DeleteWordMultiplierAt(int coorX, int coorY)
        {
            wordMultipliers[coorX, coorY] = 1;
        }

        private void InicializeMultipliers()
        {
            for (int xCoor = 0; xCoor < Settings.boardSize; xCoor++)
            {
                for (int yCoor = 0; yCoor < Settings.boardSize; yCoor++)
                {
                    letterMultipliers[xCoor, yCoor] = 1;
                    wordMultipliers[xCoor, yCoor] = 1;
                }
            }
            foreach (MultiplierInfo letterMultiplier in Settings.letterMultiplierInfos)
            {
                letterMultipliers[letterMultiplier.XCoor, letterMultiplier.YCoor] = letterMultiplier.Value;
            }
            foreach (MultiplierInfo wordMultiplier in Settings.wordMultiplierInfos)
            {
                wordMultipliers[wordMultiplier.XCoor, wordMultiplier.YCoor] = wordMultiplier.Value;
            }
        }
    }
}
