window.downloadFile = function (base64String) {

    // Create temporary <a> element
    var anchor = document.createElement('a');
    anchor.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + base64String;
    anchor.target = "_blank";
    anchor.download = "YourExcelFile.xlsx";

    // Append the <a> element to the document body
    document.body.appendChild(anchor);

    // Programmatically click the anchor to trigger the download
    anchor.click();

    // Clean up: remove the anchor from the document body
    document.body.removeChild(anchor);
}