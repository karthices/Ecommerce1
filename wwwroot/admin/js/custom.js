jQuery(function ($) {

    $(".sidebar-dropdown > a").click(function () {
        $(".sidebar-submenu").slideUp(200);
        if (
            $(this)
                .parent()
                .hasClass("active")
        ) {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .parent()
                .removeClass("active");
        } else {
            $(".sidebar-dropdown").removeClass("active");
            $(this)
                .next(".sidebar-submenu")
                .slideDown(200);
            $(this)
                .parent()
                .addClass("active");
        }
    });

    $("#close-sidebar").click(function () {
        $(".page-wrapper").removeClass("toggled");
    });
    $("#show-sidebar").click(function () {
        $(".page-wrapper").addClass("toggled");
    });

});

function getResizedCanvas(canvas, newWidth, newHeight) {

    var tmpCanvas = document.createElement('canvas');
    tmpCanvas.width = newWidth;
    tmpCanvas.height = newHeight;

    var ctx = tmpCanvas.getContext('2d');
    ctx.drawImage(canvas, 0, 0, canvas.width, canvas.height, 0, 0, newWidth, newHeight);

    return tmpCanvas;
}

const addButton = document.getElementById("AddData");
const EditScreen = document.getElementById("EditScreen");

const CategoriesImage = document.getElementById("CategoriesImage");
const CategoryImagePreview = document.getElementById("CategoryImagePreview");

const ProductImage1 = document.getElementById("ProductImage1");
const ProductImagePreview1 = document.getElementById("ProductImagePreview1");
const ProductImage2 = document.getElementById("ProductImage2");
const ProductImagePreview2 = document.getElementById("ProductImagePreview2");
const ProductImage3 = document.getElementById("ProductImage3");
const ProductImagePreview3 = document.getElementById("ProductImagePreview3");
const ProductImage4 = document.getElementById("ProductImage4");
const ProductImagePreview4 = document.getElementById("ProductImagePreview4");
const ProductImage5 = document.getElementById("ProductImage5");
const ProductImagePreview5 = document.getElementById("ProductImagePreview5");


//addButton.addEventListener("click", function (e) {
//    if (EditScreen.style.display == "none") {
//        EditScreen.style.display = "block";
//    }
//});
//debugger;
function prevImage(elm, preelm) {
    if (elm && elm.files) {
        const [file] = elm.files
        if (file) {
            preelm.src = URL.createObjectURL(file)
        }
    }
}

if (CategoriesImage)
CategoriesImage.onchange = prevImage(CategoriesImage, CategoryImagePreview);
if (ProductImage1)
    ProductImage1.addEventListener("change", function () {
        prevImage(ProductImage1, ProductImagePreview1);
    });

if (ProductImage2)
    ProductImage2.addEventListener("change", function () {
        prevImage(ProductImage2, ProductImagePreview2);
    });

if (ProductImage3)
    ProductImage3.addEventListener("change", function () {
        prevImage(ProductImage3, ProductImagePreview3);
    });

if (ProductImage4)
    ProductImage4.addEventListener("change", function () {
        prevImage(ProductImage4, ProductImagePreview4);
    });

if (ProductImage5)
    ProductImage5.addEventListener("change", function () {
        prevImage(ProductImage5, ProductImagePreview5);
    });



if (ItemImage.files && ItemImage.files[0]) {
    var _URL = window.URL || window.webkitURL;
    var img = new Image();
    img.onload = function () {
        if (true || list.EnableProdImgAutoResize == true) {

            let canvas = document.createElement('canvas');
            let ctx = canvas.getContext("2d");

            canvas.width = this.width;
            canvas.height = this.height;
            ctx.drawImage(img, 0, 0);

            canvas = getResizedCanvas(canvas, 500, 500);

            canvas.toBlob(function (blob) {
                var formData = new FormData();
                formData.append("ItemImage", ItemImage.files[0]);
                formData.append("ID", $("#ID").val());
                $.ajax({
                    type: 'POST',
                    url: "Item/ImageUpdate",
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (result) {
                        console.log("item image upload: " + result);
                        reloadImages();
                    },
                    error: function (result) {
                        console.log("item image upload error log: " + result);
                    }
                });
                return;

            }, "image/webp");
        } else {
            console.log("After Size :" + this.width + "x" + this.height);
            if (this.width < 490 || this.width > 510 || this.height > 510 || this.height < 490) {
                AlertBoxSmart(list.Title, list.MismatchError, "error");
                return false;
            }
            else {
                console.log("item image: " + $("#ID").val());
                var formData = new FormData();
                formData.append("ItemImage", ItemImage.files[0]);
                formData.append("ID", $("#ID").val());
                $.ajax({
                    type: 'POST',
                    url: "Item/ImageUpdate",
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (result) {
                        console.log("item image upload: " + result);
                        reloadImages();
                    },
                    error: function (result) {
                        console.log("item image upload error log: " + result);
                    }
                });
            }
        }
    };
    var file = ItemImage.files[0];
    img.src = _URL.createObjectURL(file);
}