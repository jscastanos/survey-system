app.controller("overallCtrl", ["$scope", "$http", function (s, h) {

    getMun();
    

    s.loadData = function(d) {

        h.get("../Monitoring/getmundetails?muncitycode=" + d).then(

            function (d) { //Success
                s.totalSynch = 0;
                s.totalNumRef = d.data[0].totalNumRef;
                s.brgyList = d.data[0].brgyList;

                console.log(s.brgyList)

                d.data[0].totalSynch.forEach(function (d) {
                    s.totalSynch += d;
                });

                if (!s.graphLoaded) {
                    s.graph = Morris.Donut({
                        element: 'mun-graph',
                        data: ([
                            { label: "Synchronized Data", value: s.totalSynch },
                            { label: "Remaining Data", value: s.totalNumRef - s.totalSynch },
                        ]),
                        colors: [
                            '#0B62A4',
                            '#c4c4c4'
                        ],
                        resize: true
                    });

                    s.graphLoaded = true;
                }

                s.graph.setData([
                     { label: "Synchronized Data", value: s.totalSynch },
                     { label: "Remaining Data", value: s.totalNumRef - s.totalSynch },
                ])

            },

            function () { //error
                console.log("hide chart")
            }
        );
        
    }

    function getMun(){

        h.get("../Home/getMunCity").then(function (d) {
            s.munList = d.data
        });

    }


}]);