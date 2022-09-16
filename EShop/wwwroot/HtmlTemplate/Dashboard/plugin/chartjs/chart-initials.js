if (document.getElementById('chartSellMonth') !== null) {
    var chartSellMonth = document.getElementById('chartSellMonth').getContext('2d');
}
if (document.getElementById('chartSellYear') !== null) {
    var chartSellYear = document.getElementById('chartSellYear').getContext('2d');
}
if (document.getElementById('chartSeenUsers') !== null) {
    var chartSeenUsers = document.getElementById('chartSeenUsers').getContext('2d');
}
if (document.getElementById('chartSeenUsersHaveDate') !== null) {
    var chartSeenUsersHaveDate = document.getElementById('chartSeenUsersHaveDate').getContext('2d');
}
if (document.getElementById('chartSellMonthHaveDate') !== null) {
    var chartSellMonthHaveDate = document.getElementById('chartSellMonthHaveDate').getContext('2d');
}
if (document.getElementById('chartSellUsers') !== null) {
    var chartSellUsers = document.getElementById('chartSellUsers').getContext('2d');
}

Chart.defaults.font.family = 'IRANYEKAN';
Chart.defaults.font.size = 12;
const DISPLAY = true;
const BORDER = true;
const CHART_AREA = true;
const TICKS = false;

var myChartSellMonth = new Chart(chartSellMonth, {
    type: 'bar',
    data: {
        labels: ['1400/11/1', '1400/11/2', '1400/11/3', '1400/11/4', '1400/11/5', '1400/11/6', '1400/11/7', '1400/11/8', '1400/11/9', '1400/11/10', '1400/11/11', '1400/11/12', '1400/11/13', '1400/11/14', '1400/11/15', '1400/11/16', '1400/11/17', '1400/11/18', '1400/11/19', '1400/11/20', '1400/11/21', '1400/11/22', '1400/11/23', '1400/11/24', '1400/11/25', '1400/11/26', '1400/11/27', '1400/11/28', '1400/11/29', '1400/11/30', '1400/11/31'],
        datasets: [{
            label: 'فروش کتاب چاپی',
            data: [9, 10, 6, 5.5, 7, 6, 2, 6, 4, 8, 8, 10, 9, 8, 10, 6, 5, 8, 8.5, 6, 4, 6, 5, 7, 8, 10, 8, 8, 9, 4, 3],
            backgroundColor: [
                '#2690ba'
            ],
            borderWidth: 0,
            borderRadius: 3
        }]
    },
    options: {
        plugins: {
            legend: {
                display: false,
            },
        },
        responsive: true,
        scales: {
            y: {
                beginAtZero: true,
                stacked: true,
                // title: false
                ticks: {
                    display: false,
                },
                grid: {
                    display: DISPLAY,
                    drawBorder: BORDER,
                    drawOnChartArea: CHART_AREA,
                    drawTicks: TICKS,
                    color: '#f0f3f5',
                }
            },
            x: {
                stacked: true,
                ticks: {
                    display: false,
                },
                grid: {
                    display: DISPLAY,
                    drawBorder: BORDER,
                    drawOnChartArea: CHART_AREA,
                    drawTicks: TICKS,
                    color: '#f0f3f5',
                }
            }
        }
    }
});

var myChartSeenUsers = new Chart(chartSeenUsers, {
    type: 'line',
    data: {
        labels: ['1400/11/1', '1400/11/2', '1400/11/3', '1400/11/4', '1400/11/5', '1400/11/6', '1400/11/7', '1400/11/8', '1400/11/9', '1400/11/10', '1400/11/11', '1400/11/12', '1400/11/13', '1400/11/14', '1400/11/15', '1400/11/16', '1400/11/17', '1400/11/18', '1400/11/19', '1400/11/20', '1400/11/21', '1400/11/22', '1400/11/23', '1400/11/24', '1400/11/25', '1400/11/26', '1400/11/27', '1400/11/28', '1400/11/29', '1400/11/30', '1400/11/31'],
        datasets: [{
            label: 'بازدید کاربران',
            data: [2, 4, 7, 4, 9, 15, 19, 32, 18, 6, 7, 21, 30, 15, 12, 14, 9, 9, 11.5, 12.8, 11.7, 18, 28, 26, 18, 13, 15, 31, 15, 14, 16],
            backgroundColor: [
                'white'
            ],
            borderWidth: 2,
            borderRadius: 1,
            borderColor: '#2690ba',
            pointRadius: 2,
            // tension: 0.3
        }]
    },
    options: {
        plugins: {
            legend: {
                display: false,
            },
        },
        responsive: true,
        scales: {
            y: {
                beginAtZero: true,
                stacked: true,
                ticks: {
                    display: false,
                },
                grid: {
                    display: DISPLAY,
                    drawBorder: BORDER,
                    drawOnChartArea: CHART_AREA,
                    drawTicks: TICKS,
                    color: '#f0f3f5',
                }
            },
            x: {
                stacked: true,
                ticks: {
                    display: false,
                },
                grid: {
                    display: DISPLAY,
                    drawBorder: BORDER,
                    drawOnChartArea: CHART_AREA,
                    drawTicks: TICKS,
                    color: '#f0f3f5',
                }
            }
        }
    }
});

var myChartSellYear = new Chart(chartSellYear, {
    type: 'bar',
    data: {
        labels: ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"],
        datasets: [{
            label: 'درآمد سالانه',
            data: [22, 22, 18, 4, 7, 21, 30, 15, 12, 14, 9, 16],
            backgroundColor: [
                '#2690ba'
            ],
            borderWidth: 0,
            borderRadius: 3
        }]
    },
    options: {
        plugins: {
            legend: {
                display: false,
            },
        },
        responsive: true,
        scales: {
            y: {
                beginAtZero: true,
                stacked: true,
                // title: false
                ticks: {
                    display: false,
                },
                grid: {
                    display: DISPLAY,
                    drawBorder: BORDER,
                    drawOnChartArea: CHART_AREA,
                    drawTicks: TICKS,
                    color: '#f0f3f5',
                }
            },
            x: {
                stacked: true,
                ticks: {
                    display: false,
                },
                grid: {
                    display: DISPLAY,
                    drawBorder: BORDER,
                    drawOnChartArea: CHART_AREA,
                    drawTicks: TICKS,
                    color: '#f0f3f5',
                }
            }
        }
    }
});

