import layout from '../../layouts/external.vue';
import guide from './guide.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'guide',
            path: '/',
            component: guide,
            meta: {
            }
        },
    ]
}

export default routes;