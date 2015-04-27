function sendJSON(service, method, args, answerCallback, errorCallback) {
	$.ajax({
		type: "POST",
		url: "/API/" + service + ".asmx/" + method,
		data: args,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		error: errorCallback,
		success: answerCallback
	});
}