
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
window.requestTypes = {
    "1": "分享",
    "2": "下载页-官网",
    "3": "下载",
    "4": "注册",
    "5": "看广告",
    "6": "下载页-微信",
};

(function() {
    var urls = {
        getData: "/admin/analysis/GetPlatformsData"
    };
    page.onLoaded = function () {
        var obj = this;
        $("#btnReport").click(function () {
            obj.search();
        });
        var timeframe = new TimeframeSelector();
        timeframe.init();
        obj.renderRequestTypes();
        obj.initGoogleChart();
        obj.search();
    };

    page.renderRequestTypes = function () {
        var html = '';
        for (var type in window.requestTypes) {
            html += '<option value="' + type + '">' + requestTypes[type] + '</option>';
        }
        $("#ddlRequestTypes").html(html);
    };

    page.search = function() {
        var data = $("#formSearch").serialize();
        var obj = this;
        admin.ajax.busyPost(urls.getData, data, function (result) {
            if (result.Success) {
                obj.drawChart(result.Value);
            } else {
                admin.notification.alertError(result.Message);
            }
        }, "加载中...");
    };

    page.initGoogleChart = function() {
        this.chart = new google.visualization.LineChart(document.getElementById('report'));
    };

    page.drawChart = function(groups) {
        if (groups.length == 0 || groups[0].Items.length == 0) {
            admin.notification.alertError("找不到数据");
            return;
        }
        this.fixChartData(groups);
        page.renderSummary(groups);
        var firstGroup = groups[0];
        var obj = this;
        var table = [];
        var columns = ['平台'];
        for (var i = 0; i < groups.length; i++) {
            columns.push(groups[i].Name);
        }
        table.push(columns);

        
        var totalRows = firstGroup.Items.length;
        for (var ri = 0; ri < totalRows; ri++) {
            var row = [firstGroup.Items[ri].Text];
            for (var gi = 0; gi < groups.length; gi++) {
                row.push(groups[gi].Items[ri].Value * 1);
            }
            table.push(row);
        }
        var data = google.visualization.arrayToDataTable(table);
        var options = {
            title: obj.getChartTitle(),
            legend: { position: 'bottom' },
            animation: {
                duration: 1000,
                easing: 'out'
            },
            backgroundColor: '#f0f0f0'
        };

        this.chart.draw(data, options);
    };

    page.getChartTitle = function() {
        return $("#ddlRequestTypes option:selected").text() + "-" + $("#ddlFrequncies option:selected").text() + "表";
    };

    page.fixChartData = function(groups) {
        var frequencyText = $("#ddlFrequncies option:selected").text();
        for (var gi = 0; gi < groups.length; gi++) {
            var group = groups[gi];
            for (var i = 0; i < group.Items.length; i++) {
                var item = group.Items[i];
                var date = new Date(item.Text);
                switch (frequencyText) {
                case "小时":
                    item.Text = date.getHours() + ":" + getTwoDigits(date.getMinutes());
                    break;
                case "日":
                case "周":
                    item.Text = date.getDate() + "日";
                    break;
                default:
                    item.Text = (date.getMonth() + 1) + "月";
                }
            }
        }

        function getTwoDigits(number) {
            return number < 10 ? "0" + number : "" + number;
        }
    };

    page.renderSummary = function(groups) {
        var $table = $("#summary").empty();
        for (var gi = 0; gi < groups.length; gi++) {
            var group = groups[gi];
            var items = group.Items;
            var html = '<tr>\
                        <td>' + group.Name + '</td>\
                        <td>' + items.sum("Value").toFixed(2) * 1 + '</td>\
                        <td>' + items.average("Value").toFixed(2) * 1 + '</td>\
                        <td>' + items.min("Value").toFixed(2) * 1 + '</td>\
                        <td>' + items.max("Value").toFixed(2) * 1 + '</td>\
                    </tr>';
            $table.append(html);
        }
    };

})();