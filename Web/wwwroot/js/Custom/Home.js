var colors = {
    primary: "#6571ff",
    secondary: "#7987a1",
    success: "#05a34a",
    info: "#66d1d1",
    warning: "#fbbc06",
    danger: "#ff3366",
    light: "#e9ecef",
    dark: "#060c17",
    muted: "#7987a1",
    gridBorder: "rgba(77, 138, 240, .15)",
    bodyColor: "#b8c3d9",
    cardBg: "#0c1427"
}

document.addEventListener("DOMContentLoaded", function () {
    loadHomeData();
    loadCountChartData();
});

function loadHomeData() {
    $.ajax({
        url: '/Home/GetCountAsync',
        type: 'GET', 
        dataType: 'json',
        success: function (response) {
            if (response.Status === "Success" && response.Data) {
                updateCounts(response.Data); 
            } else {
                InfoToast(response.Errors.join("\n"));
            }
        },
        error: function (response) {
            console.log(response);
            handleAjaxError(response);
            unblockwindow();
        },
    });
}

function updateCounts(data) {
    $('#celebrityCount').text(data.CelebrityCount); 
    $('#customerCount').text(data.CustomerCount);
    $('#pendingAds').text(data.PendingAds);
    $('#completedAds').text(data.CompletedAds);
    $('#cancelledAds').text(data.CancelledAds);
    $('#totalAds').text(data.TotalAds);
    $('#incomingTransactions').text(data.IncomingTransactionsAmount+" KWD");
    $('#outgoingTransactions').text(data.OutgoingTransactionsAmount+" KWD");
    $('#adminBalance').text(data.AdminBalance+" KWD");
}

function loadCountChartData() {
    $.ajax({
        url: '/Home/GetCountStatisticsAsync',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.Status === "Success" && response.Data) {
                renderCustomerChart(response.Data);
                renderCelebrityChart(response.Data);
                renderAdChart(response.Data);
            } else {
                InfoToast(response.Errors.join("\n"));
            }
        },
        error: function (response) {
            console.error(response);
            handleAjaxError(response);
        }
    });
}
const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
function renderCustomerChart(data) {
    const chartData = {
        categories: data.map(item => `${monthNames[item.Month - 1]} ${item.Year}`),
        values: data.map(item => item.CustomerCount)
    };
    renderChart("#customersCountChart", chartData, "Customer Count", "Customer Insights (Value)");
}

function renderCelebrityChart(data) {
    const chartData = {
        categories: data.map(item => `${monthNames[item.Month - 1]} ${item.Year}`),
        values: data.map(item => item.CelebrityCount)
    };
    renderChart("#celebritiesCountChart", chartData, "Celebrity Count", "Celebrity Insights (Value)");
}
function renderAdChart(data) {
    const chartData = {
        categories: data.map(item => `${monthNames[item.Month - 1]} ${item.Year}`),
        values: data.map(item => item.AdCount)
    };
    renderChart("#adInsightsChart", chartData, "Ad Insights","Ad Insights (Value)");
}

function renderChart(elementSelector, chartData, title, yAxisTitle) {
    const options = {
        chart: {
            type: "line",
            height: '400',
            parentHeightOffset: 0,
            foreColor: colors.bodyColor,
            background: colors.cardBg,
            toolbar: {
                show: false
            },
        },
        theme: {
            mode: 'light'
        },
        tooltip: {
            theme: 'light'
        },
        colors: [colors.primary, colors.danger, colors.warning], 
        grid: {
            padding: {
                bottom: -4,
            },
            borderColor: colors.gridBorder,
            xaxis: {
                lines: {
                    show: true
                }
            }
        },
        series: [
            {
                name: title, 
                data: chartData.values 
            },
        ],
        xaxis: {
            categories: chartData.categories,
            lines: {
                show: true
            },
            axisBorder: {
                color: colors.gridBorder,
            },
            axisTicks: {
                color: colors.gridBorder,
            },
            crosshairs: {
                stroke: {
                    color: colors.secondary,
                },
            },
        },
        yaxis: {
            title: {
                text: yAxisTitle, 
                style: {
                    size: 9,
                    color: colors.muted
                }
            },
            tickAmount: 4,
            tooltip: {
                enabled: true
            },
            crosshairs: {
                stroke: {
                    color: colors.secondary,
                },
            },
        },
        markers: {
            size: 0,
        },
        stroke: {
            width: 2,
            curve: "smooth", 
        },
    };


    const chartElement = document.querySelector(elementSelector);
    chartElement.innerHTML = "";
    const chart = new ApexCharts(chartElement, options);
    chart.render();
}

