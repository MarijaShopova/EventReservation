
function validate_step2() {

    var street = document.getElementById("street");
    var streetNo = document.getElementById("streetNo");
    var opens = document.getElementById("opening_hour");
    var closes = document.getElementById("closing_hour");

    document.getElementById("StreetNoH").innerText = "";
    document.getElementById("StreetH").innerText = "";
    document.getElementById("opening_hour").innerText = "";
    document.getElementById("closing_hour").innerText = "";

    var flag = 1;
    if (street.value == "") {
        street.focus();

        document.getElementById("StreetH").innerText = "Street value is manadatory";
        document.getElementById("StreetH").style.display = "block";
        document.getElementById("StreetH").style.color = "red";
        street.style.borderWidth = "2px";
        street.style.borderColor = "red";
        flag = 0;
    }
    if (streetNo.value == 0) {
        streetNo.focus();
        streetNo.style.borderWidth = "2px";
        streetNo.style.borderColor = "red";
        flag = 0;
        document.getElementById("StreetNoH").style.display = "block";
        document.getElementById("StreetNoH").innerText = "Street No. value is manadatory";
        document.getElementById("StreetNoH").style.display = "block";
        document.getElementById("StreetNoH").style.color = "red";
    }

    if (opens.value == "") {
        opens.focus();
        opens.style.borderWidth = "2px";
        opens.style.borderColor = "red";
        flag = 0;
        document.getElementById("opening_hour").innerText = "Please enter time in HH:MM format";
        document.getElementById("opening_hour").style.display = "block";
        document.getElementById("opening_hour").style.color = "red";
    }

    if (!(/^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/.test(opens.value))) {
        opens.style.borderWidth = "2px";
        opens.style.borderColor = "red";
        document.getElementById("opening_hour").innerText = "Please enter time in HH:MM format";
        document.getElementById("opening_hour").style.display = "block";
        document.getElementById("opening_hour").style.color = "red";
        flag = 0;
    }

    
    if (closes.value == "") {  
        closes.focus();
        closes.style.borderWidth = "2px";
        closes.style.borderColor = "red";
        flag = 0;
        document.getElementById("closing_hour").innerText = "Please enter time in HH:MM format";
        document.getElementById("closing_hour").style.display = "block";
        document.getElementById("closing_hour").style.color = "red";

    }

     if (!(/^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/.test(closes.value))) {
        closes.style.borderWidth = "2px";
        closes.style.borderColor = "red";
        flag = 0;
         document.getElementById("closing_hour").innerText = "Please enter time in HH:MM format";
         document.getElementById("closing_hour").style.display = "block";
         document.getElementById("closing_hour").style.color = "red";
        flag = 0;
    }
   
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
        document.getElementById("descH").innerHTML = "Please enter a description";
        document.getElementById("descH").style.color = "red";
        
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
    document.getElementById("active1").style.color = "#004E64";
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
    document.getElementById("active2").style.color = "#004E64";
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
