$(document).ready(function () {

    debugger;
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
       /* "order": [[12, "desc"]],*/
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=GarbageCountCTPT",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            ],

        "columns": [
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "attandDate", "name": "attandDate", "autoWidth": false },
            { "data": "HouseNumber", "name": "HouseNumber", "autoWidth": false },
            { "data": "UserName", "name": "UserName", "autoWidth": false },
            { "data": "TCount", "name": "TCount", "autoWidth": false },
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["userId"] + ')" >View</a>'; }, "width": "10%" },
        ]
    });


});

function Edit(Id) {
    debugger;
    //alert("Aa");
    if (Id != null) {
       var url = "/GarbageCollection/MenuCTPTDetailGarbageIndex?teamId=" + Id;
        window.location.href = url;
     
    }
};

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
    //Segid = $('#Segid').val();
    Client = " ";
    NesEvent = " ";
    var Product = "";
    var catProduct = "";
    var value = txt_fdate + "," + txt_tdate + "," + UserId + "," + $("#s").val() + "," + ZoneId + "," + WardId + "," + AreaId;//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}