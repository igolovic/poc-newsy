<template>
  <b-card class="item">
    <div v-html="item.content"></div>
  </b-card>
</template>

<script>
export default {
  name: "ArticleViewer",
  props: {
    item: Object,
    index: Number,
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.item {
  margin: 2px;
}
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
