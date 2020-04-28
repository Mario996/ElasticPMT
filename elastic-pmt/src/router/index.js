import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push (location) {
    return originalPush.call(this, location).catch(err => err)
}

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
        path: '/requirement',
        name: 'requirement',
        component: () => import(/* webpackChunkName: "about" */ '../views/AddRequirementView.vue'),
        props: true,
    },
    {
        path: '/list-requirements',
        name: 'list-requirements',
        component: () => import(/* webpackChunkName: "about" */ '../views/ListRequirementView.vue')
    },
    {
        path: '/task',
        name: 'task',
        component: () => import(/* webpackChunkName: "about" */ '../views/AddTaskView.vue'),
        props: true,
    },
    {
        path: '/list-tasks',
        name: 'list-tasks',
        component: () => import(/* webpackChunkName: "about" */ '../views/ListTaskView.vue')
    },
    {
        path: '/users',
        name: 'users',
        component: () => import(/* webpackChunkName: "about" */ '../views/UsersView.vue')
    },
]

const router = new VueRouter({
    mode: 'history',
    routes
})

export default router
