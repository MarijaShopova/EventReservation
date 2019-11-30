

function validate_step1() {
    var name = document.getElementById("name");
    var desc = document.getElementById("desc");
    var city = document.getElementById("city");
   
    var flag = 1;
    if (name.value == "") {
        name.focus();
        name.style.borderWidth = "2px";
        name.style.borderColor = "red";
        flag = 0;

    }
    if (desc.value == "") {
        desc.focus();
        desc.style.borderWidth = "2px";
        desc.style.borderColor = "red";
        flag = 0;
    }
    if (city.value == "") {
        city.focus();
        city.style.borderWidth = "2px";
        city.style.borderColor = "red";
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
    var street = document.getElementById("street");
    var streetNo = document.getElementById("streetNo");
    var opens = document.getElementById("openingH");
    var closes = document.getElementById("closingH");

    var flag = 1;
    if (street.value == "") {
        street.focus();
        street.style.borderWidth = "2px";
        street.style.borderColor = "red";
        flag = 0;
    }
    if (streetNo.value == "") {
        streetNo.focus();
        streetNo.style.borderWidth = "2px";
        streetNo.style.borderColor = "red";
        flag = 0;
    }
    if (opens.value == "") {
        opens.focus();
        opens.style.borderWidth = "2px";
        opens.style.borderColor = "red";
        flag = 0;
    }
    if (closes.value == "") {
        closes.focus();
        closes.style.borderWidth = "2px";
        closes.style.borderColor = "red";
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
    var tables = document.getElementById("NoOfTables");
    var parking = document.getElementById("parking");
    
    var flag = 1;
    if (tables.value == "") {
        tables.focus();
        tables.style.borderWidth = "2px";
        tables.style.borderColor = "red";
        flag = 0;
    }
    if (parking.value == "") {
        parking.focus();
        parking.style.borderWidth = "2px";
        parking.style.borderColor = "red";
        flag = 0;
    }
    
    if (flag == 0) {
        document.getElementById("text-validation").style.display = "block";
    }
    

}


/*---------------------------------------------------------*/
// Function that executes on click of first next button.
function next_step1() {


    document.getElementById("first").style.display = "none";
    document.getElementById("second").style.display = "block";
    document.getElementById("active2").style.color = "red";

}
// Function that executes on click of first previous button.
function prev_step1() {

    document.getElementById("text-validation").style.display = "none";
    document.getElementById("name").style.borderColor = "black";
    document.getElementById("desc").style.borderColor = "black";
    document.getElementById("city").style.borderColor = "black";
    document.getElementById("second").style.display = "none";
    document.getElementById("active1").style.color = "red";
    document.getElementById("active2").style.color = "gray";
}
// Function that executes on click of second next button.
function next_step2() {

   
    document.getElementById("second").style.display = "none";
    document.getElementById("third").style.display = "block";
    document.getElementById("active3").style.color = "red";
}
// Function that executes on click of second previous button.
function prev_step2() {
    ocument.getElementById("text-validation").style.display = "none";
    document.getElementById("street").style.borderColor = "black";
    document.getElementById("streetNo").style.borderColor = "black";
    document.getElementById("opens").style.borderColor = "black";
    document.getElementById("closes").style.display = "block";
    document.getElementById("third").style.display = "none";
    document.getElementById("second").style.display = "block";
    document.getElementById("active2").style.color = "red";
    document.getElementById("active3").style.color = "gray";
}