import { Link, useLoaderData, useSearchParams } from 'react-router-dom';
import { Helmet } from 'react-helmet-async';
import { useContext } from 'react';
import { MetaContext } from '../../context/metaContext';
import { getBlogPosts } from '../../util/contentUtil';
import { Post } from '../../model/post';

interface BlogLoaderData {
    posts: Post[];
    tag: string | null;
    page: number;
}

function Blog() {
    const { posts, tag, page } = useLoaderData() as BlogLoaderData;
    const meta = useContext(MetaContext);
    useSearchParams();

    const pageTitle = `Blog${tag ? ` — ${tag}` : ''} | ${meta.siteName}`;

    function buildHref(newPage: number, newTag?: string | null) {
        const params = new URLSearchParams();
        const t = newTag !== undefined ? newTag : tag;
        if (t) params.set('tag', t);
        if (newPage > 1) params.set('page', newPage.toString());
        return `/blog${params.toString() ? `?${params}` : ''}`;
    }

    return (
        <>
            <Helmet prioritizeSeoTags>
                <title>{pageTitle}</title>
            </Helmet>

            <h1>Blog</h1>

            {tag && (
                <p>
                    Filtered by: <strong>{tag}</strong> &mdash; <Link to="/blog">Clear filter</Link>
                </p>
            )}

            {posts.length === 0 ? (
                <p>No posts found.</p>
            ) : (
                <>
                    <ul>
                        {posts.map((post, index) => (
                            <li key={index}>
                                <Link to={`/blog/${post.slug}`}>{post.title}</Link>
                                {post.sys?.createdAt && (
                                    <span> &mdash; <time>{new Date(post.sys.createdAt).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' })}</time></span>
                                )}
                                {post.tags.length > 0 && (
                                    <span>
                                        {post.tags.map((t, i) => (
                                            <span key={i}>{' '}<Link to={`/blog?tag=${encodeURIComponent(t.name)}`}>{t.name}</Link></span>
                                        ))}
                                    </span>
                                )}
                            </li>
                        ))}
                    </ul>

                    {(page > 1 || posts.length === 10) && (
                        <nav>
                            {page > 1 && <Link to={buildHref(page - 1)}>Previous</Link>}
                            {posts.length === 10 && <Link to={buildHref(page + 1)}>Next</Link>}
                        </nav>
                    )}
                </>
            )}
        </>
    );
}

export async function loader({ request }: any): Promise<BlogLoaderData> {
    const url = new URL(request.url);
    const tag = url.searchParams.get('tag') || undefined;
    const page = parseInt(url.searchParams.get('page') || '1');
    const posts = await getBlogPosts(tag, page).catch(() => []);
    return { posts, tag: tag ?? null, page };
}

export default Blog;
