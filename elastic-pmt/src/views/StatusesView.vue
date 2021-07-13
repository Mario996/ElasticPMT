<template>
  <v-row align="center"
         justify="center">
    <v-col
      sm="8"
      md="6"
      lg="4">
      <v-data-table
        :headers="headers"
        :items="statuses"
        class="elevation-1">
        <template #top>
          <v-toolbar flat
                     color="white">
            <v-toolbar-title>Status of tasks</v-toolbar-title>
            <v-divider
              class="mx-4"
              inset
              vertical />
            <v-spacer />
            <v-dialog v-model="dialog"
                      max-width="500px">
              <template #activator="{ on }">
                <v-btn color="primary"
                       dark
                       class="mb-2"
                       v-on="on">
                  New Status
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
                             sm="12"
                             md="12">
                        <v-text-field v-model="editedStatus.name"
                                      label="Name" />
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
        <template>
          <v-icon
            small
            class="mr-2"
            @click="editStatus(item)">
            mdi-pencil
          </v-icon>
          <v-icon
            small
            @click="deleteStatus(item)">
            mdi-delete
          </v-icon>
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import { statusesService } from '../services/statuses-service'

export default {
    data: () => ({
        dialog: false,
        headers: [
            {
                text: 'Name',
                align: 'start',
                value: 'name',
            },
            { text: 'Actions', value: 'actions', sortable: false },
        ],
        statuses: [],
        editedIndex: -1,
        editedStatus: {
            id: '',
            name: '',
        },
        defaultStatus: {
            id: '',
            name: '',
        },
    }),
    computed: {
        formTitle () {
            return this.editedIndex === -1 ? 'New Status' : 'Edit Status'
        },
    },
    watch: {
        dialog (val) {
            // eslint-disable-next-line no-unused-expressions
            val || this.close()
        },
    },
    created () {
        statusesService.getAllStatuses()
            .then((response) => {
                this.statuses = response.map(x => x.source)
            })
    },
    methods: {
        editStatus (item) {
            this.editedIndex = this.statuses.indexOf(item)
            this.editedStatus = Object.assign({}, item)
            this.dialog = true
        },
        deleteStatus (item) {
            const index = this.statuses.indexOf(item)
            // eslint-disable-next-line no-unused-expressions
            confirm('Are you sure you want to delete this status?') && this.statuses.splice(index, 1)
            statusesService.deleteStatus(item.id)
        },
        close () {
            this.dialog = false
            setTimeout(() => {
                this.editedStatus = Object.assign({}, this.defaultStatus)
                this.editedIndex = -1
            }, 300)
        },
        save () {
            if (this.editedIndex > -1) {
                Object.assign(this.statuses[this.editedIndex], this.editedStatus)
                statusesService.updateStatus(this.editedStatus, this.editedStatus.id)
            } else {
                this.statuses.push(this.editedStatus)
                statusesService.createStatus(this.editedStatus)
            }
            this.close()
        },
    },
}
</script>
