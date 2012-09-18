$(document).ready(function () {
    var consultantId = $("#ConsultantId").val();
    if (consultantId == '') $("#BookAppointment").hide();

    $('#calendar').fullCalendar({
        year: 2012,
        month: 7,
        editable: true,
        events: "/Consultant/CalendarData?consultantId=" + consultantId,
        eventClick: function (event) {
            if (event.isAppointment == true) {
                window.location = "/Appointment/BookUpdate?updating=true&consultantId=" + event.consultantId +
                    "&appointmentId=" + event.id +
                    "&start=" + event.isoStart +
                    "&end=" + event.isoEnd +
                    "&leadName=" + event.title +
                    "&address=" + event.address;
            } else {
                alert('Only editing of appointments is allowed.');
            }
        }
    });

    $("#ConsultantId").change(function (e) {
        e.preventDefault();
        var newConsultantId = $(this).val();
        window.location = "/Consultant/Index?consultantId=" + newConsultantId;
    });
});
