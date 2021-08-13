/* istanbul ignore file */
import auth from '../services/auth'

//
// Mixin added to components, all API logic here
//

const apiScope = 'Communities.ReadWrite'

export default {
  methods: {
    apiGetAllCommunities: function () {
      return apiCall('/communities')
    },

    apiGetCommunity: function (id) {
      return apiCall(`/communities/${id}`)
    },

    apiGetCommunityDiscussions: function (id) {
      return apiCall(`/communities/${id}/discussions`)
    },

    apiNewDiscussion: function (title, body, icon, communityId) {
      return apiCall(`/discussions`, 'post', {
        title,
        body,
        icon,
        community: communityId,
      })
    },

    apiGetAllMembers: function () {
      return apiCall('/users')
    },

    apiJoinCommunity: function (communityId, userId) {
      return apiCall(`/communities/${communityId}/join/${userId}`, 'put')
    },

    apiGetUser: function (userId) {
      return apiCall(`/users/${userId}`)
    },

    apiAddUser: function (userId, name) {
      console.log(`name ${name}`)
      return apiCall(`/users`, 'post', {
        id: userId,
        name: name,
        about: 'New member',
        avatar: 'foo.png',
      })
    },
  },
}

//
// ===== Base fetch wrapper, not exported =====
//
async function apiCall(apiPath, method = 'get', data = null) {
  let headers = {}
  const url = `${process.env.VUE_APP_API_ENDPOINT || '/api'}${apiPath}`
  console.log(`### API CALL ${method} ${url}`)

  // Only get a token if logged in and configured
  if (auth.user() && process.env.VUE_APP_CLIENT_ID && apiScope) {
    // Try to get an access token with our API scope
    const accessToken = await auth.acquireToken([`api://${process.env.VUE_APP_CLIENT_ID}/${apiScope}`])

    // Send token as per the OAuth 2.0 bearer token scheme
    if (accessToken) {
      headers = {
        Authorization: `Bearer ${accessToken}`,
      }
    }
  }

  // Build request
  const request = {
    method,
    headers,
  }

  // Add payload if required
  if (data) {
    request.body = JSON.stringify(data)
    request.headers['Content-Type'] = 'application/json'
  }

  // Make the HTTP request
  const resp = await fetch(url, request)

  // Decode error message when non-HTTP OK (200~299) & JSON is received
  if (!resp.ok) {
    let error = `API call to ${url} failed with ${resp.status} ${resp.statusText}`
    if (resp.headers.get('Content-Type') === 'application/json') {
      error = `Status: ${resp.statusText}\n`
      let errorObj = await resp.json()
      for (const [key, value] of Object.entries(errorObj)) {
        error += `${key}: '${value}\n', `
      }
    }
    throw new Error(error)
  }

  // Attempt to return response body as data object if JSON
  if (resp.headers && resp.headers.get('Content-Type') && resp.headers.get('Content-Type').includes('application/json')) {
    return resp.json()
  } else {
    return resp.text()
  }
}
