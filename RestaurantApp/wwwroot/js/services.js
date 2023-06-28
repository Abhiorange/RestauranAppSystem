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

function productnames(categoryid) {
    
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

function productitem(productid) {
    alert('for prod called');
    var $overflow = $('.overflow_class');
    $overflow.removeClass('d-none');
}