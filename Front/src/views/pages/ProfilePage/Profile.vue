<template>
  <v-container fluid>
    <v-row justify="center">
      <v-menu bottom min-width="200px" rounded offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon x-large v-on="on">
            <v-img
              class="profile"
              v-bind="attrs"
              v-on="on"
              max-width="50px"
              max-height="50px"
              :src="src"
            ></v-img>
            <v-avatar
              :class="{ avatar: isActive }"
              v-on="on"
              v-bind="attrs"
              color="grey"
            >
              <v-icon dark>mdi-account-circle</v-icon>
            </v-avatar>
          </v-btn>
        </template>
        <v-card>
          <v-list-item-content class="justify-center">
            <div class="mx-auto text-center">
              <h3>{{ user.userName }}</h3>
              <p class="text-caption mt-1">
                {{ user.email }}
              </p>
              <v-divider class="my-2"></v-divider>
              <v-btn color="blue lighten-2" dark :to="profilePage" link>
                Edit Profile
              </v-btn>
            </div>
          </v-list-item-content>
        </v-card>
      </v-menu>
    </v-row>
  </v-container>
</template>

<script>
import ApiService from "../../../services/ApiService";

export default {
  data: () => ({
    api: new ApiService(),
    user: {},
    dialog: false,
    profilePage: "/ProfilePage",
    url: "",
    isActive: false,
    key: false,
    src: null,
    envURL: process.env.VUE_APP_BASE_URL,
    rules: [
      (v) => !!v || "Please select an image",
      (v) =>
        v.type === "image/png" ||
        v.type === "image/jpeg" ||
        v.type === "image/bmp" ||
        "Please select an image",
    ],
  }),
  created() {
    this.getUser();
    if (this.user.image) {
      this.src = this.envURL + "/" + this.user.image;
      this.isActive = true;
    }
  },
  methods: {
    getUser() {
      this.user = JSON.parse(localStorage.getItem("user"));
    },
  },
};
</script>

<style>
.profile {
  border-radius: 50%;
  cursor: pointer;
  margin: auto;
}

.btns {
  display: flex;
}

.avatar {
  display: none;
}
</style>