import React from 'react';
import logo from '../logo.svg';
import '../App.css';
import { useParams, useLoaderData, LoaderFunction } from 'react-router-dom';
import { getPageContent } from '../util/contentUtil';
import ReactMarkdown from 'react-markdown';

function Page() {
    var params = useParams();
    var content = useLoaderData() as string;
  return (
    <ReactMarkdown>{content}</ReactMarkdown>
  );
}

export async function loader({ params } : any) {
    const content = await getPageContent(params.slug);
    if (!content) throw new Response("", { status: 404 });
    return content;
}


export default Page;
