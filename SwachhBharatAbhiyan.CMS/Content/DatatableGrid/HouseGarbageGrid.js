$(document).ready(function () {
    var UserId = $('#selectnumber').val();





    $.ajax({
        type: "post",
        url: "/Location/UserList",
        data: { userId: UserId, },
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
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[12, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=GarbageHouse",
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
                "targets": [12],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [8],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["gpBeforImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >Before Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            },
            {
                "targets": [9],
                "visible": true,

                "render": function (data, type, full, meta) {
                    if (full["gpAfterImage"] != "/Images/default_not_upload.png") {
                        return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                            "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["attandDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                            + full["Address"] + "</li><li style='display:none' class='li_title' >After Image </li></ul></span></div>";
                    }
                    else {

                        return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                    }
                },
            },


            {
                "targets": [3],

                "visible": true,

                "render": function (data, type, full, meta) {



                    if (full["type1"] == "0") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #f44336;border-radius: 50%;    vertical-align: middle;display: inline-flex;'></div> (Mixed Garbage)";
                    }
                    else if (full["type1"] == "1") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #388e3c;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Segregated Garbage)";

                    }
                    else if (full["type1"] == "2") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #fe9436;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Garbage Not Collected)";

                    }
                    else if (full["type1"] == "4") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #63676e;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Construction And Demolition Waste)";

                    }
                    else if (full["type1"] == "5") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #1ad15c;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Horticulture Waste)";

                    }
                    else if (full["type1"] == "6") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #186634;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Wet Waste)";

                    }
                    else if (full["type1"] == "7") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #66a2d5;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Dry Waste)";

                    }
                    else if (full["type1"] == "8") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #8f8b28;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Domestic Hazardous Waste)";

                    }
                    else if (full["type1"] == "9") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #c384d3;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Sanitary Waste)";

                    }
                    else if (full["type1"] == "-1") {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #0086c3;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Garbage Type Not Specified)";

                    }

                    else {
                        return "<div class='circle' style='height: 20px;width: 20px;background-color: #0086c3;border-radius: 50%;vertical-align: middle;display: inline-flex;'></div> (Garbage Type Not Specified)";

                    }

                },
            },
            ],

        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": false },
            { "data": "attandDate", "name": "attandDate", "autoWidth": false },
            { "data": "Employee", "name": "Employee", "autoWidth": false },
            { "data": "type1", "name": "type1", "autoWidth": false },
            { "data": "UserName", "name": "UserName", "autoWidth": false },
            { "data": "Address", "name": "Address", "autoWidth": false },
            { "data": "VehicleNumber", "autoWidth": false },
            { "data": "Note", "autoWidth": false },
            { "data": "gpBeforImage", "name": "gpBeforImage", "autoWidth": false },
            { "data": "gpAfterImage", "name": "gpAfterImage", "autoWidth": false },
            { "data": "ReferanceId", "name": "ReferanceId", "autoWidth": false },
            { "data": "batteryStatus", "name": "batteryStatus", "autoWidth": false },
            { "data": "gcDate", "name": "gcDate", "autoWidth": false },
          //  { "data": "los", "name": "los", "autoWidth": false },
            {
                "data": "los", "render": function (data, type, full, meta) {
                    if (full["los"] != null)
                    {
                        return '<p> ' + (full["los"])
                    }
                    else
                    {
                        return 'Not Available';
                    }
                }
            },
           // { "data": "ctype", "name": "ctype", "autoWidth": false },
            {
                "data": "ctype", "render": function (data, type, full, meta) {
                    if (full["ctype"] == null) {
                        return 'Residential';
                    }
                    else if (full["ctype"] == 'RBW') {
                        return 'Residential Building Waste';
                    }
                    else if (full["ctype"] == 'RSW') {
                        return 'Residential Slum Waste';
                    }
                    else if (full["ctype"] == 'CW') {
                        return 'Commercial Waste';
                    }
                    else {
                        return 'Not Available';
                    }
                }
            },

        ]
    });


});

function DownloadQRCode(Id) {
    window.location.href = "/GarbagePoint/Export?Id=" + Id;
};

function showInventoriesGrid() {
    Search();
}
function noImageNotification() {
    document.getElementById("snackbar").innerHTML = "Image not uploaded...";
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

function PopImages(cel) {

    $('#myModal_Image').modal('toggle');

    var addr = $(cel).find('.addr-length').text();
    var date = $(cel).find('.li_date').text();
    var imgsrc = $(cel).find('img').attr('src');
    var head = $(cel).find('.li_title').text();
    jQuery("#latlongData").text(addr);
    jQuery("#dateData").text(date);
    jQuery("#imggg").attr('src', imgsrc);
    //jQuery("#latlongData").text(cellValue);
    jQuery("#header_data").html(head);
}

function Search() {
    debugger;
    var txt_fdate, txt_tdate, Client, UserId;
    var name = [];
    var arr = [$('#txt_fdate').val(), $('#txt_tdate').val()];

    for (var i = 0; i <= arr.length - 1; i++) {
        name = arr[i].split("/");
        arr[i] = name[1] + "/" + name[0] + "/" + name[2];
    }

    txt_fdate = arr[0];
    txt_tdate = arr[1];
    UserId = $('#selectnumber').val();
    ZoneId = $('#ZoneId').val();
    WardId = $('#WardNo').val();
    AreaId = $('#AreaId').val();
    Segid = $('#Segid').val();
    Client = " ";
    NesEvent = " ";
    var Product = "";
    var catProduct = "";
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#s").val() + "," + ZoneId + "," + WardId + "," + AreaId + "," + Segid;//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}