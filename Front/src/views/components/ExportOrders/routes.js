import layout from '../../layouts/external';
import ExportOrders from './ExportOrders.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'ExportOrders',
            path: '/',
            component: ExportOrders,
            meta: {
            }
        },
    ]
}

export default routes;