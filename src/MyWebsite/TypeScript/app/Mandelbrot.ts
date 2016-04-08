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
			urlParser("color0") != "" ? Color.fromHex(`#${urlParser("color0")}`) : new Color(64, 64, 64),
			urlParser("color1") != "" ? Color.fromHex(`#${urlParser("color1")}`) : new Color(128, 128, 128),
			urlParser("color2") != "" ? Color.fromHex(`#${urlParser("color2")}`) : new Color(192, 192, 192)
		]);
	});

/**
 * Mandelbrot
 */
class Mandelbrot {

	private currentData: string;

	public get colors(): Color[] {
		return this._colors;
	}
	public set colors(v: Color[]) {
		this._colors = v.sort(
			(a, b) => a.getBrightness() - b.getBrightness()
		);

		if (this.isLoaded) {
			this.redrawFractal(this.currentData);
		}
	}
	private _colors: Color[] = [Color.white(), Color.white(), Color.white()];

	private palleteR: Uint8Array = new Uint8Array(4096);
	private palleteG: Uint8Array = new Uint8Array(4096);
	private palleteB: Uint8Array = new Uint8Array(4096);

	private apiUrl: string = "/api/Projects/Mandelbrot/";

	private fractalModel: ViewModel;

	private fractalData: number[];

	public get isLoaded(): boolean {
		return this._isLoaded;
	}
	public set isLoaded(v: boolean) {
		this._isLoaded = v;
		this._scope.isLoaded = v;
		if (v) {
			clearInterval(this.intervalDescriptor);
			this._scope.state = "HU 3910 - Mandelbrot";
		}
		this._scope.$apply();
	}
	private _isLoaded: boolean = true;

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
		};

		this._scope.stopGeneration = () => {
			this.isLoaded = true;
		};

		this._scope.generateLink = () => {
			var link = `${window.location.href.split('?')[0]}?${this.fractalModel.exportToUrl()}`;
			for (var index = 0; index < this.colors.length; index++) {
				link += `&color${index}=${this.colors[index].exportAsHex()}`;
			}

			window.prompt("Copy to clipboard: Ctrl+C, Enter", link);
		};

		this._scope.zoom = (direction: string) => {
			alert("Zoom " + direction);
		};

		this._scope.move = (direction: string) => {
			alert("Move " + direction);
		};

		this._scope.state = "HU 3910 - Mandelbrot";
		this._scope.isLoaded = true;
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
			if (model == null) {
				this.isLoaded = true;
				this._scope.state = "Server busy. Reload the page.";
				this._scope.$apply();
				alert("There is another fractal generating for a different user. System allows only one generation at a time. Please, reload the page after a while.");
				return;
			}

			this.fractalModel = new ViewModel(this.canvas)
			this.fractalModel.initWithModel(model);

			this.intervalDescriptor = setInterval(() => {
				this.reloadFractal()
			}, 2000);
		});
	}

	private reloadFractal() {
		if (this.isLoaded) {
			return
		}

		$.get(this.apiUrl + "GetData", { id: this.fractalModel.id }, rawData => {
			if (rawData != null) {
				this.currentData = rawData;
				this.redrawFractal(rawData);
			}
		});

		$.get(this.apiUrl + "IsDone", { id: this.fractalModel.id }, response => {
			if (response == null) {
				this.isLoaded = true;
			}

			if (!this.isLoaded) {
				this._scope.state = `Loading... Iteration ${response}`; // response
				this._scope.$apply();
			}
		});
	}

	private redrawFractal(data: string) {

		let map = Mandelbrot.mapAbcToNum;

		let ctx: CanvasRenderingContext2D = this.canvas.getContext("2d");
		let imgData: ImageData = ctx.createImageData(this.fractalModel.width, this.fractalModel.height);

		let points = imgData.data.length / 4;

		this.updatePallete();

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

	private updatePallete() {

		let ci = 0;
		let theColor: Color = Color.black();
		let theColors: Color[] = [
			Color.black(),
			this.colors[0],
			this.colors[1],
			this.colors[2],
			Color.white()
		];

		for (var i = 0; i < 4096; i++) {
			let brightness = i / 16;

			while (brightness > theColors[ci + 1].getBrightness())
				ci += 1;

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

	private currentColor: Color = Color.white();
	private currentTab: ColorTab = ColorTab.First;

	private oldColors: [Color] = [Color.white(), Color.white(), Color.white()];
	private colors: [Color] = [Color.white(), Color.white(), Color.white()];

	constructor($scope: any) {

		$scope.data = {
			redColor: 255,
			greenColor: 255,
			blueColor: 255,
			brightness: 100,
			message: "The brightness is within the good range"
		};

		$scope.style = {
			colorItemStyle: {
				"height": "50px"
			}
		};

		$scope.class = {
			footer: ["bar", "bar-footer", "bar-balanced"]
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

		var tmpColors = new Array<Color>(3);
		for (var index = 0; index < colors.length; index++) {
			tmpColors[index] = Color.clone(colors[index]);
			this.colors[index] = Color.clone(colors[index]);
			this.currentTab = index;
			this.reloadColor();
		}
		globalMandelbrot.colors = tmpColors;
	}

	private colorChangeHandler(): void {
		this.$scope.style.colorItemStyle["background-color"] = this.currentColor.exportAsRGBA();
		this.$scope.style[`color${this.currentTab}BtnStyle`]["background-color"] = this.currentColor.exportAsRGBA();

		let brightness = Math.round(this.currentColor.getBrightness() * (100 / 255));
		this.$scope.data.brightness = brightness;

		let setClass = (_class: any, _value: string) => {
			_class.pop("bar-balanced");
			_class.pop("bar-assertive");
			_class.push(`bar-${_value}`);
		};
		let acceptedError = 2;

		if (brightness < (25 * (this.currentTab + 1) - acceptedError)) {
			setClass(this.$scope.class.footer, "assertive");
			this.$scope.data.message = `The brightness is too low (need around ${25 * (this.currentTab + 1)}%)`;
		} else if (brightness > (25 * (this.currentTab + 1) + acceptedError)) {
			setClass(this.$scope.class.footer, "assertive");
			this.$scope.data.message = `The brightness is too high (need around ${25 * (this.currentTab + 1)}%)`;
		} else {
			setClass(this.$scope.class.footer, "balanced");
			this.$scope.data.message = "The brightness is within the good range";
		}

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

	public static fromHex(hex: string): Color {
		let result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
		return new Color(parseInt(result[1], 16), parseInt(result[2], 16), parseInt(result[3], 16));
	}

	public static clone(color: Color): Color {
		return new Color(color.red, color.green, color.blue);
	}

	public static black(): Color {
		return new Color(0, 0, 0);
	}

	public static white(): Color {
		return new Color(256, 256, 256);
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

	public exportAsHex(includeHash: boolean = false): string {
		let r: string = (this.red > 0 ? this.red.toString(16) : "00");
		let g: string = (this.green > 0 ? this.green.toString(16) : "00");
		let b: string = (this.blue > 0 ? this.blue.toString(16) : "00");
		let h: string = (includeHash ? "#" : "");
		
		return `${h}${r}${g}${b}`;
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
		this.centerX = urlParser("centerX") !== "" ? parseFloat(urlParser("centerX")) : -0.7794494628906250;
		this.centerY = urlParser("centerY") !== "" ? parseFloat(urlParser("centerY")) : -0.1276645660400390;
		this.width = canvas.width;
		this.height = canvas.height;
		this.log2scale = urlParser("log2scale") !== "" ? parseInt(urlParser("log2scale")) : 19;
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

	public exportToUrl(): string {
		return `centerX=${this.centerX}&centerY=${this.centerY}&log2scale=${this.log2scale}`;
	}
}

let resizeHandler = () => {
	$("#contentTR").height($("#ionicContent").height() - 100);
};

let urlParser = (name) => {
	name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
	var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"), results = regex.exec(location.search);
	return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
};

// For some reason, TS does not know about this property
interface HTMLAnchorElement {
    download: string;
}
