$(document).ready(function () {
    $('input[type=radio][name=rdType]').change(function () {
        LoadGrid();
    });
    LoadGrid();




    
});
function LoadGrid() {
    var RadioValue = $("input[name='rdType']:checked").val();

    if (RadioValue == 0) {
        $("#divW").show();
        $("#divCT").hide();
        EmpBeatMapW();
    }
    else if (RadioValue == 1) {
        $("#divW").hide();
        $("#divCT").show();
        EmpBeatMapCT();
    }

}

function EmpBeatMapW() {

    $("#demoGrid").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,
        "oLanguage": {
            "sInfo": "Showing _START_ to _END_ ",// text you want show for info section
        },

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=EmpBeatMap",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0, 1],
                "visible": false,
                "searchable": false
            }],

        "columns": [

            { "data": "ebmId", "name": "ebmId", "autoWidth": false },
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "autoWidth": false },
            { "data": "Type", "name": "Type", "autoWidth": false },

            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["ebmId"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
        ]
    });
}

function EmpBeatMapCT() {

    $("#demoGridCT").DataTable({
        "sDom": "ltipr",
        "order": [[0, "desc"]],
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        destroy: true,
        "oLanguage": {
            "sInfo": "Showing _START_ to _END_ ",// text you want show for info section
        },

        "ajax": {
            "url": "/Datable/GetJqGridJson?rn=EmpBeatMapCT",
            "type": "POST",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [0, 1],
                "visible": false,
                "searchable": false
            }],

        "columns": [

            { "data": "ebmId", "name": "ebmId", "autoWidth": false },
            { "data": "userId", "name": "userId", "autoWidth": false },
            { "data": "userName", "name": "userName", "autoWidth": false },
            { "data": "Type", "name": "Type", "autoWidth": false },

            { "render": function (data, type, full, meta) { return '<a  data-toggle="modal" class="tooltip1" style="cursor:pointer"  onclick="Edit(' + full["ebmId"] + ')" ><i class="material-icons edit-icon">edit</i><span class="tooltiptext1">Edit</span> </a>'; }, "width": "10%" },
        ]
    });
}

function Edit(Id) {
    //alert("Aa");
    if (Id != null) {
        var url = "/EmpBeatMap/AddEmpBeatMap?ebmId=" + Id;

        window.location.href = url;

    }
};

function Search() {
    var value = ",,," + $("#s").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGrid').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
function SearchCT() {
    var value = ",,," + $("#sCT").val();//txt_fdate + "," + txt_tdate + "," + UserId + "," + Client + "," + NesEvent + "," + Product + "," + catProduct + "," + 1;
    // alert(value );
    oTable = $('#demoGridCT').DataTable();
    oTable.search(value).draw();
    oTable.search("");
    document.getElementById('USER_ID_FK').value = -1;
}
