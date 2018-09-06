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


            s.assignList.forEach(function (m) {

                m.assignBrgy.forEach(function (b, i) {

                    if (b.synchData != null) {
                        b.progress = (b.synchData / b.numRef) * 100;
                    } else {
                        b.synchData = 0;
                        b.progress = 0;
                    }

                })

            });


        })

    }



}]);