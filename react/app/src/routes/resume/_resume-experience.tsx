import { Resume } from "../../model/resume";
import ReactMarkdown from 'react-markdown';


export default function ResumeExperience({resume}: {resume: Resume}){

    return (
        <section className="experience pb-2 pb-4 mt-4 border-b-4 border-gray-300 first:mt-0">
            {/* <!-- To keep in the same column -------------------------------------------------------------------------> */}
            <section className="break-inside-avoid">
                <h2 className="mb-2 text-xl font-black tracking-widest text-gray-800 print:font-normal">
                    EXPERIENCE
                </h2>
                {/* @foreach (var (exp, ix) in Model.Experience.Select((s, i) => (s, i))) */}
                {
                    resume.experience.map((exp, ix) => {
                        var first = ix == 0;
                        var last = ix == resume.experience.length - 1;
                        return (
                            <section key={ix} className={"mb-2" + (last ? " border-b-0" : " border-b-2") + " border-gray-300 break-inside-avoid"}>
                                <header>
                                    <h3 className="font-semibold text-gray-800 text-md leading-snugish">
                                        {exp.heading}
                                    </h3>
                                    <p className="text-sm leading-normal text-gray-500">
                                        {exp.subHeading}
                                    </p>
                                </header>
                                <ReactMarkdown>{exp.content}</ReactMarkdown>
                            </section>);
                    })
                }
            </section>
        </section>
    );
}