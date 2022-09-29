import layout from '../../layouts/external.vue';
import register from './register.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'register',
            path: '/',
            component: register,
            meta: {
            }
        },
    ]
}

export default routes;