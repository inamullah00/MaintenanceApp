$(document).ready(function () {
    var table = $('#countriesTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": false,
        "ordering": false,
        "ajax": {
            "url": "/Country/GetFilteredCountries",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.Name = $('#searchName').val();
            }
        },
        columns: [
            { data: 'Name', name: 'Name', "autoWidth": true },
            { data: 'DialCode', name: 'DialCode', "autoWidth": true, "className": "text-center" },
            { data: 'CountryCode', name: 'CountryCode', "autoWidth": true, "className": "text-center" },
            { data: 'FlagCode', name: 'FlagCode', "autoWidth": true, "className": "text-center" },
        ],
        "pageLength": 10,
        "lengthMenu": [[10, 25, 50], [10, 25, 50]]
    });

    $("#filter").on("click", function () {
        event.preventDefault();
        table.draw();
    });
});