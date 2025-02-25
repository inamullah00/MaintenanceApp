$(document).ready(function () {
    var table = $('#packagesTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": false,
        "ordering": false,
        "ajax": {
            "url": "/Package/GetFilteredPackages",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.Name = $('#searchPackageName').val();
                d.FreelancerId = $('#FreelancerId').val();
            }
        },
        columns: [
            { data: 'Name', name: 'PackageName', "autoWidth": true },
            {
                data: 'FreelancerName', name: 'FreelancerName', "autoWidth": true
            },
            {
                data: 'Offering', name: 'Offering', "autoWidth": true
            },
            {
                data: 'Price',
                name: 'Price',
                render: function (data) {
                    return 'KWD ' + data;
                },
                "autoWidth": true,
                className: 'text-center'
            },
            {
                data: 'Id',
                render: function (data, type, row) {
                    return `
                    <div class="text-center">
                        <a href="/Package/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
                            <i class="btn-icon-prepend fa fa-pen-to-square"></i>
                        </a>
                        
                         <a href="#" class="text-danger btn-icon-text btn-xs delete" data-id="${data}" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete">
                            <i class="btn-icon-prepend fa fa-trash-can"></i>
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


    $(document).on("click", ".delete", function () {
        const title = "Do you really want to delete this package?";
        const id = $(this).attr("data-id");
        ShowDialog("Delete", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Package/Delete?id=" + id,
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