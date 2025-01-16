$("#save-btn").on("click", function (e) {
    e.preventDefault();
    if ($(".advertisement-div").length == 0) {
        InfoToast("No advertisement added.")
        return false;
    }
    $(".accordion .collapse").each(function () {
        const $accordionItem = $(this);
        if (!$accordionItem.hasClass("show")) {
            $accordionItem.addClass("show");
            $accordionItem.parent().find(".btn-collapse").attr("aria-expanded", true);
        }
    });

    var $form = $("#bookingForm");
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form)
    $form.validate();
    let isValid = $form.valid();

    if (!isValid) {
        InfoToast("Validation Error")
        return false;
    }

    let bookingData = new FormData();
    bookingData.append("CustomerId", $('#CustomerId').val());
    bookingData.append("TotalAmount", $('#totalAmount').val());

    $('.advertisement-div').each(function (index, element) {
        const advertisement = {
            CelebrityId: $(element).find('.CelebrityId').val(),
            AdPrice: $(element).find('.AdPrice').val(),
            AdDate: $(element).find('.AdDate').val(),
            CelebrityScheduleId: $(element).find('.CelebrityScheduleId').val(),
            CompanyTypeId: $(element).find('.CompanyTypeId').val(),
            CountryId: $(element).find('.CountryId').val(),
            CompanyName: $(element).find('.CompanyOrContactPersonName').val(),
            ManagerPhone: $(element).find('.ManagerPhone').val(),
            DeliveryType: $(element).find('.DeliveryType').val(),
            Questions: getCelebrityAdQuestionDatas($(element)),
            Attachments : []
        };

        Object.keys(advertisement).forEach(key => {
            if (key === 'Questions') {      
                advertisement.Questions.forEach((question, questionIndex) => {
                    Object.keys(question).forEach(questionKey => {
                        bookingData.append(
                            `Advertisements[${index}].Questions[${questionIndex}].${questionKey}`,
                            question[questionKey]
                        );
                    });
                });
            } else if (key === 'Attachments') {
              
                const fileInput = $(element).find(".Attachments")[0]; 
                if (fileInput && fileInput.files.length > 0) {
                    Array.from(fileInput.files).forEach((file, fileIndex) => {
                        bookingData.append(`Advertisements[${index}].Attachments[${fileIndex}]`, file);
                    });
                }
            } else {
                bookingData.append(`Advertisements[${index}].${key}`, advertisement[key]);
            }
        });

   

    });

    ShowDialog("Create Booking", "Are you sure to create this booking?", "warning").then((result) => {
        if (result.isConfirmed) {
            blockwindow();
            $.ajax({
                url: `/Bookings/Create`,
                type: "POST",
                processData: false, 
                contentType: false,
                data: bookingData,
                success: function (response) {
                    if (response.Status == "Success") {
                        SuccessToast("Created Successfully");
                        setTimeout(function () {
                            window.location.href = "/Bookings/Index";
                        }, 1000);
                    } else {
                        InfoToast(response.Errors.join("\n"));
                        unblockwindow();
                    }
                },
                error: function (response) {
                    handleAjaxError(response);
                    unblockwindow();
                },
            });
        } else {
          
        }
    });

})
function AddAdvertisementView() {

    const maxAdCount = $("#maxAdCount").val();
    if (!maxAdCount) {
        InfoToast("Booking setup is incomplete.");
        return false;
    }
    const existingAds = $('.advertisement-div').length;
    if (existingAds >= parseInt(maxAdCount)) {
        InfoToast(`You cannot add more than ${maxAdCount} ads per booking.`);
        return false;
    }
    const customerId = $("#CustomerId").val();
    if (!customerId) {
        InfoToast("Please select a customer.");
        return false;
    }
    var $form = $("#SelectCelerbityForm");
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form)
    $form.validate();
    let isValid = $form.valid();

    if (!isValid) {

        return false;
    }
    blockwindowformodal();
    const celebrityId = $("#SelectedCelebrityId").val();
    const index = $(".advertisement-div").length;
    $.ajax({
        url: `/Bookings/GetAdvertisementPartialView?celebrityId=${celebrityId}&index=${index}`,
        type: "Get",
        success: function (response) {
            if (response.Status == "Success") {
                $("#adAccordian").append(response.Data);
                $("#CelebrityModal").modal("hide");
                $("#SelectedCelebrityId").val("").trigger("change");
                $("#summary-div").removeClass("hidden");
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
            InitializeComponents();
            unblockwindow();
        }
    })
}
function getCelebrityAdQuestionDatas(elm) {
    const questionDatas = [];
    elm.find(".question").each(function () {
        const questionDiv = $(this);
        const questionId = parseInt(questionDiv.find(".QuestionId").val());
        const questionData = {
            QuestionId: questionId,
            TextAnswer: null,
            SelectedOptionId: null,
            DateAnswer: null,
            NumberAnswer: null
        };
        if (questionDiv.find(".TextAnswer").length > 0) {
            questionData.TextAnswer = questionDiv.find(".TextAnswer").val();
        } else if (questionDiv.find(".SelectedOptionId").length > 0) {
            questionData.SelectedOptionId = parseInt(questionDiv.find(".SelectedOptionId").val(), 10);
        } else if (questionDiv.find(".DateAnswer").length > 0) {
            questionData.DateAnswer = questionDiv.find(".DateAnswer").val();
        } else if (questionDiv.find(".NumberAnswer").length > 0) {
            questionData.NumberAnswer = parseFloat(questionDiv.find(".NumberAnswer").val());
        }
        questionDatas.push(questionData);
    });

    return questionDatas;
}


$(document).on("click", ".remove-advertisement", function () {

    ShowDialog("Remove Ad", "Do you really want to remove this ad?", "warning").then((result) => {
        if (result.isConfirmed) {
            $(this).closest(".accordion-item").remove();
            const totalAds = $(".accordion-item").length;
            if (totalAds <= 0) {
                $("#summary-div").addClass("hidden");
            }
            SuccessToast("Removed Successfully");
        } else {
        }
    });
    return false;
})



function InitializeComponents() {
    const latestAccordion = $("#adAccordian").children().last();
    $('.select2Input').each(function () {
        if (!$(this).hasClass('select2-hidden-accessible')) {
            $(this).select2({
                placeholder: "Select",
                allowClear: true,
                width: '100%',
                matcher:matchCustom
            });
        }
    });

    $('.select-with-flags').each(function () {
        if (!$(this).hasClass('select2-hidden-accessible')) {
            $(this).select2({
                templateResult: formatCountryOption,
                templateSelection: formatCountryOption,
                matcher: matchCustom
            });
        }
    });

    latestAccordion.find('.AdDatePicker').each(function () {
        const inputElement = $(this);
        inputElement.flatpickr({
            dateFormat: "Y-m-d",
            onOpen: function (selectedDates, dateStr, instance) {
                fetchAvailability(instance, inputElement);
            },
            onMonthChange: function (selectedDates, dateStr, instance) {
                fetchAvailability(instance, inputElement);
            },
            onYearChange: function (selectedDates, dateStr, instance) {
                fetchAvailability(instance, inputElement);
            }
        });
    });

}


function fetchAvailability(instance, inputElement) {

    const currentYear = instance.currentYear;
    const currentMonth = instance.currentMonth + 1; // because month starts from 0 in flatpicker
    const celebrityId = inputElement.closest(".accordion-item").find(".CelebrityId").val();
    $.ajax({
        url: '/CelebritySchedule/GetScheduleAvailableForMonth',
        method: 'GET',
        data: { year: currentYear, month: currentMonth, celebrityId: celebrityId },
        success: function (response) {

            const datesWithStatus = response.Data || [];

            $(instance.calendarContainer).find(".flatpickr-day").removeClass("available-date unavailable-date");
            datesWithStatus.forEach(item => {
                const date = new Date(item.Date);
                const ariaLabel = date.toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
                const dateElement = instance.calendarContainer.querySelector(`[aria-label="${ariaLabel}"]`);

                if (dateElement) {
                    if (item.Status === "Available") {
                        dateElement.classList.add("available-date");
                    } else if (item.Status === "Unavailable") {
                        dateElement.classList.add("unavailable-date");
                    }
                }
            });
        },
        error: function () {
            alert("Failed to fetch date availability. Please try again.");
        }
    });
}

//Validation
$(document).on("change", ".CelebrityScheduleId", function () {
    const selectedScheduleId = $(this).val();
    if (!selectedScheduleId) return;
    let scheduleIds = []
    let isDuplicate = false;
    $(".CelebrityScheduleId").each(function () {
        const scheduleId = $(this).val();
        if (scheduleIds.includes(scheduleId)) {
            isDuplicate = true;
            return;
        } else {
            scheduleIds.push(scheduleId);
        }

    });

    if (isDuplicate) {
        InfoToast("This schedule has already been selected.");
        $(this).val("").trigger("change");
    }
});


$(document).on("change", ".AdDatePicker", function () {
    const date = $(this).val();
    const celebrityId = $(this).closest(".accordion-item").find(".CelebrityId").val();
    const scheduleDropDownElm = $(this).closest(".accordion-item").find(".CelebrityScheduleId");
    blockwindow()
    $.ajax({
        url: `/CelebritySchedule/GetSchedulesByCelebrityId?celebrityId=${celebrityId}&date=${date}`,
        type: "Get",
        success: function (response) {
            if (response.Status == "Success") {
                scheduleDropDownElm.empty();
                scheduleDropDownElm.append('<option value="" disabled selected>--select--</option>');

                response.Data.forEach(item => {
                    scheduleDropDownElm.append(`<option value="${item.Id}">${item.FormattedSchedule}</option>`);
                });
            }
            else {
                InfoToast(response.Errors.join("\n"))
            }
            unblockwindow()
        },
        error: function (response) {

            handleAjaxError(response)
            unblockwindow()

        }
    })
})
$(document).on("change", ".DeliveryType", function () {
    const deliveryType = $(this).val();
    const celebrityId = $(this).closest(".accordion-item").find(".CelebrityId").val();
    const questionContainerElm = $(this).closest(".accordion-item").find(".questionsContainer");
    const adPriceElm = $(this).closest(".accordion-item").find(".AdPrice");
    const index = $(this).closest(".accordion-item").find(".index").val();
    const attachmentContainerElm = $(this).closest(".accordion-item").find(".AttachmentContainer");

    blockwindow()
    $.ajax({
        url: `/Question/GetByDeliveryType?celebrityId=${celebrityId}&deliveryType=${deliveryType}&index=${index}`,
        type: "Get",
        success: function (response) {
            if (response.Status == "Success") {
                questionContainerElm.html(response.Data.QuestionView);
                adPriceElm.val(response.Data.AdPrice);
                initializePicker(".datetime-picker")
                calculateTotalAmount();
                if (deliveryType == "Post") {
                    attachmentContainerElm.removeClass("hidden")
                }
                else {
                    attachmentContainerElm.addClass("hidden")
                    attachmentContainerElm.closest(".AttachmentContainer").find(".image-preview").empty();
                    attachmentContainerElm.closest(".AttachmentContainer").find(".Attachments").val("");
                }
            }
            else {
                InfoToast(response.Errors.join("\n"))
            }
            unblockwindow()
        },
        error: function (response) {

            handleAjaxError(response)
            unblockwindow()

        }
    })
})

//attachment handler

$(document).on("change",".Attachments", function () {
    const files = Array.from(this.files);
    const maxAttachmentCount = $("#maxAttachmentCount").val();
    if (files.length > parseInt(maxAttachmentCount)) {
        InfoToast(`You cannot upload more than ${maxAttachmentCount} post per advertisement.`);
        $(this).val("");
        $(".post-image").each(function () {
            $(this).remove();
        })
        return false;
    }
    const imagePreviewElm = $(this).closest(".AttachmentContainer").find(".image-preview");
    imagePreviewElm.empty();

    files.forEach((file) => {
        const reader = new FileReader();
        reader.onload = function (event) {
            imagePreviewElm.append(`
                            <div class="col-md-2 mt-2 position-relative post-image">
                                <img src="${event.target.result}" class="img-thumbnail">
                                <span class="btn btn-danger btn-sm position-absolute top-0 end-0 remove-image" style="cursor: pointer;">&times;</span>
                            </div>
                        `);
        };
        reader.readAsDataURL(file); 
    });
});


$(document).on("click", ".remove-image", function () {
    const index = $(this).parent().index(); 
    const fileInput = $(this).closest(".AttachmentContainer").find(".Attachments")[0];
    const dataTransfer = new DataTransfer(); 

    Array.from(fileInput.files).forEach((file, i) => {
        if (i !== index) {
            dataTransfer.items.add(file);
        }
    });

    fileInput.files = dataTransfer.files;
    $(this).parent().remove(); 
});



function calculateTotalAmount() {
    let total = 0;
    $('.AdPrice').each(function () {
        total += parseFloat($(this).val()) || 0;
    });
    $('#totalAmount').val(total.toFixed(3));
}