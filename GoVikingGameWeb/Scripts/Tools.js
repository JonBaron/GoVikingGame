//http://stackoverflow.com/questions/4503504/format-date-in-javascript
var msDateToJSDate = function (msDate) {
    var dtE = /^\/Date\((-?[0-9]+)\)\/$/.exec(msDate);
    if (dtE) {
        var dt = new Date(parseInt(dtE[1], 10));
        return dt;
    }
    return null;
}