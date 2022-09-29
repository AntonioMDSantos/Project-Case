<template>
  <v-row class="row-modal" justify="center">
    <v-card>
      <v-card-title class="justify-center">
        <span class="text-h5">Deletar Produto</span>
      </v-card-title>
      <v-card-text>
        <v-container>
          <h2 style="color: #105269">Deseja deletar o produto selecionado?</h2>
        </v-container>
      </v-card-text>
      <v-card-actions class="justify-center">
        <v-btn width="45%" dark color="red darken-1"  @click="$emit('close')"> Close </v-btn>
        <v-btn width="45%" dark color="#105269" @click="$emit('deletar')">
          Save
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-row>
</template>

<script>
import ApiService from "../../../../services/ApiService.js";
export default {
  data: () => ({
    api: new ApiService(),
    dialog: false,
    Id: "",
  }),
};
</script>

<style>
.row-modal {
  margin: 0 !important;
}
</style>
