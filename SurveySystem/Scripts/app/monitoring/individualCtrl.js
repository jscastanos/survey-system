app.controller("individualCtrl", ["$scope", "$http", function (s, h) {

    getUser();

    function getUser() {

        h.get("../Utilities/GetUser").then(function (d) {

            s.users = d.data;

        })

    }

    s.getUserList = function () {

        h.post("../Utilities/getassignList?idNo=" + s.userId.idNo).then(function (d) {

            s.selectedUser = s.userId.enumerator;
            s.assignList = d.data;
            s.totalSynch = 0;
            s.totalNumRef = 0;


            s.assignList.forEach(function (m) {

                m.assignBrgy.forEach(function (b, i) {

                    //get total
                    s.totalSynch += b.synchData;
                    s.totalNumRef += b.numRef;

                    b.progress = (b.synchData / b.numRef) * 100;

                    if (b.progress >= 100) {
                        b.status =  true;
                    } else {
                        b.status = false;
                    }

                })

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


        })

    }



}]);