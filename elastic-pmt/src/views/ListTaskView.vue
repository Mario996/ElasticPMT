<template>
  <v-container>
    <TaskPreview v-for="task in tasks"
                 :key="task.summary"
                 :task="task"
                 @delete-task="deleteTask" />
  </v-container>
</template>

<script>
import { tasksService } from '../services/tasks-service'
import router from '../router/index'
import TaskPreview from '../components/TaskPreview'

export default {
    components: {
        TaskPreview,
    },
    data: () => ({
        tasks: []
    }),
    created () {
        tasksService.getAllTasks()
            .then((response) => {
                this.tasks = response.map(x => {
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
        updateTask (id) {
            tasksService.getTaskById(id)
                .then((response) => {
                    router.push({ name: 'task', params: { task: response.source, documentId: response.id } })
                })
        },
        deleteTask (id) {
            this.tasks = this.tasks.filter(x => x.id !== id)
            tasksService.deleteTask(id)
                .then((response) => {
                    console.log(response)
                })
        }
    },
}
</script>
