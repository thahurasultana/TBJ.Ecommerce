$(document).ready(function () {
    $("#eye").mousedown(function () {
    
        $("#Password").attr("type", "text")
    });
    $("#eye").on("mouseleave", function () {
        $("#Password").attr("type", "password")
    });
});

