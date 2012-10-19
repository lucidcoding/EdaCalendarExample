$(document).ready(function () {

    var populateCalendar = function () {
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
    };
    
    var consultantId = $("#ConsultantId").val();
    if (consultantId == '') $("#BookAppointment").hide();
    populateCalendar();

    $("#ConsultantId").change(function (e) {
        e.preventDefault();
        consultantId = $("#ConsultantId").val();
        populateCalendar();
        $("#BookAppointment").show();
        $("#BookAppointment").attr("href", "/Appointment/BookUpdate?updating=False&consultantId=" + consultantId);
    });


    $("#ShowAddConsultantLink").click(function (e) {
        e.preventDefault();

        if ($('#AddConsultantDiv').css('display') == 'none') {
            $('#AddConsultantDiv').show('slow');
            $("#ShowAddConsultantLink").text('Cancel Add Consultant');
        }
        else {
            $('#AddConsultantDiv').hide('slow');
            $("#ShowAddConsultantLink").text('Add Consultant');
        }
    });

    $("#AddConsultantLink").click(function (e) {
        var forename = $("#Forename").val();
        var surname = $("#Surname").val();

        $.ajax({
            type: "POST",
            url: "/Consultant/Add",
            data: "forename=" + forename + "&surname=" + surname,
            error: function (error) {
                alert('an error occured: ' + error);
            }
        }).done(function (data) {
            $("#ConsultantId")
                        .append($('<option></option>')
                            .val(data.value)
                            .html(data.text)
                            .attr('selected', true));

            $('#AddConsultantDiv').hide('slow');
            $("#ShowAddConsultantLink").text('Add Consultant');

            $('#calendar').fullCalendar({
                year: 2012,
                month: 7,
                editable: true
            });

            consultantId = $("#ConsultantId").val();
            $("#BookAppointment").show();
            $("#BookAppointment").attr("href", "/Appointment/BookUpdate?updating=False&consultantId=" + consultantId);
        });
    });
});
