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
                v-model="task.summary"
                :error-messages="errors"
                label="Task summary"
                required />
            </ValidationProvider>
            <v-row>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Component"
                                    rules="required|max:20">
                  <v-text-field
                    v-model="task.component"
                    :error-messages="errors"
                    label="Component"
                    required />
                </ValidationProvider>
              </v-col>
              <v-col class="py-0">
                <v-combobox
                  v-model="labels"
                  label="Labels"
                  multiple
                  chips />
              </v-col>
            </v-row>
            <v-row>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Task type"
                                    rules="required">
                  <v-select
                    v-model="task.type"
                    :error-messages="errors"
                    :items="taskTypes"
                    label="Task type"
                    dense
                    outlined />
                </ValidationProvider>
              </v-col>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Task priority"
                                    rules="required">
                  <v-select
                    v-model="task.priority"
                    :error-messages="errors"
                    :items="taskPriorities"
                    label="Task priority"
                    dense
                    outlined />
                </ValidationProvider>
              </v-col>
            </v-row>
            <ValidationProvider v-slot="{ errors }"
                                name="Task environment"
                                rules="required|max:500|min:10">
              <v-textarea
                v-model="task.environment"
                solo
                no-resize
                height="8vh"
                name="input-7-4"
                label="Task environment"
                :error-messages="errors"
                :counter="500" />
            </ValidationProvider>
            <ValidationProvider v-slot="{ errors }"
                                name="Task description"
                                rules="required|max:500|min:10">
              <v-textarea
                v-model="task.description"
                solo
                no-resize
                height="8vh"
                name="input-7-4"
                label="Task description"
                required
                :error-messages="errors"
                :counter="500" />
            </ValidationProvider>
            <v-row>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Task assignee"
                                    rules="required">
                  <v-autocomplete
                    v-model="task.assignee"
                    :error-messages="errors"
                    :items="users"
                    dense
                    outlined
                    item-text="email"
                    item-value="email"
                    label="Task assignee"
                    required
                    return-object />
                </ValidationProvider>
              </v-col>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Task creator"
                                    rules="required">
                  <v-autocomplete
                    v-model="task.creator"
                    :error-messages="errors"
                    :items="users"
                    dense
                    outlined
                    item-text="email"
                    item-value="email"
                    label="Task creator"
                    required
                    return-object />
                </ValidationProvider>
              </v-col>
            </v-row>
            <v-row>
              <v-col class="py-0">
                <ValidationProvider v-slot="{ errors }"
                                    name="Task status"
                                    rules="required">
                  <v-autocomplete
                    v-model="task.status"
                    :error-messages="errors"
                    :items="statuses"
                    dense
                    outlined
                    item-text="name"
                    item-value="name"
                    label="Task status"
                    required
                    return-object />
                </ValidationProvider>
              </v-col>
              <v-col class="py-0">
                <v-autocomplete
                  v-model="task.requirement"
                  :items="requirements"
                  dense
                  outlined
                  item-text="name"
                  item-value="name"
                  label="Requirement"
                  return-object />
              </v-col>
            </v-row>
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
            <v-col cols="10">
              <v-textarea
                v-model="comment"
                solo
                no-resize
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
import { tasksService } from '../services/tasks-service'
import router from '../router/index'
import CommentComponent from '../components/CommentComponent'
import { v4 as uuidv4 } from 'uuid'
import { usersService } from '../services/users-service'
import { requirementsService } from '../services/requirements-service'
import { statusesService } from '../services/statuses-service'

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
        taskObject: { type: Object, default: () => {} },
    },
    data: () => ({
        task: {},
        comment: '',
        mode: 'CREATE',
        users: [],
        statuses: [],
        requirements: [],
        taskTypes: ['Bug', 'Story', 'Epic', 'Task', 'Subtask'],
        taskPriorities: ['Blocker', 'Critical', 'Major', 'Minor', 'Trivial'],
        labels: [],
    }),
    created () {
        if (this.taskObject !== undefined) {
            this.mode = 'UPDATE'
            this.task = this.taskObject
            this.labels = this.task.labels.map(x => x.name)
        }
        usersService.getAllUsers()
            .then((response) => {
                this.users = response.map(x => x.source)
            })
        statusesService.getAllStatuses()
            .then((response) => {
                this.statuses = response.map(x => x.source)
            })
        requirementsService.getAllRequirements()
            .then((response) => {
                this.requirements = response.map(x => x.source)
            })
    },
    methods: {
        submitForm () {
            this.$refs.observer.validate().then((result) => {
                if (result) {
                    if (this.mode === 'UPDATE') {
                        this.task.labels = this.labels.map(x => { return { name: x, id: uuidv4() } })
                        tasksService.updateTask(this.task, this.task.id)
                            .then(() => {
                                router.push('/list-tasks')
                            })
                    } else {
                        this.task.labels = this.labels.map(x => { return { name: x, id: uuidv4() } })
                        tasksService.createTask(this.task)
                            .then(() => {
                                router.push('/list-tasks')
                            })
                    }
                }
            })
        },
        addComment () {
            this.task.comments.push({ text: this.comment, id: uuidv4() })
            tasksService.updateComments(this.task.id, this.task.comments)
            this.comment = ''
        },
        updateComment (comment) {
            var commentIndexToBeUpdated = this.task.comments.findIndex(x => x.id === comment.id)
            this.task.comments.splice(commentIndexToBeUpdated, 1, comment)
            tasksService.updateComments(this.task.id, this.task.comments)
        },
        deleteComment (comment) {
            var commentIndexToBeDeleted = this.task.comments.findIndex(x => x.id === comment.id)
            this.task.comments.splice(commentIndexToBeDeleted, 1)
            tasksService.updateComments(this.task.id, this.task.comments)
        },
    },
}
</script>
