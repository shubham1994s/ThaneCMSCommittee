﻿$(document).ready(function () {
    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "pagingType": "input",
        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=SauchalayRegistration",
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
                    "targets": [11],
                    "visible": false,
                    "searchable": false
                },
         {
                 "targets": [8],
                 "visible": false,

                 "render": function (data, type, full, meta) {
                     if (full["Image"] != "/Images/default_not_upload.png") {
                         return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                     "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["CreatedDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                     + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                     }
                     else {

                         return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                     }
                 },
         },

          {
              "targets": [9],
              "visible": false,

              "render": function (data, type, full, meta) {
                  if (full["QrImage"] != "/Images/default_not_upload.png") {
                      return "<div style='cursor:pointer;display:inline-flex;'  onclick=PopImages(this)><img alt='Photo Not Found'  src='" + data +
                  "' style='height:35px;width:35px;cursor:pointer;margin-left:0px;'></img><span><ul class='dt_pop'  style='margin:2px -5px -5px -5px; padding:0px;list-style:none;display:none;'><li  class='li_date datediv' >" + full["CreatedDate"] + "</li><li class='addr-length' style='margin:0px 0px 0px 10px;'>"
                  + full["Address"] + "</li><li style='display:none' class='li_title' >Image </li></ul></span></div>";
                  }
                  else {

                      return "<img alt='Photo Not Found' onclick='noImageNotification()' src='/Images/default_not_upload.png' style='height:35px;width:35px;cursor:pointer;'></img>";
                  }
              },
                },
                {
                    "targets": [4],
                    "visible": true,
                    "searchable": false
                },
                {
                    "targets": [5],
                    "visible": false,
                    "searchable": false
                }

        ],

        "columns": [
              { "data": "Id", "name": "Id", "autoWidth": true },
              { "data": "SauchalayID", "name": "SauchalayID", "autoWidth": true },
              { "data": "Name", "name": "Name", "autoWidth": true },
            {
                "data": "QRCode", "name": "QRCode", "render": function (data, type, full, meta) {
                    return "<img src=\"" + data + "\" height=\"50\"/><span><input hidden class=\"btn btn-link\" type=\"button\" onclick='SaveQRCode(" + full["Id"] + ")'  value=\"Send Link\"/></span>";
                }
              },
            { "render": function (data, type, full, meta) { return '<input  class="btn btn-link" type="button" onclick="DownloadQRCode(' + full["Id"] + ')" value="Download" />'; } },

              { "data": "Mobile", "name": "Mobile", "autoWidth": true },
              { "data": "Tot", "name": "Tot", "autoWidth": true },
              { "data": "Tns", "name": "Tns", "autoWidth": true },
              { "data": "Image", "Image": "Name", "autoWidth": true },
              { "data": "QrImage", "QrImage": "Name", "autoWidth": true },
              { "data": "Address", "name": "Address", "autoWidth": true },
            { "data": "CreatedDate", "name": "CreatedDate", "autoWidth": true },
            { "data": "TOEMC", "name": "TOEMC", "autoWidth": true },
            { "data": "TOC", "name": "TOC", "autoWidth": true },
            { "data": "Prabhag", "name": "Prabhag", "autoWidth": true },
           
             
            //   { "render": function (data, type, full, meta) { return '<input class="btn btn-primary btn-sm" type="button" onclick="Edit(' + full["houseId"] + ')" value="Edit" /> <input style="margin-left:2px" class="btn btn-danger btn-sm" type="button" onclick="Delete(' + full["houseId"] + ',' + full["Name"] + ')" value="Delete" />'; } }
        { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["Id"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
      //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer" onclick="Delete(' + full["houseId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    });
});

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


function Edit(Id) {
    //alert("Aa");
    if (Id != null) {
        var url = "/Sauchalay/AddSauchalayDetails?teamId=" + Id;
        window.location.href = url;
    }
};

function DownloadQRCode(Id) {
    window.location.href = "/Sauchalay/Export?Id=" + Id;
};
    
function SaveQRCode(Id) {

    var UserId = $('#AreaId').val();
    $('#sendlink_pop').modal('toggle');
    $("#send_msg").css('color', 'red');
    //$('#send_msg').html('कृपया थांबा / Please Wait...');
    $('#send_msg').html('Please Wait...');

    $.ajax({
        type: "post",
        url: "/HouseMaster/Save?id=" + Id,
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {

            //alert("Send Successfully");
            //$('#send_msg').html('आपला संदेश पाठवला गेला आहे / Your message has been sent...');
            $('#send_msg').html('Your message has been sent...');
            $("#send_msg").css('color', 'green');
        }
    });


};


//function Delete(Id) {
//    if (Id != null && Id != '') {

//        if (confirm("Do you want delete selected House Details")) {
//            var url = "/HouseMaster/DeleteHouse?teamId=" + Id;
//            window.location.href = url;
//        }
//    }
//};


function Search() {
    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
