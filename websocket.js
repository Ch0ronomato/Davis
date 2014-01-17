window.onload = function () {
	var status = document.getElementById("status");
	var $ = jQuery;
	// var canvas = document.getElementById("canvas");
	// var context = canvas.getContext("2d");

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

	// trap on click to animate.
	$("#genres li").click(function() {
		if (!$(this).hasClass("current_genre")) {
			$(this).addClass("current_genre").siblings().removeClass("current_genre");
			$("#songs").show().animate({"left": "0px"}, 400, "swing", function() {
				$(this).find("li").click(function() {
					$(this).addClass("current_song").siblings().removeClass("current_song");
				});
			}).removeClass("hidden").addClass("shown");
		}

		else {
			$(this).removeClass("current_genre");
			$("#songs").removeClass("shown").addClass("hidden").animate({"left": "-330px"}, 400, "swing");
			$("#songs").find("li").removeClass("current_song");
		}
	});
}