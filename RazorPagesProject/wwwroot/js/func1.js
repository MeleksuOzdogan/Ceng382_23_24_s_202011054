function hidingFunc() {
    var elements = document.getElementById("sign-in-entery");
    if (elements.style.display === "none" || elements.style.display === "") {
        elements.style.display = "block";
    } 
    else {
            elements.style.display = "none";
    }
}

function hidingForm() {
    var elements = document.getElementById("additional-form");
    if (elements.style.display === "none" || elements.style.display === "") {
        elements.style.display = "block";
    } 
    else {
        elements.style.display = "none";
    }
}
function calculateSum() {
    console.log("Calculate Sum button clicked");
    var firstNumber = parseFloat(document.getElementById("firstNumber").value);
    var secondNumber = parseFloat(document.getElementById("secondNumber").value);
    var sum = firstNumber + secondNumber;
    var resultElement = document.getElementById("result");
    if (resultElement.style.display === "none" || resultElement.innerText === "") {
        // Sonucu göster
        resultElement.innerText = "The sum of " + firstNumber + " and " + secondNumber + " is: " + sum;
        resultElement.style.display = "block";
    } else {
        // Sonucu gizle
        resultElement.innerText = ""; // Sonucu temizle
        resultElement.style.display = "none";
    }
}