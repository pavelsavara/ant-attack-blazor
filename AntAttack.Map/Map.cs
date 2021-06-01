// Pavel Savara 2021
// http://zamboch.blogspot.com/
// Licensed under MIT

using System.IO;

namespace Ant
{
    public class Map
    {
        #region Construction


        public Map() : this(128, 128, 8)
        {
            LoadMap();
        }

        public Map(int maxx, int maxy, int maxz)
        {
            this.maxx = maxx;
            this.maxy = maxy;
            this.maxz = maxz;
            fields = new FieldType[maxz, maxx, maxy];
        }

        private readonly FieldType[,,] fields;
        private readonly int maxx;
        private readonly int maxy;
        private readonly int maxz;
        private int cubes;
        private Position man = new Position(53, 121, 0, Direction.North); //125
        //private Position man = new Position(3, 3, 0, Direction.North); //125

        #endregion

        #region Fields

        public FieldType this[int x, int y, int z]
        {
            get { return fields[z, x, y]; }
            set { fields[z, x, y] = value; }
        }

        public FieldType this[Position pos]
        {
            get { return fields[pos.Z, pos.X, pos.Y]; }
            set { fields[pos.Z, pos.X, pos.Y] = value; }
        }

        public int CubesCount
        {
            get { return cubes; }
        }

        public int MaxZ
        {
            get { return maxz; }
        }

        public int MaxX
        {
            get { return maxx; }
        }

        public int MaxY
        {
            get { return maxy; }
        }

        public Position Man
        {
            get { return man; }
            set { man = value; }
        }

        public bool IsValid(Position pos)
        {
            return
                !((pos.X < 0 || pos.X >= MaxX || pos.Y < 0 || pos.Y >= MaxY || pos.Z < 0 || pos.Z >= MaxZ) ||
                  ((this[pos]) != FieldType.Empty));
        }

        #endregion

        #region Loading

        public void LoadMap()
        {
            using (var stream = typeof(Map).Assembly.GetManifestResourceStream("Ant.antescher.pos"))
            {
                LoadMap(stream);
            }
        }

        public void LoadMap(Stream mapStream)
        {
            BinaryReader br = new BinaryReader(mapStream);
            LoadMap(br);
            br.Close();
        }
        private void LoadMap(BinaryReader br)
        {
            int cnt = br.ReadInt32();
            cubes = cnt;

            while (cnt > 0)
            {
                int x = br.ReadByte();
                int y = br.ReadByte();
                int z = br.ReadByte();
                this[x, y, z] = FieldType.Cube;
                cnt--;
            }
            this[man] = FieldType.Man;
        }

        #endregion
    }
}