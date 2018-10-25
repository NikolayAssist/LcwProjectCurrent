/// <reference path="jquery-3.3.1.js" />

$(document).ready(function () {

    var contentResult = $("#BoxListContent");
    var loading = $(".loading");
    let OrderId = $("#OrderId");

    
    $("#btnAddRow").click(function () {

        var OrderRow = {
            ProductId: $("#OrderRow_ProductId").val(),
            Barcode: $("#OrderRow_Barcode").val(),
            Description: $("#OrderRow_Description").val(),
            ProductName: $("#OrderRow_ProductId option:selected").text(),
            Amount: $("#OrderRow_Amount").val(),
            Price: $("#OrderRow_Price").val(),
            CustomerOrderId: $("#CustomerOrderId").val()
        };
        

        $.ajax({
            url: "/order/PostOrderBox",
            type: "POST",
            dataType: "html",
            data: JSON.stringify(OrderRow),
            contentType: "application/json",
            beforeSend: function(){
                loading.empty();
                loading.html("Ekleniyor...");
            },
            success: function (response) {
                contentResult.html(response);
            },
            error: function (response) {
                console.log(response);
            },
            complete: function () {
                loading.empty();
            }
        })
    })
    $("#btnEditRow").click(function () {

        var rowid = $("#CustomerOrderId").val();
        if (rowid == 0) {
            alert("Satır Seçmediniz");
        }
       
        var OrderRow = {
            ProductId: $("#OrderRow_ProductId").val(),
            Barcode: $("#OrderRow_Barcode").val(),
            Description: $("#OrderRow_Description").val(),
            Amount: $("#OrderRow_Amount").val(),
            Price: $("#OrderRow_Price").val(),
            CustomerOrderId: $("#CustomerOrderId").val(),
            GUID : $("#GUID").val()
        };
        
        $.ajax({
            url: "/order/EditOrderBoxByGuid",
            type: "GET",
            dataType: "html",
            data: OrderRow,
            success: function (response) {
                contentResult.html(response);
            },
            error: function (response) {
                console.log(response);
            },
            complete: function () {
                loading.empty();
            }
        })

    })
})

function btnClick(id,Guid, type) {
    
    var contentResult = $("#BoxListContent");
   
    switch (type) {

        case "delete":

            _url = "/order/GetOrderBox/";

            $.ajax({
                url: _url + id,
                type: "GET",
                dataType: "html",
                success: function (response) {
                    contentResult.html(response);
                },
                error: function (response) {
                    console.log(response);
                }
            });
            break;
        case "edit":
           
            _url = "/order/GetOrderBoxById/";

            $("#GUID").val(Guid);
            debugger;

            $.ajax({
                url: _url + id,
                type: "GET",
                dataType : "json",
                success: function (orderbox) {
                    
                    if (orderbox != null) {

                        $("#OrderRow_ProductId").val(orderbox.ProductId);
                        $("#OrderRow_Barcode").val(orderbox.Barcode);
                        $("#OrderRow_Description").val(orderbox.Description);
                        $("#OrderRow_Amount").val(orderbox.Amount);
                        $("#OrderRow_Price").val(orderbox.Price);
                        $("#CustomerOrderId").val(orderbox.CustomerOrderId);

                    }
                },
                error: function (response) {
                    console.log(response);
                }
            });

            break;
    }
}