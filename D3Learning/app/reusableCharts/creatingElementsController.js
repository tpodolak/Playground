(function () {

    angular.module('app.reusableCharts').controller('creatingElementsCtrl', creatingElementsController);


    function creatingElementsController($scope) {
        var data = [
            {name: 'AAPL', mentions: addData([], 150, 300), byHour: 34.3},
            {name: 'MSFT', mentions: addData([], 150, 300), byHour: 11.1},
            {name: 'GOOG', mentions: addData([], 150, 300), byHour: 19.2},
            {name: 'NFLX', mentions: addData([], 150, 300), byHour: 6.7}
        ];

        var barcode = barcodeChart().width(600).height(100);

        $scope.title = "Creating elements";
        $scope.appendData = function () {

            for(var i= 0,length = data.length;i<length;i++){
                addData(data[i].mentions, 30, 3 * 60);
            }

            d3.select('#chart').selectAll('table tr td.data-item').data(data).datum(function(d){
                return d.mentions;
            }).call(barcode);
        };

        activate();

        function activate() {

            var table = d3.select('#chart').selectAll('table')
                .data([data])
                .enter()
                .append('table')
                .attr('class', 'table table-condensed');

            var tableHead = table.append('thead'),
                tableBody = table.append('tbody');

            tableHead.append('tr').selectAll('th')
                .data(['Name', 'Today Mentions', 'mentions/hour'])
                .enter()
                .append('th')
                .text(function (d) {
                    return d;
                });

            // Add the table body rows.
            var rows = tableBody.selectAll('tr')
                .data(data)
                .enter()
                .append('tr');

            rows.append('td')
                .text(function (d) {
                    return d.name;
                });

            rows.append('td')
                .attr('class', 'data-item')
                .datum(function (d) {
                    return d.mentions;
                })
                .call(barcode);

            rows.append('td').append('p')
                .attr('class', 'pull-right')
                .html(function (d) {
                    return d.byHour;
                });
        }

        function barcodeChart() {

            // Definition of the chart variables.
            // Chart Variables.Attributes
            var width = 600,
                height = 30,
                margin = {top: 5, right: 5, bottom: 5, left: 5},
                value = function (d) {
                    return d.date;
                },
                timeInterval = d3.time.day;


            function chart(selection) {
                selection.each(function (data) {

                    // Select the SVG elements and bind it to a single element dataset.
                    var div = d3.select(this),
                        svg = div.selectAll('svg').data([data]);

                    // Append the SVG on enter.
                    svg.enter().append('svg')
                        .call(chart.svgInit);

                    // Select the chart group.
                    var g = svg.select('g.chart-content'),
                        lines = g.selectAll('line');

                    // Compute the most recent date of the dataset.
                    var lastDate = d3.max(data, value);
                    lastDate = lines.empty() ? lastDate : d3.max(lines.data(), value);
                    //// Compute the most recent date of the data items binded to the bars.
                    //if (!lines.empty()) {
                    //    lastDate = d3.max(lines.data(), value);
                    //}

                    // Compute the horizontal scale.
                    var xScale = d3.time.scale()
                        .domain([timeInterval.offset(lastDate, -1), lastDate])
                        .range([0, width - margin.left - margin.right]);

                    // Bind the data to the selection with the lines.
                    var bars = g.selectAll('line').data(data, value);

                    // Create the bars on enter
                    bars.enter().append('line')
                        .attr('x1', function (d) {
                            return xScale(value(d));
                        })
                        .attr('x2', function (d) {
                            return xScale(value(d));
                        })
                        .attr('y1', 0)
                        .attr('y2', height - margin.top - margin.bottom)
                        .attr('stroke', '#000')
                        .attr('stroke-opacity', 0.5);

                    // Update the scale to use the new dataset.
                    lastDate = d3.max(data, value);
                    xScale.domain([timeInterval.offset(lastDate, -1), lastDate]);

                    // Update the position of the bars.
                    bars.transition()
                        .duration(300)
                        .attr('x1', function (d) {
                            return xScale(value(d));
                        })
                        .attr('x2', function (d) {
                            return xScale(value(d));
                        });

                    // Remove the exiting bars.
                    bars.exit().transition()
                        .duration(300)
                        .attr('stroke-opacity', 0)
                        .remove();
                });
            }


            chart.svgInit = function (svg) {
                // Set the SVG size
                svg
                    .attr('width', width)
                    .attr('height', height);

                // Create and translate the container group
                var g = svg.append('g')
                    .attr('class', 'chart-content')
                    .attr('transform', 'translate(' + [margin.top, margin.left] + ')');

                // Add a background rectangle
                g.append('rect')
                    .attr('width', width - margin.left - margin.right)
                    .attr('height', height - margin.top - margin.bottom)
                    .attr('fill', 'white');
            };

            chart.width = function (value) {
                if (!arguments.length) {
                    return width;
                }
                width = value;
                return chart;
            };

            chart.height = function (value) {
                if (!arguments.length) {
                    return height;
                }
                height = value;
                return chart;
            };

            chart.value = function (accesor) {
                if (!arguments.length) {
                    return value;
                }
                value = accesor;

                return chart;
            };

            chart.timeInterval = function (value) {
                if (!arguments.length) {
                    return timeInterval;
                }
                timeInterval = value;
                return chart;
            };

            return chart;
        }


        // Compute a random interval using an Exponential Distribution
        function randomInterval(avgSeconds) {
            return Math.floor(-Math.log(Math.random()) * 1000 * avgSeconds);
        }

        // Create or extend an array of increasing dates by adding
// a number of random seconds.
        function addData(data, numItems, avgSeconds) {
            // Compute the most recent time in the data array.
            var n = data.length,
                t = (n > 0) ? data[n - 1].date : new Date();

            // Append items with increasing times in the data array.
            for (var k = 0; k < numItems - 1; k += 1) {
                t = new Date(t.getTime() + randomInterval(avgSeconds));
                data.push({date: t});
            }

            return data;
        }
    }


}());