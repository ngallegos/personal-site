import { Resume } from '../model/resume';
import { SiteMetaData } from '../model/sitemetadata';

export const contentApiRoot = process.env.REACT_APP_API_GATEWAY_URL;
export const contentDomain = process.env.REACT_APP_CONTENT_DOMAIN;

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
