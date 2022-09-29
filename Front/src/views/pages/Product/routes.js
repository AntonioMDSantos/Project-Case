import layout from '../../layouts/internal.vue';
import Product from './Product.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'Product',
            path: '/',
            component: Product,
            meta: {
                requiresAuth: true
            }
        },
    ]
}

export default routes;