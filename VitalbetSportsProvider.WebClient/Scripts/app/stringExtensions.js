String.prototype.format = function () {
    const xx = this + "";
    const rex = /\{(\d+):?(.*?)\}/igm;

    var callback = function (expr, index, format) {
        index = parseInt(index);
        if (isNaN(index)) {
            return expr;
        }

        var val = callback.context;
        if ((!val) || (val.length <= index)) {
            return expr;
        }

        val = val[index];

        if (format && val != null && val != undefined && val.toStringEx) {
            return val.toStringEx(format);
        }

        return val != undefined || val != null ? val.toString() : "";
    };

    const args = new Array(arguments.length);
    for (var i = 0, len = args.length; i < len; i++) {
        args[i] = arguments[i];
    }

    callback.context = args;
    return xx.replace(rex, callback);
};