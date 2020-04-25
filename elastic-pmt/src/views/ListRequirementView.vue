<template>
  <v-container>
    <RequirementPreview v-for="requirement in requirements"
                        :key="requirement.requirement_name"
                        :requirement="requirement"
                        @delete-requirement="deleteRequirement" />
  </v-container>
</template>

<script>
import { requirementsService } from '../services/requirements-service'
import router from '../router/index'
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
                        id: x.id,
                        objectValue: x.source,
                    }
                })
            })
    },
    methods: {
        clear () {
        },
        updateRequirement (id) {
            requirementsService.getRequirementById(id)
                .then((response) => {
                    router.push({ name: 'requirement', params: { requirement: response.source, documentId: response.id } })
                })
        },
        deleteRequirement (id) {
            this.requirements = this.requirements.filter(x => x.id !== id)
            requirementsService.deleteRequirement(id)
                .then((response) => {
                    console.log(response)
                })
        }
    },
}
</script>
