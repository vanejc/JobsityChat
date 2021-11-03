"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
const chat = document.getElementById('chat');
connection.on("ReceiveMessage", function (user, message) {
    let currentUser = document.getElementById("hdUserName").value;
    let isCurrentUserMessage = currentUser === user;
    
    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "direct-chat-msg left" : "direct-chat-msg right";

    let chatInfo = document.createElement('div');
    chatInfo.className = "direct-chat-info clearfix";

    let spanUserName = document.createElement('span');
    spanUserName.className = isCurrentUserMessage ? "direct-chat-name pull-left" : "direct-chat-name pull-right";
    spanUserName.innerHTML = user;

    let spanDate = document.createElement('span');
    spanDate.className = isCurrentUserMessage ? "direct-chat-timestamp pull-right" : "direct-chat-timestamp pull-left";

    let img = document.createElement('img');
    img.className = "direct-chat-img";
    img.src = "https://raw.githubusercontent.com/twbs/icons/main/icons/person-fill.svg";

    let chatMsg = document.createElement('div');
    chatMsg.className = "direct-chat-text";
    chatMsg.innerHTML = message;

    var currentdate = new Date();
    spanDate.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    chatInfo.appendChild(spanUserName);
    chatInfo.appendChild(spanDate);
    container.appendChild(chatInfo);
    container.appendChild(img);
    container.appendChild(chatMsg);    
   
    chat.appendChild(container);
    document.getElementById("messageInput").value = "";    
    window.location.reload();    
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {    
    var user = document.getElementById("hdUserName").value;
    var message = document.getElementById("messageInput").value;
    if (message.trim() != "" && message != undefined) {
        $('#nomessage').hide();
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        if (message.toLowerCase().indexOf("/stock=") == -1) {
            SaveMessage(message);
        }
        event.preventDefault();        
    }   
});

function SaveMessage(message) {
    $.ajax({
        type: "POST",
        url: "Home/SaveMessage",
        data: { message: message },
        dataType: "json"
    });
};

document.getElementById("messageInput").addEventListener("keypress", function (event) {
    var key = event.which;
    if (key == 13) // the enter key code
    {
        event.preventDefault();
        document.getElementById("sendButton").click();
    }
});

document.getElementById("messageInput").focus();
window.scrollTo(0, document.body.scrollHeight);