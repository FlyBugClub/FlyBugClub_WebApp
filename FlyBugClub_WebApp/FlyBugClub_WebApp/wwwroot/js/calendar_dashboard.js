document.addEventListener("DOMContentLoaded", function () {
    const currentMonthYearElement = document.getElementById("current-month-year");
    const prevMonthButton = document.getElementById("prev-month");
    const nextMonthButton = document.getElementById("next-month");
    const calendarDaysElement = document.getElementById("calendar-days");

    // Lấy tháng và năm hiện tại từ nội dung phần tử "current-month-year"
    let currentMonthYear = currentMonthYearElement.textContent.split(' ');
    let currentMonth = currentMonthYear[0];
    let currentYear = parseInt(currentMonthYear[1]); // Chuyển sang số nguyên

    // Xử lý khi nút "prev-month" được nhấn
    prevMonthButton.addEventListener("click", function () {
        // Giảm tháng đi 1
        currentMonth = getPreviousMonth(currentMonth);
        if (currentMonth === "December") {
            currentYear -= 1;
        }

        // Cập nhật nội dung phần tử "current-month-year"
        currentMonthYearElement.textContent = currentMonth + " " + currentYear;

        // Cập nhật danh sách ngày
        updateCalendarDays();
    });

    // Xử lý khi nút "next-month" được nhấn
    nextMonthButton.addEventListener("click", function () {
        // Tăng tháng lên 1
        currentMonth = getNextMonth(currentMonth);
        if (currentMonth === "January") {
            currentYear += 1;
        }

        // Cập nhật nội dung phần tử "current-month-year"
        currentMonthYearElement.textContent = currentMonth + " " + currentYear;

        // Cập nhật danh sách ngày
        updateCalendarDays();
    });

    // Hàm lấy tháng trước
    function getPreviousMonth(currentMonth) {
        const months = [
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        ];

        const currentIndex = months.indexOf(currentMonth);
        if (currentIndex === 0) {
            return "December";
        } else {
            return months[currentIndex - 1];
        }
    }

    // Hàm lấy tháng tiếp theo
    function getNextMonth(currentMonth) {
        const months = [
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        ];

        const currentIndex = months.indexOf(currentMonth);
        if (currentIndex === 11) {
            return "January";
        } else {
            return months[currentIndex + 1];
        }
    }

    // Hàm cập nhật danh sách ngày dựa trên tháng và năm hiện tại
    function updateCalendarDays() {
        // Loại bỏ tất cả các ngày hiện có
        calendarDaysElement.innerHTML = "";

        // Tạo một đối tượng Date cho tháng và năm hiện tại
        const currentDate = new Date(currentYear, getMonthIndex(currentMonth), 1);

        // Tính số ngày trong tháng
        const lastDay = new Date(currentYear, currentDate.getMonth() + 1, 0).getDate();

        // Tính thứ
