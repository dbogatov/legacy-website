define(["require", "exports"], function (require, exports) {
    "use strict";
    var Utility;
    (function (Utility_1) {
        var Utility = (function () {
            function Utility() {
            }
            Utility.getMonthName = function (month) {
                var monthNames = ["January", "February", "March", "April", "May", "June",
                    "July", "August", "September", "October", "November", "December"
                ];
                return monthNames[month];
            };
            Utility.enableAJAXLoadBar = function () {
                var handler = function (event) {
                    event.stopPropagation();
                    event.preventDefault();
                };
                $(document).ajaxStart(function () {
                    document.addEventListener("click", handler, true);
                    $('#myModal').modal({
                        keyboard: false
                    });
                    $('#myModal').modal('show');
                });
                $(document).ajaxStop(function () {
                    $('#myModal').modal('hide');
                    document.removeEventListener("click", handler, true);
                });
            };
            return Utility;
        }());
        Utility_1.Utility = Utility;
    })(Utility = exports.Utility || (exports.Utility = {}));
});

//# sourceMappingURL=Utility.js.map
