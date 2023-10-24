$(document).ready(function () {
    // Ẩn sub-menu ban đầu
    $(".sub-menu").hide();

    // Sự kiện khi click vào nút sub-btn
    $(".sub-btn").click(function (e) {
        e.preventDefault(); // Ngăn chặn sự kiện mặc định (chuyển hướng trang)

        // Tìm div.sub-menu liên quan đến nút sub-btn đã nhấn và chỉ hiển thị nó
        $(this).closest("li").find(".sub-menu").slideToggle();
        $(this).find(".dropdown").toggleClass("rotate");
    });

    // Khi hover vào nav
    $("nav").hover(
        // function () {
        // // Hiển thị sub-menu khi hover vào nav
        // $(".sub-menu").slideDown();
        // },
        function () {
            // Ẩn sub-menu khi hover ra ngoài nav
            $(".sub-menu").slideUp();
        }
    );
});