
$(document).ready(function () {
    debugger;
    $('input[type=radio][name=rdType]').change(function () {
        LoadGrid();
    });
    LoadGrid();
    // $('#demoGrid').css("display", "block");
    $('#btn').hide();


});

$('#NotActivebtn').on('click', function (e) {
    debugger;

    $('#change').text('Non Active Employee');
    $('#btn').show();
    $("#hdActive").val('false');
    LoadGrid();
    e.preventDefault();

});

function NotActivebtnClick() {
    $('#change').text('Non Active Employee');
    $('#btn').show();
    $("#hdActive").val('false');
    LoadGrid();
}




function LoadGrid() {
    var RadioValue = $("input[name='rdType']:checked").val();

    if (RadioValue == 0) {
        if ($("#hdActive").val() == 'true') {
            $("#divActiveW").show();
            $("#divActiveCT").hide();
            $("#divNonActiveW").hide();
            $("#divNonActiveCT").hide();
            EmployeeW();
        }
        else {
            $("#divActiveW").hide();
            $("#divActiveCT").hide();
            $("#divNonActiveW").show();
            $("#divNonActiveCT").hide();
            NotActiveEmployeeW();
        }


    }
    else if (RadioValue == 1) {
        if ($("#hdActive").val() == 'true') {
            $("#divActiveW").hide();
            $("#divActiveCT").show();
            $("#divNonActiveW").hide();
            $("#divNonActiveCT").hide();
            EmployeeCT();
        }
        else {
            $("#divActiveW").hide();
            $("#divActiveCT").hide();
            $("#divNonActiveW").hide();
            $("#divNonActiveCT").show();
            NotActiveEmployeeCT();
        }
    }

}



function goBack() {
    location.reload();
}
function EmployeeW() {
    $("#demoGridW").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=ActiveEmployeeW",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["userProfileImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            },
            {
                "targets": [7],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["EmployeeType"] == "" || full["EmployeeType"] == null) {
                        return "Waste";
                    }
                    else {

                        return "CTPT";
                    }
                },
            },
            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "width": "30%" },
            { "data": "userNameMar", "name": "userNameMar", "width": "20%" },
            { "data": "userMobileNumber", "name": "userMobileNumber", "width": "13%" },
            { "data": "userEmployeeNo", "name": "userEmployeeNo", "width": "15%" },
            { "data": "userProfileImage", "name": "userProfileImage", "width": "15%" },
            { "data": "userAddress", "name": "userAddress", "width": "25%" },
            { "data": "EmployeeType", "name": "EmployeeType", "width": "25%" },
            { "data": "bloodGroup", "name": "bloodGroup", "width": "25%" },
            { "data": "isActive", "name": "isActive", "width": "25%" },
            { "data": "gcTarget", "name": "gcTarget", "width": "25%" },
            { "data": "ComgcTarget", "name": "ComgcTarget", "width": "25%" },
            { "data": "userDesignation", "name": "userDesignation", "width": "25%" },
            { "data": "Zone", "name": "Zone", "width": "25%" },
            { "data": "Prabhag", "name": "Prabhag", "width": "25%" },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["userId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer"onclick="Delete(' + full["userId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    })
}

function EmployeeCT() {
    $("#demoGridCT").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=ActiveEmployeeCT",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [10],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [11],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["userProfileImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            },
            {
                "targets": [7],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["EmployeeType"] == "" || full["EmployeeType"] == null) {
                        return "Waste";
                    }
                    else {

                        return "CTPT";
                    }
                },
            },
            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "width": "30%" },
            { "data": "userNameMar", "name": "userNameMar", "width": "20%" },
            { "data": "userMobileNumber", "name": "userMobileNumber", "width": "13%" },
            { "data": "userEmployeeNo", "name": "userEmployeeNo", "width": "15%" },
            { "data": "userProfileImage", "name": "userProfileImage", "width": "15%" },
            { "data": "userAddress", "name": "userAddress", "width": "25%" },
            { "data": "EmployeeType", "name": "EmployeeType", "width": "25%" },
            { "data": "bloodGroup", "name": "bloodGroup", "width": "25%" },
            { "data": "isActive", "name": "isActive", "width": "25%" },
            { "data": "gcTarget", "name": "gcTarget", "width": "25%" },
            { "data": "ComgcTarget", "name": "ComgcTarget", "width": "25%" },
            { "data": "userDesignation", "name": "userDesignation", "width": "25%" },
            { "data": "Zone", "name": "Zone", "width": "25%" },
            { "data": "Prabhag", "name": "Prabhag", "width": "25%" },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["userId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer"onclick="Delete(' + full["userId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    })
}


//function ActiveEmployee() {
//    $("#demoGridActive").DataTable({
//        "sDom": "ltipr",
//        "order": [[0, "desc"]],
//        "processing": true, // for show progress bar
//        "serverSide": true, // for process server side
//        "filter": true, // this is for disable filter (search box)
//        "orderMulti": false, // for disable multiple column at once
//        "pageLength": 10,
//        destroy: true,

//        "ajax": {
//            "url": "/Datable/GetJqGridJson?rn=ActiveEmployee",
//            "type": "POST",
//            "datatype": "json"
//        },

//        "columnDefs":
//            [{
//                "targets": [0],
//                "visible": false,
//                "searchable": false
//            },
//            {
//                "targets": [2],
//                "visible": false,
//                "searchable": false
//            },
//            {
//                "targets": [5],
//                "visible": true,

//                "render": function (data, type, full, meta) {
//                    if (full["userProfileImage"] != "/Images/default_not_upload.png") {
//                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
//                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
//                            + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
//                    }
//                    else {

//                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
//                    }
//                },
//            }
//            ],

//        "columns": [
//            { "data": "userId", "name": "userId", "autoWidth": false },
//            { "data": "userName", "name": "userName", "width": "30%" },
//            { "data": "userNameMar", "name": "userNameMar", "width": "20%" },
//            { "data": "userMobileNumber", "name": "userMobileNumber", "width": "13%" },
//            { "data": "userEmployeeNo", "name": "userEmployeeNo", "width": "15%" },
//            { "data": "userProfileImage", "name": "userProfileImage", "width": "15%" },
//            { "data": "userAddress", "name": "userAddress", "width": "25%" },
//            { "data": "bloodGroup", "name": "bloodGroup", "width": "25%" },
//            { "data": "isActive", "name": "isActive", "width": "25%" },
//            { "data": "gcTarget", "name": "gcTarget", "width": "25%" },
//            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["userId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
//            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer"onclick="Delete(' + full["userId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
//        ]
//    })
//}

function NotActiveEmployeeW() {
    debugger;
    $("#demoGridNonActiveW").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=NotActiveEmployeeW",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["userProfileImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            },
            {
                "targets": [7],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["EmployeeType"] == "") {
                        return "Waste";
                    }
                    else {

                        return "CTPT";
                    }
                },
            },
            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "width": "30%" },
            { "data": "userNameMar", "name": "userNameMar", "width": "20%" },
            { "data": "userMobileNumber", "name": "userMobileNumber", "width": "13%" },
            { "data": "userEmployeeNo", "name": "userEmployeeNo", "width": "15%" },
            { "data": "userProfileImage", "name": "userProfileImage", "width": "15%" },
            { "data": "userAddress", "name": "userAddress", "width": "25%" },
            { "data": "EmployeeType", "name": "EmployeeType", "width": "25%" },
            { "data": "bloodGroup", "name": "bloodGroup", "width": "25%" },
            { "data": "isActive", "name": "isActive", "width": "25%" },
            { "data": "gcTarget", "name": "gcTarget", "width": "25%" },
            { "data": "userDesignation", "name": "userDesignation", "width": "25%" },
            { "data": "Zone", "name": "Zone", "width": "25%" },
            { "data": "Prabhag", "name": "Prabhag", "width": "25%" },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["userId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer"onclick="Delete(' + full["userId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    })
}


function NotActiveEmployeeCT() {
    debugger;
    $("#demoGridNonActiveCT").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=NotActiveEmployeeCT",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [10],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [11],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["userProfileImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            },
            {
                "targets": [7],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["EmployeeType"] == "") {
                        return "Waste";
                    }
                    else {

                        return "CTPT";
                    }
                },
            },
            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "width": "30%" },
            { "data": "userNameMar", "name": "userNameMar", "width": "20%" },
            { "data": "userMobileNumber", "name": "userMobileNumber", "width": "13%" },
            { "data": "userEmployeeNo", "name": "userEmployeeNo", "width": "15%" },
            { "data": "userProfileImage", "name": "userProfileImage", "width": "15%" },
            { "data": "userAddress", "name": "userAddress", "width": "25%" },
            { "data": "EmployeeType", "name": "EmployeeType", "width": "25%" },
            { "data": "bloodGroup", "name": "bloodGroup", "width": "25%" },
            { "data": "isActive", "name": "isActive", "width": "25%" },
            { "data": "gcTarget", "name": "gcTarget", "width": "25%" },
            { "data": "userDesignation", "name": "userDesignation", "width": "25%" },
            { "data": "Zone", "name": "Zone", "width": "25%" },
            { "data": "Prabhag", "name": "Prabhag", "width": "25%" },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"   onclick="Edit(' + full["userId"] + ')"  ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer"onclick="Delete(' + full["userId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    })
}

function noImageNotification() {
    document.getElementById("snackbar").innerHTML = "Image not uploaded...";
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

function PopImages(cel) {

    $('#myModal_Image').modal('toggle');
    var imgsrc = $(cel).find('img').attr('src');
    var head = $(cel).find('.li_title').text();

    jQuery("#imggg").attr('src', imgsrc);
    //jQuery("#latlongData").text(cellValue);
    jQuery("#header_data").html(head);
}
function Edit(Id) {
    window.location.href = "/Employee/AddEmployeeDetails?teamId=" + Id;
};
function Delete(Id) {
    window.location.href = "/Employee/DeleteEmployee?teamId=" + Id;
};


function SearchActiveW() {
    var value = ",,," + $("#sActiveW").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGridW').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
function SearchActiveCT() {
    var value = ",,," + $("#sActiveCT").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGridCT').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
function SearchNonActiveW() {
    var value = ",,," + $("#sNonActiveW").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGridNonActiveW').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
function SearchNonActiveCT() {
    var value = ",,," + $("#sNonActiveCT").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGridNonActiveCT').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
