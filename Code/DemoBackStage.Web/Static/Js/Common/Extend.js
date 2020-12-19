(function () {

    Array.prototype.dCopy = function () {
        var str = JSON.stringify(this);

        return JSON.parse(str);
    }

})();