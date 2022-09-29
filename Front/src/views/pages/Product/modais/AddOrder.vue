<template>
  <v-row class="row-modal" justify="center">
    <v-card width="100%">
      <v-card-title>
        <span class="text-h5">Create Order</span>
      </v-card-title>
      <v-card-text>
        <v-container>
          <v-form ref="form" class="form">
            <v-row>
              <v-col cols="12" sm="16" md="14">
                <v-autocomplete
                  disabled
                  label="Product"
                  :items="ProductsName"
                  v-model="product.productName"
                  required
                  outlined
                />
              </v-col>
              <v-col cols="12" sm="16" md="14">
                <v-text-field
                  type="number"
                  label="Quantity order"
                  v-model="quantity"
                  outlined
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="16" md="14">
                <v-text-field 
                type="number"
                label="Price"
                outlined 
                v-model="price" />
              </v-col>
              <v-col cols="12" sm="16">
                <v-select
                  :items="items"
                  v-model="type"
                  item-value="id"
                  item-text="name"
                  label="Type order"
                  required
                  outlined
                ></v-select>
              </v-col>
            </v-row>
          </v-form>
        </v-container>
      </v-card-text>
      <v-card-actions class="justify-center">
        <v-btn dark width="45%" color="red darken-1" @click="$emit('close')">
          Close
        </v-btn>
        <v-btn dark width="45%" color="#105269" @click="validateFields">
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
    api: new ApiService(),
    quantity: "",
    price: 0.0,
    user: {},
    type: "",
    items: [
      {
        id: "0",
        name: "Purchase",
      },
      {
        id: "1",
        name: "Sale",
      },
    ],
  }),
  created() {
    let userString = localStorage.getItem("user");
    let user = JSON.parse(userString);
    this.user = user;
  },
  props: {
    Order: Object,
    product: Object,
    ProductsName: Array,
  },
  methods: {
    validateFields() {
      var getPrice = parseFloat(this.price)
      var getQuantity = parseInt(this.quantity)
      var getType = parseInt(this.type)
      console.log(typeof(this.type))
      if (this.$refs.form.validate()) {
        let data = {
          productid: this.product.id,
          price: getPrice,
          quantity: getQuantity,
          type: getType,
        };
        this.$emit("action", data);
        this.fildReset();
      }
    },
    fildReset() {
      this.quantity = "";
      this.type = "";
      this.price = "";
    },
  },
};
</script>

<style>
.row-modal {
  margin: 0 !important;
}
</style>