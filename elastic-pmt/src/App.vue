<template>
  <v-app>
    <v-app-bar
      app
      color="primary"
      dark>
      <div class="d-flex align-center">
        <v-img
          alt="ElasticPMT"
          class="shrink"
          contain
          height="60"
          width="50"
          :src="require('./assets/elasticsearch-logo.png')"
          transition="scale-transition" />
      </div>
      <v-toolbar-title class="toolbar__title">
        lasticPMT
      </v-toolbar-title>
      <v-spacer />
      <router-link to="add-requirement"
                   class="navigation-link">
        Requirements
      </router-link>
      <router-link to="home"
                   class="navigation-link">
        Tasks
      </router-link>
    </v-app-bar>
    <v-content>
      <v-container>
        <router-view />
      </v-container>
    </v-content>
  </v-app>
</template>

<script>

export default {
    name: 'App',
    data: () => ({
    //
    })
}
</script>

<style>
a.navigation-link{
  color: white!important;
  text-decoration: none;
  padding-right: 10px;
}
</style>
