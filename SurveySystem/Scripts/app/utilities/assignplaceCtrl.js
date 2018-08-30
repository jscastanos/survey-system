app.controller("assignCtrl", ["$scope", "$http", function (s, h) {

    getUser();


    function getUser() {

        h.get("../Utilities/getUser").then(function (d) {

            s.users = d.data;
        });

    }


    s.getUserList = function () {

        h.post("../Utilities/getassignList?idNo=" + s.userId.idNo).then(function(d){
           
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


    s.openModal = function () {
        h.get("../Home/getMunCity").then(function (d) {

            s.munList = d.data;

        })
    }


    s.getModalMun = function (mun) {

        h.post("../Utilities/getBrgyList?munCode=" + mun).then(function (d) {

            s.brgyList = d.data;
        });
     
    }

    s.getAMData = function (am) {

        am.idNo = s.userId.idNo;
        am.tag = 1;

        var JSONdata = JSON.stringify(am);


        h.post("../Utilities/saveAssignPlace?data=" + JSONdata).then(function (d) {

            if (d.data == "success") {
                $("#assignModal").modal("hide");
            }
        })


    }



}]);