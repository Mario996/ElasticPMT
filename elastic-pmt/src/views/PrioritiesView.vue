<template>
  <v-row align="center"
         justify="center">
    <v-col
      sm="8"
      md="6"
      lg="4">
      <v-data-table
        :headers="headers"
        :items="priorities"
        class="elevation-1">
        <template v-slot:top>
          <v-toolbar flat
                     color="white">
            <v-toolbar-title>Priority of requirements</v-toolbar-title>
            <v-divider
              class="mx-4"
              inset
              vertical />
            <v-spacer />
            <v-dialog v-model="dialog"
                      max-width="500px">
              <template v-slot:activator="{ on }">
                <v-btn color="primary"
                       dark
                       class="mb-2"
                       v-on="on">
                  New Priority
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="6"
                             sm="6"
                             md="6">
                        <v-text-field v-model="editedPriority.name"
                                      label="Name" />
                      </v-col>
                      <v-col cols="6"
                             sm="6"
                             md="6">
                        <v-text-field v-model="editedPriority.value"
                                      label="Value" />
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
            @click="editPriority(item)">
            mdi-pencil
          </v-icon>
          <v-icon
            small
            @click="deletePriority(item)">
            mdi-delete
          </v-icon>
        </template>
      </v-data-table>
    </v-col>
    <v-col>
      <v-textarea type="text"
               v-model="search"
               @input="searchControls" />
    </v-col>
  </v-row>
</template>

<script>
import { prioritiesService } from '../services/priorities-service'
import { searchService } from '../services/search-service'

export default {
    data: () => ({
        dialog: false,
        search: '',
        headers: [
            {
                text: 'Name',
                align: 'start',
                value: 'name',
            },
            { text: 'Value', value: 'value' },
            { text: 'Actions', value: 'actions', sortable: false },
        ],
        priorities: [],
        editedIndex: -1,
        editedPriority: {
            id: '',
            name: '',
            value: 0,
        },
        defaultPriority: {
            id: '',
            name: '',
            value: 0,
        },
    }),
    computed: {
        formTitle () {
            return this.editedIndex === -1 ? 'New Priority' : 'Edit Priority'
        },
    },
    watch: {
        dialog (val) {
            // eslint-disable-next-line no-unused-expressions
            val || this.close()
        },
    },
    created () {
        prioritiesService.getAllPriorities()
            .then((response) => {
                this.priorities = response.map(x => x.source)
            })
    },
    methods: {
        editPriority (item) {
            this.editedIndex = this.priorities.indexOf(item)
            this.editedPriority = Object.assign({}, item)
            this.dialog = true
        },
        deletePriority (item) {
            const index = this.priorities.indexOf(item)
            // eslint-disable-next-line no-unused-expressions
            confirm('Are you sure you want to delete this priority?') && this.priorities.splice(index, 1)
            prioritiesService.deletePriority(item.id)
        },
        close () {
            this.dialog = false
            setTimeout(() => {
                this.editedPriority = Object.assign({}, this.defaultPriority)
                this.editedIndex = -1
            }, 300)
        },
        save () {
            if (this.editedIndex > -1) {
                Object.assign(this.priorities[this.editedIndex], this.editedPriority)
                prioritiesService.updatePriority(this.editedPriority, this.editedPriority.id)
            } else {
                this.priorities.push(this.editedPriority)
                prioritiesService.createPriority(this.editedPriority)
            }
            this.close()
        },
        searchControls () {
            searchService.search(this.search).then((response) => {
                console.log(response)
            })
        }
    },
}
</script>
