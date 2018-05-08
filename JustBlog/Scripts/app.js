
$(function () {

    $('#search-form').submit(function () {
        return !($("#s").val() == "" || $("#s").val() == "Search here...");
    });

    var frm = $("form#avatar-upload");

    $("#File").change(function() {
        frm.submit();
    });

    //frm.submit(function () {
    //    var $input = $("#File", frm);
    //    var fd = new FormData;
    //    var token = $('input[name="__RequestVerificationToken"]', frm).val();

    //    fd.append('img', $input.prop('files')[0]);

    //    $.ajax({
    //        url: frm.attr('action'),
    //        type: 'POST',
    //        dataType: "json",
    //        traditional: true,
    //        dataType: "json",
    //        data: {
    //            "__RequestVerificationToken" : token,
    //            "File" : fd 
    //        },
    //        success: function (data) {
    //            alert(data);
    //        }
    //    });
    //    return false;
    //});


});
