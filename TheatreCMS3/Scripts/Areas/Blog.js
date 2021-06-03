﻿//const { data } = require("jquery");
//const { default: index } = require("../esm/popper-utils");


function upvote(id) {
	$.ajax({
		url: "/Comment/Upvote/" + id,
		type: "POST",
		data: JSON.stringify({ "id" : id }),
		success: function (message) {
			console.log(message)
			$(".comment-likes[data-commentId=" + id + "]").html(message.message)
			$(".progress-bar[data-commentId=" + id + "]").css('width', message.ratio)
	}
	})
}

function downvote(id) {
	$.ajax({
		url: "/Comment/Downvote/" + id,
		type: "POST",
		data: JSON.stringify({ "id" : id }),
		success: function (message) {
			console.log(message)
			$(".comment-dislikes[data-commentId=" + id + "]").html(message.message)
			$(".progress-bar[data-commentId=" + id + "]").css('width', message.ratio)
	}
	})
}

//Delete function for BlogPosts
function deletePost(id) {
	$.ajax({
		url: "/BlogPosts/DeleteRecord/" + id,
		type: "POST",
		data: JSON.stringify({ "id" : id }),
		success: function (message) {
			console.log(message);
			if (true) {
				$(document).ready(function () {
					console.log("Success");
					console.log(id);
					$(".modal").each(function () {
						$(this).modal('hide');
					})
					$('.alert').show(3000);
					$('.alert').fadeOut();
					document.getElementById("hiddenContent-" + id).style.display = "none";
				});
			}
			
		}
	});
	
}
