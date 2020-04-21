<template>
  <ul id="example-1">
    <li v-for="requirement in requirements"
        :key="requirement.requirement_id">
      {{ requirement.source.requirement_id }}
      {{ requirement.source.requirement_version }}
      {{ requirement.source.requirement_status }}
      <v-btn
        @click="updateRequirement(requirement.id)">
        UPDATE
      </v-btn>
      <v-btn
        @click="deleteRequirement(requirement.id)">
        DELETE
      </v-btn>
    </li>
  </ul>
</template>

<script>
import { requirementsService } from '../services/requirements-service'
import router from '../router/index'

export default {
    components: {

    },
    data: () => ({
        requirements: []
    }),
    created () {
        requirementsService.getAllRequirements()
            .then((response) => {
                this.requirements = response
            })
    },
    methods: {
        clear () {
        },
        updateRequirement (id) {
            requirementsService.getRequirementById(id)
                .then((response) => {
                    router.push({ name: 'add-requirement', params: { requirement: response[0].source } })
                })
        },
        deleteRequirement (id) {
            requirementsService.deleteRequirement(id)
                .then((response) => {
                    console.log(response)
                })
        }
    },
}
</script>
