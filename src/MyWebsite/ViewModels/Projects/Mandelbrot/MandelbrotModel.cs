namespace MyWebsite.ViewModels.Mandelbrot
{
	public class MandelbrotModel
	{
        public int id { get; set; }

        public byte log2scale { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        public double centerX { get; set; }
        public double centerY { get; set; }
    }
}