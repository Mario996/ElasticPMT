import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
    {
        path: '*',
        component: () => import(/* webpackChunkName: "home" */ '../views/Home.vue')
    },
    {
        path: '/home',
        name: 'Home',
        component: () => import(/* webpackChunkName: "home" */ '../views/Home.vue')
    },
    {
        path: '/add-requirement',
        name: 'add-requirement',
        component: () => import(/* webpackChunkName: "about" */ '../views/AddRequirementView.vue')
    },
    {
        path: '/list-requirement',
        name: 'list-requirement',
        component: () => import(/* webpackChunkName: "about" */ '../views/ListRequirementView.vue')
    },
]

const router = new VueRouter({
    mode: 'history',
    routes
})

export default router
