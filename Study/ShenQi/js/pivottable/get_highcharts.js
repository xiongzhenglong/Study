function getChart(chartType, dataArray, metaData, options) {
    switch (chartType) {
        case "bar":
            return getBarChart(dataArray, metaData);
            break;
        case "column":
            return getColumnChart(dataArray, metaData);
            break;
        case "stackedcolumn":
            return getStackedColumnChart(dataArray, metaData, options);
            break;
        case "stackedcolumnPercent":
            return getStackedColumnPercentChart(dataArray, metaData);
            break;
        case "trend":
            return getTrendChart(dataArray, metaData);
            break;
        case "line":
            return getLineChart(dataArray, metaData);
            break;
        case "area":
            return getAreaChart(dataArray, metaData);
            break;
        case "pie":
            return getPieChart(dataArray, metaData);
            break;
        case "scatter":
            return getScatterPlot(dataArray, metaData);
            break;
        case "bubbles":
            return getBubbleChart(dataArray, metaData);
            break;
        case "time":
            return getTimeSeries(dataArray, metaData);
            break;
        case "box":
            return getBoxPlot(dataArray, metaData);
            break;
    }
}
function getBarChart(dataArray, metaData) {
    var height = 600 + (dataArray.length / 5) * 30;
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'bar',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: height
        },
        //        colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
        //            return {
        //                radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
        //                stops: [
        //                    [0, color], [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        //                ]
        //            };
        //        }),
        title: {
            text: chartTitle
        },
        xAxis: {
            title: {
                text: xAxisTitle
            },
            categories: [],
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            }
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: yAxisTitle
            },
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' + '<b>' + this.series.name + ':</b> ' + this.point.y;
            }
        },
        plotOptions: {
            bar: {
                dataLabels: {
                    enabled: true
                    //                    formatter: function () {
                    //                        return Highcharts.numberFormat(this.point.y, 2, '.');
                    //                    }
                }
            },
            series: {
                shadow: false
            }
        },
        series: []
    };
    var headers = new Array();
    headers = dataArray[0];
    var xAxisLableMaxLength = 0;
    for (var i = 1; i < headers.length; i++) {
        myOptions.xAxis.categories.push(headers[i]);
    }
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        //console.log("CategoryName: " + categoryName + " for data: " + dataArray[i]);
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
            //console.log("Category Data: " + currentArray[j]);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }
    //console.log("Bar Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
function getBoxPlot(dataArray, metaData) {
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'boxplot',
            height: 600
        },
        title: {
            text: chartTitle
        },
        xAxis: {
            categories: [],

            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            }
        },
        yAxis: {
            title: {
                text: yAxisTitle
            },
            allowDecimals: false,
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            },
            plotLines: [{
                //value: 932,
                color: 'red',
                width: 1,
                label: {
                    //text: 'Theoretical mean: 932',
                    align: 'center',
                    style: {
                        color: 'gray'
                    }
                }
            }]
        },
        series: []
    };
    var headers = new Array();
    headers = dataArray[0];
    for (var i = 1; i < headers.length; i++) {
        myOptions.xAxis.categories.push(headers[i]);
    }
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        //console.log("CategoryName: " + categoryName + " for data: " + dataArray[i]);
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
            //console.log("Category Data: " + currentArray[j]);
        }
        myOptions.series.push({ 'data': categoryData });
    }
    //console.log("Box Plot Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
function getColumnChart(dataArray, metaData) {
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'column',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: 600,
            alignTicks: false
        },
        //colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
        //    return {
        //        radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
        //        stops: [[0, color], [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        //        ]
        //    };
        //}),
        title: {
            text: chartTitle
        },
        xAxis: {
            title: {
                text: xAxisTitle
            },
            categories: [],
            labels: {
                useHTML: true,
                rotation: -45
                //                formatter: function () {
                //                    if (this.value.length > 7) {
                //                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                //                    }
                //                    else {
                //                        return this.value;
                //                    }
                //                }
            }
        },
        yAxis: [
        {
            allowDecimals: false,
            title: {
                text: yAxisTitle
            },
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            }
        },
        {
            labels: {
                formatter: function () {
                    return this.value + '%';
                }
            },
            allowDecimals: false,
            max: 100,
            min: 0,
            opposite: true,
            endOnTick: true,
            gridLineWidth: 0,
            title: {
                text: 'Cumulative Percentage'
            }
        }],
        tooltip: {
            formatter: function () {
                if (this.series.name == 'Accumulated') {
                    return '<b>Cumulative Percentage: </b>' + this.y + '%';
                }
                return '<b>' + this.x + '</b><br/>' + '<b>' + this.series.name + ':</b> ' + Highcharts.numberFormat(this.point.y, 0);
            }
        },
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true
                    //                    formatter: function () {
                    //                        return Highcharts.numberFormat(this.point.y, 0, '.');
                    //                    }
                }
            },
            series: {
                shadow: false
            }
        },
        exporting: {
            filename: 'pareto chart',
            width: 1600,
            sourceWidth: 1600
        },
        series: []
    };
    //var headers = new Array();

    var keyPariArray = [];
    for (var i = 1; i < dataArray[0].length; i++) {
        keyPariArray.push({ 'name': dataArray[0][i], 'value': dataArray[1][i] });
    }

    keyPariArray.sort(function (a, b) { return parseFloat(b.value) - parseFloat(a.value) });

    headers = dataArray[0];
    //console.log("GetCharts: dataArray.length: " + dataArray.length);

    var serialValuesArray = new Array();
    var xAxisLableMaxLength = 0;
    var valueTotalSum = 0;
    for (var i = 0; i < keyPariArray.length; i++) {
        if (keyPariArray[i].name.length > xAxisLableMaxLength) {
            xAxisLableMaxLength = keyPariArray[i].name.length;
        }
        myOptions.xAxis.categories.push(keyPariArray[i].name);
        serialValuesArray.push(keyPariArray[i].value);
        valueTotalSum = valueTotalSum + parseInt(keyPariArray[i].value);
    }
    //    for (var i = 0; i < keyPariArray.length; i++) {

    //        //        currentArray = dataArray[i];
    //        //        var categoryName = currentArray[0];
    //        //        var categoryData = new Array();
    //        //        //console.log("CategoryName: " + categoryName + " for data: " + dataArray[i]);
    //        for (var j = 0; j < currentArray.length; j++) {
    //            categoryData.push(currentArray[j]);
    //            //console.log("Category Data: " + currentArray[j]);
    //        }
    //        myOptions.series.push({ 'name': 'Finding', 'data': keyPariArray[i].value, 'yAxis': 0 });
    //    }
    myOptions.chart.height = myOptions.chart.height + xAxisLableMaxLength * 6;
    myOptions.series.push({ 'name': 'Finding', 'data': serialValuesArray, 'yAxis': 0 });

    var accumulatedValue = 0;
    var accumulatedPercentArray = [];
    for (var i = 0; i < serialValuesArray.length; i++) {
        accumulatedValue = accumulatedValue + parseFloat(serialValuesArray[i]);
        accumulatedPercentArray.push(parseFloat((accumulatedValue / valueTotalSum * 100).toFixed(2)));
    }
    myOptions.series.push({ 'name': 'Accumulated', 'data': accumulatedPercentArray, 'type': 'spline', 'yAxis': 1 });
    //console.log("Bar Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
function getStackedColumnChart(dataArray, metaData, options) {
    var height;
    if (options == null || options.height == null) {
        height = 600 + (dataArray.length / 5) * 20;
    }
    else {
        height = options.height;
    }

    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'column',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: height
        },
        //colors: ['#006633', '#6699CC', '#006666', '#336666', '#006699', '#333333', '#663333', '#009999', '#33CCCC', '#990033', '#0099CC', '#006699', '#009966'],
        title: {
            text: chartTitle
        },
        xAxis: {
            title: {
                text: '<b>' + xAxisTitle + '</b>'
            },
            categories: [],
            labels: {
                useHTML: true,
                rotation: -25
                //                formatter: function () {
                //                    if (this.value.length > 7) {
                //                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                //                    }
                //                    else {
                //                        return this.value;
                //                    }
                //                }
            }
        },
        yAxis: {
            title: {
                text: yAxisTitle
            },
            allowDecimals: false,
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            },
            stackLabels: {
                enabled: true,
                style: {
                    'font-size': '14px',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || '#555'
                }
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' + '<b>' + this.series.name + ':</b> ' + this.point.y + '<br/><b>Total:</b> ' + this.point.stackTotal;
            }
        },
        plotOptions: {
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: 'white'
                    //formatter: function () {
                    //    return Highcharts.numberFormat(this.point.y, 2, '.');
                    //}
                }
            },
            series: {
                shadow: false
            }
        },
        legend: {
        },
        exporting: {
            filename: 'stacked column chart',
            width: 1600,
            sourceWidth: 1600
        },
        series: []
    };
    var headers = new Array();
    headers = dataArray[0];
    var xAxisLableMaxLength = 0;
    for (var i = 1; i < headers.length; i++) {
        if (headers[i].length > xAxisLableMaxLength) {
            xAxisLableMaxLength = headers[i].length;
        }
        myOptions.xAxis.categories.push(headers[i]);
    }
    myOptions.chart.height = myOptions.chart.height + xAxisLableMaxLength * 6;

    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];


        var categoryData = new Array();
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }

    //console.log("Stacked Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
function getStackedColumnPercentChart(dataArray, metaData) {
    var height = 600 + (dataArray.length / 5) * 20;
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'column',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: height
        },
        //colors: ['#006633', '#6699CC', '#006666', '#336666', '#006699', '#333333', '#663333', '#009999', '#33CCCC', '#990033', '#0099CC', '#006699', '#009966'],
        title: {
            text: chartTitle
        },
        xAxis: {
            title: {
                text: '<b>' + xAxisTitle + '</b>'
            },
            categories: [],
            labels: {
                useHTML: true,
                rotation: -45
                //                formatter: function () {
                //                    if (this.value.length > 7) {
                //                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                //                    }
                //                    else {
                //                        return this.value;
                //                    }
                //                }
            }
        },
        yAxis: {
            title: {
                text: yAxisTitle
            },
            allowDecimals: false,
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            },
            stackLabels: {
                enabled: true,
                style: {
                    'font-size': '14px',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || '#555'
                }
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' + '<b>' + this.series.name + ':</b> ' + this.point.y + '(' + Highcharts.numberFormat(this.percentage, 0) + '%)<br/><b>Total:</b> ' + this.point.stackTotal;
            }
        },
        plotOptions: {
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                    formatter: function () {
                        return this.y + " (" + Highcharts.numberFormat(this.percentage, 0) + "%)";
                    }
                }
            },
            series: {
                shadow: false
            }
        },
        exporting: {
            filename: 'stacked column chart',
            width: 1600,
            sourceWidth: 1600
        },
        series: []
    };
    var headers = new Array();
    headers = dataArray[0];
    var xAxisLableMaxLength = 0;
    for (var i = 1; i < headers.length; i++) {
        if (headers[i].length > xAxisLableMaxLength) {
            xAxisLableMaxLength = headers[i].length;
        }
        myOptions.xAxis.categories.push(headers[i]);
    }
    myOptions.chart.height = myOptions.chart.height + xAxisLableMaxLength * 6;

    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];


        var categoryData = new Array();
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }

    //console.log("Stacked Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
function getTrendChart(dataArray, metaData) {
    var height = 600 + (dataArray.length / 5) * 20;
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    //var yAxisTitle = metaData[2];
    var yAxisTitle = 'Percentage';
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'column',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: height
        },
        colors: ['#7CB5EC', '#F7A35C', '#8085E9', '#CC3333', '#CC6600', '#CC9900', '#CC9966'],
        title: {
            text: chartTitle
        },
        xAxis: {
            title: {
                text: '<b>' + xAxisTitle + '</b>'
            },
            categories: [],
            labels: {
                useHTML: true,
                rotation: -45
            }
        },
        yAxis: {
            title: {
                text: yAxisTitle
            },
            allowDecimals: false,
            labels: {
                useHTML: true,
                formatter: function () {
                    return this.value + '%';
                }
            },
            min: 0,
            max: 100
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' + '<b>' + this.series.name + ':</b> ' + this.point.y + '%';
            }
        },
        plotOptions: {
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: false,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                    formatter: function () {
                        return this.point.y + '%'
                    }
                }
            },
            series: {
                shadow: false
            }
        },
        exporting: {
            filename: 'trend chart',
            width: 1600,
            sourceWidth: 1600
        },
        series: []
    };
    var headers = new Array();
    headers = dataArray[0];
    var xAxisLableMaxLength = 0;
    for (var i = 1; i < headers.length; i++) {
        if (headers[i].length > xAxisLableMaxLength) {
            xAxisLableMaxLength = headers[i].length;
        }
        myOptions.xAxis.categories.push(headers[i]);
    }
    myOptions.chart.height = myOptions.chart.height + xAxisLableMaxLength * 6;

    //计算独立的总数
    var separateCountArray = [];
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        for (var j = 1; j < currentArray.length; j++) {
            if (separateCountArray.length < j) {
                if (currentArray[j] == null) {
                    separateCountArray.push(0);
                }
                else {
                    separateCountArray.push(currentArray[j]);
                }
            }
            else {
                if (currentArray[j] == null) {
                }
                else {
                    separateCountArray[j - 1] += currentArray[j];

                }
            }
        }
    }

    //stack colum chart
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        for (var j = 1; j < currentArray.length; j++) {
            if (currentArray[j] == null) {
                categoryData.push(0);
            }
            else {
                categoryData.push(Math.round(currentArray[j] * 100 / separateCountArray[j - 1] * 10) / 10);
            }
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }

    //转化成累加
    //trend line chart
    var countCalc = 0, accumulatedCountArray = [];
    for (var i = 0; i < separateCountArray.length; i++) {
        countCalc += separateCountArray[i];
        accumulatedCountArray.push(countCalc);
    }

    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        var separatedAccumulatedCount = 0;
        for (var j = 1; j < currentArray.length; j++) {

            if (currentArray[j] != null) {
                separatedAccumulatedCount += currentArray[j];
            }
            var percentData = (separatedAccumulatedCount * 100 / accumulatedCountArray[j - 1]);
            categoryData.push(Math.round(percentData * 10) / 10);
        }
        myOptions.series.push({
            'name': categoryName,
            'data': categoryData,
            'type': 'spline',
            'dataLabels': {
                enabled: true,
                color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || '#000',
                formatter: function () {
                    return this.point.y + '%'
                }
            }
        });
    }

    //console.log("Stacked Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
function getLineChart(dataArray, metaData) {
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: 600
        },
        title: {
            text: chartTitle
        },
        //        colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
        //            return {
        //                radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
        //                stops: [[0, color], [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        //                ]
        //            };
        //        }),
        xAxis: {
            title: {
                text: xAxisTitle
            },
            categories: [],
            labels: {
                useHTML: true,
                rotation: -45
            }
        },
        yAxis: {
            title: {
                text: yAxisTitle
            },
            allowDecimals: false,
            labels: {
                useHTML: true,
                formatter: function () {
                    return this.value + '%';
                }
            },
            max: 100,
            min: 0
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' + '<b>' + this.series.name + ':</b> ' + this.point.y;
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true,
                    formatter: function () {
                        //return Highcharts.numberFormat(this.point.y, 2, '.');
                        return this.point.y + '%';
                    }
                }
            },
            series: {
                shadow: false
            }
        },
        exporting: {
            filename: 'line chart',
            width: 1600,
            sourceWidth: 1600
        },
        series: []
    };
    var headers = new Array();
    var xAxisLableMaxLength = 0;
    headers = dataArray[0];
    //console.log("GetCharts: dataArray.length: " + dataArray.length);
    // Set the height
    /*if (headers.length < 5 ) {
    myOptions.chart.height = 360;
    } else if (headers.length > 5) {
    myOptions.chart.height = 60 * headers.length;
    }
    */
    for (var i = 1; i < headers.length; i++) {
        if (headers[i].length > xAxisLableMaxLength) {
            xAxisLableMaxLength = headers[i].length;
        }
        myOptions.xAxis.categories.push(headers[i]);
    }
    myOptions.chart.height = myOptions.chart.height + xAxisLableMaxLength * 6;

    //计算独立的总数
    var separateCountArray = [];
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        for (var j = 1; j < currentArray.length; j++) {
            if (separateCountArray.length < j) {
                if (currentArray[j] == null) {
                    separateCountArray.push(0);
                }
                else {
                    separateCountArray.push(currentArray[j]);
                }
            }
            else {
                if (currentArray[j] == null) {
                }
                else {
                    separateCountArray[j - 1] += currentArray[j];

                }
            }
        }
    }
    //转化成累加
    var countCalc = 0, accumulatedCountArray = [];
    for (var i = 0; i < separateCountArray.length; i++) {
        countCalc += separateCountArray[i];
        accumulatedCountArray.push(countCalc);
    }

    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        var separatedAccumulatedCount = 0;
        for (var j = 1; j < currentArray.length; j++) {

            if (currentArray[j] != null) {
                separatedAccumulatedCount += currentArray[j];
            }
            var percentData = (separatedAccumulatedCount * 100 / accumulatedCountArray[j - 1]);
            categoryData.push(Math.round(percentData * 10) / 10);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }

    //column chart
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];


        var categoryData = new Array();
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }

    //console.log("Bar Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
function getAreaChart(dataArray, metaData) {
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'area',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: 600
        },
        //        colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
        //            return {
        //                radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
        //                stops: [
        //[0, color],
        //[1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        //]
        //            };
        //        }),
        title: {
            text: chartTitle
        },
        xAxis: {
            title: {
                text: xAxisTitle
            },
            categories: [],
            labels: {
                useHTML: true,
                rotation: -45
            }
        },
        yAxis: {
            title: {
                text: yAxisTitle
            },
            allowDecimals: false,
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' + '<b>' + this.series.name + ':</b> ' + Highcharts.numberFormat(this.point.y, 2, '.');
            }
        },
        plotOptions: {
            area: {
                pointStart: 0,
                marker: {
                    enabled: false,
                    symbol: 'circle',
                    radius: 2,
                    states: {
                        hover: {
                            enabled: true
                        }
                    }
                }
            }
        },
        series: []
    };
    var headers = new Array();
    headers = dataArray[0];
    var xAxisLableMaxLength = 0;
    //console.log("GetCharts: dataArray.length: " + dataArray.length);
    // Set the height
    /*if (headers.length < 5 ) {
    myOptions.chart.height = 360;
    } else if (headers.length > 5) {
    myOptions.chart.height = 60 * headers.length;
    }
    */
    for (var i = 1; i < headers.length; i++) {
        if (headers[i].length > xAxisLableMaxLength) {
            xAxisLableMaxLength = headers[i].length;
        }
        myOptions.xAxis.categories.push(headers[i]);
    }
    myOptions.chart.height = myOptions.chart.height + xAxisLableMaxLength * 6;

    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }
    return myOptions;
}
function getPieChart(dataArray, metaData) {
    //    Highcharts.getOptions().plotOptions.pie.colors = (function () {
    //        var colors = [],
    //            base = Highcharts.getOptions().colors[0],
    //            i;

    //        for (i = 0; i < 10; i += 1) {
    //            // Start out with a darkened base color (negative brighten), and end
    //            // up with a much brighter color
    //            colors.push(Highcharts.Color(base).brighten((i - 3) / 7).get());
    //        }
    //        return colors;
    //    } ());

    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    // Radialize the colors
    /*Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function(color) {
    return {
    radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
    stops: [
    [0, color],
    [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
    ]
    };
    });*/
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'pie',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: 650
        },
        //        colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
        //            return {
        //                radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
        //                stops: [
        //        [0, color],
        //        [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        //        ]
        //            };
        //        }),
        title: {
            text: chartTitle
        },
        tooltip: {
            pointFormat: 'Count: {point.y}<br/>{series.name}: <b>{point.percentage:.1f} %</b>'
        },
        legend: {
            enabled: false
        },
        exporting: {
            filename: 'pie chart',
            width: 1600,
            sourceWidth: 1600
        },
        series: [],
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    //color: '#000000',
                    //connectorColor: '#000000',
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                    //formatter: function () {
                    //    if (this.point.name.length > 10) {
                    //        return this.point.name.substr(0, 10) + "...: " + '<span style="color:#CC0000;">' + this.point.y + '</span><span>(' + this.point.percentage + '%)</span>';
                    //    } else {
                    //        return this.point.name + ": " + '<span style="color:#CC0000;">' + this.point.y + '</span><span>(' + this.point.percentage + '%)</span>';
                    //    }
                    // }
                },
                showInLegend: true
            }
        }
    };
    var headers = new Array();
    headers = dataArray[0];
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        for (var j = 1; j < currentArray.length; j++) {
            var pieDataArray = new Array();
            pieDataArray.push(headers[j], currentArray[j]);
            categoryData.push(pieDataArray);
        }

        categoryData.sort(function (a, b) {
            return parseFloat(b[1]) - parseFloat(a[1])
        });

        myOptions.series.push({ 'data': categoryData, name: 'Finding percentage' });
    }
    return myOptions;
}
function getScatterPlot(dataArray, metaData) {
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'scatter',
            zoomType: 'xy',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false
        },
        //        colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
        //            return {
        //                radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
        //                stops: [
        //[0, color],
        //[1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        //]
        //            };
        //        }),
        title: {
            text: chartTitle
        },
        xAxis: {
            categories: [],
            labels: {
                useHTML: true,
                rotation: -45
            },
            startOnTick: true,
            endOnTick: true,
            showLastLabel: true
        },
        yAxis: {
            title: {
                text: yAxisTitle
            },
            allowDecimals: false,
            labels: {
                useHTML: true,
                formatter: function () {
                    if (this.value.length > 7) {
                        return ('<span class="labelTips" title="' + this.value + '">' + this.value.substring(0, 7) + "...</span>");
                    }
                    else {
                        return this.value;
                    }
                }
            }
        },
        plotOptions: {
            scatter: {
                marker: {
                    radius: 5,
                    states: {
                        hover: {
                            enabled: true,
                            lineColor: 'rgb(100,100,100)'
                        }
                    }
                },
                states: {
                    hover: {
                        marker: {
                            enabled: false
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<b>{series.name}</b><br/>',
                    pointFormat: '{point.y:.2f}'
                }
            },
            series: {
                shadow: false
            }
        },
        series: []
    };
    var headers = new Array();
    var xAxisLableMaxLength = 0;
    headers = dataArray[0];
    for (var i = 1; i < headers.length; i++) {
        if (headers[i].length > xAxisLableMaxLength) {
            xAxisLableMaxLength = headers[i].length;
        }
        myOptions.xAxis.categories.push(headers[i]);
    }
    myOptions.chart.height = myOptions.chart.height + xAxisLableMaxLength * 6;

    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }
    return myOptions;
}
function getBubbleChart(dataArray, metaData) {
    var chartTitle = metaData[0];
    var xAxisTitle = metaData[1];
    var yAxisTitle = metaData[2];
    var myOptions = {
        chart: {
            renderTo: 'containerChart',
            type: 'bubble',
            zoomType: 'xy',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            height: 600
        },
        colors: Highcharts.map(Highcharts.getOptions().colors, function (color) {
            return {
                radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
                stops: [
                    [0, color], [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
                    ]
            };
        }),
        title: {
            text: chartTitle
        },
        plotOptions: {
            bubble: {
                tooltip: {
                    pointFormat: '{point.y:.2f}'
                }
            }
        },
        series: []
    };
    var headers = new Array();
    headers = dataArray[0];
    for (var i = 1; i < dataArray.length; i++) {
        var currentArray = new Array();
        currentArray = dataArray[i];
        var categoryName = currentArray[0];
        var categoryData = new Array();
        //console.log("CategoryName: " + categoryName + " for data: " + dataArray[i]);
        for (var j = 1; j < currentArray.length; j++) {
            categoryData.push(currentArray[j]);
            //console.log("Category Data: " + currentArray[j]);
        }
        myOptions.series.push({ 'name': categoryName, 'data': categoryData });
    }
    //console.log("Bubble Chart Options: " + JSON.stringify(myOptions));
    return myOptions;
}
/*function getTimeSeries(dataArray, metaData) {
var chartTitle = metaData[0];
var xAxisTitle = metaData[1];
var yAxisTitle = metaData[2];
var myOptions = {
chart: {
renderTo: 'containerChart',
zoomType: 'x'
},
title: {
text: chartTitle
},
xAxis: {
categories: [],
labels:{
formatter: function(){
if (this.value.length > 10){
return this.value.substr(0,5) + "...";
}else{
return this.value;
}
}
}
},
yAxis: {
title: {
text: yAxisTitle
},
},
tooltip: {
shared: true
},
plotOptions: {
area: {
fillColor: {
linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1},
stops: [
[0, Highcharts.getOptions().colors[0]],
[1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
]
},
lineWidth: 1,
marker: {
enabled: false
},
shadow: false,
states: {
hover: {
lineWidth: 1
}
},
threshold: null
}
},
series:[{
type: 'area',
pointInterval: 24 * 3600 * 1000
}]
};
var headers = new Array();
headers = dataArray[0];
for(var i = 1; i < headers.length; i++) {
myOptions.xAxis.categories.push(headers[i]);
}
for(var i = 1; i < dataArray.length; i++) {
var currentArray = new Array();
currentArray = dataArray[i];
var categoryName = currentArray[0];
var categoryData = new Array();
//console.log("CategoryName: " + categoryName + " for data: " + dataArray[i]);
for(var j = 1; j < currentArray.length; j++) {
categoryData.push(currentArray[j]);
//console.log("Category Data: " + currentArray[j]);
}
myOptions.series.push({'data': categoryData});
}
//console.log("Bar Chart Options: " + JSON.stringify(myOptions));
return myOptions;
}*/