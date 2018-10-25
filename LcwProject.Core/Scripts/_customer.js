
let url = "";

$(document).ready(function () {

    function GETURL() {
        $.ajax({
            url: "apiInfo/GetApiURL",
            async: false,
            success: function (APIURL) {
                url = APIURL;
            },
            error: function (response) {
                console.log(response.responseText);
            }
        })
    }
    GETURL();
    var dataTableData = $("#customerDataTable");
    var result = $("#result");
    var formData = $("#formData");
    var btnNewCustomer = $("button[id = btnNewCustomer]");
    var GetAllCustomerData = function () {
        $.ajax({
            async: true,
            url: url + "GetAllCustomers",
            dataType: "json",
            method: "GET",
            success: function (data) {
                dataTableData.DataTable({

                    "data": data,
                    "destroy": true,
                    "ordering": true,
                    "paging": true,
                    "columns": [
                        { "data": "CustomerId" },
                        { "data": "CustomerName" },
                        { "data": "CustomerAdress" },
                        {
                            "data": "CustomerId",
                            "render": function (data, type, row) {
                                return "<input type='button' onclick = 'GetCustomerInfo(" + data + "," + 0 + ")' class = 'btn btn-primary btn-xs' value='Düzenle'/>";
                            }

                        },
                        {
                            "data": "CustomerId",
                            "render": function (data, type, row) {
                                return "<input type='button' id=" + data + " onclick = 'GetConfirm(" + data + ")' class = 'btn btn-danger btn-xs' value='Sil'/>";
                            }
                        }
                    ]
                });
            },
            error: function (response) {
                console.log(response);
            }
        })
    }
    GetAllCustomerData();
    formData.submit(function (e) {

        e.preventDefault();
        let id = $("#CustomerId").val();
        debugger;
        if (!formData.valid()) {
            return false;
        }
        let _type = "";
        let _url = "";

        let _data = {

            CustomerId: $("#CustomerId").val(),
            CustomerName: $("#CustomerName").val(),
            CustomerAdress: $("#CustomerAdress").val()
        }

        if (id == 0) {
            _type = "POST";
            _url = url + "customers/";
        }
        else {
            _type = "PUT";
            _url = url + "customers/" + id;
        }

        debugger;
        $.ajax({
            url: _url,
            type: _type,
            data: JSON.stringify(_data),
            dataType: "json",
            contentType: "application/json",
            beforeSend: function () {
                result.hide();
            },
            success: function (response) {

                GetAllCustomerData();
                result.html(response);
                result.show(1000);

            },
            error: function (response) {
                console.log(response);
            }
        })


    })
    btnNewCustomer.on("click", function () {
        $("#CustomerId").val(0);
        GetCustomerInfo(0, 1);
    })

})
function GetCustomerInfo(CustomerId, type) {

    var modal = $("#customerModal");

    if (CustomerId == 0) {
        modal.modal("show");
        return false;
    }

    let _type = "";

    switch (type) {

        case 0:
            _type = "GET";
            break;
        case 1:
            _type = "POST";
            break;
    }
    debugger;
    $.ajax({
        url: url + "customers/" + CustomerId,
        type: _type,
        dataType: "json",
        success: function (customerInfo) {

            if (customerInfo != null) {

                $("#CustomerId").val(customerInfo.CustomerId);
                $("#CustomerName").val(customerInfo.CustomerName);
                $("#CustomerAdress").val(customerInfo.CustomerAdress);

                modal.modal("show");
            }

        },
        error: function (error) {

            console.log(error);
        }

    })


}
function GetConfirm(CustomerId) {

    var Row = $("#" + CustomerId).parent().parent()
    debugger;
    bootbox.confirm({
        message: "Müşteri No : " + CustomerId + " Silinecektir Onaylıyor Musunuz ?",
        buttons: {
            confirm: {
                label: 'Evet',
                className: 'btn-success'
            },
            cancel: {
                label: 'Hayır',
                className: 'btn-danger'
            }
        },
        callback: function (result) {

            debugger;
            if (result) {

                $.ajax({
                    url: url + "customers/" + CustomerId,
                    dataType: "json",
                    contentType: "application/json",
                    method: "DELETE",
                    success: function (response) {
                        Row.remove();
                    },
                    error: function (response) {
                        console.log(response.responseText);
                    }
                })
            }
        }
    });
}

