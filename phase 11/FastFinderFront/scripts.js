const indexName = 'test-index'

function onLoad() {
    const btnAddDocument = document.getElementById('btn-add-document')
    const btnCheckPing = document.getElementById('btn-check-ping')
    const searchInputBox = document.getElementById('search-input-box');
    const searchIcon = document.getElementById('search-icon');

    searchInputBox.addEventListener('keyup', function (event) {
        if (event.code === 'Enter') {
            searchIcon.click();
        }
    })

    btnAddDocument.addEventListener('click', onAddDocumentBtnClicked)
    btnCheckPing.addEventListener('click', onCheckPingBtnClicked)
    searchIcon.addEventListener('click', onSearchBtnClicked)
}

function onCheckPingBtnClicked() {
    const request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            switch (this.status) {
                case 500:
                    alert("Server error could not :(")
                    break;
                case 200:
                    alert("Your connection is OK :)")
                    break;
            }
        }
    }
    request.open("GET", getPingUrl());
    request.send();
}

function onAddDocumentBtnClicked() {
    window.location.href = './addDocument.html';
}

function onSearchBtnClicked() {
    const resultsElement = document.getElementsByClassName('results')[0]
    const resultEmpty = document.getElementsByClassName('result-empty')[0];
    const searchInputBox = document.getElementById('search-input-box');
    const request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            switch (this.status) {
                case 200:
                    const results = JSON.parse(this.responseText);
                    resultsElement.innerHTML = resultListToElementContent(results);
                    resultEmpty.style.display = 'none';
                    break;
                case 404:
                    resultsElement.innerHTML = resultListToElementContent([]);
                    resultEmpty.style.display = 'block';
                    break;
            }
        }
    }
    request.open("GET", getSearchUrl(indexName, searchInputBox.value));
    request.send();
}

function resultObjToElement(item) {
    return `<span class="result-card"><span class="card-header">${item.id}</span><span class="card-details">${item.content}</span></span>`
}

function resultListToElementContent(results) {
    let content = '';
    for (const item of results) {
        content += resultObjToElement(item);
    }
    return content;
}

function getPingUrl() {
    return "https://localhost:5001/api/v1/Document/ping";
}

function getSearchUrl(indexName, query) {
    return `https://localhost:5001/api/v1/Document/${indexName}?query=${query}`
}
