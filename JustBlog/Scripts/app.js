
//$(function () {
    
    // ----- Search handler -----------
    $('#search-form').submit(function () {
        return !($("#s").val() == "" || $("#s").val() == "Search here...");
    });
    // ----- End search handler -----------


    // ----- Comments handlers -----------
    var commentId = "0";
    var url = "/AddComment";
    var respond = $(".respond");
    var lastRespond = null;
    var postId = $(".entry-header").data("id");
    var ownerId = null;

    var form = $(".respond form");

    function replyHandler() {
        
        // Remove last added comment block and show new 
        if (lastRespond !== null) {
            $(lastRespond).remove();
        }

        url = "/AddComment";
        lastRespond = $(respond).clone();
        $(this).parents(".comment-controls").after(lastRespond);
        ownerId = $($(this).parents(".comment")[0]).data("id");
        commentId = "0";

        $("a", $($(this).parents(".comment-controls"))).each(function () {
            $(this).toggle();
        });

        $("form", lastRespond).submit(formHandler);
        
    }

    function editHandler() {

        if (lastRespond !== null) {
            $(lastRespond).remove();
        }

        url = "/EditComment";
        lastRespond = $(respond).clone();
        // set value to textarea from comment
        var commText = $(".comment-text p", $($(this).parents("li")[0]));
        $("form textarea", lastRespond).val(commText.html().trim());
        // add respond block
        $(this).parents(".comment-controls").after(lastRespond);
        
        commentId = $($(this).parents(".comment")[0]).data("id");
        $("button", lastRespond).html("Сохранить");

        $("a", $($(this).parents(".comment-controls"))).each(function () {
            $(this).toggle();
        });

        $("form", lastRespond).submit(formHandler);

    }

    function deleteHandler() {

        url = "/EditComment";
        commentId = $($(this).parents(".comment")[0]).data("id");

        callAjax(true);
    }

    function cancelControlsHandler() {

        $(lastRespond).remove();
        ownerId = null;
        $(form).submit(formHandler);

        $("a", $($(this).parents(".comment-controls"))).each(function () {
            $(this).toggle();
        });

    }

    // Form handler for post comment
    function formHandler(e) {
        e.preventDefault();
        callAjax();
    }

    function callAjax(deleted = false) {
        $.ajax({
            url: "/IsLoggedIn",
            method: "POST",
            success: function (data) {
                if (data.isLoggedIn) {
                    $.ajax({
                        url: url,
                        method: "POST",
                        data: {
                            Id: commentId,
                            DateSent: new Date().toUTCString(),
                            Content: $(".respond form textarea").val(),
                            Owner: ownerId,
                            User: -1,
                            Post: postId,
                            Deleted: deleted
                        },
                        success: function (data) {
                            $("#comments").html(data);
                            respond = $(".respond");
                            $(".respond form").submit(formHandler);
                            $(".cancel-controls").click(cancelControlsHandler);
                            $(".reply").click(replyHandler);
                            $(".edit").click(editHandler);
                            $(".delete").click(deleteHandler);

                            if (url === "/EditComment") {
                                notify("Комментарий был обновлен", "success");
                            }
                        }
                    });
                    ownerId = null;
                } else {
                    notify("Произошла ошибка при отправке сообщения", "error");
                }
            }
        });
    }

    $(form).submit(formHandler);
    $(".cancel-controls").click(cancelControlsHandler);
    $(".reply").click(replyHandler);
    $(".edit").click(editHandler);
    $(".delete").click(deleteHandler);

    // ----- End comment handler -----------
//});
