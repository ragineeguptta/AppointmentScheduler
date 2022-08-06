var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#appointmentDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });

    InitializeCalendar();
});
var calendar;
function InitializeCalendar() {
    try {


        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                }
            });
            calendar.render();
        }

    }
    catch (e) {
        alert(e);
    }

}


function onShowModal(obj, isEventDetail) {
    $("#appointmentInput").modal("show");
}


function onCloseModal() {
    $("#appointmentInput").modal("hide")
}

function onSubmitForm() {
    if (checkValidation) {
    var requestData = {
        Id: parseInt($("#id").val()),
        Title: $("#title").val(),
        Description: $("#description").val(),
        StartDate: $("#appointmentDate").val(),
        Duration: $("#duration").val(),
        DoctorId: $("#doctorId").val(),
        PatientId: $("#patientId").val(),
    };

    $.ajax({
        url: routeURL + '/api/Appointment/SaveCalenderData',
        type: 'POST',
        data: JSON.stringify(requestData),
        contentType: 'application/json',
        success: function (response) {
            if (response.status === 1 || response.status === 2) {
                $.notify(response.message, "success");
            }
            else {
                $.notify(response.message, "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
    }
}

function checkValidation() {
    var isValid = true;
    if ($("#title").val() === undefined || $("#title").val() === "") {
        isValid = false;
        $("#title").addClass('error');
    }
    else {
        $("#title").addClass('error');
    }

    if ($("#appointmentDate").val() === undefined || $("#appointmentDate").val() === "") {
        isValid = false;
        $("#appointmentDate").addClass('error');
    }
    else {
        $("#appointmentDate").addClass('error');
    }

    return isValid;
}