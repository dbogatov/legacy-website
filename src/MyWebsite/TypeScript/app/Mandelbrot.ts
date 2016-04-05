/// <reference path="../extdefinitions/tsd.d.ts" />

// Ionic Starter App

var globalMandelbrot: Mandelbrot;
var globalSettings: Settings;

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
			globalSettings.willOpen();
			$scope.modal.show();
		};
		$scope.closeModal = function(shouldSave) {
			globalSettings.willClose(shouldSave);
			$scope.modal.hide();
		};
		//Cleanup the modal when we're done with it!
		$scope.$on('$destroy', function() {
			$scope.modal.remove();
		});

		globalMandelbrot = new Mandelbrot($scope);

		globalSettings = new Settings($scope);
		globalSettings.loadSettings([
			new Color(64, 64, 64),
			new Color(128, 128, 128),
			new Color(192, 192, 192)
		]);
	});

/**
 * Mandelbrot
 */
class Mandelbrot {

	private _colors: Color[] = [Color.white(), Color.white(), Color.white()];
	
	public get colors() : Color[] {
		return this._colors;
	}

	public set colors(v: Color[]) {
		this._colors = v.sort(
			(a, b) => a.getBrightness() - b.getBrightness()
		);
	}
		
	private palleteR: Uint8Array = new Uint8Array(4096);
	private palleteG: Uint8Array = new Uint8Array(4096);
	private palleteB: Uint8Array = new Uint8Array(4096);

	private apiUrl: string = "/api/Projects/Mandelbrot/";

	private fractalModel: ViewModel;

	private fractalData: number[];

	private isLoaded: boolean = true;

	private intervalDescriptor: number;

	private canvas: HTMLCanvasElement = <HTMLCanvasElement>document.getElementById("myCanvas");
	private jCanvas: JQuery = $("#myCanvas");

	private canvasOriginalPosition: JQueryCoordinates;

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

		this.canvasOriginalPosition = this.jCanvas.position();

		(<any>this.jCanvas).draggable({
			stop: (event, ui) => {
				this.viewMoved(
					ui.position.top - ui.originalPosition.top,
					ui.position.left - ui.originalPosition.left
				);
			}
		});

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

	private viewMoved(offsetTop: number, offsetLeft: number): void {
		console.log(`True offsets: 
			top: ${offsetTop},
			left: ${offsetLeft}`
		);
		this.putCanvasBack();
	}

	private putCanvasBack() {
		this.jCanvas.css({
			top: this.canvasOriginalPosition.top,
			left: this.canvasOriginalPosition.left,
			position: "absolute"
		});
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

		this.UpdatePallete();

		for (var i = 0; i < points; i++) {
			var value =
				(map[data.charCodeAt(i << 1) & 127] << 6) |
				map[data.charCodeAt((i << 1) | 1) & 127];

			imgData.data[(i << 2)] = this.palleteR[value];
			imgData.data[(i << 2) | 1] = this.palleteG[value];
			imgData.data[(i << 2) | 2] = this.palleteB[value];
			imgData.data[(i << 2) | 3] = 255;

		}
		ctx.putImageData(imgData, 0, 0);
	}

	private UpdatePallete() {

		let ci = 0;
		let theColor: Color = Color.black();
		let theColors: Color[] = [Color.black(), this.colors[0], this.colors[1], this.colors[2], Color.white()];
        for (var i = 0; i < 4096; i++) {
			let brightness = i / 16;
			while (brightness > theColors[ci + 1].getBrightness()) ci += 1;
			theColor.update(theColors[ci], brightness, theColors[ci + 1]);
			this.palleteR[i] = Math.floor(theColor.red);
			this.palleteG[i] = Math.floor(theColor.green);
			this.palleteB[i] = Math.floor(theColor.blue);
		}
	}
}

/**
 * Settings
 */
class Settings {

	private $scope: any;
	private colorElement: string = "colorsItem";

	private currentColor: Color = Color.white();
	private currentTab: ColorTab = ColorTab.First;

	private oldColors: [Color] = [Color.white(), Color.white(), Color.white()];
	private colors: [Color] = [Color.white(), Color.white(), Color.white()];

	constructor($scope: any) {

		$scope.data = {
			redColor: 255,
			greenColor: 255,
			blueColor: 255
		};

		$scope.style = {
			colorItemStyle: {
				"height": "50px"
			}
		};

		for (var index = 0; index < this.colors.length; index++) {
			$scope.style[`color${index}BtnStyle`] = {};
		}

		this.$scope = $scope;

		let _this = this;

		$scope.setRangeLabel = function(rangeName) {
			let value = _this.$scope.data[rangeName];

			_this.currentColor.setValue(rangeName, parseInt(value));
			_this.colorChangeHandler();

			_this.save();
		};

		$scope.setTab = function(newTab) {
			_this.currentTab = parseInt(newTab);
			_this.reloadColor();
		};
	}

	public willOpen(): void {
		for (var index = 0; index < this.colors.length; index++) {
			this.oldColors[index] = Color.clone(this.colors[index]);
		}
	}

	public willClose(shouldSave: boolean): void {
		if (shouldSave) {
			var tmpColors = new Array<Color>(3);
			for (var index = 0; index < this.colors.length; index++) {
				tmpColors[index] = Color.clone(this.colors[index]);
			}
			globalMandelbrot.colors = tmpColors;
		} else {
			for (var index = 0; index < this.colors.length; index++) {
				this.colors[index] = Color.clone(this.oldColors[index]);
				this.currentTab = index;
				this.reloadColor();
			}
		}
	}

	public loadSettings(colors: [Color]): void {
		for (var index = 0; index < colors.length; index++) {
			this.colors[index] = Color.clone(colors[index]);
			this.currentTab = index;
			this.reloadColor();
			globalMandelbrot[index] = Color.clone(colors[index]);
		}
	}

	private colorChangeHandler(): void {
		this.$scope.style.colorItemStyle["background-color"] = this.currentColor.exportAsRGBA();
		this.$scope.style[`color${this.currentTab}BtnStyle`]["background-color"] = this.currentColor.exportAsRGBA();
	}

	private reloadColor(): void {
		this.$scope.data.redColor = this.colors[this.currentTab].red;
		this.$scope.data.greenColor = this.colors[this.currentTab].green;
		this.$scope.data.blueColor = this.colors[this.currentTab].blue;

		this.currentColor = this.colors[this.currentTab];
		this.colorChangeHandler();
	}

	private save(): void {
		this.colors[this.currentTab] = this.currentColor;
	}

}

enum ColorTab {
	First = 0,
	Second = 1,
	Third = 2
}

/**
 * Color
 */
class Color {
	public red: number = 255.0;
	public green: number = 255.0;
	public blue: number = 255.0;

	public constructor(red: number, green: number, blue: number) {
		this.red = red;
		this.green = green;
		this.blue = blue;
	}

	public static clone(color: Color): Color {
		return new Color(color.red, color.green, color.blue);
	}

	public static black(): Color {
		return new Color(0, 0, 0);
	}

	public static white(): Color {
		return new Color(255, 255, 255);
	}

	public setRGB(red: number, green: number, blue: number) {
		this.red = red;
		this.green = green;
		this.blue = blue;
	}

	public setColor(color: Color) {
		this.red = color.red;
		this.green = color.green;
		this.blue = color.blue;
	}

	public exportAsRGBA(alpha: number = 1.0): string {
		return `rgba(${this.red},${this.green},${this.blue},${alpha})`;
	}

	public exportAsHex(): string {
		return `#${this.red.toString(16)}${this.green.toString(16)}${this.blue.toString(16)}`;
	}

	public setValue(color: string, value: number): void {
		switch (color) {
			case "redColor":
				this.red = value;
				break;
			case "greenColor":
				this.green = value;
				break;
			case "blueColor":
				this.blue = value;
				break;
		}
	}

    public update(prev: Color, curr: number, next: Color) {

		let prevBright = prev.getBrightness();
		let nextBright = next.getBrightness();

		if (prevBright == nextBright) {
			this.red = (prev.red + next.red) / 2;
			this.green = (prev.green + next.green) / 2;
			this.blue = (prev.blue + next.blue) / 2;
		} else {
			let scale = (curr - prevBright) / (nextBright - prevBright);
			this.red = prev.red + (next.red - prev.red) * scale;
			this.green = prev.green + (next.green - prev.green) * scale;
			this.blue = prev.blue + (next.blue - prev.blue) * scale;
		}
	}

	public getBrightness(): number {
		return 0.299 * this.red + 0.587 * this.green + 0.114 * this.blue;
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