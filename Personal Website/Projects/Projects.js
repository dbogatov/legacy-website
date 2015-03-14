$(document).ready(function () {

	$('#myTab a').click(function (e) {
		e.preventDefault();
		$(this).tab('show')

		filterUsingKey($(this).attr('href').substr(1));
	});

	$(".fixHeight").each(function () {
		$(this).height(260 - $(this).parent().children().eq(0).height());
	});

});

function filterUsingKey(key) {

	if (key == "all") {
		$('.project-thumbnail').fadeIn("slow", function () {});
	} else {
		$('.project-thumbnail:not(.project-' + key + ')').fadeOut("slow", function () {});
		$('.project-' + key).fadeIn("slow", function () {});
	}
}