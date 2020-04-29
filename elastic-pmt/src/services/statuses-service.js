import { v4 as uuidv4 } from 'uuid'

async function getAllStatuses () {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch('https://localhost:44332/statuses', requestOptions)
        .then(response => response.json())
}

async function getStatusById (id) {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/statuses/${id}`, requestOptions)
        .then(response => response.json())
}

async function createStatus (status) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: uuidv4(),
            Name: status.name,
        }),
    }

    return fetch('https://localhost:44332/statuses', requestOptions)
        .then(response => response.json())
}

async function updateStatus (status, id) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: id,
            Name: status.name,
        }),
    }

    return fetch(`https://localhost:44332/statuses/${id}`, requestOptions)
        .then(response => response.json())
}

async function deleteStatus (id) {
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/statuses/${id}`, requestOptions)
        .then(response => response.json())
}

export const statusesService = {
    createStatus,
    getAllStatuses,
    getStatusById,
    deleteStatus,
    updateStatus,
}
