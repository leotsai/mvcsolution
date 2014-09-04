Array.prototype.each = function (func) {
    for (var i = 0; i < this.length; i++) {
        var item = this[i];
        var result = func.call(item, i, item);
        if (result == false) {
            return;
        }
    }
};

Array.prototype.sum = function(propertyOrFunc) {
    var total = 0;
    var isFunc = typeof(propertyOrFunc) == "function";
    this.each(function() {
        if (isFunc) {
            total += propertyOrFunc.call(this);
        } else {
            var value = this[propertyOrFunc];
            if (value != undefined) {
                total += value * 1;
            }
        }
    });
    return total;
};

Array.prototype.average = function (propertyOrFunc) {
    if (this.length == 0) {
        return 0;
    }
    return this.sum(propertyOrFunc) / this.length;
};

Array.prototype.max = function (propertyOrFunc) {
    var max = 0;
    var value = 0;
    var isFunc = typeof (propertyOrFunc) == "function";
    this.each(function () {
        if (isFunc) {
            value = propertyOrFunc.call(this);
        } else {
            value = this[propertyOrFunc];
            if (value != undefined) {
                value = value * 1;
            }
        }
        if (value > max) {
            max = value;
        }
    });
    return max;
};

Array.prototype.min = function (propertyOrFunc) {
    if (this.length == 0) {
        return 0;
    }
    var value = 0;
    var isFunc = typeof (propertyOrFunc) == "function";
    var min = getValue(this[0]);
    this.each(function () {
        value = getValue(this);
        if (value < min) {
            min = value;
        }
    });

    function getValue(item) {
        var val = 0;
        if (isFunc) {
            val = propertyOrFunc.call(item);
        } else {
            val = item[propertyOrFunc];
            if (val != undefined) {
                val = val * 1;
            }
        }
        return val;
    }

    return min;
};

Array.prototype.where = function (predicateFunction) {
    var results = new Array();
    this.each(function() {
        if (predicateFunction.call(this)) {
            results.push(this);
        }
    });
    return results;
};

Array.prototype.orderBy = function (property, compare) {
    var items = this;
    for (var i = 0; i < items.length - 1; i++) {
        for (var j = 0; j < items.length - 1 - i; j++) {
            if (isFirstGreaterThanSecond(items[j], items[j + 1])) {
                var temp = items[j + 1];
                items[j + 1] = items[j];
                items[j] = temp;
            }
        }
    }
    function isFirstGreaterThanSecond(first, second) {
        if (compare != undefined) {
            return compare(first, second);
        }
        else if (property == undefined || property == null) {
            return first > second;
        }
        else {
            return first[property] > second[property];
        }
    }

    return items;
};

Array.prototype.orderByDescending = function (property, compare) {
    var items = this;
    for (var i = 0; i < items.length - 1; i++) {
        for (var j = 0; j < items.length - 1 - i; j++) {
            if (!isFirstGreaterThanSecond(items[j], items[j + 1])) {
                var temp = items[j + 1];
                items[j + 1] = items[j];
                items[j] = temp;
            }
        }
    }
    function isFirstGreaterThanSecond(first, second) {
        if (compare != undefined) {
            return compare(first, second);
        }
        else if (property == undefined || property == null) {
            return first > second;
        }
        else {
            return first[property] > second[property];
        }
    }
    return items;
};

Array.prototype.groupBy = function (predicate) {
    var results = [];
    var items = this;

    var keys = {}, index = 0;
    for (var i = 0; i < items.length; i++) {
        var selector;
        if (typeof predicate === "string") {
            selector = items[i][predicate];
        } else {
            selector = predicate(items[i]);
        }
        if (keys[selector] === undefined) {
            keys[selector] = index++;
            results.push({ key: selector, value: [items[i]] });
        } else {
            results[keys[selector]].value.push(items[i]);
        }
    }
    return results;
};

Array.prototype.skip = function (count) {
    var items = new Array();
    for (var i = count; i < this.length; i++) {
        items.push(this[i]);
    }
    return items;
};

Array.prototype.take = function (count) {
    var items = new Array();
    for (var i = 0; i < this.length && i < count; i++) {
        items.push(this[i]);
    }
    return items;
};

Array.prototype.firstOrDefault = function (predicateFunction) {
    if (this.length == 0) {
        return null;
    }
    if (predicateFunction == undefined) {
        return this[0];
    }
    var item = null;
    this.each(function () {
        if (predicateFunction.call(this)) {
            item = this;
            return false;
        }
        return true;
    });
    return item;
};

Array.prototype.any = function(predicateFunction) {
    if (predicateFunction == undefined) {
        return this.length > 0;
    }
    var hasAny = false;
    this.each(function() {
        if (predicateFunction.call(this)) {
            hasAny = true;
            return false;
        }
        return true;
    });
    return hasAny;
};

Array.prototype.select = function (predicateFunction) {
    if (predicateFunction == undefined) {
        throw "parameter predicateFunction cannot be null or undefined";
    }
    var items = [];
    this.each(function () {
        items.push(predicateFunction.call(this));
    });
    return items;
};
