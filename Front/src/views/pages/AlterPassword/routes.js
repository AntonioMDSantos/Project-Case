import layout from "../../layouts/internal.vue"
import UserPassword from './UserPassword.vue'

const routes = {
    component: layout,
    children: [
        {
            name: 'UserPassword',
            path: '/',
            component: UserPassword,
            meta: {
                requiresAuth: true
            }
        },
    ]
}

export default routes;