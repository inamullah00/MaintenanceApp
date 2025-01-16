var table = $("#AdvertisementTable").DataTable({
    "processing": true,
    "serverSide": true,
    "filter": false,
    "ordering": false,
    "ajax": {
        "url": "/Advertisements/GetFilteredAdvertisements",
        "type": "POST",
        "datatype": "json",
        "data": function (d) {
            d.FromDate = $('#FromDate').val();
            d.ToDate = $('#ToDate').val();
            d.CustomerId = $('#CustomerId').val();
            d.CelebrityId = $('#CelebrityId').val();
            d.Status = $('#Status').val();
        }
    },
    "columns": [
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
        { "data": "CelebrityName", "name": "CelebrityName", "autoWidth": true },
        {
            "render": function (data, type, row, meta) {
                return `<span class="Status-${row.Status}">${row.Status}</span>`;
            }
        },
        {
            "render": function (data, type, row, meta) {
                return `${row.Price}`
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