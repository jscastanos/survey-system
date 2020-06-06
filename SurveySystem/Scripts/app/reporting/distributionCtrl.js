app.controller("distributionCtrl", ["$scope", "$http", function (s, r) {

    loadMun();

    function loadMun() {
        r.get("../home/getMunCity").then(function (d) {

            s.munList = d.data;
            
        });
    }

    s.getDis = function (sm, g) {


        r.post("../reporting/DistributionPerLGU?munCityCode=" + sm).then(function (d) {
                s.dis = d.data;

        })
    }

}]);