// Pavel Savara 2007
// http://zamboch.blogspot.com/
// http://pavel.savara.googlepages.com/AntAttack.html
// Licensed under LGPL

namespace Ant
{
    public struct Position
    {
        #region Construction

        public Position(int x, int y, int z, Direction dir)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            direction = dir;
        }

        public Position(Position src)
        {
            x = src.X;
            y = src.Y;
            z = src.Z;
            direction = src.Direction;
        }

        public Position(Position src, Direction dir)
        {
            x = src.X;
            y = src.Y;
            z = src.Z;
            direction = dir;
        }

        private Direction direction;
        private int x;
        private int y;
        private int z;

        #endregion

        #region Properties

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Z
        {
            get { return z; }
            set { z=value; }
        }

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        #endregion

        #region Methods

        public void TurnLeft()
        {
            direction--;
            NormalizeDirection();
        }

        public void TurnRight()
        {
            direction++;
            NormalizeDirection();
        }

        public bool GoDirection(Direction dir)
        {
            bool res = (dir != direction);
            direction = dir;
            MoveDirection(dir);
            return res;
        }

        public void GoForward()
        {
            MoveDirection(direction);
        }

        public void GoBack()
        {
            MoveDirection(NormalizeDirection(direction + 2));
        }

        private void MoveDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.East:
                    x++;
                    break;
                case Direction.West:
                    x--;
                    break;
            }
        }

        public void NormalizeDirection()
        {
            direction = NormalizeDirection(direction);
        }

        static public Direction NormalizeDirection(Direction direction)
        {
            return (Direction)(((int)direction + 256) % 4);
        }

        static public Direction NormalizeDirection(Direction direction1, Direction direction2)
        {
            return (Direction)(((int)direction1 + (int)direction2 + 256) % 4);
        }

        static public Direction NormalizeDirection2(Direction direction1, Direction direction2)
        {
            return (Direction)(((int)direction1 - (int)direction2 + 256 + 2 ) % 4);
        }

        #endregion
    }
}
