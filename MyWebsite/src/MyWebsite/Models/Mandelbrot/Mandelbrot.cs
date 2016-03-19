using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;
using MyWebsite.Services;
using Newtonsoft.Json;
using Microsoft.AspNet.Http;
using MyWebsite.ViewModels.Mandelbrot;
using System;

namespace MyWebsite.Models.Mandelbrot {
	
	public class Mandelbrot
	{
        private readonly MandelbrotViewModel _model;

        public Mandelbrot(MandelbrotViewModel model) {
            _model = model;
        }
		
		public IEnumerable<IEnumerable<int>> computeMatrix() {
			Random random = new Random();
			
            return Enumerable.Repeat(Enumerable.Repeat(random.Next(100), 15), 15);
        }
	}
	
}