$(document).on('click', ".show-more-text", function () {
    $(this).prev('span').toggle();
    $(this).text(function (_, text) {
        return text === "Show more" ? "Show less" : "Show more";
    });
});
function updateNotificationBadge() {
    $.ajax({
        url: '/ContactUs/GetNotificationCount',
        method: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.Status === "Success") {
                $("#notification-count").text(response.Data);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching notification count:", error);

        }
    });
}


$(".notificationButton").on("click", function () {

    if (!$("#notificationsList").hasClass("loaded")) {
        $.ajax({
            url: '/ContactUs/GetNotification',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                $("#notificationsList").html(data.Data);
                $("#notificationsList").addClass("loaded");
            },
            error: function (xhr, status, error) {
                console.error("Error fetching notifications:", error);
            }
        });
    }
});

$(document).on("click", ".mark-contact-as-read", function (e) {
    e.preventDefault();
    const elm = $(this);
    const id = $(this).attr("data-itemid");
    $.ajax({
        url: '/ContactUs/MarkAsRead?id=' + id,
        type: "Patch",
        success: function (data) {
            if (data.Status == "Success") {
                const notificationList = elm.closest(".notification-list");
                const infoBadge = notificationList.find(".info-badge");
                notificationList.removeClass("notification-list--unread");
                notificationList.addClass("notification-list--read");
                infoBadge.text("read");
                infoBadge.removeClass("bg-danger");
                infoBadge.addClass("bg-success");
                $("#notification-count").text(data.Data);
                elm.remove();
                SuccessToast("Marked as read");
            }
        },
        error: function (errRespons) {
            console.log(errRespons)
        }
    })
})

updateNotificationBadge();

