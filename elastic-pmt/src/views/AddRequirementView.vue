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
                                name="Requirement name"
                                rules="required|max:30">
              <v-text-field
                v-model="requirement.name"
                :error-messages="errors"
                label="Requirement name"
                required />
            </ValidationProvider>
            <v-row>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Project"
                                    rules="required">
                  <v-select
                    v-model="requirement.project"
                    :error-messages="errors"
                    :items="projects"
                    label="Project"
                    item-text="name"
                    item-value="name"
                    dense
                    outlined
                    return-object />
                </ValidationProvider>
              </v-col>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Requirement author"
                                    rules="required|max:100">
                  <v-autocomplete
                    v-model="requirement.author"
                    :error-messages="errors"
                    :items="users"
                    dense
                    outlined
                    item-text="email"
                    item-value="email"
                    label="Requirement author"
                    required
                    return-object />
                </ValidationProvider>
              </v-col>
            </v-row>
            <v-row>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Requirement version"
                                    rules="required|max:10">
                  <v-text-field
                    v-model="requirement.version"
                    class="pt-0"
                    :error-messages="errors"
                    label="Requirement version"
                    required />
                </ValidationProvider>
              </v-col>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Requirement priority"
                                    rules="required">
                  <v-autocomplete
                    v-model="requirement.priority"
                    :error-messages="errors"
                    :items="priorities"
                    dense
                    outlined
                    item-text="name"
                    item-value="name"
                    label="Requirement priority"
                    required
                    return-object />
                </ValidationProvider>
              </v-col>
            </v-row>
            <ValidationProvider v-slot="{ errors }"
                                name="Requirement description"
                                rules="required|max:500|min:10">
              <v-textarea
                v-model="requirement.description"
                solo
                no-resize
                name="input-7-4"
                label="Requirement description"
                height="8vh"
                required
                :error-messages="errors"
                :counter="500" />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Requirement rationale"
                                rules="required|max:500|min:10">
              <v-textarea
                v-model="requirement.rationale"
                solo
                no-resize
                name="input-7-4"
                height="8vh"
                label="Requirement rationale"
                :error-messages="errors"
                :counter="500" />
            </ValidationProvider>
            <v-row>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Requirement type"
                                    rules="required">
                  <v-select
                    v-model="requirement.type"
                    :error-messages="errors"
                    :items="requirementTypes"
                    label="Requirement type"
                    dense
                    outlined />
                </ValidationProvider>
              </v-col>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Requirement status"
                                    rules="required">
                  <v-select
                    v-model="requirement.status"
                    :error-messages="errors"
                    :items="requirementStatuses"
                    label="Requirement status"
                    dense
                    outlined />
                </ValidationProvider>
              </v-col>
            </v-row>
            <v-row justify="center">
              <v-btn
                class="mr-4"
                width="300"
                color="primary"
                @click="submitForm">
                {{ mode }}
              </v-btn>
            </v-row>
          </form>
          <v-row v-if="mode !== 'CREATE'">
            <v-col cols="12">
              <label>Comments:</label>
              <CommentComponent v-for="comment in requirement.comments"
                                :key="comment.id"
                                :comment="comment"
                                @update-comment="updateComment"
                                @delete-comment="deleteComment" />
            </v-col>
          </v-row>
          <v-row v-if="mode !== 'CREATE'"
                 justify="center">
            <v-col cols="10">
              <v-textarea
                v-model="comment"
                solo
                height="8vh"
                name="input-7-4"
                label="Comment"
                no-resize
                :counter="500" />
            </v-col>
            <v-col cols="2">
              <v-btn
                class="mb-4"
                color="primary"
                @click="addComment">
                ADD COMMENT
              </v-btn>
            </v-col>
          </v-row>
        </ValidationObserver>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { required, max, min } from 'vee-validate/dist/rules'
import { extend, ValidationObserver, ValidationProvider, setInteractionMode } from 'vee-validate'
import CommentComponent from '../components/CommentComponent'
import { requirementsService } from '../services/requirements-service'
import router from '../router/index'
import { v4 as uuidv4 } from 'uuid'
import { usersService } from '../services/users-service'
import { projectsService } from '../services/projects-service'
import { prioritiesService } from '../services/priorities-service'

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
        CommentComponent,
    },
    props: {
        requirementObject: { type: Object, default: () => {} },
    },
    data: () => ({
        requirement: {},
        mode: 'CREATE',
        comment: '',
        requirementTypes: ['Functional requirement', 'Nonfunctional requirement'],
        requirementStatuses: ['Waiting for revision', 'Approved', 'Declined'],
        users: [],
        projects: [],
        priorities: [],
    }),
    created () {
        if (this.requirementObject !== undefined) {
            this.mode = 'UPDATE'
            this.requirement = this.requirementObject
        }
        usersService.getAllUsers()
            .then((response) => {
                this.users = response.map(x => x.source)
            })
        projectsService.getAllProjects()
            .then((response) => {
                this.projects = response.map(x => x.source)
            })
        prioritiesService.getAllPriorities()
            .then((response) => {
                this.priorities = response.map(x => x.source)
            })
    },
    methods: {
        submitForm () {
            this.$refs.observer.validate().then((result) => {
                if (result) {
                    if (this.mode === 'UPDATE') {
                        requirementsService.updateRequirement(this.requirement, this.requirement.id)
                            .then(() => {
                                router.push('/list-requirements')
                            })
                    } else {
                        requirementsService.createRequirement(this.requirement)
                            .then(() => {
                                router.push('/list-requirements')
                            })
                    }
                }
            })
        },
        addComment () {
            this.requirement.comments.push({ text: this.comment, id: uuidv4() })
            requirementsService.updateRequirement(this.requirement, this.requirement.id)
            this.comment = ''
        },
        updateComment (comment) {
            var commentIndexToBeUpdated = this.requirement.comments.findIndex(x => x.id === comment.id)
            this.requirement.comments.splice(commentIndexToBeUpdated, 1, comment)
            requirementsService.updateRequirement(this.requirement, this.requirement.id)
        },
        deleteComment (comment) {
            var commentIndexToBeDeleted = this.requirement.comments.findIndex(x => x.id === comment.id)
            this.requirement.comments.splice(commentIndexToBeDeleted, 1)
            requirementsService.updateRequirement(this.requirement, this.requirement.id)
        },
    },
}
</script>
