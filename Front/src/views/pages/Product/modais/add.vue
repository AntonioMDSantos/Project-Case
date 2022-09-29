<template>
  <v-row class="row-modal" justify="center">
      <v-card width="100%">
        <v-card-title class="justify-center">
          <span class="text-h5">Create Product</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form ref="form" class="form">
              <v-row>
              <v-col cols="12" sm="16" md="14">
                <v-text-field
                  label="Product name"
                  v-model="productName"
                  required
                  outlined
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="16" md="14">
                <v-text-field
                  type="number"
                  label="Quantity in stock"
                  v-model="productQtd"
                  outlined
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="16" md="14">
                <v-file-input
                  accept="image/png, image/jpeg, image/bmp"
                  label="ProductImage"
                  v-model="src"
                  prepend-inner-icon="mdi-camera"
                  prepend-icon=""
                  outlined
                ></v-file-input>
              </v-col>
              </v-row>
            </v-form>
          </v-container>
        </v-card-text>
        <v-card-actions class="justify-center">
          <v-btn width="45%" dark color="red darken-1" @click="$emit('close')">
            Close
          </v-btn>
          <v-btn width="45%" dark color="#105269"  @click="validate()">
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
  </v-row>
</template>

<script>
import ApiService from "../../../../services/ApiService";

export default {
  data: () => ({
    dialog: false,
    url: "",
    src: null,
    productName: "",
    productQtd: "",
    ProductImage: "",
    api: new ApiService(),
  }),
  methods: {
    validate() {
      if(this.$refs.form.validate()) {
        if (this.src != null) {
          this.saveImage();
        }
        setTimeout(() => {
          this.emitData();
        }, 400);
      }
    },
     saveImage(e) {
      let reader = new FileReader();
      reader.readAsDataURL(this.src);
      reader.onload = (e) => {
        this.productImage = e.target.result;
      };
    },
    emitData() {
      let product = {
        ProductName: this.productName,
        ProductQtd: this.productQtd,
        ProductImage: this.productImage,
      };
      this.$emit("action", product);
    },
  },
};
</script>

<style>
.row-modal {
  margin: 0 !important;
}
</style>