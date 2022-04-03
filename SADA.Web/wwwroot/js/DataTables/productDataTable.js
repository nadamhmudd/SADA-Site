var dataTable;

$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: "/api/Products",
            dataSrc: ""
        },
        order: [],
        columns: [
            {
                data: "coverUrl",
                render: function (data) {
                    return `<img src="${data}" width=100%; height=100%/>`
                },
                width: "15%",
            },
            {
                data: "name",
                render: function (data, type, product) {
                    return `<div style='margin-top:50px'>
                            <a href="/Client/Home/Details?productId=${product.id}">
                                ${data}
                            </a></div>`
                },
                width: "15%"
            },
            {
                data: "description",
                render: function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                width: "15%"
            },
            {
                data: "price",
                render: function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                width: "20%"
            },
            {
                data: "discountAmount",
                render: function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                width: "10%"
            },
            {
                data: "category.name",
                render: function (data) {
                    return `<div style='margin-top:50px'>${data}</div>`
                },
                width: "10%"
            },
            {
                data: "id",
                render: function (data) {
                    return `
                        <div role="group" style="text-align:center">
                            <a href="/Admin/Product/ProductForm?id=${data}" class="btn btn-outline-light btn-sm" style="margin-top:50px">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>

                            <a onClick=Delete('${data}')
                            class="btn btn-outline-danger btn-sm" style="margin-top:50px">
                            <i class="bi bi-trash-fill"></i>
                            </a>
                        </div>
                            `
                },
                width: "15%"
            }
        ],
    });
}

function Delete(id) {
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
                url: '/api/Products/' + id,
                method: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                }
            })
        }
    })
}