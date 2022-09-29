import layout from '../../layouts/external';
import ExportProduct from './ExportProduct.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'ExportProduct',
            path: '/',
            component: ExportProduct,
            meta: {
            }
        },
    ]
}

export default routes;