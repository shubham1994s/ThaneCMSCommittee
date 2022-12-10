$(document).ready(function () {
    var UserId = $('#selectnumber').val();

    $('#Segid').change(function () {
        debugger;
        Segid = $('#Segid').val();
        if (Segid == 1) {
            $('#stype').show();
        } else {
            SegidSub = $('#SegidSub').val('');
            $('#stype').hide();
        }
    });



    $.ajax({
        type: "post",
        url: "/Location/UserList?rn=DSI",
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
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=GarbageDSI",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0,3,4,6,7,8,9,11,12,13,14,15],
                "visible": false,
                "searchable": false
            }
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
                    if (full["los"] != null) {
                        return '<p> ' + (full["los"])
                    }
                    else {
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
            { "data": "PrabhagName", "name": "PrabhagName", "autoWidth": false },
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
    PrabhagId = $('#PrabhagNo').val();
    Segid = $('#Segid').val();
    SegidSub = $('#SegidSub').val();
    S = $('#s').val();
    Client = " ";
    NesEvent = " ";
    var Product = "";
    var catProduct = "";
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#s").val() + "," + ZoneId + "," + WardId + "," + AreaId + "," + Segid + "," + SegidSub + "," + PrabhagId;//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}