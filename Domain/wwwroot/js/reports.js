
$(document).ready(function () {
    $("#fromDate").val(new Date(new Date().getFullYear(), new Date().getMonth(), 1).toISOString().slice(0, 10));
    $('#toDate').val(new Date().toDateInputValue());
});​