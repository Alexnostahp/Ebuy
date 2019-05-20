// AJAX functionallity for Items (index?)


let selectCurrency = document.querySelector("#selectCurrency");
let convertButton = document.querySelector("#convertButton");

let priceTexts = document.querySelectorAll("#priceValue");
let lastSelection = "sek";



//Make an ajax call to local file for convertion numbers
convertButton.addEventListener("click", function () {
    $.ajax({
        url: "http://localhost:51714/items/getFile",
        dataType: 'text',
        success: function (data) {

            //Make it an json object
            var JSONobj = JSON.parse(data);


            //The three diffrent values
            var sekusd = JSONobj.sekusd;
            var usdsek = JSONobj.usdsek;
           

            //make the users selection the current one
            let selection = selectCurrency.value;

            

            if (selection == "usd" && lastSelection != "usd") {
                lastSelection = "usd";
                setPrice(sekusd);
            }
            else if (selection == "sek" && lastSelection != "sek") {
                lastSelection = "sek";
                setPrice(usdsek);
            }


            function setPrice(convertionrate) {
                //Set the price of the items * values
                priceTexts.forEach(price => {
                    let currentPriceAsString = price.innerHTML;

                    let convertedPrice = currentPriceAsString * convertionrate;

                    convertedPrice = convertedPrice.toFixed(1);

                    price.innerHTML = convertedPrice;
                });
            }
        }
    });

});