function handleButtonClick(button) {
    event.preventDefault();

    var status;
    var heartIcon_regular = button.querySelector('i.fa-heart.fa-regular');
    var heartIcon_solid = button.querySelector('i.fa-heart.fa-solid');
    // tim rong
    if (heartIcon_regular != null) {
        status = "true";
    }
    // tim dac
    else if (heartIcon_solid != null) {
        status = "false";

    }

    var deviceId = button.getAttribute("data-index");

    // Sử dụng userId ở phạm vi JavaScript

    var userId = "@userId";
    if (userId !== "") {

        fetchData(userId, deviceId, status);

    }

    async function fetchData(userId, deviceId, status) {
        const response = await fetch(`/api/users/getuserbyuid?uid=${userId}&deviceid=${deviceId}&status=${status}`);

        if (response.ok) {


            if (status === "true") {
                heartIcon_regular.classList.remove('fa-regular');
                heartIcon_regular.classList.add('fa-solid');
            }
            else {
                heartIcon_solid.classList.remove('fa-solid');
                heartIcon_solid.classList.add('fa-regular');
            }
        }

    }
}