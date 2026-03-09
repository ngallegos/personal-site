import { Resume } from '../model/resume';
import { SiteMetaData } from '../model/sitemetadata';
import { Post } from '../model/post';

export const contentApiRoot = import.meta.env.VITE_API_GATEWAY_URL;
export const contentDomain = import.meta.env.VITE_CONTENT_DOMAIN;

export async function getPageContent(slug: string): Promise<string> {
    slug = slug || "home";
    const response = await fetch(`${contentApiRoot}/${contentDomain}/page/${slug}`);
    return response.text();
}

export async function getSiteMetadata(): Promise<SiteMetaData> {
    const response = await fetch(`${contentApiRoot}/${contentDomain}/meta`);
    return await response.json() as SiteMetaData;
}

export async function getSiteResume(): Promise<Resume> {
    const response = await fetch(`${contentApiRoot}/${contentDomain}/resume`);
    return await response.json() as Resume;
}

export async function getBlogPosts(tag?: string, page: number = 1): Promise<Post[]> {
    const params = new URLSearchParams();
    if (tag) params.set('tag', tag);
    if (page > 1) params.set('page', page.toString());
    const query = params.toString() ? `?${params}` : '';
    const response = await fetch(`${contentApiRoot}/${contentDomain}/blog${query}`);
    return await response.json() as Post[];
}

export async function getBlogPost(slug: string): Promise<Post | null> {
    const response = await fetch(`${contentApiRoot}/${contentDomain}/blog/${slug}`);
    if (!response.ok) return null;
    return await response.json() as Post;
}
