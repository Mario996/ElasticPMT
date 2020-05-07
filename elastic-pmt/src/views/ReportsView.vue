<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-row align="center">
          <v-col cols="6"
                 class="pb-0">
            <h1>
              REPORT:
            </h1>
            <h2>
              {{ currentReport }}
            </h2>
          </v-col>
          <v-col cols="6"
                 class="pb-0">
            <v-menu offset-y>
              <template v-slot:activator="{ on }">
                <v-btn
                  dark
                  class="mr-2"
                  v-on="on">
                  Reports
                </v-btn>
              </template>
              <v-list>
                <v-list-item
                  v-for="(item, index) in reports"
                  :key="index"
                  @click="pickReport(item)">
                  <v-list-item-title>{{ item }}</v-list-item-title>
                </v-list-item>
              </v-list>
            </v-menu>
          </v-col>
        </v-row>
        <v-divider
          class="my-2" />
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">
                  {{ firstHeader }}
                </th>
                <th class="text-left">
                  {{ secondHeader }}
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in Object.keys(items)"
                  :key="item">
                <td>{{ item }}</td>
                <td>{{ items[item] }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { tasksService } from '../services/tasks-service'

export default {
    data: () => ({
        items: [],
        mostTasksCreatedItems: [],
        mostTasksPerRequirementItems: [],
        firstHeader: '',
        secondHeader: '',
        currentReport: '',
        reports: [
            'Most tasks created',
            'Most tasks per requirement',
        ],

    }),
    created () {
        tasksService.orderByCreator()
            .then((response) => {
                this.mostTasksItems = response
            })
        tasksService.orderRequirementsByNumberOfTasks()
            .then((response) => {
                this.mostTasksPerRequirementItems = response
            })
    },
    methods: {
        pickReport (item) {
            switch (item) {
            case 'Most tasks created':
                this.firstHeader = 'Task creator'
                this.secondHeader = 'Tasks created'
                this.items = this.mostTasksItems
                this.currentReport = item
                break
            case 'Most tasks per requirement':
                this.firstHeader = 'Requirement name'
                this.secondHeader = 'Tasks created'
                this.items = this.mostTasksPerRequirementItems
                this.currentReport = item
                break
            }
        }
    },
}
</script>

<style>
.theme--light.v-divider {
    border-color: rgba(0, 0, 0, 1);
}

</style>
