import Vue from "vue";
import VueRouter from "vue-router";
import guideRoutes from '../views/pages/guide/routes';
import registerRoutes from '../views/pages/register/routes';
import ProductRoutes from '../views/pages/Product/routes';
import MovementsRoutes from '../views/pages/Movements/routes';
import ProfilePageRoutes from '../views/pages/ProfilePage/routes';
import DarkmodeRoutes from '../views/components/Darkmode/routes';
import ExportProductRoutes from '../views/components/ExportProduct/routes';
import ExportOrdersRoutes from '../views/components/ExportOrders/routes';
import recoveryPasswordRoutes from "../views/pages/RecoveryPassword/routes";
import UserPasswordRoutes from "../views/pages/AlterPassword/routes"


Vue.use(VueRouter);

const routes = [
{
    path: "/",
    redirect: "/login",
  },
  {
    path: "/recovery-password",
    ...recoveryPasswordRoutes,
  },
  {
    path: '/login',
    ...guideRoutes
  },
  {
    path: '/register',
    ...registerRoutes
  },
  {
    path: '/Product',
    ...ProductRoutes
  },
  {
    path: '/UserPassword',
    ...UserPasswordRoutes
  },
  {
    path: '/Movements',
    ...MovementsRoutes
  },
  {
    path: '/ProfilePage',
    ...ProfilePageRoutes
  },
  {
    path: '/Darkmode',
    ...DarkmodeRoutes
  },
  {
    path: '/ExportProduct',
    ...ExportProductRoutes
  },
  {
    path: '/ExportOrders',
    ...ExportOrdersRoutes
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});


router.beforeEach((to, from, next) => {
  const user = localStorage.getItem('token');

  if (to.meta.requiresAuth) {
      if(user == null) {
        next("/login")
      } else {
        next()
      }
  } else {
      next()
  }
});

export default router;
