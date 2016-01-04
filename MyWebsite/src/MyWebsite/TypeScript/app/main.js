define(["require", "exports"], function (require, exports) {
    var Main = (function () {
        function Main() {
            this.runString = "hello from main MAC";
        }
        Main.prototype.run = function () {
            $.get("api/Projects", {}, function (data) {
                console.log(data);
            });
        };
        return Main;
    })();
    return Main;
});
//# sourceMappingURL=main.js.map