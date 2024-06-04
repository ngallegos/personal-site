import React from 'react';
import logo from '../logo.svg';
import '../App.css';
import { useParams, useLoaderData } from 'react-router-dom';
import { getPageContent } from '../util/contentUtil';
import ReactMarkdown from 'react-markdown';

function Page() {
    var params = useParams();
    var content = useLoaderData();
  return (
    <ReactMarkdown>{content}</ReactMarkdown>
  );
}

export async function loader({ params}){
    const content = await getPageContent(params.slug);
    if (!content) throw new Response("", { status: 404 });
    return content;
}


export default Page;
