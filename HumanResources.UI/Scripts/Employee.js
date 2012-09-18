$(document).ready(function () {
    var employeeId = $("#EmployeeId").val();

    if (employeeId == '') $("#BookHoliday").hide();

    $('#calendar').fullCalendar({
        year: 2012,
        month: 7,
        editable: true,
        events: "/Employee/CalendarData?employeeId=" + employeeId,
        eventClick: function (event) {
            if (event.isHoliday == true) {
                window.location = "/Holiday/BookUpdate?updating=true&employeeId=" + event.employeeId +
                    "&holidayId=" + event.id +
                    "&start=" + event.isoStart +
                    "&end=" + event.isoEnd;
            } else {
                alert('Only editing of holidays is allowed.');
            }
        }
    });

    $("#EmployeeId").change(function (e) {
        e.preventDefault();
        var newEmployeeId = $(this).val();
        window.location = "/Employee/Index?employeeId=" + newEmployeeId;
    });
});
