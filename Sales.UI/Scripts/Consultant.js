$(document).ready(function () {
    var consultantId = $("#ConsultantId").val();
    if (consultantId == '') $("#BookAppointment").hide();

    $('#calendar').fullCalendar({
        year: 2012,
        month: 7,
        editable: true,
        events: "/Consultant/CalendarData?consultantId=" + consultantId
    });

    $("#ConsultantId").change(function (e) {
        e.preventDefault();
        var newConsultantId = $(this).val();
        window.location = "/Consultant/Index?consultantId=" + newConsultantId;
    });
});
