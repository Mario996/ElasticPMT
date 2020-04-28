import { v4 as uuidv4 } from 'uuid'

async function getAllUsers () {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch('https://localhost:44332/users', requestOptions)
        .then(response => response.json())
}

async function getUserById (id) {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/users/${id}`, requestOptions)
        .then(response => response.json())
}

async function createUser (user) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: uuidv4(),
            CompanyId: user.companyId,
            FirstName: user.firstName,
            LastName: user.lastName,
            Email: user.email,
        }),
    }

    return fetch('https://localhost:44332/users', requestOptions)
        .then(response => response.json())
}

async function updateUser (user, id) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Id: id,
            CompanyId: user.companyId,
            FirstName: user.firstName,
            LastName: user.lastName,
            Email: user.email,
        }),
    }

    return fetch(`https://localhost:44332/users/${id}`, requestOptions)
        .then(response => response.json())
}

async function deleteUser (id) {
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    }

    return fetch(`https://localhost:44332/users/${id}`, requestOptions)
        .then(response => response.json())
}

export const usersService = {
    createUser,
    getAllUsers,
    getUserById,
    deleteUser,
    updateUser,
}
