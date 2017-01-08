using System;

namespace ScrabbleEngine
{
    public sealed class Tile : MarshalByRefObject
    {
        private readonly char letter;

        public Tile(char letter)
        {
            this.letter = letter;
        }
       
        public char Letter
        {
            get
            {
                return letter;
            }
        }

        public int Value
        {
            get
            {
                return Settings.ValueOfLetter(letter);
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
