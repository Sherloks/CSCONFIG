//var isCustomView = false;
//var fieldsWrapper = $('#input-field-wrapper');

//function onTypeChanged()
//{


//    buildInputsForView();
//}

//function buildInputsForView()
//{
//    var categoryValue = $('#Attribute_AttributeTypeId')[0].selectedIndex;
//    var categoryText = $('#Attribute_AttributeTypeId')[0].options[categoryValue].text

//    switch(categoryText) {
//        case "boolean":
//            buildBooleanInputs();
//            break;
//        case "number":
//            buildNumberInputs();
//            break;
//        case "string":
//            buildSelectionInputs();
//            break;
//        default:
//            break;
//    }
//}

//function buildNumberInputs()
//{
//    var controls = $(fieldsWrapper).find('[class*="-input-control"]');

//    for (i = 0; i < controls.length; i++)
//    {
//        if ($(controls[i]).hasClass('number-input-control'))
//        {
//            $(controls[i]).removeClass('hidden');
//        }
//        else
//        {
//            $(controls[i]).addClass('hidden');
//        }
//    }
//}

//function buildBooleanInputs()
//{
//    var controls = $(fieldsWrapper).find('[class*="-input-control"]');

//    for (i = 0; i < controls.length; i++) {
//        if ($(controls[i]).hasClass('boolean-input-control')) {
//            $(controls[i]).removeClass('hidden');
//        }
//        else {
//            $(controls[i]).addClass('hidden');
//        }
//    }
//}

//function buildSelectionInputs()
//{
//    function buildBooleanInputs() {
//        var controls = $(fieldsWrapper).find('[class*="-input-control"]');

//        for (i = 0; i < controls.length; i++) {
//            if ($(controls[i]).hasClass('selection-input-control')) {
//                $(controls[i]).removeClass('hidden');
//            }
//            else {
//                $(controls[i]).addClass('hidden');
//            }
//        }
//    }

//}

//$('#Attribute_AttributeTypeId').change(function () {
//    buildInputsForView();
//});

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

$(function () {
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
