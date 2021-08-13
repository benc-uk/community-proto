const getAllMembers = async () => {
  const url = `${process.env.API_ENDPOINT || ''}/users`
  let response = await fetch(url, {
    method: 'GET',
  })

  if (response.ok) {
    let respData = await response.json()
    return respData
  } else {
    throw new Error(`API Error: ${url} ${response.statusText}`)
  }
}

const getMyCommunities = async (userId) => {
  const url = `${process.env.API_ENDPOINT || ''}/communities/joinedBy/${userId}`
  let response = await fetch(url, {
    method: 'GET',
  })

  if (response.ok) {
    let respData = await response.json()
    return respData
  } else {
    throw new Error(`API Error: ${url} ${response.statusText}`)
  }
}

const getNotJoinedCommunities = async (userId) => {
  const url = `${process.env.API_ENDPOINT || ''}/communities/notJoinedBy/${userId}`
  let response = await fetch(url, {
    method: 'GET',
  })

  if (response.ok) {
    let respData = await response.json()
    return respData
  } else {
    throw new Error(`API Error: ${url} ${response.statusText}`)
  }
}

const getCommunity = async (id) => {
  const url = `${process.env.API_ENDPOINT || ''}/communities/${id}`
  let response = await fetch(url, {
    method: 'GET',
  })

  if (response.ok) {
    let respData = await response.json()
    return respData
  } else {
    throw new Error(`API Error: ${url} ${response.statusText}`)
  }
}

const isMember = async (communityId, userId) => {
  const url = `${process.env.API_ENDPOINT || ''}/communities/${communityId}/isMember/${userId}`
  let response = await fetch(url, {
    method: 'GET',
  })

  if (response.ok) {
    return true
  } else {
    return false
  }
}

const getDiscussions = async (communityId) => {
  const url = `${process.env.API_ENDPOINT || ''}/discussions/inCommunity/${communityId}`
  let response = await fetch(url, {
    method: 'GET',
  })

  if (response.ok) {
    let respData = await response.json()
    return respData
  } else {
    throw new Error(`API Error: ${url} ${response.statusText}`)
  }
}

export { getAllMembers, getMyCommunities, getNotJoinedCommunities, getCommunity, isMember, getDiscussions }
