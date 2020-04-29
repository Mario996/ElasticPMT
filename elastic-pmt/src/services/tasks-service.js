import { v4 as uuidv4 } from 'uuid'

async function getAllTasks () {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch('https://localhost:44332/tasks', requestOptions)
        .then(response => response.json())
}

async function getTaskById (id) {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/tasks/${id}`, requestOptions)
        .then(response => response.json())
}

async function createTask (task) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: uuidv4(),
            Summary: task.summary,
            Component: task.component,
            Type: task.type,
            Priority: task.priority,
            Assignee: task.assignee,
            Environment: task.environment,
            Description: task.description,
            Status: task.status,
            Comments: [],
        }),
    }

    return fetch('https://localhost:44332/tasks', requestOptions)
        .then(response => response.json())
}

async function updateTask (task, id) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: id,
            Summary: task.summary,
            Component: task.component,
            Type: task.type,
            Priority: task.priority,
            Assignee: task.assignee,
            Environment: task.environment,
            Description: task.description,
            Comments: task.comments,
            Status: task.status
        }),
    }

    return fetch(`https://localhost:44332/tasks/${id}`, requestOptions)
        .then(response => response.json())
}

async function deleteTask (id) {
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/tasks/${id}`, requestOptions)
        .then(response => response.json())
}

export const tasksService = {
    createTask,
    getAllTasks,
    getTaskById,
    deleteTask,
    updateTask,
}
