function likeInc(itemID) {
    var likeCount = document.getElementById("likeCount");
    var likeIcon = document.getElementById("likeIcon");
    jQuery.ajax(
        {
            url: "/api/sitecore/Like/Like",
            method: "POST",
            data: {
                itemID: itemID
            },
            success: function (data) {
                if (data.error) {
                    console.log("Error [likeInc]: " + data.error);
                }
                else if (data.isLiked === true) {
                    likeIcon.style.color = 'red';
                    likeCount.innerText = data.count;
                }
                else if (data.isLiked === false) {
                    likeIcon.style.color = 'darkgrey';
                    likeCount.innerText = data.count;
                }
            },
            error: function (data) {
                console.log("Error [likeInc]: " + data);
            }
        });
}

function getEvents(input) {
    var name = input.value;
        jQuery.ajax(
            {
                url: "/api/sitecore/Search/GetEvents",
                method: "POST",
                data: {
                    name: name
                },
                success: function (data) {
                    if (data.error) {
                        console.log("Error [getEvents]: " + data.error);
                    }
                    else if (data.events) {
                        var options = "";
                        for (var i = 0; i < data.events.length; i++) {
                            options += '<option value="' + data.events[i].name + '"/>';
                        }
                        document.getElementById("txtSearchList").innerHTML = options;
                    }
                },
                error: function (data) {
                    console.log("Error [getEvents]: " + data);
                }
            });
}