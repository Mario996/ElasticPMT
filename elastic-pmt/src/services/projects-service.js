import { v4 as uuidv4 } from 'uuid'

async function getAllProjects () {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch('https://localhost:44332/projects', requestOptions)
        .then(response => response.json())
}

async function getProjectById (id) {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/projects/${id}`, requestOptions)
        .then(response => response.json())
}

async function createProject (project) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: uuidv4(),
            Name: project.name,
            Methodology: project.methodology,
            Tag: project.tag,
            Version: project.version,
            Manager: project.manager,
        }),
    }

    return fetch('https://localhost:44332/projects', requestOptions)
        .then(response => response.json())
}

async function updateProject (project, id) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: id,
            Name: project.name,
            Methodology: project.methodology,
            Tag: project.tag,
            Version: project.version,
            Manager: project.manager,
        }),
    }

    return fetch(`https://localhost:44332/projects/${id}`, requestOptions)
        .then(response => response.json())
}

async function deleteProject (id) {
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/projects/${id}`, requestOptions)
        .then(response => response.json())
}

export const projectsService = {
    createProject,
    getAllProjects,
    getProjectById,
    deleteProject,
    updateProject,
}
