import { getCommunity, isMember, getDiscussions } from '../../lib/api'
import DiscussionList from '../../components/discussions-list'

export default function MyCommunities({ community, error, joined, discussions }: any) {
  const joinButton = joined ? null : (
    <button v-if="!isMember" className="button is-success is-pulled-right is-medium" onClick={() => {}}>
      <i className="fas fa-user-plus"></i>&nbsp; Join Community
    </button>
  )

  const startDiscussion = joined ? (
    <button v-if="!isMember" className="button is-success is-pulled-right is-medium" onClick={() => {}}>
      <i className="fas fa-comments"></i>&nbsp; Start New Discussion
    </button>
  ) : null

  const errorBox = error ? <div className="notification is-danger">{error}</div> : null
  const progressBox = !community && !error ? <progress className="progress is-large is-info m-5" max="100"></progress> : null

  return (
    <div>
      {errorBox}
      {progressBox}
      <section className="hero is-primary is-medium cover" style={{ backgroundImage: `url('${community.image}')` }}>
        <div className="hero-body">
          <p className="title">{community.name}</p>
          <p className="subtitle">{community.memberCount} members</p>
          {joinButton}
          {startDiscussion}
        </div>
      </section>
      <DiscussionList discussions={discussions}></DiscussionList>
    </div>
  )
}

export async function getServerSideProps(context: any) {
  let error = null
  let community = null
  let joined = false
  let discussions = null
  const { id } = context.query
  const userId = 'demo@example.net'

  try {
    community = await getCommunity(id)
    joined = await isMember(id, userId)
    discussions = await getDiscussions(id)
  } catch (e) {
    error = e.message
  }

  return {
    props: { community, error, joined, discussions },
  }
}
