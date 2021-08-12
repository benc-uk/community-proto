import { getNotJoinedCommunities } from '../lib/api'
import CommunitiesList from '../components/communities-list'

export default function MyCommunities({ communities, error }: any) {
  return <CommunitiesList communities={communities} error={error}></CommunitiesList>
}

export async function getServerSideProps(context: any) {
  let error = null
  let communities = null
  const userId = 'demo@example.net'

  try {
    communities = await getNotJoinedCommunities(userId)
  } catch (e) {
    error = e.message
  }

  return {
    props: { communities, error },
  }
}
