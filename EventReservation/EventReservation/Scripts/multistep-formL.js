
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
        document.getElementById("StreetH").style.display = "block";
        flag = 0;
    }
    if (streetNo.value == "") {
        streetNo.focus();
        streetNo.style.borderWidth = "2px";
        streetNo.style.borderColor = "red";
        flag = 0;
        document.getElementById("StreetNoH").style.display = "block";
    }
    status = /^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/.test(opens.value);
    if (opens.value == "") {
        opens.focus();
        opens.style.borderWidth = "2px";
        opens.style.borderColor = "red";
        flag = 0;
        document.getElementById("OpenH").style.display = "block";
    }

    else if (!(/^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/.test(opens.value))) {
        document.getElementById("OpenH").style.display = "block";
        flag = 0;
    }
    else {
        flag = 1;
    }

    if (closes.value == "") {
        closes.focus();
        closes.style.borderWidth = "2px";
        closes.style.borderColor = "red";
        flag = 0;
        document.getElementById("CloseH").style.display = "block";
    }

    else if (!(/^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/.test(closes.value))) {
        document.getElementById("CloseH").style.display = "block";
        flag = 0;
    }
    else { flag = 1; }
    if (flag != 0) {
        next_step2();
    }

}

function validate_step3() {
   
    var desc = document.getElementById("desc");

    if (desc.value == "") {
        desc.focus();
        desc.style.borderWidth = "2px";
        desc.style.borderColor = "red";
        flag = 0;
        document.getElementById("descH").style.display = "block";
    }
 


}

/*---------------------------------------------------------*/
// Function that executes on click of first next button.
function next_step1() {

    document.getElementById("first").style.display = "none";
    document.getElementById("second").style.display = "block";
    document.getElementById("active2").style.color = "#004E64";
}
// Function that executes on click of first previous button.
function prev_step1() {


    document.getElementById("first").style.display = "block";
    document.getElementById("second").style.display = "none";
    document.getElementById("active1").style.color = "red";
    document.getElementById("active2").style.color = "lightgray";

}
// Function that executes on click of second next button.
function next_step2() {

    document.getElementById("second").style.display = "none";
    document.getElementById("third").style.display = "block";
    document.getElementById("active3").style.color = "#004E64";
}
// Function that executes on click of second previous button.
function prev_step2() {

    document.getElementById("third").style.display = "none";
    document.getElementById("second").style.display = "block";
    document.getElementById("active2").style.color = "red";
    document.getElementById("active3").style.color = "gray";
    document.getElementById("StreetH").style.display = "none";
    document.getElementById("StreetNoH").style.display = "none";
    document.getElementById("OpenH").style.display = "none";
    document.getElementById("CloseH").style.display = "none";
    document.getElementById("street").style.borderColor = "black";
    document.getElementById("streetNo").style.borderColor = "black";
    document.getElementById("openingH").style.borderColor = "black";
    document.getElementById("closingH").style.borderColor = "black";
}
