
Date.prototype.addDays = function (number) {
    var date = new Date(this);
    date.setDate(date.getDate() + number);
    return date;
};

Date.prototype.addMonths = function (number) {
    var date = new Date(this);
    date.setMonth(date.getMonth() + number);
    return date;
};

Date.prototype.addYears = function (number) {
    var date = new Date(this);
    date.setFullYear(date.getFullYear() + number);
    return date;
};

Date.prototype.toWeekStart = function (startsFromSunday) {
    var dayOfWeek = this.getDay();
    if (startsFromSunday) {
        return this.addDays(-dayOfWeek);
    }
    if (dayOfWeek == 0) {
        dayOfWeek = 7;
    }
    return this.addDays(1 - dayOfWeek);
};

Date.prototype.toWeekEnd = function (startsFromSunday) {
    var dayOfWeek = this.getDay();
    if (startsFromSunday) {
        return this.addDays(6 - dayOfWeek);
    }
    if (dayOfWeek == 0) {
        dayOfWeek = 7;
    }
    return this.addDays(7 - dayOfWeek);
};

Date.prototype.toMonthStart = function () {
    var date = new Date(this);
    date.setDate(1);
    return date;
};

Date.prototype.toMonthEnd = function () {
    var date = new Date(this);
    date.setMonth(date.getMonth() + 1);
    date.setDate(0);
    return date;
};

Date.prototype.toYearStart = function () {
    var date = new Date(this);
    date.setMonth(0);
    date.setDate(1);
    return date;
};

Date.prototype.toYearEnd = function () {
    var date = new Date(this);
    date.setMonth(12);
    date.setDate(0);
    return date;
};

Date.prototype.toUSDateString = function () {
    return (this.getMonth() + 1) + "/" + this.getDate() + "/" + this.getFullYear();
};


Date.parseFromJsonFormat = function(dateStr) {
    // "/Date(1383667450669)/"
    var str = dateStr.substring(6, dateStr.length - 2);
    return new Date(str * 1);
};