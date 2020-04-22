<template>
  <ValidationObserver ref="observer">
    <form ref="requirementsForm">
      <ValidationProvider v-slot="{ errors }"
                          name="Requirement ID"
                          rules="required|max:30">
        <v-text-field
          v-model="requirementId"
          :error-messages="errors"
          label="Requirement ID"
          required />
      </ValidationProvider>
      <ValidationProvider v-slot="{ errors }"
                          name="Requirement version"
                          rules="required|max:10">
        <v-text-field
          v-model="requirementVersion"
          :error-messages="errors"
          label="Requirement version"
          required />
      </ValidationProvider>
      <ValidationProvider v-slot="{ errors }"
                          name="Requirement description"
                          rules="required|max:500|min:10">
        <v-textarea
          v-model="requirementDescription"
          solo
          name="input-7-4"
          label="Requirement description"
          required
          :error-messages="errors"
          :counter="500" />
      </ValidationProvider>
      <ValidationProvider v-slot="{ errors }"
                          name="Requirement rationale"
                          rules="required|max:500|min:10">
        <v-textarea
          v-model="requirementRationale"
          solo
          name="input-7-4"
          label="Requirement rationale"
          :error-messages="errors"
          :counter="500" />
      </ValidationProvider>
      <ValidationProvider v-slot="{ errors }"
                          name="Requirement type"
                          rules="required">
        <v-radio-group v-model="requirementType"
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
        <v-radio-group v-model="requirementStatus"
                       row
                       :error-messages="errors"
                       label="Requirement status:">
          <v-radio label="Waiting for revision"
                   value="Waiting for revision" />
          <v-radio label="Approved"
                   value="Approved" />
          <v-radio label="Declined"
                   value="Decilned" />
        </v-radio-group>
      </ValidationProvider>
      <v-btn class="mr-4"
             @click="submitForm">
        submit
      </v-btn>
    </form>
  </ValidationObserver>
</template>

<script>
import { required, max, min } from 'vee-validate/dist/rules'
import { extend, ValidationObserver, ValidationProvider, setInteractionMode } from 'vee-validate'
import { requirementsService } from '../services/requirements-service'
import router from '../router/index'

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
        requirement: { type: Object, default: () => {} },
        documentId: { type: String, default: '' }
    },
    data: () => ({
        requirementId: '',
        requirementVersion: '',
        requirementRationale: '',
        requirementDescription: '',
        requirementType: '',
        requirementStatus: '',
        updateMode: false
    }),
    created () {
        if (this.requirement !== undefined) {
            this.updateMode = true
            this.requirementId = this.requirement.requirement_id
            this.requirementVersion = this.requirement.requirement_version
            this.requirementRationale = this.requirement.requirement_rationale
            this.requirementDescription = this.requirement.requirement_description
            this.requirementType = this.requirement.requirement_type
            this.requirementStatus = this.requirement.requirement_status
        }
    },
    methods: {
        submitForm () {
            this.$refs.observer.validate()
            if (this.updateMode) {
                requirementsService.updateRequirement({ requirementId: this.requirementId,
                    requirementVersion: this.requirementVersion,
                    requirementRationale: this.requirementRationale,
                    requirementDescription: this.requirementDescription,
                    requirementType: this.requirementType,
                    requirementStatus: this.requirementStatus }, this.documentId)
                router.push('/list-requirement')
            } else {
                requirementsService.createRequirement({ requirementId: this.requirementId,
                    requirementVersion: this.requirementVersion,
                    requirementRationale: this.requirementRationale,
                    requirementDescription: this.requirementDescription,
                    requirementType: this.requirementType,
                    requirementStatus: this.requirementStatus })
                router.push('/list-requirement')
            }
        },
        clear () {
            this.requirementId = ''
            this.requirementVersion = ''
            this.requirementRationale = ''
            this.requirementDescription = ''
            this.requirementType = ''
            this.requirementStatus = ''
            this.$refs.observer.reset()
        },
    },
}
</script>
