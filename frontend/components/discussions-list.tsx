export default function CommunitiesList({ discussions, error }: any) {
  const discussionCards = []

  for (let discussion of discussions) {
    discussionCards.push(
      <div className="card is-fullwidth m-3">
        <div className="card-content">
          <div className="media">
            <div className="media-content">
              <p className="title is-4">{discussion.title}</p>
              <p className="subtitle is-6">{discussion.created}</p>
            </div>
          </div>

          <div className="content">{discussion.body}</div>
        </div>
      </div>
    )
  }

  const errorBox = error ? <div className="notification is-danger">{error}</div> : null
  const progressBox = !discussions && !error ? <progress className="progress is-large is-info m-5" max="100"></progress> : null
  return (
    <div className="column">
      {errorBox}
      {progressBox}
      {discussionCards}
    </div>
  )
}
