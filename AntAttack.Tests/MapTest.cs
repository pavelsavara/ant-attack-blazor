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
    }
}
