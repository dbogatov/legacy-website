/// <reference path="../extdefinitions/tsd.d.ts" />

// Ionic Starter App

angular.module('starter', ['ionic'])
	.run(function($ionicPlatform) {
		$ionicPlatform.ready(function() {

		});
	})
	.controller('MainCtrl', function($scope, $ionicModal) {

		$ionicModal.fromTemplateUrl('settings-modal.html', {
			scope: $scope,
			animation: 'slide-in-up',
			backdropClickToClose: false,
			hardwareBackButtonClose: false
		}).then(function(modal) {
			$scope.modal = modal;
		});
		$scope.openModal = function() {
			$scope.modal.show();
		};
		$scope.closeModal = function() {
			$scope.modal.hide();
		};
		//Cleanup the modal when we're done with it!
		$scope.$on('$destroy', function() {
			$scope.modal.remove();
		});

		new Mandelbrot($scope);

		new Settings($scope);
	});

/**
 * Mandelbrot
 */
class Mandelbrot {

	private apiUrl: string = "/api/Projects/Mandelbrot/";

	private fractalModel: ViewModel;

	private fractalData: number[];

	private isLoaded: boolean = true;

	private intervalDescriptor: number;

	private canvas: HTMLCanvasElement = <HTMLCanvasElement>document.getElementById("myCanvas");

	private static mapAbcToNum = [
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 0, 0, 0, 0, 0,
		0, 36, 35, 34, 33, 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22,
		21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 0, 0, 0, 63, 0,
		0, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
		52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 0, 0, 0, 0, 0
	];

	private _scope: any;

	constructor($scope: any) {
		this._scope = $scope;

		this.setup();
		this.getNewFractal();
	}

	private setup(): void {
		$(window).resize(resizeHandler);
		$(window).trigger("resize");

		// Make canvas visually fill the positioned parent
		this.canvas.style.width = "100%";
		this.canvas.style.height = "100%";
		// ...then set the internal size to match
		this.canvas.width = this.canvas.offsetWidth;
		this.canvas.height = this.canvas.offsetHeight;

		this._scope.exportImage = () => {
			if (confirm("Do you want to download the current fractal as PNG image?")) {

				this.isLoaded = true;

				var dt = this.canvas.toDataURL('image/png');
				// Change MIME type to trick the browser to downlaod the file instead of displaying it
				dt = dt.replace(/^data:image\/[^;]*/, 'data:application/octet-stream');

				// In addition to <a>'s "download" attribute, you can define HTTP-style headers
				dt = dt.replace(/^data:application\/octet-stream/, 'data:application/octet-stream;headers=Content-Disposition%3A%20attachment%3B%20filename=Canvas.png');

				var link: HTMLAnchorElement = document.createElement("a");
				link.href = dt;
				link.download = "Mandelbrot.png";

				link.click();
			}
		}

		this._scope.zoom = (direction: string) => {
			alert("Zoom " + direction);
		}

		this._scope.move = (direction: string) => {
			alert("Move " + direction);
		}
	}

	private getNewFractal() {
		this.isLoaded = false;
		this.fractalData = null;
		this.fractalModel = null;
		clearInterval(this.intervalDescriptor);

		$.get(this.apiUrl + "GetNew", new ViewModel(this.canvas), model => {

			this.fractalModel = new ViewModel(this.canvas)
			this.fractalModel.initWithModel(model);

			this.intervalDescriptor = setInterval(() => {
				this.reloadFractal()
			}, 2000);
		});
	}

	private reloadFractal() {
		$.get(this.apiUrl + "GetData", { id: this.fractalModel.id }, rawData => {
			if (rawData != null) {
				this.redrawFractal(rawData);
			}
		});

		$.get(this.apiUrl + "IsDone", { id: this.fractalModel.id }, response => {
			if (response != null) {
				if (!response) {
					this.isLoaded = false;
				}
				if (this.isLoaded) {
					clearInterval(this.intervalDescriptor);
				}
			}
		});
	}

	private redrawFractal(data: string) {

		let map = Mandelbrot.mapAbcToNum;

		let ctx: CanvasRenderingContext2D = this.canvas.getContext("2d");
		let imgData: ImageData = ctx.createImageData(this.fractalModel.width, this.fractalModel.height);

		let points = imgData.data.length / 4;

		for (var i = 0; i < points; i++) {
			var value =
				(map[data.charCodeAt(i << 1) & 127] << 6) |
				map[data.charCodeAt((i << 1) | 1) & 127];

			imgData.data[(i << 2)] = Math.floor(value >> 4);
			imgData.data[(i << 2) | 1] = Math.floor(value >> 4);
			imgData.data[(i << 2) | 2] = Math.floor(value >> 4);
			imgData.data[(i << 2) | 3] = 255;

		}
		ctx.putImageData(imgData, 0, 0);
	}
}

/**
 * Settings
 */
class Settings {

	private $scope: any;
	private colorElement: string = "colorsItem";
	private currentColor = {
		redColor: 255,
		greenColor: 255,
		blueColor: 255
	}

	constructor($scope: any) {

		$scope.data = {
			redColor: 255,
			greenColor: 255,
			blueColor: 255,
			iterations: 1024,
			gamma: 50
		}

		this.$scope = $scope;

		let _this = this;

		$scope.setRangeLabel = function(rangeName) {
			let value = _this.$scope.data[rangeName];
			$("#" + rangeName + "RangeLabel").text(value);

			switch (rangeName) {
				case "redColor":
				case "greenColor":
				case "blueColor":
					_this.currentColor[rangeName] = value;
					_this.setColor();
					break;
				default:
					break;
			}
		}
	}

	private setColor(): void {
		$("#" + this.colorElement)
			.css(
			"background-color",
			"rgba("
			+ this.currentColor.redColor + ","
			+ this.currentColor.greenColor + ","
			+ this.currentColor.blueColor + ",1)");
	}
}

/**
 * ViewModel
 */
class ViewModel {
	public centerX: number;
	public centerY: number;
	public width: number;
	public height: number;
	public log2scale: number;
	public id: number;

	constructor(canvas: HTMLCanvasElement) {
		this.centerX = -0.7794494628906250;
		this.centerY = -0.1276645660400390;
		this.width = canvas.width;
		this.height = canvas.height;
		this.log2scale = 19;
	}

	public initWithModel(model: any): void {
		if (model == null) {
			return;
		}

		this.centerX = model.centerX;
		this.centerY = model.centerY;
		this.width = model.width;
		this.height = model.height;
		this.log2scale = model.log2scale;
		this.id = model.id;
	}
}

let resizeHandler = () => {
	$("#contentTR").height($("#ionicContent").height() - 100);
};

// For some reason, TS does not know about this property
interface HTMLAnchorElement {
    download: string;
}