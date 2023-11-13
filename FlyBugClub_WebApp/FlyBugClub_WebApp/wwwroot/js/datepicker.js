$(function () {
    $('input[name="daterange"]').daterangepicker({
        opens: 'left'
    }, function (start, end, label) {
        const inputDateRange = document.getElementById('cuong');
        var result = start.format('YYYY-MM-DD') + "@@" + end.format('YYYY-MM-DD');
        inputDateRange.classList.add(result);
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));

    });
});