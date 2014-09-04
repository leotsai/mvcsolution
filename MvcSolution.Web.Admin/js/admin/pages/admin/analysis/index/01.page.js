(function() {
    var urls = {
        getData: "/admin/analysis/GetData"
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

    page.search = function() {
        var data = $("#formSearch").serialize();
        $("#formSearch .request-type:checked").each(function (i, elem) {
            data += "&types[" + i + "]=" + $(elem).val();
        });
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
        var columns = ['类型'];
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
            backgroundColor: '#f0f0f0',
            colors: obj.getColors()
        };

        this.chart.draw(data, options);
    };

    page.getChartTitle = function() {
        return $("#ddlPlatforms option:selected").text() + "-" + $("#ddlFrequncies option:selected").text() + "表";
    };

    page.fixChartData = function(groups) {
        var frequencyText = $("#ddlFrequncies option:selected").text();
        for (var gi = 0; gi < groups.length; gi++) {
            var group = groups[gi];
            group.Name = requestTypes[group.Name];
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

    page.getColors = function() {
        var allColors = ["blue", "orangered", "orange", "green", "purple", "darkcyan", "darkred", "greenyellow", "hotpink"];
        var colors = [];
        $("#formSearch .request-type").each(function (i, elem) {
            if ($(elem).is(":checked")) {
                colors.push(allColors[i]);
            }
        });
        return colors;
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

    page.renderRequestTypes = function() {
        var html = '';
        for (var type in window.requestTypes) {
            html += '<input type="checkbox" checked="checked" id="type' + type + '" value="' + type + '" class="request-type"/>\
            <label for="type' + type + '">' + requestTypes[type] + '</label>';
        }
        $("#ddlFrequncies").after(html);
    };

})();