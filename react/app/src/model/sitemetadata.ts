export interface SiteMetaData {
    siteName: string;
    navLinks: Link[];
    contactLinks: Link[];
    headMetaData: HeadMeta[];
    aboutMe: string | null;
}

export interface Link {
    slug: string;
    text: string;
    external: boolean | null;
}

export interface HeadMeta {
    name: string;
    content: string;
}