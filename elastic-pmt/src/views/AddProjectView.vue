<template>
  <v-container
    fluid
    class="fill-height">
    <v-row align="center"
           justify="center">
      <v-col cols="12"
             md="6"
             lg="6">
        <ValidationObserver ref="observer">
          <form>
            <ValidationProvider v-slot="{ errors }"
                                name="Project name"
                                rules="required|max:50">
              <v-text-field
                v-model="project.name"
                :error-messages="errors"
                label="Project name"
                required />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Project tag"
                                rules="required|max:20">
              <v-text-field
                v-model="project.tag"
                :error-messages="errors"
                label="Project tag"
                required />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Project version"
                                rules="required|max:20">
              <v-text-field
                v-model="project.version"
                :error-messages="errors"
                label="Project version"
                required />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Project methodology"
                                rules="required">
              <v-select
                v-model="project.methodology"
                :error-messages="errors"
                :items="projectMethodologies"
                label="Project methodology"
                dense
                outlined />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Project manager"
                                rules="required">
              <v-autocomplete
                v-model="project.manager"
                :error-messages="errors"
                :items="users"
                dense
                outlined
                item-text="email"
                item-value="email"
                label="Project manager"
                required
                return-object />
            </ValidationProvider>
            <v-row justify="center">
              <v-btn class="mr-4"
                     width="300"
                     color="primary"
                     @click="submitForm">
                {{ mode }}
              </v-btn>
            </v-row>
          </form>
        </ValidationObserver>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { required, max, min } from 'vee-validate/dist/rules'
import { extend, ValidationObserver, ValidationProvider, setInteractionMode } from 'vee-validate'
import { projectsService } from '../services/projects-service'
import router from '../router/index'
import { usersService } from '../services/users-service'

setInteractionMode('eager')

extend('required', {
    ...required,
    message: '{_field_} can not be empty.',
})

extend('max', {
    ...max,
    message: '{_field_} may not be greater than {length} characters.',
})

extend('min', {
    ...min,
    message: '{_field_} may not be lesser than {length} characters.',
})

export default {
    components: {
        ValidationProvider,
        ValidationObserver,
    },
    props: {
        projectObject: { type: Object, default: () => {} },
    },
    data: () => ({
        project: {},
        mode: 'CREATE',
        users: [],
        projectMethodologies: ['Kanban', 'Scrum', 'Bug tracking'],
    }),
    created () {
        if (this.projectObject !== undefined) {
            this.mode = 'UPDATE'
            this.project = this.projectObject
        }
        usersService.getAllUsers()
            .then((response) => {
                this.users = response.map(x => x.source)
            })
    },
    methods: {
        submitForm () {
            this.$refs.observer.validate().then((result) => {
                if (result) {
                    if (this.mode === 'UPDATE') {
                        projectsService.updateProject(this.project, this.project.id)
                            .then(() => {
                                router.push('/list-projects')
                            })
                    } else {
                        projectsService.createProject(this.project)
                            .then(() => {
                                router.push('/list-projects')
                            })
                    }
                }
            })
        },
    },
}
</script>
