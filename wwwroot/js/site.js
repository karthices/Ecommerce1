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
                if (result.status == "true") {
                    if ($("#cartcount").length == 0) {
                        $("#cartlink").append("<span id='cartcount'>" + result.cartqty + "</span>");
                    } else {
                        $("#cartcount").html(result.cartqty);
                    }
                }
            },
            error: function (err) {

            }

        });
    });

    function updatecart(productid, quantity, cb = null) {
        var obj = { productid: productid, quantity: quantity };
        $.ajax({
            url: baseurl + "Home/updatecart",
            data: obj,
            dataType: "JSON",
            success: function (result) {
                if (result.status == "true") {
                    if ($("#cartcount").length == 0) {
                        $("#cartlink").append("<span id='cartcount'>" + result.cartqty + "</span>");
                    } else {
                        $("#cartcount").html(result.cartqty);
                    }
                }
                if (cb) cb(result);
            },
            error: function (err) {

            }

        });
    }

    $(".qtychange").click(function () {
        var prodId = $(this).closest("tr").data("id");
        var inpqty = $(this).parent().find("input.addqnty");
        var currentqty = $(this).parent().find("input.addqnty").val();
        var maxqty = $(this).parent().find("input.addqnty").attr("size");
        if ($(this).hasClass("plus")) {
            // inc qty
            currentqty = parseInt(currentqty) + 1;
        }
        else if ($(this).hasClass("minus")) {
            // dec qty
            currentqty = parseInt(currentqty) - 1;
        }

        if (currentqty < 1) currentqty = 1;
        if (currentqty > maxqty) currentqty = maxqty;

        updatecart(prodId, currentqty, function (res) {
            if (res.status) {
                $.each(res.items, function (key, value) {
                    $("tr[data-id='" + value["productid"] + "']").find("input.addqnty").val(value["qty"]);
                    $("tr[data-id='" + value["productid"] + "']").find("span.updatesubtotal").html(value["subtotal"]);
                });



            }
        });


    });

    $(".removecart").click(function () {
        var currrow = $(this).closest("tr");
        var productid = $(this).closest("tr").data("id");
        var obj = { productid: productid };
        $.ajax({
            url: baseurl + "Home/removecart",
            data: obj,
            dataType: "JSON",
            success: function (result) {
                if (result.status == "true") {
                    if (result.cartqty == 0) {
                        window.location.reload(true);
                    }
                    if ($("#cartcount").length == 0) {
                        $("#cartlink").append("<span id='cartcount'>" + result.cartqty + "</span>");
                    } else {
                        $("#cartcount").html(result.cartqty);
                    }

                    currrow.remove();
                }
                
            },
            error: function (err) {

            }

        });
    });
}

cartFunction();