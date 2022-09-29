<template>
  <v-app>
    <div class="background"></div>
    <v-main class="d-flex justify-center align-center">
      <v-col cols="10" lg="4" class="mx-auto">
        <v-card class="pa-4">
          <div class="text-center">
            <v-form class="form">
              <AvatarProfile v-model="src" @getImage="getImage" />
            </v-form>
            <v-list-item-content>
              <v-list-item-title>{{ user.userName }}</v-list-item-title>
              <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
            </v-list-item-content>
          </div>
          <v-form>
            <v-card-text>
              <v-text-field
                v-model="user.userName"
                type="userName"
                label="Name"
                placeholder="Name"
              />
              <v-text-field
                v-model="user.email"
                :rules="emailRules"
                type="email"
                label="Email"
                placeholder="Email"
              />
            </v-card-text>
            <v-card-actions class="justify-center">
              <v-btn @click.stop="dialogout = true" color="red"
                ><span class="white--text">Delete</span></v-btn
              >
              <v-btn @click="updateUser" max-width="80px"
                >Update</v-btn
              >
              <v-dialog v-model="dialogout" max-width="290">
                <v-card>
                  <v-card-title class="text-h5">
                    Deletar usuario
                  </v-card-title>
                  <v-card-text>
                    Tem certeza que deseja deletar o usuario?
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="green darken-1"
                    text
                    @click="deleteUser">
                    Agree</v-btn>
                    <v-btn color="red darken-1"
                    text
                    @click="dialogout = false">
                    Disagree</v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </v-card-actions>
          </v-form>
        </v-card>
      </v-col>
    </v-main>
  </v-app>
</template>

<script>
import AvatarProfile from "./AvatarProfile.vue";
import ApiService from "../../../services/ApiService.js";
export default {
  components: {
    AvatarProfile,
  },
  data: () => ({
    api: new ApiService(),
    user: {},
    src: null,
    isActive: false,
    urlEnv: process.env.VUE_APP_BASE_URL,
    dialog: false,
    userName: "",
    dialogout: false,
    email: "",
    url: "",
    emailRules: "",
    avatar:
      "https://t3.ftcdn.net/jpg/01/18/01/98/360_F_118019822_6CKXP6rXmVhDOzbXZlLqEM2ya4HhYzSV.jpg",
  }),

  created() {
    this.getUser();
  },
  props: {
    src: {
      type: String,
      default: String,
    },
  },
  methods: {
    getUser() {
      let userString = localStorage.getItem("user");
      this.user = JSON.parse(userString);
      this.userImage = this.url + "/" + this.user.image;
    },
    updateUser() {
      this.api
        .put(`/User/${this.user.id}`, this.user)
        .then((response) => {
          localStorage.setItem("user", JSON.stringify(response.content));
          this.user = response.content;
          this.$emit("reload", response.content);
          this.$toast.success(response.message);
        });
    },
    deleteUser() {
        this.api
      .delete(`/User/${this.user.id}`)
      .then(() => {
        this.$toast.success("Usuario deletado com sucesso")
        localStorage.clear();
        this.$router.push("/login");
        this.lista();
      }) 
    },
    getImage(e) {
      this.user.image = e;
    },
    },
};
</script>

<style scoped>
.inputs {
  border: none;
  background: none;
  outline: none;
}

.avatar {
  display: none;
}
.form {
  display: flex;
  justify-content: center;
  margin-bottom: 0.8rem;
}
.icon {
  border: none;
}

@media only screen and (max-width: 280px) {
  v-btn {
    max-width: 80px;
  }
}
</style>