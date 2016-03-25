angular.module('starter', ['ionic'])
    .run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        new Mandelbrot();
    });
})
    .controller('MainCtrl', function ($scope, $ionicModal) {
    $ionicModal.fromTemplateUrl('settings-modal.html', {
        scope: $scope,
        animation: 'slide-in-up',
        backdropClickToClose: false,
        hardwareBackButtonClose: false
    }).then(function (modal) {
        $scope.modal = modal;
    });
    $scope.openModal = function () {
        $scope.modal.show();
    };
    $scope.closeModal = function () {
        $scope.modal.hide();
    };
    $scope.$on('$destroy', function () {
        $scope.modal.remove();
    });
    new Settings($scope);
});
var Mandelbrot = (function () {
    function Mandelbrot() {
        this.setup();
    }
    Mandelbrot.prototype.setup = function () {
        $(window).resize(resizeHandler);
        $(window).trigger("resize");
        $("#redColorRange").change(function () {
            alert("H");
        });
    };
    return Mandelbrot;
}());
var Settings = (function () {
    function Settings($scope) {
        this.colorElement = "colorsItem";
        this.currentColor = {
            redColor: 255,
            greenColor: 255,
            blueColor: 255
        };
        $scope.data = {
            redColor: 255,
            greenColor: 255,
            blueColor: 255,
            iterations: 1024,
            gamma: 50
        };
        this.$scope = $scope;
        var _this = this;
        $scope.setRangeLabel = function (rangeName) {
            var value = _this.$scope.data[rangeName];
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
        };
    }
    Settings.prototype.setColor = function () {
        $("#" + this.colorElement)
            .css("background-color", "rgba("
            + this.currentColor.redColor + ","
            + this.currentColor.greenColor + ","
            + this.currentColor.blueColor + ",1)");
    };
    return Settings;
}());
var resizeHandler = function () {
    $("#contentTR").height($("#ionicContent").height() - 100);
};

//# sourceMappingURL=Mandelbrot.js.map
