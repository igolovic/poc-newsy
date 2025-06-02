<template>
  <div>
    <b-row>Add article text as plain text or HTML</b-row>
    <b-row>
      <b-form-textarea
        id="textarea"
        v-model="editableContent"
        placeholder="Article content"
        rows="3"
        max-rows="6"
      ></b-form-textarea>
    </b-row>
    <b-row> Preview </b-row>
    <b-row>
      <b-card class="w-100">
        <span v-html="editableContent"></span>
      </b-card>
    </b-row>
    <b-row class="float-right">
      <b-button id="addNewArticle" @click="createNewArticle">Create article</b-button>
    </b-row>
  </div>
</template>

<script>
export default {
  name: "ArticleEditor",
  props: {
    content: String,
  },
  inject:['createNewArticleProvide'],
  data() {
    return {
      editableContent: this.content,
    };
  },
  methods:{
    createNewArticle(){
      this.createNewArticleProvide(this.editableContent);
      this.editableContent='';
    },
    clearControls(){
      this.editableContent='';
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#addNewArticle{
  margin-top: 5px;
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
