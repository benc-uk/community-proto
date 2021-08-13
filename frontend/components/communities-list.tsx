import Image from 'next/image'
import Link from 'next/link'

export default function CommunitiesList({ communities, error }: any) {
  const communityCards = []

  if (!error) {
    for (let community of communities) {
      communityCards.push(
        <Link href={`/community/${community.id}`} passHref={true}>
          <div className="card comm-card" style={{ cursor: 'pointer' }}>
            <div className="card-image">
              <figure className="image is-4by3">
                <Image src={community.image} alt="Community image" layout="fill" />
              </figure>
            </div>

            <div className="card-content">
              <div className="media">
                <div className="media-content">
                  <p className="title is-4">{community.name}</p>
                  <p className="subtitle is-6">{community.members}</p>
                </div>
              </div>

              <div className="content">{community.description}</div>
            </div>
          </div>
        </Link>
      )
    }
  }

  const errorBox = error ? <div className="notification is-danger">{error}</div> : null
  const progressBox = !communities && !error ? <progress className="progress is-large is-info m-5" max="100"></progress> : null
  return (
    <div className="is-flex is-flex-wrap-wrap">
      {errorBox}
      {progressBox}
      {communityCards}
    </div>
  )
}
