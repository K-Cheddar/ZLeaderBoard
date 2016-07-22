angular.module('adminApp').factory('fancyboxService', function () {
    return {
        open: function (selector) {
            $.fancybox.open($(selector));
        },
        close: function () {
            $.fancybox.close();
        }
    };
});