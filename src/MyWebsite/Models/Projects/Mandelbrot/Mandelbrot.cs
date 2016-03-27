using System;
using System.Collections.Generic;
using System.Threading;

namespace MyWebsite.Models.Mandelbrot
{
    public class Mandelbrot
    {
        private static SortedList<int, Mandelbrot> all;
        private static void monitor()
        {
            while (all != null)
            {
                List<int> toRemove = new List<int>();

                foreach (KeyValuePair<int, Mandelbrot> item in all)
                {
                    if (item.Value.working)
                    {
                        if ((DateTime.UtcNow - item.Value.lastAccessTime).TotalMinutes > 1)
                        {
                            item.Value.stop = true;
                        }
                    }
                    else
                    {
                        if ((DateTime.UtcNow - item.Value.lastAccessTime).TotalMinutes > 11)
                        {
                            toRemove.Add(item.Key);
                        }
                    }
                }

                lock (all)
                {
                    foreach (int keyId in toRemove)
                    {
                        all.Remove(keyId);
                    }
                }

                Thread.Sleep(30000);
            }
        }

        static Mandelbrot()
        {
            all = new SortedList<int, Mandelbrot>();
            (new Thread(monitor)).Start();
        }

        public static Mandelbrot GetNew(int oldId, double centerX, double centerY, int width, int height, byte log2scale)
        {
            if (oldId != 0)
            {
                Mandelbrot oldOne;
                if (all.TryGetValue(oldId, out oldOne))
                {
                    oldOne.stop = true;
                    all.Remove(oldId);
                }
                else return null;
            }
            else
            {
                if (all.Count > 11) return null;

                int active = 0;
                foreach (Mandelbrot instance in all.Values)
                    if (instance.working) active += 1;
                if (active > 1) return null;
            }

            int newId;
            do newId = rnd.Next(1, int.MaxValue);
            while (all.ContainsKey(newId));

            Mandelbrot newOne = new Mandelbrot(newId, centerX, centerY, width, height, log2scale);
            all.Add(newOne.id, newOne);

            (new Thread(newOne.calculate)).Start();

            return newOne;
        }

        public static string GetData(int id)
        {
            Mandelbrot instance;
            if (all.TryGetValue(id, out instance) && !instance.drawing)
            {
                return new string(instance.display());
            }
            return null;
        }


        private static readonly byte[] mapAbcToNum = new byte[128]
        {
            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
            64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
            01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 64, 64, 64, 64, 64, 64,
            64, 36, 35, 34, 33, 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22,
            21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 64, 64, 64, 63, 00,
            64, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
            52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 64, 64, 64, 64, 64
        };

        private static readonly char[] mapNumToAbc = new char[64]
        {
            '_', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'Z', 'Y', 'X', 'W', 'V',
            'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G', 'F',
            'E', 'D', 'C', 'B', 'A', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '^'
        };

        private static Random rnd = new Random();

        private DateTime lastAccessTime;

        private bool working;
        private bool drawing;

        private bool stop;

        private int incomplete;
        private int iterations;

        public readonly int id;

        public readonly byte log2scale;

        public readonly int width;
        public readonly int height;

        public readonly double centerX;
        public readonly double centerY;

        readonly PixelData[] pixels;
        readonly List<LayerData> layers;

        private Mandelbrot(int id, double centerX, double centerY, int width, int height, byte log2scale)
        {
            this.id = id;
            this.width = width - (width % 2);
            this.height = height - (height % 2);
            this.log2scale = log2scale;
            double delta = Math.Pow(2, -log2scale);
            this.centerX = centerX - (centerX % delta);
            this.centerY = centerY - (centerY % delta);

            this.layers = new List<LayerData>();
            this.pixels = new PixelData[this.width * this.height];

            int n = 0;
            for (int yi = 0; yi < this.height; yi++)
            {
                double y = this.centerY + delta * (yi - this.height / 2);
                for (int xi = 0; xi < this.width; xi++)
                {
                    double x = this.centerX + delta * (xi - this.width / 2);
                    pixels[n++] = new PixelData(x, y, xi, yi);
                }
            }

            incomplete = n;
            iterations = 0;

            lastAccessTime = DateTime.UtcNow;
        }

        private void calculate()
        {
            working = true;

            int n = incomplete;

            do
            {
                for (int i = 0; i < n;)
                {
                    double xx = pixels[i].x * pixels[i].x;
                    double yy = pixels[i].y * pixels[i].y;
                    if (xx + yy > 4)
                    {
                        n -= 1;
                        PixelData done = pixels[i];
                        pixels[i] = pixels[n];
                        pixels[n] = done;
                    }
                    else
                    {
                        double xy = pixels[i].x * pixels[i].y;
                        pixels[i].x = xx - yy + pixels[i].x0;
                        pixels[i].y = 2 * xy + pixels[i].y0;
                        i += 1;
                    }
                }
                iterations += 1;
                if (n < incomplete)
                {
                    int doneOnStep = incomplete - n;
                    layers.Add(new LayerData(iterations, doneOnStep));
                    incomplete = n;
                }
            } while (incomplete > 0 && !stop);

            working = false;
        }

        private char[] display()
        {
            drawing = true;

            int ready = layers.Count;
            double[] Ls = new double[ready];

            double sum = Ls[0] = Math.Pow(layers[0].pN, 0.75);
            for (int li = 1; li < ready; li++)
                sum = Ls[li] = sum + Math.Pow(layers[li].pN, 0.75) / (double)(layers[li].iN - layers[li - 1].iN);

            int pi = pixels.Length - 1;
            char[] result = new char[width * height * 2];

            for (int li = 0; li < ready; li++)
            {
                int L = 4095 - (int)Math.Floor(4096 * (1 - Ls[li] / sum));
                char low = mapNumToAbc[L & 63];
                char high = mapNumToAbc[(L >> 6) & 63];

                for (int j = pi - layers[li].pN; pi > j; pi--)
                {
                    int index = 2 * (pixels[pi].xi + pixels[pi].yi * width);
                    result[index] = high;
                    result[index + 1] = low;
                }
            }

            drawing = false;

            return result;
        }
    }

    class LayerData
    {
        public int iN, pN;
        public LayerData(int iN, int pN)
        {
            this.iN = iN;
            this.pN = pN;
        }
    }

    class PixelData
    {
        public double x0, y0, x, y;
        public int xi, yi;
        public PixelData(double x0, double y0, int sX, int sY)
        {
            this.x0 = this.x = x0;
            this.y0 = this.y = y0;
            this.xi = sX;
            this.yi = sY;
        }
    }
}