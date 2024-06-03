const uri = 'api/Entrants';
let entrants = [];

function getEntrants() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayEntrants(data))
        .catch(error => console.error('Unable to get entrants.', error));
}

function addEntrant() {
    const addFirstNameTextbox = document.getElementById('add-firstname');
    const addLastNameTextbox = document.getElementById('add-lastname');
    const addNationalExamGradeTextbox = document.getElementById('add-nationalexamgrade');
    const addIsPrivilegedCheckbox = document.getElementById('add-isprivileged');

    const entrant = {
        FirstName: addFirstNameTextbox.value.trim(),
        LastName: addLastNameTextbox.value.trim(),
        NationalExamGrade: parseFloat(addNationalExamGradeTextbox.value.trim()),
        IsPrivileged: addIsPrivilegedCheckbox.checked
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(entrant)
    })
        .then(response => response.json())
        .then(() => {
            getEntrants();
            addFirstNameTextbox.value = "";
            addLastNameTextbox.value = "";
            addNationalExamGradeTextbox.value = "";
            addIsPrivilegedCheckbox.checked = false;
        })
        .catch(error => console.error('Unable to add entrant.', error));
}

function deleteEntrant(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getEntrants())
        .catch(error => console.error('Unable to delete entrant.', error));
}

function displayEditForm(id) {
    const entrant = entrants.find(entrant => entrant.id === id);

    document.getElementById('edit-id').value = entrant.id;
    document.getElementById('edit-firstname').value = entrant.firstName;
    document.getElementById('edit-lastname').value = entrant.lastName;
    document.getElementById('edit-nationalexamgrade').value = entrant.nationalExamGrade;
    document.getElementById('edit-isprivileged').checked = entrant.isPrivileged;
    document.getElementById('editEntrant').style.display = 'block';
}

function updateEntrant() {
    const entrantId = document.getElementById('edit-id').value;
    const entrant = {
        id: parseInt(entrantId, 10),
        firstName: document.getElementById('edit-firstname').value.trim(),
        lastName: document.getElementById('edit-lastname').value.trim(),
        nationalExamGrade: parseFloat(document.getElementById('edit-nationalexamgrade').value.trim()),
        isPrivileged: document.getElementById('edit-isprivileged').checked
    };

    fetch(`${uri}/${entrantId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(entrant)
    })
        .then(() => getEntrants())
        .catch(error => console.error('Unable to update entrant.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editEntrant').style.display = 'none';
}

function _displayEntrants(data) {
    const tBody = document.getElementById('Entrants');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(entrant => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${entrant.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteEntrant(${entrant.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(entrant.firstName);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(entrant.lastName);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(entrant.nationalExamGrade);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        let textNode4 = document.createTextNode(entrant.isPrivileged ? 'Yes' : 'No');
        td4.appendChild(textNode4);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    entrants = data;
}
