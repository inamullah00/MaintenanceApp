
function validateTimes() {
    var fromTime = $('#FromTime').val();
    var toTime = $('#ToTime').val();

    $('#FromTime').siblings('.text-danger').text('');
    $('#ToTime').siblings('.text-danger').text('');

    if (!fromTime || !toTime) {
        return;
    }

    var from = new Date("1970-01-01 " + fromTime);
    var to = new Date("1970-01-01 " + toTime);

    if (from >= to) {
        $('#FromTime').siblings('.text-danger').text('From Time must be earlier than To Time.');
        return;
    }

    var diffInHours = (to - from) / (1000 * 60 * 60);
    const maxDurationPerSchedule = $("#maxDurationPerSchedule").val();
    if (!maxDurationPerSchedule) {
        InfoToast("Schedule setup is incomplete.");
        return false;
    }
    debugger;
    if (diffInHours > parseInt(maxDurationPerSchedule)) {
        $('#FromTime').siblings('.text-danger').text(`Time difference cannot exceed ${maxDurationPerSchedule} hours.`);
    }
}

$('#FromTime, #ToTime').on('change', function () {
    validateTimes();
});




