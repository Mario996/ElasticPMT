<template>
  <v-container>
    <RequirementPreview v-for="requirement in requirements"
                        :key="requirement.id"
                        :requirement="requirement"
                        @delete-requirement="deleteRequirement" />
  </v-container>
</template>

<script>
import { requirementsService } from '../services/requirements-service'
import RequirementPreview from '../components/RequirementPreview'

export default {
    components: {
        RequirementPreview,
    },
    data: () => ({
        requirements: []
    }),
    created () {
        requirementsService.getAllRequirements()
            .then((response) => {
                this.requirements = response.map(x => {
                    return {
                        objectValue: x.source,
                    }
                })
            })
    },
    methods: {
        clear () {
        },
        deleteRequirement (id) {
            this.requirements = this.requirements.filter(x => x.objectValue.id !== id)
            requirementsService.deleteRequirement(id)
                .then((response) => {
                    console.log(response)
                })
        }
    },
}
</script>
