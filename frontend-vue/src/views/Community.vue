<template>
  <div>
    <div v-if="error" class="notification is-danger">{{ error }}</div>
    <progress v-if="!community && !error" class="progress is-large is-info" max="100">-</progress>
    <div v-if="community">
      <section class="hero is-primary is-medium" v-bind:style="{ backgroundImage: 'url(' + community.image + ')' }">
        <div class="hero-body">
          <p class="title">{{ community.name }}</p>
          <p class="subtitle">{{ community.members.length }} members</p>
          <button v-if="isMember" class="button is-success is-pulled-right is-medium" @click="startDiscussion = true">
            <i class="far fa-comments"></i>&nbsp; Start New Discussion
          </button>
          <button v-if="!isMember" class="button is-success is-pulled-right is-medium" @click="join">
            <i class="fas fa-user-plus"></i>&nbsp; Join Community
          </button>
        </div>
      </section>

      <progress v-if="!discussions && !error" class="progress is-large is-info" max="100">-</progress>
      <div class="card" v-for="(discussion, index) of discussions" :key="index">
        <router-link :to="{ name: 'Discussion', params: { id: discussion.id } }">
          <div class="card-content">
            <div class="media">
              <div class="media-content">
                <p class="title is-4 has-background-info-dark p-3 has-text-white">
                  <i :class="'fas fa-' + discussion.icon"></i>&nbsp; {{ discussion.title }}
                </p>
                <p class="subtitle is-6">{{ discussion.created.replace('T', ' ').slice(0, -3) }}</p>
              </div>
            </div>

            <div class="content">
              {{ discussion.body }}
            </div>
          </div>
        </router-link>
      </div>
    </div>

    <div class="modal" :class="{ 'is-active': startDiscussion }">
      <div class="modal-background" />
      <div class="modal-content">
        <div class="box">
          <div class="field">
            <label class="label">Title</label>
            <div class="control">
              <input v-model="newDiscussionTitle" class="input" type="text" placeholder="Discussion title" />
            </div>
          </div>
          <div class="field">
            <label class="label">Icon</label>
            <div class="control">
              <div class="select">
                <select v-model="newDiscussionIcon">
                  <option value="comments">&#xf086; Comments</option>
                  <option value="comment-alt">&#xf27a; Comment</option>
                  <option value="dollar-sign">&#xf155; Money</option>
                  <option value="id-card">&#xf2c2; Identity</option>
                  <option value="truck">&#xf0d1; Truck</option>
                  <option value="cat">&#xf6be; Cat</option>
                  <option value="laptop-code">&#xf5fc; Code</option>
                </select>
              </div>
            </div>
          </div>
          <div class="field">
            <label class="label">Body</label>
            <div class="control">
              <textarea v-model="newDiscussionBody" class="textarea" placeholder="What do you want to discuss"></textarea>
            </div>
          </div>
          <button class="button is-success disabled" :disabled="!newDiscussionTitle || !newDiscussionBody" @click="createDiscussion">Create</button
          >&nbsp;
          <button class="button is-dark" @click="startDiscussion = false">Cancel</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import apiMixin from '../mixins/apiMixin.js'
import auth from '../services/auth.js'

export default {
  // Adds functions to call the API
  mixins: [apiMixin],

  data: function () {
    return {
      community: null,
      discussions: null,
      error: null,
      isMember: false,

      // Used by new discussion dialog, this should probably be in it's own component
      startDiscussion: false,
      newDiscussionTitle: '',
      newDiscussionBody: '',
      newDiscussionIcon: 'comments',
    }
  },

  mounted() {
    console.log(auth.user())
    this.getCommunity()
    // Update discussions every 5 seconds
    setInterval(this.fetchDiscussions, 5000)
  },

  methods: {
    getCommunity: async function () {
      try {
        this.community = await this.apiGetCommunity(this.$route.params.id)
        this.isMember = this.community.members.includes(auth.user().username)
        this.fetchDiscussions()
      } catch (err) {
        this.error = err
      }
    },

    fetchDiscussions: async function () {
      try {
        this.discussions = await this.apiGetCommunityDiscussions(this.$route.params.id)
      } catch (err) {
        this.error = err
      }
    },

    createDiscussion: async function () {
      try {
        await this.apiNewDiscussion(this.newDiscussionTitle, this.newDiscussionBody, this.newDiscussionIcon, this.community.id)
        this.startDiscussion = false
        this.newDiscussionTitle = ''
        this.newDiscussionBody = ''
        this.$nextTick(async function () {
          await this.fetchDiscussions()
        })
      } catch (err) {
        this.error = err
      }
    },

    join: async function () {
      try {
        await this.apiJoinCommunity(this.community.id, auth.user().username)
        this.isMember = true
        this.community.members.push(auth.user().username)
      } catch (err) {
        this.error = err
      }
    },
  },
}
</script>

<style scoped>
select,
option {
  font-family: 'Font Awesome 5 Free', BlinkMacSystemFont, -apple-system, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Fira Sans', 'Droid Sans',
    'Helvetica Neue', Helvetica, Arial !important;
  font-weight: 600;
}
.hero {
  background-size: cover;
}
.hero-body {
  text-shadow: 2px 2px 2px rgba(0, 0, 0, 0.36);
}
.button {
  box-shadow: 0px 7px 10px rgba(0, 0, 0, 0.3);
}
.card {
  margin: 1rem;
}
progress {
  margin: 3rem;
}
</style>
