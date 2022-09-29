<template>
  <v-app>
    <div class="background"></div>
    <v-main class="d-flex justify-center align-center">
      <v-col cols="10" lg="4" class="mx-auto">
        <v-card class="pa-4">
          <div class="text-center">
            <modedark />
            <v-avatar size="70" color="">
              <v-icon size="50">mdi-robot</v-icon>
            </v-avatar>
            <h2>Registeneer</h2>
          </div>
          <v-form ref="form" class="imputs">
            <v-card-text>
              <v-text-field
                v-model="username"
                :rules="usernameRules"
                type="text"
                label="Username"
                placeholder="Username"
                prepend-inner-icon="mdi-account"
                required
              />
              <v-text-field
                v-model="email"
                :rules="emailRules"
                type="email"
                label="Email"
                placeholder="Email"
                prepend-inner-icon="mdi-account"
                required
                v-on:keyup.enter="register"
              />
              <v-text-field
                v-model="password"
                :rules="passwordRules"
                :type="passSw ? 'text' : 'password'"
                label="Password"
                placeholder="Password"
                prepend-inner-icon="mdi-key"
                :append-icon="passSw ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="passSw = !passSw"
                required
                v-on:keyup.enter="register"
              />
              <v-text-field
                label="Confirm Password"
                :type="show2 ? 'text' : 'password'"
                prepend-inner-icon="mdi-key"
                :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="show2 = !show2"
                v-model="confirmPassword"
                :rules="confirmPasswordRules.concat(passwordConfirmationRule)"
                required
              ></v-text-field>
              <v-switch
                label="Admin"
                v-model="IsAdmin"
                color="black"
              ></v-switch>
            </v-card-text>
            <div class="singin">
              <span
                >Already have account? <a v-on:click="login">Sing in</a></span
              >
            </div>
            <v-card-actions class="justify-center">
              <v-btn @click="register"
                ><span class="font-weight-bold">Create</span></v-btn
              >
            </v-card-actions>
          </v-form>
        </v-card>
      </v-col>
    </v-main>
  </v-app>
</template>

<script>
import ApiService from "../../../services/ApiService.js";
import Modedark from "../../components/Darkmode/Darkmode.vue";

export default {
  name: "App",
  components: {
    Modedark,
  },

  data: () => ({
    api: new ApiService(),
    IsAdmin: false,
    passSw: false,
    valid: true,
    email: "",
    emailRules: [
      (v) => !!v || "E-mail is required",
      (v) => /.+@.+/.test(v) || "E-mail must be valid",
    ],
    show2: "",
    password: "",
    confirmPassword: "",
    confirmPasswordRules: [
      (v) => !!v || "A confirmação de senha é obrigatória",
      (v) => (v && v.length >= 6) || "A senha deve ter no mínimo 6 caracteres",
    ],
    passwordRules: [
      (v) => !!v || "Password is required",
      (v) => v.length >= 6 || "Password must be less than 6 characters or ore",
    ],
    username: "",
    usernameRules: [(v) => !!v || "Username is required"],
  }),
  computed: {
    passwordConfirmationRule() {
      return () =>
        this.password === this.confirmPassword || "As senhas não conferem";
    },
  },
  methods: {
    register() {
      if (this.$refs.form.validate()) {
        this.api
          .post("/User/Add", {
            UserName: this.username,
            email: this.email,
            password: this.password,
            Master: this.IsAdmin ? 0 : 1,
          })
          .then((response) => {
            this.$toast.success("Usuario cadastrado com sucesso");
            this.$router.replace("/login");
          })
          .catch((error) => {
            this.type = "error";
            this.valid = false;
          });
      } else {
        this.$toast.error("Fill in the fields");
      }
    },
    login() {
      this.$router.replace("/login");
    },
  },
};
</script>

<style>
* {
  margin: 0;
  padding: 0;
}
.sing {
  border: none;
  box-shadow: none;
  background: none;
}

main {
  background: rgb(15, 15, 15);
}

.singin {
  text-align: center;
}
</style>
