// Copied and modified from InventoryKamera(https://github.com/Andrewthe13th/Inventory_Kamera) under MIT License.

using Accord.Imaging.Filters;
using System;
using System.Drawing;
using Tesseract;

namespace ArtifactSelector.Scanner
{
    internal class BitmapProcessor
    {
        private static readonly string tesseractDatapath = $".\\tessdata";
        private static readonly string tesseractLanguage = "genshin_fast_09_04_21";

        internal static TesseractEngine tessEngine = new TesseractEngine(tesseractDatapath, tesseractLanguage, EngineMode.LstmOnly);

        internal static Bitmap ConvertToGrayscale(Bitmap bitmap)
        {
            return new Grayscale(0.2125, 0.7154, 0.0721).Apply(bitmap);
        }
        internal static void SetContrast(double contrast, ref Bitmap bitmap)
        {
            new ContrastCorrection((int)contrast).ApplyInPlace(bitmap);
        }
        internal static void SetInvert(ref Bitmap bitmap)
        {
            new Invert().ApplyInPlace(bitmap);
        }

        internal static void SetThreshold(int threshold, ref Bitmap bitmap)
        {
            new Threshold(threshold).ApplyInPlace(bitmap);
        }

        internal static void SetBrightness(int brightness, ref Bitmap bitmap)
        {
            if (brightness < -255)
            {
                brightness = -255;
            }

            if (brightness > 255)
            {
                brightness = 255;
            }

            Bitmap temp = bitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0)
                    {
                        cR = 1;
                    }

                    if (cR > 255)
                    {
                        cR = 255;
                    }

                    if (cG < 0)
                    {
                        cG = 1;
                    }

                    if (cG > 255)
                    {
                        cG = 255;
                    }

                    if (cB < 0)
                    {
                        cB = 1;
                    }

                    if (cB > 255)
                    {
                        cB = 255;
                    }

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            bitmap = (Bitmap)bmap.Clone();
        }

        private static byte[] CreateGammaArray(double color)
        {
            byte[] gammaArray = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                gammaArray[i] = (byte)Math.Min(255,
        (int)((255.0 * Math.Pow(i / 255.0, 1.0 / color)) + 0.5));
            }
            return gammaArray;
        }

        internal static void SetGamma(double red, double green, double blue, ref Bitmap bitmap)
        {
            Bitmap temp = bitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            byte[] redGamma = CreateGammaArray(red);
            byte[] greenGamma = CreateGammaArray(green);
            byte[] blueGamma = CreateGammaArray(blue);
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(redGamma[c.R],
                       greenGamma[c.G], blueGamma[c.B]));
                }
            }
            bitmap = (Bitmap)bmap.Clone();
        }

        internal static Bitmap CopyBitmap(Bitmap source, Rectangle region)
        {
            ClipToSource(source, ref region);
            return source.Clone(region, source.PixelFormat);

            void ClipToSource(Bitmap s, ref Rectangle r)
            {
                if (r.X + r.Width > source.Width) { r.Width = s.Width - r.X; }
                if (r.Y + r.Height > source.Height) { r.Height = s.Height - r.Y; }
            }
        }
        internal static string AnalyzeText(Bitmap bitmap, PageSegMode pageMode = PageSegMode.SingleLine, bool numbersOnly = false)
        {
            string text = "";

            if (numbersOnly)
            {
                tessEngine.SetVariable("tessedit_char_whitelist", "0123456789");
            }

            using (var page = tessEngine.Process(bitmap, pageMode))
            {
                using (var iter = page.GetIterator())
                {
                    iter.Begin();
                    do
                    {
                        text += iter.GetText(PageIteratorLevel.TextLine);
                    }
                    while (iter.Next(PageIteratorLevel.TextLine));
                }
            }

            return text;
        }

    }
}
