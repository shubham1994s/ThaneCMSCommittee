
$(document).ready(function () {

    $('#demoGrid_filter').hide();

    $('input[type=radio][name=rdType]').change(function () {
        LoadGrid();
    });
    LoadGrid();
    
    var UserIdP = $('#PrabhagNo').val();
    $.ajax({
        type: "post",
        url: "/GarbageCollection/PrabhagList",
        data: { userId: UserIdP },
        datatype: "json",
        traditional: true,
        success: function (data) {
            var district;
            for (var i = 0; i < data.length; i++) {

                if (data[i].Value == $('#PrabhagNo').val()) {
                    district = district + '<option value=' + data[i].Value + ' selected>' + data[i].Text + '</option>';
                } else { district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>'; }
            }
            //district = district + '</select>';
            $('#PrabhagNo').html(district);
        }
    });

    


});
function LoadGrid() {
    var date = new Date();

    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = day + "/" + month + "/" + year;

    document.getElementById('txt_fdate').value = today;
    document.getElementById('txt_tdate').value = today;

    $("#demoGrid").DataTable().buttons().container()
        .appendTo('#demoGrid_wrapper .col-md-6:eq(1)').addClass('float-right');

    $("#demoGridCTPT").DataTable().buttons().container()
        .appendTo('#demoGridCTPT_wrapper .col-md-6:eq(1)').addClass('float-right');

    debugger;
    var RadioValue = $("input[name='rdType']:checked").val();

    if (RadioValue == 0) {
        LoadUserList('NULL');
        $("#divCTPT").hide();
        $("#divW").show();
        EmpIdelW();
    }
    else if (RadioValue == 1) {
        LoadUserList('CT');
        $("#divW").hide();
        $("#divCTPT").show();
        EmpIdelCTPT();
    }

}
function LoadUserList(userType) {

    var UserId = $('#selectnumber').val();
    $.ajax({
        type: "post",
        url: "/Location/UserList?rn=" + userType,
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            district = '<option value="-1">Select Employee</option>';
            for (var i = 0; i < data.length; i++) {
                district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
            }
            //district = district + '</select>';
            $('#selectnumber').html(district);
        }
    });


}

function EmpIdelW() {
    $("#demoGrid").DataTable({
        buttons: [

            { extend: 'excel', className: 'btn btn-sm btn-success filter-button-style', title: 'Ideal time Report', text: 'Export to Excel', },
        ],
        //"sDom": "ltipr",
        dom: 'lBfrtip',
        lbFilter: false,
        "sDom": "ltipr",
        //"sDom": "ltipr",
        "order": [[13, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=UserIdel",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [6],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [7],
                    "visible": false,
                    "searchable": false
                }, {
                    "targets": [8],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [9],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [13],
                    "visible": false,
                    "searchable": false
                },

            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": true },
            { "data": "UserName", "name": "UserName", "autoWidth": false },
            { "data": "Date", "name": "Date", "autoWidth": false },
            { "data": "StartTime", "name": "StartTime", "autoWidth": false },
            { "data": "EndTime", "name": "EndTime", "autoWidth": false },
            { "data": "IdelTime", "name": "IdelTime", "autoWidth": false },
            { "data": "startLat", "name": "startLat", "autoWidth": true },
            { "data": "startLong", "name": "startLong", "autoWidth": true },
            { "data": "EndLat", "name": "EndLat", "autoWidth": true },
            { "data": "EndLong", "name": "EndLong", "autoWidth": true },

            {
                "render": function (data, type, full, meta) {

                    //date_str[full["userId"]] = full["Date"];
                    date_str[full["EndLat"]] = full;

                    //alert(date_str[full["userId"]]);
                    return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="user_route(' + full["EndLat"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>';
                }, "width": "10%"
            },
            { "data": "StartAddress", "name": "StartAddress", "autoWidth": false },

            { "data": "EndAddress", "name": "EndAddress", "autoWidth": false },
            { "data": "daDateTIme", "name": "daDateTIme", "autoWidth": true }

        ]
    });


}
function EmpIdelCTPT() {

    $("#demoGridCTPT").DataTable({
        buttons: [

            { extend: 'excel', className: 'btn btn-sm btn-success filter-button-style', title: 'Ideal time Report', text: 'Export to Excel', },
        ],
        //"sDom": "ltipr",
        dom: 'lBfrtip',
        lbFilter: false,
        "sDom": "ltipr",
        //"sDom": "ltipr",
        "order": [[13, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=UserIdelCTPT",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [6],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [7],
                    "visible": false,
                    "searchable": false
                }, {
                    "targets": [8],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [9],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [13],
                    "visible": false,
                    "searchable": false
                },

            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": true },
            { "data": "UserName", "name": "UserName", "autoWidth": false },
            { "data": "Date", "name": "Date", "autoWidth": false },
            { "data": "StartTime", "name": "StartTime", "autoWidth": false },
            { "data": "EndTime", "name": "EndTime", "autoWidth": false },
            { "data": "IdelTime", "name": "IdelTime", "autoWidth": false },
            { "data": "startLat", "name": "startLat", "autoWidth": true },
            { "data": "startLong", "name": "startLong", "autoWidth": true },
            { "data": "EndLat", "name": "EndLat", "autoWidth": true },
            { "data": "EndLong", "name": "EndLong", "autoWidth": true },

            {
                "render": function (data, type, full, meta) {

                    //date_str[full["userId"]] = full["Date"];
                    date_str[full["EndLat"]] = full;

                    //alert(date_str[full["userId"]]);
                    return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer" onclick="user_route(' + full["EndLat"] + ')" ><i class="material-icons location-icon">location_on</i><span class="tooltiptext1">Route</span> </a>';
                }, "width": "10%"
            },
            { "data": "StartAddress", "name": "StartAddress", "autoWidth": false },

            { "data": "EndAddress", "name": "EndAddress", "autoWidth": false },
            { "data": "daDateTIme", "name": "daDateTIme", "autoWidth": true }

        ]
    });

}
var date_str = {};
function user_route(id) {
    //console.log(date_str[id]);
    //debugger;
    var objIdle = JSON.stringify(date_str[id]);
    window.localStorage.setItem("mJson", objIdle);
    window.location.href = "/GarbageCollection/IdleTime_Route";
};
function showInventoriesGrid() {
    Search();
}
function Search() {
    var RadioValue = $("input[name='rdType']:checked").val();
    if (RadioValue == 0) {
        SearchW();
    }
    else if (RadioValue == 1) {
        SearchCTPT();
    }
}


function SearchW() {
    var txt_fdate, txt_tdate, Client, UserId, PrabhagId;
    var name = [];
    var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

    for (var i = 0; i <= arr.length - 1; i++) {
        name = arr[i].split("/");
        arr[i] = name[1] + "/" + name[0] + "/" + name[2];
    }

    txt_fdate = arr[0];
    txt_tdate = arr[1];
    UserId = $('#selectnumber').val();
    PrabhagId = $('#PrabhagNo').val();
    Client = " ";
    NesEvent = " ";
    var Product = "";
    var catProduct = "";
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#s").val() + "," + PrabhagId;//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}

function SearchCTPT() {
    var txt_fdate, txt_tdate, Client, UserId, PrabhagId;
    var name = [];
    var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

    for (var i = 0; i <= arr.length - 1; i++) {
        name = arr[i].split("/");
        arr[i] = name[1] + "/" + name[0] + "/" + name[2];
    }

    txt_fdate = arr[0];
    txt_tdate = arr[1];
    UserId = $('#selectnumber').val();
    PrabhagId = $('#PrabhagNo').val();
    Client = " ";
    NesEvent = " ";
    var Product = "";
    var catProduct = "";
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#sCTPT").val() + "," + PrabhagId;//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGridCTPT').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}