$(function () {
    //Admin zone
    $("#SelectCategory").change(function () {
        var url = $(this).val();

        if (url) {
            window.location = "/admin/shop/Products?categoryId=" + url;
        }
        return false;
    });

    $(".delete").click(function () {
        if (!confirm("Are you sure about deletion of product?")) {
            return false;
        }
    });

    //UI zone
    var productsCount = $(".product").length;

    if (productsCount < 3) {
        $(".products").css("justify-content", "normal");
        $(".product").css("margin-right", "15px");
    };

});