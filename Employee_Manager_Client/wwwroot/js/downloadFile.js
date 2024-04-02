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

window.downloadFile = function (base64File, fileName) {
    // Convert base64 string to binary
    var binaryString = window.atob(base64File);
    var binaryLen = binaryString.length;
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }

    // Create blob object
    var blob = new Blob([bytes], { type: "application/octet-stream" });

    // Create download link
    var link = document.createElement("a");
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;

    // Trigger download
    link.click();
}