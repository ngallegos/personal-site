import React, { useContext } from 'react';
import { useParams, useLoaderData } from 'react-router-dom';
import { getPageContent } from '../util/contentUtil';
import ReactMarkdown from 'react-markdown';
import { MetaContext } from '../context/metaContext';
import { Helmet } from 'react-helmet';
import rehypeRaw from 'rehype-raw';
import { useEffect } from 'react';
import hljs from 'highlight.js';
import 'highlight.js/styles/obsidian.css';

function Page() {
    const meta = useContext(MetaContext);
    var params = useParams();
    const pageTitle = !!params.slug ? params.slug + " | " : "";
    var content = useLoaderData() as string;

  useEffect(() => {
    hljs.highlightAll();
  });
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
