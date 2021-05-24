const indexName = 'test-index'


function onLoad() {
    const btnHomePage = document.getElementById('btn-home');
    const btnAddDocument = document.getElementById('btn-add-document')

    btnHomePage.addEventListener('click', onHomeButtonClicked)
    btnAddDocument.addEventListener('click', onAddDocumentClicked)
}

function onHomeButtonClicked() {
    window.location.href = './index.html'
}

function onAddDocumentClicked() {
    const docId = document.getElementById('input-document-id').value
    const docContent = document.getElementById('input-document-content').value

    // send request to server
    const request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 201) {
                alert("Document created.");
            } else {
                alert("Could not create the document");
            }
        }
    }
    request.open("POST", getAddDocumentUrl(indexName))
    request.setRequestHeader("Content-Type", "application/json");
    const data = {id: docId, content: docContent};
    request.send(JSON.stringify(data));
}


function getAddDocumentUrl(indexName) {
    return `https://localhost:5001/api/v1/Document/${indexName}`
}
