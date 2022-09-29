<template>
  <v-row class="row-modal" justify="center">
      <v-card>
        <v-card-title class="justify-center">
          <span class="text-h5">Delete Order</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <h2 style="color: #105269">Deseja deletar a ordem selecionado?</h2>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions class="justify-center">
          <v-btn width="45%" dark color="red darken-1" @click="$emit('close')">
            Close
          </v-btn>
          <v-btn width="45%" dark color="#105269" @click="$emit('deletar')"> Save </v-btn>
        </v-card-actions>
      </v-card>
  </v-row>
</template>

<script>
import ApiService from "../../../../services/ApiService";
export default {
  data: () => ({
    api: new ApiService(),
    dialog: false,
    orderId: "",
  }),
  methods: {
  },
};
</script>