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
                        if ((DateTime.UtcNow - item.Value.lastAccessTime).TotalSeconds > 7)
                        {
                            item.Value.stop = true;
                        }
                    }
                    else
                    {
                        if ((DateTime.UtcNow - item.Value.lastAccessTime).TotalSeconds > 15)
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

                Thread.Sleep(1000);
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
                if (all.Count > 0) return null;

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
            if (all.TryGetValue(id, out instance))
            {
				return instance.display();
            }
            return null;
		}

        public static bool? IsDone(int id)
        {
            Mandelbrot instance;
            if (all.TryGetValue(id, out instance))
            {
                return !instance.working;
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

        public readonly int id;
        public readonly int width;
        public readonly int height;
        public readonly double centerX;
        public readonly double centerY;
        public readonly byte log2scale;

        private readonly int total;
        private readonly double[] xValues;
        private readonly double[] yValues;
        private readonly double[] xArray;
        private readonly double[] yArray;
        private readonly ushort[] layers;
        private readonly uint[] iterations;
        private readonly uint[] pointsDone;

        private uint totalDone;
        private uint maxDoneOnStep;
        private uint iteration;
        private uint lastIteration;
        private int layer;

        private DateTime lastAccessTime;

        private bool working;
        private bool stop;

        private Mandelbrot(int id, double centerX, double centerY, int width, int height, byte log2scale)
        {
            this.id = id;
            this.width = width - (width % 2);
            this.height = height - (height % 2);
            this.log2scale = log2scale;
            double delta = Math.Pow(2, -log2scale);
            this.centerX = centerX - (centerX % delta);
            this.centerY = centerY - (centerY % delta);

            this.total = this.width * this.height;

            this.xValues = new double[this.width];
            this.yValues = new double[this.height];
            this.xArray = new double[this.total];
            this.yArray = new double[this.total];
            this.layers = new ushort[this.total];
            this.iterations = new uint[65536];
            this.pointsDone = new uint[65536];

            for (int xi = 0; xi < this.width; xi++)
                xValues[xi] = this.centerX + delta * (xi - this.width / 2);

            for (int yi = 0; yi < this.height; yi++)
                yValues[yi] = this.centerY + delta * (yi - this.height / 2);

            int pi = 0;
            for (int yi = 0; yi < this.height; yi++)
            {
                for (int xi = 0; xi < this.width; xi++)
                {
                    xArray[pi] = xValues[xi];
                    yArray[pi] = yValues[yi];
                    pi++;
                }
            }

            this.totalDone = 0;
            this.maxDoneOnStep = 0;

            this.iteration = 0;
			this.lastIteration = 0;

            this.layer = 0;

            lastAccessTime = DateTime.UtcNow;
        }

        private void calculate()
        {
            working = true;

            while (totalDone < total && layer < 65535)
            {
                uint doneOnStep = 0;
                ushort nextLayer = (ushort)(layer + 1);

                int pi = 0;
                for (int yi = 0; yi < this.height; yi++)
                {
                    for (int xi = 0; xi < this.width; xi++)
                    {
                        if (layers[pi] == 0)
                        {
                            double x = xArray[pi];
                            double y = yArray[pi];
                            double xx = x * x;
                            double yy = y * y;
                            if (xx + yy > 4)
                            {
                                doneOnStep += 1;
                                layers[pi] = nextLayer;
                            }
                            else
                            {
                                xArray[pi] = xx - yy + xValues[xi];
                                yArray[pi] = 2 * x * y + yValues[yi];
                            }
                        }
                        pi += 1;
                    }
                }
                iteration += 1;

                if (doneOnStep > 0)
                {
                    if (maxDoneOnStep < doneOnStep)
                        maxDoneOnStep = doneOnStep;
                    lastIteration = iteration;

                    totalDone += doneOnStep;
                    layer += 1;

                    iterations[layer] = iteration;
                    pointsDone[layer] = doneOnStep;
                }
                else if (lastIteration > 0)
                {
                    if (iteration - lastIteration > maxDoneOnStep) break;
                }

                if (stop) break;
            }

            working = false;
        }

        private string display()
        {
            if (layer == 0 || (DateTime.UtcNow - lastAccessTime).TotalSeconds < 2) return null;

            int ready = layer + 1;
            double[] Ls = new double[ready];

            double sum = Ls[0] = 0;
            for (int li = 1; li < ready; li++)
                sum = Ls[li] = sum + Math.Pow(pointsDone[li], 0.75) / (double)(iterations[li] - iterations[li - 1]);

            char[] low = new char[65536];
            char[] high = new char[65536];

            low[0] = high[0] = mapNumToAbc[0];
            for (int li = ready; li < 65536; li++)
                low[li] = high[li] = mapNumToAbc[0];

            for (int li = 1; li < ready; li++)
            {
                int L = 4095 - (int)Math.Floor(4096 * (1 - Ls[li] / sum));
                low[li] = mapNumToAbc[L & 63];
                high[li] = mapNumToAbc[(L >> 6) & 63];
            }

            char[] result = new char[total * 2];
            for (int pi = 0; pi < total; pi++)
            {
                int li = layers[pi];
                result[pi << 1] = high[li];
                result[(pi << 1) | 1] = low[li];
            }

            lastAccessTime = DateTime.UtcNow;

            return new string(result);
        }
    }
}