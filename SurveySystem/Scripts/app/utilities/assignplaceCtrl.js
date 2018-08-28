app.controller("assignCtrl", ["$scope", "$http", function (s, h) {

    getUser();


    function getUser() {

        h.get("../Utilities/getUser").then(function (d) {

            s.users = d.data;
        });

    }


    s.getUserList = function () {

        s.oddAssignList = [];
        s.evenAssignList = [];

        h.post("../Utilities/getassignList?idNo=" + s.userId.idNo).then(function(d){
           
            s.selectedUser = s.userId.enumerator;
          
            console.log(d.data)

            for (x = 0; x < d.data.length; x++) {

                if (x % 2 == 0) {
                    s.evenAssignList.push(d.data[x]);
                } else {
                    s.oddAssignList.push(d.data[x]);
                }

            }


        })

    }


    s.openModal = function () {
        h.get("../Utilities/getMun").then(function (d) {

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

        console.log(am)

        h.post("../Utilities/saveAssignPlace?data=" + JSONdata).then(function (d) {

            if (d.data == "success") {
                $("#assignModal").modal("hide");
            }
        })


    }



}]);