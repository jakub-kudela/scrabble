using System;

namespace ScrabbleEngine
{
    public sealed class TileMove : MarshalByRefObject
    {
        private MoveAction action;
        private int xCoor;
        private int yCoor;

        public TileMove()
        {
            action = MoveAction.None;
            xCoor = 0;
            yCoor = 0;
        }

        public MoveAction Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
            }
        }

        public int XCoor
        {
            get
            {
                return xCoor;
            }
            set
            {
                xCoor = value;
            }
        }

        public int YCoor
        {
            get
            {
                return yCoor;
            }
            set
            {
                yCoor = value;
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
