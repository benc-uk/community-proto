import { getAllMembers } from '../lib/api'
import Image from 'next/image'
import { useRouter } from 'next/router'
import { useEffect } from 'react'

export default function Members({ members, error }: any) {
  const router = useRouter()

  // This totally bizarre code sets up server side refreshing of the data
  // NOT NEEDED! BUT LEFT FOR REFERENCE
  useEffect(() => {
    const timerId = setInterval(() => {
      router.replace(router.asPath)
    }, 5000)
    return () => clearInterval(timerId)
  })

  const memberCards = []

  for (let member of members) {
    memberCards.push(
      <div className="card member-card">
        <div className="card-content">
          <div className="media">
            <div className="media-left">
              <figure className="image is-128x128">
                <Image src={member.avatar} alt={`Picture of ${member.name}`} layout="fill" />
              </figure>
            </div>
            <div className="media-content">
              <p className="title is-4">{member.name}</p>
            </div>
          </div>

          <div className="content">{member.about}</div>
        </div>
      </div>
    )
  }

  const errorBox = error ? <div className="notification is-danger">{error}</div> : null
  const progressBox = (!members || members.length <= 0) && !error ? <progress className="progress is-large is-info m-5" max="100"></progress> : null
  return (
    <div className="is-flex is-flex-wrap-wrap">
      {errorBox}
      {progressBox}
      {memberCards}
    </div>
  )
}

export async function getServerSideProps(context: any) {
  let error = null
  let members = []

  try {
    members = await getAllMembers()
  } catch (e) {
    error = e.message
  }

  return {
    props: { members, error },
  }
}
