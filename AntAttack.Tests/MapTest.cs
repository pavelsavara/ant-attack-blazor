// Pavel Savara 2021
// http://zamboch.blogspot.com/
// Licensed under MIT

using Xunit;

namespace Ant
{
    public class MapTest
    {
        [Fact]
        public void LoadMap()
        {
            Map m = new Map();
            m.LoadMap();

            FieldType fieldType = m[m.Man.X, m.Man.Y, m.Man.Z];
            Assert.Equal(FieldType.Man, fieldType);
        }

        [Theory]
        [InlineData(-100, 100)]
        [InlineData(-100, 0)]
        [InlineData(-100, -100)]
        [InlineData(0, 100)]
        [InlineData(0, 0)]
        [InlineData(0, -100)]
        [InlineData(100, 100)]
        [InlineData(100, 0)]
        [InlineData(100, -100)]
        public void Render(int xShift, int yShift)
        {
            Map map = new Map();
            var view = new View(800, 600, map);
            view.Save("map", xShift, yShift);
        }

        [Fact]
        public void RenderEasy()
        {
            Map map = new Map(10, 10, 10);
            map[5, 5, 5] = FieldType.Cube;

            map[6, 5, 5] = FieldType.Cube;
            map[7, 5, 5] = FieldType.Cube;
            map[9, 5, 5] = FieldType.Cube;

            map[5, 6, 5] = FieldType.Cube;
            map[5, 7, 5] = FieldType.Cube;
            map[5, 9, 5] = FieldType.Cube;

            map[5, 5, 6] = FieldType.Cube;
            map[5, 5, 7] = FieldType.Cube;
            map[5, 5, 9] = FieldType.Cube;

            var view = new View(800, 600, map);
            view.Save("easy", 0, 0);
        }
    }
}
