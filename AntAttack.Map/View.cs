using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Threading.Tasks;

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
                return image.ToBase64String(PngFormat.Instance);
            }
        }

        private Image Render(int xShift, int yShift)
        {
            var image = new Image<Rgb24>(widht + (border * 2), height + (border * 2));

            image.Mutate(x => x.Fill(Color.White));
            image.Mutate(async ctx =>
            {
                for (int z = 0; z < map.MaxZ; z++)
                {
                    for (int y = 0; y < map.MaxY; y++)
                    {
                        for (int x = 0; x < map.MaxX; x++)
                        {

                            var cell = map[x, y, z];
                            if (cell != FieldType.Cube)
                            {
                                continue;
                            }
                            DrawCube(ctx, z, y, x, xShift, yShift);
                        }
                    }
                }
            });
            image.Mutate(i => i.Crop(new Rectangle(border, 32, widht, height)));

            return image;
        }

        private const int border = 32;

        private const int x2x = 14;
        private const int x2y = 7;

        private const int y2x = 16;
        private const int y2y = 8;

        private const int z2x = 0;
        private const int z2y = 16;


        private void DrawCube(IImageProcessingContext ctx, int z, int y, int x, int xShift, int yShift)
        {
            var x2d = (32 * xShift) + (widht / 2) + (x * x2x) - (y * y2x) + border;
            var y2d = (32 * yShift) + (8 * z2y) + (x * x2y) + (y * y2y) - (z * z2y) + border;

            if (x2d <= 0 || x2d >= widht  || y2d <= 0 || y2d >= height )
            {
                return;
            }

            DrawCube(ctx, x2d, y2d);
        }

        private static void DrawCube(IImageProcessingContext ctx, int x2d, int y2d)
        {
            //try
            //{
                ctx.DrawImage(cube, new Point(x2d, y2d), 1);
            //}
            //catch (Exception ex)
            //{
                //Console.WriteLine(ex);
                //Console.WriteLine($"{x2d} {y2d}");
            //}
        }
    }
}
