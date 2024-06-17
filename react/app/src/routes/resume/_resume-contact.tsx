import { Resume } from "../../model/resume";


export default function ResumeContact({resume}: {resume: Resume}){

    return (
        <section className="pb-2 mt-4 mb-0 first:mt-0">
            {/* <!-- To keep in the same column --------------------------------------------------------------------------> */}
            <section className="break-inside-avoid">
                <section className="pb-4 mb-2 border-b-4 border-gray-300 break-inside-avoid">
                    <ul className="pr-7 list-inside">
                        {!!resume.website &&
                            <li
                                className="mt-1 leading-normal text-black text-gray-500 transition duration-100 ease-in hover:text-gray-700 text-md print:">
                                <a href={resume.website} className="group nav">
                                    <span
                                        className="mr-2 text-lg font-semibold text-gray-700 leading-snugish">
                                        Website:
                                    </span>
                                    {resume.cleanWebsite}
                                    <span
                                        className="inline-block font-normal text-black text-gray-500 transition duration-100 ease-in group-hover:text-gray-700 print:text-black print:">
                                        ↗
                                    </span>
                                </a>
                            </li>
                        }
                        {!!resume.gitHub &&
                            <li
                                className="mt-1 leading-normal text-gray-500 transition duration-100 ease-in hover:text-gray-700 text-md">
                                <a href={resume.gitHub} className="group nav">
                                    <span
                                        className="mr-5 text-lg font-semibold text-gray-700 leading-snugish">
                                        Github:
                                    </span>
                                    {resume.gitHubUsername}
                                    <span
                                        className="inline-block font-normal text-black text-gray-500 transition duration-100 ease-in group-hover:text-gray-700 print:text-black print:">
                                        ↗
                                    </span>
                                </a>
                            </li>
                        }
                        <li
                            className="mt-1 leading-normal text-gray-500 transition duration-100 ease-in hover:text-gray-700 text-md">
                            <a href={"mailto:" + resume.email} className="group nav">
                                <span
                                    className="mr-8 text-lg font-semibold text-gray-700 leading-snugish">
                                    Email:
                                </span>
                                {resume.email}
                                <span
                                    className="inline-block font-normal text-gray-500 transition duration-100 ease-in group-hover:text-gray-700 print:text-black">
                                    ↗
                                </span>
                            </a>
                        </li>
                        {resume.phone &&
                            <li
                                className="mt-1 leading-normal text-gray-500 transition duration-100 ease-in hover:text-gray-700 text-md">
                                <a className="group nav" href={"tel:" + resume.phone}>
                                    <span
                                        className="mr-5 text-lg font-semibold text-gray-700 leading-snugish">
                                        Phone:
                                    </span>
                                    {resume.phone}
                                </a>
                            </li>
                        }
                    </ul>
                </section>
            </section>
            </section>
    );
}