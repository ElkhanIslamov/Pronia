const loadMoreBtn = document.getElementById("loadMoreBtn");
const productBox = document.getElementById("productBox");
const productCount = document.getElementById("productCount").value;
const productDetails = document.querySelectorAll(".product-detail");
const productModal = document.getElementById("product-modal-content");
const addToBasketBtns = document.querySelectorAll(".add-to-basket");
const basketItems = document.getElementById("basket-items");
const subtotal = document.getElementById("subtotal");
const totalCount = document.getElementById("total-count");


let skip = 8
loadMoreBtn.addEventListener("click", function () {

    let url = `/Shop/LoadMore?skip=${skip}`;
    fetch(url).then(response => response.text())
        .then(data => productBox.innerHTML +=data)

    skip += 8;
    if (skip >= productCount) {
        loadMoreBtn.remove()
    }
    
});

productDetails.forEach(productDetail => {
    productDetail.addEventListener("click", function (e) {
        e.preventDefault();
        console.log(productModal)

        let url = this.getAttribute("href");
        fetch(url).then(response => response.text())
            .then(data => {
                productModal.innerHTML = data
            });
    })
})
addToBasketBtns.forEach(addToBasketBtn => {
    addToBasketBtn.addEventListener("click", function (e) {
        e.preventDefault();

        let url = this.getAttribute("href");

        fetch(url).then(response => response.text())
            .then(data => {
                basketItems.innerHTML = data;

                let countValue = Number(totalCount.innerText);
                countValue++;
                totalCount.innerText = countValue;

                sum = 0;
                var totalPrices = basketItems.querySelectorAll(".total-price")
                totalPrices.forEach(totalPrice => {
                    sum += Number(totalPrice.value);
                })
                subtotal.innerText = "$" + sum

            })

    })
})