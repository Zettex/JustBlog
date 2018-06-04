$(function () {
      
    var frm = $('#main form');

    var msg = $("input[name='Message']", frm).val();
    var redirectUrl = $("input[name='RedirectUrl']", frm).val();

    if (msg) {
        notify(msg, "success");
    }

    setTimeout(function () {

        if (redirectUrl) {
            window.location.href = redirectUrl
        }

    }, 600);
});