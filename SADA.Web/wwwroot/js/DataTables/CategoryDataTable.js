var dataTable;

$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: "/api/Categories",
            dataSrc: ""
        },
        order: [],
        columns: [
            {data: "name", width: "15%"},
            {data: "displayOrder", width: "15%"},
            {
                data: "id",
                render: function (data) {
                    return `
                        <div role="group" style="text-align:center">
                            <a href="/Admin/Category/Edit?id=${data}"
                            class="btn btn-outline-light btn-sm">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>

                            <a onClick=Delete('${data}')
                            class="btn btn-outline-danger btn-sm">
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
                url: '/api/Categories/'+id,
                method: 'DELETE',
                success: function() {
                    dataTable.ajax.reload();
                }
            })
        }
    })
}