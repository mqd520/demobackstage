(function () {

    //$(document).ajaxStart(function (event) {
    //    console.log("ajaxStart --------------------------------------");
    //    console.log(event);
    //    console.log("                                                ");
    //});

    //$(document).ajaxSend(function (event, xhr, opts, err) {
    //    console.log("ajaxSend --------------------------------------");
    //    console.log(event);
    //    console.log(xhr);
    //    console.log(opts);
    //    console.log(err);
    //    console.log("                                                ");
    //});

    //$(document).ajaxSuccess(function (event, xhr, opts) {
    //    console.log("ajaxSuccess --------------------------------------");
    //    console.log(event);
    //    console.log(xhr);
    //    console.log(opts);
    //    console.log("                                                ");
    //});

    //$(document).ajaxError(function (event, xhr, opts, err) {
    //    console.log("ajaxError --------------------------------------");
    //    console.log(event);
    //    console.log(xhr);
    //    console.log(opts);
    //    console.log(err);
    //    console.log("                                                ");
    //});

    $(document).ajaxComplete(function (event, xhr, opts) {
        //console.log("ajaxComplete --------------------------------------");
        //console.log(event);
        //console.log(xhr);
        //console.log(opts);
        //console.log("                                                ");

        var str = xhr.getResponseHeader("sessionTimeout");
        if (str == "true") {
            if (window.top) {
                window.top.location.href = "/User";
            } else {
                window.location.href = "/User";
            }
        }
    });

    //$(document).ajaxStop(function (event) {
    //    console.log("ajaxStop --------------------------------------");
    //    console.log(event);
    //    console.log("                                                ");
    //});

})();