// Global style sheet
import '../styles/globals.scss'

import type { AppProps } from 'next/app'
import Head from 'next/head'
import Layout from '../components/layout'

export default function MyApp({ Component, pageProps }: AppProps) {
  return (
    <>
      <Head>
        <title>Community Prototype</title>
        <meta name="viewport" content="initial-scale=1.0, width=device-width" />
        <link
          rel="icon"
          href="data:image/svg+xml,<svg xmlns=%22http://www.w3.org/2000/svg%22 viewBox=%220 0 100 100%22><text y=%22.9em%22 font-size=%2290%22>💬</text></svg>"
        />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
      </Head>
      <Layout>
        <section className="container">
          <Component {...pageProps} />
        </section>
      </Layout>
    </>
  )
}
