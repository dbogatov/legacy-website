using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebsite.ViewModels.Mandelbrot
{
	public struct Point
	{
		public int x { get; set; }
		public int y { get; set; }
    }
	
	public class MandelbrotViewModel
	{
		public Point leftTop { get; set; }
		public Point bottomRight { get; set; }
		public int zoom { get; set; }
    }	
}