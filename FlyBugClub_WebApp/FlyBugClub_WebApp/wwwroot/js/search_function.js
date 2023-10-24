$(document).ready(function () {
    // Lấy trường input theo id
    var keywordInput = $("#keyword");

    // Xử lý sự kiện khi trường input được nhấp vào
    keywordInput.on("click", function () {
        // Nếu trường input có giá trị, tô đen nó
        if ($(this).val().trim() !== "") {
            $(this).select();
        }
    });
});

function preventEnter(event) {
    var inputField = document.getElementById("keyword");
    var inputValue = inputField.value.trim();

    if (event.key === "Enter" && inputValue === "") {
        event.preventDefault();
        return false;
    }

    return true;
}