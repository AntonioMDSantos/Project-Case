<template>
  <div id="app">
    <v-card class="mx-auto" style="height: 100vh">
      <v-card-actions class="justify-end">
        <v-dialog max-width="550" v-model="dialogAdd">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="#105269" dark v-bind="attrs" v-on="on">
              Create
            </v-btn>
          </template>
          <AddBtn @close="dialogAdd = false" @action="create" />
        </v-dialog>
        <v-dialog max-width="550" v-model="dialogUpdate">
          <UpdateBtn
            @action="updateProduct"
            @close="dialogUpdate = false"
            :product="product"
          />
        </v-dialog>
        <v-dialog max-width="475" v-model="dialogDelete">
          <DeleteBtn
            @close="dialogDelete = false"
            @deletar="deleteProduct(arrId)"
          />
        </v-dialog>
        <v-dialog max-width="475" v-model="dialogAddOrder">
          <AddOrder
            @close="dialogAddOrder = false"
            @action="createOrder"
            :product="product"
            :ProductsName="productNames"
          />
        </v-dialog>
      </v-card-actions>
      <download-product />
      <v-data-table
        :headers="headers"
        :items="products"
        hide-default-footer
        disable-pagination
      >
        <template v-slot:top>
          <v-text-field
            v-model="search"
            label="Search"
            @keyup.capture="loadproducts"
            class="mx-4"
          ></v-text-field>
        </template>
        <template v-slot:item.productImage="{ item }">
          <v-img
            :src="urlEnv + '/' + item.productImage"
            max-height="50px"
            max-width="50px"
          ></v-img>
        </template>
        <template v-slot:item.actions="{ item }">
          <div class="icon-pai">
            <v-icon
              class="icon-filho"
              color="#105269"
              size="35"
              @click="
                product = item;
                dialogUpdate = true;
              "
              >mdi-pen</v-icon
            >
            <v-icon
              class="icon-filho"
              color="#105269"
              size="35"
              @click="
                arrId = item.id;
                dialogDelete = true;
              "
              >mdi-delete</v-icon
            >
          </div>
        </template>
        <template v-slot:item.order="{ item }">
          <div class="icon-pai">
            <div class="icon-filho">
              <v-icon
                @click="
                  dialogAddOrder = true;
                  product = item;
                "
                color="#105269"
                size="35"
              >
                mdi-chart-bar
              </v-icon>
            </div>
          </div>
        </template>
      </v-data-table>
      <v-pagination v-model="page" :length="totalPages"> </v-pagination>
    </v-card>
  </div>
</template>

<script>
import ApiService from "../../../services/ApiService.js";
import AddBtn from "./modais/Add.vue";
import AddOrder from "./modais/AddOrder.vue";
import UpdateBtn from "./modais/Update.vue";
import DeleteBtn from "./modais/Delete.vue";
import DownloadProduct from "../../components/ExportProduct/ExportProduct.vue";
export default {
  components: {
    AddOrder,
    AddBtn,
    UpdateBtn,
    DeleteBtn,
    DownloadProduct,
  },
  data: () => ({
    api: new ApiService(),
    urlEnv: process.env.VUE_APP_BASE_URL,
    drawer: null,
    dialog: false,
    isActive: false,
    dialogOrder: false,
    search: "",
    productId: 0,
    productNames: [],
    dialogAdd: false,
    dialogUpdate: false,
    dialogDelete: false,
    dialogAddOrder: false,
    url: "",
    arrId: 0,
    page: 1,
    totalPages: 0,
    product: {},
    headers: [
      {
        text: "ID",
        align: "start",
        sortable: false,
        value: "id",
      },
      { text: "PRODUCT", value: "productName" },
      { text: "QUANTITY", value: "productQtd" },
      { text: "IMAGE", value: "productImage" },
      { text: "ACTION", value: "actions" },
      { text: "ORDER", value: "order" },
    ],
    user: {},
    products: [],
    src: "",
    profilePage: "/ProfilePage",
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
    loadproducts() {
      console.log(this.search);
      this.api
        .post(`/product?searchString=${this.search}&page=${this.page}`)
        .then((response) => {
          this.products = response.content.products;
          this.totalPages = response.content.pager.totalPages;
          this.products.map((product) => {
            this.productNames.push(product.productName);
          });
        })
        .catch((erro) => console.log(erro));
    },
    createOrder(e) {
      let orderModal = {
        ProductId: e.productid,
        Price: e.price,
        Quantity: e.quantity,
        OrderType: e.type,
      };
      this.api
        .post("/ProductOrder/add", orderModal)
        .then((response) => {
          this.$toast.success(response.message);
          this.loadproducts();
          this.dialogAddOrder = false;
        })
        .catch((error) => {
          this.dialogAddOrder = false;
        });
    },
    create(e) {
      let productService = {
        ProductName: e.ProductName,
        ProductQtd: e.ProductQtd,
        ProductImage: e.ProductImage,
      };
      this.api
        .post("/Product/add", productService)
        .then((response) => {
          this.loadproducts();
          this.dialogAdd = false;
          this.$toast.success(response.message);
        })
        .catch((error) => {
          this.$toast.error(error.message);
        });
    },
    save(e) {
      let reader = new FileReader();
      reader.readAsDataURL(this.url);
      reader.onload = (e) => {
        this.dialog = false;
        this.src = e.target.result;
        localStorage.setItem("ProfileImg", this.src);
      };
    },
    getProductImage(url) {
      if (url != null) {
        this.api.get(`/${url}`).then((response) => {
          console.log(response);
        });
      }
    },
    deleteProduct(id) {
      this.api
        .delete("/Product/delete?ids=" + id)
        .then((response) => {
          console.log(response);
          this.$emit("reload");
          this.dialogDelete = false;
          this.$toast.success("Produto deletado com sucesso");
          this.loadproducts();
        })
        .catch((error) => {
          this.$toast.error("Erro ao deletar o produto");
        });
    },
    updateProduct(e) {
      let productModal = {
        id: e.id,
        productName: e.productName,
        productQtd: e.productQtd,
        productImage: e.productImage,
      };
      this.api
        .put("/Product/update", productModal)
        .then((response) => {
          console.log(response);
          this.dialogUpdate = false;
          this.loadproducts();
          this.$toast.success(response.message);
        })
        .catch((error) => {
          this.dialogUpdate = false;
        });
    },
  },
  watch: {
    page() {
      this.loadproducts();
    },
    product() {
      console.log(this.product);
    },
  },
};
</script>

<style>
.movi {
  display: flex;
  top: 10px;
  left: 540px;
}
.profile {
  border-radius: 50%;
  cursor: pointer;
  margin: auto;
}
.avatar {
  display: none;
}
</style>