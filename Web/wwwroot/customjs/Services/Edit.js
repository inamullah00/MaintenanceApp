$(document).ready(function () {
    $(document).on("click", "#approve-service", function () {
        const title = "Do you really want to approve?";
        const freelancerId = $(this).attr("data-id");

        ShowDialog("Approve Service", title, "warning").then((result) => {
            if (result.isConfirmed) {
                blockwindow();
                $.ajax({
                    type: "PATCH",
                    url: "/Service/Approve?id=" + freelancerId,
                    success: function (response) {
                        if (response.Status === "Success") {
                            SuccessToast(response.Message);
                        } else {
                            InfoToast(response.Errors.join("\n"));
                        }
                        unblockwindow();
                        window.location.href = "/Service";
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