<template>
  <div id="app">
        <v-card class="mx-auto" style="height: 100vh">
          <v-card-actions class="btns">
            <download-order />
            <v-dialog max-width="435" v-model="dialogDelete">
            <DeleteBtn 
            @close="dialogDelete = false"
            @deletar="deleteOrder(arrId)"/>
            </v-dialog>
          </v-card-actions>
          <v-data-table
            :headers="headers"
            :items="orders"
            :actions="actions"
            disable-pagination
            hide-default-footer
          >
            <template v-slot:top>
              <v-text-field
                v-model="search"
                label="Search"
                @keyup.capture="loadproducts"
                class="mx-4"
              ></v-text-field>
            </template>
             <template v-slot:item.actions="{ item }">
          <v-icon
            @click="
              arrId = item.id;
              dialogDelete = true;
            "
            >mdi-delete</v-icon
          >
             </template>
          </v-data-table>
          <v-pagination v-model="page" :length="totalPages"> </v-pagination>
        </v-card>
  </div>
</template>

<script>
import ApiService from "../../../services/ApiService.js";
import DeleteBtn from "./modais/Delete.vue";
import DownloadOrder from "../../components/ExportOrders/ExportOrders.vue";
export default {
  components: {
    DownloadOrder,
    DeleteBtn,
  },
  data: () => ({
    api: new ApiService(),
    orders: [],
    dialog: false,
    url: "",
    src: "",
    isActive: false,
    arrId: 0,
    dialogAdd: false,
    dialogDelete: false,
    urlEnv: process.env.VUE_APP_BASE_URL,
    order: {
      id: null,
      productId: "",
      productName: "",
      quantity: "",
      price: "",
      orderType: "",
    },
    headers: [
      {
        text: "ID",
        align: "start",
        sortable: false,
        value: "id",
      },
      { text: "PRODUCTNAME", value: "product.productName" },
      { text: "QUANTITY", value: "quantity" },
      { text: "PRICE", value: "price" },
      { text: "ORDERTYPE", value: "orderType" },
      { text: "ACTION", value: "actions" },
    ],
    user: {},
    profilePage: "/ProfilePage",
    search: "",
    page: 1,
    totalPages: 0,
  }),
  created() {
    this.loadproducts();
    let userString = localStorage.getItem("user");
    this.user = JSON.parse(userString);
    if (this.user.image) {
      this.src = this.urlEnv + "/" + this.user.image;
      this.isActive = true;
    }
  },
  methods: {
     deleteOrder(id) {
      this.api
        .delete("/ProductOrder/delete?ids=" + id)
        .then((response) => {
          this.$emit("reload");
          this.dialogDelete = false;
          this.$toast.success("Ordem deletada com sucesso");
          this.loadproducts();
        })
        .catch((error) => {
          this.dialog = false;
        });
    },
    loadproducts() {
      this.api
        .post(`/productOrder?searchString=${this.search}&page=${this.page}`)
        .then((response) => {
          this.orders = response.content.orders;
          this.totalPages = response.content.pager.totalPages;
        })
        .catch((erro) => console.log(erro));
    },
    save(e) {
      let reader = new FileReader();
      reader.readAsDataURL(this.url);
      reader.onload = (e) => {
        this.dialog = false;
        this.src = e.target.result;
        localStorage.setItem("ProfileImg", this.src);
      };
      this.isActive = true;
    },
  },
  watch: {
    page() {
      this.loadproducts();
    },
  },
};
</script>

<style>
.btns {
  display: flex;
  gap: 2rem;
  margin: auto;
}

.movi {
  display: flex;
  top: 10px;
  left: 540px;
}
</style>