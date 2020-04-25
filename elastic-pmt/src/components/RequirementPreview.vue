<template>
  <v-card
    class="mx-auto my-3"
    max-width="344">
    <v-card-text class="px-2 pt-2 pb-0">
      <v-row justify="center">
        <h3 class="mb-2 text--primary">
          {{ requirement.objectValue.requirement_name }}
        </h3>
      </v-row>
      <p class="mb-2 text--primary">
        Version: {{ requirement.objectValue.requirement_version }}
      </p>
      <p class="mb-2 text--primary">
        Type: {{ requirement.objectValue.requirement_type }}
      </p>
      <p class="text--primary mb-0">
        Status: {{ requirement.objectValue.requirement_status }}
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
          <v-btn @click="deleteRequirement">
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
        requirement: { type: Object, default: () => {} },
    },
    data: () => ({
    }),
    methods: {
        openDetails () {
            router.push({ name: 'requirement', params: { requirement: this.requirement.objectValue, documentId: this.requirement.id } })
        },
        deleteRequirement () {
            this.$emit('delete-requirement', this.requirement.id)
        }
    },
}
</script>
