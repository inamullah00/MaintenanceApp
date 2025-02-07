$(document).ready(function () {
    var table = $('#servicesTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": false,
        "ordering": false,
        "ajax": {
            "url": "/Service/GetFilteredServices",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.Name = $('#searchServiceName').val();
                d.IsUserCreated = $('#createdBy').val();
            }
        },
        columns: [
            { data: 'Name', name: 'ServiceName', "autoWidth": true },
            {
                data: 'IsUserCreated',  
                name: 'CreatedBy',
                render: function (data) {
                    return data === true ? 'Freelancer' : 'Admin';  
                },
                "autoWidth": true
            },
            {
                data: 'IsApproved',
                name : 'Status',
                render: function (data) {
                    let status = "";
                    if (data === true) {
                        status = `<span class="badge bg-success">Approved</span>`;
                    } else {
                        status = `<span class="badge bg-warning">Pending</span>`;
                    }
                    return status;
                },
                "autoWidth": true
            },

            {
                data: 'Id',
                render: function (data, type, row) {
                    const isActive = row.IsActive;
                    return `
                    <div class="text-center">
                        <a href="/Service/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
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
        const title = "Do you really want to activate this service?";
        const id = $(this).attr("data-id");
        ShowDialog("Activate", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Service/Activate?id=" + id,
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
        const title = "Do you really want to deactivate this service?";
        const id = $(this).attr("data-id");
        ShowDialog("Deactivate", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Service/Deactivate?id=" + id,
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
