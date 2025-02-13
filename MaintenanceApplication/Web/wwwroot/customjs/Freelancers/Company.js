$(document).ready(function () {
    var table = $('#companiesTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": false,
        "ordering": false,
        "ajax": {
            "url": "/Company/GetFilteredCompanies",
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
                "autoWidth": true
            },
            {
                data: 'Status',
                render: function (data) {
                    let statusClass = "";
                    switch (data) {
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
                "autoWidth": true
            },

            {
                data: 'Id',
                render: function (data, type, row) {
                    return `
            <div class="text-center">
                <a href="/Company/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
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
});
