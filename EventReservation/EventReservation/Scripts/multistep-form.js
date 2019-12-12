

function validate_step1() {
    var genre = document.getElementById("genre");
    var desc = document.getElementById("description");
    var band = document.getElementById("band");
    var title = document.getElementById("title");
    var flag = 1;
    if (genre.value == "") {
        genre.focus();
        genre.style.borderWidth = "2px";
        genre.style.borderColor = "red";
        flag = 0;

        }
    if (desc.value == "") {
        desc.focus();
        desc.style.borderWidth = "2px";
        desc.style.borderColor = "red";
        flag = 0;
    }
    if (band.value == "") {
        band.focus();
        band.style.borderWidth = "2px";
         band.style.borderColor = "red";
         flag = 0;
    }
    if (title.value == "") {
        title.focus();
        title.style.borderWidth = "2px";
         title.style.borderColor = "red";
         flag = 0;
    }
    if (flag == 0) {
        document.getElementById("text-validation").style.display = "block";
                }
    if (flag == 1) {
       
        next_step1();
    }
}

function validate_step2() {
    var start = document.getElementById("start");
    var end = document.getElementById("end");
    var datepicker = document.getElementById("datepicker");
    var flag = 1;
    if (start.value == "") {
        start.focus();
        start.style.borderWidth = "2px";
        start.style.borderColor = "red";
        flag = 0;
    }
    if (end.value == "") {
        end.focus();
        end.style.borderWidth = "2px";
        end.style.borderColor = "red";
        flag = 0;
    }
    if (datepicker.value == "") {
        datepicker.focus();
        datepicker.style.borderWidth = "2px";
        datepicker.style.borderColor = "red";
        flag = 0;
    }

    if (flag == 0) {
        document.getElementById("text-validation").style.display = "block";
    }
    if (flag == 1) {

        next_step2();
    }
}


function validate_step3() {
    var start = document.getElementById("ticket");
    var end = document.getElementById("price");
    var datepicker = document.getElementById("table");
    var flag = 1;
    if (ticket.value == "") {
        ticket.focus();
        ticket.style.borderWidth = "2px";
        ticket.style.borderColor = "red";
        flag = 0;
    }
    if (price.value == "") {
        price.focus();
        price.style.borderWidth = "2px";
        price.style.borderColor = "red";
        flag = 0;
    }
    if (table.value == "") {
        table.focus();
        table.style.borderWidth = "2px";
        table.style.borderColor = "red";
        flag = 0;
    }

    if (flag == 0) {
        document.getElementById("text-validation").style.display = "block";
    }
    if (flag == 1) {

       
    }

}


/*---------------------------------------------------------*/
// Function that executes on click of first next button.
function next_step1() {
    document.getElementById("text-validation").style.display = "none";
    document.getElementById("title").style.borderColor = "black";
    document.getElementById("genre").style.borderColor = "black";
    document.getElementById("description").style.borderColor = "black";
    document.getElementById("band").style.borderColor = "black";
    
        document.getElementById("first").style.display = "none";
        document.getElementById("second").style.display = "block";
        document.getElementById("active2").style.color = "red";
 
}
// Function that executes on click of first previous button.
function prev_step1() {

    document.getElementById("text-validation").style.display = "none";
    document.getElementById("start").style.borderColor = "black";
    document.getElementById("end").style.borderColor = "black";
    document.getElementById("datepicker").style.borderColor = "black";
    document.getElementById("first").style.display = "block";
    document.getElementById("second").style.display = "none";
    document.getElementById("active1").style.color = "red";
    document.getElementById("active2").style.color = "gray";
}
// Function that executes on click of second next button.
function next_step2() {
    document.getElementById("text-validation").style.display = "none";
    document.getElementById("start").style.borderColor = "black";
    document.getElementById("end").style.borderColor = "black";
    document.getElementById("datepicker").style.borderColor = "black";
    document.getElementById("second").style.display = "none";
    document.getElementById("third").style.display = "block";
    document.getElementById("active3").style.color = "red";
}
// Function that executes on click of second previous button.
function prev_step2() {
    document.getElementById("third").style.display = "none";
    document.getElementById("second").style.display = "block";
    document.getElementById("active2").style.color = "red";
    document.getElementById("active3").style.color = "gray";
}