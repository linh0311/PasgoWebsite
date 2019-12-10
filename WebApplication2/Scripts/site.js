
var x = window.matchMedia("(max-width: 768px)");
var y = window.matchMedia("(max-width: 576px)");
var body = document.body;
var sven1 = document.getElementById("sven1");
var sven2 = document.getElementById("sven2");
var phone = document.getElementById("pasgo-phonenumber");
var indicators = document.getElementById("indicators");
var textSearch = document.getElementById("textSearch");
var slTypeSearch = document.getElementById("slTypeSearch");
var fQuanId = document.getElementById("fQuanId");
var btnsearchSlide = document.getElementById("btnsearchSlide");
let blogteaser = document.querySelectorAll(".blog-teaser");

function searchWebsite() {
    var i = textSearch.value;
    c = fQuanId.options[fQuanId.selectedIndex].value;
    //r = fQuanId.options[fQuanId.selectedIndex].text;
    n = "https://localhost:44389/api/APIuser?city=";
    // n = "https://cors-anywhere.herokuapp.com/https://pasgo.vn/Search/SearchHeader/?keySearch=&typeName="
    //m = "&typeName=";
    m = "&keyword=";
    $.ajax({
        url: n + c + m + i ,
        //url: n,
        data: {
            // keySearch: i
            //'id': c,
            //'keyword': i
        },
        dataType: "html",
        accept: "text/html",
        contentType: "text/html; charset=UTF-8",
        traditional: !0,
        type: "POST",
        success: function (n) {
            document.getElementById("result").innerHTML = n;
        }

    })
}

textSearch.addEventListener("keypress", function (event) {
    console.log(event.keyCode);
    if (event.keyCode === 13) {
        // event.preventDefault();
        btnsearchSlide.click();
    }
});

function btnsearchSlideClick() {
    var n = slTypeSearch.value, i = textSearch.value;
    if (n === "1") {
        location.replace("https://pasgo.vn/tim-kiem?search=" + i);
    }
    if (n === "2") {
        location.replace("https://pasgo.vn/tim-kiem-kham-pha?search=" + i);
    }
    if (n === "3") {
        location.replace("https://pasgo.vn/video/tim-kiem?search=" + i);
    }
    console.log(n + ":" + i)
}

window.addEventListener('click', function (e) {
    if (document.getElementById('textSearch').contains(e.target)) {
        document.getElementById('result').style.display = "block";
    } else {
        if (document.getElementById('result').contains(e.target)) {
        } else {
            document.getElementById('result').style.display = "none";
        }
    }
})

function searchpc() {
    if (y.matches) {
        sven1.style.borderRadius = "30px";
        sven2.style.borderRadius = "30px";
        // for (let i = 0; i < blogteaser.length; i++) {
        // 		blogteaser[i].style.display = "none";
        // 	}
    } else {
        sven1.style.borderRadius = "30px 0 0 30px";
        sven2.style.borderRadius = "0 30px 30px 0";
        // for (let i = 0; i < blogteaser.length; i++) {
        // 		blogteaser[i].style.display = "block";
        // 	}
    }
    if (x.matches) {
        phone.style.display = "none";
    } else {
        phone.style.display = "block";
    }
}

function slideMobile() {
    if (window.matchMedia("(max-width: 414px)").matches) {
        indicators.classList.remove("carousel-indicators");
    } else {
        indicators.classList.add("carousel-indicators");
    }
}

//body.onresize = function() {searchpc();slideMobile();};
body.onload = function () { searchpc(); slideMobile(); setProvince(); searchpc(); slideMobile(); };
window.onscroll = function () { ScrollFunction(x); soslidethuonghieu(); };
window.resize = function () { ScrollFunction(x); soslidethuonghieu(); };

// <!-- Khóa vị trí Header -->

var body = document.body;
var headermain = document.getElementById("header-main");
var headersearch = document.getElementById("header-search");
var headermainlogo = document.getElementById("header-main-logo");
var logored = document.getElementById("logored");
var logowhite = document.getElementById("logowhite");
var xul = document.getElementById("xul");
var searchsticky = document.getElementById("searchsticky");
var loginbtn = document.getElementById("login-btn");
var scrollbtn = document.getElementById("scrollbtn")
let menubutton = document.querySelectorAll(".header-main-menu-button");
let dropdownheader = document.querySelectorAll(".pasgo-icondesktop-dropdown");
var sticky = headermain.offsetTop;
var og = window.matchMedia("(max-width: 414px)"); 	// Lỗi: width<768px header không chuyển sang đỏ khi scroll
// Nguyên nhân: đặt nhiều biến trùng tên với window.matchMedia

var count = 0;
var indicators = document.getElementById("indicators");

function ScrollFunction(x) {
    if (window.pageYOffset > sticky) {
        headermain.classList.add("sticky");
        scrollbtn.style.visibility = "visible";
        scrollbtn.style.opacity = "0.5";

        if (og.matches) {
            //Mobile
            headermainlogo.style.transform = "translate3d(0, -45px, 0)";
            headersearch.style.transform = "translate3d(10%, -40px, 0)";
            headersearch.style.width = "83%";
            headersearch.style.height = "40px";
            headersearch.style.paddingTop = "0px";
        } else {
            //PC
            for (let i = 0; i < menubutton.length; i++) {
                menubutton[i].style.color = "white";
            }
            for (let i = 0; i < 5; i++) {
                dropdownheader[i].style.filter = "invert(0)";
            }
            headermain.classList.add("red");
            logored.classList.add("hide");
            logowhite.classList.add("show");
            xul.classList.add("border-bottom-red");
            loginbtn.classList.add("hide");
            searchsticky.classList.add("show");
        }
    }
    else {
        headermain.classList.remove("sticky");
        scrollbtn.style.visibility = "hidden";
        scrollbtn.style.opacity = "0";
        if (og.matches) {
            //Mobile
            headermainlogo.style.transform = "translate3d(0, 0, 0)";
            headersearch.style.transform = "translate3d(0, 0, 0)";
            headersearch.style.width = "100%";
        } else {
            //PC
            headermain.classList.remove("red");
            headermainlogo.classList.remove("hide");
            for (let i = 0; i < menubutton.length; i++) {
                menubutton[i].style.color = "black";
            }
            for (let i = 0; i < 5; i++) {
                dropdownheader[i].style.filter = "invert(75%)";
            }
            logored.classList.remove("hide");
            logowhite.classList.remove("show");
            xul.classList.remove("border-bottom-red");
            loginbtn.classList.remove("hide");
            searchsticky.classList.remove("show");
        }
    }
}




// <!-- Khóa scroll khi mở Nav Mobile -->

function openNav() {
    document.getElementById("header-main-menu-desktop").style.width = "100%";
    document.getElementById("header-main-menu-desktop").style.transform = "translate3d(0, 0 ,0)";
    // body.style.transform = "translate3d(280px, 0 ,0)";   //Di chuyển body sẽ làm mất fixed nav
    body.classList.add("noscroll"); //Khóa con lăn
    //dim screen
    document.getElementById("dim").style.opacity = "1";
    document.getElementById("dim").style.visibility = "visible";
    document.getElementById("dim").style.transform = "translate3d(280px, 0 ,0)";
}

function closeNav() {
    document.getElementById("header-main-menu-desktop").style.width = "100%";
    document.getElementById("header-main-menu-desktop").style.transform = "translate3d(-280px, 0 ,0)";
    // body.style.transform = "translate3d(0, 0 ,0)";   	 //Di chuyển body sẽ làm mất fixed nav
    body.classList.remove("noscroll");
    //dim
    document.getElementById("dim").style.visibility = "hidden";
    document.getElementById("dim").style.transform = "translate3d(0, 0 ,0)";
    document.getElementById("dim").style.opacity = "0";
}

function openSearch() {
    document.getElementById("header-search-pc").style.transform = "translate3d(0, 0, 0)";
    body.classList.add("noscroll");
}

function closeSearch() {
    document.getElementById("header-search-pc").style.transform = "translate3d(0, 100%, 0)";
    body.classList.remove("noscroll");
}


var heso = 6;
function soslidethuonghieu() {
    var w = window.innerWidth;
    if (w > 1200) {
        heso = 6;
    } else if (w > 992) {
        heso = 5;
    }
    else if (w > 768) {
        heso = 4;
    }
    else if (w > 576) {
        heso = 3;
    } else heso = 2;
}

function goleft() {
    count += 1;
    var re = count * heso * 200;
    var step = heso * 200;
    var limit = Math.ceil(2000 / (heso * 200));
    if (count < (limit)) {
        var n = "translate3d(-" + re.toString() + "px, 0, 0)";
        document.getElementById("12345").style.transform = n;
    } else {
        count = 0;
        document.getElementById("12345").style.transform = "translate3d(0,0,0)";
    }
}

function goright() {
    count -= 1;
    var re = count * heso * 200;
    var step = heso * 200;
    var limit = Math.ceil(2000 / (heso * 200));
    var co = (limit - 1) * heso * 200;
    if (count > -1) {
        var n = "translate3d(-" + re.toString() + "px, 0, 0)";
        document.getElementById("12345").style.transform = n;
    } else {
        var n = "translate3d(-" + co.toString() + "px, 0, 0)";
        document.getElementById("12345").style.transform = n;
        count = limit - 1;
    }
}

function scrolltop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function setCookies(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000)); 	//Lưu 1 ngày
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    console.log(cname + "=" + cvalue + ";" + expires + ";path=/");
}

function setProvince() {
    var provinceValue = document.getElementById("province").value;
    console.log(provinceValue);

    setCookies("PROVINCE_ID_COOKIES", provinceValue, 5);
}

function searchs() {
    var i = document.getElementById("inputsticky").value,
        n = "https://pasgo.vn/tim-kiem?search=";
    location.replace(n + i);
}

// <!-- Dropdown thẳng đứng -->
var dropdown = document.getElementsByClassName("header-main-menu-button");
for (var i = dropdown.length - 1; i >= 0; i--) {
    dropdown[i].addEventListener("click", function () {
        var dropdownContent = this.nextElementSibling;
        var dropdownArrow = this.firstElementChild;
        if (dropdownContent.style.maxHeight === "0px") {
            dropdownContent.style.maxHeight = "600px";
            dropdownArrow.style.transform = "rotate(180deg)";
        } else {
            dropdownContent.style.maxHeight = "0px";
            dropdownArrow.style.transform = "rotate(0deg)";
        }
    });
}

// <!-- Slide ăn uống -->
var x = window.matchMedia("(max-width: 768px)");
$('#pasgo-carousel-menu').carousel({ interval: false })
$('#pasgo-carousel-menu-2').carousel({ interval: 30000 });
$('#pasgo-carousel-menu-3').carousel({ interval: 30000 });
$('#pasgo-carousel-menu-4').carousel({ interval: 30000 });
$('#pasgo-carousel-menu-5').carousel({ interval: 30000 });

		// Không cần thiết
		// $("#pasgo-carousel-menu-3 .carousel-inner .carousel-item").each(function(){
		//     var next = $(this).next();
		//     if (!next.length) {
		// 	    next = $(this).siblings(':first');      
		// 		}
		// 	next.children(':first-child').clone().appendTo($(this));
		// 	if(x.matches==false){
		// 		for (var i=0;i<4;i++) {
		// 			next=next.next();
		// 			if (!next.length) {
		// 			    next = $(this).siblings(':first');
		// 			}
		// 			next.children(':first-child').clone().appendTo($(this));
		// 		}
		// 	}

		// });
var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("demo");
    var captionText = document.getElementById("caption");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
    captionText.innerHTML = dots[slideIndex - 1].alt;
}

function chathistory(result) {
    var chat = $.connection.chatHub;
    $(".chat-right").remove();
    $(".chat-left").remove();
    $.each(result, function () {
        var result = this;
        chat.client.addNewMessageToPage(result.name, result.message, result.date, result.Status, result.IdUser);
    });
}

function sendmessage(name, message, date) {
    $(".notification1").hide();
    $(".notification2").hide();
    if (name == "@Session["FullName"]")
    $('#discussion').append('<li class="chat-right"><span class="timestamp">' + htmlEncode(date) + '</span><br /><span>' + htmlEncode(message) + '</span><strong> :' + htmlEncode(name)
        + '</strong></li>');
            else
    $('#discussion').append('<li class="chat-left"><span class="timestamp">' + htmlEncode(date) + '</span><br /><strong>' + htmlEncode(name)
        + '</strong>:<span>' + htmlEncode(message) + '</span></li>');
    $('#discussion').animate({ scrollTop: 99999999 }, 500);
}

function listconversation(id, name, date, status, idreceived) {
    if (htmlEncode(status) == "true")
        $('#list-discussion').append('<li class="list-discussion conversationopen" id="' + htmlEncode(id) + '" status="' + htmlEncode(status) + '" idreceived="' + htmlEncode(idreceived) + '">' + htmlEncode(name) + htmlEncode(date) + '</li>');
    else
        $('#list-discussion').append('<li class="list-discussion conversationclose" id="' + htmlEncode(id) + '" status="' + htmlEncode(status) + '" idreceived="' + htmlEncode(idreceived) + '">' + htmlEncode(name) + htmlEncode(date) + '</li>');
}

function notification(type) {
    switch (type) {
        case "1":
            $(".notification1").hide();
            $('#discussion').append('<li class="chat-middle notification1"><span>đang trả lời...</span></li>').children(':last').delay(3000).fadeOut(100);
            break;
        case "2":
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Đã đọc</span></li>');
            break;
        case "3":
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Không gửi được tin nhắn</span></li>');
            break;
        case "4":
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Cuộc hội thoại đã kết thúc! Để được hỗ trợ hãy tạo hội thoại mới!</span></li>');
            break;
        default:
            $(".notification2").hide();
            $('#discussion').append('<li class="chat-middle notification2"><span>Đã gửi</span></li>');
            break;
    }
    $('#discussion').animate({ scrollTop: 99999999 }, 500);
}

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

