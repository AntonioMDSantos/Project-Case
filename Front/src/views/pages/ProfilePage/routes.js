import layout from '../../layouts/internal.vue';
import ProfilePage from './ProfilePage.vue';

const routes = {
    component: layout,
    children: [
        {
            name: 'ProfilePage',
            path: '/',
            component: ProfilePage,
            meta: {
                requiresAuth: true
            }
        },
    ]
}

export default routes;