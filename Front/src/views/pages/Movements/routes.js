import layout from '../../layouts/internal.vue';
import Movements from './Movements.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'Movements',
            path: '/',
            component: Movements,
            meta: {
                requiresAuth: true
            }
        },
    ]
}

export default routes;