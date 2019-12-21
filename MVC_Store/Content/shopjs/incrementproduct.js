$(function () {
    $("a.incproduct").click(function (e) {
        e.preventDefault();

        var productId = $(this).data("id");
        var url = "/cart/IncrementProduct";

        $.getJSON(url, { productId: productId }, function (data) {
            $(".qty" + productId).html(data.qty);

            var price = (data.qty * data.price).toFixed(2);
            var priceHtml = price + " UAH";

            $(".total" + productId).html(priceHtml);
            var cartTotal = parseFloat($(".cart-total-price span").text())

            var cartTotalFin = (cartTotal + data.price).toFixed(2);

            $(".cart-total-price span").html(cartTotalFin);
        });
    });
});