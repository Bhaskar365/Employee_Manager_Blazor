function PrintBtn() {
    window.print();
}

window.BlazorDownloadFile = (fileName, data) => {
    const blob = base64toBlob(data, 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    document.body.appendChild(a);
    a.href = url;
    a.download = fileName;
    a.click();
    window.URL.revokeObjectURL(url);
};