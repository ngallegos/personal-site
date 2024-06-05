export interface Resume {
    resumeName: string;
    name: string;
    title: string | null;
    skills: string[];
    tools: string[];
    concepts: string[];
    location: string | null;
    email: string | null;
    phone: string | null;
    website: string | null;
    gitHub: string | null;
    linkedIn: string | null;
    active: boolean;
    sections: ResumeSection[];
    initials: string[];
    cleanWebsite: string;
    gitHubUsername: string;
    experience: ResumeSection[];
    education: ResumeSection[];
    summary: ResumeSection[];
}

export interface ResumeSection {
    heading: string | null;
    subHeading: string | null;
    category: string | null;
    content: string | null;
}