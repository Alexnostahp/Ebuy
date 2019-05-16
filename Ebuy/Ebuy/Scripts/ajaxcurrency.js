// AJAX functionallity for Items (index?)


let selectCurrency = document.querySelector("#selectCurrency");
let convertButton = document.querySelector("#convertButton");

let priceTexts = document.querySelectorAll("#priceValue");


//Make an ajax call to local file for convertion numbers
convertButton.addEventListener("click", function () {
    $.ajax({
        url: "http://localhost:51714/items/getFile",
        dataType: 'text',
        success: function (data) {

            //Make it an json object
            var JSONobj = JSON.parse(data);


            //The three diffrent values
            var usd = JSONobj.usd;

            var eur = JSONobj.eur;

            var sek = JSONobj.sek;

            //make the users selection the current one
            let selection = selectCurrency.value;

            var current = "";

            if (selection == "usd") {
                current = usd;
            }
            else if (selection == "eur") {
                current = eur;
            }
            else if (selection == "sek") {
                current = sek;
            }


            //Set the price of the items * values
            priceTexts.forEach(price => {
                let currentPriceAsString = price.innerHTML;

                let convertedPrice = currentPriceAsString * current;

                price.innerHTML = convertedPrice;
            });


        }
    });

});