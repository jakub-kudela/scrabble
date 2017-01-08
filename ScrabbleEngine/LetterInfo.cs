namespace ScrabbleEngine
{
    internal struct LetterInfo
    {
        private readonly char character;
        private readonly int distribution;
        private readonly int value;

        public LetterInfo(char character, int distribution, int value)
        {
            this.character = character;
            this.distribution = distribution;
            this.value = value;
        }

        internal char Character
        {
            get
            {
                return character;
            }
        }

        internal int Distribution
        {
            get
            {
                return distribution;
            }
        }

        internal int Value
        {
            get
            {
                return value;
            }
        }
    }
}
