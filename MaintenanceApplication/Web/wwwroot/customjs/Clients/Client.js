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
                    return `
            <div class="text-center">
                <a href="/Client/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit">
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