/// <reference path="../extdefinitions/tsd.d.ts" />

// Ionic Starter App

angular.module('starter', ['ionic'])
	.run(function($ionicPlatform) {
		$ionicPlatform.ready(function() {
			new Mandelbrot();
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

		new Settings($scope);
	});

/**
 * Mandelbrot
 */
class Mandelbrot {
	constructor() {
		this.setup();
	}

	private setup(): void {
		$(window).resize(resizeHandler);
		$(window).trigger("resize");

		$("#redColorRange").change(() => {
			alert("H");
		});
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

	private parseData(data: string): number[] {
		const mapAbcToNum =
			[
				 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
				 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
				 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
				 1,  2,  3,  4,  5,  6,  7,  8,  9, 10,  0,  0,  0,  0,  0,  0,
				 0, 36, 35, 34, 33, 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22,
				21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11,  0,  0,  0, 63,  0,
				 0, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
				52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62,  0,  0,  0,  0,  0
			]
		let result: number[] = new Array<number>(data.length / 2);
		for (var i = 0; i < result.length; i++) {
			result[i] = (mapAbcToNum[data.charCodeAt(i << 1) & 127] << 6) |
				mapAbcToNum[data.charCodeAt((i << 1) | 1) & 127];
		}
		return result;
	}
}

let resizeHandler = () => {
	$("#contentTR").height($("#ionicContent").height() - 100);
};
