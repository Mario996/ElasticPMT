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
            <ValidationProvider v-slot="{ errors }"
                                name="Requirement version"
                                rules="required|max:10">
              <v-text-field
                v-model="requirement.version"
                :error-messages="errors"
                label="Requirement version"
                required />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Requirement description"
                                rules="required|max:500|min:10">
              <v-textarea
                v-model="requirement.description"
                solo
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
                name="input-7-4"
                height="8vh"
                label="Requirement rationale"
                :error-messages="errors"
                :counter="500" />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Requirement type"
                                rules="required">
              <v-radio-group v-model="requirement.type"
                             row
                             :error-messages="errors"
                             label="Requirement type:">
                <v-radio label="Functional requirement"
                         value="Functional requirement" />
                <v-radio label="Nonfunctional requirement"
                         value="Nonfunctional requirement" />
              </v-radio-group>
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Requirement status"
                                rules="required">
              <v-radio-group v-model="requirement.status"
                             row
                             :error-messages="errors"
                             label="Requirement status:">
                <v-radio label="Waiting for revision"
                         value="Waiting for revision" />
                <v-radio label="Approved"
                         value="Approved" />
                <v-radio label="Declined"
                         value="Declined" />
              </v-radio-group>
            </ValidationProvider>
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
    }),
    created () {
        if (this.requirementObject !== undefined) {
            this.mode = 'UPDATE'
            this.requirement = this.requirementObject
        }
    },
    methods: {
        submitForm () {
            this.$refs.observer.validate()
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
