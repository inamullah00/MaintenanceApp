var table = $('#usersTable').DataTable({
    "processing": true,
    "serverSide": true,
    "filter": false,
    "ordering": false,
    "ajax": {
        "url": "/Users/GetFilteredUsers",
        "type": "POST",
        "datatype": "json",
        "data": function (d) {
            d.UserId = $('#UserId').val();
        }
    },
    columns: [
        { data: 'FirstName', name: 'FirstName', "autoWidth": true },
        { data: 'LastName', name: 'LastName', "autoWidth": true },
        { data: 'EmailAddress', name: 'Email', "autoWidth": true },
        { data: 'PhoneNumber', name: 'Mobile', "autoWidth": true },
        {
            data: 'Status',
            render: function () {
                return `<span class="badge bg-success">Approved</span>`;
            },
            "autoWidth": true
        },
        {
            data: 'Role',
            render: function (data) {
                return `<span class="badge bg-primary">${data}</span>`;
            },
            "autoWidth": true
        },
        {
            data: 'Id',
            render: function (data, type, row) {
                const isBlocked = row.IsBlocked;

                return `
                <div class="text-center">
                    <a href="/Users/Edit/${data}" class="text-primary btn-icon-text btn-xs" data-bs-toggle="tooltip" data-bs-placement="top" title="Update">
                        <i class="btn-icon-prepend fa fa-edit"></i>
                    </a>
                    ${isBlocked
                        ? `<a class="card-link unBlockUser" data-userId="${data}" data-bs-toggle="tooltip" data-bs-placement="top" title="Unblock">
                            <i class="fa fa-unlock"></i>
                        </a>`
                        : `<a class="card-link blockUser text-danger" data-userId="${data}" data-bs-toggle="tooltip" data-bs-placement="top" title="Block">
                            <i class="fa fa-lock"></i>
                        </a>`}
                    <a data-userId="${data}" class="text-primary btn-icon-text btn-xs reset-password" data-bs-toggle="tooltip" data-bs-placement="top" title="Reset Password">
                        <i class="btn-icon-prepend fa fa-sync-alt"></i>
                    </a>
                </div>`;
            },
            "autoWidth": true,
            "className": "text-center"
        }

    ]
});


$(document).on("click", ".reset-password", function () {
    $("#UserId").val($(this).attr("data-userId"));
    $("#resetPasswordModel").modal("show");
})

$(document).on("submit", "#resetPasswordForm", function (e) {
    e.preventDefault();
    var $form = $(this);
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form);
    $form.validate();
    let isValid = $form.valid();
    if (!isValid) {
        return false;
    }
    const data = {
        UserId: $("#UserId").val(),
        NewPassword: $("#NewPassword").val(),
        ConfirmPassword: $("#ConfirmPassword").val()
    }
    blockwindow();
    $.ajax({
        url: `/Users/ResetPassword`,
        type: "post",
        data: data,
        success: function (result) {
            if (result.Status == "Success") {
                SuccessToast("Password Reset Successfull");
                $("#resetPasswordModel").modal("show");
                window.location.reload();
            }
            else {
                InfoToast(result.Errors.join("\n"))
            }
            unblockwindow()
        },
        error: function (response) {
            unblockwindow()
            handleAjaxError(response)

        }
    })

})
$(document).on("click", ".blockUser", function () {
    const title = "Do you really want to block this user?";
    const userId = $(this).attr("data-userId");
    ShowDialog("BlockUser", title, "warning").then((result) => {
        if (result.isConfirmed) {
            blockwindow();
            $.ajax({
                type: "Patch",
                url: "/Users/BlockUser?id=" + userId,
                success: function (response) {
                    if (response.Status == "Success") {
                        SuccessToast(response.Message);
                        window.location.reload();
                    }
                    else {
                        InfoToast(response.Errors.join("\n"))
                    }
                    unblockwindow()
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


$(document).on("click", ".unBlockUser", function () {
    const title = "Do you really want to unblock this user?";
    const userId = $(this).attr("data-userId");
    ShowDialog("UnBlockUser", title, "warning").then((result) => {
        if (result.isConfirmed) {
            blockwindow();
            $.ajax({
                type: "Patch",
                url: "/Users/UnBlockUser?id=" + userId,
                success: function (response) {
                    if (response.Status == "Success") {
                        SuccessToast(response.Message);
                        window.location.reload();
                    }
                    else {
                        InfoToast(response.Errors.join("\n"))
                    }
                    unblockwindow()

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

