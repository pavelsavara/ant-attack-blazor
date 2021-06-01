using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace Ant
{
    public class View
    {
        static View()
        {
            using (var stream = typeof(Map).Assembly.GetManifestResourceStream("Ant.cube.png"))
            {
                cube = Image.Load(stream);
            }
        }
        private static Image cube;
        private readonly int widht;
        private readonly int height;
        private readonly Map map;

        public View(int widht, int height, Map map)
        {
            this.widht = widht;
            this.height = height;
            this.map = map;
        }


        public void Save(string prefix, int xShift, int yShift)
        {
            using (var image = Render(xShift, yShift))
            {
                image.Save($"{prefix}.{xShift}.{yShift}.png");
            }
        }

        public string DataUrl(int xShift, int yShift)
        {
            using (var image = Render(xShift, yShift))
            {
                return "data:image/png;base64, " + image.ToBase64String(PngFormat.Instance);
            }
        }

        private Image Render(int xShift, int yShift)
        {
            var image = new Image<Rgb24>(widht, height);

            image.Mutate(x => x.Fill(Color.White));
            for (int z = 0; z < map.MaxZ; z++)
            {
                for (int y = 0; y < map.MaxY; y++)
                {
                    for (int x = 0; x < map.MaxX; x++)
                    {

                        int sx = x + xShift;
                        int sy = y + yShift;
                        if (sx < 0 || sx >= map.MaxX || sy < 0 || sy >= map.MaxY)
                        {
                            continue;
                        }
                        var cell = map[sx, sy, z];
                        if (cell != FieldType.Cube)
                        {
                            continue;
                        }
                        DrawCube(image, z, y, x);
                    }
                }
            }


            return image;
        }

        /*private const int gridA = 16; //18/16/14
        private const int gridB = 14;
        private const int gridC = 8;
        private const int gridD = 7;
        private const int gridE = 16;

        x = (y * gridE) + ((x) * gridB);
        y=  (y * gridC) - (x * gridD) - (z * gridA);

        */

        private const int x2x = 14;
        private const int x2y = 7;

        private const int y2x = 16;
        private const int y2y = 8;

        private const int z2x = 0;
        private const int z2y = 16;


        private void DrawCube(Image image, int z, int y, int x)
        {
            var x2d = (widht / 2) + (x * x2x) - (y * y2x);
            var y2d = (height / 2) + (x * x2y) + (y * y2y) - (z * z2y);

            if (x2d < 0 || x2d + cube.Width >= widht || y2d < 0 || y2d + cube.Height >= height)
            {
                return;
            }

            DrawCube(image, x2d, y2d);
        }

        private static void DrawCube(Image image, int x2d, int y2d)
        {
            try
            {
                image.Mutate(x => x.DrawImage(cube, new Point(x2d, y2d), 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{x2d}, {y2d}");
            }
        }
    }
}
