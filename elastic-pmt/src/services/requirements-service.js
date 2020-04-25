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
            requirement_name: requirement.requirementName,
            requirement_version: requirement.requirementVersion,
            requirement_description: requirement.requirementDescription,
            requirement_rationale: requirement.requirementRationale,
            requirement_type: requirement.requirementType,
            requirement_status: requirement.requirementStatus,
        }),
    }

    return fetch('https://localhost:44332/requirements', requestOptions)
}

async function updateRequirement (requirement, id) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            requirement_name: requirement.requirementName,
            requirement_version: requirement.requirementVersion,
            requirement_description: requirement.requirementDescription,
            requirement_rationale: requirement.requirementRationale,
            requirement_type: requirement.requirementType,
            requirement_status: requirement.requirementStatus,
        }),
    }

    return fetch(`https://localhost:44332/requirements/${id}`, requestOptions)
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
