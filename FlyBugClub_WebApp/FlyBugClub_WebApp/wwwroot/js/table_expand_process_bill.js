// JavaScript để xử lý sự kiện nhấp vào hàng để mở rộng
function toggleRow(rowId) {
    var row = document.getElementById(rowId);
    if (row.style.display === "none") {
        row.style.display = "table-row"; // Hiển thị hàng mở rộng
    } else {
        row.style.display = "none"; // Ẩn hàng mở rộng
    }
}