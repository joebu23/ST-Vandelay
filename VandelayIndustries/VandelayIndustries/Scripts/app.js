
var items = [];

function CreateTransaction() {
    var date = $("#Date").val();
    var buyer = $("#Buyers option:selected").val();
    var seller = $("#Sellers option:selected").val();
    var sales = $("#SalesPersons option:selected").val();

    if (date == "") {
        alert('Please Input Date');
        return
    }
    if (buyer == "") {
        alert('Please Select a Buyer');
        return
    }
    if (seller == "") {
        alert('Please Select a Seller');
        return
    }
    if (sales == "") {
        alert('Please Select a Sales Person');
        return
    }
    if (items == "") {
        alert('Please Select an Item or Items');
        return
    }

    var model = {};
    var url = "/Transaction/Create"
    model.Date = date;
    model.Seller = seller;
    model.Buyer = buyer;
    model.SalesPerson = sales;
    model.Items = items;

    $.ajax({
        url: url,
        method: "post",
        data: model
    }).success(function (data) {
        if (data == "good") {
            window.location = "/Transaction/Index";
        } else {
            alert("There was a problem saving your Transaction");
        }
    });

}

function ItemAdd() {
    var item = $("#Items option:selected");

    if (item.val() != "") {
        var node = document.createElement("DIV");
        var textNode = document.createTextNode(item.text());
        node.appendChild(textNode);

        //item.parent().parent().append(node);
        $("#item-list").append(node);

        items.push(item.val());
    }
}


