$(function () {
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
});