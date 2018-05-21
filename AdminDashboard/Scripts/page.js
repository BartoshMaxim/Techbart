$(document).ready(function () {
	init();
	function init() {
		$("tr#search label").on('click', function (e) {
			settingsOrderBy(this);
			sort();
		});
		$("tr#search input.search").on('change', function (e) {
			sort();
		});

		bindPager();
	}

	function bindPager() {
		$("ul.pages li span").on('click', function (e) {
			settingPagerParams($(this).attr('data-page'));
			sort();
		});
	}

	function settingsOrderBy(e) {
		let el = $(e);

		if (el.hasClass('active')) {
			el.toggleClass('desc');
		}
		else {
			$("tr#search label").removeAttr('class');
			el.addClass('active');
		}

		$("#OrderBy").val(el.attr('name'));
		$("#IsDesc").val(el.hasClass('desc'));
	}

	function settingPagerParams(e) {
		var take = $("#Take").val();

		var skip = (e - 1) * take;

		$("#Skip").val(skip);
	}

	function getData() {
		let data = {
			OrderBy: $("#OrderBy").val(),
			IsDesc: $("#IsDesc").val(),
			Skip: $("#Skip").val(),
			Take: $("#Take").val(),
		};

		$('tr#search input').each(function (i, e) {
			let el = $(e);
			if (el.val() != '') {
				data[el.attr('name')] = el.val();
			}
		});

		return data;
	}

	function sort() {
		let data = getData();

		var controllerName = window.location.pathname.split("/")[1];

		$.ajax({
			data: data,
			url: "/" + controllerName + "/ShowPager",
			success: function success(result) {
				$("#pager").html(result);
				bindPager();
			}
		});

		$.ajax({
			data: data,
			url: "/" + controllerName + "/PagesData",
			success: function (result) {
				$("#pagesdata").html(result);
			}
		});
	}
});

