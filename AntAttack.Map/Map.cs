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

        #endregion

        #region Fields

        public FieldType this[int x, int y, int z]
        {
            get { return fields[z, x, y]; }
            set { fields[z, x, y] = value; }
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
            this[53, 121, 0] = FieldType.Man;
        }

        #endregion
    }
}