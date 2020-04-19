<template>
  <ul id="example-1">
    <li v-for="requirement in requirements"
        :key="requirement.requirement_id">
      {{ requirement.source.requirement_id }}
      {{ requirement.source.requirement_version }}
      {{ requirement.source.requirement_status }}
      <v-btn
        @click="getThisRequirement(requirement.id)">
        GET
      </v-btn>
    </li>
  </ul>
</template>

<script>
import { requirementsService } from '../services/requirements-service'

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
        getThisRequirement (id) {
            requirementsService.getRequirementById(id)
                .then((response) => {
                    console.log(response)
                })
        }
    },
}
</script>
