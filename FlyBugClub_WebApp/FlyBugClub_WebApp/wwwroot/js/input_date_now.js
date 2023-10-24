// Lấy thẻ input datetime-local
var datetimeInput = document.getElementById("myDatetimeInput");

// Lấy thời gian hiện tại
var now = new Date();

// Chuyển đổi thời gian hiện tại thành chuỗi có định dạng datetime-local
var nowString = now.toISOString().slice(0, 16);

// Gán giá trị cho thuộc tính min của thẻ input
datetimeInput.min = nowString;