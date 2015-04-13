/*
 * Copyright (c) 2014 Mike King (@micjamking)
 *
 * jQuery Succinct plugin
 * Version 1.1.0 (October 2014)
 *
 * Licensed under the MIT License
 */

/*global jQuery*/
(function($) {
	'use strict';

	$.fn.succinct = function(options) {

		var settings = $.extend({
			size: 240,
			omission: '...',
			ignore: true
		}, options);

		return this.each(function() {

			var textDefault,
				textTruncated,
				elements = $(this),
				regex    = /[!-\/:-@\[-`{-~]$/,
				init     = function() {
					elements.each(function() {
						textDefault = $(this).html();

						if (textDefault.length > settings.size) {
							textTruncated = $.trim(textDefault)
											.substring(0, settings.size)
											.split(' ')
											.slice(0, -1)
											.join(' ');

							if (settings.ignore) {
								textTruncated = textTruncated.replace(regex, '');
							}

							$(this).html(textTruncated + settings.omission);
						}
					});
				};
			init();
		});
	};
})(jQuery);






$(window).resize(function () {
	$(".fixHeight").each(function () {
		$(this).height(260 - $(this).parent().children().eq(0).height());
	});
});

$(".fixHeight").ready(function () { $(window).trigger('resize'); });

$(document).ready(function () {

	$('#myTab a').click(function (e) {
		e.preventDefault();
		$(this).tab('show')

		filterUsingKey($(this).attr('href').substr(1));
	});	

	$(".description").each(function () {
		if ($(this).text().length > 70) {

			var title = $(this).text();

			$(this).succinct({
				size: 70
			}).append(" <a href='#' data-toggle='tooltip' data-placement='top' title='"+title+"'>view all</a>");
		}
	});

	$(function () {
		$('[data-toggle="tooltip"]').tooltip()
	});

	setInterval(function () { $(window).trigger('resize'); }, 300);
});

function filterUsingKey(key) {

	if (key == "all") {
		$('.project-thumbnail').fadeIn("slow", function () {});
	} else {
		$('.project-thumbnail:not(.project-' + key + ')').fadeOut("slow", function () {});
		$('.project-' + key).fadeIn("slow", function () {});
	}
}