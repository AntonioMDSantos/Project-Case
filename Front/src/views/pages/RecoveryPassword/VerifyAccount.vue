<template>
  <div class="app">
    <div class="container">
      <v-card class="pa-4">
        <h2>Esqueceu a senha?</h2>
        <p>
          Coloque seu email abaixo para que possamos te identificar e te enviar
          um codigo para que você possa redefinir a sua senha!!
        </p>
        <div class="input-email">
          <v-text-field v-model="email" label="Email"></v-text-field>
          <div class="send">
            <v-btn dark @click="mail">Proximo</v-btn>
          </div>
          <div class="cancel">
            <v-btn dark color="red" @click="exit">Cancelar</v-btn>
          </div>
        </div>
      </v-card>
    </div>
  </div>
</template>

<script>
import ApiService from "../../../services/ApiService.js";
export default {
  data() {
    return {
      api: new ApiService(),
      email: "",
    };
  },
  methods: {
    mail() {
      this.api
        .post(`/User/verify/${this.email}`)
        .then((response) => {
          console.log(response);
          localStorage.setItem("email", this.email);
          this.$router.push("/change-password");
          this.$toast.success(response.message);
        })
        .catch((error) => {
          this.$toast.error(error.message);
        });
    },
    exit() {
      this.$router.replace("/login");
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

.send,
.cancel {
  padding: 5px;
  align-content: center;
  justify-content: center;
  display: flex;
}

.loader {
  display: none;
}
p {
  text-align: center;
}
h2 {
  text-align: center;
}
</style>