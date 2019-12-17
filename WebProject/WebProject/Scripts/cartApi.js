function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function addItem() {
    let book = event.currentTarget.children[0].getAttribute("id")
    let numberOfBooksToBuy = document.getElementsByClassName("sach-soluong")
    let booksLength = numberOfBooksToBuy.length
    let checkCart = getCookie("cart")
    if (!checkCart) {
        for (var i = 0; i < booksLength; i++) {
            let findingBook = numberOfBooksToBuy[i].getAttribute("id")
            if (findingBook == book) {
                document.cookie = "cart=" + findingBook + "," + numberOfBooksToBuy[i].value
                break
            }
        }
        alert("added to your cart")
    } else {
        for (var i = 0; i < booksLength; i++) {
            let findingBook = numberOfBooksToBuy[i].getAttribute("id")
            if (findingBook == book) {
                document.cookie = "cart=" + checkCart + "," + findingBook + "," + numberOfBooksToBuy[i].value
                break
            }
        }
        alert("added to your cart")
    }
}
function buyItems() {
    let data = {}
    post("/Book/Buy", data, (resJson) => {
        document.cookie = "cart="
        alert(resJson.message)
        window.location = "/Book/Index"
    })
}