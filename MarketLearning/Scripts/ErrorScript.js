function LogErrorScript(Message, Location) {
    return $.post("/Error/LogErrorClient", {
        message: Message,
        location: Location
    });
}

$('#errorSearch').on('change input', function () {
    search("errorTable", $('#errorSearch').val());
    searchDate();
});

$('#startSearch').click(function () {
    search("errorTable", $('#errorSearch').val());
    searchDate();
});

function searchDate() {
    try {
        var string1 = $('#startDate').val();
        var string2 = $('#endDate').val();
        if (string1 != "") {
            var array1 = string1.split('-');
            string1 = array1[1] + "/" + array1[2] + "/" + array1[0] + " 12:00:00 AM";
        }
        if (string2 != "") {
            var array2 = string2.split('-');
            string2 = array2[1] + "/" + array2[2] + "/" + array2[0] + " 12:00:00 AM";
        }
        searchDateRange("errorTable", string1, string2);
        hasAnyInList();
    }
    catch (err) {
        LogErrorScript(err.message, "ErrorScript.searchDate")
    }
}

function hasAnyInList() {
    if ($("#errorTable tr").length < 2 || $("#errorTable tr td:visible").length < 1) {
        $('#NoRecordPlaceHolder').html("No error record");
    }
    else {
        $('#NoRecordPlaceHolder').html("");
    }
}

$(document).ready(function () {
    hasAnyInList();
});

$('#showtest').click(function () {
    $('#errorTable > tbody > tr').show();
    $('#startDate').val(this.defaultValue);
    $('#endDate').val(this.defaultValue);
    $('#errorSearch').val(this.defaultValue);
    hasAnyInList();
});
$('#startDate').on('change', function () {
    if ($('#startDate').val() == "") {
        search("errorTable", $('#errorSearch').val());
        searchDate();
    }
});
$('#endDate').on('change', function () {
    if ($('#endDate').val() == "") {
        search("errorTable", $('#errorSearch').val());
        searchDate();
    }
});

