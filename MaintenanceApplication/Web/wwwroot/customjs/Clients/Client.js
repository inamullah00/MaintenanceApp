$(document).ready(function () {
    var table = $('#clientsTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": false,
        "ordering": false,
        "ajax": {
            "url": "/Client/GetFilteredClients",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.FullName = $('#searchFullName').val();
            }
        },
        columns: [
            { data: 'FullName', name: 'FullName', "autoWidth": true },
            { data: 'Email', name: 'Email', "autoWidth": true },
            {
                data: null, name: 'Mobile', "autoWidth": true,
                render: function (data, type, row) {
                    return row.DialCode ? `${row.DialCode} ${row.PhoneNumber}` : row.PhoneNumber;
                }
            },
            {
                data: 'Id',
                render: function (data, type, row) {
                    const isActive = row.IsActive;
                    return `
            <div class="text-center">
                <a href="/Client/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
                    <i class="btn-icon-prepend fa fa-edit"></i>
                </a>
                  ${isActive
                            ? ` <a href="#" class="text-danger btn-icon-text btn-xs deactivate" data-id="${data}" data-bs-toggle="tooltip" data-bs-placement="top" title="Deactivate">
                            <i class="btn-icon-prepend fa fa-ban"></i>
                        </a>`
                            : ` <a href="#" class="text-success btn-icon-text btn-xs activate" data-id="${data}" data-bs-toggle="tooltip" data-bs-placement="top" title="Activate">
                            <i class="btn-icon-prepend fa fa-check-circle"></i>
                        </a>`}
            </div>`;
                },
                "autoWidth": true,
                "className": "text-center"
            }

        ],
        "pageLength": 10,
        "lengthMenu": [[10, 25, 50], [10, 25, 50]]
    });

    $("#filter").on("click", function () {
        event.preventDefault();
        table.draw();
    });


    $(document).on("click", ".activate", function () {
        const title = "Do you really want to activate this client?";
        const id = $(this).attr("data-id");
        ShowDialog("Activate", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Client/Activate?id=" + id,
                    success: function (response) {
                        if (response.Status === "Success") {
                            SuccessToast(response.Message);
                            table.draw();
                        } else {
                            InfoToast(response.Errors.join("\n"));
                        }
                        unblockwindow();
                    },
                    error: function (response) {
                        unblockwindow();
                        handleAjaxError(response);
                    },
                });
            }
        });
        return false;
    });

    $(document).on("click", ".deactivate", function () {
        const title = "Do you really want to deactivate this client?";
        const id = $(this).attr("data-id");
        ShowDialog("Deactivate", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Client/Deactivate?id=" + id,
                    success: function (response) {
                        if (response.Status === "Success") {
                            SuccessToast(response.Message);
                            table.draw();
                        } else {
                            InfoToast(response.Errors.join("\n"));
                        }
                        unblockwindow();
                    },
                    error: function (response) {
                        unblockwindow();
                        handleAjaxError(response);
                    },
                });
            }
        });
        return false;
    });

});