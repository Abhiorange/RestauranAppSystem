function productsbyid(orderid) {

    $.ajax({
        type: 'GET',
        url: '/orders/Orders/OrdersDetails',
        data: {
            orderid: orderid
        },
        traditional: true,
        contentType: 'application/json',
        success: function (result) {
            $('#flush-collapseOne-' + orderid).html($(result).find('#flush-collapseOne-' + orderid).html());
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('error', errorThrown);
        }
    })
}