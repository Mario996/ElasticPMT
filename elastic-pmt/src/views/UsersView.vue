<template>
  <v-data-table
    :headers="headers"
    :items="users"
    class="elevation-1"
    :search="search">
    <template v-slot:top>
      <v-toolbar flat
                 color="white">
        <v-toolbar-title>ElasticPMT users</v-toolbar-title>
        <v-divider
          class="mx-4"
          inset
          vertical />
        <v-col cols="4">
          <v-text-field
            v-model="search"
            append-icon="mdi-magnify"
            label="Search"
            single-line
            hide-details />
        </v-col>
        <v-spacer />
        <v-dialog v-model="dialog"
                  max-width="500px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary"
                   dark
                   class="mb-2"
                   v-on="on">
              New User
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12"
                         sm="6"
                         md="6">
                    <v-text-field v-model="editedUser.companyId"
                                  label="Company id" />
                  </v-col>
                  <v-col cols="12"
                         sm="6"
                         md="6">
                    <v-text-field v-model="editedUser.email"
                                  label="Email" />
                  </v-col>
                  <v-col cols="12"
                         sm="6"
                         md="6">
                    <v-text-field v-model="editedUser.firstName"
                                  label="First name" />
                  </v-col>
                  <v-col cols="12"
                         sm="6"
                         md="6">
                    <v-text-field v-model="editedUser.lastName"
                                  label="Last name" />
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer />
              <v-btn color="blue darken-1"
                     text
                     @click="close">
                Cancel
              </v-btn>
              <v-btn color="blue darken-1"
                     text
                     @click="save">
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon
        small
        class="mr-2"
        @click="editUser(item)">
        mdi-pencil
      </v-icon>
      <v-icon
        small
        @click="deleteUser(item)">
        mdi-delete
      </v-icon>
    </template>
  </v-data-table>
</template>

<script>
import { usersService } from '../services/users-service'

export default {
    data: () => ({
        search: '',
        dialog: false,
        headers: [
            {
                text: 'Company id',
                align: 'start',
                value: 'companyId',
            },
            { text: 'Email', value: 'email' },
            { text: 'First name', value: 'firstName' },
            { text: 'Last name', value: 'lastName' },
            { text: 'Actions', value: 'actions', sortable: false },
        ],
        users: [],
        editedIndex: -1,
        editedUser: {
            id: '',
            companyId: '',
            firstName: '',
            lastName: '',
        },
        defaultUser: {
            id: '',
            companyId: '',
            firstName: '',
            lastName: '',
        },
    }),
    computed: {
        formTitle () {
            return this.editedIndex === -1 ? 'New User' : 'Edit User'
        },
    },
    watch: {
        dialog (val) {
            // eslint-disable-next-line no-unused-expressions
            val || this.close()
        },
    },
    created () {
        usersService.getAllUsers()
            .then((response) => {
                this.users = response.map(x => x.source)
            })
    },
    methods: {
        editUser (item) {
            this.editedIndex = this.users.indexOf(item)
            this.editedUser = Object.assign({}, item)
            this.dialog = true
        },
        deleteUser (item) {
            const index = this.users.indexOf(item)
            // eslint-disable-next-line no-unused-expressions
            confirm('Are you sure you want to delete this user?') && this.users.splice(index, 1)
            usersService.deleteUser(item.id)
        },
        close () {
            this.dialog = false
            setTimeout(() => {
                this.editedUser = Object.assign({}, this.defaultUser)
                this.editedIndex = -1
            }, 300)
        },
        save () {
            if (this.editedIndex > -1) {
                Object.assign(this.users[this.editedIndex], this.editedUser)
                usersService.updateUser(this.editedUser, this.editedUser.id)
            } else {
                this.users.push(this.editedUser)
                usersService.createUser(this.editedUser)
            }
            this.close()
        },
    },
}
</script>
