function processTransaction(endPoint) {

    var items = [];
    $('#item-list').children('div').each(function () {
        items.push($(this).attr("Value"));
    });

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
    var url = "/Transaction/" + endPoint;
    if (endPoint == "edit") {
        model.Id = $("#Id").attr("value");
    }
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
    var items = [];
    $('#item-list').children('div').each(function () {
        items.push($(this).attr("Value"));
    });

    var item = $("#ItemId option:selected");

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
    }
}

function RemoveItem(item) {
    $("#item-list > div[value='" + item + "']").remove();
}

$.fn.dataTableExt.afnFiltering.push(
				function (oSettings, aData, iDataIndex) {

				    if (oSettings.nTable.id != 'transaction-list') {
				        return true;
				    }
				    else {
				        var today = new Date();
				        var dd = today.getDate();
				        var mm = today.getMonth() + 1;
				        var yyyy = today.getFullYear();

				        if (dd < 10)
				            dd = '0' + dd;

				        if (mm < 10)
				            mm = '0' + mm;

				        today = mm + '/' + dd + '/' + yyyy;

				        var iMin_temp = $('#begin-date').val();
				        if (iMin_temp == '') {
				            iMin_temp = '01/01/1980';
				        }

				        var iMax_temp = $('#end-date').val();
				        if (iMax_temp == '') {
				            iMax_temp = '12/31/2999';
				        }

				        var arr_min = iMin_temp.split("/");
				        var arr_max = iMax_temp.split("/");
				        var arr_date = aData[0].split("/");

				        var iMin = new Date(arr_min[2], arr_min[0], arr_min[1], 0, 0, 0, 0)
				        var iMax = new Date(arr_max[2], arr_max[0], arr_max[1], 0, 0, 0, 0)
				        var iDate = new Date(arr_date[2], arr_date[0], arr_date[1], 0, 0, 0, 0)

				        if (iMin == "" && iMax == "") {
				            return true;
				        }
				        else if (iMin == "" && iDate < iMax) {
				            return true;
				        }
				        else if (iMin <= iDate && "" == iMax) {
				            return true;
				        }
				        else if (iMin <= iDate && iDate <= iMax) {
				            return true;
				        }
				        return false;
				    }

				}
			);

$(document).ready(
  function () {
      $(".datepicker").datepicker({
          changeMonth: true,
          changeYear: true,
      });

      $(function () {
          $("#begin-date").datepicker();
      });

      $(function () {
          $("#end-date").datepicker();
      });

      var oTable = $('#transaction-list').dataTable();

      $("#sales-transaction-list").DataTable({
          order: [[1, 'desc']]
      });

      $('#begin-date').change(function () { oTable.fnDraw(); });
      $('#end-date').change(function () { oTable.fnDraw(); });

      $(".clickable-row").click(function () {
          window.document.location = $(this).data("href");
      });
  });

function filterSalesTable() {
    var startDate = $("#begin-sales-date").val();
    var endDate = $("#end-sales-date").val();
    var sales = parseInt($("#SalesPersons option:selected").val());

    if (isNaN(sales)) {
        alert('Please Select a Sales Person');
        return
    }

    var model = {};
    model.SalesPersonCommissionDateRangeLow = startDate;
    model.SalesPersonCommissionDateRangeHigh = endDate;
    model.SalesPersonIdToGet = sales;

    $.ajax({
        url: "/Admin/FilterSalesCommissionTable",
        method: "post",
        data: model
    }).success(function (data) {
        console.log(data);
        $("#commission-table-body").html(data);
    });

    var x = 1;
}
