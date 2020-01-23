function validate_step1() {
    //alert("fleva")
    var title = document.getElementById("title");
    var desc = document.getElementById("description");
    var performer = document.getElementById("performer");
    var table = document.getElementById("tables");

    var flag = 1;
    refresh1();
    if (title.value == "") {
        title.focus();
        title.style.borderWidth = "2px";
        title.style.borderColor = "red";
        document.getElementById("TitleH").innerHTML = "Please enter a value for genre";
        document.getElementById("TitleH").style.display = "block";
        document.getElementById("TitleH").style.color = "red";
        flag = 0;
    }

    if (desc.value == "") {
        desc.focus();
        desc.style.borderWidth = "2px";
        desc.style.borderColor = "red";
        document.getElementById("DescriptionH").innerHTML = "We accept only letters as value of description.";
        document.getElementById("DescriptionH").style.display = "block";
        document.getElementById("DescriptionH").style.color = "red";
        flag = 0;
    }

    if (performer.value == "") {
        performer.focus();
        performer.style.borderWidth = "2px";
        performer.style.borderColor = "red";
        document.getElementById("PerformerH").innerHTML = "Please enter a value for performer";
        document.getElementById("PerformerH").style.display = "block";
        document.getElementById("PerformerH").style.color = "red";
        flag = 0;
    }

    if (table.value == "") {
        table.focus();
        table.style.borderWidth = "2px";
        table.style.borderColor = "red";
        document.getElementById("tableH").innerHTML = "Please enter a value for number of tables";
        document.getElementById("tableH").style.display = "block";
        document.getElementById("tableH").style.color = "red";
        flag = 0;
    }



    if (flag != 0) {
        
        next_step1();
    }
}

function validate_step2() {
    var start = document.getElementById("start");
    var end = document.getElementById("end");
    var datepicker = document.getElementById("datepicker");
    var flag = 1;
    refresh2();
    if (start.value == "") {
        start.focus();
        start.style.borderWidth = "2px";
        start.style.borderColor = "red";
        document.getElementById("startH").innerText = "We only accept HH:MM format for the value of opening hour";
        document.getElementById("startH").style.display = "block";
        document.getElementById("startH").style.color = "red";
        flag = 0;
    }
  
    if (end.value == "") {
        end.focus();
        end.style.borderWidth = "2px";
        end.style.borderColor = "red";
        document.getElementById("endH").innerText = "We only accept HH:MM format for the value of closing hour";
        document.getElementById("endH").style.display = "block";
        document.getElementById("endH").style.color = "red";
        flag = 0;
    }
    
    if (datepicker.value == "") {
        datepicker.focus();
        datepicker.style.borderWidth = "2px";
        datepicker.style.borderColor = "red";
        document.getElementById("datepickerH").innerText = "Please choose date!";
        document.getElementById("datepickerH").style.display = "block";
        document.getElementById("datepickerH").style.color = "red";
        flag = 0;
    }

    if (flag == 1) {
        document.getElementById("active3").style.color = "#004E64;";
        next_step2();
        //document.getElementById("active3").style.color = "#004E64;";
    }
}

function validate_step3() {
    var ticket = document.getElementById("ticket");
    var price = document.getElementById("price");
    var table = document.getElementById("table");
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
    if (!(/[0-9]*$/.test(price.value))) {
        document.getElementById("CloseH").innerText = "We accept only numbers for the value of price ex:150";
        document.getElementById("CloseH").style.display = "block";
        flag = 0;
    }
    if (table.value == "") {
        table.focus();
        table.style.borderWidth = "2px";
        table.style.borderColor = "red";
        flag = 0;
    }


    if (flag = 0) {
        document.getElementById("text-validation").style.display = "block";
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
    document.getElementById("active1").style.color = "#004E64;";
    document.getElementById("active2").style.color = "gray";
    //document.getElementById("start").style.borderColor = "black";
    document.getElementById("end").style.borderColor = "black";
    document.getElementById("datepicker").style.borderColor = "black";
    //document.getElementById("GenreH").style.display = "none";
    document.getElementById("TitleH").style.display = "none";
    document.getElementById("DescriptionH").style.display = "none";
}

function refresh1() {
    var title = document.getElementById("title");
    var desc = document.getElementById("description");
    var performer = document.getElementById("performer");
    var table = document.getElementById("tables");
 
    document.getElementById("TitleH").style.display = "none";
    document.getElementById("DescriptionH").style.display = "none";
  
    title.style.borderColor = "black";
    desc.style.borderColor = "black";
    table.style.borderColor = "black";
    performer.style.borderColor = "black";
}

function refresh2() {
    document.getElementById("startH").style.display = "none";
    document.getElementById("endH").style.display = "none";
    document.getElementById("datepickerH").style.display = "none";
    document.getElementById("start").style.borderColor = "black";
    document.getElementById("end").style.borderColor = "black";
    document.getElementById("datepicker").style.borderColor = "black";
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
    document.getElementById("active2").style.color = "#004E64;";
    document.getElementById("active3").style.color = "gray";
}


