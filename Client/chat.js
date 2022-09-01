var connection = new signalR.HubConnectionBuilder()
                        .withUrl("https://localhost:44351/chatHub")
                        //.configureLogging(signalR.LogLevel.Information)
                        .build();

// Disable join group button until connection is established
document.getElementById("joinButton").disabled = true;

// Hide display of sending messages until user join a group
document.getElementById("sendMessagesInGroup").hidden = true;

// As a best practice, call the 'start' method on the HubConnection after 'on'. Doing so ensures your handlers are registered before any messages are received.
connection.on("ReceiveMessage", function(user, message){
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = message;
});

connection.on("ReceiveGroupMembers", function(members){
    document.getElementById("members").innerHTML = "";

    members.forEach(member => {
        var li = document.createElement("li");
        document.getElementById("members").appendChild(li);
        li.textContent = member;
    });
});

connection.start()
    .then(function(){
        document.getElementById("joinButton").disabled = false;
    })
    .catch(function(err){
        return console.error(err.toString());
    });

document.getElementById("joinButton").addEventListener("click", function(event){
    event.preventDefault();

    var user = document.getElementById("userInput").value;
    var group = document.getElementById("groupInput").value;

    connection.invoke("AddToGroup", user, group)
            .catch(function(err){ // Exceptions thrown in hub methods are sent to the client that invoked the method. So catch() will catch those exceptions.
                // Connections aren't closed when a hub throws an exception.
                return console.error(err.toString());
            });
    
    document.getElementById("sendMessagesInGroup").hidden = false;
    document.getElementById("joinGroup").hidden = true;

    document.getElementById("user").innerHTML = `<b>${user}</b>`;
    document.getElementById("group").innerHTML = `<b>${group}</b>`;
});

document.getElementById("sendButton").addEventListener("click", function(event){
    event.preventDefault();
    
    var user = document.getElementById("user").textContent;
    var group = document.getElementById("group").textContent;

    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessageToGroup", user, group, message)
            .catch(function(err){
                return console.error(err.toString());
            });
    
    document.getElementById("messageInput").value = "";
});

document.getElementById("leaveButton").addEventListener("click", function(event){
    event.preventDefault();

    var user = document.getElementById("user").textContent;
    var group = document.getElementById("group").textContent;
    
    connection.invoke("RemoveFromGroup", user, group)
            .catch(function(err){
                return console.error(err.toString());
            });
    
    document.getElementById("sendMessagesInGroup").hidden = true;
    document.getElementById("joinGroup").hidden = false;

    document.getElementById("messagesList").innerHTML = "";
});