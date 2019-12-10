function chathistory(result) {
    var chat = $.connection.chatHub;
    $(".chat-right").remove();
    $(".chat-left").remove();
    $.each(result, function () {
        var result = this;
        chat.client.addNewMessageToPage(result.name, result.message, result.date, result.Status, result.IdUser);
    });
}

function sendmessage(name, message, date) {
    $(".notification1").hide();
    $(".notification2").hide();
    if (name == "@Session["FullName"]")
    $('#discussion').append('<li class="chat-right"><span class="timestamp">' + htmlEncode(date) + '</span><br /><span>' + htmlEncode(message) + '</span><strong> :' + htmlEncode(name)
        + '</strong></li>');
            else
    $('#discussion').append('<li class="chat-left"><span class="timestamp">' + htmlEncode(date) + '</span><br /><strong>' + htmlEncode(name)
        + '</strong>:<span>' + htmlEncode(message) + '</span></li>');
    $('#discussion').animate({ scrollTop: 99999999 }, 500);
}

function listconversation(id, name, date, status, idreceived) {
    if (htmlEncode(status) == "true")
        $('#list-discussion').append('<li class="list-discussion conversationopen" id="' + htmlEncode(id) + '" status="' + htmlEncode(status) + '" idreceived="' + htmlEncode(idreceived) + '">' + htmlEncode(name) + htmlEncode(date) + '</li>');
    else
        $('#list-discussion').append('<li class="list-discussion conversationclose" id="' + htmlEncode(id) + '" status="' + htmlEncode(status) + '" idreceived="' + htmlEncode(idreceived) + '">' + htmlEncode(name) + htmlEncode(date) + '</li>');
}

function notification(type) {
    switch (type) {
        case "1":
            $(".notification1").hide();
            $('#discussion').append('<li class="chat-middle notification1"><span>đang trả lời...</span></li>').children(':last').delay(3000).fadeOut(100);
            break;
        case "2":
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Đã đọc</span></li>');
            break;
        case "3":
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Không gửi được tin nhắn</span></li>');
            break;
        case "4":
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Cuộc hội thoại đã kết thúc! Để được hỗ trợ hãy tạo hội thoại mới!</span></li>');
            break;
        default:
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Đã gửi</span></li>');
            break;
    }
    $('#discussion').animate({ scrollTop: 99999999 }, 500);
}

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}