<template>
  <v-row class="row-modal" justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="#105269" v-bind="attrs" v-on="on">
          <v-icon color="white">mdi-cloud-download</v-icon>
        </v-btn>
      </template>
      <v-card>
        <v-card-title class="justify-center">
          <span class="text-h5">Download Product</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row align="center" justify="space-around">
              <v-btn @click="pdf" color="#105269" dark
                ><v-icon>mdi-file-pdf-box</v-icon><span>PDF</span>
              </v-btn>

              <v-btn @click="excel" color="#105269" dark
                ><v-icon>mdi-file-excel</v-icon><span>EXCEL</span>
              </v-btn>
            </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions class="justify-center">
          <v-btn color="red darken-1" width="90%" @click="dialog = false">
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
import ApiService from "../../../services/ApiService";

export default {
  data: () => ({
    dialog: false,
    url: "",
    src: null,
    file: "",
    api: new ApiService(),
  }),
  methods: {
    excel() {
      this.api
        .get("/ProductOrder/excel", {})
        .then((response) => {
          this.$toast.success("Ordem gerada em Excel");
          this.file = response.message;
          this.file = "data:application/octet-stream;base64," + this.file;
          const link = document.createElement("a");
          link.href = this.file;
          link.setAttribute("download", `order.csv`);
          document.body.appendChild(link);
          link.click();
          console.log(response);
        })
        .catch((error) => console.log(error));
    },
    pdf() {
      this.api
        .get("/ProductOrder/pdf")
        .then((response) => {
          this.$toast.success("Ordem gerada em Pdf");
          this.pdffile = response;
          this.pdffile = "data:application/octet-stream;base64," + this.pdffile;
          const link = document.createElement("a");
          link.href = this.pdffile;
          link.setAttribute("download", `Order.pdf`);
          document.body.appendChild(link);
          link.click();
          console.log(response);
        })
        .catch((error) => console.log(error));
    },
  },
};
</script>