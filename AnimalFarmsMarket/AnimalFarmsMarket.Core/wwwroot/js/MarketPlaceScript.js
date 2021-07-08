$(function () {



    function doLivestock(x) {
        $.ajax({
            type: "GET",
            url: "/Market/MarketPlacePartial/?page=" + x,
            data: $("#myForm").serialize(),
            success: function (data) {
                console.log(data);
                $("#customerInformation").html(data);
            }
        });
    };

    

});





//invoice pagination
function GoToPage(btn) {
    var pageNumber = +btn.id;
    GetInvoice(pageNumber);
}


function GetInvoice(pageNumber) {
    $.ajax({
        type: "GET",
        url: "/Dashboard/Invoice/?paginated=true&" +"page=" + pageNumber,
        data: $("#invoiceForm").serialize(),
        success: function (data) {
            
            $("#accordionFlushExample").html(data);
            history.pushState('', "/Dashboard/Invoice/?" + "page=" + pageNumber,);
        }
    });
};


//market place pagination
//market view pagination
function curBtnPayment(btn) {
    var p = +btn.id;
    doPayment(p);
}


function doPayment(x) {
    $.ajax({
        type: "GET",
        url: "/Dashboard/PaymentHistory?check=true&page=" + x,
        data: $("#tbody").serialize(),
        success: function (data) {
            console.log(data);
            $("#paymentInfo").html(data);
            console.log(x);
            history.pushState({},"", "/Dashboard/PaymentHistory/?page=" + x);
        }
    });
};


//market view pagination
function curBtn(btn) {
    var p = +btn.id;
    var location = document.getElementById("location");
    doLivestock(p, location);
}


function doLivestock(x, location) {
    if (x == 1) {
        $.ajax({
            type: "GET",
            url: "/Market/Index/?check=false&page=" + x + "&location=" + location,
            data: $("#myForm").serialize(),
            success: function (data) {
                console.log(data);
                $("#customerInformation").html(data);
            }
        });
    } else {
        $.ajax({
            type: "GET",
            url: "/Market/Index/?check=true&page=" + x + "&location=" + location,
            data: $("#myForm").serialize(),
            success: function (data) {
                console.log(data);
                $("#customerInformation").html(data);
            }
        });

    }
};

//Toggle accordion mobile view
var btn = document.getElementById("acc-btn");
btn.addEventListener("click", function (e) {
    e.preventDefault();
    var acc = document.getElementById("accordions");
    if (acc.style.display === "none") {
        acc.style.display = "block";
    }
    else {
        acc.style.display = "none";
    }
});


