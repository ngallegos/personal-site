export interface Resume {
    resumeName: string;
    name: string;
    title: string;
    skills: string[];
    tools: string[];
    concepts: string[];
    location: string | null;
    email: string;
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
    heading: string;
    subHeading: string | null;
    category: string;
    content: string;
}