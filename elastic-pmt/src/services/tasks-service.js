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
            Summary: task.taskSummary,
            Component: task.taskComponent,
            Type: task.taskType,
            Priority: task.taskPriority,
            Assignee: task.taskAssignee,
            Environment: task.taskEnvironment,
            Description: task.taskDescription,
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
            Summary: task.taskSummary,
            Component: task.taskComponent,
            Type: task.taskType,
            Priority: task.taskPriority,
            Assignee: task.taskAssignee,
            Environment: task.taskEnvironment,
            Description: task.taskDescription,
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
