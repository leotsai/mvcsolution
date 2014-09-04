
var TimeframeSelector = function () {

};

TimeframeSelector.prototype.parseData = function () {
    var data = "";
    data += "From=" + $(".date-from").val();
    data += "&To=" + $(".date-to").val();
    return data;
};

TimeframeSelector.prototype.init = function () {
    var selector = this;
    $(".date-options").change(function () {
        selector.bindValues();
    });
    selector.bindValues();
};

TimeframeSelector.prototype.bindValues = function () {
    var selectedDate = $(".date-options").val();
    $(".date-selector .date-from, .date-selector .date-to").attr("disabled", "disabled").hide();
    $("#timeframeFromTo").hide();
    switch (selectedDate) {
        case "Today":
            bindTextboxes(new Date(), new Date());
            break;
        case "Yesterday":
            bindTextboxes(new Date().addDays(-1), new Date().addDays(-1));
            break;
        case "ThisWeek":
            bindTextboxes(new Date().toWeekStart(false), new Date().toWeekEnd(false));
            break;
        case "LastWeek":
            bindTextboxes(new Date().toWeekStart(false).addDays(-7), new Date().toWeekEnd(false).addDays(-7));
            break;
        case "Last7Days":
            bindTextboxes(new Date().addDays(-7), new Date());
            break;
        case "ThisMonth":
            bindTextboxes(new Date().toMonthStart(), new Date().toMonthEnd());
            break;
        case "LastMonth":
            bindTextboxes(new Date().addMonths(-1).toMonthStart(), new Date().addMonths(-1).toMonthEnd());
            break;
        case "Last30Days":
            bindTextboxes(new Date().addDays(-30), new Date());
            break;
        case "Custom":
            $(".date-selector .date-from, .date-selector .date-to").removeAttr("disabled").show();
            $("#timeframeFromTo").show();
            break;
        default:
            $(".date-from").val("");
            $(".date-to").val("");
            break;
    }

    function bindTextboxes(from, to) {
        $(".date-from").val(from.toShortDateString());
        $(".date-to").val(to.toShortDateString());
    }
};