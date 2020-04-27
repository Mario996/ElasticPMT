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
                                name="Task summary"
                                rules="required|max:100">
              <v-text-field
                v-model="taskSummary"
                :error-messages="errors"
                label="Task summary"
                required />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Component"
                                rules="required|max:20">
              <v-text-field
                v-model="taskComponent"
                :error-messages="errors"
                label="Component"
                required />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Task type"
                                rules="required">
              <v-select
                v-model="taskType"
                :error-messages="errors"
                :items="taskTypes"
                label="Task type"
                dense
                outlined />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Task priority"
                                rules="required">
              <v-select
                v-model="taskPriority"
                :error-messages="errors"
                :items="taskPriorities"
                label="Task priority"
                dense
                outlined />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Task environment"
                                rules="required|max:500|min:10">
              <v-textarea
                v-model="taskEnvironment"
                solo
                name="input-7-4"
                label="Task environment"
                :error-messages="errors"
                :counter="500" />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Task description"
                                rules="required|max:500|min:10">
              <v-textarea
                v-model="taskDescription"
                solo
                name="input-7-4"
                label="Task description"
                required
                :error-messages="errors"
                :counter="500" />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Task assignee"
                                rules="required|max:100">
              <v-text-field
                v-model="taskAssignee"
                :error-messages="errors"
                label="Task assignee"
                required />
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
          <v-row v-if="mode !== 'CREATE'">
            <v-col cols="12">
              <label>Comments:</label>
              <CommentComponent v-for="comment in task.comments"
                                :key="comment.id"
                                :comment="comment"
                                @update-comment="updateComment"
                                @delete-comment="deleteComment" />
            </v-col>
          </v-row>
          <v-row v-if="mode !== 'CREATE'"
                 justify="center">
            <v-col cols="8">
              <v-textarea
                v-model="comment"
                solo
                height="8vh"
                name="input-7-4"
                label="Comment"
                :counter="500" />
            </v-col>
            <v-col cols="4">
              <v-btn
                class="mb-4"
                width="300"
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
import { tasksService } from '../services/tasks-service'
import router from '../router/index'
import CommentComponent from '../components/CommentComponent'
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
        task: { type: Object, default: () => {} },
    },
    data: () => ({
        taskSummary: '',
        taskComponent: '',
        taskType: '',
        taskPriority: '',
        taskAssignee: '',
        taskEnvironment: '',
        taskDescription: '',
        comment: '',
        updateMode: false,
        mode: 'CREATE',
        taskTypes: ['Bug', 'Story', 'Epic', 'Task', 'Subtask'],
        taskPriorities: ['Blocker', 'Critical', 'Major', 'Minor', 'Trivial']
    }),
    created () {
        if (this.task !== undefined) {
            this.updateMode = true
            this.mode = 'UPDATE'
            this.taskSummary = this.task.summary
            this.taskComponent = this.task.component
            this.taskType = this.task.type
            this.taskPriority = this.task.priority
            this.taskAssignee = this.task.assignee
            this.taskEnvironment = this.task.environment
            this.taskDescription = this.task.description
        }
    },
    methods: {
        submitForm () {
            this.$refs.observer.validate()
            if (this.updateMode) {
                tasksService.updateTask({ taskSummary: this.taskSummary,
                    taskComponent: this.taskComponent,
                    taskType: this.taskType,
                    taskPriority: this.taskPriority,
                    taskAssignee: this.taskAssignee,
                    taskEnvironment: this.taskEnvironment,
                    taskDescription: this.taskDescription,
                    taskComments: this.task.comments }, this.task.id)
                    .then(() => {
                        router.push('/list-tasks')
                    })
            } else {
                tasksService.createTask({ taskSummary: this.taskSummary,
                    taskComponent: this.taskComponent,
                    taskType: this.taskType,
                    taskPriority: this.taskPriority,
                    taskAssignee: this.taskAssignee,
                    taskEnvironment: this.taskEnvironment,
                    taskDescription: this.taskDescription })
                    .then(() => {
                        router.push('/list-tasks')
                    })
            }
        },
        addComment () {
            this.task.comments.push({ text: this.comment, id: uuidv4() })
            tasksService.updateTask({ taskSummary: this.taskSummary,
                taskComponent: this.taskComponent,
                taskType: this.taskType,
                taskPriority: this.taskPriority,
                taskAssignee: this.taskAssignee,
                taskEnvironment: this.taskEnvironment,
                taskDescription: this.taskDescription,
                taskComments: this.task.comments }, this.task.id)
            this.comment = ''
        },
        updateComment (comment) {
            // TODO: OVDE IMA BUG KOD UPDATEA, IMA I NA REQUIREMENTU
            var commentIndexToBeUpdated = this.task.comments.find(x => x.id === comment.id)
            this.task.comments.splice(commentIndexToBeUpdated, 1, comment)
            tasksService.updateTask({ taskSummary: this.taskSummary,
                taskComponent: this.taskComponent,
                taskType: this.taskType,
                taskPriority: this.taskPriority,
                taskAssignee: this.taskAssignee,
                taskEnvironment: this.taskEnvironment,
                taskDescription: this.taskDescription,
                taskComments: this.task.comments }, this.task.id)
        },
        deleteComment (comment) {
            var commentIndexToBeDeleted = this.task.comments.findIndex(x => x.id === comment.id)
            this.task.comments.splice(commentIndexToBeDeleted, 1)
            tasksService.updateTask({ taskSummary: this.taskSummary,
                taskComponent: this.taskComponent,
                taskType: this.taskType,
                taskPriority: this.taskPriority,
                taskAssignee: this.taskAssignee,
                taskEnvironment: this.taskEnvironment,
                taskDescription: this.taskDescription,
                taskComments: this.task.comments }, this.task.id)
        },
        clear () {
            this.taskSummary = ''
            this.taskComponent = ''
            this.taskType = ''
            this.taskPriority = ''
            this.taskAssignee = ''
            this.taskEnvironment = ''
            this.taskDescription = ''
            this.$refs.observer.reset()
        },
    },
}
</script>
