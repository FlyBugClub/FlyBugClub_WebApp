function getDaysInMonth(year, month) {
    // Lưu ý: Tháng trong JavaScript đếm từ 0 đến 11 (0 là tháng 1, 11 là tháng 12)
    // Nếu month không được chỉ định, nó sẽ lấy tháng hiện tại
    const lastDay = new Date(year, month + 1, 0).getDate();
    return lastDay;
}

// Lấy ngày, tháng và năm hiện tại
const currentDate = new Date();
const currentYear = currentDate.getFullYear();
const currentMonth = currentDate.getMonth();
const daysInCurrentMonth = getDaysInMonth(currentYear, currentMonth);

console.log(`Số ngày trong tháng hiện tại: ${daysInCurrentMonth}`);

// Hàm cập nhật ngày trong tháng
function updateCalendar() {
    const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay();

    // Vị trí bắt đầu của các ngày trong tuần
    const startDayPosition = firstDayOfMonth === 0 ? 6 : firstDayOfMonth - 1;

    // Đối tượng HTML của ul#calendar-days
    const calendarDaysElement = document.getElementById('calendar-days');

    // Xóa các phần tử con cũ của ul#calendar-days
    calendarDaysElement.innerHTML = '';

    // Thêm các thẻ li vào ul#calendar-days
    for (let i = 0; i < startDayPosition; i++) {
        const emptyLi = document.createElement('li');
        calendarDaysElement.appendChild(emptyLi);
    }

    for (let i = 1; i <= daysInCurrentMonth; i++) {
        const dayLi = document.createElement('li');
        dayLi.textContent = i;

        // Nếu đây là ngày hiện tại, bạn có thể thêm một lớp CSS để làm nổi bật
        if (i === new Date().getDate() && currentMonth === new Date().getMonth() && currentYear === new Date().getFullYear()) {
            dayLi.classList.add('active');
        }

        // Thêm các logic khác tùy thuộc vào yêu cầu của bạn (ví dụ: xác định ngày có sự kiện)

        calendarDaysElement.appendChild(dayLi);
    }
}

// Sự kiện nhấn cho nút "prev-month"
document.getElementById('prev-month').addEventListener('click', function () {
    // Giảm tháng đi 1
    currentMonth--;
    // Nếu tháng là tháng 0 (tháng 1), cập nhật năm và tháng về tháng 12 của năm trước
    if (currentMonth < 0) {
        currentMonth = 11;
        currentYear--;
    }
    // Cập nhật lịch
    updateCalendar();
});

// Sự kiện nhấn cho nút "next-month"
document.getElementById('next-month').addEventListener('click', function () {
    // Tăng tháng đi 1
    currentMonth++;
    // Nếu tháng là tháng 11 (tháng 12), cập nhật năm và tháng về tháng 1 của năm sau
    if (currentMonth > 11) {
        currentMonth = 0;
        currentYear++;
    }
    // Cập nhật lịch
    updateCalendar();
});

// Khởi tạo lịch khi trang được tải lần đầu
updateCalendar();