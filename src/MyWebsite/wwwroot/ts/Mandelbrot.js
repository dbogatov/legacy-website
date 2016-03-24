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
    $scope.$on('modal.hidden', function () {
    });
    $scope.$on('modal.removed', function () {
    });
});
var Mandelbrot = (function () {
    function Mandelbrot() {
        this.setup();
    }
    Mandelbrot.prototype.setup = function () {
        $(window).resize(resizeHandler);
        $(window).trigger("resize");
    };
    return Mandelbrot;
}());
var resizeHandler = function () {
    $("#contentTR").height($("#ionicContent").height() - 100);
};

//# sourceMappingURL=Mandelbrot.js.map
