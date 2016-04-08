$(function () {

    var refreshPage = function () {
        location.reload();
    };

    var moveToTheTopOfPage = function () {
        $("body").scrollTop(0);
    };

    var isPartialViewReturned = function (data) {
        return data.match("^<!DOCTYPE") == null; //TODO
    }

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        }

        $.ajax(options).done(function (data) {
            if (data.success) {
                refreshPage();
                moveToTheTopOfPage();
            }
            else {
                if (!isPartialViewReturned(data)) {
                    // it means that the authorization expired, so by refreshing page we will
                    // be redirected in a standard way to login page.
                    refreshPage();
                    return false;
                }
                else {
                    var $target = $($form.attr("data-parking-target"));
                    $target.html($(data).find($form.attr("data-parking-target")));
                }
            }
        });

        return false;
    };

    var ajaxButtonClick = function () {
        var $button = $(this);
        
        $.get($button.attr("data-parking-href"), function (data) {
            if (!isPartialViewReturned(data)) {
                // it means that the authorization expired, so by refreshing page we will
                // be redirected in a standard way to login page.
                refreshPage();
                return false;
            }
            else {
                var $target = $($button.attr("data-target"));
                $target.html(data);
                initFormsInsideModals();
            }
        });
        
        //return false;
    };

    var initButtonsForOpeningModals = function () {
        $("button[data-parking-modal='true']").click(ajaxButtonClick);
    }

    var initFormsInsideModals = function () {
        $("form[data-parking-ajax='true']").submit(ajaxFormSubmit);
    }

    var initToolTips = function () {
        $('[data-toggle="tooltip"]').tooltip();
    }

    var initPopovers = function () {
        $('[data-toggle="popover"]').popover({
            html: true,
            content: function () {
                var $popover = $(this);
                var $popoverContent = $popover.find('#popover-content-wrapper');

                return $popoverContent.html();
            }
        }).on('shown.bs.popover', function() {
            initButtonsForOpeningModals();
        });
    }

    //<span class="fa fa-spinner fa-spin"></span>

    initButtonsForOpeningModals();
    initFormsInsideModals();
    initToolTips();
    initPopovers();
});