async function createRequirement (requirement) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            requirement_id: requirement.requirementId,
            requirement_version: requirement.requirementVersion,
            requirement_description: requirement.requirementDescription,
            requirement_rationale: requirement.requirementRationale,
            requirement_type: requirement.requirementType,
            requirement_status: requirement.requirementStatus,
        }),
    }

    return fetch('https://localhost:44332/requirements', requestOptions)
}

export const requirementsService = {
    createRequirement,
}
