
$(document).ready(function () {
    if ($(window).width() < 500) {
        $('.col').addClass('col-6');
        $('.col').append('<br/><br/>');

    }
    //if ($("#asdf").text() != "namratas@appynitty.com!") {
    //    $(".custom_col_hide").css("display", "block")
    //    $(".custom_col").removeClass('col-xl-3')
    //    $(".custom_col").addClass('col-xl-4')
    //}
    if ($("#asdf").text() == "ralegaon@np.com!" || $("#asdf").text() == "badnapur@appynitty.com!" || $("#asdf").text() == "jamkhed@np.com!" || $("#asdf").text() == "shevgaon@np.com!") {

        $(".custom_col_hide").css("display", "none")
        $(".custom_col").removeClass('col-xl-3')
        $(".custom_col").addClass('col-xl-4')
    }
    //if ($("#asdf").text() == "satana@np.com!") {
    //    $(".custom_col_hide").css("display", "block")
    //    $(".custom_col").removeClass('col-xl-3')
    //    $(".custom_col").addClass('col-xl-4')
    //}
    $('#refresh').click(function () {

        myMap2();

    });
    $('#refresh2').click(function () {

        myMapHouse();

    });
})
//Dashboardmap 1 (attendance)
function myMap2() {
    $("#wait").css("display", "block");
    $("#googleMap").css("display", "none");
    $.ajax({
        type: "post",
        url: "/Location/LocatioList?date=" + $('#txt_fdate').val(),
        datatype: "json",
        traditional: true,
        success: function (data) {
            $("#wait").css("display", "none");
            $("#googleMap").css("display", "block");
            if (data.length == 0) {
                var map = new google.maps.Map(document.getElementById('googleMap'), {
                    zoom: 10,
                    center: new google.maps.LatLng($('#deflat').val(), $('#deflang').val()),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });

            }
            var map = new google.maps.Map(document.getElementById('googleMap'), {

                center: new google.maps.LatLng(data[0].lat, data[0].log),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var infowindow = new google.maps.InfoWindow();
            bounds = new google.maps.LatLngBounds();
            var marker, i;
            // Get Addres
            debugger;
            for (i = 0; i < data.length; i++) {
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(data[i].lat, data[i].log),
                    map: map,

                    icon: {
                        labelOrigin: new google.maps.Point(16, 65),
                        url: "../Content/images/img/marker24.png"
                    },
                    label: {
                        text: data[i].userName,
                        color: "black",
                        fontWeight: "bold",
                        fontSize: "13px",
                        margin: "55px"
                    }
                });

                loc = new google.maps.LatLng(data[i].lat, data[i].log),

                    bounds.extend(loc);


                //New Marker Click
                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        debugger;
                        var latlng = new google.maps.LatLng(data[i].lat, data[i].log);
                        var geocoder = geocoder = new google.maps.Geocoder();
                        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                debugger;
                                if (results[0]) {
                                    //alert("Location: " + results[1].formatted_address);
                                    var address = results[0].formatted_address;
                                    infowindow.setContent('<div class=infowindow> <h3>' + data[i].userName + '</h3><h5><b>Details:</b></h5><p><b>Date:</b>' + data[i].date + '</p><p><b>Time:</b>' + data[i].time + '</p><p><b>Mobile:</b>' + data[i].userMobile + '<p><b>Vehicle No:</b>' + data[i].vehcileNumber + '</p><div style="height:auto; width:150px"><p><b>Address:</b>' + address + '</p></div></div>');
                                    infowindow.open(map, marker);
                                }
                                else {
                                    var address = "Not Mention";
                                    infowindow.setContent('<div class=infowindow> <h3>' + data[i].userName + '</h3><h5><b>Details:</b></h5><p><b>Date:</b>' + data[i].date + '</p><p><b>Time:</b>' + data[i].time + '</p><p><b>Mobile:</b>' + data[i].userMobile + '<p><b>Vehicle No:</b>' + data[i].vehcileNumber + '</p><div style="height:auto; width:150px"><p><b>Address:</b>' + address + '</p></div></div>');
                                    infowindow.open(map, marker);
                                }
                            }
                            else {
                                infowindow.setContent('<div class=infowindow> <h3>' + data[i].userName + '</h3><h5><b>Details:</b></h5><p><b>Date:</b>' + data[i].date + '</p><p><b>Time:</b>' + data[i].time + '</p><p><b>Mobile:</b>' + data[i].userMobile + '<p><b>Vehicle No:</b>' + data[i].vehcileNumber + '</p><div style="height:auto; width:150px"><p><b>Address:</b>' + data[i].address + '</p></div></div>');
                                infowindow.open(map, marker);
                            }
                        });


                    }
                })(marker, i));

                //Old Marker Click
                //google.maps.event.addListener(marker, 'click', (function (marker, i) {
                //    return function () {
                //        infowindow.setContent('<div class=infowindow> <h3>' + data[i].userName + '</h3><h5><b>Details:</b></h5><p><b>Date:</b>' + data[i].date + '</p><p><b>Time:</b>' + data[i].time + '</p><p><b>Mobile:</b>' + data[i].userMobile + '<p><b>Vehicle No:</b>' + data[i].vehcileNumber + '</p><div style="height:auto; width:150px"><p><b>Address:</b>' + data[i].address + '</p></div></div>');
                //        infowindow.open(map, marker);
                //    }
                //})(marker, i));


            }
            map.fitBounds(bounds);
            map.panToBounds(bounds);

        }

    });
}
//dashboard map2 (house on map)
function myMapHouse() {
    $("#waithouse").css("display", "block");
    $("#googleMapHouse").css("display", "none");
    $.ajax({
        type: "post",
        url: "/Location/HouseLocationList?date=" + $('#txt_fdate').val(),
        datatype: "json",
        traditional: true,
        success: function (data) {
            $("#waithouse").css("display", "none");
            $("#googleMapHouse").css("display", "block");
            if (data.length == 0) {
                var map = new google.maps.Map(document.getElementById('googleMapHouse'), {
                    zoom: 10,
                    center: new google.maps.LatLng($('#deflathouse').val(), $('#deflanghouse').val()),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });

            }
            var map = new google.maps.Map(document.getElementById('googleMapHouse'), {

                center: new google.maps.LatLng(data[0].houseLat, data[0].houseLong),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var infowindow = new google.maps.InfoWindow();
            bounds = new google.maps.LatLngBounds();
            var marker, i;
            // Get Addres
            for (i = 0; i < data.length; i++) {
                if (data[i].houseOwnerName == null) {
                    data[i].houseOwnerName = 'Not Available';
                }
                if (data[i].houseOwnerMobile == null) {
                    data[i].houseOwnerMobile = '';
                }
                if (data[i].houseAddress == null) {
                    data[i].houseAddress = '';
                }
                if (data[i].garbageType == 0) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../Content/images/img/segregationImg/icn_mixed_garbage.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                        bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == 1) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../Content/images/img/segregationImg/icn_segregated_garbage.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                        bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == 2) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../Content/images/img/segregationImg/icn_garbage_not_recevied.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                        bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == 3) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../Content/images/img/segregationImg/icn_not_specified.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                        bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '<p><b>Date:</b>' + data[i].gcDate + '</p><div style="height:auto; width:150px"><p><b>Time:</b>' + data[i].gcTime + '</p></div></div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }
                if (data[i].garbageType == null) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(data[i].houseLat, data[i].houseLong),
                        map: map,

                        icon: {
                            labelOrigin: new google.maps.Point(16, 65),
                            url: "../Content/images/img/segregationImg/icn_house.png"
                        },
                        label: {
                            //text: data[i].userName,
                            color: "black",
                            fontWeight: "bold",
                            fontSize: "13px",
                            margin: "55px"
                        }
                    });

                    loc = new google.maps.LatLng(data[i].houseLat, data[i].houseLong),

                        bounds.extend(loc);

                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent('<div class=infowindow style="max-width:190px"> <h3>' + data[i].houseOwnerName + '</h3><p><b>House Id:</b>' + data[i].ReferanceId + '</p><p><b>Mobile:</b>' + data[i].houseOwnerMobile + '</p><p><b>Address:</b>' + data[i].houseAddress + '</div>');
                            infowindow.open(map, marker);
                        }
                    })(marker, i));
                }





            }
            map.fitBounds(bounds);
            map.panToBounds(bounds);

        }

    });
}


//Bar Chart

$(document).ready(function () {

    var UserId = -1;

    $.ajax({
        type: "post",
        url: "/GarbageCollection/UserCount",
        data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {

            var ary = []
            var ary2 = []
            for (var i = 0; i < data.length; i++) {
                var name = data[i].UserName;
                name = name.trim();
                var fname = name.substring(0, name.indexOf(" "));
                ary.push({ label: fname, y: data[i]._Count, z: data[i].StartTime });

                //bb.push(data[i].UserName);

                // alert(data[i]._Count);

            }
            //console.log(ary);
            // console.log(ary2);

            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "light1", // "light1", "ligh2", "dark1", "dark2"
                animationEnabled: true,
                axisY: {
                    title: "House Collection",
                },
                title: {
                    // text: "घर संकलन"
                },
                toolTip: {
                    content: "In Time: {z}",
                },
                data: [{
                    type: "column",
                    indexLabel: "{y}",
                    indexLabelFontSize: 14,
                    indexLabelFontColor: "black",
                    dataPoints: ary
                }]
            });
            showDefaultText(chart, "No Data available");
            chart.render();
            function showDefaultText(chart, text) {
                var isEmpty = !(chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);

                if (!chart.options.subtitles)
                    (chart.options.subtitles = []);

                if (isEmpty)
                    chart.options.subtitles.push({
                        text: text,
                        verticalAlign: 'center',
                    });
                else
                    (chart.options.subtitles = []);
            }


        }
    });;
    chart.render();


});



//House Collection Pie chart
$(document).ready(function () {
    debugger;

    var bif_coll = $('#bif_coll').val();
    var mixed_coll = $('#mixed_coll').val();
    var TotalCDW_coll = $('#TotalCDW_coll').val();
    var TotalHW_coll = $('#TotalHW_coll').val();
    var not_coll = $('#not_coll').val();
    var not_spec_coll = $('#not_spec_coll').val();


    //var TotalDryWaste_coll = $('#TotalDryWaste_coll').val();
    //var TotalWetWaste_coll = $('#TotalWetWaste_coll').val();
    //var TotalDHW_coll = $('#TotalDHW_coll').val();
    //var TotalSW_coll = $('#TotalSW_coll').val();

    var tot_house_null_check = $('#tot_house_coll').val();

    // var TotalCW_coll = $('#TotalCW_coll').val();

    //var not_coll = 10;
    //var mixed_coll = 10;
    //var bif_coll = 10;
    //var not_spec_coll = 10;

    //var TotalCDW_coll = 10;
    //var TotalHW_coll = 10;
    //var TotalDryWaste_coll = 10;
    //var TotalWetWaste_coll = 10;
    //var TotalDHW_coll = 10;
    //var TotalSW_coll = 10;
    //var tot_house_null_check = 100;
    //var TotalCW_coll = 10;

    var tot_house_coll;
    if (tot_house_null_check == 0) {
        tot_house_coll = null;
    } else {
        tot_house_coll = $('#tot_house_coll').val();
        //tot_house_coll = 100;
    }


    var res_bif_coll = bif_coll * 100 / tot_house_coll;
    var res_mixed_coll = mixed_coll * 100 / tot_house_coll;
    var res_TotalCDW_coll = TotalCDW_coll * 100 / tot_house_coll;
    var res_TotalHW_coll = TotalHW_coll * 100 / tot_house_coll;
    var res_not_coll = not_coll * 100 / tot_house_coll;
    var res_not_spec_coll = not_spec_coll * 100 / tot_house_coll;



    //var res_TotalDryWaste_coll = TotalDryWaste_coll * 100 / tot_house_coll;
    //var res_TotalWetWaste_coll = TotalWetWaste_coll * 100 / tot_house_coll;
    //var res_TotalDHW_coll = TotalDHW_coll * 100 / tot_house_coll;
    //var res_TotalSW_coll = TotalSW_coll * 100 / tot_house_coll;

    // var res_TotalCW_coll = TotalCW_coll * 100 / tot_house_coll;


    var ary3 = []
    ary3.push({ v: bif_coll });
    ary3.push({ v: mixed_coll });
    ary3.push({ v: TotalCDW_coll });
    ary3.push({ v: TotalHW_coll });
    ary3.push({ v: not_coll });
    ary3.push({ v: not_spec_coll });


    //ary3.push({ v: TotalDryWaste_coll });
    //ary3.push({ v: TotalWetWaste_coll });
    //ary3.push({ v: TotalDHW_coll });
    //ary3.push({ v: TotalSW_coll });

    //ary3.push({ v: TotalCW_coll });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPie", {
        theme: "light2",
        animationEnabled: true,
        // exportEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number}",
        },
        //legend: {
        //    maxWidth: 300,
        //    itemWidth: 300,
        //    fontSize: 14,
        //     horizontalAlign: "left", // left, center ,right 
        //    verticalAlign: "top",
        //},
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            indexLabelMaxWidth: 80,
            showInLegend: true,
            legendText: "{name}:{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            legend: {
                itemMaxWidth: 250,
                itemWrap: false,// Comment itemWidth to see the difference
            },
            dataPoints: [
                //{ y: res_bif_coll, label: "वर्गीकृत कचरा", hover_number: bif_coll, color: '#388e3c' },
                //{ y: res_mixed_coll, label: "मिश्र कचरा", hover_number: mixed_coll, color: '#f44336' },
                //{ y: res_not_coll, label: "कचरा मिळाला नाही", hover_number: not_coll, color: '#fe9436' },
                //{ y: res_not_spec_coll, label: "वर्णन उपलब्ध नाही", hover_number: not_spec_coll, color: '#0086c3' },

                { y: res_bif_coll, label: "Segregated Garbage", hover_number: bif_coll, name: 'Segregated Garbage', color: '#388e3c' },
                { y: res_mixed_coll, label: "Mixed Garbage", hover_number: mixed_coll, name: 'Mixed Garbage', color: '#f44336' },
                { y: res_TotalCDW_coll, label: "Construction And Demolition Waste", hover_number: TotalCDW_coll, name: 'Construction And Demolition Waste', color: '#63676e' },
                { y: res_TotalHW_coll, label: "Horticulture Waste", hover_number: TotalHW_coll, name: 'Horticulture Waste', color: '#1ad15c' },
                { y: res_not_coll, label: "Garbage Not Received", hover_number: not_coll, name: 'Garbage Not Received', color: '#fe9436' },
                { y: res_not_spec_coll, label: "Garbage Type Not Specified", hover_number: not_spec_coll, name: 'Garbage Type Not Specified', color: '#0086c3' },

                //{ y: res_TotalWetWaste_coll, label: "Wet Waste", hover_number: TotalWetWaste_coll, name: 'Wet Waste', color: '#186634' },
                //{ y: res_TotalDryWaste_coll, label: "Dry Waste", hover_number: TotalDryWaste_coll, name: 'Dry Waste', color: '#66a2d5' },
                //{ y: res_TotalDHW_coll, label: "Domestic Hazardous Waste", hover_number: TotalDHW_coll, name: 'Domestic Hazardous Waste', color: '#8f8b28' },
                //{ y: res_TotalSW_coll, label: "Sanitary Waste", hover_number: TotalSW_coll, name: 'Sanitary Waste', color: '#c384d3' },

                // { y: res_TotalCW_coll, label: "Commercial Waste", hover_number: TotalCW_coll, color: '#63676e' },
            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(tot_house_coll && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});

//Segregation Type Bifurcation Pie Chart
$(document).ready(function () {
    debugger;

    //var bif_coll = $('#bif_coll').val();
    //var mixed_coll = $('#mixed_coll').val();
    //var TotalCDW_coll = $('#TotalCDW_coll').val();
    //var TotalHW_coll = $('#TotalHW_coll').val();
    //var not_coll = $('#not_coll').val();
    //var not_spec_coll = $('#not_spec_coll').val();


    var TotalDryWaste_coll = $('#TotalDryWaste_coll').val();
    var TotalWetWaste_coll = $('#TotalWetWaste_coll').val();
    var TotalDHW_coll = $('#TotalDHW_coll').val();
    var TotalSW_coll = $('#TotalSW_coll').val();

    var tot_house_null_check = $('#bif_coll').val();

    // var TotalCW_coll = $('#TotalCW_coll').val();

    //var not_coll = 10;
    //var mixed_coll = 10;
    //var bif_coll = 10;
    //var not_spec_coll = 10;

    //var TotalCDW_coll = 10;
    //var TotalHW_coll = 10;
    //var TotalDryWaste_coll = 10;
    //var TotalWetWaste_coll = 10;
    //var TotalDHW_coll = 10;
    //var TotalSW_coll = 10;
    //var tot_house_null_check = 100;
    //var TotalCW_coll = 10;

    var tot_house_coll;
    if (tot_house_null_check == 0) {
        tot_house_coll = null;
    } else {
        tot_house_coll = $('#tot_house_coll').val();
        //tot_house_coll = 100;
    }


    //var res_bif_coll = bif_coll * 100 / tot_house_coll;
    //var res_mixed_coll = mixed_coll * 100 / tot_house_coll;
    //var res_TotalCDW_coll = TotalCDW_coll * 100 / tot_house_coll;
    //var res_TotalHW_coll = TotalHW_coll * 100 / tot_house_coll;
    //var res_not_coll = not_coll * 100 / tot_house_coll;
    //var res_not_spec_coll = not_spec_coll * 100 / tot_house_coll;



    var res_TotalDryWaste_coll = TotalDryWaste_coll * 100 / tot_house_coll;
    var res_TotalWetWaste_coll = TotalWetWaste_coll * 100 / tot_house_coll;
    var res_TotalDHW_coll = TotalDHW_coll * 100 / tot_house_coll;
    var res_TotalSW_coll = TotalSW_coll * 100 / tot_house_coll;

    // var res_TotalCW_coll = TotalCW_coll * 100 / tot_house_coll;


    var ary3 = []
    //ary3.push({ v: bif_coll });
    //ary3.push({ v: mixed_coll });
    //ary3.push({ v: TotalCDW_coll });
    //ary3.push({ v: TotalHW_coll });
    //ary3.push({ v: not_coll });
    //ary3.push({ v: not_spec_coll });


    ary3.push({ v: TotalDryWaste_coll });
    ary3.push({ v: TotalWetWaste_coll });
    ary3.push({ v: TotalDHW_coll });
    ary3.push({ v: TotalSW_coll });

    //ary3.push({ v: TotalCW_coll });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerSegregationPie", {
        theme: "light2",
        animationEnabled: true,
        // exportEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number}",
        },
        //legend: {
        //    maxWidth: 300,
        //    itemWidth: 300,
        //    fontSize: 14,
        //     horizontalAlign: "left", // left, center ,right 
        //    verticalAlign: "top",
        //},
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            indexLabelMaxWidth: 80,
            showInLegend: true,
            legendText: "{name}:{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            legend: {
                itemMaxWidth: 250,
                itemWrap: false,// Comment itemWidth to see the difference
            },
            dataPoints: [
                //{ y: res_bif_coll, label: "वर्गीकृत कचरा", hover_number: bif_coll, color: '#388e3c' },
                //{ y: res_mixed_coll, label: "मिश्र कचरा", hover_number: mixed_coll, color: '#f44336' },
                //{ y: res_not_coll, label: "कचरा मिळाला नाही", hover_number: not_coll, color: '#fe9436' },
                //{ y: res_not_spec_coll, label: "वर्णन उपलब्ध नाही", hover_number: not_spec_coll, color: '#0086c3' },

                //{ y: res_bif_coll, label: "Segregated Garbage", hover_number: bif_coll, name: 'Segregated Garbage', color: '#388e3c' },
                //{ y: res_mixed_coll, label: "Mixed Garbage", hover_number: mixed_coll, name: 'Mixed Garbage', color: '#f44336' },
                //{ y: res_TotalCDW_coll, label: "Construction And Demolition Waste", hover_number: TotalCDW_coll, name: 'Construction And Demolition Waste', color: '#63676e' },
                //{ y: res_TotalHW_coll, label: "Horticulture Waste", hover_number: TotalHW_coll, name: 'Horticulture Waste', color: '#1ad15c' },
                //{ y: res_not_coll, label: "Garbage Not Received", hover_number: not_coll, name: 'Garbage Not Received', color: '#fe9436' },
                //{ y: res_not_spec_coll, label: "Garbage Type Not Specified", hover_number: not_spec_coll, name: 'Garbage Type Not Specified', color: '#0086c3' },

                { y: res_TotalWetWaste_coll, label: "Wet Waste", hover_number: TotalWetWaste_coll, name: 'Wet Waste', color: '#186634' },
                { y: res_TotalDryWaste_coll, label: "Dry Waste", hover_number: TotalDryWaste_coll, name: 'Dry Waste', color: '#66a2d5' },
                { y: res_TotalDHW_coll, label: "Domestic Hazardous Waste", hover_number: TotalDHW_coll, name: 'Domestic Hazardous Waste', color: '#8f8b28' },
                { y: res_TotalSW_coll, label: "Sanitary Waste", hover_number: TotalSW_coll, name: 'Sanitary Waste', color: '#c384d3' },

                // { y: res_TotalCW_coll, label: "Commercial Waste", hover_number: TotalCW_coll, color: '#63676e' },
            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(tot_house_coll && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});


//target chart

$(document).ready(function () {


    $.ajax({
        type: "post",
        url: "/Home/EmployeeTargetCount",
        //data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {
            //console.log(data);
            var ary = [];
            var ary2 = [];
            for (var i = 0; i < data.length; i++) {

                var name = data[i].UserName;
                name = name.trim();
                var fname = name.substring(0, name.indexOf(" "));
                // alert(data[i]._Count)
                ary.push({ y: data[i]._Count, label: fname });
                ary2.push({ y: parseInt(data[i].Target), label: fname });

            }

            var chart = new CanvasJS.Chart("chartContainerTarget",
                {

                    axisY: {
                        title: "Employee Target",
                    },
                    legend: {
                        fontSize: 14
                    },
                    data: [
                        {
                            type: "column",
                            color: "#9bbb59",
                            toolTipContent: "Achieved: {y}",
                            showInLegend: true,
                            legendText: "Achieved",
                            indexLabelFontSize: 14,
                            indexLabel: "{y}",
                            dataPoints:
                                ary

                        }, {
                            type: "column",
                            color: "#c0504d",
                            toolTipContent: "Target: {y}",
                            showInLegend: true,
                            legendText: "Target",
                            indexLabelFontSize: 14,
                            indexLabelPlacement: "outside",
                            indexLabel: "{y}",
                            dataPoints:
                                ary2

                        }
                    ]
                });
            showDefaultText(chart, "No Data available");
            chart.render();
            function showDefaultText(chart, text) {
                var isEmpty = !(chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);

                if (!chart.options.subtitles)
                    (chart.options.subtitles = []);

                if (isEmpty)
                    chart.options.subtitles.push({
                        text: text,
                        verticalAlign: 'center',
                    });
                else
                    (chart.options.subtitles = []);
            }


        }
    });
    chart.render();
});


//Dump Pie Chart

$(document).ready(function () {
    // debugger;
    var dry_count = $('#dry_count').val();
    var wet_count = $('#wet_count').val();
    var tot_dump_null_check = $('#tot_dump_count').val();
    //var dry_count = 45;
    //var wet_count =55;
    //var tot_dump_null_check =100;

    var tot_dump_count;
    if (tot_dump_null_check == 0) {
        tot_dump_count = null;
    } else {
        tot_dump_count = $('#tot_dump_count').val();
        // tot_dump_count = 100;
    }

    var res_dry_count = parseFloat(dry_count) * 100 / parseFloat(tot_dump_count);
    var res_wet_count = parseFloat(wet_count) * 100 / parseFloat(tot_dump_count);

    var ary3 = []
    ary3.push({ v: dry_count });
    ary3.push({ v: wet_count });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPieDump", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number} Ton",
        },
        legend: {
            //maxWidth: 180,
            //itemWidth: 75,
            fontSize: 12,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{name}:{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [
                //{ y: res_dry_count, label: "एकुण वजन (सुका कचरा)", hover_number: dry_count, color: '#0086c3' },
                //{ y: res_wet_count, label: "एकुण वजन (ओला कचरा)", hover_number: wet_count, color: '#01ad35' },

                { y: res_dry_count, label: "Total Weight (Dry Waste)", hover_number: dry_count, name: 'Total Weight (Dry Waste)', color: '#0086c3' },
                { y: res_wet_count, label: "Total Weight (Wet Waste)", hover_number: wet_count, name: 'Total Weight (Wet Waste)', color: '#01ad35' },


            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(tot_dump_count && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});

// Commercial Pie Chart

$(document).ready(function () {
    debugger;
    var CommercialSegregetedCount = $('#tot_CommercialSegregetedCount').val();
    var CommercialMixCount = $('#tot_CommercialMixCount').val();
    var CommercialNotCollectedCount = $('#tot_CommercialNotCollectedCount').val();
    var CommercialNotSpecifiedCount = $('#tot_CommercialNotSpecifiedCount').val();
    var TotalCommercialCount_check = $('#tot_TotalCommercialCount').val();

    var TotalCommercialCDW = $('#TotalCommercialCDW_coll').val();
    var TotalCommercialHW = $('#TotalCommercialHW_coll').val();

    //var CommercialSegregetedCount = 10;
    //var CommercialMixCount = 10;
    //var CommercialNotCollectedCount = 10;
    //var CommercialNotSpecifiedCount = 10;
    //var TotalCommercialCount_check = 40;

    var TotalCommercialCount;
    if (TotalCommercialCount_check == 0) {
        TotalCommercialCount = null;
    } else {
        TotalCommercialCount = $('#tot_TotalCommercialCount').val();
       // TotalCommercialCount = 40;
    }

    var res_seg1_count = parseFloat(CommercialSegregetedCount) * 100 / parseFloat(TotalCommercialCount);
    var res_mix_count = parseFloat(CommercialMixCount) * 100 / parseFloat(TotalCommercialCount);

    var res_notcollected_count = parseFloat(CommercialNotCollectedCount) * 100 / parseFloat(TotalCommercialCount);
    var res_notspecified_count = parseFloat(CommercialNotSpecifiedCount) * 100 / parseFloat(TotalCommercialCount);

    var res_construction_count = parseFloat(TotalCommercialCDW) * 100 / parseFloat(TotalCommercialCount);
    var res_horticulture_count = parseFloat(TotalCommercialHW) * 100 / parseFloat(TotalCommercialCount);

    var ary3 = []
    ary3.push({ v: CommercialSegregetedCount });
    ary3.push({ v: CommercialMixCount });
    ary3.push({ v: CommercialNotCollectedCount });
    ary3.push({ v: CommercialNotSpecifiedCount });
    ary3.push({ v: TotalCommercialCDW });
    ary3.push({ v: TotalCommercialHW });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPieCommercial", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number} ",
        },
        legend: {
            //maxWidth: 180,
            //itemWidth: 75,
            fontSize: 12,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{name}:{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [
                { y: res_seg1_count, label: "Total (Segregated)", hover_number: CommercialSegregetedCount, name: 'Total (Segregated)', color: '#388e3c' },
                { y: res_mix_count, label: "Total (Mix Waste)", hover_number: CommercialMixCount, name: 'Total (Mix Waste)', color: '#dc3545' },
                { y: res_notcollected_count, label: "Total (Not Received)", hover_number: CommercialNotCollectedCount, name: 'Total (Not Received)', color: '#fe9436' },
                { y: res_notspecified_count, label: "Total (Not Specified)", hover_number: CommercialNotSpecifiedCount, name: 'Total (Not Specified)', color: '#0086c3' },
                { y: res_construction_count, label: "Total (Counstruction Demolition)", hover_number: TotalCommercialCDW, name: 'Total (Counstruction Demolition)', color: '#63676e' },
                { y: res_horticulture_count, label: "Total (Horticulture)", hover_number: TotalCommercialHW, name: 'Total (Horticulture)', color: '#1ad15c' },

            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(TotalCommercialCount && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});

// Commercial Segregation Type Bifurcation Pie Chart

$(document).ready(function () {
    // debugger;
    var CommercialSegregetedCount = $('#tot_CommercialSegregetedCount').val();
    var CommercialWetCount = $('#tot_CommercialWetCount').val();
    var CommercialDryCount = $('#tot_CommercialDryCount').val();

    var CommercialMixCount = $('#tot_CommercialMixCount').val();
    var CommercialNotCollectedCount = $('#tot_CommercialNotCollectedCount').val();
    var CommercialNotSpecifiedCount = $('#tot_CommercialNotSpecifiedCount').val();
    var TotalCommercialCount_check = $('#tot_TotalCommercialCount').val();

    //var CommercialSegregetedCount = 10;
    //var CommercialWetCount = 5;
    //var CommercialDryCount = 5;
    ////var CommercialMixCount = 10;
    ////var CommercialNotCollectedCount = 10;
    ////var CommercialNotSpecifiedCount = 10;
    //// var TotalCommercialCount_check = 100;

    var TotalCommercialCount;
    if (CommercialSegregetedCount == 0) {
        TotalCommercialCount = null;
    } else {
        TotalCommercialCount = $('#tot_CommercialSegregetedCount').val();
        //TotalCommercialCount = 10;
    }


    var res_wet_count = parseFloat(CommercialWetCount) * 100 / parseFloat(TotalCommercialCount);
    var res_dry_count = parseFloat(CommercialDryCount) * 100 / parseFloat(TotalCommercialCount);

    //var res_seg_count = parseFloat(CommercialSegregetedCount) * 100 / parseFloat(TotalCommercialCount);
    //var res_mix_count = parseFloat(CommercialMixCount) * 100 / parseFloat(TotalCommercialCount);
    //var res_notcollected_count = parseFloat(CommercialNotCollectedCount) * 100 / parseFloat(TotalCommercialCount);
    //var res_notspecified_count = parseFloat(CommercialNotSpecifiedCount) * 100 / parseFloat(TotalCommercialCount);

    var ary3 = []

    ary3.push({ v: CommercialDryCount });
    ary3.push({ v: CommercialWetCount });

    //ary3.push({ v: CommercialSegregetedCount });
    //ary3.push({ v: CommercialMixCount });
    //ary3.push({ v: CommercialNotCollectedCount });
    //ary3.push({ v: CommercialNotSpecifiedCount });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPieCommercialSeg", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number} ",
        },
        legend: {
            //maxWidth: 180,
            //itemWidth: 75,
            fontSize: 12,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{name}:{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [
                //{ y: res_dry_count, label: "एकुण वजन (सुका कचरा)", hover_number: dry_count, color: '#0086c3' },
                //{ y: res_wet_count, label: "एकुण वजन (ओला कचरा)", hover_number: wet_count, color: '#01ad35' },

                { y: res_dry_count, label: "Total (Dry Waste)", hover_number: CommercialDryCount, name: 'Total (Dry Waste)', color: '#66a2d5' },
                { y: res_wet_count, label: "Total (Wet Waste)", hover_number: CommercialWetCount, name: 'Total (Wet Waste)', color: '#01ad35' },
                //{ y: res_seg_count, label: "Total (Segregeted)", hover_number: CommercialSegregetedCount, name: 'Total (Segregeted)', color: '#388e3c' },
                //{ y: res_mix_count, label: "Total (Mix Waste)", hover_number: CommercialMixCount, name: 'Total (Mix Waste)', color: '#dc3545' },
                //{ y: res_notcollected_count, label: "Total (Not Received)", hover_number: CommercialNotCollectedCount, name: 'Total (Not Received)', color: '#fe9436' },
                //{ y: res_notspecified_count, label: "Total (Not Specified)", hover_number: CommercialNotSpecifiedCount, name: 'Total (Not Specified)', color: '#0086c3' },



            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(TotalCommercialCount && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});

// SWM Pie Chart
$(document).ready(function () {
    // debugger;

    var TotalSWMCurrent = $('#tot_TotalSWMCurrent').val();
    var TotalSWM = $('#tot_TotalSWM').val();

    //var TotalSWMCurrent = 2;
    //var TotalSWM = 10;
    var remSWMCount = TotalSWM - TotalSWMCurrent




    var TotalSWM_check = $('#tot_TotalSWM').val();
    // var TotalSWM_check = 10;

    var TotalSWMCount;
    if (TotalSWM_check == 0) {
        TotalSWMCount = null;
    } else {
        TotalSWMCount = $('#tot_TotalSWM').val();
        //TotalSWMCount = 10;
    }

    var res_swmcurrent_count = (TotalSWMCurrent) * 100 / (TotalSWMCount);
    var res_swm_count1 = (TotalSWM) - (TotalSWMCurrent);
    var res_swm_count = (res_swm_count1) * 100 / (TotalSWMCount);




    var ary3 = []
    ary3.push({ v: res_swmcurrent_count });
    ary3.push({ v: res_swm_count });




    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPieSWM", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number} ",
        },
        legend: {
            //maxWidth: 180,
            //itemWidth: 75,
            fontSize: 12,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{name}:{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [

                { y: res_swmcurrent_count, label: "Total (SWM Scan)", hover_number: TotalSWMCurrent, name: 'Total (SWM Scan)', color: '#186634' },
                { y: res_swm_count, label: "Total (Not Scan)", hover_number: remSWMCount, name: 'Total (Not Scan)', color: '#f44336' },

            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(TotalSWMCount && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});

//CTPT Pie Chart
$(document).ready(function () {
     debugger;

    var TotalCTCount = $('#tot_TotalCTCount').val();
    var TotalPTCount = $('#tot_TotalPTCount').val();
    var TotalUCount = $('#tot_TotalUCount').val();
    var TodayCTPTScanCount_check = $('#tot_TodayCTPTScanCount').val();



    var TotalTodayCTPTScanCount;
    if (TodayCTPTScanCount_check == 0) {
        TotalTodayCTPTScanCount = null;
    } else {
        TotalTodayCTPTScanCount = $('#tot_TodayCTPTScanCount').val();

    }

    var res_ct_count = (TotalCTCount) * 100 / (TotalTodayCTPTScanCount);
    var res_pt_count = (TotalPTCount) * 100 / (TotalTodayCTPTScanCount);
    var res_u_count = (TotalUCount) * 100 / (TotalTodayCTPTScanCount);



    var ary3 = []
    ary3.push({ v: TotalCTCount });
    ary3.push({ v: TotalPTCount });
    ary3.push({ v: TotalUCount });



    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPieCTPT", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number} ",
        },
        legend: {
            //maxWidth: 180,
            //itemWidth: 75,
            fontSize: 12,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{name}:{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [
                { y: res_ct_count, label: "Total (CT)", hover_number: TotalCTCount, name: 'Total (Community Toilets)', color: '#917b0f' },
                { y: res_pt_count, label: "Total (PT)", hover_number: TotalPTCount, name: 'Total (Public Toilets)', color: '#a0a004' },
                { y: res_u_count, label: "Total (U)", hover_number: TotalUCount, name: 'Total (Urinal Toilets)', color: '#835787' },
            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(TotalTodayCTPTScanCount && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});

// Horiculture , Construction Pie Chart
$(document).ready(function () {
    //debugger;
    var TotalCDW_coll = $('#GcCDW_Weight_coll').val();
    var TotalHW_coll = $('#GcHW_Weight_coll').val();

    //var TotalCDW_coll = 45;
    //var TotalHW_coll = 55;

    var tot_Total_HW_CDW = TotalCDW_coll + TotalHW_coll;
    if (tot_Total_HW_CDW == 0) {
        tot_Total_HW_CDW = null;

    }
    else {
        tot_Total_HW_CDW = TotalCDW_coll + TotalHW_coll;
        // tot_Total_HW_CDW = 100;
    }
    //var tot_dump_count;
    //if (tot_dump_null_check == 0) {
    //    tot_dump_count = null;
    //} else {
    //    /* tot_dump_count = $('#tot_dump_count').val();*/
    //    tot_dump_count = 100;
    //}

    var res_TotalCDW_coll = TotalCDW_coll * 100 / tot_Total_HW_CDW;
    var res_TotalHW_coll = TotalHW_coll * 100 / tot_Total_HW_CDW;

    var ary3 = []
    ary3.push({ v: TotalCDW_coll });
    ary3.push({ v: TotalHW_coll });


    //console.log(ary3);
    var chart = new CanvasJS.Chart("chartContainerPieHWCDW", {
        theme: "light2",
        animationEnabled: true,
        title: {
            //text: "विलगिकरण प्रकार ",
            fontSize: 24,
            padding: 10
        },
        subtitles: [{
            //text: "United Kingdom, 2016",
            //fontSize: 16
        }],
        toolTip: {
            content: "In Numbers {hover_number} Ton",
        },
        legend: {
            maxWidth: 180,
            itemWidth: 75,
            fontSize: 12,
            // horizontalAlign: "right", // left, center ,right 
            //verticalAlign: "center",
        },
        data: [{
            type: "pie",
            indexLabelFontSize: 12,
            showInLegend: true,
            legendText: "{hover_number}",
            radius: 60,
            indexLabel: "{label} - {y}",
            yValueFormatString: "###0.0\"%\"",
            click: explodePie,
            dataPoints: [
                { y: res_TotalCDW_coll, label: "CDW", hover_number: TotalCDW_coll, color: '#63676e' },
                { y: res_TotalHW_coll, label: "HW", hover_number: TotalHW_coll, color: '#1ad15c' },

            ],
        }]
    });
    showDefaultText(chart, "No Data available");
    chart.render();
    function showDefaultText(chart, text) {
        var isEmpty = !(tot_Total_HW_CDW && chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);



        if (isEmpty) {
            chart.options.subtitles.push({
                text: text,
                verticalAlign: 'center',
            });
            (chart.options.data[0].dataPoints = []);
        }



    }
    function explodePie(e) {
        for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
            if (i !== e.dataPointIndex)
                e.dataSeries.dataPoints[i].exploded = false;
        }
    }

});


var date = new Date();

var day = date.getDate();
var month = date.getMonth() + 1;
var year = date.getFullYear();

if (month < 10) month = "0" + month;
if (day < 10) day = "0" + day;

var today = month + "/" + day + "/" + year;

document.getElementById('txt_fdate').value = today;







$('.datepicker').datepicker({
    format: 'mm/dd/yyyy',
    weekStart: 1,
    color: 'red',
    pickTime: false
}).on('changeDate', function (e) {

    $(this).datepicker('hide');

});
//hide  show on hover
$('#txt_fdate').focus(function () {
    $('.dtpk_drpdwn').eq(1).hide();
});


// by neha 8 july 2019
$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Home/EmployeeHouseCollectionType",
        //data: { userId: UserId, },
        datatype: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            var not_spec = [];
            var not_coll = [];
            var mixed = [];
            var seg = [];
            var cdw = [];
            var hw = [];
            var dry = [];
            var wet = [];
            var dhw = [];
            var sw = [];
            var emp_tar = [];
            var emp_tar2 = [];
            var cwns = [];
            var cwnr = [];
            var cwm = [];
            var cww = [];
            var cwd = [];
            var commercialsegregeted = [];
            debugger;
            for (var i = 0; i < data.length; i++) {
                // alert(data[i].inTime);
                var name = data[i].userName;
                name = name.trim();
                var lastname_array = name.split(' ');
                var lastname_firstchar;
                if (lastname_array.length == 1) {
                    //if condition lastname_array[1] == undefined
                    lastname_firstchar = ""
                } else {
                    lastname_firstchar = lastname_array[1][0];
                }


                //var fname = name.substring(0, name.indexOf(" "));
                var fname = name.replace(/ .*/, ' ');
                // alert(data[i]._Count)
                not_spec.push({ y: data[i].NotSpecidfied, label: 'Not Specified', color: '#0086c3', intime: data[i].inTime });
                not_coll.push({ y: data[i].NotCollected, label: 'Not Received', color: '#fe9436', intime: data[i].inTime });
                mixed.push({ y: data[i].MixedCount, label: 'Mixed', color: '#f44336', intime: data[i].inTime });
                seg.push({ y: data[i].Bifur, label: 'Segregated', color: '#388e3c', intime: data[i].inTime });
                cdw.push({ y: data[i].ConstructionAndDemolition, label: 'Construction & Demolition', color: '#63676e', intime: data[i].inTime });
                hw.push({ y: data[i].Horticulture, label: 'Horticulture', color: '#1ad15c', intime: data[i].inTime });
                //wet.push({ y: data[i].WetWaste, label: 'Wet Waste', color: '#186634', intime: data[i].inTime });
                // dry.push({ y: data[i].DryWaste, label: 'Dry Waste', color: '#66a2d5', intime: data[i].inTime });
                // dhw.push({ y: data[i].DomesticHazardous, label: 'Domestic Hazardous', color: '#8f8b28', intime: data[i].inTime });
                //sw.push({ y: data[i].Sanitary, label: 'Sanitary', color: '#c384d3', intime: data[i].inTime });
                cwns.push({ y: data[i].CommercialWasteNotSpecified, label: 'Commercial Waste Not Specified', color: '#0086c3', intime: data[i].inTime });
                cwnr.push({ y: data[i].CommercialNotReceived, label: 'Commercial Waste Not Received', color: '#fe9436', intime: data[i].inTime });
                cwm.push({ y: data[i].CommercialMixed, label: 'Commercial Waste Mixed', color: '#f44336', intime: data[i].inTime });
                //cww.push({ y: data[i].CommercialWet, label: 'Commercial Waste Wet', color: '#186634', intime: data[i].inTime });
                //cwd.push({ y: data[i].CommercialDry, label: 'Commercial Waste Dry', color: '#66a2d5', intime: data[i].inTime });
                commercialsegregeted.push({ y: data[i].CommercialSegregeted, label: 'Commercial Segregeted', color: '#388e3c', intime: data[i].inTime });
                emp_tar.push({ y: parseInt(data[i].gcTarget), label: fname + lastname_firstchar + ' Target', z: data[i].Count, intime: data[i].inTime });
                emp_tar2.push({ y: parseInt(data[i].gcTarget2), label: fname + lastname_firstchar + ' Target', z: data[i].Count, intime: data[i].inTime });
                // ary2.push({ y: parseInt(data[i].gcTarget), label: data[i].userName });

            }

            var chart = new CanvasJS.Chart("chartContainerTarget2",
                {

                    //title: {
                    //    text: "Grouped Stacked Chart"
                    //},
                    theme: "theme3",
                    // interval :1,
                    axisY: {
                        labelFontSize: 10,
                        labelFontColor: "dimGrey",
                        interval: 1
                    },


                    axisX: {
                        labelAngle: 0,
                        labelFontSize: 10,
                        interval: 1
                    },
                    axisY: {
                        title: "Residential Collection",
                    },
                    axisY2: {
                        labelFontSize: 10,
                        labelFontColor: "dimGrey",
                        title: "Commercial Collection",
                        // interval: 1
                    },

                    data: [

                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            showInLegend: true,
                            legendText: "Segregated",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#388e3c",
                            dataPoints: seg
                        },
                        //{
                        //    //indexLabel: "#total",
                        //    //indexLabelPlacement: "outside",
                        //    type: "stackedColumn",
                        //    showInLegend: true,
                        //    legendText: "Drywaste",
                        //    toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                        //    color: "#66a2d5",
                        //    dataPoints: dry
                        //},
                        //{
                        //    //indexLabel: "#total",
                        //    //indexLabelPlacement: "outside",
                        //    type: "stackedColumn",
                        //    showInLegend: true,
                        //    legendText: "Wetwaste",
                        //    toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                        //    color: "#186634",
                        //    dataPoints: wet
                        //},
                        //{
                        //    //indexLabel: "#total",
                        //    //indexLabelPlacement: "outside",
                        //    type: "stackedColumn",
                        //    showInLegend: true,
                        //    legendText: "Domestic Hazardous",
                        //    toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                        //    color: "#8f8b28",
                        //    dataPoints: dhw
                        //},
                        //{
                        //    //indexLabel: "#total",
                        //    //indexLabelPlacement: "outside",
                        //    type: "stackedColumn",
                        //    showInLegend: true,
                        //    legendText: "Sanitary",
                        //    toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                        //    color: "#c384d3",
                        //    dataPoints: sw
                        //},
                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            showInLegend: true,
                            legendText: "Mixed",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#f44336",
                            dataPoints: mixed
                        },
                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            showInLegend: true,
                            legendText: "Construction And Demolition",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#63676e",
                            dataPoints: cdw
                        },
                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            showInLegend: true,
                            legendText: "Horticulture",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#1ad15c",
                            dataPoints: hw
                        },
                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            showInLegend: true,
                            legendText: "Not Specified",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#0086c3",
                            dataPoints: not_spec
                        },


                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            showInLegend: true,
                            legendText: "Not Received",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#fe9436",
                            dataPoints: not_coll
                        },
                        //{
                        //    //indexLabel: "#total",
                        //    //indexLabelPlacement: "outside",
                        //    type: "stackedColumn",
                        //    axisYType: "secondary",
                        //    axisYIndex: 1,
                        //    showInLegend: true,
                        //    legendText: "Commercial Dry",
                        //    toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                        //    color: "#66a2d5",
                        //    dataPoints: cwd
                        //},
                        //{
                        //    //indexLabel: "#total",
                        //    //indexLabelPlacement: "outside",
                        //    type: "stackedColumn",
                        //    axisYType: "secondary",
                        //    axisYIndex: 1,
                        //    showInLegend: true,
                        //    legendText: "Commercial Wet",
                        //    toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                        //    color: "#186634",
                        //    dataPoints: cww
                        //},

                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            axisYType: "secondary",
                            axisYIndex: 1,
                            showInLegend: true,
                            legendText: "Commercial Segregeted",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#388e3c",
                            dataPoints: commercialsegregeted
                        },


                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            axisYType: "secondary",
                            axisYIndex: 1,
                            showInLegend: true,
                            legendText: "Commercial Mixed",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#f44336",
                            dataPoints: cwm
                        },

                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            axisYType: "secondary",
                            axisYIndex: 1,
                            showInLegend: true,
                            legendText: "Commercial Not Specified",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#0086c3",
                            dataPoints: cwns
                        },
                        {
                            //indexLabel: "#total",
                            //indexLabelPlacement: "outside",
                            type: "stackedColumn",
                            axisYType: "secondary",
                            axisYIndex: 1,
                            showInLegend: true,
                            legendText: "Commercial Not Received",
                            toolTipContent: "InTime:{intime} <br>{label}:{y} ",
                            color: "#fe9436",
                            dataPoints: cwnr
                        },



                        {
                            type: "line",
                            color: "#c0504d",
                            dataPoints: emp_tar,
                            // indexLabel: "{y}",
                            showInLegend: true,
                            name: "House Target",
                        }
                        ,
                        {
                            type: "line",
                            color: "#000000",
                            axisYIndex: 1,
                            axisYType: "secondary",
                            dataPoints: emp_tar2,
                            // indexLabel: "{y2}",
                            showInLegend: true,
                            name: "Commercial Target",
                        }
                        //{
                        //    type: "stackedColumn",
                        //    color: "#c0504d",
                        //    axisYType: "secondary",
                        //    toolTipContent: "Target:{y} <br> InTime:{intime}",
                        //    showInLegend: true,
                        //    legendText: "Target",
                        //    indexLabelFontSize: 14,
                        //    indexLabelPlacement: "outside",
                        //    indexLabel: "{z}/{y}",
                        //    dataPoints: emp_tar
                        //}


                    ]
                });
            showDefaultText(chart, "No Data available");
            chart.render();
            function showDefaultText(chart, text) {
                debugger;
                var isEmpty = !(chart.options.data[0].dataPoints && chart.options.data[0].dataPoints.length > 0);


                if (!chart.options.subtitles)
                    (chart.options.subtitles = []);


                if (isEmpty)
                    chart.options.subtitles.push({
                        text: text,
                        verticalAlign: 'center',
                    });

                else
                    (chart.options.subtitles = []);

            }


        }
    });
    chart.render();


});
