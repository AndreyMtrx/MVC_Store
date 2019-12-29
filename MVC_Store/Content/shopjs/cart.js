$(function () {
    $("span.qty").each(function (index, value) {
        var qty = parseInt($(this).text());
        if (qty < 2) {
            $(this).prev().css("visibility", "hidden");
        }
    });

    $("a.incproduct").click(function (e) {
        e.preventDefault();

        $(this).parent().children().css("visibility", "visible");

        var productId = $(this).data("id");
        var url = "/cart/IncrementProduct";

        $.getJSON(url, { productId: productId }, function (data) {
            $(".qty" + productId).html(data.qty);

            var price = data.qty * data.price;
            var priceHtml = price + " UAH";
            $(".total" + productId).html(priceHtml);

            var cartTotal = parseInt($(".cart-total-price span").text())
            var cartTotalFin = cartTotal + data.price;
            $(".cart-total-price span").html(cartTotalFin);

            var cartQtyLayout = parseInt($("span.cart-qty-layout").text());
            $("span.cart-qty-layout").text(cartQtyLayout + 1);

            var cartPriceLayout = parseInt($("span.cart-price-layout").text());
            $("span.cart-price-layout").text(cartPriceLayout + data.price);
        });
    });

    $("a.decproduct").click(function (e) {
        e.preventDefault();

        var qty = parseInt($(this).next(".qty").text());

        if (qty < 3) {
            $(this).css("visibility", "hidden");
        }

        var productId = $(this).data("id");
        var url = "/cart/DecrementProduct";

        $.getJSON(url, { productId: productId }, function (data) {
            $(".qty" + productId).html(data.qty);

            var price = data.qty * data.price;
            var priceHtml = price + " UAH";
            $(".total" + productId).html(priceHtml);

            var cartTotal = parseInt($(".cart-total-price span").text())
            var cartTotalFin = cartTotal - data.price;
            $(".cart-total-price span").html(cartTotalFin);

            var cartQtyLayout = parseInt($("span.cart-qty-layout").text());
            $("span.cart-qty-layout").text(cartQtyLayout - 1);

            var cartPriceLayout = parseInt($("span.cart-price-layout").text());
            $("span.cart-price-layout").text(cartPriceLayout - data.price);
        });
    });

    $("a.removeproduct").click(function (e) {
        e.preventDefault();

        var url = "/cart/RemoveProduct";
        var productId = $(this).data("id");

        $(this).closest(".cart-item").slideUp(300);
        $(".hr" + productId).hide();

        $.getJSON(url, { productId: productId }, function (data) {
            var cartQtyLayout = parseInt($("span.cart-qty-layout").text());
            $("span.cart-qty-layout").text(cartQtyLayout - data.quantity);

            var cartPriceLayout = parseInt($("span.cart-price-layout").text());
            $("span.cart-price-layout").text(cartPriceLayout - data.totalPrice);

            var cartTotal = parseInt($(".cart-total-price span").text())
            var cartTotalFin = cartTotal - data.totalPrice;
            $(".cart-total-price span").html(cartTotalFin);

            if (data.isEmpty == true) {
                $(".checkout-btn").hide();
            }
        });
    });
});