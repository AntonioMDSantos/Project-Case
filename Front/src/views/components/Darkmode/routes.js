import layout from '../../layouts/external';
import Darkmode from './Darkmode.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'Darkmode',
            path: '/',
            component: Darkmode,
            meta: {
            }
        },
    ]
}

export default routes;