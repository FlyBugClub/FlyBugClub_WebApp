function checkNetworkStatus() {
    var loadingScreen = document.getElementById("backgrounf-loding");
    var mainScreen = document.getElementsByClassName('body');

    // Kiểm tra navigator.onLine để biết trạng thái mạng
    if (!navigator.onLine) {
        // Mạng bị mất
        $(window).on('load', function () {
            $('#backgrounf-loding').hide();
        }) 
    } else {
        // Mạng hoạt động
        $(window).on('load', function () {
            $('#backgrounf-loding').hide();
        }) 
    }
}

// Gọi hàm kiểm tra trạng thái mạng ban đầu
checkNetworkStatus();

// Lắng nghe sự kiện online/offline để cập nhật trạng thái mạng
window.addEventListener('online', checkNetworkStatus);
window.addEventListener('offline', checkNetworkStatus);




