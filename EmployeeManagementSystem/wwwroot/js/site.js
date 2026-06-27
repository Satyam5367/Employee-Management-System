// Auto-dismiss toasts after 4 seconds
document.addEventListener('DOMContentLoaded', function () {
    var toasts = document.querySelectorAll('.toast.show');
    toasts.forEach(function (toastEl) {
        setTimeout(function () {
            var bsToast = bootstrap.Toast.getOrCreateInstance(toastEl);
            bsToast.hide();
        }, 4000);
    });
});
