$(document).ready(function () {
    var chat = $.connection.chatHub;

    chat.client.addNewMessageToPage = function (name, message) {
        if (name === $('#user-username').val()) {
            console.log('enter');
            $('#discussion').prepend('<li class="current-user"><strong>' + htmlEncode(name)
                + '</strong>: ' + htmlEncode(message) + '</li>');
            console.log("user");
        }
        else {
            $('#discussion').prepend('<li class="other-user"><strong>' + htmlEncode(name)
                + '</strong>: ' + htmlEncode(message) + '</li>');
        }
    };

    $.connection.hub.start().done(function () {
        var room = $('#trip-url').val();
        chat.server.join(room);

        $('#message').keypress(function (event) {
            if (event.which == 13) {
                $('#sendmessage').click();
            }
        });
        $('#sendmessage').click(function () {
            chat.server.send($('#message').val(), room);

            $('#message').val('');
        });
    });

});
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}