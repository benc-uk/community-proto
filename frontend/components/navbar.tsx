import Link from 'next/link'
import Image from 'next/image'

export default function Layout({}) {
  return (
    <nav className="navbar is-info" role="navigation" aria-label="main navigation">
      <div className="navbar-brand">
        <Link href="/">
          <a className="navbar-item">
            <Image src="/img/community.svg" width="66" height="66" alt="Logo" /> &nbsp; Home
          </a>
        </Link>

        <a role="button" className="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div id="navbarBasicExample" className="navbar-menu">
        <div className="navbar-start">
          <Link href="/mycommunities">
            <a className="navbar-item">My Communities</a>
          </Link>
          <Link href="/discover">
            <a className="navbar-item">Discover</a>
          </Link>
          <Link href="/members">
            <a className="navbar-item">Members</a>
          </Link>
        </div>

        <div className="navbar-end">
          <div className="navbar-item">
            <div className="buttons">
              <a className="button is-primary">Log in</a>
            </div>
          </div>
        </div>
      </div>
    </nav>
  )
}
