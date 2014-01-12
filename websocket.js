window.onload = function () {
	var status = document.getElementById("status");
	var canvas = document.getElementById("canvas");
	var context = canvas.getContext("2d");

	if (!window.WebSocket) {
		status.innerHTML = "No web sockets";
		return;
	}

	status.innerHTML = "Connecting to server...";

	var socket = new WebSocket("ws://localhost:8181/Davis_Server");

	// established connection
	socket.onopen = function () {
		status.innerHTML = "Connection successful";
	}

	socket.onclose = function () {
		status.innerHTML = "Connection closed";
	}

	socket.onmessage = function (event) {
		alert("Message recieved");
		console.dir(event);
	}
}