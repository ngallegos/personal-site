import { Resume } from "../../model/resume";


export default function ResumeSkills({resume}: {resume: Resume}){

    return (
        <section className="skills pb-6 mt-0 mb-4 border-b-4 border-gray-300 first:mt-0 break-inside-avoid">
            {/* <!-- To keep in the same column --> */}
            <section className="break-inside-avoid">
                <h2 className="mb-2 text-lg font-bold tracking-widest text-gray-700 print:font-normal">
                    SKILLS
                </h2>
                <section className="mb-0 break-inside-avoid">
                    <section className="mt-1 last:pb-1">
                        <ul className="flex flex-wrap -mb-1 font-bold leading-relaxed text-md -mr-1.6">
                            {resume.skills.map((skill, ix) => {
                                return (<li key={ix} className="p-1.5 mb-1 leading-relaxed text-white bg-gray-800 mr-1.6 print:bg-white print:border-inset">
                                    {skill}
                                </li>);
                                })
                            }
                        </ul>
                    </section>
                </section>
                {!!resume.tools && resume.tools.length > 0 &&
                    <>
                    <h2 className="mb-2 text-lg font-bold tracking-widest text-gray-700 print:font-normal">
                        TOOLS
                    </h2>
                    <section className="mb-0 break-inside-avoid">
                        <section className="mt-1 last:pb-1">
                            <ul className="flex flex-wrap -mb-1 font-bold leading-relaxed text-md -mr-1.6">
                                {resume.tools.map((tool, ix) => {
                                    return (<li key={ix} className="p-1.5 mb-1 leading-relaxed text-white bg-gray-800 mr-1.6 print:bg-white print:border-inset">
                                        {tool}
                                    </li>);
                                    })
                                }
                            </ul>
                        </section>
                    </section>
                    </>
                }
                { !!resume.concepts && resume.concepts.length > 0 &&
                    <>
                    <h2 className="mb-2 text-lg font-bold tracking-widest text-gray-700 print:font-normal">
                        CONCEPTS
                    </h2>
                    <section className="mb-0 break-inside-avoid">
                        <section className="mt-1 last:pb-1">
                            <ul className="flex flex-wrap -mb-1 font-bold leading-relaxed text-md -mr-1.6">
                                {resume.concepts.map((concept, ix) => {
                                    return (<li key={ix} className="p-1.5 mb-1 leading-relaxed text-white bg-gray-800 mr-1.6 print:bg-white print:border-inset">
                                        {concept}
                                    </li>);
                                    })
                                }
                            </ul>
                        </section>
                    </section>
                    </>
                }
                
                
            </section>
        </section>
    );
}