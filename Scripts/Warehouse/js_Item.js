$(document).ready(function () { 

    $('#Menu_Warehouse').addClass("active");
    $('#Menu_Warehouses').addClass("active");  
    $("#AllItem").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "ajax": {
            "url": '../Warehouse/LoadItem',
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Item_No" },
            { "data": "Item_Name", "name": "Item_Name", "autoWidth": true },
            { "data": "Item_Des", "name": "Item_Des", "autoWidth": true },
            { "data": "Site_Name", "name": "Site_Name", "autoWidth": true },
            { "data": "Item_Create", "name": "Item_Create", "autoWidth": true },
        //    { "data": "Barcode", "name": "Barcode", "autoWidth": true },
            {
                "data": "Barcode",
                render: function (file_id) {
                    return file_id ?
                        '<a href="../Warehouse/PrintViewToPdf?id=' + file_id + '" target="_blank" width="100">' + file_id + '</a>' :
                        null;
                    // window.open("/WMS-PD/Stock/PrintViewToPdf" + '?id=' + $("#Barcode" + bb).val());
                }
            },
            { "data": "Type_Name", "name": "Type_Name", "autoWidth": true },
            { "data": "Mem_Name", "name": "Mem_Name", "autoWidth": true },
            { "data": "Status", "name": "Status", "autoWidth": true },
          
            {
                "data": "Item_Img",
                render: function (file_id) {
                    return file_id ?
                        '<a href="../Content/img/Item/' + file_id + '" target="_blank" width="100"><img src="../Content/img/Item/' + file_id + '" title="' + file_id + '" width="50" /></a>' :
                        null;  
                },
                defaultContent: "No image",
                title: "Image"
                
            }
        ], "order": [[1, "asc"]]
       // ], "order": [[7, "desc"]]
        

    });  
});



