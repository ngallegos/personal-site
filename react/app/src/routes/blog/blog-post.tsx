import { Link, useLoaderData } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import { useContext, useEffect } from 'react';
import ReactMarkdown from 'react-markdown';
import rehypeRaw from 'rehype-raw';
import hljs from 'highlight.js';
import { MetaContext } from '../../context/metaContext';
import { getBlogPost } from '../../util/contentUtil';
import { Post } from '../../model/post';

function BlogPost() {
    const post = useLoaderData() as Post;
    const meta = useContext(MetaContext);

    useEffect(() => {
        hljs.highlightAll();
    });

    const pageTitle = `${post.title} | ${meta.siteName}`;

    return (
        <>
            <Helmet prioritizeSeoTags>
                <title>{pageTitle}</title>
            </Helmet>

            <article>
                <h1>{post.title}</h1>

                <div>
                    {post.sys?.createdAt && (
                        <time>{new Date(post.sys.createdAt).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' })}</time>
                    )}
                    {post.tags.length > 0 && (
                        <span>
                            {post.tags.map((tag, i) => (
                                <span key={i}>{' '}<Link to={`/blog?tag=${encodeURIComponent(tag.name)}`}>{tag.name}</Link></span>
                            ))}
                        </span>
                    )}
                </div>

                {post.featuredImage && (
                    <img src={post.featuredImage.file.url} alt={post.featuredImage.description} />
                )}

                <ReactMarkdown rehypePlugins={rehypeRaw as any}>{post.content}</ReactMarkdown>
            </article>
        </>
    );
}

export async function loader({ params }: any): Promise<Post> {
    const post = await getBlogPost(params.slug);
    if (!post) throw new Response("", { status: 404 });
    return post;
}

export default BlogPost;
