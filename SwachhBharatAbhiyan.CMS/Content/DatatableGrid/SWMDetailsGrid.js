$(document).ready(function () {
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
            "url": "/Datable/GetJqGridJson?rn=SWMDetail",
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
                    "visible": true,

                    "render": function (data, type, full, meta) {
                        if (full["swmSubType"] == "MSW") {
                            return "Municiple Solid Waste";
                        }
                        else if (full["swmSubType"] == "C") {
                            return "Compost";
                        }
                        else if (full["swmSubType"] == "B") {
                            return "Biogas";
                        }
                        else if (full["swmSubType"] == "BBS") {
                            return "Biodigester Bottom Sludge";
                        }
                        else if (full["swmSubType"] == "RDF") {
                            return "Refuse Derived Fuel";
                        }
                        else if (full["swmSubType"] == "P") {
                            return "Plastics";
                        }
                        else if (full["swmSubType"] == "MAG") {
                            return "Metal And Glass";
                        }
                        else if (full["swmSubType"] == "CB") {
                            return "Cardboards";
                        }
                        else if (full["swmSubType"] == "AOV") {
                            return "Any Other Veriety";
                        }
                        else if (full["swmSubType"] == "SO") {
                            return "Soil";
                        }
                        else if (full["swmSubType"] == "SA") {
                            return "Sand";
                        }
                        else if (full["swmSubType"] == "LA") {
                            return "<8mm Aggregates";
                        }
                        else if (full["swmSubType"] == "BA") {
                            return "8-16mm Aggregates";
                        }
                        
                        else if (full["swmSubType"] == "GA") {
                            return ">18mm Aggregates";
                        }
                        else if (full["swmSubType"] == "MG") {
                            return "Manufactured Goods(Bricks,Tiles,etc.)";
                        }
                        else if (full["swmSubType"] == "FA") {
                            return "Fly Ash";
                        }
                        else if (full["swmSubType"] == "SAS") {
                            return "Sand and Soil";
                        }
                        else if (full["swmSubType"] == "DS") {
                            return "Dried Sludge";
                        }
                        else if (full["swmSubType"] == "TW") {
                            return "Treated Wastewater";
                        }
                        else if (full["swmSubType"] == "SLF") {
                            return "Sanitary Landfill Facility";
                        }
                        else {

                            return "Not Available";
                        }
                    },
                },

            ],

        "columns": [

            { "data": "swmId", "name": "swmId", "autoWidth": false },
            { "data": "ReferanceId", "name": "ReferanceId", "autoWidth": false },
            { "data": "Name", "name": "Name", "autoWidth": false },
            {
                "data": "QRCode", "name": "QRCode", "render": function (data, type, full, meta) {
                    return "<img src=\"" + data + "\" height=\"50\"/>";//<span><input class=\"btn btn-link\" type=\"button\" onclick='SaveQRCode(" + full["swmId"] + ")'  value=\"Send Link\"/></span>";
                }
            },
            { "render": function (data, type, full, meta) { return '<input  class="btn btn-link" type="button" onclick="DownloadQRCode(' + full["swmId"] + ')" value="Download" />'; } },


            { "data": "zone", "autoWidth": false },
            { "data": "Prabhag", "name": "Prabhag", "autoWidth": false },
            { "data": "WardNo", "autoWidth": false },
            { "data": "Area", "name": "Area", "autoWidth": false },
            { "data": "ManagerName", "autoWidth": false },
            { "data": "Mobile", "autoWidth": false },
            { "data": "Address", "autoWidth": false },
            { "data": "swmType", "name": "swmType", "autoWidth": false },
            { "data": "swmSubType", "name": "swmSubType", "autoWidth": false },

            //   { "render": function (data, type, full, meta) { return '<input class="btn btn-primary btn-sm" type="button" onclick="Edit(' + full["houseId"] + ')" value="Edit" /> <input style="margin-left:2px" class="btn btn-danger btn-sm" type="button" onclick="Delete(' + full["houseId"] + ',' + full["Name"] + ')" value="Delete" />'; } }
            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["swmId"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
            //<a  data-toggle="modal" style="cursor:pointer;margin-left:10px;" class="tooltip1" style="cursor:pointer" onclick="Delete(' + full["houseId"] + ')" ><i class="material-icons delete-icon">delete</i><span class="tooltiptext1">Delete</span> </a>
        ]
    });
});

function DownloadQRCode(Id) {
    window.location.href = "/SWMMaster/Export?Id=" + Id;
};

function Edit(Id) {
    //alert("Aa");
    if (Id != null) {
        var url = "/SWMMaster/AddSWMDetails?teamId=" + Id;

        window.location.href = url;

    }
};

function Delete(Id) {
    if (Id != null && Id != '') {

        if (confirm("Do you want delete selected SWM Facility Details")) {
            var url = "/HouseMaster/DeleteHouse?teamId=" + Id;
            window.location.href = url;
        }
    }
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

function Search() {
    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
