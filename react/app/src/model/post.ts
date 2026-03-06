export interface Post {
    title: string;
    slug: string;
    content: string;
    tags: Tag[];
    sticky: boolean;
    featuredImage: { file: { url: string }; description: string } | null;
    sys: { createdAt: string; updatedAt: string } | null;
}

export interface Tag {
    name: string;
    slug: string;
}
