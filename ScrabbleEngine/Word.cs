using System;

namespace ScrabbleEngine
{
    public class Word : IEquatable<Word>
    {
        private const string toStringStringFormat = "{0}[{1},{2},{3}]";
        private readonly string value;
        private readonly int xCoor;
        private readonly int yCoor;
        private WordOrientation orientation;
        private int points;

        public Word(string value, int xCoor, int yCoor, WordOrientation orientation, int points)
        {
            this.value = value;
            this.xCoor = xCoor;
            this.yCoor = yCoor;
            this.orientation = orientation;
            this.points = points;
        }

        public string Value
        {
            get
            {
                return value;
            }
        }

        public int XCoor
        {
            get
            {
                return xCoor;
            }
        }

        public int YCoor
        {
            get
            {
                return yCoor;
            }
        }

        public WordOrientation Orientation
        {
            get
            {
                return orientation;
            }
        }

        public int Points 
        {
            get
            {
                return points;
            }
        }

        public bool IsValid()
        {
            return WordValidator.IsValid(value);
        }

        public bool Equals(Word other)
        {
            if (other == null)
            {
                return false;
            }

            return (value == other.value && xCoor == other.xCoor && yCoor == other.yCoor && orientation == other.orientation);
        }

        public override string ToString()
        {
            return String.Format(toStringStringFormat, value, xCoor, yCoor, orientation.ToString());
        }
    }
}
