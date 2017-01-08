using System;
using System.Collections.Generic;

namespace ScrabbleEngine
{
    internal sealed class TileBag
    {
        private static readonly Random random = new Random();
        private List<char> letters;

        internal TileBag()
        {
            letters = new List<char>();

            foreach (LetterInfo letter in Settings.letterInfos)
            {
                for (int countIndex = 0; countIndex < letter.Distribution; countIndex++)
                {
                    letters.Add(letter.Character);
                }
            }
        }

        internal List<Tile> PullOutTiles(int tileCount)
        {
            List<Tile> newTiles = new List<Tile>();

            while (IsNonEmpty() && newTiles.Count < tileCount)
            {
                newTiles.Add(PullOutTile());
            }

            return newTiles;
        }

        internal List<Tile> ExchangeTiles(List<Tile> tilesToChange)
        {
            List<Tile> newTiles = PullOutTiles(tilesToChange.Count);
            foreach (Tile oldTile in tilesToChange)
            {
                letters.Add(oldTile.Letter);
            }
            return newTiles;
        }

        internal bool IsNonEmpty()
        {
            if (letters.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool CanChangeTiles()
        {
            if (letters.Count >= Settings.handSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Tile PullOutTile()
        {
            int letterIndex = TileBag.random.Next(letters.Count);
            char pulledOutLetter = letters[letterIndex];
            letters.RemoveAt(letterIndex);
            return new Tile(pulledOutLetter);
        }
    }
}
