app.controller("overallCtrl", ["$scope", "$http", function (s, h) {

    getMun();
    

    s.loadData = function (d) {

        h.get("../Monitoring/getmundetails?muncitycode=" + d).then(

            function (d) { //Success
                s.totalSynch = 0;
                s.totalNumRef = d.data[0].totalNumRef;
                s.brgyList = d.data[0].brgyList;
                s.totalSynch = d.data[0].totalSynch;

                if((s.totalNumRef - s.totalSynch) > 0){
                    s.remainingData = s.totalNumRef - s.totalSynch;
                }else{
                    s.remainingData = 0;
                }


                if (!s.graphLoaded) {
                    s.graph = Morris.Donut({
                        element: 'mun-graph',
                        data: ([
                            { label: "Synchronized Data", value: s.totalSynch },
                            { label: "Remaining Data", value: s.remainingData },
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
                     { label: "Remaining Data", value: s.remainingData },
                ])

            },

        );
        
    }

    function getMun(){

        h.get("../Home/getMunCity").then(function (d) {
            s.munList = d.data
        });

    }


}]);