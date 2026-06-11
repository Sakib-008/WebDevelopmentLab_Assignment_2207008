(function () {
    function updateFooterYear() {
        var footerText = document.querySelector('.site-footer p');
        if (!footerText) {
            return;
        }

        footerText.textContent = footerText.textContent.replace(/\b\d{4}\b/, String(new Date().getFullYear()));
    }

    function reloadOnBackForwardCache() {
        window.addEventListener('pageshow', function (event) {
            if (event.persisted) {
                window.location.reload();
            }
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        updateFooterYear();
        reloadOnBackForwardCache();
    });
})();
