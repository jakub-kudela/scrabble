namespace ScrabbleEngine
{
    internal struct MultiplierInfo
    {
        private readonly int xCoor;
        private readonly int yCoor;
        private readonly int value;

        internal MultiplierInfo(int xCoor, int yCoor, int value)
        {
            this.xCoor = xCoor;
            this.yCoor = yCoor;
            this.value = value;
        }

        internal int XCoor
        {
            get
            {
                return xCoor;
            }
        }

        internal int YCoor
        {
            get
            {
                return yCoor;
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
