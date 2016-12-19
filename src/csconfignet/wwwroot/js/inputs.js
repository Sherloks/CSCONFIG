//var originalVal;


//function slideStartEvent(event) {
//    originalVal = $('#volume-slider').val();
//}

//function slideStopEvent(event) {

//    var newVal = $('#volume-slider').val();

//    if (originalVal != newVal) {
//        $('#volume-input').val(newValue);
//    }
//}

//$('#volume-slider').on('update', slideStartEvent);

//$('#volume-slider').on('update', slideStopEvent);

$(function() {
    $('[data-toggle="tooltip"]').tooltip();
});

var sliders = $("[id$=-slider]");

for (i = 0; i < sliders.length; i++) {

    $("#" + sliders[i].id).on("change", function () {
        var name = this.id.substring(0, this.id.length - 7);
        $("#" + name + "-input").val(this.value);

        $("#" + name + "-input").on("change", function () {
            var name = this.id.substring(0, this.id.length - 6);
            $("#" + name + "-slider").val(this.value);
        });
    });
}

//$("#volume-slider").on("change", function() {
//    $('#volume-input').val(this.value);
//});

//$("#volume-input").on("change", function () {
//    $('#volume-slider').val(this.value);
//});
