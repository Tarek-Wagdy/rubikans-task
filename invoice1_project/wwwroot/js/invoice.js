

var itemQuantity
var itemId

function addItem() {
    itemId = $("#itemId").val()
    itemQuantity = $("#itemQuantity").val()
    if (itemQuantity) {
        $.post("/invoice/add",
            {
                itemId: itemId,
                quantity: itemQuantity,
            },
            success
        )
    }
    else {
        alert('enter the item quantity')
        return
    }

    
}

function success(data) {
    getInvoiceCost();
    var taxValue = Number(data.price) * 0.14 * Number(itemQuantity);
    var totalCost = Number(data.price) * Number(itemQuantity) + taxValue;
    var tableBody = $("#tableBody")
    var tableRow = `
    <tr>
        <td>${data.name}</td>
        <td>${data.unit}</td>
        <td>${data.price}</td>
        <td>${itemQuantity}</td>
        <td>14%</td>
        <td>${taxValue}</td>
        <td>${totalCost}</td>
    <tr>`
    tableBody.append(tableRow)
}
function getInvoiceCost() {
    $.get("/invoice/GetTotalCost"
        ,updateCost
         )
}
function updateCost(data) {
    console.log(data);
    $("#costLabel").html(`Total Cost : ${data.cost} LE`);
    $("#taxLabel").html(`Total tax : ${data.tax} LE`);
    $("#netCostLabel").html(`Net Total Cost : ${data.netCost} LE`);
}