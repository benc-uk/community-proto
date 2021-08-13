import Navbar from './navbar'
import Footer from './footer'
//import React, { ReactChildren, ReactChild } from 'react'
type Props = {
  children: JSX.Element
}
export default function Layout({ children }: Props) {
  return (
    <>
      <Navbar />
      <main>{children}</main>
      <Footer />
    </>
  )
}
