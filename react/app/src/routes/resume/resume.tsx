import { useLoaderData } from 'react-router-dom';
import { getSiteMetadata, getSiteResume } from '../../util/contentUtil';
import { Helmet, HelmetProvider } from 'react-helmet-async';
import { Resume as ResumeModel } from '../../model/resume';
import ResumeContact from './_resume-contact';
import ResumeSummary from './_resume-summary';
import ResumeSkills from './_resume-skills';
import ResumeEducation from './_resume-education';
import ResumeExperience from './_resume-experience';
import { SiteMetaData } from '../../model/sitemetadata';

function Resume() {
    const {resume, meta} = useLoaderData() as { resume: ResumeModel, meta: SiteMetaData };
    const title = `${meta.siteName} | Resume`;
    const keywords = `resume,cv,${resume.name}`;
    

    function print(){
        window.print();
        return false;
    }
  return (
    <HelmetProvider>
      <Helmet prioritizeSeoTags>
        <title>{title}</title>
        <meta name="description" content="Senior Software Engineer" />
        <meta name="keywords" content={keywords} />
        <meta name="author" content={resume.name} />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <link
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"
        rel="stylesheet"
        />
        <link
        rel="preload"
        href="./fonts/Jost-Medium.woff2"
        as="font"
        crossOrigin="anonymous"
        />
        <link rel="preconnect" href="https://fonts.googleapis.com" />
        <link rel="preconnect" href="https://fonts.gstatic.com" crossOrigin="anonymous" />
        <link
        href="https://fonts.googleapis.com/css2?family=Jost:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap"
        rel="stylesheet"
        />
        <link rel="stylesheet" href="./resume.css" />
      </Helmet>  
      <main className="font-jost hyphens-manual">
        {/* <!-- Page --------------------------------------------------------------------------------------------------------> */}
        <section className="p-3 my-auto mx-auto max-w-3xl bg-gray-100 rounded-2xl border-4 border-gray-700 sm:p-9 md:p-16 lg:mt-6 print:border-0 page print:max-w-letter print:max-h-letter print:mx-0 print:my-o xsm:p-8 print:bg-white md:max-w-letter md:h-letter lg:h-letter">
            {/* <!-- Name ----------------------------------------------------------------------------------------------------> */}
            <header className="inline-flex justify-between items-baseline mb-2 w-full align-top border-b-4 border-gray-300">
            <section className="block">
                <h1 className="mb-0 text-5xl font-bold text-gray-700">
                {resume.name}
                <a className="print:hidden nav" href="" onClick={print}>
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-7 h-7 inline-block mb-2">
                    <path strokeLinecap="round" strokeLinejoin="round" d="M9 8.25H7.5a2.25 2.25 0 0 0-2.25 2.25v9a2.25 2.25 0 0 0 2.25 2.25h9a2.25 2.25 0 0 0 2.25-2.25v-9a2.25 2.25 0 0 0-2.25-2.25H15M9 12l3 3m0 0 3-3m-3 3V2.25"/>
                    </svg>
                </a>
                </h1>
                {/* <!--Job Title---------------------------------------------------------------------------------------------------------> */}
                <h2 className="m-0 ml-2 text-2xl font-semibold text-gray-700 leading-snugish">{resume.title}</h2>
                {/* <!--Location ---------------------------------------------------------------------------------------------------------> */}

                <h3
                className="m-0 mt-2 ml-2 text-xl font-semibold text-gray-500 leading-snugish"
                >
                {resume.location}
                </h3>
            </section>
            {/* <!--   Initials Block         --> */}
            <section
                className="justify-between py-6 px-3 mt-0 mb-5 text-4xl font-black leading-none text-white bg-gray-700 initials-container print:bg-black"
                >
                    {resume.initials.map((init, index) => (
                        <section key={index} className="text-center initial">{init}</section>
                        ))}
            </section>
            </header>
            {/* <!-- Column --------------------------------------------------------------------------------------------------> */}
            <section className="col-gap-8 print:col-count-2 print:h-letter-col-full col-fill-balance md:col-count-2 md:h-letter-col-full">
            <section className="flex-col">
                <ResumeContact resume={resume}/>
                <ResumeSummary resume={resume}/>
                <ResumeSkills resume={resume}/>
                <ResumeEducation resume={resume}/>
                <ResumeExperience resume={resume}/>
            </section>
            {/* <!-- end Column --> */}
            </section>
            {/* <!-- end Page --> */}
        </section>
        </main>
    </HelmetProvider>
  );
}

export async function loader({ params } : any) {
    const content = { 
        resume: await getSiteResume(), 
        meta: await getSiteMetadata()
    };
    if (!content) throw new Response("", { status: 404 });
    return content;
}


export default Resume;
