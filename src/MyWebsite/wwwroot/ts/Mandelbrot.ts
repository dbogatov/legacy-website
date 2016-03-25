/// <reference path="../extdefinitions/tsd.d.ts" />

// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'

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

	
	constructor($scope : any) {

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

	private setColor() : void {
		$("#" + this.colorElement)
			.css(
			"background-color",
			"rgba("
			+ this.currentColor.redColor + ","
			+ this.currentColor.greenColor + ","
			+ this.currentColor.blueColor + ",1)");
	}	
}

let resizeHandler = () => {
	$("#contentTR").height($("#ionicContent").height() - 100);
};
