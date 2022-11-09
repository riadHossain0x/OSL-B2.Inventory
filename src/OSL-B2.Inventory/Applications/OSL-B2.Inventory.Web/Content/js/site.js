function format() {
    var args = arguments;
    if (args.length <= 1) {
        return args;
    }
    var result = args[0];
    for (var i = 1; i < args.length; i++) {
        result = result.replace(new RegExp("\\{" + (i - 1) + "\\}", "g"), args[i]);
    }
    return result;
}