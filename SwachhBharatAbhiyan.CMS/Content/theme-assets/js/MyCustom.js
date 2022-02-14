
//window.onbeforeunload = function () {
//    debugger;
//    var inputs = document.getElementsById("btnSubmit");
//           /* var inputs = document.getelementsbytagname("button");*/
//           /*var inputs = document.getElementsById("btnSubmit");*/
//    for (var i = 0; i < inputs.length; i++) {
//        debugger;
//        if (inputs[i].type == "button" || inputs[i].type == "submit") {
//            debugger;
//        inputs[i].disabled = true;
//                }
//            }
//        };


//document.getElementById("btnSubmit").onclick = function () {
//    //disable
//    this.disabled = true;

//    //do some validation stuff
//}


//20/01/2022



function disableButton(btn) {
    debugger;
    document.getElementById(btn.id).disabled = true;
    //alert("Button has been disabled.");
}
$(document).keydown(function (event) {
    //if (event.keyCode == 123 || event.keyCode === 67 || event.keyCode === 86 || event.keyCode === 85 || event.keyCode === 117 || event.keyCode === 75) {
    //    return false;
    //}
    //if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.keyCode == 74) || (event.ctrlKey && event.keyCode == 123) || (event.ctrlKey && event.keyCode == 67) || (event.ctrlKey && event.keyCode == 86) || (event.ctrlKey && event.keyCode == 85) || (event.ctrlKey && event.keyCode == 117) || (event.ctrlKey && event.keyCode == 75)) {
    //    return false;
    //}
});

document.addEventListener('contextmenu', event => event.preventDefault());
