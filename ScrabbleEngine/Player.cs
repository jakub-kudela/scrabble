using System;

namespace ScrabbleEngine
{
    public sealed class Player : MarshalByRefObject
    {
        private readonly int id;
        private readonly string name;
        private readonly int code;
        private int points;
        private Hand hand;

        public Player(int id, string name, int code)
        {
            this.id = id;
            this.name = name;
            this.code = code;
            points = 0;
            hand = new Hand();
        }
        
        public int Id 
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int Points
        {
            get
            {
                return points;
            }
        }

        public int HandTileCount
        {
            get
            {
                return Hand.TileCount;
            }
        }

        public Hand Hand
        {
            get
            {
                return hand;
            }
        }

        internal int Code
        {
            get
            {
                return code;
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        internal void AddPoints(int points)
        {
            this.points += points;
        }

        internal void DeductPoints(int points)
        {
            if (this.points > points)
            {
                this.points -= points;
            }
            else
            {
                this.points = 0;
            }
        }
    }
}
