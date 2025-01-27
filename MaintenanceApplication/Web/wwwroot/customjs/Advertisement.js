$(document).on("click", ".cancel-advertisement", function () {
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
                    window.location.reload();
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
