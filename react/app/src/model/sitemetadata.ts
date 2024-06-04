export interface SiteMetaData {
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
    name: string | null;
    content: string | null;
}