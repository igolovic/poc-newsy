<template>
  <b-container id="mainContainer">
    <b-row class="w-100">
        <b-button class="btn login" @click="login" v-if="!isLoggedIn">Login</b-button>
        <label v-if="!isLoggedIn">Log into the editor-viewer application</label>

        <b-button class="btn login" @click="logout" v-if="isLoggedIn">Logout</b-button>
        <label v-if="isLoggedIn">{{ userUserName }}</label>

    </b-row>
    <b-row class="w-100" v-if="isLoggedIn"
      ><ArticleUsers :users="users" ref="articleUserComp"
    /></b-row>
    <b-row class="w-100">
      <b-col v-if="isLoggedIn"
        ><ArticleEditor
          :content="selectedArticleContent"
          ref="articleEditorComp"
      /></b-col>
      <b-col v-if="isLoggedIn">
        <ArticleViewer
          v-for="(item, key, index) in articles"
          :item="item"
          :index="index"
          :key="key"
        />
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import ArticleEditor from "./components/ArticleEditor.vue";
import ArticleUsers from "./components/ArticleUsers.vue";
import ArticleViewer from "./components/ArticleViewer.vue";

import axios from "axios";

import { UserManager, WebStorageStateStore } from "oidc-client";

const authorizationHeader = "Authorization";

export default {
  name: "App",
  data() {
    return {
      selectedArticleId: "",
      selectedArticleContent: "",
      articles: [],
      users: [],
      selectedUsers: [],
      userId: "e95fa28b-1ed1-4a1b-a981-a4e608ca7cda",
      userFirstName: "John",
      userUserName: "jd",
      userManager: Object,
      isLoggedIn: false,
      accessTokenExpired: false,
      settings: {
        userStore: new WebStorageStateStore({ store: window.localStorage }),
        authority: "https://host.docker.internal:44343",
        client_id: "newsy-editor-viewer-application",
        redirect_uri: "https://localhost:8080/callback.html",
        automaticSilentRenew: true,
        silent_redirect_uri: "https://localhost:8080/silent-renew.html",
        response_type: "code",
        scope: "newsy-api.read newsy-api.write",
        post_logout_redirect_uri: "https://localhost:8080/",
        filterProtocolClaims: true,
      },
    };
  },
  components: {
    ArticleEditor,
    ArticleUsers,
    ArticleViewer,
  },
  mounted() {
    this.userManager.getUser().then((user) => {
      if (user) {
        this.userUserName = user.name;
        this.accessTokenExpired = user.expired;

        this.isLoggedIn = user !== null && !user.expired;
        if (this.isLoggedIn) {
          this.getAuthors();
          this.getArticles();
        }
      }
      else{
        this.isLoggedIn = false;
        this.accessTokenExpired = true;
      }
    });
  },
  created: function () {
    console.log("created");
    this.userManager = new UserManager(this.settings);
  },
  computed: {
  },
  watch: {},
  methods: {
    uuidv4() {
      return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (c) =>
        (
          c ^
          (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
        ).toString(16)
      );
    },

    getArticles() {
      this.getAccessToken().then((userToken) => {
        axios.defaults.headers.common[
          authorizationHeader
        ] = `Bearer ${userToken}`;

        axios
          .get("https://localhost:5001/Article")
          .then((res) => {
            this.articles = res.data;
          })
          .catch(function (error) {
            console.log("[newsy-editor-viewer] ERROR");
            if (error.response) {
              console.log(error.response.data);
              console.log(error.response.status);
              console.log(error.response.headers);
            } else if (error.request) {
              console.log(error.request);
            } else {
              console.log(error.message);
            }
            console.log(error.config);
          });
      });
    },

    getAuthors() {
      this.getAccessToken().then((userToken) => {
        axios.defaults.headers.common[
          authorizationHeader
        ] = `Bearer ${userToken}`;

        axios
          .get("https://localhost:5001/User")
          .then((res) => {
            this.users = res.data;
          })
          .catch(function (error) {
            console.log("[newsy-editor-viewer] ERROR");
            if (error.response) {
              console.log(error.response.data);
              console.log(error.response.status);
              console.log(error.response.headers);
            } else if (error.request) {
              console.log(error.request);
            } else {
              console.log(error.message);
            }
            console.log(error.config);
          });
      });
    },

    createNewArticle($value) {
      if (!$value) {
        alert("Please enter text or HTML content for new article");
        return;
      }

      if (this.selectedUsers.length == 0) {
        alert("Please select at least one user/author");
        return;
      }

      let aid = this.uuidv4();
      let date = new Date();
      let articleUsers = [];
      for (let selUserId of this.selectedUsers) {
        articleUsers.push({ articleId: aid, user_id: selUserId });
      }
      let param = {
        id: aid,
        content: $value,
        created: date.toISOString(),
        articleUser: articleUsers,
      };

      this.getAccessToken().then((userToken) => {
        axios.defaults.headers.common[
          authorizationHeader
        ] = `Bearer ${userToken}`;

        axios
          .post("https://localhost:5001/Article", param)
          .then(() => {
            this.getArticles();
            this.$refs.articleUserComp.clearControls();
            this.$refs.articleEditorComp.clearControls();
          })
          .catch(function (error) {
            console.log("[newsy-editor-viewer] ERROR");
            if (error.response) {
              console.log(error.response.data);
              console.log(error.response.status);
              console.log(error.response.headers);
            } else if (error.request) {
              console.log(error.request);
            } else {
              console.log(error.message);
            }
            console.log(error.config);
          });
      });
    },

    selectedAuthorsChanged($value) {
      this.selectedUsers = $value;
    },

    getUser() {
      return this.userManager.getUser();
    },

    login() {
      return this.userManager.signinRedirect();
    },

    logout() {
      return this.userManager.signoutRedirect();
    },

    getAccessToken() {
      return this.userManager.getUser().then((data) => {
        return data.access_token;
      });
    },
  },
  provide() {
    return {
      createNewArticleProvide: this.createNewArticle,
      selectedUsersChangedProvide: this.selectedAuthorsChanged,
    };
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#mainContainer{
  margin-top: 10px;
}
.login{
  margin-right:10px;
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
