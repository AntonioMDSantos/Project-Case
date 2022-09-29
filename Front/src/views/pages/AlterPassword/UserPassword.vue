<template>
  <v-app>
    <div class="background"></div>
    <v-main class="d-flex justify-center align-center">
      <v-col cols="10" lg="4" class="mx-auto">
        <v-card class="pa-4">
          <div class="text-center">
              <span class="text-h5" style="color:#105269">Para atualizar sua senha, digite a senha antiga, para criar uma nova</span>
            <v-list-item-content>
              <v-list-item-title>{{ user.userName }}</v-list-item-title>
              <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
            </v-list-item-content>
          </div>
          <v-form ref="form" class="form">
            <v-card-text>
                <v-text-field
                v-model="oldPassword"
                :rules="passwordRules"
                :type="passSw ? 'text' : 'password'"
                label="Senha antiga"
                placeholder="Senha antiga"
                prepend-inner-icon="mdi-key"
                :append-icon="passSw ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="passSw = !passSw"
                required
              />
              <v-text-field
                v-model="newPassword"
                :rules="newPasswordRules"
                :type="passSw2 ? 'text' : 'password'"
                label="Senha nova"
                placeholder="Senha nova"
                prepend-inner-icon="mdi-key"
                :append-icon="passSw2 ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="passSw2 = !passSw2"
                required
              />
              <v-text-field
                label="Confirmaçao nova senha"
                :type="passSw3 ? 'text' : 'password'"
                prepend-inner-icon="mdi-key"
                :append-icon="passSw3 ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="passSw3 = !passSw3"
                v-model="confirmPassword"
                :rules="confirmPasswordRules"
                required
              ></v-text-field>
               <v-card-actions class="justify-center">
              <v-btn @click="updatePassword" max-width="80px"
                >Update</v-btn
              >
            </v-card-actions>
            </v-card-text>
          </v-form>
        </v-card>
      </v-col>
    </v-main>
  </v-app>
</template>

<script>

import ApiService from "../../../services/ApiService.js";
export default {
  data: () => ({
    api: new ApiService(),
    user: {},
    isActive: false,
    urlEnv: process.env.VUE_APP_BASE_URL,
    userName: "",
    oldPassword: "",
    newPassword: "",
    passSw: "",
    passSw2: "",
    passSw3: "",
    newPasswordRules: [
      (v) => !!v || "A senha é obrigatória", 
      (v) => (v && v.length >= 6) || "A senha deve ter no mínimo 6 caracteres"],
    confirmPassword: "",
    // confirmPasswordRules: [
    //   (v) => !!v || "A confirmação de senha é obrigatória",
    //   (v) => (v && v.length >= 6) || "A senha deve ter no mínimo 6 caracteres",
      // (v) => (v && v === this.newPassword) || "As senhas não conferem"
    // ],
    passwordRules: [
      (v) => !!v || "A senha é obrigatória",
      (v) => v.length >= 6 || "A senha tem no mínimo 6 caracteres",
    ],
    url: "",
    avatar:
      "https://t3.ftcdn.net/jpg/01/18/01/98/360_F_118019822_6CKXP6rXmVhDOzbXZlLqEM2ya4HhYzSV.jpg",
    show: false,
  }),

  created() {
    this.user = JSON.parse(localStorage.getItem("user"))
  },
  methods: {
    updatePassword() {
      if (this.$refs.form.validate()) {
        let filedsRequest = {
          NewPassword: this.newPassword,
          OldPassword: this.oldPassword,
          Email: this.user.email
        }
        this.api
        .post(`/User/alterPassword`, filedsRequest)
        .then((response) => {
          this.$root.$emit("/profilePage");
          this.$toast.success(response.message)
        })
        .catch((error) => {});
      }
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

.alterpass {
    text-align: center;
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