<template>
  <v-app>
    <div class="background"></div>
    <v-main class="d-flex justify-center align-center">
      <v-col cols="10" lg="4" class="mx-auto">
        <v-card class="pa-4">
          <div class="text-center">
            <modedark />
            <v-avatar size="70">
              <v-icon size="50">mdi-robot</v-icon>
            </v-avatar>
            <h1>Logineener</h1>
          </div>
          <v-form @submit.prevent="login" ref="form">
            <v-card-text>
              <v-text-field
                v-model="email"
                :rules="emailRules"
                type="email"
                label="Email"
                placeholder="Email"
                prepend-inner-icon="mdi-account"
                required
                v-on:keyup.enter="login"
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
                v-on:keyup.enter="login"
              />
              <p class="recover">
                <span @click="recovery">Esqueceu sua senha?</span>
              </p>
              <g-recaptcha
                class="captcha"
                :sitekey="sitekey"
                :callback="callback"
              />
            </v-card-text>
            <v-card-actions class="justify-center">
              <v-btn @click="login"
                ><span class="font-weight-bold">Login</span></v-btn
              >
              <v-btn @click="loading = true"
                ><span class="font-weight-bold">Register</span></v-btn
              >
            </v-card-actions>
            <v-progress-linear
              :active="loading"
              :indeterminate="loading"
              absolute
              bottom
              color="deep-purple accent-4"
            ></v-progress-linear>
          </v-form>
        </v-card>
      </v-col>
    </v-main>
  </v-app>
</template>

<script>
import ApiService from "../../../services/ApiService.js";
import gRecaptcha from "../../../views/pages/guide/recaptcha.vue";
import Modedark from "../../components/Darkmode/Darkmode.vue";
export default {
  name: "App",
  components: {
    gRecaptcha,
    Modedark,
  },
  data: () => ({
    api: new ApiService(),
    passSw: false,
    loading: false,
    email: "",
    sitekey: "6LeqjlUfAAAAAAYtTCJ8kebfrCEFrqQVKNmPspHv",
    emailRules: [
      (v) => !!v || "E-mail is required",
      (v) => /.+@.+/.test(v) || "E-mail must be valid",
    ],
    password: "",
    passwordRules: [
      (v) => !!v || "Password is required",
      (v) => v.length >= 6 || "Password must be less than 6 characters or ore",
    ],
  }),
  methods: {
    callback(token) {
      this.user.token = token;
    },
    login() {
      if (this.$refs.form.validate()) {
        this.api
          .post("/User/Login", {
            email: this.email,
            password: this.password,
          })
          .then((response) => {
            this.$toast.success("Usuario logado com sucesso");
            localStorage.setItem("token", response.content.token);
            localStorage.setItem("user", JSON.stringify(response.content.user));
            this.$router.push("/product");
          })
          .catch((error) => {
            this.type = "error";
            this.valid = false;
          });
      } else {
        this.$toast.error("Fill in the fields");
      }
    },
    recovery() {
      this.$router.replace("/verify");
    },
  },
  watch: {
    loading(val) {
      if (!val) return;
      setTimeout(
        () => ((this.loading = false), this.$router.replace("/register")),
        1000
      );
    },
  },
};
</script>

<style>
* {
  margin: 0;
  padding: 0;
}
.captcha {
  display: flex;
  align-items: center;
  justify-content: center;
}

@media only screen and (max-width: 330px) {
  h1 {
    font-size: 30px;
  }
  iframe {
    width: 204px;
  }
  .g-recaptcha {
    width: 204px;
  }
}

@media only screen and (max-width: 390px) {
  iframe {
    max-width: 254px;
  }
  .g-recaptcha {
    max-width: 254px;
  }
}

.recover {
  display: flex;
  align-content: center;
  justify-content: center;
}

.recover span:hover {
  color: blue;
  cursor: pointer;
}
</style>
