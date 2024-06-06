import { Resume } from "../../model/resume";
import ReactMarkdown from 'react-markdown';


export default function ResumeEducation({resume}: {resume: Resume}){

    return (
        <>
            {!!resume.education && resume.education.length > 0 &&
              <section className="education pb-0 mt-2 border-b-4 border-gray-300 first:mt-0 break-inside-avoid">
                  {/* <!-- To keep in the same column --> */}
                  <section className="break-inside-avoid">
                      <h2
                          className="mb-2 text-lg font-bold tracking-widest text-gray-700 print:font-normal">
                          EDUCATION
                      </h2>
                      {                        
                        resume.education.map((edu, ix) => {
                          var first = ix == 0;
                          var last = ix == resume.education.length - 1;
                          return (
                          <section key={ix} className={"break-inside-avoid" + (first ? " mt-2" : " mt-4") + (last ? " pb-4 mb-4" : " border-b-2")}>
                              <header>
                                  <h3
                                      className="text-lg font-semibold text-gray-700 leading-snugish">
                                      {edu.heading}
                                  </h3>
                                  <p className="leading-normal text-gray-500 text-md">
                                      {edu.subHeading}
                                  </p>
                              </header>
                              <ReactMarkdown>{edu.content}</ReactMarkdown>
                          </section>);
                        
                      })}
                  </section>
              </section>
            }
        </>
    );
}