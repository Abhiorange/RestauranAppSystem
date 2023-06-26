
    $(document).ready(function () {

        // $("#main").css('marginLeft', '250px')
        // $(".abhiclass").css('display','none')


        $(".submit").click(function () {

            console.log("submit")
            console.log($("#exampleInputEmail1"))

            if ($("#exampleInputEmail1").val == "") {
                console.log("enter email")
            }
        })

        $(".drp").click(function () {
            $(".abhiclass").slideToggle(100);
        });


        $(".drp").click(function () {
            // $(".boom").removeAttr("style");
            var drpclass = $(".drpclass")
            if (drpclass.hasClass("d-none")) {
                drpclass.removeClass("d-none");
                $(".arrow").removeClass("bi-chevron-down").addClass("bi-chevron-up")
            }
            else {
                drpclass.addClass("d-none");
                $(".arrow").removeClass("bi-chevron-up").addClass("bi-chevron-down")
            }
        });

      


    })

    /* Set the width of the sidebar to 250px and the left margin of the page content to 250px */
    function openNav() {
        document.getElementById("mySidebar").style.width = "250px";
        document.getElementById("main").style.marginLeft = "0px";
    }

    /* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
    function closeNav() {
        document.getElementById("mySidebar").style.width = "0";
        document.getElementById("main").style.marginLeft = "-250px";
    }

    function openNav2() {
        document.getElementById("mySidebar2").style.width = "250px";
        document.getElementById("main2").style.marginLeft = "250px";
    }

    /* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
    function closeNav2() {
        document.getElementById("mySidebar2").style.width = "0";
        document.getElementById("main2").style.marginLeft = "0px";
    }



      


        $(document).ready(function () {
            //animateBox();

            //function animateBox() {

            //    $("#popcorn").animate({ right: '0px' }, 6000, 'linear', function () {
            //        $(this).animate({ opacity: '0' }, 1000, 'linear', function () {
            //            $(this).animate({ opacity: '1' }, 1000, 'linear', function () {

            //                $(this).animate({ left: '0px' }, 6000, 'linear', function () {
            //                    animateBox();
            //                });
            //            });
            //        });
            //    });

            //}

            animate();

        function animate() {

            $('#popcorn').animate({
                left: '85%'
            }, 4000, 'linear', function () {

                $("#popcorn").css("transform", "scaleX(-1)");

                animate2();

            });
                
        }

        // animation ends

        function animate2() {

            $('#popcorn').animate({
                left: '0'
            }, 4000, 'linear', function () {

                $("#popcorn").css("transform", "scaleX(1)");

                animate();

            });
              
        }

        $("#submitbtn").mouseenter(function () {
            $(".finger").animate({ width: "toggle" }, 300);

        })

        $("#submitbtn").mouseleave(function () {
            $(".finger").animate({ width: "toggle" }, 300);

        })

        $("#submitbtn").mouseenter(function () {
            $(this).css({ "backgroundColor": "black" });

        })

        $("#submitbtn").mouseleave(function () {
            $(this).css({ "backgroundColor": "red" });

        })


    });
