var table = $("#BookingTable").DataTable({
    "processing": true,
    "serverSide": true,
    "filter": false,
    "ordering": false,
    "ajax": {
        "url": "/Bookings/GetFilteredBookings",
        "type": "POST",
        "datatype": "json",
        "data": function (d) {
            d.FromDate = $('#FromDate').val();
            d.ToDate = $('#ToDate').val();
            d.CustomerId = $('#CustomerId').val();
            d.Status = $('#Status').val();
        }
    },
    "columns": [
        {
            className: 'dt-control',
            orderable: false,
            data: null,
            defaultContent: ''
        },
        {
            data: "datetime",
            render: function (data, type, row, meta) {
                if (type === 'display') {
                    return formatDateToYyyyMmDd(row.CreatedDate);
                }
                return "";
            }
        },
        { "data": "TrackingId", "name": "TrackingId", "autoWidth": true },
        { "data": "CustomerName", "name": "CustomerName", "autoWidth": true },
        {
            "render": function (data, type, row, meta) {
                return `<span class="Status-${row.Status}">${row.Status}</span>`;
            }
        },
        {
            "render": function (data, type, row, meta) {
                return `${row.Total}`
            }
        },
        {
            "render": function (data, type, row, meta) {
                const encodedTrackingId = encodeURIComponent(row.TrackingId);
                return `<a href="/Bookings/Details?id=${encodedTrackingId}" target="_blank" class="text-primary btn-icon-text" data-bs-toggle="tooltip" data-bs-placement="top" title="View Details">
                                                        <i class="btn-icon-prepend fa fa-eye"></i>
                                                    </a>`
            }
        }
    ]
});


table.on('click', 'td.dt-control', function (e) {
    let tr = e.target.closest('tr');
    let row = table.row(tr);
    if (row.child.isShown()) {
        destroyChild(row);
    }
    else {
        createChild(row);
    }
});

function createChild(row) {
    var table = $(`
                <table style="font-size:12px" class="datatables-products table">
                    <thead class="border-top bg-secondary">
                        <tr>
                            <th style="font-size:12px !important;color:white !important">Celebrity</th>
                            <th style="font-size:12px !important;color:white !important">TrackingId</th>
                            <th style="font-size:12px !important;color:white !important">Schedule</th>
                            <th style="font-size:12px !important;color:white !important">Company/Person</th>
                            <th style="font-size:12px !important;color:white !important">Manager Phone</th>
                            <th style="font-size:12px !important;color:white !important;width:10%">Status</th>
                            <th style="font-size:12px !important;color:white !important">Total</th>
                            <th style="font-size:12px !important;color:white !important">Action</th>
                        </tr>
                    </thead>
                </table>`);

    if (row.child.isShown()) {
        row.child.hide();
        return;
    }

    row.child(table).show();
    const bookingId = row.data().BookingId;
    var adTable = table.DataTable({
        "processing": true,
        "serverSide": true,
        "filter": false,
        "ordering": false,
        "lengthChange": false,
        "ajax": {
            "url": "/CelebrityAdvertisements/GetAdvertismentsByBooking",
            "type": "GET",
            "data": { bookingId: bookingId },
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return `
                <div style="display: flex; align-items: center; gap: 10px;">
                    <img
                        class="img-lg rounded-circle"
                        src="${row.CelebrityImage}"
                        alt=""
                        id="profileImage"
                        style="width: 35px; height: 35px; object-fit: cover; border: 1px solid #ccc;">
                    <span>${row.CelebrityName}</span>
                </div>`;
                }
            },
            { "data": "TrackingId", "name": "TrackingId", "autoWidth": true },
            {
                data: "datetime",
                render: function (data, type, row, meta) {
                    const adDate = formatDateToYyyyMmDd(row.AdDate);
                    return `${adDate} (${row.Schedule})`;
                }
            },
            { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
            { "data": "ManagerPhone", "name": "ManagerPhone", "autoWidth": true },
            {
                "render": function (data, type, row, meta) {
                    return `<span class="Status-${row.Status}">${row.Status}</span>`;
                }
            },
            {
                "render": function (data, type, row, meta) {
                    return `${row.AdPrice} KWD`;
                }
            },
            {
                "render": function (data, type, row, meta) {
                    let returnValue = `
                                <a
                                    data-trackingId ="${row.TrackingId}"
                                    class="text-primary btn-icon-text viewAdDetail"
                                    data-bs-toggle="tooltip"
                                    data-bs-placement="top"
                                    title="View Details">
                                    <i class="btn-icon-prepend fa fa-eye"></i>
                                </a>`;
                    if (row.Status === "Pending") {
                        returnValue += `
                                    | <a
                                        data-trackingId="${row.TrackingId}"
                                        class="text-danger btn-icon-text cancelAd"
                                        data-bs-toggle="tooltip"
                                        data-bs-placement="top"
                                        title="Cancel">
                                        <i class="btn-icon-prepend fa fa-ban"></i>
                                    </a>`;
                    }
                    return returnValue;
                }
            }
        ]
    });


    table.on('draw.dt', function () {
        $('[data-bs-toggle="tooltip"]').tooltip();
    });
}

$(document).on("click", ".viewAdDetail", function () {
    const trackingId = $(this).attr("data-trackingId");
    const encodedTrackingId = encodeURIComponent(trackingId);

    $.ajax({
        url: `/CelebrityAdvertisements/GetAdvertisementDetailView?trackingId=${encodedTrackingId}`,
        type: "Get",
        success: function (response) {
            if (response.Status == "Success") {
                $("#AdViewModalContent").html(response.Data);
                $("#AdViewModal").modal("show");
            }
            else {
                InfoToast(response.Errors.join("\n"))
            }

        },
        error: function (response) {
            console.log(response)
            handleAjaxError(response)
            unblockwindow()

        },
        complete: function () {

            unblockwindow();
        }
    })

})

function destroyChild(row) {
    var table = $("table", row.child());
    table.detach();
    table.DataTable().destroy();
    row.child.hide();
}



$(document).on("click", ".cancelAd", function () {
    const title = "Do you really want to cancel this ad?";
    const trackingId = $(this).attr("data-trackingId");
    ShowDialogWithTextInput("Cancel Advertisement", title, "warning").then((result) => {
        if (result.isConfirmed) {
            if (!result.value) {
                InfoToast("Comment is required ");
                return false;
            }
            blockwindow();
            $.ajax({
                type: "Post",
                url: `/CelebrityAdvertisements/CancelAdvertisementFromAdmin`,
                data: { trackingId: trackingId, comment: result.value },
                success: function (response) {
                    if (response.Status == "Success") {
                        SuccessToast(response.Message);
                    }
                    else {
                        InfoToast(response.Errors.join("\n"))
                    }
                    unblockwindow()
                    table.draw();
                },
                error: function (response) {
                    unblockwindow();
                    handleAjaxError(response)
                },
            });
        } else {
        }
    });
    return false;
})