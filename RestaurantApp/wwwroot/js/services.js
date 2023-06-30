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
    var $tableClass = $('.tableClass');
    $tableClass.hasClass('tableClass') ? $tableClass.removeClass('d-none') : $tableClass.addClass('d-none');
}

// Get references to the buttons and the span element


const incrementButton = document.getElementById('incrementitems');
const decrementButton = document.getElementById('Decressitems');
const spanElement = document.querySelector('.textShow');

// Add click event listener to increment button
incrementButton.addEventListener('click', function () {
    let value = parseInt(spanElement.textContent);
    value++;
    spanElement.textContent = value;
});

// Add click event listener to decrement button
decrementButton.addEventListener('click', function () {
    let value = parseInt(spanElement.textContent);
    if (value > 0) {
        value--;
        spanElement.textContent = value;
    }
});

function productnames(categoryid, card) {
    debugger;
    var cards = document.getElementsByClassName('catcard');
    for (var i = 0; i < cards.length; i++) {
        cards[i].classList.remove('cardactive');
    }

    // Add the "active" class to the clicked card
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
    alert('for prod called');
   
    var value = parseInt($('#unitvalue').text()) || 1;
    console.log("value", value);
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
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('error', errorThrown);
        }
    })
   
}