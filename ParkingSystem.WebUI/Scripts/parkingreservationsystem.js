$(function () {

    var refreshPage = function () {
        location.reload();
    };

    var moveToTheTopOfPage = function () {
        $("body").scrollTop(0);
    };

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
                var $target = $($form.attr("data-parking-target"));
                $target.html($(data).find($form.attr("data-parking-target")));
            }
        });

        return false;
    };

    var ajaxButtonClick = function () {
        var $button = $(this);
        
        $.get($button.attr("data-parking-href"), function (data) {
            var $target = $($button.attr("data-target"));
            $target.html(data);
            initFormsInsideModals();
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
        $('[data-toggle="popover"]').popover();
    }

    //<span class="fa fa-spinner fa-spin"></span>

    initButtonsForOpeningModals();
    initFormsInsideModals();
    initToolTips();
    initPopovers();
});