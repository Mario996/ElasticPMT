<template>
  <v-container>
    <ProjectPreview v-for="project in projects"
                    :key="project.id"
                    :project="project"
                    @delete-project="deleteProject" />
  </v-container>
</template>

<script>
import { projectsService } from '../services/projects-service'
import ProjectPreview from '../components/ProjectPreview'

export default {
    components: {
        ProjectPreview,
    },
    data: () => ({
        projects: []
    }),
    created () {
        projectsService.getAllProjects()
            .then((response) => {
                this.projects = response.map(x => {
                    return {
                        objectValue: x.source,
                    }
                })
            })
    },
    methods: {
        deleteProject (id) {
            this.projects = this.projects.filter(x => x.objectValue.id !== id)
            projectsService.deleteProject(id)
        }
    },
}
</script>
