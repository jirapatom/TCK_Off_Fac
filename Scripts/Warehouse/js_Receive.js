$(document).ready(function () {   
    document.getElementById("SaveSR").disabled = false;
    $('#Menu_Warehouse').addClass("active");
    $('#Menu_Warehouses').addClass("active");  
    $("#Item_No").change(function (e) {
        $.post(baseUrl + "Warehouse/CheckItemNoReceive", {
            ITEM_NO: $("#Item_No").val()
        }).done(function (data) {
            var pr = $.parseJSON(data);
            if (data != "[]") {
                e.preventDefault();
                var nFrom = $(this).attr('data-from');
                var nAlign = $(this).attr('data-align');
                var nIcons = $(this).attr('data-icon');
                var nType = "warning";
                var nAnimIn = $(this).attr('data-animation-in');
                var nAnimOut = $(this).attr('data-animation-out');
                var mEss = "หมายเลขครุภัณฑ์ซ้ำกันในระบบ";
                notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
                $("#Item_No").val("").focus();
            }
            else {
                $("#Item_Name").focus();
            }
        });
    }); 
    $("#User").change(function (e) {
        $.post(baseUrl + "Class/CheckUser", {
            USER: $("#User").val(),
            FUNC: "WH"
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
                $("#User").val("").focus();
            }
            else {
                $("#User").val(pr[0]["Mem_Name"])
                // $("#Use").val(pr[0]["Use"])
            }
        });
    }); 
    $("#SaveSR").click(function (e) { 
        if ($("#Item_No").val() != "" && $("#Item_Name").val() != "" && $("#Item_Des").val() != "" && $("#Factory").val() != "" && $("#Type").val() != "" && $("#User").val() != ""){
            setTimeout(
                function () { 
                    document.getElementById("SaveSR").disabled = true;
            $.post(baseUrl + "Warehouse/CreateRC", {
                ITEM_NO: $("#Item_No").val(),
                ITEM_NAME: $("#Item_Name").val(),
                ITEM_DES: $("#Item_Des").val(),
                BARCODE: $("#BarCode").val(),
                REF: $("#Ref").val(),
                FAC: $("#Factory").val(),
                TYPE: $("#Type").val(),
                USER: $("#User").val() 
            }).done(function (data) { 
                if (data == "S") {
                    UpImg();
                    var nFrom = "bottom";
                    var nAlign = "center";
                    var nIcons = $(this).attr('data-icon');
                    var nType = "success";
                    var nAnimIn = $(this).attr('data-animation-in');
                    var nAnimOut = $(this).attr('data-animation-out');
                    var mEss = "บันทึกข้อมูลสำเร็จ";
                    notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut, mEss);
                }
            });
                }, 2000);
            setTimeout(
                function () {
                    window.open("../Warehouse/PrintViewToPdf" + '?id=' + $("#BarCode").val());
                    window.location = baseUrl + "Warehouse/Receive";
                }, 5000);
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
function UpImg() {
    var formData = new FormData($("#formUP")[0]);
    $.ajax({
        type: 'POST',
        url: "../Warehouse/UploadFiles",
        data: formData,
        cache: false,
        contentType: false,
        processData: false
    });
}



 