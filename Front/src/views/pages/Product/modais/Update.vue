<template>
  <v-row class="row-modal" justify="center">
    <v-card width="100%">
      <v-card-title class="justify-center">
        <span class="text-h5">Update Product</span>
      </v-card-title>
      <v-card-text>
        <v-container>
          <v-form ref="form" class="form">
            <v-text-field
              label="Product"
              v-model="product.productName"
              :rules="rulesProduct.productName"
              required
              outlined
            ></v-text-field>
            <v-text-field
              type="number"
              label="Quantity"
              v-model="product.productQtd"
              :rules="rulesProduct.productQtd"
              outlined
            ></v-text-field>
            <v-file-input
              class="ImageProduct"
              accept="image/png, image/jpeg, image/bmp"
              label="ProductImage"
              v-model="src"
              prepend-inner-icon="mdi-camera"
              prepend-icon=""
              outlined
            ></v-file-input>
          </v-form>
        </v-container>
      </v-card-text>
      <v-card-actions class="justify-center">
        <v-btn width="45%" dark color="red darken-1" @click="$emit('close')">
          Close
        </v-btn>
        <v-btn width="45%" dark color="#105269" @click="validate"> Save </v-btn>
      </v-card-actions>
    </v-card>
  </v-row>
</template>

<script>
import ApiService from "../../../../services/ApiService";
export default {
  data: () => ({
    api: new ApiService(),
    ProductImage: "",
    src: "",
    rulesProduct: {
      productName: [(v) => !!v || "Nome é obrigatório"],
      productQtd: [(v) => !!v || "Quantidade é obrigatória"],
    },
  }),
  props: {
    product: Object,
  },
  created() {
    console.log(this.product);
  },
  methods: {
    validate() {
      if (this.$refs.form.validate()) {
        this.saveImage();
        setTimeout(() => {
          this.$emit("action", this.product);
        }, 400);
      }
    },
    saveImage(e) {
      let reader = new FileReader();
      reader.readAsDataURL(this.src);
      reader.onload = (e) => {
        this.product.productImage = e.target.result;
      };
    },
  },
};
</script>

<style>
</style>
