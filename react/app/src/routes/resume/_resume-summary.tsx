import { Resume } from "../../model/resume";
import ReactMarkdown from 'react-markdown';


export default function ResumeSummary({resume}: {resume: Resume}){

    return (
        <>
            {!!resume.education && resume.education.length > 0 &&
                <section className="summary pb-2 pb-4 mt-0 border-b-4 border-gray-300 first:mt-0">
                    {/* <!-- To keep in the same column --> */}
                    <section className="break-inside-avoid">
                        <h2
                            className="mb-2 text-xl font-bold tracking-widest text-gray-700 print:font-normal">
                            SUMMARY
                        </h2>
              
                        <section className="mb-2 break-inside-avoid">
                            {resume.summary.map((summary, ix) => {
                                return (<ReactMarkdown key={ix}>{summary.content}</ReactMarkdown>);
                            })}
                        </section>
                    </section>
                </section>
            }
        </>
    );
}