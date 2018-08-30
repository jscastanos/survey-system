app.controller("brgyCtrl", ["$scope", "$http", function (s, h) {

    s.isModalError = false;

    getBrgy();

    function getBrgy() {
        h.get("../Home/getMunCity").then(function (d) {

            s.munList = d.data;

        })
    }

    s.getSelectedMun = function () {
   
        h.post("../Utilities/getBrgyList?munCode=" + s.selectMun).then(function (d) {

            s.brgyList = d.data;
        });
    }

    s.editBrgy = function (b) {
        s.modalState = "Edit"
        s.selectedBrgy = b;
    }
    
    s.addBrgy = function () {
        s.modalState = "Add";
        s.selectedBrgy = {
            brgyCode: "",
            brgyName: "",
            numRef: ""
        }
    }


    s.updateBrgy = function (b) {

        if (s.modalState == "Add") {
            var newData = JSON.stringify(b);

            console.log(b);

            if(b.brgyCode != "" ||
               b.brgyName != "" ||
               b.numRef != "") {
           
                h.post("../Utilities/addBrgyDetails?data=" + newData).then(function (d) {

                    if (d.data == "success") {

                        $("#brgyModal").modal("hide");

                    } else {
                        console.log("error")
                    }
                });

                s.isModalError = false;
                

            } else {

                s.isModalError = true;
                s.modalError = "No Data to be saved!";

            }


          


        } else if (s.modalState == "Edit") {
            var newData = JSON.stringify(b);


            if (b.brgyCode != "" ||
               b.brgyName != "" ||
               b.numRef != "") {

                h.post("../Utilities/updateBrgyDetails?data=" + newData).then(function (d) {

                    console.log(d)

                    if (d.data == "success") {
                        console.log("success")
                        $("#brgyModal").modal("hide");

                    } else {
                        console.log("error")
                    }
                });

                s.isModalError = false;

            } else {

                s.isModalError = true;
                s.modalError = "No Data to be saved!";

            }


        }

    }

}]);