import layout from '../../layouts/external.vue';
import verifyAccount from './VerifyAccount.vue';
import changePassword from './ChangePassword.vue';

const routes = {
  component: layout,
  children: [
      {
          name: '',
          path: '/change-password',
          component: changePassword,
      },
      {
        name: '',
        path: '/verify',
        component: verifyAccount,
      }
  ]
}

export default routes;