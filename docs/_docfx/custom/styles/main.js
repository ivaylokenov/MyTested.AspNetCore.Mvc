$(function() {
	var interval = setInterval(function() {
		var elements = $('.nav.level1').find('a[href]');
		
		if (elements.length != 0) {
			clearInterval(interval);
			
			elements.each(function (i, el) {
			  $(el).text($(el).text().replace('MyTested.AspNetCore.Mvc.Builders.Contracts.', ''));
			  $(el).text($(el).text().replace('MyTested.AspNetCore.Mvc', 'Common Classes & Extensions'));
			});
			
			$('#toc.toc').show();
		}
	}, 200);
})