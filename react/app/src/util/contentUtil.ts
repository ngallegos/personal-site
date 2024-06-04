import { get } from "http";

export const contentApiRoot = process.env.REACT_APP_API_GATEWAY_URL;
export const contentDomain = process.env.REACT_APP_CONTENT_DOMAIN;

export async function getPageContent(slug: string): Promise<string> {
    const response = await fetch(`${contentApiRoot}/${contentDomain}/page/${slug}`);
    return response.text();
}