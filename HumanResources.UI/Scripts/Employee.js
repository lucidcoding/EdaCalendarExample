$(document).ready(function () {
    var employeeId = $("#EmployeeId").val();

    if (employeeId == '') $("#BookHoliday").hide();

    $('#calendar').fullCalendar({
        year: 2012,
        month: 7,
        editable: true,
        events: "/Employee/CalendarData?employeeId=" + employeeId
    });

    $("#EmployeeId").change(function (e) {
        e.preventDefault();
        var newEmployeeId = $(this).val();
        window.location = "/Employee/Index?employeeId=" + newEmployeeId;
    });
});
