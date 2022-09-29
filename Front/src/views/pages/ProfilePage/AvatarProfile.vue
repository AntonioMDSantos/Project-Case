<template>
  <div>
    <input type="file" accept="image/*" class="d-none" ref="file" @change="change" required>
    
    <div class="header">
      <img :src="src" class="img-profile" />
      <div class="icon">
        <v-icon @click="browse" color="black">mdi-camera</v-icon>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    value: File,
    defaultSrc: String,
  },
  data() {
    return {
      src: this.defaultSrc,
      file: null,
      url: "",
      avatar: "https://t3.ftcdn.net/jpg/01/18/01/98/360_F_118019822_6CKXP6rXmVhDOzbXZlLqEM2ya4HhYzSV.jpg"
    }
  },
  created() {
    this.url = process.env.VUE_APP_BASE_URL;
    this.getUser();
  },
  methods: {
    browse() {
      this.$refs.file.click();
    },
    change(e) {
      this.file = e.target.files[0];
      this.$emit('input', this.file);
      let reader = new FileReader();
      reader.readAsDataURL(this.file);
      reader.onload = (e) => {
        this.src = e.target.result;
        this.$emit("getImage", this.src);
      };
    },  
     getUser() {
      this.user = JSON.parse(localStorage.getItem("user"));
      if(this.user.image == null){
        this.src = this.avatar;
      }else{
        this.src = this.url + "/" + this.user.image;
      }
    },
  }
}
</script>

<style scoped>
.img-profile {
  width: 120px;
  height: 120px;
  border-radius: 50%;
  margin: auto;
}

.header {
  position: relative;
  display: inline-block;
}

.icon {
  position: absolute;
  top: 0;
  left: 2px;
  opacity: 0;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.icon:hover {
  opacity: 60%;
}
</style>