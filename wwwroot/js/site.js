$('.search-icon').click(function () {
    $('.search-items').addClass('active');
});

const cartFunction = function () {
    $("#addToCart").off("click");
    $("#addToCart").on("click", function () {
        var obj = { productid: parseInt($(this).data("productid")), quantity: parseInt($(this).closest(".product-desc-section").find("input.addqnty").val()) };
        $.ajax({
            url: baseurl + "Home/addtocart",
            data: obj,
            dataType: "JSON",
            success: function (result) {
                $("#h11").html(result);
            },
            error: function (err) {

            }

        });
    });
}

cartFunction();