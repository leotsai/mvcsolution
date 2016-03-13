if (!window.Number) {
    window.Number = function() {

    };
}

Number.get2Digits = function(number) {
    return number < 10 ? "0" + number : number;
};