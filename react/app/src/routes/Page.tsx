import React, { useContext } from 'react';
import logo from '../logo.svg';
import '../App.css';
import { useParams, useLoaderData, LoaderFunction } from 'react-router-dom';
import { getPageContent } from '../util/contentUtil';
import ReactMarkdown from 'react-markdown';
import { MetaContext } from '../context/metaContext';
import { Helmet } from 'react-helmet';
import rehypeRaw from 'rehype-raw';

function Page() {
    const meta = useContext(MetaContext);
    var params = useParams();
    const pageTitle = !!params.slug ? params.slug + " | " : "";
    var content = useLoaderData() as string;
  return (
    <>
      <Helmet>
        <title>{pageTitle}{meta.siteName}</title>
      </Helmet>  
      <ReactMarkdown rehypePlugins={rehypeRaw as any}>{content}</ReactMarkdown>
    </>
  );
}

export async function loader({ params } : any) {
    const content = await getPageContent(params.slug);
    if (!content) throw new Response("", { status: 404 });
    return content;
}


export default Page;
