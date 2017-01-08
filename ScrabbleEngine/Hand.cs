using System;
using System.Collections.Generic;

namespace ScrabbleEngine
{
    public sealed class Hand :  MarshalByRefObject
    {
        private int tileCount;
        private Tile[] tiles;

        internal Hand()
        {
            tileCount = 0;
            tiles = new Tile[Settings.handSize];
        }
  
        public int Size
        {
            get
            {
                return tiles.Length;
            }
        }

        public int TileCount
        {
            get
            {
                return tileCount;
            }
        }

        public Tile this[int tileIndex]
        {
            get
            {
                return tiles[tileIndex];
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool HasTileAt(int coor)
        {
            return this[coor] != null;
        }

        internal void AddTiles(List<Tile> tiles)
        {
            int addingTilesIndex = 0;
            int handTilesindex = 0;

            while (addingTilesIndex < tiles.Count && handTilesindex < Size)
            {
                if (this.tiles[handTilesindex] == null)
                {
                    this.tiles[handTilesindex] = tiles[addingTilesIndex];
                    addingTilesIndex++;
                    tileCount++;
                }
                handTilesindex++;
            }
        }

        internal Tile RemoveTile(int tileIndex)
        {
            Tile removedTile = tiles[tileIndex];
            tiles[tileIndex] = null;
            tileCount--;
            return removedTile;
        }

        internal void SwapTiles(int tileIndex1, int tileIndex2)
        {
            Tile tempTile = tiles[tileIndex1];
            tiles[tileIndex1] = tiles[tileIndex2];
            tiles[tileIndex2] = tempTile;
        }
    }
}
