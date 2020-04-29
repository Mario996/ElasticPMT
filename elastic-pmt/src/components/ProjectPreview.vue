<template>
  <v-card
    class="mx-auto my-3"
    max-width="344">
    <v-card-text class="px-2 pt-2 pb-0">
      <v-row justify="center">
        <v-col class="py-0">
          <h3 class="mb-2 text--primary">
            {{ project.objectValue.name }}
          </h3>
        </v-col>
      </v-row>
      <p class="mb-2 text--primary">
        Version: {{ project.objectValue.version }}
      </p>
      <p class="mb-2 text--primary">
        Methodology: {{ project.objectValue.methodology }}
      </p>
      <p class="mb-2 text--primary">
        Manager: {{ project.objectValue.manager.email }}
      </p>
    </v-card-text>
    <v-card-actions class="text-center py-0">
      <v-row>
        <v-col cols="6">
          <v-btn @click="openDetails">
            Edit
          </v-btn>
        </v-col>
        <v-col cols="6">
          <v-btn @click="deleteProject">
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
        project: { type: Object, default: () => {} },
    },
    data: () => ({
    }),
    methods: {
        openDetails () {
            router.push({ name: 'project', params: { projectObject: this.project.objectValue, documentId: this.project.objectValue.id } })
        },
        deleteProject () {
            this.$emit('delete-project', this.project.objectValue.id)
        }
    },
}
</script>
