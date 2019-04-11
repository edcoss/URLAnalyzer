$(document).ready(function () {
    var chartObj;

    function DisplayWordCount(data) {
        var wordCountContainer = $('#totalWordCountResult');
        if (wordCountContainer.length > 0) {
            wordCountContainer.text(data.ContentWordCount);
        }
    }

    function DisplayCarousel(data) {
        var carouselContainer = $('#carouselItemsContainer');
        var carouselIndicators = $('#carouselIndicators');
        if (carouselContainer.length > 0 && carouselIndicators.length > 0) {
            carouselContainer.empty();
            carouselIndicators.empty();
            data.ContentImages.forEach(function (item, index) {
                var activeClass = '';
                if (index == 0) {
                    activeClass = "active";
                }
                var carouselItem = $('<div class="carousel-item ' + activeClass + '"><img class="d-block w-100" src="' + item + '" alt="Image-' + (index + 1) + '"></div >');
                var carouselIndicator = $('<li data-target="#carouselItemsContainer" data-slide-to="' + index + '" class="' + activeClass + '"></li>')
                carouselItem.appendTo(carouselContainer);
                carouselIndicator.appendTo(carouselIndicators);
            });
            $('.carousel').carousel({
                interval: 2000,
            });
        }
    }

    function DisplayChart(data) {
        // sorting in descending order
        var graphContainer = $('#wordGraphContainer');
        graphContainer.empty();
        $('<canvas id="chart" width="600" height="400"></canvas>').appendTo(graphContainer);
        data.ContentWordCollection.sort((b, a) => (a.Value > b.Value) ? 1 : ((b.Value > a.Value) ? -1 : 0));
        var dataset = data.ContentWordCollection.slice(0, 10);
        var labels = [], values = [];
        dataset.forEach(function (item, index) {
            labels.push(item.Key);
            values.push(item.Value);
        });
        var chartCanvas = document.getElementById('chart');
        chartObj = new Chart(chartCanvas, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Word Count',
                    data: values,
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(66, 134, 244, 0.2)',
                        'rgba(255, 255, 114, 0.2)',
                        'rgba(145, 143, 252, 0.2)',
                        'rgba(255, 163, 227, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(66, 134, 244, 1)',
                        'rgba(255, 255, 114, 1)',
                        'rgba(145, 143, 252, 1)',
                        'rgba(255, 163, 227, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }

    $('#scrapCTA').on('click', function () {
        $this = $(this);
        $('.error-message').css('display', 'none');
        var apiUrl = $this.data('url');
        var param = $('#inputUrl').val();
        if (param && apiUrl) {
            $('.loader').css('display', 'block');
            $.ajax({
                type: 'POST',
                url: apiUrl + '?url=' + param,
                contentType: "application/json",
                dataType: 'json',
                success: function (data) {
                    // uncomment line below to debug data
                    //console.dir(data);
                    $('.loader').css('display', 'none');
                    DisplayWordCount(data);
                    DisplayCarousel(data);
                    DisplayChart(data);
                },
            });
        }
        else {
            $('.error-message').css('display', 'block');
        }
    });

});