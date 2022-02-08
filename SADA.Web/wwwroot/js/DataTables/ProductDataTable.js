
var dataTable;

$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "order": [],
        "columns": [
            {
                "data": "coverUrl",
                "render": function (data) {
                    return `<img src="${data}" width=100%; height=100%/>`
                },
                "width": "15%",
            },
            {
                "data": "name",
                "render": function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                "width": "15%"
            },
            {
                "data": "description",
                "render": function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                "width": "15%"
            },
            {
                "data": "price",
                "render": function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                "width": "20%"
            },
            {
                "data": "discountAmount",
                "render": function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                "width": "10%"
            },
            {
                "data": "category.name",
                "render": function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div role="group" style="text-align:center">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-outline-light btn-sm" style="margin-top:50px">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>

                            <a onClick=Delete('/Admin/Product/Delete/${data}')
                            class="btn btn-outline-danger btn-sm" style="margin-top:50px">
                            <i class="bi bi-trash-fill"></i>
                            </a>
                        </div>
                            `
                },
                "width": "15%"
            }
        ],
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            //here
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}