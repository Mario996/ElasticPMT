import { v4 as uuidv4 } from 'uuid'

async function getAllRequirements () {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch('https://localhost:44332/requirements', requestOptions)
        .then(response => response.json())
}

async function getRequirementById (id) {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/requirements/${id}`, requestOptions)
        .then(response => response.json())
}

async function createRequirement (requirement) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: uuidv4(),
            Name: requirement.name,
            Version: requirement.version,
            Description: requirement.description,
            Rationale: requirement.rationale,
            Type: requirement.type,
            Status: requirement.status,
            Comments: [],
        }),
    }

    return fetch('https://localhost:44332/requirements', requestOptions)
        .then(response => response.json())
}

async function updateRequirement (requirement, id) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: id,
            Name: requirement.name,
            Version: requirement.version,
            Description: requirement.description,
            Rationale: requirement.rationale,
            Type: requirement.type,
            Status: requirement.status,
            Comments: requirement.comments,
        }),
    }

    return fetch(`https://localhost:44332/requirements/${id}`, requestOptions)
        .then(response => response.json())
}

async function deleteRequirement (id) {
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/requirements/${id}`, requestOptions)
        .then(response => response.json())
}

export const requirementsService = {
    createRequirement,
    getAllRequirements,
    getRequirementById,
    deleteRequirement,
    updateRequirement,
}
