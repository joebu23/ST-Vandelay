
var items = [];

function CreateTransaction() {
    var date = $("#Date").val();
    var buyer = $("#BuyerId option:selected").val();
    var seller = $("#SellerId option:selected").val();
    var sales = $("#SalesPersonId option:selected").val();

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

function EditTransaction() {
    var items = [];
    $('#item-list').children('div').each(function () {
        //alert($(this).attr("Value")); // "this" is the current element in the loop
        items.push($(this).attr("Value"));
    });

    //console.log(items);

    var id = $("#Id").attr("value");
    var date = $("#Date").val();
    var buyer = $("#BuyerId option:selected").val();
    var seller = $("#SellerId option:selected").val();
    var sales = $("#SalesPersonId option:selected").val();

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
    var url = "/Transaction/Edit"
    model.Id = id;
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
    var item = $("#ItemId option:selected");

    if (item.val() != "") {
        var node = document.createElement("DIV");
        var textNode = document.createTextNode(item.text());
        node.appendChild(textNode);

        $("#item-list").append(node);

        items.push(item.val());
    }
}

function ItemAddEdit() {
    var item = $("#Items option:selected");

    var test = item.val();
    if (item.val() != "") {
        var minusNode = document.createElement("SPAN");
        minusNode.setAttribute("class", "glyphicon glyphicon-minus-sign");
        minusNode.setAttribute("style", "padding-left: 5px;");
        minusNode.setAttribute("onclick", "RemoveItem(" + item.val() + ")");

        var node = document.createElement("DIV");
        node.setAttribute("Value", item.val());
        var textNode = document.createTextNode(item.text());
        node.appendChild(textNode);
        node.appendChild(minusNode);

        $("#item-list").append(node);

        items.push(item.val());


        //<div>@item.Description<span class="glyphicon glyphicon-minus-sign" Value="@item.Id" style="padding-left: 5px;" onclick="RemoveItem(this)"></span></div>
    }
}

function RemoveItem(item) {
    $("#item-list > div[value='" + item + "']").remove();

    //item.parentNode.parentNode.removeChild(item.parentNode.parentNode.childNodes[1]);
    //var x = 1;
}


