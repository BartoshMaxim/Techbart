function removeImage(imageid) {
    var result = confirm("You sure about that you want delete this image?");

    if (result) {

        var id = imageid.replace("image", "");

        $.ajax({
            type: "POST",
			url: "/Product/DeleteImage",
            data: {
                ImageId: $('div.' + imageid + ' label input').val(),
				ProductId: $('#ProductId').val()
            }
        });

        $('div.' + imageid + ' label img').remove();
        $('div.' + imageid + ' label').attr("for", imageid);
        $('div.' + imageid + ' a').remove();
        $('div.' + imageid + ' label').text("+");
    }
}