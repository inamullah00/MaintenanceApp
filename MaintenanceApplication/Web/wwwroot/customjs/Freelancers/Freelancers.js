$(document).ready(function () {
    var table = $('#freelancersTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": false,
        "ordering": false,
        "ajax": {
            "url": "/Freelancer/GetFilteredFreelancers",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.FullName = $('#searchFullName').val();
                d.AccountStatus = $('#statusFilter').val();
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
            { data: 'DateOfBirth', name: 'DateOfBirth', "autoWidth": true, "className": "text-center" },
            {
                data: 'ExperienceLevel',
                render: function (data) {
                    let levelClass = "";
                    switch (data) {
                        case "New":
                            levelClass = "bg-warning"; 
                            break;
                        case "Experienced":
                            levelClass = "bg-primary"; 
                            break;
                        case "Expert":
                            levelClass = "bg-success"; 
                            break;
                        default:
                            levelClass = "bg-light text-dark"; 
                    }
                    return `<span class="badge ${levelClass}">${data}</span>`;
                },
                "autoWidth": true,
                "className": "text-center"
            },

            {
                data: 'Status',
                render: function (data) {
                    let statusClass = "";
                    switch (data) {
                        case "Pending":
                            statusClass = "bg-warning"; 
                            break;
                        case "Suspended":
                            statusClass = "bg-danger"; 
                            break;
                        case "Approved":
                            statusClass = "bg-success"; 
                            break;
                        default:
                            statusClass = "bg-light text-dark";
                    }
                    return `<span class="badge ${statusClass}">${data}</span>`;
                },
                "autoWidth": true,
                "className": "text-center"
            },

            {
                data: 'Id',
                render: function (data, type, row) {
                    return `
            <div class="text-center">
                <a href="/Freelancer/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
                    <i class="btn-icon-prepend fa fa-edit"></i>
                </a>
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

    $(document).on("click", ".deactivate", function () {
        const title = "Do you really want to deactivate this freelancer?";
        const id = $(this).attr("data-id");
        ShowDialog("Deactivate", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Freelancer/Deactivate?id=" + id,
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

    $(document).on("click", ".activate", function () {
        const title = "Do you really want to activate this freelancer?";
        const id = $(this).attr("data-id");
        ShowDialog("Activate", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Freelancer/Activate?id=" + id,
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
