<template>
  <v-app>
    <v-navigation-drawer app dark color="#105269" right>
      <template v-slot:prepend>
        <v-list-item two-line>
          <ProfileBtn :key="reload" />
          <v-list-item-content>
            <v-list-item-title>{{ user.userName }}</v-list-item-title>
            <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </template>
      <div class="container container--fluid">
        <div class="row justify-center">
          <v-btn @click="logout" color="#b23b3b" small justify="center">
            <v-icon color="black">mdi-login-variant</v-icon>
          </v-btn>
        </div>
      </div>
      <v-divider></v-divider>

      <v-list dense>
        <v-list-item v-for="item in items" :key="item.title" :to="item.link">
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-main> <router-view @reload="getUser" /> </v-main>
  </v-app>
</template>

<script>
import ProfileBtn from "../pages/ProfilePage/Profile.vue";
export default {
  name: "internal",
  components: {
    ProfileBtn,
  },
  data: () => ({
    drawer: null,
    isActive: false,
    url: "",
    reload: false,
    items: [
      {
        icon: "mdi-folder",
        title: "Products",
        link: "/product",
      },
      {
        icon: "mdi-chart-bar",
        title: "Movements",
        link: "/movements",
      },
      {
        icon: "mdi-account",
        title: "Avatar",
        link: "/ProfilePage",
      },
      {
        icon: "mdi-lock",
        title: "Alter Password",
        link: "/UserPassword",
      },
    ],
    user: {},
    src: null,
    profilePage: "/ProfilePage",
  }),
  props: {
    src: {
      type: String,
      default: String,
    },
  },
  created() {
    this.getUser();
  },
  methods: {
    logout() {
      localStorage.clear();
      this.$toast.success("Voce foi desconectado");
      this.$router.push("/login");
    },
    getUser() {
      let userString = localStorage.getItem("user");
      this.user = JSON.parse(userString);
      this.userImage = this.url + "/" + this.user.image;
      this.reload = !this.reload;
    },
  },
};
</script>

<style>
</style>