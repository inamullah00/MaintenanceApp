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
            { data: 'DateOfBirth', name: 'DateOfBirth', "autoWidth": true },
            { data: 'ExperienceLevel', name: 'ExperienceLevel', "autoWidth": true },
            {
                data: 'Status',
                render: function (data) {
                    return `<span class="badge bg-primary">${data}</span>`;
                },
                "autoWidth": true
            },
            {
                data: 'Id',
                render: function (data, type, row) {
                    console.log(data);  // This will log the 'Id' value for each row.
                    return `
            <div class="text-center">
                <a href="/Freelancer/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Update">
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

    // Filter Button Click Event
    $("#filter").on("click", function () {
        event.preventDefault();
        table.draw();  // Redraw the table with new filters
    });
});
