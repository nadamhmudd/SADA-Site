var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    LoadDataTable(url);
});

function LoadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/OrderManagement/GetAll" + status
        },
        "order": [],
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "25%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "orderTotal", "width": "10%" },
            { "data": "orderStatus", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div role="group" style="text-align:center">
                            <a href="/Admin/OrderManagement/Details?orderId=${data}"
                            class="btn btn-outline-light btn-sm">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                        </div>
                            `
                },
                "width": "5%"
            }
        ],
    });
}