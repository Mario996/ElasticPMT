<template>
  <v-card
    class="mx-auto my-3"
    max-width="344">
    <v-card-text class="px-2 pt-2 pb-0">
      <v-row justify="center">
        <h3 class="mb-2 text--primary">
          {{ task.objectValue.summary }}
        </h3>
      </v-row>
      <p class="mb-2 text--primary">
        Priority: {{ task.objectValue.priority }}
      </p>
      <p class="mb-2 text--primary">
        Assignee: {{ task.objectValue.assignee }}
      </p>
    </v-card-text>
    <v-card-actions class="text-center">
      <v-row>
        <v-col cols="6">
          <v-btn @click="openDetails">
            Edit
          </v-btn>
        </v-col>
        <v-col cols="6">
          <v-btn @click="deleteTask">
            Delete
          </v-btn>
        </v-col>
      </v-row>
    </v-card-actions>
  </v-card>
</template>

<script>
import router from '../router/index'

export default {
    props: {
        task: { type: Object, default: () => {} },
    },
    data: () => ({
    }),
    methods: {
        openDetails () {
            router.push({ name: 'task', params: { task: this.task.objectValue, documentId: this.task.id } })
        },
        deleteTask () {
            this.$emit('delete-task', this.task.id)
        }
    },
}
</script>
