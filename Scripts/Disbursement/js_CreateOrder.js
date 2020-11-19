$(document).ready(function () {

    $('#Menu_Disbursement').addClass("active");
    $('#Menu_Disbursements').addClass("active"); 
   
    $('#Type_Item').change(function () {
        //ค้นหาข้อมูลเพื่อเลือกครุภัณฑ์ที่จะเบิก ตามประเภทครุภัณฑ์
        document.getElementById('FormCreateSR').style.display = 'none';
        GetData($(this).val());
    });

    $('#Name_Item').change(function () {
        //ค้นหาข้อมูลเพื่อเลือกของที่จะเบิก ตามรายชื่อครุภัณฑ์
        GetData($(this).val());
        document.getElementById('FormCreateSR').style.display = 'none';
    });
    $('#data_1 .input-group.date').datepicker({
        format: 'dd/mm/yyyy',
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true
    });
    $("#Qty_Item").change(function (e) { 
        if (parseInt($('#Qty_Item').val()) > parseInt($('#Use').val()))
        // Alert แจ้งเตือนกรณีกรอกจำนวนเบิกสินค้าที่มีมากกว่า Stock
        {
            e.preventDefault();
            var nFrom = $(this).attr('data-from');
            var nAlign = $(this).attr('data-align');
            var nIcons = $(this).attr('data-icon');
            var nType = "warning";
            var nAnimIn = $(this).attr('data-animation-in');
            var nAnimOut = $(this).attr('data-animation-out');
            var mEss = "ไม่สามารถกรอกจำนวนมากกว่า จำนวนคงเหลือได้";
            notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
            $("#Qty_Item").val("").focus();
        }
    }); 
    $("#CO_User").change(function (e) { 
        $.post(baseUrl + "Class/CheckUser", {
            USER: $("#CO_User").val(),
            FUNC: "PL"
        }).done(function (data) {
            var pr = $.parseJSON(data);
           // alert(pr[0]["Mem_Name"]);
            if (data == "[]") {
                e.preventDefault();
                var nFrom = $(this).attr('data-from');
                var nAlign = $(this).attr('data-align');
                var nIcons = $(this).attr('data-icon');
                var nType = "warning";
                var nAnimIn = $(this).attr('data-animation-in');
                var nAnimOut = $(this).attr('data-animation-out');
                var mEss = "ไม่มีUserนี้ / Userไม่มีสิทธิ์";
                notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
                $("#CO_User").val("").focus();
            }
            else {
                $("#CO_User").val(pr[0]["Mem_Name"])
               // $("#Use").val(pr[0]["Use"])
            }
        }); 
    });
    $("#SaveCO").click(function () { 
       // alert($("#Date").val());
        if ($("#Qty_Item").val() != "" && $("#CO_User").val() != "") {
            $.post(baseUrl + "Disbursement/SaveCO", {
                PREFIX: $("#Prefix").val(),
                QTY_PL: $("#Plan_Qty").val(),
                RQ_DATE: $("#Date").val(),
                REF: $("#Ref").val(),
                USER: $("#PL_User").val(),
                CUS: $("#Customer").val(),
                ID_SR: 0
            }).done(function (data) {
                if (data == "S") {
                    var nFrom = "bottom";
                    var nAlign = "center";
                    var nIcons = $(this).attr('data-icon');
                    var nType = "success";
                    var nAnimIn = $(this).attr('data-animation-in');
                    var nAnimOut = $(this).attr('data-animation-out');
                    var mEss = "บันทึกข้อมูลสำเร็จ";
                    notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
                    //document.getElementById('FormCreateSR').style.display = 'none';
                    setTimeout(
                        function () {
                            window.location = baseUrl + "Planning/PLCreateOrder";
                        }, 2000);
                }
            }); 
        }
        else {
            var nFrom = $(this).attr('data-from');
            var nAlign = $(this).attr('data-align');
            var nIcons = $(this).attr('data-icon');
            var nType = "danger";
            var nAnimIn = $(this).attr('data-animation-in');
            var nAnimOut = $(this).attr('data-animation-out');
            var mEss = "กรุณากรอกข้อมูลให้ครบ";
            notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
        }
    }); 
});
    function GetData(Data) { //
        $('#data-table-basic').dataTable().fnClearTable();
        $.post(baseUrl + "Disbursement/GetDATA", {
            DATA: Data
        }).done(function (data) {
            // alert(data);
            var pr = $.parseJSON(data);
            $.each(JSON.parse(data), function (i, obj) {

                $('#data-table-basic').dataTable().fnAddData([
                    (i + 1),
                    '<a class="RQEdit" id="' + pr[i]["ID"] + '" href="#">' + pr[i]["Prefix"] + '</a>',
                    pr[i]["Item_Name"],
                    pr[i]["Type_Name"],
                    pr[i]["Qty_Item"],
                    pr[i]["Use"],
                    '<img src="../Content/img/Item/' + pr[i]["Item_Img"] + '" title="' + pr[i]["Item_Img"] + '" border="0" alt="" class="IMGS"/>'

                ]);
            });
            ImgClick();
            $('.RQEdit').on('click', function () {
                var d = new Date(),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();
                if (month.length < 2) month = '0' + month;
                if (day.length < 2) day = '0' + day;
                var val = day + "/" + month + "/" + year;

                document.getElementById('FormCreateSR').style.display = '';
                //$("#Prefix").val(this.id)
                $.post(baseUrl + "Disbursement/GetDATA", {
                    ID: this.id
                }).done(function (data) {
                    var pr = $.parseJSON(data);
                    $("#Prefix").val(pr[0]["Prefix"])
                    $("#Item_Name").val(pr[0]["Item_Name"])
                    $("#Type_Name").val(pr[0]["Type_Name"])
                    $("#Use").val(pr[0]["Use"])
                    $('#Img').empty();
                    $('#Img').append('<img src="../Content/img/Item/' + pr[0]["Item_Img"] + '" title="' + pr[0]["Item_Img"] + '" alt="" style="height:100PX" class="IMGS" />');
                    $('#Date').val(val)
                    $("#Ref").val("")
                    $("#PL_User").val("")
                    $("#Plan_Qty").val("")
                    ImgClick();
                });
            });
        });
    }
    function ImgClick() {
        $('.IMGS').on('click', function () {
            swal({
                title: this.title,
                imageUrl: "../Content/img/Item/" + this.title
            });
        });
    }
    function notify(from, align, icon, type, animIn, animOut, mEssage) { //Notify
        $.growl({
            icon: icon,
            title: ' แจ้งเตือน ',
            message: mEssage,

            url: ''
        }, {
            element: 'body',
            type: type,
            allow_dismiss: true,
            placement: {
                from: from,
                align: align
            },
            offset: {
                x: 20,
                y: 85
            },
            spacing: 10,
            z_index: 1031,
            delay: 2500,
            timer: 2000,
            url_target: '_blank',
            mouse_over: false,
            animate: {
                enter: animIn,
                exit: animOut
            },
            icon_type: 'class',
            template: '<div data-growl="container" class="alert" role="alert">' +
                '<button type="button" class="close" data-growl="dismiss">' +
                '<span aria-hidden="true">&times;</span>' +
                '<span class="sr-only">Close</span>' +
                '</button>' +
                '<span data-growl="icon"></span>' +
                '<span data-growl="title"></span>' +
                '<span data-growl="message"></span>' +
                '<a href="#" data-growl="url"></a>' +
                '</div>'
        });
    };



