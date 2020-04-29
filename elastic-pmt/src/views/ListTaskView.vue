<template>
  <v-container>
    <TaskPreview v-for="task in tasks"
                 :key="task.id"
                 :task="task"
                 @delete-task="deleteTask" />
  </v-container>
</template>

<script>
import { tasksService } from '../services/tasks-service'
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
                        objectValue: x.source,
                    }
                })
            })
    },
    methods: {
        deleteTask (id) {
            this.tasks = this.tasks.filter(x => x.objectValue.id !== id)
            tasksService.deleteTask(id)
                .then((response) => {
                    console.log(response)
                })
        }
    },
}
</script>
