<template>
  <div class="app">
    <div class="container">
      <v-card class="pa-4">
        <h3>Digite o código para redefinir a senha</h3>
        <v-form ref="form" lazy-validation class="input-email">
          <v-text-field
            label="Código"
            v-model="code"
            :rules="codeRules"
            required
          ></v-text-field>
          <v-text-field
            label="Senha"
            :type="show ? 'text' : 'password'"
            :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
            @click:append="show = !show"
            v-model="password"
            :rules="passwordRules"
            required
          ></v-text-field>
          <v-text-field
            label="Confirmar senha"
            :type="show2 ? 'text' : 'password'"
            :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
            @click:append="show2 = !show2"
            v-model="confirmPassword"
            :rules="confirmPasswordRules.concat(passwordConfirmationRule)"
            required
          ></v-text-field>
        </v-form>
        <div class="alterpass">
          <v-btn dark @click="alterPassword">Alterar Senha</v-btn>
        </div>
      </v-card>
    </div>
  </div>
</template>

<script>
import ApiService from "../../../services/ApiService";
export default {
  data() {
    return {
      show: false,
      show2: false,
      api: new ApiService(),
      code: "",
      codeRules: [(v) => !!v || "Código é obrigatório"],
      password: "",
      confirmPassword: "",
      passwordRules: [
        (v) => !!v || "A senha é obrigatória",
        (v) =>
          (v && v.length >= 6) || "A senha deve ter no mínimo 6 caracteres",
      ],
      confirmPasswordRules: [
        (v) => !!v || "A confirmação de senha é obrigatória",
        (v) =>
          (v && v.length >= 6) || "A senha deve ter no mínimo 6 caracteres",
      ],
      email: "",
    };
  },
  computed: {
    passwordConfirmationRule() {
      return () =>
        this.password === this.confirmPassword || "As senhas não conferem";
    },
  },
  created() {
    this.email = localStorage.getItem("email");
  },
  methods: {
    validate() {
      if (this.$refs.form.validate()) {
        this.snackbar = true;
      }
    },
    alterPassword() {
      if (this.$refs.form.validate()) {
        this.api
          .post(`/User/password`, {
            email: this.email,
            code: this.code,
            newPassword: this.password || this.confirmPassword,
          })
          .then((response) => {
            this.$toast.success(response.message);
            this.$router.push("/login");
            localStorage.removeItem("email");
          })
          .catch((error) => {
            this.$toast.error(error.message);
          });
      }
    },
  },
};
</script>

<style scoped>
.app {
  height: 100vh;
  width: 100vw;
  display: flex;
  justify-content: center;
  align-items: center;
  background: black;
}

.container {
  display: flex;
  justify-content: center;
  align-items: center;
}
.card h3 {
  text-align: center;
}

.input-email {
  margin-bottom: 1rem;
}
.alterpass {
  display: flex;
  align-content: center;
  justify-content: center;
}
</style>