<template>
  <div>
    <b-form-group label="Please select at least one author">
      <b-form-checkbox-group
        v-model="selectedUsers"
        :options="users"
        name="selectedUsers"
        value-field="id"
        text-field="lastName"
      ></b-form-checkbox-group>
    </b-form-group>
  </div>
</template>

<script>
export default {
  name: "ArticleUsers",
  props: ['users'],
  inject: ["selectedUsersChangedProvide"],
  data() {
    return {
      editableContent: this.content,
      selectedUsers: this.users
    };
  },
  methods: {
    clearControls() {
      this.selectedUsers = [];
    }
  },
  watch: {
    selectedUsers: function () {
      this.selectedUsersChangedProvide(this.selectedUsers);
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#addNewArticle {
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
