$('.tabresponsive').slick({
    dots: false,
    infinite: true,
    speed: 100,
    slidesToShow: 7,
    slidesToScroll: 7,
    responsive: [
        {
            breakpoint: 1180,
            settings: {
                slidesToShow: 5,
                slidesToScroll: 4,
                infinite: true,

            }
        },
        {
            breakpoint: 1165,
            settings: {
                slidesToShow: 4,
                slidesToScroll: 3,
                infinite: true,
            }
        },
        {
            breakpoint: 768,
            settings: {
                slidesToShow: 8,
                slidesToScroll: 5,
                infnite:true,
            }
        }

    ]
});
$('.responsive').slick({
    
    speed: 100,
    slidesToShow: 6,
    slidesToScroll: 6,
    arrows: true,
    infnite:true,
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                infinite: true,

            }
        },
        {
            breakpoint: 1340,
            settings: {
                slidesToShow: 4,
                slidesToScroll: 1,
                infinite: true,

            }
        },
        {
            breakpoint: 880,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 2,
                infinite: true,
            }
        },
        
      
    ]
});
function showBill() {
    var tableNo = $('#tableno').val().trim();
    if (tableNo === '') {
        $('.tablewarn').removeClass('d-none');
    } else {
        var tableNo = parseInt(tableNo);
        var data = {
            tableno: tableNo
        }
        $.ajax({
            type: 'POST',
            url: '/services/Service/BillItems',
            traditional: true,
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (result) {
                $('.overflow_class').html($(result).find('.overflow_class').html());
                $('.tableClass').html($(result).find('.tableClass').html());
                $('.overflow_class').removeClass('d-none');
                $('.Bill').addClass('d-none');
                $('.bill_btn').removeClass('d-none');
                $('.tableClass').removeClass('d-none');
                $('.tablewarn').addClass('d-none');

            },
            error: function (xhr, textStatus, errorThrown) {
                alert('error', errorThrown);
            }
        })
    }

   

}

// Get references to the buttons and the span element

/*
const incrementButton = document.getElementById('incrementitems');
const decrementButton = document.getElementById('Decressitems');
const spanElement = document.querySelector('.textShow');*/
function delteitem(orderId, productId) {
    alert('enter into delete btn');
    var data = {
        orderid: orderId,
        productid: productId
    }
    $.ajax({
        type: 'POST',
        url: '/services/Service/DeleteItems',
        data: JSON.stringify(data),
        traditional: true,
        contentType: 'application/json',
        success: function (result) {
            $('.overflow_class').html($(result).find('.overflow_class').html());
            $('.overflow_class').removeClass('d-none');
            $('.Bill').addClass('d-none');
            $('.bill_btn').removeClass('d-none');
            $('.tableClass').addClass('d-none');

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('error', errorThrown);
        }
    })
}
function incre(orderId, productId) {
    var value = parseInt($('#unitvalue-' + productId).text());
    value++;
    var finalval = value;
    var data = {
        orderid: orderId,
        productid: productId,
        itemunit: finalval
    }
    $.ajax({
        type: 'POST',
        url: '/services/Service/IncreItems',
        data: JSON.stringify(data),
        traditional: true,
        contentType: 'application/json',
        success: function (result) {
            $('.overflow_class').html($(result).find('.overflow_class').html());
            $('.overflow_class').removeClass('d-none');
            $('.Bill').addClass('d-none');
            $('.bill_btn').removeClass('d-none');
            $('.tableClass').html($(result).find('.tableClass').html());
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('error', errorThrown);
        }
    })
}

function decre(orderId,productId) {
    var value = parseInt($('#unitvalue-' + productId).text());
    if (value > 0) {
        value--;
    }
    var finalval = value;
    var data = {
        orderid: orderId,
        productid: productId,
        itemunit: finalval
    }
    $.ajax({
        type: 'POST',
        url: '/services/Service/DecreItems',
        data: JSON.stringify(data),
        traditional: true,
        contentType: 'application/json',
        success: function (result) {
            $('.overflow_class').html($(result).find('.overflow_class').html());
            $('.overflow_class').removeClass('d-none');
            $('.Bill').addClass('d-none');
            $('.bill_btn').removeClass('d-none');
            $('.tableClass').html($(result).find('.tableClass').html());
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('error', errorThrown);
        }
    })
}


function productnames(categoryid, card) {
    var cards = document.getElementsByClassName('catcard');
    for (var i = 0; i < cards.length; i++) {
        cards[i].classList.remove('cardactive');
    }
    card.getElementsByClassName('catcard')[0].classList.add('cardactive');

    $.ajax({
        type:'GET',
        url: '/services/Service/ServicesPage',
        type: 'GET',
        data: {
            id: categoryid
        },
        traditional: true,
        success: function (result) {
            $('.details').html($(result).find('.details').html());


        }
    });
}


$('#formm').submit(function (event) {
    alert('for called');
    event.preventDefault();
  
    alert("enter in cust");
    const custname = document.getElementById("custname").value;
    const custcode = parseInt(document.getElementById("custcode").value); // parseInt(document.getElementById("custaddre").value);
    const custemail = document.getElementById("custemail").value;
    const custaddre = document.getElementById("custaddre").value;
    const custpho = parseInt(document.getElementById("custpho").value);
    console.log("name,email,addres,code,pho", custname, custemail, custaddre, custcode, custpho);
    var data = {
        name: custname,
        customerCode: custcode,
        address: custaddre,
        email: custemail,
        phone: custpho
    }
    $.ajax({
        type: 'POST',
        url: '/services/Service/CustomerAdd',
        data: JSON.stringify(data),
        traditional: true,
        contentType: 'application/json',
        success: function (result) {
            alert('success');
            debugger;
            //var htmlContent = $(result);
            $('.customer').html($(result).find('.customer').html());
            //$('.customer').replaceWith(htmlContent.find('.customer'));           
            $('.customer').removeClass('open');
            $('.customer').addClass('d-none');
            $('.warning').addClass('d-none');
            console.log("string", $('.customer'))
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('error', errorThrown);
        }
    })
    
});

function productitem(productId) {
    var value = 1;
    var data = {
        productid: productId,
        itemunit: value
    }
    $.ajax({
        type: 'POST',
        url: '/services/Service/AddItems',
        data: JSON.stringify(data),
        traditional: true,
        contentType: 'application/json',
        success: function (result) {
            $('.overflow_class').html($(result).find('.overflow_class').html());
            $('.overflow_class').removeClass('d-none');
            $('.Bill').addClass('d-none');
            $('.bill_btn').removeClass('d-none');
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('error', errorThrown);
        }
    })
   
}