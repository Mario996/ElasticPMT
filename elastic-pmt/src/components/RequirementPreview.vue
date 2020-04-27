<template>
  <v-card
    class="mx-auto my-3"
    max-width="344">
    <v-card-text class="px-2 pt-2 pb-0">
      <v-row justify="center">
        <h3 class="mb-2 text--primary">
          {{ requirement.objectValue.name }}
        </h3>
      </v-row>
      <p class="mb-2 text--primary">
        Version: {{ requirement.objectValue.version }}
      </p>
      <p class="mb-2 text--primary">
        Type: {{ requirement.objectValue.type }}
      </p>
      <p class="text--primary mb-0">
        Status: {{ requirement.objectValue.status }}
      </p>
    </v-card-text>
    <v-card-actions class="text-center">
      <v-row>
        <v-col cols="6"
               class="px-0">
          <v-btn @click="editRequirement">
            Edit
          </v-btn>
        </v-col>
        <v-col cols="6"
               class="px-0">
          <v-btn @click="deleteRequirement">
            Delete
          </v-btn>
        </v-col>
      </v-row>
    </v-card-actions>
    <v-row>
      <v-col cols="12"
             class="py-0 pl-5">
        <p class="mb-2 text--primary">
          Comments: {{ requirement.objectValue.comments.length }}
        </p>
      </v-col>
    </v-row>
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
        editRequirement () {
            router.push({ name: 'requirement', params: { requirement: this.requirement.objectValue, documentId: this.requirement.objectValue.id } })
        },
        deleteRequirement () {
            this.$emit('delete-requirement', this.requirement.objectValue.id)
        }
    },
}
</script>
