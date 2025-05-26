// AJAX POST request helper
function postAjax(url, data, onSuccess, onError) {
    $.ajax({
        url: url,
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response.Status) {
                toastr.success(response.Info || "İşlem başarılı.");
                if (onSuccess) onSuccess(response);
            } else {
                toastr.error(response.ErrorMessage || "Bir hata oluştu.");
                if (onError) onError(response);
            }
        },
        error: function () {
            toastr.error("Sunucu ile bağlantı kurulamadı.");
            if (onError) onError();
        }
    });
}

var currentUserId = window.currentUserId || 0;

function requireLogin() {
    if (!currentUserId || currentUserId === 0) {
        toastr.warning("Bu işlem için giriş yapmanız gerekiyor.");
        return false;
    }
    return true;
}