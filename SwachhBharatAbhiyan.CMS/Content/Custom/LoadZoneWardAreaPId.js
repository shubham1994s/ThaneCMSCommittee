﻿$(document).ready(function () {
    zone();
    //prabhag();
    //ward();
    //area();
    $('#ZoneId').change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();

        //if(selectedValue==0){
        //    ward();
        //}
        //if (selectedValue == 0) {
        //    area();
        //}

        $.ajax({
            type: "post",
            url: "/GarbageCollection/LoadPrabhagNoList?",
            data: { ZoneId: $('#ZoneId').val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                var prabhag;
                for (var i = 0; i < data.length; i++) {
                    prabhag = prabhag + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
                }

                $('#PrabhagNo').html(prabhag);

                $('#WardNo').find('option').remove().end().append('<option value="0">--Select Ward--</option>');

                $('#AreaId').find('option').remove().end().append('<option value="0">--Select Area--</option>');
            }
        });

    });
    $('#PrabhagNo').change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();

        //if(selectedValue==0){
        //    ward();
        //}
        //if (selectedValue == 0) {
        //    area();
        //}

        $.ajax({
            type: "post",
            url: "/GarbageCollection/LoadWardNoList?",
            data: { PrabhagId: $('#PrabhagNo').val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                var ward;
                for (var i = 0; i < data.length; i++) {
                    ward = ward + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
                }

                $('#WardNo').html(ward);

                $('#AreaId').find('option').remove().end().append('<option value="0">--Select Area--</option>');
            }
        });

    });

    $('#WardNo').change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();

        $.ajax({
            type: "post",
            url: "/GarbageCollection/LoadAreaList?",
            data: { WardNo: $('#WardNo').val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                var area;
                for (var i = 0; i < data.length; i++) {
                    area = area + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
                }
                $('#AreaId').html(area);
            }
        });

    });

});



function zone() {

    var UserId = $('#ZoneId').val();
    $.ajax({
        type: "post",
        url: "/GarbageCollection/ZoneList",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            var district;
            for (var i = 0; i < data.length; i++) {

                if (data[i].Value == $('#ZoneId').val()) {
                    district = district + '<option value=' + data[i].Value + ' selected>' + data[i].Text + '</option>';
                } else { district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>'; }

            }
            //district = district + '</select>';
            $('#ZoneId').html(district);
        }
    });

}

function area() {

    var UserId = $('#AreaId').val();
    $.ajax({
        type: "post",
        url: "/GarbageCollection/AreaList",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            var district;
            for (var i = 0; i < data.length; i++) {

                if (data[i].Value == $('#AreaId').val()) {
                    district = district + '<option value=' + data[i].Value + ' selected>' + data[i].Text + '</option>';
                } else { district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>'; }

            }
            //district = district + '</select>';
            $('#AreaId').html(district);
        }
    });

}

function ward() {

    var UserId = $('#WardNo').val();
    $.ajax({
        type: "post",
        url: "/GarbageCollection/WardList",
        data: { userId: UserId },
        datatype: "json",
        traditional: true,
        success: function (data) {
            var district;
            for (var i = 0; i < data.length; i++) {

                if (data[i].Value == $('#WardNo').val()) {
                    district = district + '<option value=' + data[i].Value + ' selected>' + data[i].Text + '</option>';
                } else { district = district + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>'; }
            }
            //district = district + '</select>';
            $('#WardNo').html(district);
        }
    });


}

function prabhag() {

    var UserId = $('#PrabhagNo').val();
    $.ajax({
        type: "post",
        url: "/GarbageCollection/PrabhagList",
        data: { userId: UserId },
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


}